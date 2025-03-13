using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IGCV.GUI.Themes
{
    /// <summary>
    /// Base abstract class for IGCV GUI Themes that implements the ITheme interface
    /// </summary>
    public abstract class ThemeBase : ITheme
    {
        #region Names and Identifiers
        
        /// <summary>
        /// Gets the name of the theme
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// Gets the theme version
        /// </summary>
        public virtual Version Version => new Version(1, 0, 0, 0);
        
        #endregion
        
        #region Colors
        
        /// <summary>
        /// Primary color of the theme
        /// </summary>
        public abstract Color PrimaryColor { get; }
        
        /// <summary>
        /// Secondary color of the theme
        /// </summary>
        public abstract Color SecondaryColor { get; }
        
        /// <summary>
        /// Accent color of the theme for highlighting elements
        /// </summary>
        public abstract Color AccentColor { get; }
        
        /// <summary>
        /// Background color for the application
        /// </summary>
        public abstract Color BackgroundColor { get; }
        
        /// <summary>
        /// Text color on light backgrounds
        /// </summary>
        public abstract Color TextOnLightColor { get; }
        
        /// <summary>
        /// Text color on dark backgrounds
        /// </summary>
        public abstract Color TextOnDarkColor { get; }
        
        /// <summary>
        /// Success color (typically green)
        /// </summary>
        public abstract Color SuccessColor { get; }
        
        /// <summary>
        /// Warning color (typically yellow/amber)
        /// </summary>
        public abstract Color WarningColor { get; }
        
        /// <summary>
        /// Error color (typically red)
        /// </summary>
        public abstract Color ErrorColor { get; }
        
        #endregion
        
        #region Fonts
        
        /// <summary>
        /// Main font for headers
        /// </summary>
        public abstract Font HeaderFont { get; }
        
        /// <summary>
        /// Font for subheaders
        /// </summary>
        public abstract Font SubHeaderFont { get; }
        
        /// <summary>
        /// Font for regular text
        /// </summary>
        public abstract Font BodyFont { get; }
        
        /// <summary>
        /// Font for buttons
        /// </summary>
        public abstract Font ButtonFont { get; }
        
        /// <summary>
        /// Font for smaller UI elements
        /// </summary>
        public abstract Font SmallFont { get; }
        
        #endregion
        
        #region Shapes and Borders
        
        /// <summary>
        /// Corner radius for rounded elements (0 for sharp corners)
        /// </summary>
        public abstract int CornerRadius { get; }
        
        /// <summary>
        /// Border width for elements
        /// </summary>
        public abstract int BorderWidth { get; }
        
        /// <summary>
        /// Default border color
        /// </summary>
        public abstract Color BorderColor { get; }
        
        #endregion
        
        #region Spacing
        
        /// <summary>
        /// Small spacing value (typically 4-8 pixels)
        /// </summary>
        public virtual int SpacingSmall => 4;
        
        /// <summary>
        /// Medium spacing value (typically 8-16 pixels)
        /// </summary>
        public virtual int SpacingMedium => 8;
        
        /// <summary>
        /// Large spacing value (typically 16-32 pixels)
        /// </summary>
        public virtual int SpacingLarge => 16;
        
        #endregion
        
        #region Element Styling Methods
        
        /// <summary>
        /// Apply the theme's primary button style
        /// </summary>
        public virtual void ApplyPrimaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = PrimaryColor;
            button.ForeColor = TextOnDarkColor;
            button.Font = ButtonFont;
            button.FlatAppearance.BorderSize = BorderWidth;
            button.FlatAppearance.BorderColor = BorderColor;
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the theme's secondary button style
        /// </summary>
        public virtual void ApplySecondaryButtonStyle(Button button)
        {
            if (button == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = SecondaryColor;
            button.ForeColor = TextOnLightColor;
            button.Font = ButtonFont;
            button.FlatAppearance.BorderSize = BorderWidth;
            button.FlatAppearance.BorderColor = BorderColor;
            
            // Apply corner radius if supported by the control
            if (button is IThemeableControl themeableButton)
            {
                themeableButton.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the theme's tertiary/text button style
        /// </summary>
        public virtual void ApplyTertiaryButtonStyle(Button button)
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
        /// Apply the theme's label style for headers
        /// </summary>
        public virtual void ApplyHeaderLabelStyle(Label label)
        {
            if (label == null) return;
            
            label.Font = HeaderFont;
            label.ForeColor = TextOnLightColor;
            label.AutoSize = true;
        }
        
        /// <summary>
        /// Apply the theme's label style for subheaders
        /// </summary>
        public virtual void ApplySubHeaderLabelStyle(Label label)
        {
            if (label == null) return;
            
            label.Font = SubHeaderFont;
            label.ForeColor = TextOnLightColor;
            label.AutoSize = true;
        }
        
        /// <summary>
        /// Apply the theme's panel style
        /// </summary>
        public virtual void ApplyPanelStyle(Panel panel)
        {
            if (panel == null) return;
            
            panel.BackColor = BackgroundColor;
            panel.BorderStyle = BorderStyle.None;
            
            // Apply corner radius if supported by the control
            if (panel is IThemeableControl themeablePanel)
            {
                themeablePanel.CornerRadius = CornerRadius;
            }
        }
        
        /// <summary>
        /// Apply the theme's text box style
        /// </summary>
        public virtual void ApplyTextBoxStyle(TextBox textBox)
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
        
        /// <summary>
        /// Apply the theme's combo box style
        /// </summary>
        public virtual void ApplyComboBoxStyle(ComboBox comboBox)
        {
            if (comboBox == null) return;
            
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = BodyFont;
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = TextOnLightColor;
        }
        
        /// <summary>
        /// Apply gradient background to a form or control
        /// </summary>
        /// <param name="e">Paint event arguments</param>
        /// <param name="bounds">Rectangle defining the area to fill</param>
        public virtual void ApplyGradientBackground(PaintEventArgs e, Rectangle bounds)
        {
            if (e == null) return;
            
            using (LinearGradientBrush brush = new LinearGradientBrush(
                bounds,
                PrimaryColor,
                SecondaryColor,
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, bounds);
            }
        }
        
        #endregion
        
        #region Factory Methods
        
        /// <summary>
        /// Creates a themed primary button
        /// </summary>
        public virtual Button CreatePrimaryButton(string text, Point location, Size size)
        {
            var button = new Button
            {
                Text = text,
                Location = location,
                Size = size
            };
            
            ApplyPrimaryButtonStyle(button);
            return button;
        }
        
        /// <summary>
        /// Creates a themed secondary button
        /// </summary>
        public virtual Button CreateSecondaryButton(string text, Point location, Size size)
        {
            var button = new Button
            {
                Text = text,
                Location = location,
                Size = size
            };
            
            ApplySecondaryButtonStyle(button);
            return button;
        }
        
        /// <summary>
        /// Creates a themed header label
        /// </summary>
        public virtual Label CreateHeaderLabel(string text, Point location)
        {
            var label = new Label
            {
                Text = text,
                Location = location,
                AutoSize = true
            };
            
            ApplyHeaderLabelStyle(label);
            return label;
        }
        
        /// <summary>
        /// Creates a themed subheader label
        /// </summary>
        public virtual Label CreateSubHeaderLabel(string text, Point location)
        {
            var label = new Label
            {
                Text = text,
                Location = location,
                AutoSize = true
            };
            
            ApplySubHeaderLabelStyle(label);
            return label;
        }
        
        /// <summary>
        /// Creates a themed panel
        /// </summary>
        public virtual Panel CreatePanel(Point location, Size size)
        {
            var panel = new Panel
            {
                Location = location,
                Size = size
            };
            
            ApplyPanelStyle(panel);
            return panel;
        }
        
        /// <summary>
        /// Creates a themed text box
        /// </summary>
        public virtual TextBox CreateTextBox(Point location, Size size)
        {
            var textBox = new TextBox
            {
                Location = location,
                Size = size
            };
            
            ApplyTextBoxStyle(textBox);
            return textBox;
        }
        
        /// <summary>
        /// Creates a themed combo box
        /// </summary>
        public virtual ComboBox CreateComboBox(Point location, Size size)
        {
            var comboBox = new ComboBox
            {
                Location = location,
                Size = size,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            ApplyComboBoxStyle(comboBox);
            return comboBox;
        }
        
        #endregion
    }
}
