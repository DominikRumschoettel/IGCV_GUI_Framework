using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes.FraunhoferCI;
using IGCV.GUI.Themes.DarkTheme;

namespace IGCV.GUI.Themes
{
    /// <summary>
    /// Manages theme selection and application for the IGCV GUI framework
    /// </summary>
    public static class ThemeManager
    {
        #region Fields
        
        // Dictionary of available themes
        private static Dictionary<string, ITheme> _themes = new Dictionary<string, ITheme>();
        
        // Current active theme
        private static ITheme _currentTheme;
        
        // Event for theme changes
        public static event EventHandler ThemeChanged;
        
        #endregion
        
        #region Static Constructor
        
        /// <summary>
        /// Static constructor to initialize themes
        /// </summary>
        static ThemeManager()
        {
            try
            {
                // Register available themes
                RegisterTheme(FraunhoferTheme.Instance);
                RegisterTheme(IGCV.GUI.Themes.DarkTheme.DarkTheme.Instance);
                
                // Set the Fraunhofer theme as default
                SetTheme(FraunhoferTheme.Instance.Name);
            }
            catch (Exception ex)
            {
                // Log or show error
                System.Diagnostics.Debug.WriteLine("Error initializing ThemeManager: " + ex.Message);
                
                // Create a safe fallback theme directly if theme initialization fails
                _currentTheme = CreateFallbackTheme();
            }
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets the current active theme
        /// </summary>
        public static ITheme CurrentTheme => _currentTheme;
        
        /// <summary>
        /// Gets the dictionary of available themes
        /// </summary>
        public static IReadOnlyDictionary<string, ITheme> AvailableThemes => _themes;
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Registers a theme in the theme manager
        /// </summary>
        /// <param name="theme">The theme to register</param>
        public static void RegisterTheme(ITheme theme)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            
            // Add or update the theme
            _themes[theme.Name] = theme;
        }
        
        /// <summary>
        /// Sets the active theme by name
        /// </summary>
        /// <param name="themeName">Name of the theme to activate</param>
        public static bool SetTheme(string themeName)
        {
            if (string.IsNullOrEmpty(themeName)) return false;
            
            // Check if the theme exists
            if (!_themes.TryGetValue(themeName, out var theme)) return false;
            
            // Set the current theme
            _currentTheme = theme;
            
            // Raise event
            ThemeChanged?.Invoke(null, EventArgs.Empty);
            
            return true;
        }
        
        /// <summary>
        /// Sets the active theme
        /// </summary>
        /// <param name="theme">Theme to activate</param>
        public static bool SetTheme(ITheme theme)
        {
            if (theme == null) return false;
            
            // Register the theme if it's not registered
            if (!_themes.ContainsKey(theme.Name))
            {
                RegisterTheme(theme);
            }
            
            // Set the current theme
            _currentTheme = theme;
            
            // Raise event
            ThemeChanged?.Invoke(null, EventArgs.Empty);
            
            return true;
        }

        /// <summary>
        /// Creates a minimal fallback theme if regular theme initialization fails
        /// </summary>
        private static ITheme CreateFallbackTheme()
        {
            // This is a simple implementation that doesn't depend on any external resources
            return new EmergencyTheme();
        }
        
        /// <summary>
        /// Applies the current theme to all controls in a container
        /// </summary>
        /// <param name="container">The container with controls to theme</param>
        public static void ApplyThemeToContainer(Control container)
        {
            if (container == null || _currentTheme == null) return;
            
            // Process all controls in the container
            foreach (Control control in container.Controls)
            {
                // Apply theme based on control type
                if (control is ThemedButton themedButton)
                {
                    themedButton.ApplyTheme(_currentTheme);
                }
                else if (control is ThemedPanel themedPanel)
                {
                    themedPanel.ApplyTheme(_currentTheme);
                }
                else if (control is ThemedLabel themedLabel)
                {
                    themedLabel.ApplyTheme(_currentTheme);
                }
                else if (control is ThemedTextBox themedTextBox)
                {
                    themedTextBox.ApplyTheme(_currentTheme);
                }
                else if (control is Button button)
                {
                    _currentTheme.ApplySecondaryButtonStyle(button);
                }
                else if (control is Label label)
                {
                    _currentTheme.ApplySubHeaderLabelStyle(label);
                }
                else if (control is Panel panel)
                {
                    _currentTheme.ApplyPanelStyle(panel);
                }
                else if (control is TextBox textBox)
                {
                    _currentTheme.ApplyTextBoxStyle(textBox);
                }
                else if (control is ComboBox comboBox)
                {
                    _currentTheme.ApplyComboBoxStyle(comboBox);
                }
                
                // Recursively process child controls
                if (control.Controls.Count > 0)
                {
                    ApplyThemeToContainer(control);
                }
            }
        }
        
        /// <summary>
        /// Applies the gradient background to a form
        /// </summary>
        /// <param name="form">The form to apply the gradient to</param>
        /// <param name="e">Paint event arguments</param>
        public static void ApplyGradientBackground(Form form, PaintEventArgs e)
        {
            if (form == null || e == null || _currentTheme == null) return;
            
            _currentTheme.ApplyGradientBackground(e, form.ClientRectangle);
        }
        
        #endregion

        /// <summary>
        /// Minimal theme implementation used as a fallback
        /// </summary>
        private class EmergencyTheme : ITheme
        {
            public string Name => "Emergency Theme";
            public Version Version => new Version(1, 0, 0, 0);
            
            // Colors
            public Color PrimaryColor => SystemColors.Highlight;
            public Color SecondaryColor => SystemColors.Control;
            public Color AccentColor => SystemColors.HotTrack;
            public Color BackgroundColor => SystemColors.Window;
            public Color TextOnLightColor => SystemColors.WindowText;
            public Color TextOnDarkColor => SystemColors.HighlightText;
            public Color SuccessColor => Color.Green;
            public Color WarningColor => Color.Orange;
            public Color ErrorColor => Color.Red;
            public Color BorderColor => SystemColors.ControlDark;
            
            // Fonts
            public Font HeaderFont => new Font(SystemFonts.DefaultFont.FontFamily, 16, FontStyle.Bold);
            public Font SubHeaderFont => new Font(SystemFonts.DefaultFont.FontFamily, 12, FontStyle.Regular);
            public Font BodyFont => SystemFonts.DefaultFont;
            public Font ButtonFont => SystemFonts.DefaultFont;
            public Font SmallFont => new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size - 2);
            
            // Shapes and borders
            public int CornerRadius => 0;
            public int BorderWidth => 1;
            
            // Spacing
            public int SpacingSmall => 4;
            public int SpacingMedium => 8;
            public int SpacingLarge => 16;
            
            // Simple styling methods
            public void ApplyPrimaryButtonStyle(Button button)
            {
                if (button == null) return;
                button.UseVisualStyleBackColor = true;
            }
            
            public void ApplySecondaryButtonStyle(Button button)
            {
                if (button == null) return;
                button.UseVisualStyleBackColor = true;
            }
            
            public void ApplyTertiaryButtonStyle(Button button)
            {
                if (button == null) return;
                button.UseVisualStyleBackColor = true;
            }
            
            public void ApplyHeaderLabelStyle(Label label)
            {
                if (label == null) return;
                label.Font = HeaderFont;
            }
            
            public void ApplySubHeaderLabelStyle(Label label)
            {
                if (label == null) return;
                label.Font = SubHeaderFont;
            }
            
            public void ApplyPanelStyle(Panel panel)
            {
                if (panel == null) return;
                panel.BorderStyle = BorderStyle.FixedSingle;
            }
            
            public void ApplyTextBoxStyle(TextBox textBox)
            {
                if (textBox == null) return;
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }
            
            public void ApplyComboBoxStyle(ComboBox comboBox)
            {
                if (comboBox == null) return;
                comboBox.FlatStyle = FlatStyle.System;
            }
            
            public void ApplyGradientBackground(PaintEventArgs e, Rectangle bounds)
            {
                if (e == null) return;
                e.Graphics.FillRectangle(SystemBrushes.Window, bounds);
            }
            
            // Factory methods with default implementations
            public Button CreatePrimaryButton(string text, Point location, Size size)
            {
                Button button = new Button { Text = text, Location = location, Size = size };
                ApplyPrimaryButtonStyle(button);
                return button;
            }
            
            public Button CreateSecondaryButton(string text, Point location, Size size)
            {
                Button button = new Button { Text = text, Location = location, Size = size };
                ApplySecondaryButtonStyle(button);
                return button;
            }
            
            public Label CreateHeaderLabel(string text, Point location)
            {
                Label label = new Label { Text = text, Location = location, AutoSize = true };
                ApplyHeaderLabelStyle(label);
                return label;
            }
            
            public Label CreateSubHeaderLabel(string text, Point location)
            {
                Label label = new Label { Text = text, Location = location, AutoSize = true };
                ApplySubHeaderLabelStyle(label);
                return label;
            }
            
            public Panel CreatePanel(Point location, Size size)
            {
                Panel panel = new Panel { Location = location, Size = size };
                ApplyPanelStyle(panel);
                return panel;
            }
            
            public TextBox CreateTextBox(Point location, Size size)
            {
                TextBox textBox = new TextBox { Location = location, Size = size };
                ApplyTextBoxStyle(textBox);
                return textBox;
            }
            
            public ComboBox CreateComboBox(Point location, Size size)
            {
                ComboBox comboBox = new ComboBox { Location = location, Size = size };
                ApplyComboBoxStyle(comboBox);
                return comboBox;
            }
        }
    }
}
