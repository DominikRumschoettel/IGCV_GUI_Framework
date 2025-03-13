using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IGCV.GUI.Themes.FraunhoferCI
{
    /// <summary>
    /// Implementation of the Fraunhofer Corporate Identity theme
    /// </summary>
    public class FraunhoferTheme : ThemeBase
    {
        #region Singleton Implementation
        
        private static FraunhoferTheme _instance;
        
        /// <summary>
        /// Gets the singleton instance of the Fraunhofer theme
        /// </summary>
        public static FraunhoferTheme Instance => _instance ?? (_instance = new FraunhoferTheme());
        
        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Private constructor to enforce singleton pattern
        /// </summary>
        private FraunhoferTheme()
        {
            // Initialize fonts
            _headerFont = new Font(PreferredFontFamily, 24f, FontStyle.Bold);
            _subHeaderFont = new Font(PreferredFontFamily, 14f, FontStyle.Regular);
            _bodyFont = new Font(PreferredFontFamily, 10f, FontStyle.Regular);
            _buttonFont = new Font(PreferredFontFamily, 10f, FontStyle.Regular);
            _buttonFontBold = new Font(PreferredFontFamily, 10f, FontStyle.Bold);
            _smallFont = new Font(PreferredFontFamily, 8f, FontStyle.Regular);
        }
        
        /// <summary>
        /// Finalizer to dispose of resources
        /// </summary>
        ~FraunhoferTheme()
        {
            _headerFont?.Dispose();
            _subHeaderFont?.Dispose();
            _bodyFont?.Dispose();
            _buttonFont?.Dispose();
            _buttonFontBold?.Dispose();
            _smallFont?.Dispose();
        }
        
        #endregion
        
        #region Font Management
        
        // Default font family name for Fraunhofer
        private const string PREFERRED_FONT_FAMILY = "Frutiger LT Com";
        private const string FALLBACK_FONT_FAMILY = "Arial";
        
        private readonly Font _headerFont;
        private readonly Font _subHeaderFont;
        private readonly Font _bodyFont;
        private readonly Font _buttonFont;
        private readonly Font _buttonFontBold;
        private readonly Font _smallFont;
        
        /// <summary>
        /// Gets the preferred font family, falling back to Arial if Frutiger isn't available
        /// </summary>
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
        
        #endregion
        
        #region Theme Properties
        
        public override string Name => "Fraunhofer CI";
        
        public override Version Version => new Version(1, 0, 0, 0);
        
        #endregion
        
        #region Color Properties
        
        // Primary Colors from the Fraunhofer CI
        public override Color PrimaryColor => Color.FromArgb(0, 156, 125);   // Fraunhofer Green (#009c7d)
        public Color SteelBlue => Color.FromArgb(0, 67, 119);                // Dark Steel Blue (#004377) - for gradients
        public Color SilverGrey => Color.FromArgb(166, 187, 200);            // Silver Grey (#a6bbc8) - original
        public Color DarkSidebarColor => Color.FromArgb(13, 45, 80);          // Dark blue for sidebar (#0d2d50)
        public Color DarkPanelColor => Color.FromArgb(0, 84, 129);           // Mid blue for panels
        public Color HeaderBlue => Color.FromArgb(0, 122, 159);              // Blue for header areas (#007a9f)
        
        // Accent Color
        public override Color AccentColor => Color.FromArgb(245, 130, 32);   // Orange (#f58220)
        
        // Secondary Colors
        public Color Graphit => Color.FromArgb(28, 63, 82);                  // Graphit (#1c3f52)
        public Color Sand => Color.FromArgb(211, 199, 174);                  // Sand (#d3c7ae)
        public Color Petrol => Color.FromArgb(0, 133, 152);                  // Petrol (#008598)
        public Color Aqua => Color.FromArgb(57, 193, 205);                   // Aqua (#39c1cd)
        public Color Lime => Color.FromArgb(178, 210, 53);                   // Lime (#b2d235)
        public Color Gelb => Color.FromArgb(253, 185, 19);                   // Gelb/Yellow (#fdb913)
        public Color Rot => Color.FromArgb(187, 0, 86);                      // Rot/Red (#bb0056)
        public Color Weinrot => Color.FromArgb(124, 21, 77);                 // Weinrot/Burgundy (#7c154d)
        
        // Default Theme Colors
        public override Color SecondaryColor => SteelBlue;
        public override Color BackgroundColor => Color.White;
        public override Color TextOnLightColor => Color.FromArgb(60, 60, 60);
        public override Color TextOnDarkColor => Color.White;
        public override Color SuccessColor => Lime;
        public override Color WarningColor => Gelb;
        public override Color ErrorColor => Rot;
        public override Color BorderColor => SilverGrey;
        
        #endregion
        
        #region Fonts Properties
        
        public override Font HeaderFont => _headerFont;
        public override Font SubHeaderFont => _subHeaderFont;
        public override Font BodyFont => _bodyFont;
        public override Font ButtonFont => _buttonFont;
        public Font ButtonFontBold => _buttonFontBold;
        public override Font SmallFont => _smallFont;
        
        #endregion
        
        #region Shape and Border Properties
        
        public override int CornerRadius => 3;
        public override int BorderWidth => 1;
        
        #endregion
        
        #region Overridden Styling Methods
        
        /// <summary>
        /// Apply the Fraunhofer primary button style
        /// </summary>
        public override void ApplyPrimaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = PrimaryColor;
            button.ForeColor = TextOnDarkColor;
            button.Font = ButtonFontBold;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            
            // Add mouse over effect
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(PrimaryColor);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(PrimaryColor);
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the Fraunhofer secondary button style
        /// </summary>
        public override void ApplySecondaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.FromArgb(230, 237, 243); // Light gray-blue
            button.ForeColor = TextOnLightColor;
            button.Font = ButtonFont;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            
            // Add mouse over effect
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(button.BackColor);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(button.BackColor);
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the theme's tertiary/text button style
        /// </summary>
        public override void ApplyTertiaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.Transparent;
            button.ForeColor = TextOnDarkColor; // Use white text for better visibility
            button.Font = ButtonFont;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(100, 255, 255, 255); // Semi-transparent white border
            button.Cursor = Cursors.Hand;
            
            // Add mouse over effect
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 255, 255, 255);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 255, 255, 255);
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply gradient background to a form using the Fraunhofer gradient
        /// </summary>
        public override void ApplyGradientBackground(PaintEventArgs e, Rectangle bounds)
        {
            if (e == null) return;
            
            using (LinearGradientBrush brush = new LinearGradientBrush(
                bounds,
                SteelBlue,    // Dark Steel Blue (#004377)
                Color.FromArgb(0, 133, 152),   // Petrol (#008598)
                LinearGradientMode.Horizontal))
            {
                // Add a color blend for smoother transition
                ColorBlend blend = new ColorBlend(3);
                blend.Colors = new Color[] { 
                    SteelBlue,
                    Color.FromArgb(0, 103, 140),  // Mid-blue
                    Color.FromArgb(0, 133, 152)   // Petrol
                };
                blend.Positions = new float[] { 0.0f, 0.5f, 1.0f };
                brush.InterpolationColors = blend;
                
                e.Graphics.FillRectangle(brush, bounds);
            }
        }
        
        /// <summary>
        /// Apply the theme's panel style with Fraunhofer styling
        /// </summary>
        public override void ApplyPanelStyle(Panel panel)
        {
            if (panel == null) return;
            
            panel.BackColor = DarkPanelColor;
            panel.ForeColor = TextOnDarkColor;
            panel.BorderStyle = BorderStyle.None;
            
            // Apply corner radius if supported by the control
            if (panel is IThemeableControl themeablePanel)
            {
                themeablePanel.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the theme's label style for headers that ensures text is visible
        /// </summary>
        public override void ApplyHeaderLabelStyle(Label label)
        {
            if (label == null) return;
            
            label.Font = HeaderFont;
            label.ForeColor = TextOnDarkColor; // Always use white text for headers on dark backgrounds
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
        }
        
        /// <summary>
        /// Apply the theme's label style for subheaders that ensures text is visible
        /// </summary>
        public override void ApplySubHeaderLabelStyle(Label label)
        {
            if (label == null) return;
            
            label.Font = SubHeaderFont;
            label.ForeColor = TextOnDarkColor; // Always use white text for subheaders on dark backgrounds
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
        }
        
        /// <summary>
        /// Apply the theme's text box style with clear visibility
        /// </summary>
        public override void ApplyTextBoxStyle(TextBox textBox)
        {
            if (textBox == null) return;
            
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = BodyFont;
            textBox.BackColor = Color.White;
            textBox.ForeColor = TextOnLightColor;
            
            // Apply corner radius if supported by the control
            if (textBox is IThemeableControl themeableTextBox)
            {
                themeableTextBox.CornerRadius = CornerRadius;
            }
        }
        
        #endregion
        
        #region Fraunhofer-Specific Methods
        
        /// <summary>
        /// Creates a themed status indicator light (red/green)
        /// </summary>
        public Panel CreateStatusLight(Point location, bool isActive = false)
        {
            Panel statusLight = new Panel
            {
                Size = new Size(30, 30),
                Location = location,
                BackColor = Color.Transparent
            };
            
            statusLight.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Draw outer glow effect
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(1, 1, 28, 28);
                    
                    using (PathGradientBrush brush = new PathGradientBrush(path))
                    {
                        Color centerColor = isActive ? SuccessColor : ErrorColor;
                        Color surroundColor = Color.FromArgb(30, centerColor);
                        
                        brush.CenterColor = centerColor;
                        brush.SurroundColors = new Color[] { surroundColor };
                        
                        e.Graphics.FillPath(brush, path);
                    }
                }
                
                // Draw main circle
                using (SolidBrush brush = new SolidBrush(isActive ? SuccessColor : ErrorColor))
                {
                    e.Graphics.FillEllipse(brush, 4, 4, 22, 22);
                }
                
                // Draw highlight for 3D effect
                using (GraphicsPath highlightPath = new GraphicsPath())
                {
                    highlightPath.AddEllipse(8, 8, 10, 10);
                    
                    using (PathGradientBrush highlightBrush = new PathGradientBrush(highlightPath))
                    {
                        highlightBrush.CenterColor = Color.FromArgb(150, 255, 255, 255);
                        highlightBrush.SurroundColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };
                        
                        e.Graphics.FillPath(highlightBrush, highlightPath);
                    }
                }
                
                // Draw border
                using (Pen pen = new Pen(Color.FromArgb(100, 0, 0, 0), 1))
                {
                    e.Graphics.DrawEllipse(pen, 4, 4, 22, 22);
                }
            };
            
            return statusLight;
        }
        
        /// <summary>
        /// Creates a themed tab button for navigation
        /// </summary>
        public Button CreateTabButton(string text, Point location, Size size, bool isActive = false)
        {
            Button button = new Button
            {
                Text = text,
                Location = location,
                Size = size,
                FlatStyle = FlatStyle.Flat,
                BackColor = isActive ? SteelBlue : Color.FromArgb(230, 237, 243),
                ForeColor = isActive ? TextOnDarkColor : TextOnLightColor,
                Font = ButtonFont,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };
            
            // Add mouse over effect
            button.FlatAppearance.MouseOverBackColor = isActive ? 
                ControlPaint.Light(SteelBlue) : 
                Color.FromArgb(210, 220, 230);
            
            return button;
        }
        
        /// <summary>
        /// Creates a Fraunhofer-styled section panel
        /// </summary>
        public Panel CreateSectionPanel(string title, Point location, Size size)
        {
            Panel panel = new Panel
            {
                Location = location,
                Size = size,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            
            // Add a header
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = SteelBlue
            };
            
            Label headerLabel = new Label
            {
                Text = title,
                ForeColor = TextOnDarkColor,
                Font = new Font(ButtonFont, FontStyle.Bold),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            
            headerPanel.Controls.Add(headerLabel);
            panel.Controls.Add(headerPanel);
            
            // Add a content panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            
            panel.Controls.Add(contentPanel);
            
            return panel;
        }
        
        /// <summary>
        /// Creates a Fraunhofer-styled sidebar panel
        /// </summary>
        public Panel CreateSidebarPanel(Point location, Size size)
        {
            Panel panel = new Panel
            {
                Location = location,
                Size = size,
                BackColor = DarkSidebarColor,
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Left
            };
            
            return panel;
        }
        
        /// <summary>
        /// Creates a navigation bar button with proper styling
        /// </summary>
        public Button CreateNavigationBarButton(string text, Point location, Size size, bool isActive = false)
        {
            Button button = new Button
            {
                Text = text,
                Location = location,
                Size = size,
                FlatStyle = FlatStyle.Flat,
                Font = ButtonFont,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            
            if (isActive)
            {
                button.BackColor = Color.FromArgb(0, 120, 215);
                button.ForeColor = TextOnDarkColor;
                button.FlatAppearance.BorderSize = 0;
            }
            else
            {
                button.BackColor = Color.FromArgb(240, 240, 240);
                button.ForeColor = TextOnLightColor;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.FromArgb(210, 210, 210);
            }
            
            // Add mouse over effect
            button.FlatAppearance.MouseOverBackColor = isActive ? 
                ControlPaint.Light(button.BackColor) : 
                Color.FromArgb(220, 230, 240);
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = 0; // Keep these buttons square for navigation bar
            }
            
            return button;
        }
        
        #endregion
    }
}
