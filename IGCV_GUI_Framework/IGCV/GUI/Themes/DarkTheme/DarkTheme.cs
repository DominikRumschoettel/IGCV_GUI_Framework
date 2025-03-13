using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IGCV.GUI.Themes.DarkTheme
{
    /// <summary>
    /// Implementation of a dark theme for the IGCV.GUI framework
    /// </summary>
    public class DarkTheme : ThemeBase
    {
        #region Singleton Implementation
        
        private static DarkTheme _instance;
        
        /// <summary>
        /// Gets the singleton instance of the Dark theme
        /// </summary>
        public static DarkTheme Instance => _instance ?? (_instance = new DarkTheme());
        
        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Private constructor to enforce singleton pattern
        /// </summary>
        private DarkTheme()
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
        ~DarkTheme()
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
        
        // Default font family name
        private const string PREFERRED_FONT_FAMILY = "Segoe UI";
        private const string FALLBACK_FONT_FAMILY = "Microsoft Sans Serif";
        
        private readonly Font _headerFont;
        private readonly Font _subHeaderFont;
        private readonly Font _bodyFont;
        private readonly Font _buttonFont;
        private readonly Font _buttonFontBold;
        private readonly Font _smallFont;
        
        /// <summary>
        /// Gets the preferred font family with fallback
        /// </summary>
        private string PreferredFontFamily
        {
            get
            {
                // Safely check if Segoe UI is installed
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
                
                // Fall back to a system default font
                return FALLBACK_FONT_FAMILY;
            }
        }
        
        #endregion
        
        #region Theme Properties
        
        public override string Name => "Dark Theme";
        
        public override Version Version => new Version(1, 0, 0, 0);
        
        #endregion
        
        #region Color Properties
        
        // Dark theme colors
        public override Color PrimaryColor => Color.FromArgb(0, 120, 215);     // Blue
        public override Color SecondaryColor => Color.FromArgb(85, 85, 85);    // Dark Gray
        public override Color AccentColor => Color.FromArgb(255, 185, 0);      // Amber
        
        // Background colors
        public override Color BackgroundColor => Color.FromArgb(30, 30, 30);   // Very Dark Gray
        public Color WindowColor => Color.FromArgb(45, 45, 45);                // Slightly lighter Dark Gray
        public Color SurfaceColor => Color.FromArgb(60, 60, 60);               // Surface elements
        
        // Text colors
        public override Color TextOnLightColor => Color.FromArgb(230, 230, 230); // Light text for dark backgrounds
        public override Color TextOnDarkColor => Color.FromArgb(250, 250, 250);  // Brighter text for darker backgrounds
        
        // Status colors
        public override Color SuccessColor => Color.FromArgb(92, 184, 92);     // Green
        public override Color WarningColor => Color.FromArgb(240, 173, 78);    // Orange
        public override Color ErrorColor => Color.FromArgb(217, 83, 79);       // Red
        
        // Border color
        public override Color BorderColor => Color.FromArgb(100, 100, 100);    // Medium Gray
        
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
        
        public override int CornerRadius => 4;
        public override int BorderWidth => 1;
        
        #endregion
        
        #region Overridden Styling Methods
        
        /// <summary>
        /// Apply the Dark theme primary button style
        /// </summary>
        public override void ApplyPrimaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = PrimaryColor;
            button.ForeColor = TextOnDarkColor;
            button.Font = ButtonFontBold;
            button.FlatAppearance.BorderSize = 0;
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the Dark theme secondary button style
        /// </summary>
        public override void ApplySecondaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = SecondaryColor;
            button.ForeColor = TextOnDarkColor;
            button.Font = ButtonFont;
            button.FlatAppearance.BorderSize = 0;
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the Dark theme tertiary/text button style
        /// </summary>
        public override void ApplyTertiaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.Transparent;
            button.ForeColor = AccentColor;
            button.Font = ButtonFont;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, AccentColor);
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the Dark theme header label style
        /// </summary>
        public override void ApplyHeaderLabelStyle(Label label)
        {
            if (label == null) return;
            
            label.Font = HeaderFont;
            label.ForeColor = TextOnLightColor;
            label.BackColor = Color.Transparent;
            label.AutoSize = true;
        }
        
        /// <summary>
        /// Apply the Dark theme subheader label style
        /// </summary>
        public override void ApplySubHeaderLabelStyle(Label label)
        {
            if (label == null) return;
            
            label.Font = SubHeaderFont;
            label.ForeColor = TextOnLightColor;
            label.BackColor = Color.Transparent;
            label.AutoSize = true;
        }
        
        /// <summary>
        /// Apply the Dark theme panel style
        /// </summary>
        public override void ApplyPanelStyle(Panel panel)
        {
            if (panel == null) return;
            
            panel.BackColor = SurfaceColor;
            panel.ForeColor = TextOnLightColor;
            panel.BorderStyle = BorderStyle.None;
            
            // Apply corner radius if supported by the control
            if (panel is IThemeableControl themeablePanel)
            {
                themeablePanel.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the Dark theme text box style
        /// </summary>
        public override void ApplyTextBoxStyle(TextBox textBox)
        {
            if (textBox == null) return;
            
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = BodyFont;
            textBox.BackColor = WindowColor;
            textBox.ForeColor = TextOnLightColor;
            
            // Apply corner radius if supported by the control
            if (textBox is IThemeableControl themeableTextBox)
            {
                themeableTextBox.CornerRadius = CornerRadius;
                themeableTextBox.BorderColor = BorderColor;
            }
        }
        
        /// <summary>
        /// Apply the Dark theme combo box style
        /// </summary>
        public override void ApplyComboBoxStyle(ComboBox comboBox)
        {
            if (comboBox == null) return;
            
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = BodyFont;
            comboBox.BackColor = WindowColor;
            comboBox.ForeColor = TextOnLightColor;
        }
        
        /// <summary>
        /// Apply gradient background to a form using the Dark theme gradient
        /// </summary>
        public override void ApplyGradientBackground(PaintEventArgs e, Rectangle bounds)
        {
            if (e == null) return;
            
            using (LinearGradientBrush brush = new LinearGradientBrush(
                bounds,
                Color.FromArgb(20, 20, 20),    // Almost black
                Color.FromArgb(45, 45, 45),    // Dark gray
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, bounds);
            }
        }
        
        #endregion
        
        #region Dark Theme Specific Methods
        
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
                using (SolidBrush brush = new SolidBrush(isActive ? SuccessColor : ErrorColor))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, 30, 30);
                }
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    e.Graphics.DrawEllipse(pen, 0, 0, 30, 30);
                }
            };
            
            return statusLight;
        }
        
        /// <summary>
        /// Creates a themed card panel for dark theme
        /// </summary>
        public Panel CreateCardPanel(string title, Point location, Size size)
        {
            Panel panel = new Panel
            {
                Location = location,
                Size = size,
                BackColor = WindowColor,
                BorderStyle = BorderStyle.None
            };
            
            if (panel is IThemeableControl themeablePanel)
            {
                themeablePanel.CornerRadius = CornerRadius;
                themeablePanel.BorderColor = BorderColor;
                themeablePanel.BorderWidth = 1;
            }
            
            // Add a header
            if (!string.IsNullOrEmpty(title))
            {
                Label headerLabel = new Label
                {
                    Text = title,
                    ForeColor = TextOnLightColor,
                    Font = new Font(ButtonFont, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(10, 10)
                };
                
                panel.Controls.Add(headerLabel);
                
                // Add a separator
                Panel separator = new Panel
                {
                    Location = new Point(10, 35),
                    Size = new Size(size.Width - 20, 1),
                    BackColor = BorderColor
                };
                panel.Controls.Add(separator);
            }
            
            return panel;
        }
        
        #endregion
    }
}
