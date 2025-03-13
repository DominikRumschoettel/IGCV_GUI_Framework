# IGCV GUI Framework

A GUI framework for creating themed Windows Forms applications with Fraunhofer corporate identity.

## Overview

The IGCV GUI Framework provides a theming-capable user interface toolkit for Windows Forms applications. It enables developers to create applications with consistent visual styling while supporting multiple themes, with a primary focus on the Fraunhofer Corporate Identity theme.

## Features

- **Theme-based architecture**: All visual elements follow a consistent theme
- **Compile-time theme selection**: Themes are selected at compile time for optimal performance
- **Fraunhofer Corporate Identity integration**: Colors, fonts, and styles matching Fraunhofer CI guidelines
- **Custom themed controls**: Enhanced versions of standard controls with additional styling capabilities
- **Theme extensibility**: Easily add new themes by extending base classes
- **Error resilience**: Graceful fallbacks for missing fonts or resources
- **Demo applications**: Sample forms showcasing the framework's capabilities

## Getting Started

1. Clone this repository
2. Open the solution file in Visual Studio
3. Build and run the demo launcher

## Project Structure

```
IGCV/
├── GUI/
│   ├── Controls/               # Themed control implementations
│   ├── Themes/                 # Theme definitions and management
│   │   ├── FraunhoferCI/       # Fraunhofer corporate identity theme
│   │   ├── DarkTheme/          # Dark theme example
│   └── Demo/                   # Demo forms showcasing the framework
```

## Usage

### Applying Themes

```csharp
// Apply the current theme to a form
ThemeManager.ApplyThemeToContainer(this);

// Apply a specific theme
ThemeManager.SetTheme("Dark Theme");
ThemeManager.ApplyThemeToContainer(this);
```

### Using Themed Controls

```csharp
// Create a primary button
ThemedButton primaryButton = new ThemedButton
{
    Text = "Primary Button",
    ButtonStyle = ButtonStyle.Primary,
    Location = new Point(10, 10),
    Size = new Size(150, 40)
};
```

## Demo Applications

The framework includes several demo applications:

1. **Controls Demo**: Showcases all themed controls with different styles
2. **Theme Integration Sample**: Demonstrates a realistic application layout 
3. **Demo Launcher**: Central hub for accessing all demos

## License

[MIT License](LICENSE)

## Acknowledgements

Developed at Fraunhofer IGCV for internal application development.
