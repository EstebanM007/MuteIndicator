# MuteIndicator

[![Build](https://github.com/EstebanM007/MuteIndicator/actions/workflows/build.yml/badge.svg)](https://github.com/EstebanM007/MuteIndicator/actions/workflows/build.yml)
[![Latest release](https://img.shields.io/github/v/release/EstebanM007/MuteIndicator)](https://github.com/EstebanM007/MuteIndicator/releases/latest)
[![License](https://img.shields.io/badge/license-MIT-yellow.svg)](LICENSE)

Aplicacion Windows ligera que muestra un indicador visual cuando el microfono esta silenciado. Pensada para gaming, streaming y videoconferencias.

## Caracteristicas

- Indicador rojo discreto en la esquina inferior derecha.
- Actualizacion del estado aproximadamente cada 100 ms.
- Ventana transparente a los clics y siempre visible.
- Menu en la bandeja del sistema para mostrar, ocultar o salir.
- Ejecutable de un solo archivo para Windows x64.

## Descarga y uso

Descarga `MuteIndicator.exe` desde la [ultima release](https://github.com/EstebanM007/MuteIndicator/releases/latest) y ejecutalo.

Requiere Windows 10/11 y [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0/runtime). No necesita instalacion: el ejecutable es portable.

```powershell
.\MuteIndicator.exe
```

Para iniciar con Windows, presiona `Win + R`, escribe `shell:startup` y copia alli el ejecutable. Tambien puedes hacerlo con PowerShell:

```powershell
$source = "ruta\a\MuteIndicator.exe"
$dest = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\Startup\"
Copy-Item $source $dest
```

## Requisitos

| Metrica | Valor |
|---------|-------|
| Sistema | Windows 10/11 x64 |
| Runtime | .NET 8 Desktop Runtime |
| Intervalo de comprobacion | 100 ms |
| Tamano del ejecutable | aproximadamente 1.4 MB |

## Desarrollo

Necesitas el SDK de .NET 8 y Windows. Para compilar desde el codigo fuente:

```powershell
git clone https://github.com/EstebanM007/MuteIndicator.git
cd MuteIndicator
dotnet build MuteIndicatorCSharp/MuteIndicatorCSharp.csproj -c Release
dotnet publish MuteIndicatorCSharp/MuteIndicatorCSharp.csproj -c Release -o publish
.\publish\MuteIndicator.exe
```

El proyecto usa [NAudio](https://github.com/naudio/NAudio) para consultar el dispositivo de audio predeterminado.

## Contribuir y soporte

Consulta [CONTRIBUTING.md](CONTRIBUTING.md) para contribuir o abre un [issue](https://github.com/EstebanM007/MuteIndicator/issues) para reportar un error o proponer una mejora.

Consulta el [CHANGELOG.md](CHANGELOG.md) para ver el historial de versiones.

## Licencia

Distribuido bajo la licencia [MIT](LICENSE).
