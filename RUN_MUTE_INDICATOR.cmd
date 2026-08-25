@echo off
REM MuteIndicator - Gaming Edition Launcher
REM Ejecutor rápido para la aplicación

cls
echo.
echo ============================================
echo   MuteIndicator - Gaming Edition
echo   Indicador Visual de Microfono
echo ============================================
echo.
echo Iniciando aplicacion...
echo.

REM Obtener la ruta del archivo actual
cd /d "%~dp0"

REM Ejecutar el programa
if exist "publish\MuteIndicator.exe" (
    start "" "publish\MuteIndicator.exe"
    echo Aplicacion iniciada correctamente.
    echo.
    echo Busca el indicador en la esquina inferior derecha de tu pantalla.
    echo.
    echo Presiona tu botón de MUTE para probar:
    echo - Rojo = Microfono silenciado
    echo - Sin punto = Microfono activo
    echo.
    timeout /t 2 /nobreak
) else (
    echo ERROR: No se encontró MuteIndicator.exe en la carpeta publish\
    echo.
    echo Por favor, asegúrate de compilar primero:
    echo   dotnet publish -c Release -o publish
    echo.
    pause
)
