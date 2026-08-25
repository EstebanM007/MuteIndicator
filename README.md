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

Requiere Windows 10/11 x64. No necesita instalar .NET ni ningun componente adicional: el ejecutable es portable.

```powershell
.\MuteIndicator.exe
```

### Si Windows bloquea el ejecutable

El ejecutable es portable y no esta firmado digitalmente. Por eso Windows puede mostrar una advertencia o bloquearlo al descargarlo desde Internet.

Si aparece la opcion **Desbloquear**:

1. Haz clic derecho en `MuteIndicator.exe` y selecciona **Propiedades**.
2. En la pestaña **General**, marca **Desbloquear**.
3. Pulsa **Aplicar** y ejecuta el archivo otra vez.

Tambien puedes hacerlo desde PowerShell:

```powershell
Unblock-File -Path ".\MuteIndicator.exe"
```

Si **Control inteligente de aplicaciones** bloquea el archivo, abre **Seguridad de Windows > Control de aplicaciones y explorador > Control inteligente de aplicaciones** y selecciona **Desactivado**. Esta proteccion puede requerir restablecer o reinstalar Windows para volver a activarse, por lo que solo debes cambiarla si confias en el archivo descargado.

El bloqueo no significa que falte .NET: el ejecutable incluye el runtime necesario y esta compilado para Windows 10/11 x64.

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
| Runtime | Incluido en el ejecutable |
| Intervalo de comprobacion | 100 ms |
| Tamano del ejecutable | aproximadamente 171 MB |

## Desarrollo

Necesitas el SDK de .NET 8 y Windows. Para compilar desde el codigo fuente:

```powershell
git clone https://github.com/EstebanM007/MuteIndicator.git
cd MuteIndicator
dotnet build MuteIndicatorCSharp.csproj -c Release
dotnet publish MuteIndicatorCSharp.csproj -c Release -o publish
.\publish\MuteIndicator.exe
```

El proyecto usa [NAudio](https://github.com/naudio/NAudio) para consultar el dispositivo de audio predeterminado.

## Contribuir y soporte

Consulta [CONTRIBUTING.md](CONTRIBUTING.md) para contribuir o abre un [issue](https://github.com/EstebanM007/MuteIndicator/issues) para reportar un error o proponer una mejora.

Consulta el [CHANGELOG.md](CHANGELOG.md) para ver el historial de versiones.

## Licencia

Distribuido bajo la licencia [MIT](LICENSE).
