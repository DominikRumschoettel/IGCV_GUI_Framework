using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A check box control that supports themes with custom styling options
    /// </summary>
    public class ThemedCheckBox : CheckBox, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 3;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private Color _checkColor = Color.Empty;
        private Color _boxColor = Color.White;
        private CheckBoxStyle _checkBoxStyle = CheckBoxStyle.Default;
        
        /// <summary>
        /// Initializes a new instance of the ThemedCheckBox control
        /// </summary>
        public ThemedCheckBox()
        {
            // Enable custom painting
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            
            // Set default properties
            BackColor = Color.Transparent;
            AutoSize = true;
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius for the check box
        /// </summary>
        [Category("Appearance")]
        [Description("The radius of the check box corners")]
        [DefaultValue(3)]
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
        
        /// <summary>
        /// Gets or sets the border color for the check box
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the check box border")]
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
        
        /// <summary>
        /// Gets or sets the border width for the check box
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the check box border")]
        [DefaultValue(1)]
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
        
        /// <summary>
        /// Gets or sets the color of the check mark
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the check mark")]
        public Color CheckColor
        {
            get => _checkColor;
            set
            {
                if (_checkColor != value)
                {
                    _checkColor = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the background color of the check box
        /// </summary>
        [Category("Appearance")]
        [Description("The background color of the check box")]
        public Color BoxColor
        {
            get => _boxColor;
            set
            {
                if (_boxColor != value)
                {
                    _boxColor = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the check box style
        /// </summary>
        [Category("Appearance")]
        [Description("The predefined style for the check box")]
        [DefaultValue(CheckBoxStyle.Default)]
        public CheckBoxStyle CheckBoxStyle
        {
            get => _checkBoxStyle;
            set
            {
                if (_checkBoxStyle != value)
                {
                    _checkBoxStyle = value;
                    ApplyTheme();
                    Invalidate();
                }
            }
        }
        
        #endregion
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this check box based on CheckBoxStyle
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use ThemeManager's current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            
            if (theme == null) return;
            
            // Apply common properties
            Font = theme.BodyFont;
            
            // Apply the appropriate style based on CheckBoxStyle
            switch (_checkBoxStyle)
            {
                case CheckBoxStyle.Default:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.BorderColor;
                    _boxColor = Color.White;
                    _checkColor = theme.TextOnLightColor;
                    break;
                
                case CheckBoxStyle.Primary:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.PrimaryColor;
                    _boxColor = Color.White;
                    _checkColor = theme.PrimaryColor;
                    break;
                
                case CheckBoxStyle.Secondary:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.SecondaryColor;
                    _boxColor = Color.White;
                    _checkColor = theme.SecondaryColor;
                    break;
                
                case CheckBoxStyle.Accent:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.AccentColor;
                    _boxColor = Color.White;
                    _checkColor = theme.AccentColor;
                    break;
                
                case CheckBoxStyle.Filled:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.PrimaryColor;
                    _boxColor = theme.PrimaryColor;
                    _checkColor = theme.TextOnDarkColor;
                    break;
            }
        }
        
        #endregion
        
        #region Painting
        
        protected override void OnPaint(PaintEventArgs e)
        {
            // Set up high quality rendering for both shapes and text
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            
            // Clear the background (important for transparent rendering)
            if (BackColor == Color.Transparent)
            {
                e.Graphics.Clear(Parent?.BackColor ?? SystemColors.Control);
            }
            else
            {
                e.Graphics.Clear(BackColor);
            }
            
            // Set default check color if not specified
            Color checkColor = _checkColor == Color.Empty ? ForeColor : _checkColor;
            
            // Calculate check box size and position
            int checkSize = Font.Height - 2;
            Rectangle boxRect = new Rectangle(0, (Height - checkSize) / 2, checkSize, checkSize);
            
            // Draw check box background
            using (GraphicsPath path = CreateRoundedRectangle(boxRect, _cornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(_boxColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
                
                // Draw border
                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
            
            // Draw check mark if checked
            if (Checked)
            {
                // Adjust check mark size and position
                Rectangle checkRect = new Rectangle(
                    boxRect.X + 3,
                    boxRect.Y + 3,
                    boxRect.Width - 6,
                    boxRect.Height - 6);
                
                // Draw check mark
                using (Pen pen = new Pen(checkColor, 2))
                {
                    // Draw a check mark (√)
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    
                    Point[] checkMarkPoints = new Point[]
                    {
                        new Point(checkRect.X, checkRect.Y + checkRect.Height / 2),
                        new Point(checkRect.X + checkRect.Width / 3, checkRect.Y + checkRect.Height - 2),
                        new Point(checkRect.X + checkRect.Width, checkRect.Y)
                    };
                    
                    e.Graphics.DrawLines(pen, checkMarkPoints);
                }
            }
            
            // Draw text
            if (!string.IsNullOrEmpty(Text))
            {
                Rectangle textRect = new Rectangle(
                    boxRect.Right + 5,
                    0,
                    Width - boxRect.Right - 5,
                    Height);
                
                // Use TextRenderer.DrawText instead of e.Graphics.DrawString for clearer text
                TextRenderer.DrawText(
                    e.Graphics,
                    Text,
                    Font,
                    textRect,
                    ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding | TextFormatFlags.PreserveGraphicsClipping);
            }
        }
        
        /// <summary>
        /// Creates a rounded rectangle graphics path
        /// </summary>
        private GraphicsPath CreateRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            
            if (radius > 0)
            {
                // Ensure radius is not too large for the rectangle
                radius = Math.Min(radius, Math.Min(rect.Width, rect.Height) / 2);
                
                // Top-left arc
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                
                // Top-right arc
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                
                // Bottom-right arc
                path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                
                // Bottom-left arc
                path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                
                // Close the path
                path.CloseFigure();
            }
            else
            {
                // If radius is 0, create a simple rectangle
                path.AddRectangle(rect);
            }
            
            return path;
        }
        
        /// <summary>
        /// Calculate the preferred size of the control
        /// </summary>
        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = base.GetPreferredSize(proposedSize);
            
            // Add padding for check box
            preferredSize.Width += Font.Height + 5;
            
            return preferredSize;
        }
        
        #endregion
    }
    
    /// <summary>
    /// Predefined check box styles
    /// </summary>
    public enum CheckBoxStyle
    {
        /// <summary>Default check box style</summary>
        Default,
        /// <summary>Check box with primary color</summary>
        Primary,
        /// <summary>Check box with secondary color</summary>
        Secondary,
        /// <summary>Check box with accent color</summary>
        Accent,
        /// <summary>Filled check box with primary color background</summary>
        Filled
    }
}
