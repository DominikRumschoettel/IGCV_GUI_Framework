using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV_GUI_Framework.Common
{
    /// <summary>
    /// Provides consistent styling for the Fraunhofer corporate identity.
    /// This is a legacy class that now acts as a wrapper around the new theme system.
    /// </summary>
    [Obsolete("Use IGCV.GUI.Themes.ThemeManager and ITheme implementations instead. This class will be removed in a future version.")]
    public static class FraunhoferTheme
    {
        // Redirect colors to the new theme system
        public static Color PrimaryBlue => ThemeManager.CurrentTheme.SecondaryColor;
        public static Color DarkBlue => Color.FromArgb(0, 67, 119); // Map to IGCV.GUI.Themes.FraunhoferCI.FraunhoferTheme.SteelBlue
        public static Color Teal => Color.FromArgb(0, 169, 169);    // Legacy color maintained for compatibility
        public static Color Green => ThemeManager.CurrentTheme.PrimaryColor;
        public static Color DarkPanel => Color.FromArgb(0, 84, 129); // Map to IGCV.GUI.Themes.FraunhoferCI.FraunhoferTheme.DarkPanelColor
        
        // UI Element colors
        public static Color ButtonBackground => Color.FromArgb(230, 237, 243);
        public static Color FooterBackground => Color.FromArgb(240, 240, 240);
        public static Color TextColor => ThemeManager.CurrentTheme.TextOnDarkColor;
        public static Color DarkTextColor => ThemeManager.CurrentTheme.TextOnLightColor;
        
        // Font definitions - redirect to the new theme system
        public static Font HeaderFont => ThemeManager.CurrentTheme.HeaderFont;
        public static Font SubheaderFont => ThemeManager.CurrentTheme.SubHeaderFont;
        public static Font ButtonFont => ThemeManager.CurrentTheme.ButtonFont;
        public static Font ButtonFontBold => ThemeManager.CurrentTheme.ButtonFont; // Use standard button font
        public static Font TileFont => new Font(ThemeManager.CurrentTheme.BodyFont.FontFamily, 12F, FontStyle.Bold);
        
        /// <summary>
        /// Applies the Fraunhofer gradient background to a form
        /// </summary>
        public static void ApplyGradientBackground(Form form, PaintEventArgs e)
        {
            ThemeManager.CurrentTheme.ApplyGradientBackground(e, form.ClientRectangle);
        }
        
        /// <summary>
        /// Styles a button with the Fraunhofer primary style
        /// </summary>
        public static void StylePrimaryButton(Button button)
        {
            ThemeManager.CurrentTheme.ApplyPrimaryButtonStyle(button);
        }
        
        /// <summary>
        /// Styles a button with the Fraunhofer secondary style
        /// </summary>
        public static void StyleSecondaryButton(Button button)
        {
            ThemeManager.CurrentTheme.ApplySecondaryButtonStyle(button);
        }
        
        /// <summary>
        /// Creates a panel with the Fraunhofer dark panel style
        /// </summary>
        public static Panel CreateDarkPanel(int x, int y, int width, int height)
        {
            Panel panel = ThemeManager.CurrentTheme.CreatePanel(new Point(x, y), new Size(width, height));
            ThemeManager.CurrentTheme.ApplyPanelStyle(panel);
            return panel;
        }
        
        /// <summary>
        /// Creates a header label with Fraunhofer styling
        /// </summary>
        public static Label CreateHeaderLabel(string text, int x, int y)
        {
            Label label = ThemeManager.CurrentTheme.CreateHeaderLabel(text, new Point(x, y));
            return label;
        }
        
        /// <summary>
        /// Creates a subheader label with Fraunhofer styling
        /// </summary>
        public static Label CreateSubheaderLabel(string text, int x, int y)
        {
            Label label = ThemeManager.CurrentTheme.CreateSubHeaderLabel(text, new Point(x, y));
            return label;
        }
        
        /// <summary>
        /// Draws a status indicator light (red/green)
        /// </summary>
        public static void DrawStatusLight(PaintEventArgs e, bool isActive)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush brush = new SolidBrush(isActive ? 
                  ThemeManager.CurrentTheme.SuccessColor : ThemeManager.CurrentTheme.ErrorColor))
            {
                e.Graphics.FillEllipse(brush, 0, 0, 30, 30);
            }
            using (Pen pen = new Pen(Color.Gray, 1))
            {
                e.Graphics.DrawEllipse(pen, 0, 0, 30, 30);
            }
        }
    }
}
