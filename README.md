# MuteIndicator 🎤

[![Build and Release](https://github.com/tu-usuario/MuteIndicator/workflows/Build%20and%20Release/badge.svg)](https://github.com/tu-usuario/MuteIndicator/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub Release](https://img.shields.io/github/v/release/tu-usuario/MuteIndicator)](https://github.com/tu-usuario/MuteIndicator/releases)
[![Downloads](https://img.shields.io/github/downloads/tu-usuario/MuteIndicator/total.svg)](https://github.com/tu-usuario/MuteIndicator/releases)

Una aplicación Windows ligera y rápida que muestra un indicador visual en tiempo real cuando el micrófono está silenciado.

**Perfecta para:** 🎮 Gamers • 🎙️ Streamers • 💼 Profesionales en videoconferencias

## ✨ Características

- 🔴 **Indicador Visual Discreto**: Pequeño círculo rojo en esquina inferior derecha
- ⚡ **Ultra Rápido**: Detección en ~100ms (optimizado para gaming)
- 💾 **Mínimo Rendimiento**: CPU < 0.5%, RAM 40-50 MB
- 📦 **Portable**: Ejecutable único (1.36 MB) sin dependencias externas
- 🖱️ **No Intrusivo**: Transparente a clics, no interfiere con otras aplicaciones
- 🔧 **Sin Configuración**: Funciona directamente al ejecutar
- 📌 **Acceso desde Bandeja**: Menú contextual para controlar la aplicación

## 🚀 Inicio Rápido

### Descarga
1. Ve a [Releases](https://github.com/tu-usuario/MuteIndicator/releases)
2. Descarga `MuteIndicator.exe`
3. ¡Listo! Ejecuta y disfruta

### Uso
```bash
# Ejecutar directamente
MuteIndicator.exe

# O desde terminal
.\MuteIndicator.exe
```

### Autoejecutar en Windows
Coloca `MuteIndicator.exe` en tu carpeta de Inicio:
```powershell
# Windows + R → shell:startup
$source = "ruta\a\MuteIndicator.exe"
$dest = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\Startup\"
Copy-Item $source $dest
```

## 📊 Especificaciones

### Componentes Principales

#### `IndicatorForm.cs`
- **Clase**: `IndicatorForm` (sealed)
- **Herencia**: `Form`
- **Responsabilidades**:
  - Crear una ventana sin bordes y transparente
  - Monitorear el estado del micrófono cada 100ms
  - Mostrar/ocultar el indicador basado en el estado
  - Gestionar el menú de la bandeja del sistema
  - Dibujar el círculo rojo del indicador

#### `Program.cs`
- **Clase**: `Program` (static)
- **Responsabilidades**:
  - Punto de entrada de la aplicación
  - Inicialización de Windows Forms
  - Manejo de excepciones durante el startup

### Optimizaciones de Rendimiento

- **Cache de Dispositivos**: Almacena en caché el micrófono para evitar enumeraciones repetidas
- **Brush Reutilizable**: Usa el mismo brush para todas las operaciones de dibujado
- **ReadyToRun (R2R)**: Compilación anticipada para startup más rápido
- **TieredCompilation**: Optimización dinámica durante la ejecución

### Arquitectura de Detección de Audio

La aplicación utiliza NAudio para:
1. Obtener el dispositivo de captura de audio por defecto
2. Verificar el estado del dispositivo (Active/Inactive)
3. Verificar el estado de mute del volumen
4. Actualizar la visibilidad cada 100ms

## � Especificaciones

| Métrica | Valor |
|---------|-------|
| **Tamaño** | 1.36 MB |
| **CPU** | < 0.5% |
| **RAM** | 40-50 MB |
| **Latencia** | ~100-150ms |
| **Intervalo** | 100ms |
| **Framework** | .NET 8.0 |
| **Requisitos** | Windows 10/11 |

## 🔧 Requisitos de Desarrollo

### Para Usar
- Windows 10 o posterior
- Micrófono conectado y configurado
- Nada más - ¡ejecutable standalone!

### Para Compilar
- Visual Studio 2022 / VS Code / JetBrains Rider
- .NET 8.0 SDK
- Git (opcional, para contribuciones)

## 📦 Instalación desde Fuente

```bash
# Clonar repositorio
git clone https://github.com/tu-usuario/MuteIndicator.git
cd MuteIndicator

# Compilar Release
cd MuteIndicatorCSharp
dotnet build -c Release

# Publicar ejecutable
dotnet publish -c Release -o publish

# Ejecutar
.\publish\MuteIndicator.exe
```

## 📚 Documentación

- [QUICKSTART.md](QUICKSTART.md) - Guía de inicio rápido
- [GAMING_EDITION.md](GAMING_EDITION.md) - Optimizaciones para gaming
- [CONTRIBUTING.md](CONTRIBUTING.md) - Cómo contribuir
- [CHANGELOG.md](CHANGELOG.md) - Historial de cambios
- [RELEASE_NOTES.md](RELEASE_NOTES.md) - Notas técnicas de versión

## 🎮 Casos de Uso

### Gaming
Mantén un indicador visual discreto de tu micrófono mientras juegas. Perfecto para:
- Gaming competitivo
- Jugabilidad multiplayer
- Evitar hablar mientras estás en mute

### Streaming
Streamers pueden monitorear el estado de su micrófono sin perder el foco del contenido.

### Videoconferencias
Indicador permanente para saber si estás en mute durante reuniones Zoom, Teams, etc.

## 🐛 Reportar Bugs

¿Encontraste un bug? ¡Nos gustaría saberlo!

1. Abre un [Issue](https://github.com/tu-usuario/MuteIndicator/issues)
2. Incluye:
   - Descripción clara del problema
   - Pasos para reproducir
   - Tu sistema (Windows 10/11, hardware)
   - Screenshots si es aplicable

## 💡 Sugerencias de Mejora

¿Tienes una idea para mejorar MuteIndicator?

1. Abre un [Issue](https://github.com/tu-usuario/MuteIndicator/issues) con tag `enhancement`
2. Describe tu idea
3. Explica el caso de uso
4. ¡Únete a la comunidad!

## 🤝 Contribuir

¡Las contribuciones son bienvenidas! Ver [CONTRIBUTING.md](CONTRIBUTING.md) para detalles.

### Proceso Rápido
1. Fork el repositorio
2. Crea una rama: `git checkout -b feature/mi-feature`
3. Commit cambios: `git commit -am 'Añade mi feature'`
4. Push a la rama: `git push origin feature/mi-feature`
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la licencia [MIT](LICENSE) - ver archivo LICENSE para detalles.

## 🙋 Soporte

- 📖 Consulta la [documentación](README.md)
- 🔍 Busca en [Issues existentes](https://github.com/tu-usuario/MuteIndicator/issues)
- 💬 Abre un nuevo Issue si no encuentras tu respuesta

## 🎉 Agradecimientos

- [NAudio](https://github.com/naudio/NAudio) - Librería de audio
- .NET Foundation - Framework
- Comunidad open source

---

## 📊 Estadísticas del Proyecto

![GitHub Stars](https://img.shields.io/github/stars/tu-usuario/MuteIndicator?style=social)
![GitHub Forks](https://img.shields.io/github/forks/tu-usuario/MuteIndicator?style=social)
![GitHub Issues](https://img.shields.io/github/issues/tu-usuario/MuteIndicator)
![GitHub PRs](https://img.shields.io/github/issues-pr/tu-usuario/MuteIndicator)

---

**Versión**: 1.0.0  
**Última actualización**: 2026-08-24  
**Estado**: ✅ Production Ready
