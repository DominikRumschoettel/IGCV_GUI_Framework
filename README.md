# IGCV GUI Framework

A comprehensive GUI framework for creating themed Windows Forms applications with Fraunhofer corporate identity support.

## Overview

The IGCV GUI Framework provides a robust, theming-capable user interface toolkit for Windows Forms applications. It enables developers to create applications with consistent visual styling while supporting multiple themes, with a primary focus on the Fraunhofer Corporate Identity theme.

## Key Features

- **Theme-based architecture**: All visual elements follow a consistent theme
- **Custom themed controls**: Enhanced versions of standard controls with additional styling capabilities 
- **Themeable controls**: Comprehensive support for common UI controls with proper theming
- **Fraunhofer corporate identity**: Colors, fonts, and styles matching Fraunhofer CI guidelines
- **Multiple theme support**: Support for different visual themes with easy switching
- **Robust error handling**: Graceful fallbacks for missing resources or fonts
- **Demo applications**: Comprehensive control demo pages showcasing all themed controls

## Documentation

Comprehensive documentation is available in the `docs` directory:

- [User Guide](docs/USER_GUIDE.md) - Practical guide for developers using the framework
- [Architecture Overview](docs/ARCHITECTURE.md) - Technical details of the framework architecture
- [Demo Controls Guide](docs/DEMO_CONTROLS.md) - Guide to the control demonstration pages
- [LLM Developer Guide](docs/LLM_DEVELOPER_GUIDE.md) - Detailed guide for Language Model-based development

## Getting Started

### Prerequisites

- Visual Studio 2022 or later
- .NET 8.0 or later
- Windows operating system

### Installation

1. Clone this repository:
   ```
   git clone https://github.com/fraunhofer-igcv/IGCV_GUI_Framework.git
   ```

2. Open the solution file in Visual Studio:
   ```
   IGCV_GUI_Framework.sln
   ```

3. Build the solution:
   ```
   dotnet build
   ```

4. Run the demo application to explore the controls:
   ```
   dotnet run --project IGCV_GUI_Framework/IGCV_GUI_Framework.csproj
   ```

   The demo application will open with a window size of 1200x800 pixels and display several pages showcasing different themed controls:
   - Button Controls - Various button styles and usage examples
   - Input Controls - Text inputs, dropdowns, and selection controls
   - Progress Controls - Progress bars and step indicators
   - Layout Controls - Panel layouts, cards, and dashboard examples

## Project Structure

The framework follows a modular architecture:

```
IGCV_GUI_Framework/
├── IGCV/
│   ├── GUI/
│   │   ├── Controls/               # Themed control implementations
│   │   │   ├── ThemedButton.cs
│   │   │   ├── ThemedPanel.cs
│   │   │   ├── ThemedTextBox.cs
│   │   │   └── ... (other controls)
│   │   ├── IThemeableControl.cs    # Interface for themeable controls
│   │   ├── Themes/                 # Theme management
│   │       ├── ITheme.cs           # Theme interface
│   │       ├── ThemeBase.cs        # Base theme implementation
│   │       ├── ThemeManager.cs     # Theme management
│   │       ├── FraunhoferCI/       # Fraunhofer theme
│   │       │   └── FraunhoferTheme.cs
│   │       └── DarkTheme/          # Dark theme example
│   │           └── DarkTheme.cs
├── Common/                         # Common utilities
│   ├── Controls/                   # Application-level controls
│   │   ├── navigation-bar.cs
│   │   ├── page-container.cs
│   │   └── status-panel.cs
│   └── FraunhoferTheme.cs          # Legacy theme (for compatibility)
├── Interfaces/                     # Application interfaces
│   └── interfaces.cs
└── Pages/                          # Application pages
    ├── page-base.cs                # Base page implementation
    ├── ButtonControlsPage.cs       # Button controls demo page
    ├── InputControlsPage.cs        # Input controls demo page
    ├── ProgressControlsPage.cs     # Progress indicators demo page
    └── LayoutControlsPage.cs       # Layout controls demo page
```

## Usage

### Using the Framework in Your Application

1. Reference the IGCV_GUI_Framework project or assembly in your application.

2. Initialize theming in your application:

```csharp
using IGCV.GUI.Themes;

// In your application startup, e.g., Program.cs or Form_Load
ThemeManager.SetTheme("Fraunhofer CI"); // Or any other registered theme
```

3. Apply themes to your forms:

```csharp
// In your form constructor or Load event
ThemeManager.ApplyThemeToContainer(this);
```

### Using Themed Controls

```csharp
using IGCV.GUI.Controls;

// Create a themed button with primary style
ThemedButton primaryButton = new ThemedButton
{
    Text = "Primary Action",
    ButtonStyle = ButtonStyle.Primary,
    Location = new Point(20, 20),
    Size = new Size(150, 40)
};
this.Controls.Add(primaryButton);

// Create a themed panel
ThemedPanel panel = new ThemedPanel
{
    Location = new Point(20, 70),
    Size = new Size(300, 200),
    BorderColor = ThemeManager.CurrentTheme.BorderColor,
    BorderWidth = 1,
    CornerRadius = ThemeManager.CurrentTheme.CornerRadius
};
this.Controls.Add(panel);
```

### Creating a Custom Theme

1. Create a new class that inherits from `ThemeBase`:

```csharp
using IGCV.GUI.Themes;
using System.Drawing;

public class MyCustomTheme : ThemeBase
{
    #region Singleton

    private static MyCustomTheme _instance;
    public static MyCustomTheme Instance => _instance ?? (_instance = new MyCustomTheme());

    private MyCustomTheme() { }

    #endregion

    // Implement required properties
    public override string Name => "My Custom Theme";
    public override Color PrimaryColor => Color.FromArgb(0, 120, 215);
    public override Color SecondaryColor => Color.FromArgb(230, 230, 230);
    public override Color AccentColor => Color.FromArgb(255, 140, 0);
    public override Color BackgroundColor => Color.White;
    public override Color TextOnLightColor => Color.Black;
    public override Color TextOnDarkColor => Color.White;
    public override Color SuccessColor => Color.Green;
    public override Color WarningColor => Color.Orange;
    public override Color ErrorColor => Color.Red;
    public override Color BorderColor => Color.LightGray;

    // Font properties
    public override Font HeaderFont => new Font("Segoe UI", 16f, FontStyle.Bold);
    public override Font SubHeaderFont => new Font("Segoe UI", 14f, FontStyle.Regular);
    public override Font BodyFont => new Font("Segoe UI", 10f, FontStyle.Regular);
    public override Font ButtonFont => new Font("Segoe UI", 10f, FontStyle.Regular);
    public override Font SmallFont => new Font("Segoe UI", 8f, FontStyle.Regular);

    // Shape properties
    public override int CornerRadius => 3;
    public override int BorderWidth => 1;

    // Override styling methods as needed
}
```

2. Register your theme with the `ThemeManager`:

```csharp
ThemeManager.RegisterTheme(MyCustomTheme.Instance);
ThemeManager.SetTheme("My Custom Theme");
```

For more detailed usage instructions and examples, see the [User Guide](docs/USER_GUIDE.md).

## Contributing

Contributions are welcome! Please feel free to submit pull requests.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Make your changes
4. Commit your changes (`git commit -m 'Add some feature'`)
5. Push to the branch (`git push origin feature/my-feature`)
6. Create a new Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

Developed at Fraunhofer IGCV for internal application development.
