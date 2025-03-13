using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IGCV_GUI_Framework.Common
{
    /// <summary>
    /// Provides consistent styling for the Fraunhofer corporate identity
    /// </summary>
    public static class FraunhoferTheme
    {
        // Fraunhofer corporate colors
        public static Color PrimaryBlue = Color.FromArgb(0, 103, 172);      // Main blue
        public static Color DarkBlue = Color.FromArgb(0, 67, 119);          // Dark blue for gradients
        public static Color Teal = Color.FromArgb(0, 169, 169);             // Teal for gradients
        public static Color Green = Color.FromArgb(0, 169, 132);            // Green for action buttons
        public static Color DarkPanel = Color.FromArgb(0, 84, 129);         // Mid blue for panels
        
        // UI Element colors
        public static Color ButtonBackground = Color.FromArgb(230, 237, 243);   // Light gray-blue
        public static Color FooterBackground = Color.FromArgb(240, 240, 240);   // Light gray
        public static Color TextColor = Color.White;                            // White text on dark bg
        public static Color DarkTextColor = Color.FromArgb(70, 70, 70);         // Dark text on light bg
        
        // Font definitions
        public static Font HeaderFont = new Font("Segoe UI", 24F, FontStyle.Bold);
        public static Font SubheaderFont = new Font("Segoe UI", 14F, FontStyle.Regular);
        public static Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static Font ButtonFontBold = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static Font TileFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        
        /// <summary>
        /// Applies the Fraunhofer gradient background to a form
        /// </summary>
        public static void ApplyGradientBackground(Form form, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                form.ClientRectangle,
                DarkBlue,
                Teal,
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, form.ClientRectangle);
            }
        }
        
        /// <summary>
        /// Styles a button with the Fraunhofer primary style
        /// </summary>
        public static void StylePrimaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Green;
            button.ForeColor = TextColor;
            button.Font = ButtonFontBold;
            button.FlatAppearance.BorderSize = 0;
        }
        
        /// <summary>
        /// Styles a button with the Fraunhofer secondary style
        /// </summary>
        public static void StyleSecondaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = ButtonBackground;
            button.ForeColor = DarkTextColor;
            button.Font = ButtonFont;
            button.FlatAppearance.BorderSize = 0;
        }
        
        /// <summary>
        /// Creates a panel with the Fraunhofer dark panel style
        /// </summary>
        public static Panel CreateDarkPanel(int x, int y, int width, int height)
        {
            return new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = DarkPanel
            };
        }
        
        /// <summary>
        /// Creates a header label with Fraunhofer styling
        /// </summary>
        public static Label CreateHeaderLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = HeaderFont,
                ForeColor = TextColor,
                Location = new Point(x, y)
            };
        }
        
        /// <summary>
        /// Creates a subheader label with Fraunhofer styling
        /// </summary>
        public static Label CreateSubheaderLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = SubheaderFont,
                ForeColor = TextColor,
                Location = new Point(x, y)
            };
        }
        
        /// <summary>
        /// Draws a status indicator light (red/green)
        /// </summary>
        public static void DrawStatusLight(PaintEventArgs e, bool isActive)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush brush = new SolidBrush(isActive ? Color.LimeGreen : Color.Red))
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
