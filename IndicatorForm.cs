using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace MuteIndicatorCSharp;

/// <summary>
/// Formulario que muestra un indicador visual cuando el micrófono está silenciado.
/// La aplicación se ejecuta como una ventana sin bordes en la esquina inferior derecha de la pantalla,
/// con acceso a través de un ícono en la bandeja del sistema.
/// </summary>
internal sealed class IndicatorForm : Form
{
    private const int HotkeyId = 1;
    private bool hotkeyRegistered;

    [Flags]
    private enum KeyModifiers : uint
    {
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Win = 0x0008
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    /// <summary>Temporizador para verificar periódicamente el estado del micrófono.</summary>
    private readonly System.Windows.Forms.Timer timer;
    
    /// <summary>Ícono en la bandeja del sistema para acceder al menú contextual.</summary>
    private readonly NotifyIcon trayIcon;
    
    /// <summary>Menú contextual con opciones para mostrar, ocultar y salir de la aplicación.</summary>
    private readonly ContextMenuStrip trayMenu;
    
    /// <summary>Indica si el indicador visual está siendo mostrado actualmente.</summary>
    private bool indicatorVisible;
    
    /// <summary>Dispositivo de micrófono en caché para evitar enumeraciones repetidas.</summary>
    private MMDevice? cachedMicrophone;
    
    /// <summary>Enumerador de dispositivos de audio en caché.</summary>
    private MMDeviceEnumerator? enumerator;
    
    /// <summary>Brush para dibujado optimizado (reutilizado).</summary>
    private SolidBrush? indicatorBrush;
    
    /// <summary>Intervalo de verificación del estado del micrófono en milisegundos (100ms para gaming).</summary>
    private const int CheckIntervalMs = 100;

    /// <summary>
    /// Inicializa una nueva instancia de la clase IndicatorForm.
    /// Configura la ventana sin bordes, el ícono de la bandeja del sistema y el temporizador de verificación.
    /// </summary>
    public IndicatorForm()
    {
        // Configurar propiedades del formulario para que sea una ventana pequeña sin bordes
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;                                    // No mostrar en la barra de tareas
        StartPosition = FormStartPosition.Manual;                  // Posicionamiento manual
        TopMost = true;                                           // Siempre encima de otras ventanas
        BackColor = Color.Magenta;                                // Color de fondo (se hará transparente)
        TransparencyKey = Color.Magenta;                          // Magenta será transparente
        ClientSize = new Size(26, 26);                            // Tamaño pequeño (punto indicador)
        DoubleBuffered = true;                                    // Evitar parpadeos
        SetStyle(ControlStyles.SupportsTransparentBackColor, true); // Permitir fondos transparentes

        // Posicionar en la esquina inferior derecha de la pantalla principal
        var workArea = Screen.PrimaryScreen!.WorkingArea;
        Location = new Point(workArea.Right - Width - 18, workArea.Bottom - Height - 18);

        // Crear menú contextual para la bandeja del sistema
        trayMenu = new ContextMenuStrip();
        trayMenu.Items.Add("Mostrar indicador", null, (_, _) => SetIndicator(true));
        trayMenu.Items.Add("Ocultar indicador", null, (_, _) => SetIndicator(false));
        trayMenu.Items.Add(new ToolStripSeparator());
        trayMenu.Items.Add("Salir", null, (_, _) => Application.Exit());

        // Configurar ícono en la bandeja del sistema con el icono personalizado del ejecutable
        trayIcon = new NotifyIcon
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? SystemIcons.Information,
            Text = "Indicador de mute",
            ContextMenuStrip = trayMenu,
            Visible = true
        };

        // Inicializar enumerador de dispositivos de audio
        enumerator = new MMDeviceEnumerator();
        indicatorBrush = new SolidBrush(Color.FromArgb(235, 220, 30, 40));
        
        // Cachear el micrófono por defecto
        try
        {
            cachedMicrophone = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
        }
        catch { }
        
        // Configurar temporizador para verificar el estado del micrófono cada 100ms (optimizado para gaming)
        timer = new System.Windows.Forms.Timer { Interval = CheckIntervalMs };
        timer.Tick += (_, _) => CheckMicrophone();
        timer.Start();

        // Registrar acceso directo global Alt + M para mutear o desmutear el micrófono
        RegisterHotKeyIfNeeded();
        
        // Realizar verificación inicial del estado del micrófono
        CheckMicrophone();
    }

    /// <summary>
    /// Evita que la ventana sea activada cuando se muestra, permitiendo que otros elementos mantengan el foco.
    /// </summary>
    protected override bool ShowWithoutActivation => true;

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        RegisterHotKeyIfNeeded();
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == 0x0312 && (int)m.WParam == HotkeyId)
        {
            ToggleMicrophoneMute();
            return;
        }

        base.WndProc(ref m);
    }

    private void RegisterHotKeyIfNeeded()
    {
        if (IsHandleCreated && !hotkeyRegistered)
        {
            hotkeyRegistered = RegisterHotKey(Handle, HotkeyId, (uint)KeyModifiers.Alt, (uint)Keys.M);
        }
    }

    private void ToggleMicrophoneMute()
    {
        try
        {
            cachedMicrophone ??= enumerator?.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            var volume = cachedMicrophone?.AudioEndpointVolume;
            if (volume != null)
            {
                volume.Mute = !volume.Mute;
            }
        }
        catch
        {
            cachedMicrophone = null;
        }
    }

    /// <summary>
    /// Configura parámetros de creación de ventana para hacerla transparente a clics y una ventana de herramienta.
    /// Esto permite que el indicador no interfiera con la interacción del usuario con otras ventanas.
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            var parameters = base.CreateParams;
            // WS_EX_TRANSPARENT (0x20): Permite clics a través de la ventana
            // WS_EX_TOOLWINDOW (0x80): Identifica la ventana como una ventana de herramienta
            parameters.ExStyle |= 0x00000020 | 0x00000080;
            return parameters;
        }
    }

    /// <summary>
    /// Dibuja un círculo rojo suave que representa el estado de silenciamiento del micrófono.
    /// Optimizado con brush cacheado y suavizado de bordes para gaming.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        
        if (indicatorBrush != null)
        {
            // Aplicar suavizado de bordes para mejor calidad visual
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Dibujar el círculo indicador rojo (22x22 píxeles con margen de 2)
            e.Graphics.FillEllipse(indicatorBrush, 2, 2, 22, 22);
        }
    }

    /// <summary>
    /// Verifica el estado actual del micrófono por defecto del sistema.
    /// Optimizado con caché para reducir enumeraciones de dispositivos.
    /// Actualiza la visibilidad del indicador basándose en si el micrófono está silenciado o inactivo.
    /// </summary>
    private void CheckMicrophone()
    {
        try
        {
            // Usar micrófono cacheado para máximo rendimiento
            if (cachedMicrophone == null)
            {
                cachedMicrophone = enumerator?.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            }
            
            // Verificar si el micrófono está silenciado o no está activo
            // Condiciones:
            // 1. Dispositivo no está en estado Active
            // 2. El control de volumen del endpoint tiene Mute activado
            var isMuted = cachedMicrophone?.State != DeviceState.Active || 
                         (cachedMicrophone?.AudioEndpointVolume?.Mute ?? false);
            
            // Actualizar la visibilidad del indicador
            SetIndicator(isMuted);
        }
        catch
        {
            // Invalidar caché si hay error y reintentar en siguiente ciclo
            cachedMicrophone = null;
        }
    }

    /// <summary>
    /// Establece la visibilidad del indicador.
    /// Solo actualiza si el estado ha cambiado para optimizar rendimiento.
    /// </summary>
    /// <param name="visible">Si es true, muestra el indicador; si es false, lo oculta.</param>
    private void SetIndicator(bool visible)
    {
        // Evitar actualizaciones innecesarias si el estado no ha cambiado
        if (indicatorVisible == visible)
            return;

        indicatorVisible = visible;
        if (visible)
            Show();   // Mostrar el indicador rojo
        else
            Hide();   // Ocultar el indicador
    }

    /// <summary>
    /// Libera los recursos utilizados por la instancia de IndicatorForm.
    /// Se asegura de que todos los componentes gestionados se cierren correctamente.
    /// </summary>
    /// <param name="disposing">True si el método fue llamado desde Dispose(); false si fue llamado desde el finalizador.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (IsHandleCreated && hotkeyRegistered)
            {
                UnregisterHotKey(Handle, HotkeyId);
                hotkeyRegistered = false;
            }

            // Liberar recursos gestionados
            timer?.Dispose();              // Detener y liberar el temporizador
            trayIcon?.Dispose();           // Liberar el ícono de la bandeja
            trayMenu?.Dispose();           // Liberar el menú contextual
            cachedMicrophone?.Dispose();   // Liberar el dispositivo de micrófono cacheado
            enumerator?.Dispose();         // Liberar el enumerador de dispositivos
            indicatorBrush?.Dispose();     // Liberar el brush de dibujado
        }

        base.Dispose(disposing);
    }
}
