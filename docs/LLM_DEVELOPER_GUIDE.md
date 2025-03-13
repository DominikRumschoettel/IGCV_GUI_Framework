# IGCV GUI Framework - LLM Developer Guide

This comprehensive guide is designed for Language Model-based development and maintenance of the IGCV GUI Framework. It provides all the necessary information to understand, modify, and extend the framework.

## Related Documentation
- [README](../README.md) - Main project documentation and quick start
- [Architecture Overview](ARCHITECTURE.md) - Technical details of the framework architecture
- [User Guide](USER_GUIDE.md) - Practical guide for developers using the framework

## Table of Contents

1. [Project Overview](#project-overview)
2. [Core Architecture](#core-architecture)
3. [Theme System](#theme-system)
4. [Control Framework](#control-framework)
5. [Application Structure](#application-structure)
6. [Development Patterns](#development-patterns)
7. [Extending the Framework](#extending-the-framework)
8. [Common Tasks](#common-tasks)
9. [File Reference](#file-reference)

## Project Overview

The IGCV GUI Framework is a Windows Forms-based UI toolkit that enables themed application development with a focus on the Fraunhofer corporate identity. The framework consists of four main components:

1. **Theme System**: Defines visual styles for UI elements
2. **Control Framework**: Custom controls that support theming
3. **Application Framework**: Base application structure and navigation
4. **Demo Implementation**: Sample code showcasing framework capabilities

### Key Design Goals

- **Consistent Styling**: Ensure consistent appearance across applications
- **Flexibility**: Support multiple themes without code changes
- **Maintainability**: Clean separation of UI styling from logic
- **Compatibility**: Work with standard Windows Forms patterns
- **Extensibility**: Easy to add new controls and themes

### Technology Stack

- **Platform**: .NET 8.0+
- **UI Framework**: Windows Forms
- **Language**: C#
- **Build System**: MSBuild / Visual Studio

## Core Architecture

### Namespace Structure

The framework uses the following namespace hierarchy:

- **IGCV.GUI**: Core UI framework
  - **Controls**: Themed control implementations
  - **Themes**: Theme definitions and management
    - **FraunhoferCI**: Fraunhofer theme implementation
    - **DarkTheme**: Alternative theme implementation
  - **Demo**: Demo applications

- **IGCV_GUI_Framework**: Application framework
  - **Common**: Common utilities and controls
  - **Interfaces**: Core interfaces
  - **Pages**: Page implementations

### Key Interfaces

1. **ITheme**: Defines a complete theme with colors, fonts, and styling methods
   ```csharp
   public interface ITheme
   {
       string Name { get; }
       Color PrimaryColor { get; }
       // ... other visual properties
       void ApplyPrimaryButtonStyle(Button button);
       // ... other styling methods
   }
   ```

2. **IThemeableControl**: Interface for controls that support theming
   ```csharp
   public interface IThemeableControl
   {
       int CornerRadius { get; set; }
       Color BorderColor { get; set; }
       int BorderWidth { get; set; }
   }
   ```

3. **IModulePage**: Interface for application pages
   ```csharp
   public interface IModulePage
   {
       string Title { get; }
       string Subtitle { get; }
       string NavigationName { get; }
       Image Icon { get; }
       int Order { get; }
       UserControl GetPageContent();
       void OnActivated();
       void OnDeactivated();
   }
   ```

## Theme System

The theme system is central to the framework's functionality. It defines visual styles and applies them to controls.

### Theme Architecture

1. **ITheme Interface**: Contract for all themes
2. **ThemeBase**: Abstract base implementation
3. **FraunhoferTheme**: Fraunhofer corporate identity theme
4. **DarkTheme**: Alternative dark theme
5. **ThemeManager**: Static class for theme registration and application

### ThemeManager Class

The `ThemeManager` is responsible for:

1. **Theme Registration**: Add themes to the available set
2. **Theme Selection**: Set the active theme
3. **Theme Application**: Apply themes to controls and containers
4. **Fallback Theme**: Provide an emergency theme if initialization fails

Key methods:

```csharp
// Register a new theme
ThemeManager.RegisterTheme(MyCustomTheme.Instance);

// Set the active theme
ThemeManager.SetTheme("Fraunhofer CI");

// Apply theme to a container and its children
ThemeManager.ApplyThemeToContainer(myForm);

// Apply gradient background in a Paint event
ThemeManager.ApplyGradientBackground(myForm, e);
```

### Creating New Themes

To create a new theme:

1. Create a class that inherits from `ThemeBase`
2. Implement required abstract properties
3. Override styling methods as needed
4. Use the Singleton pattern for efficient instantiation
5. Register with ThemeManager

Example skeleton:

```csharp
public class MyCustomTheme : ThemeBase
{
    // Singleton pattern
    private static MyCustomTheme _instance;
    public static MyCustomTheme Instance => _instance ?? (_instance = new MyCustomTheme());
    
    private MyCustomTheme() { /* Initialize fonts, etc. */ }
    
    // Required implementations
    public override string Name => "My Custom Theme";
    public override Color PrimaryColor => Color.FromArgb(0, 120, 215);
    // ... implement all required properties
    
    // Custom overrides (optional)
    public override void ApplyPrimaryButtonStyle(Button button)
    {
        // Custom implementation
        base.ApplyPrimaryButtonStyle(button);
        // Additional customization
    }
}
```

## Control Framework

The control framework provides themed versions of standard Windows Forms controls.

### Control Architecture

Each themed control follows this pattern:

1. **Inheritance**: Inherits from a standard Windows Forms control
2. **Interface Implementation**: Implements `IThemeableControl`
3. **Properties**: Adds theme-specific properties
4. **Custom Drawing**: Uses custom drawing for consistent appearance
5. **Theme Application**: Has a method to apply the current theme

### Key Controls

1. **ThemedButton**: Button with multiple styles and states
2. **ThemedPanel**: Panel with rounded corners and borders
3. **ThemedLabel**: Label with enhanced styling
4. **ThemedTextBox**: TextBox with theming capabilities
5. **ThemedCheckBox**: CheckBox with custom drawing
6. **ThemedRadioButton**: RadioButton with custom drawing
7. **ThemedComboBox**: ComboBox with theming support
8. **ThemedProgressBar**: ProgressBar with gradients and labels

### Control Implementation Pattern

Each themed control follows this implementation pattern:

```csharp
public class ThemedControl : Control, IThemeableControl
{
    // IThemeableControl properties
    public int CornerRadius { get; set; }
    public Color BorderColor { get; set; }
    public int BorderWidth { get; set; }
    
    // Additional properties
    public ControlStyle ControlStyle { get; set; }
    
    // Constructor
    public ThemedControl()
    {
        // Enable custom painting
        SetStyle(ControlStyles.UserPaint | /* other flags */, true);
        
        // Default properties
        CornerRadius = 3;
        BorderColor = Color.Gray;
        BorderWidth = 1;
    }
    
    // Apply theme method
    public void ApplyTheme(ITheme theme = null)
    {
        theme = theme ?? ThemeManager.CurrentTheme;
        if (theme == null) return;
        
        // Apply theme based on control style
        switch (ControlStyle)
        {
            case ControlStyle.Primary:
                // Apply primary style
                break;
            // Other styles...
        }
    }
    
    // Override OnPaint for custom drawing
    protected override void OnPaint(PaintEventArgs e)
    {
        // Custom drawing implementation
    }
    
    // Helper methods for drawing components
    private GraphicsPath CreateRoundedRectangle(/* params */)
    {
        // Implementation
    }
}
```

### Adding a New Themed Control

To add a new themed control:

1. Create a class inheriting from the appropriate Windows Forms control
2. Implement the `IThemeableControl` interface
3. Enable custom painting flags in the constructor
4. Implement the `ApplyTheme` method
5. Override `OnPaint` for custom drawing
6. Add mouse event handlers for state changes (hover, press)
7. Register with ThemeManager if necessary

## Application Structure

The application framework provides the structure for building themed applications.

### Page Architecture

1. **PageBase**: Abstract base class for all pages
2. **IModulePage**: Interface for page functionality
3. **Concrete Pages**: Specific page implementations

### Navigation System

The navigation system uses:

1. **Navigation Bar**: Sidebar menu for navigation
2. **Page Container**: Hosts the active page
3. **Main Form**: Main application window

### Page Implementation Pattern

```csharp
public class MyPage : PageBase
{
    public MyPage() : base("My Page", "Subtitle", "Navigation Name", myIcon, 1)
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        // Create panels, buttons, etc. using themed controls
        Panel mainPanel = CreateStandardPanel(10, 10, 600, 400);
        
        Label headerLabel = CreateSectionHeader("Section Title", 20, 20);
        mainPanel.Controls.Add(headerLabel);
        
        Button actionButton = CreatePrimaryButton("Action", 20, 60, 120, 40);
        actionButton.Click += ActionButton_Click;
        mainPanel.Controls.Add(actionButton);
        
        this.Controls.Add(mainPanel);
    }
    
    // Event handlers
    private void ActionButton_Click(object sender, EventArgs e)
    {
        // Handle click
    }
    
    // Override activation methods if needed
    public override void OnActivated()
    {
        base.OnActivated();
        // Page activation logic
    }
    
    public override void OnDeactivated()
    {
        base.OnDeactivated();
        // Page deactivation logic
    }
}
```

## Development Patterns

### Theming Control Flow

1. **Theme Registration**: Themes are registered with ThemeManager
2. **Theme Selection**: A theme is selected at application startup
3. **Control Creation**: Controls are created with default properties
4. **Theme Application**: Theme is applied to containers/controls
5. **Custom Drawing**: Controls use custom drawing based on theme properties

### Event Handling

```csharp
// Create a button
ThemedButton button = new ThemedButton
{
    Text = "Action",
    ButtonStyle = ButtonStyle.Primary,
    Location = new Point(10, 10),
    Size = new Size(120, 40)
};

// Add event handler
button.Click += (sender, e) => 
{
    // Handle click
};

// Add to container
this.Controls.Add(button);
```

### Gradient Backgrounds

To apply a gradient background to a form:

```csharp
// In the form class
protected override void OnPaint(PaintEventArgs e)
{
    base.OnPaint(e);
    ThemeManager.ApplyGradientBackground(this, e);
}
```

### Resource Management

The framework uses:

1. **Embedded Resources**: For essential framework resources
2. **Dynamic Creation**: For placeholder images when resources are unavailable
3. **Font Fallbacks**: For fonts that might not be installed

## Extending the Framework

### Adding a New Theme

1. Create a new class inheriting from `ThemeBase`
2. Implement all abstract properties
3. Override styling methods as needed
4. Register the theme with ThemeManager

```csharp
// Register theme
ThemeManager.RegisterTheme(MyCustomTheme.Instance);

// Use in application
ThemeManager.SetTheme("My Custom Theme");
```

### Adding a New Control

1. Create a new class inheriting from a Windows Forms control
2. Implement the `IThemeableControl` interface
3. Add custom properties and methods
4. Implement custom drawing
5. Implement theme application

### Adding a New Page

1. Create a new class inheriting from `PageBase`
2. Define page properties (title, subtitle, etc.)
3. Implement UI using themed controls
4. Add event handlers for user interactions
5. Override activation/deactivation methods if needed

```csharp
public class MyNewPage : PageBase
{
    public MyNewPage() : base("Title", "Subtitle", "Navigation", myIcon, 10)
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        // UI implementation
    }
}
```

## Common Tasks

### Creating Themed Forms

To create a themed form:

```csharp
public class MyThemedForm : Form
{
    public MyThemedForm()
    {
        InitializeComponent();
        
        // Apply theme to this form
        ThemeManager.ApplyThemeToContainer(this);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        
        // Apply gradient background
        ThemeManager.ApplyGradientBackground(this, e);
    }
    
    private void InitializeComponent()
    {
        // Use themed controls
        ThemedButton button = new ThemedButton
        {
            Text = "Action",
            ButtonStyle = ButtonStyle.Primary,
            Location = new Point(20, 20),
            Size = new Size(150, 40)
        };
        
        this.Controls.Add(button);
    }
}
```

### Font Handling

The framework includes robust font handling with fallbacks:

```csharp
// Example from FraunhoferTheme.cs
private string PreferredFontFamily
{
    get
    {
        // Safely check if Frutiger is installed
        try
        {
            foreach (FontFamily family in FontFamily.Families)
            {
                if (family.Name.Equals(PREFERRED_FONT_FAMILY, StringComparison.OrdinalIgnoreCase))
                {
                    return PREFERRED_FONT_FAMILY;
                }
            }
        }
        catch
        {
            // Ignore any exceptions during font enumeration
        }
        
        // Fall back to Arial or another system font if Frutiger is not found
        return FALLBACK_FONT_FAMILY;
    }
}
```

### Handling Control States

Themed controls track mouse states (hover, pressed) to provide visual feedback:

```csharp
protected override void OnMouseEnter(EventArgs e)
{
    _isHovering = true;
    Invalidate(); // Request repaint
    base.OnMouseEnter(e);
}

protected override void OnMouseLeave(EventArgs e)
{
    _isHovering = false;
    Invalidate(); // Request repaint
    base.OnMouseLeave(e);
}

// In OnPaint method
private Color GetBackgroundColor()
{
    if (!Enabled) return DisabledColor;
    if (_isPressed) return PressedColor;
    if (_isHovering) return HoverColor;
    return NormalColor;
}
```

## File Reference

### Core Framework Files

#### Theme System

- **`IGCV\GUI\Themes\ITheme.cs`** - Theme interface
- **`IGCV\GUI\Themes\ThemeBase.cs`** - Base theme implementation
- **`IGCV\GUI\Themes\ThemeManager.cs`** - Theme management
- **`IGCV\GUI\Themes\FraunhoferCI\FraunhoferTheme.cs`** - Fraunhofer theme
- **`IGCV\GUI\Themes\DarkTheme\DarkTheme.cs`** - Dark theme example
- **`IGCV\GUI\IThemeableControl.cs`** - Interface for themeable controls

#### Control Framework

- **`IGCV\GUI\Controls\ThemedButton.cs`** - Themed button control
- **`IGCV\GUI\Controls\ThemedPanel.cs`** - Themed panel control
- **`IGCV\GUI\Controls\ThemedLabel.cs`** - Themed label control
- **`IGCV\GUI\Controls\ThemedTextBox.cs`** - Themed text box control
- **`IGCV\GUI\Controls\ThemedCheckBox.cs`** - Themed check box control
- **`IGCV\GUI\Controls\ThemedRadioButton.cs`** - Themed radio button control
- **`IGCV\GUI\Controls\ThemedComboBox.cs`** - Themed combo box control
- **`IGCV\GUI\Controls\ThemedProgressBar.cs`** - Themed progress bar control

### Application Framework Files

#### Page System

- **`Interfaces\interfaces.cs`** - Core interfaces including IModulePage
- **`Pages\page-base.cs`** - Base page implementation 
- **`Pages\main-menu-page.cs`** - Main menu implementation
- **`Pages\axes-page.cs`** - Example page implementation
- **`Pages\sample-page.cs`** - Example page implementation

#### Navigation

- **`Common\Controls\navigation-bar.cs`** - Navigation sidebar
- **`Common\Controls\page-container.cs`** - Page container
- **`Common\Controls\status-panel.cs`** - Status display panel
- **`main-form.cs`** - Main application form

### Legacy Files

- **`Common\FraunhoferTheme.cs`** - Legacy theme implementation for compatibility

### Demo Files

- **`IGCV\GUI\Demo\ControlsDemoForm.cs`** - Demo for themed controls
- **`IGCV\GUI\Demo\ThemeIntegrationSample.cs`** - Demo for theme integration
- **`IGCV\GUI\Demo\DemoLauncher.cs`** - Demo launcher application

## Best Practices

### Custom Drawing Performance

1. **Enable optimized double buffering** to prevent flicker
   ```csharp
   SetStyle(ControlStyles.OptimizedDoubleBuffer | /* other flags */, true);
   ```

2. **Use proper invalidation** - only invalidate when needed
   ```csharp
   // Only invalidate when a property changes
   if (_borderWidth != value)
   {
       _borderWidth = value;
       Invalidate(); // Request repaint
   }
   ```

3. **Dispose graphics resources** properly
   ```csharp
   using (Brush brush = new SolidBrush(color))
   {
       // Use brush
   } // Automatically disposed
   ```

### Theme Consistency

1. Always use the current theme for styling:
   ```csharp
   ITheme theme = ThemeManager.CurrentTheme;
   ```

2. Apply themes to all controls in a container:
   ```csharp
   ThemeManager.ApplyThemeToContainer(form);
   ```

3. Check for null theme references:
   ```csharp
   theme = theme ?? ThemeManager.CurrentTheme;
   if (theme == null) return;
   ```

### Error Handling

1. **Font Fallbacks**: Use fallback fonts when the preferred one isn't available
2. **Safe Theme Application**: Check for null references before applying themes
3. **Emergency Theme**: Use the emergency theme when initialization fails

### Extension Patterns

1. **Follow existing patterns**: Match the existing code structure
2. **Use interface implementations**: Implement the appropriate interfaces
3. **Document new components**: Add proper XML documentation comments
4. **Override only what's needed**: Use base implementations where possible
5. **Test thoroughly**: Ensure new components work with all themes

## Conclusion

This Developer Guide provides a comprehensive reference for LLM-based development of the IGCV GUI Framework. By following the patterns and practices outlined here, you can effectively maintain, modify, and extend the framework to meet new requirements while maintaining code quality and consistency.

For specific implementation details, refer to the actual code files and the internal documentation comments. The framework is designed to be self-documenting with clear patterns that can be followed for new development.

If you need to extend the framework with new components, refer to the existing implementations as examples and follow the same architectural patterns to ensure consistency and compatibility with the theming system.

For practical usage examples, see the [User Guide](USER_GUIDE.md).
For technical architecture details, see the [Architecture Overview](ARCHITECTURE.md).
For a quick start, refer to the main [README](../README.md).
