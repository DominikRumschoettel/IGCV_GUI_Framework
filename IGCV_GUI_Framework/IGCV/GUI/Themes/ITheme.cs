using System;
using System.Drawing;
using System.Windows.Forms;

namespace IGCV.GUI.Themes
{
    /// <summary>
    /// Interface defining the theme capabilities of the IGCV GUI framework
    /// </summary>
    public interface ITheme
    {
        #region Names and Identifiers
        
        /// <summary>
        /// Gets the name of the theme
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the theme version
        /// </summary>
        Version Version { get; }
        
        #endregion
        
        #region Colors
        
        /// <summary>
        /// Primary color of the theme
        /// </summary>
        Color PrimaryColor { get; }
        
        /// <summary>
        /// Secondary color of the theme
        /// </summary>
        Color SecondaryColor { get; }
        
        /// <summary>
        /// Accent color of the theme for highlighting elements
        /// </summary>
        Color AccentColor { get; }
        
        /// <summary>
        /// Background color for the application
        /// </summary>
        Color BackgroundColor { get; }
        
        /// <summary>
        /// Text color on light backgrounds
        /// </summary>
        Color TextOnLightColor { get; }
        
        /// <summary>
        /// Text color on dark backgrounds
        /// </summary>
        Color TextOnDarkColor { get; }
        
        /// <summary>
        /// Success color (typically green)
        /// </summary>
        Color SuccessColor { get; }
        
        /// <summary>
        /// Warning color (typically yellow/amber)
        /// </summary>
        Color WarningColor { get; }
        
        /// <summary>
        /// Error color (typically red)
        /// </summary>
        Color ErrorColor { get; }
        
        #endregion
        
        #region Fonts
        
        /// <summary>
        /// Main font for headers
        /// </summary>
        Font HeaderFont { get; }
        
        /// <summary>
        /// Font for subheaders
        /// </summary>
        Font SubHeaderFont { get; }
        
        /// <summary>
        /// Font for regular text
        /// </summary>
        Font BodyFont { get; }
        
        /// <summary>
        /// Font for buttons
        /// </summary>
        Font ButtonFont { get; }
        
        /// <summary>
        /// Font for smaller UI elements
        /// </summary>
        Font SmallFont { get; }
        
        #endregion
        
        #region Shapes and Borders
        
        /// <summary>
        /// Corner radius for rounded elements (0 for sharp corners)
        /// </summary>
        int CornerRadius { get; }
        
        /// <summary>
        /// Border width for elements
        /// </summary>
        int BorderWidth { get; }
        
        /// <summary>
        /// Default border color
        /// </summary>
        Color BorderColor { get; }
        
        #endregion
        
        #region Spacing
        
        /// <summary>
        /// Small spacing value (typically 4-8 pixels)
        /// </summary>
        int SpacingSmall { get; }
        
        /// <summary>
        /// Medium spacing value (typically 8-16 pixels)
        /// </summary>
        int SpacingMedium { get; }
        
        /// <summary>
        /// Large spacing value (typically 16-32 pixels)
        /// </summary>
        int SpacingLarge { get; }
        
        #endregion
        
        #region Element Styling Methods
        
        /// <summary>
        /// Apply the theme's primary button style
        /// </summary>
        void ApplyPrimaryButtonStyle(Button button);
        
        /// <summary>
        /// Apply the theme's secondary button style
        /// </summary>
        void ApplySecondaryButtonStyle(Button button);
        
        /// <summary>
        /// Apply the theme's tertiary/text button style
        /// </summary>
        void ApplyTertiaryButtonStyle(Button button);
        
        /// <summary>
        /// Apply the theme's label style for headers
        /// </summary>
        void ApplyHeaderLabelStyle(Label label);
        
        /// <summary>
        /// Apply the theme's label style for subheaders
        /// </summary>
        void ApplySubHeaderLabelStyle(Label label);
        
        /// <summary>
        /// Apply the theme's panel style
        /// </summary>
        void ApplyPanelStyle(Panel panel);
        
        /// <summary>
        /// Apply the theme's text box style
        /// </summary>
        void ApplyTextBoxStyle(TextBox textBox);
        
        /// <summary>
        /// Apply the theme's combo box style
        /// </summary>
        void ApplyComboBoxStyle(ComboBox comboBox);
        
        /// <summary>
        /// Apply gradient background to a form or control
        /// </summary>
        /// <param name="e">Paint event arguments</param>
        /// <param name="bounds">Rectangle defining the area to fill</param>
        void ApplyGradientBackground(PaintEventArgs e, Rectangle bounds);
        
        #endregion
        
        #region Factory Methods
        
        /// <summary>
        /// Creates a themed primary button
        /// </summary>
        Button CreatePrimaryButton(string text, Point location, Size size);
        
        /// <summary>
        /// Creates a themed secondary button
        /// </summary>
        Button CreateSecondaryButton(string text, Point location, Size size);
        
        /// <summary>
        /// Creates a themed header label
        /// </summary>
        Label CreateHeaderLabel(string text, Point location);
        
        /// <summary>
        /// Creates a themed subheader label
        /// </summary>
        Label CreateSubHeaderLabel(string text, Point location);
        
        /// <summary>
        /// Creates a themed panel
        /// </summary>
        Panel CreatePanel(Point location, Size size);
        
        /// <summary>
        /// Creates a themed text box
        /// </summary>
        TextBox CreateTextBox(Point location, Size size);
        
        /// <summary>
        /// Creates a themed combo box
        /// </summary>
        ComboBox CreateComboBox(Point location, Size size);
        
        #endregion
    }
}
