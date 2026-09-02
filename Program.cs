using System;
using System.Windows.Forms;

namespace MuteIndicatorCSharp;

/// <summary>
/// Punto de entrada principal de la aplicacion MuteIndicator.
/// Esta aplicacion muestra un indicador visual en la esquina inferior derecha de la pantalla
/// que se activa cuando el microfono esta silenciado.
/// </summary>
internal static class Program
{
    private const string SingleInstanceMutexName = "Global\\IconMuteSingleInstance";

    /// <summary>
    /// Punto de entrada principal de la aplicacion.
    /// Se configura como STAThread (Single Threaded Apartment) como es requerido para Windows Forms.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        using var mutex = new Mutex(true, SingleInstanceMutexName, out var createdNew);

        if (!createdNew)
        {
            return;
        }

        try
        {
            // Inicializar configuracion de la aplicacion Windows Forms
            ApplicationConfiguration.Initialize();
            
            // Crear y ejecutar la ventana principal del indicador
            Application.Run(new IndicatorForm());
        }
        catch (Exception ex)
        {
            // Mostrar mensaje de error si algo falla durante la inicializacion
            MessageBox.Show(
                $"Error al iniciar la aplicacion MuteIndicator:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
