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
- Atajo global de teclado: Alt + M para mutear o desmutear el microfono sin sacar foco del juego o la reunion.
- Funcionamiento discreto en segundo plano, pensado para reuniones, streaming y gaming.
- Codigo fuente C# para Windows Forms y .NET 8.

## Requisitos

Necesitas Windows 10/11 x64 y el [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0/runtime). Para compilar necesitas el [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

Instala el **Desktop Runtime** para ejecutar la aplicacion. Instala tambien el **SDK** si quieres compilarla.

## Ejecutar desde el codigo

Clona el repositorio, entra en la carpeta y ejecuta:

```powershell
git clone https://github.com/EstebanM007/MuteIndicator.git
cd MuteIndicator
dotnet restore
dotnet run -c Release
```

La aplicacion se muestra en la esquina inferior derecha y queda disponible en la bandeja del sistema.

### Uso rapido en segundo plano

- Mantiene el programa ejecutandose en la bandeja del sistema.
- El indicador aparece solo cuando el microfono queda silenciado.
- Puedes pulsar Alt + M desde cualquier aplicacion para alternar el estado del microfono sin perder el foco.
- Es util para reuniones, videollamadas, juegos y streaming.

## Crear tu propio ejecutable

Con el SDK de .NET 8 instalado, ejecuta:

```powershell
dotnet publish MuteIndicatorCSharp.csproj -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true -o publish
```

El ejecutable generado usa el icono `BotonR.ico` y se genera como `publish\MuteIndicator.exe`. Si quieres usar el nombre final del proyecto, puedes renombrarlo a `IconMute.exe` para dejarlo más claro. Para ejecutarlo:

```powershell
\.\publish\MuteIndicator.exe
```

Este ejecutable utiliza el **.NET 8 Desktop Runtime** instalado en el equipo. Si Windows bloquea un ejecutable generado localmente, usa el codigo que tu mismo compilaste y revisa la configuracion de seguridad de tu equipo; este repositorio no distribuye ejecutables firmados.

Para iniciar tu ejecutable con Windows, presiona `Win + R`, escribe `shell:startup` y copia alli un acceso directo a `publish\MuteIndicator.exe`.

| Metrica | Valor |
|---------|-------|
| Sistema | Windows 10/11 x64 |
| Runtime | .NET 8 Desktop Runtime |
| Intervalo de comprobacion | 100 ms |

El proyecto usa [NAudio](https://github.com/naudio/NAudio) para consultar el dispositivo de audio predeterminado.

## Contribuir y soporte

Consulta [CONTRIBUTING.md](CONTRIBUTING.md) para contribuir o abre un [issue](https://github.com/EstebanM007/MuteIndicator/issues) para reportar un error o proponer una mejora.

Consulta el [CHANGELOG.md](CHANGELOG.md) para ver el historial de versiones.

## Licencia

Distribuido bajo la licencia [MIT](LICENSE).
