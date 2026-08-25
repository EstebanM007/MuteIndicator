# Contributing to MuteIndicator

¡Agradecemos tu interés en contribuir a MuteIndicator! Este documento proporciona directrices para contribuir.

## Código de Conducta

Por favor, sé respetuoso y profesional. Queremos mantener una comunidad inclusiva y amable.

## Cómo Reportar Bugs

1. Verifica que el bug no haya sido reportado ya en [Issues](https://github.com/tu-usuario/MuteIndicator/issues)
2. Si no existe, abre un nuevo Issue con:
   - Descripción clara del problema
   - Pasos para reproducir
   - Comportamiento esperado vs actual
   - Tu entorno (Windows 10/11, dispositivo de audio, etc.)
   - Screenshots si es aplicable

## Solicitudes de Características

1. Abre un Issue con el tag `enhancement`
2. Describe claramente qué característica deseas
3. Explica por qué sería útil
4. Proporciona ejemplos de cómo funcionaría

## Proceso de Contribución

1. **Fork el repositorio**
   ```bash
   git clone https://github.com/tu-usuario/MuteIndicator.git
   ```

2. **Crea una rama para tu feature**
   ```bash
   git checkout -b feature/nombre-descriptivo
   ```

3. **Realiza tus cambios**
   - Sigue los estándares de código C#
   - Añade comentarios explicativos
   - Mantén la compatibilidad hacia atrás cuando sea posible

4. **Compila y prueba**
   ```bash
   cd MuteIndicatorCSharp
   dotnet build -c Release
   dotnet publish -c Release -o publish
   ```

5. **Haz commit de tus cambios**
   ```bash
   git add .
   git commit -m "Descripción clara de los cambios"
   ```

6. **Push a tu fork**
   ```bash
   git push origin feature/nombre-descriptivo
   ```

7. **Abre un Pull Request**
   - Describe qué cambios realizaste
   - Explica por qué son necesarios
   - Referencia cualquier Issue relacionado

## Estándares de Código

- Usa nomenclatura clara en inglés
- Añade comentarios XML para métodos públicos
- Sigue las convenciones de C# y .NET
- Mantén las líneas de código razonablemente cortas
- Asegúrate de que el código se compila sin warnings

## Configuración de Desarrollo

### Requisitos
- Visual Studio 2022 / VS Code
- .NET 8.0 SDK
- Git

### Setup Inicial
```bash
# Clonar repositorio
git clone https://github.com/tu-usuario/MuteIndicator.git
cd MuteIndicator

# Restaurar dependencias
cd MuteIndicatorCSharp
dotnet restore
```

## Tipos de Contribuciones

### 🐛 Bug Fixes
- Reporta el bug en Issues
- Crea una rama: `bugfix/descripcion`
- Incluye pruebas para validar el fix

### ✨ Nuevas Características
- Discute la idea primero en un Issue
- Implementa manteniendo compatibilidad
- Documenta la nueva funcionalidad

### 📚 Documentación
- Mejora README.md
- Actualiza comentarios de código
- Añade ejemplos de uso

### 🧪 Tests
- Mejora la cobertura de pruebas
- Valida funcionalidades existentes

## Preguntas

Si tienes preguntas, puedes:
- Abrir una Issue con el tag `question`
- Consultar la documentación existente
- Revisar Issues cerradas relacionadas

---

¡Gracias por contribuir a MuteIndicator! 🎉
