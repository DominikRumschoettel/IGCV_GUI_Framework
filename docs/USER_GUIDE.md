# IGCV GUI Framework - User Guide

This guide provides a practical introduction to using the IGCV GUI Framework for developing Windows Forms applications with the Fraunhofer corporate identity.

## Related Documentation
- [README](../README.md) - Main project documentation and quick start
- [Architecture Overview](ARCHITECTURE.md) - Technical details of the framework architecture
- [LLM Developer Guide](LLM_DEVELOPER_GUIDE.md) - Detailed guide for Language Model-based development

## Getting Started

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/fraunhofer-igcv/IGCV_GUI_Framework.git
   ```

2. Open the solution in Visual Studio:
   ```
   IGCV_GUI_Framework.sln
   ```

3. Build the solution:
   ```
   dotnet build
   ```

### Creating Your First Application

1. Create a new Windows Forms project in Visual Studio
2. Add a reference to the IGCV_GUI_Framework project or DLL
3. Import the necessary namespaces:
   ```csharp
   using IGCV.GUI.Controls;
   using IGCV.GUI.Themes;
   ```

4. Update your main form to use theming:
   ```csharp
   public partial class MainForm : Form
   {
       public MainForm()
       {
           InitializeComponent();
           
           // Initialize theming
           ThemeManager.SetTheme("Fraunhofer CI");
           
           // Apply theme to this form and all its controls
           ThemeManager.ApplyThemeToContainer(this);
       }
       
       protected override void OnPaint(PaintEventArgs e)
       {
           base.OnPaint(e);
           
           // Apply gradient background
           ThemeManager.ApplyGradientBackground(this, e);
       }
   }
   ```

## Using Themed Controls

### Adding Controls to Your Form

The framework provides themed versions of common controls:

```csharp
// Create a primary button
ThemedButton primaryButton = new ThemedButton
{
    Text = "Submit",
    ButtonStyle = ButtonStyle.Primary,
    Location = new Point(20, 20),
    Size = new Size(120, 40)
};
this.Controls.Add(primaryButton);

// Create a secondary button
ThemedButton secondaryButton = new ThemedButton
{
    Text = "Cancel",
    ButtonStyle = ButtonStyle.Secondary,
    Location = new Point(150, 20),
    Size = new Size(120, 40)
};
this.Controls.Add(secondaryButton);

// Create a text box
ThemedTextBox textBox = new ThemedTextBox
{
    Location = new Point(20, 80),
    Size = new Size(250, 30),
    PlaceholderText = "Enter a value..."
};
this.Controls.Add(textBox);

// Create a panel with rounded corners
ThemedPanel panel = new ThemedPanel
{
    Location = new Point(20, 130),
    Size = new Size(300, 200),
    CornerRadius = 8,
    BorderWidth = 1
};
this.Controls.Add(panel);
```

### Handling Events

Events work the same way as standard Windows Forms controls:

```csharp
// Button click handler
primaryButton.Click += (sender, e) =>
{
    MessageBox.Show("Button clicked!");
};

// Text changed handler
textBox.TextChanged += (sender, e) =>
{
    // Handle text changes
};
```

## Creating a Multi-Page Application

### Setting Up the Navigation Structure

1. Create a main form with navigation controls:
   ```csharp
   public partial class MainApplicationForm : Form
   {
       private PageContainer pageContainer;
       private NavigationBar navigationBar;
       
       public MainApplicationForm()
       {
           InitializeComponent();
           
           // Setup theme
           ThemeManager.SetTheme("Fraunhofer CI");
           
           // Create navigation bar
           navigationBar = new NavigationBar
           {
               Dock = DockStyle.Left,
               Width = 200
           };
           
           // Create page container
           pageContainer = new PageContainer
           {
               Dock = DockStyle.Fill
           };
           
           // Add controls
           this.Controls.Add(pageContainer);
           this.Controls.Add(navigationBar);
           
           // Apply theme
           ThemeManager.ApplyThemeToContainer(this);
           
           // Register pages
           RegisterPages();
       }
       
       private void RegisterPages()
       {
           // Create and register pages
           var mainMenuPage = new MainMenuPage();
           var settingsPage = new SettingsPage();
           
           // Add pages to navigation
           navigationBar.AddPage(mainMenuPage);
           navigationBar.AddPage(settingsPage);
           
           // Set default page
           navigationBar.SetActivePage(mainMenuPage);
       }
   }
   ```

### Creating Pages

1. Create a new class inheriting from `PageBase`:
   ```csharp
   public class SettingsPage : PageBase
   {
       public SettingsPage()
           : base("Settings", "Application Settings", "Settings", null, 2)
       {
           InitializeComponent();
       }
       
       private void InitializeComponent()
       {
           // Create a panel for the content
           Panel contentPanel = CreateStandardPanel(10, 10, Width - 20, Height - 20);
           
           // Add a section header
           Label headerLabel = CreateSectionHeader("General Settings", 10, 10);
           contentPanel.Controls.Add(headerLabel);
           
           // Add settings controls
           ThemedCheckBox enableNotificationsCheckbox = new ThemedCheckBox
           {
               Text = "Enable Notifications",
               Location = new Point(10, 40),
               Checked = true
           };
           contentPanel.Controls.Add(enableNotificationsCheckbox);
           
           // Add buttons
           Button saveButton = CreatePrimaryButton("Save Settings", 10, 100, 150, 40);
           saveButton.Click += SaveButton_Click;
           contentPanel.Controls.Add(saveButton);
           
           // Add panel to page
           this.Controls.Add(contentPanel);
       }
       
       private void SaveButton_Click(object sender, EventArgs e)
       {
           // Handle save settings
           MessageBox.Show("Settings saved successfully!");
       }
       
       // Override activation methods (optional)
       public override void OnActivated()
       {
           base.OnActivated();
           // Load current settings when page is activated
       }
       
       public override void OnDeactivated()
       {
           base.OnDeactivated();
           // Save any pending changes when leaving page
       }
   }
   ```

## Working with Themes

### Using the Default Theme

The framework includes the Fraunhofer corporate identity theme which is applied by default:

```csharp
// Use the Fraunhofer theme
ThemeManager.SetTheme("Fraunhofer CI");
```

### Using the Dark Theme

The framework also includes a dark theme:

```csharp
// Use the dark theme
ThemeManager.SetTheme("Dark Theme");
```

### Creating a Custom Theme

You can create a custom theme by inheriting from `ThemeBase`:

```csharp
public class MyCustomTheme : ThemeBase
{
    // Singleton pattern
    private static MyCustomTheme _instance;
    public static MyCustomTheme Instance => _instance ?? (_instance = new MyCustomTheme());
    
    private MyCustomTheme()
    {
        // Initialize fonts, etc.
    }
    
    // Define theme properties
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
    public override Color BorderColor => Color.Gray;
    
    // Define fonts
    public override Font HeaderFont => new Font("Segoe UI", 16f, FontStyle.Bold);
    public override Font SubHeaderFont => new Font("Segoe UI", 14f, FontStyle.Regular);
    public override Font BodyFont => new Font("Segoe UI", 10f, FontStyle.Regular);
    public override Font ButtonFont => new Font("Segoe UI", 10f, FontStyle.Regular);
    public override Font SmallFont => new Font("Segoe UI", 8f, FontStyle.Regular);
    
    // Define shape properties
    public override int CornerRadius => 5;
    public override int BorderWidth => 1;
    
    // Optional: Override styling methods for custom control appearance
    public override void ApplyPrimaryButtonStyle(Button button)
    {
        base.ApplyPrimaryButtonStyle(button);
        // Add custom styling
    }
}
```

Then register and use your custom theme:

```csharp
// Register custom theme
ThemeManager.RegisterTheme(MyCustomTheme.Instance);

// Use custom theme
ThemeManager.SetTheme("My Custom Theme");
```

## Best Practices

### Layout Management

For responsive layouts:

1. Use docking and anchoring for controls:
   ```csharp
   panel.Dock = DockStyle.Fill;
   button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
   ```

2. Use the built-in panel creation methods:
   ```csharp
   Panel contentPanel = CreateStandardPanel(10, 10, 500, 300);
   ```

### Visual Consistency

To maintain visual consistency:

1. Use the same button style for similar actions:
   - Primary style for main actions
   - Secondary style for alternative actions
   - Tertiary style for minor actions

2. Use spacing constants for layout:
   ```csharp
   int margin = ThemeManager.CurrentTheme.SpacingMedium;
   button.Location = new Point(margin, margin);
   ```

3. Use the theme's colors for custom elements:
   ```csharp
   customPanel.BackColor = ThemeManager.CurrentTheme.BackgroundColor;
   ```

### Performance Optimization

For optimal performance:

1. Minimize control creation during runtime
2. Use suspension of layout during bulk updates:
   ```csharp
   this.SuspendLayout();
   // Add multiple controls
   this.ResumeLayout();
   ```

3. Only invalidate controls when necessary:
   ```csharp
   // Instead of this.Invalidate(); which redraws everything
   specificControl.Invalidate();
   ```

## Troubleshooting

### Common Issues

1. **Theme Not Applied**
   - Ensure `ThemeManager.ApplyThemeToContainer()` is called after all controls are added
   - Check that the theme was properly set using `ThemeManager.SetTheme()`

2. **Controls Not Styled**
   - Verify that you're using themed controls (`ThemedButton` instead of `Button`)
   - Check if control properties override theme settings

3. **Visual Glitches**
   - Enable double buffering on your form:
     ```csharp
     this.DoubleBuffered = true;
     ```
   - Ensure proper disposal of graphics resources

### Debugging Themes

To debug theme application:

```csharp
// Check current theme
ITheme currentTheme = ThemeManager.CurrentTheme;
Console.WriteLine($"Current theme: {currentTheme?.Name ?? "None"}");

// List available themes
foreach (var theme in ThemeManager.AvailableThemes)
{
    Console.WriteLine($"Available theme: {theme.Key}");
}
```

## Advanced Topics

### Custom Control Creation

To create a custom themed control:

1. Inherit from a Windows Forms control and implement `IThemeableControl`:
   ```csharp
   public class MyCustomControl : Control, IThemeableControl
   {
       private int _cornerRadius = 3;
       private Color _borderColor = Color.Gray;
       private int _borderWidth = 1;
       
       public int CornerRadius
       {
           get => _cornerRadius;
           set
           {
               if (_cornerRadius != value)
               {
                   _cornerRadius = value;
                   Invalidate();
               }
           }
       }
       
       public Color BorderColor
       {
           get => _borderColor;
           set
           {
               if (_borderColor != value)
               {
                   _borderColor = value;
                   Invalidate();
               }
           }
       }
       
       public int BorderWidth
       {
           get => _borderWidth;
           set
           {
               if (_borderWidth != value)
               {
                   _borderWidth = value;
                   Invalidate();
               }
           }
       }
       
       public MyCustomControl()
       {
           SetStyle(
               ControlStyles.UserPaint |
               ControlStyles.ResizeRedraw |
               ControlStyles.SupportsTransparentBackColor |
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer,
               true);
       }
       
       public void ApplyTheme(ITheme theme = null)
       {
           theme = theme ?? ThemeManager.CurrentTheme;
           if (theme == null) return;
           
           BackColor = theme.BackgroundColor;
           ForeColor = theme.TextOnLightColor;
           BorderColor = theme.BorderColor;
           CornerRadius = theme.CornerRadius;
           BorderWidth = theme.BorderWidth;
           
           Invalidate();
       }
       
       protected override void OnPaint(PaintEventArgs e)
       {
           base.OnPaint(e);
           
           // Custom drawing implementation
           e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
           
           // Draw background
           using (var brush = new SolidBrush(BackColor))
           {
               if (CornerRadius > 0)
               {
                   using (var path = CreateRoundedRectangle(ClientRectangle, CornerRadius))
                   {
                       e.Graphics.FillPath(brush, path);
                   }
               }
               else
               {
                   e.Graphics.FillRectangle(brush, ClientRectangle);
               }
           }
           
           // Draw border if needed
           if (BorderWidth > 0)
           {
               using (var pen = new Pen(BorderColor, BorderWidth))
               {
                   if (CornerRadius > 0)
                   {
                       using (var path = CreateRoundedRectangle(
                           new Rectangle(
                               ClientRectangle.X + BorderWidth / 2,
                               ClientRectangle.Y + BorderWidth / 2,
                               ClientRectangle.Width - BorderWidth,
                               ClientRectangle.Height - BorderWidth),
                           CornerRadius))
                       {
                           e.Graphics.DrawPath(pen, path);
                       }
                   }
                   else
                   {
                       e.Graphics.DrawRectangle(pen, 
                           BorderWidth / 2, 
                           BorderWidth / 2, 
                           Width - BorderWidth, 
                           Height - BorderWidth);
                   }
               }
           }
       }
       
       private GraphicsPath CreateRoundedRectangle(Rectangle rect, int radius)
       {
           GraphicsPath path = new GraphicsPath();
           
           // Top-left arc
           path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
           
           // Top-right arc
           path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
           
           // Bottom-right arc
           path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
           
           // Bottom-left arc
           path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
           
           path.CloseFigure();
           return path;
       }
   }
   ```

2. Use your custom control:
   ```csharp
   MyCustomControl customControl = new MyCustomControl
   {
       Location = new Point(20, 20),
       Size = new Size(200, 100),
       Text = "Custom Control"
   };
   customControl.ApplyTheme();
   this.Controls.Add(customControl);
   ```

## Conclusion

The IGCV GUI Framework provides a powerful foundation for creating visually consistent Windows Forms applications with the Fraunhofer corporate identity. By using the themed controls and following the page-based architecture, you can rapidly develop professional-looking applications.

For more detailed information, refer to:
- [README](../README.md) - Main project documentation and quick start
- [Architecture Overview](ARCHITECTURE.md) - Technical details of the framework architecture
- [LLM Developer Guide](LLM_DEVELOPER_GUIDE.md) - Comprehensive guide for Language Model-based development

You can also explore the demo applications in the source code for practical examples.
