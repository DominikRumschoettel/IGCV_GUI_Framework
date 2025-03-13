using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A radio button control that supports themes with custom styling options
    /// </summary>
    public class ThemedRadioButton : RadioButton, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 0; // Not used for radio buttons, but required by interface
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private Color _checkColor = Color.Empty;
        private Color _circleColor = Color.White;
        private RadioButtonStyle _radioButtonStyle = RadioButtonStyle.Default;
        
        /// <summary>
        /// Initializes a new instance of the ThemedRadioButton control
        /// </summary>
        public ThemedRadioButton()
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
        /// Gets or sets the corner radius (not used for radio buttons, but required by interface)
        /// </summary>
        [Category("Appearance")]
        [Description("Not used for radio buttons")]
        [DefaultValue(0)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set => _cornerRadius = value; // Not used for radio buttons
        }
        
        /// <summary>
        /// Gets or sets the border color for the radio button
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the radio button border")]
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
        /// Gets or sets the border width for the radio button
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the radio button border")]
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
        /// Gets or sets the background color of the radio button circle
        /// </summary>
        [Category("Appearance")]
        [Description("The background color of the radio button circle")]
        public Color CircleColor
        {
            get => _circleColor;
            set
            {
                if (_circleColor != value)
                {
                    _circleColor = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the radio button style
        /// </summary>
        [Category("Appearance")]
        [Description("The predefined style for the radio button")]
        [DefaultValue(RadioButtonStyle.Default)]
        public RadioButtonStyle RadioButtonStyle
        {
            get => _radioButtonStyle;
            set
            {
                if (_radioButtonStyle != value)
                {
                    _radioButtonStyle = value;
                    ApplyTheme();
                    Invalidate();
                }
            }
        }
        
        #endregion
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this radio button based on RadioButtonStyle
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use ThemeManager's current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            
            if (theme == null) return;
            
            // Apply common properties
            Font = theme.BodyFont;
            
            // Apply the appropriate style based on RadioButtonStyle
            switch (_radioButtonStyle)
            {
                case RadioButtonStyle.Default:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.BorderColor;
                    _circleColor = Color.White;
                    _checkColor = theme.TextOnLightColor;
                    break;
                
                case RadioButtonStyle.Primary:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.PrimaryColor;
                    _circleColor = Color.White;
                    _checkColor = theme.PrimaryColor;
                    break;
                
                case RadioButtonStyle.Secondary:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.SecondaryColor;
                    _circleColor = Color.White;
                    _checkColor = theme.SecondaryColor;
                    break;
                
                case RadioButtonStyle.Accent:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.AccentColor;
                    _circleColor = Color.White;
                    _checkColor = theme.AccentColor;
                    break;
                
                case RadioButtonStyle.Filled:
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.PrimaryColor;
                    _circleColor = theme.PrimaryColor;
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
            
            // Calculate radio button size and position
            int circleSize = Font.Height - 2;
            Rectangle circleRect = new Rectangle(0, (Height - circleSize) / 2, circleSize, circleSize);
            
            // Draw outer circle
            using (SolidBrush brush = new SolidBrush(_circleColor))
            {
                e.Graphics.FillEllipse(brush, circleRect);
            }
            
            using (Pen pen = new Pen(_borderColor, _borderWidth))
            {
                e.Graphics.DrawEllipse(pen, circleRect);
            }
            
            // Draw inner circle if checked
            if (Checked)
            {
                int innerSize = circleSize - 8;
                Rectangle innerRect = new Rectangle(
                    circleRect.X + 4,
                    circleRect.Y + 4,
                    innerSize,
                    innerSize);
                
                using (SolidBrush brush = new SolidBrush(checkColor))
                {
                    e.Graphics.FillEllipse(brush, innerRect);
                }
            }
            
            // Draw text
            if (!string.IsNullOrEmpty(Text))
            {
                Rectangle textRect = new Rectangle(
                    circleRect.Right + 5,
                    0,
                    Width - circleRect.Right - 5,
                    Height);
                
                // Use improved text rendering options to prevent shadow effect
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
        /// Calculate the preferred size of the control
        /// </summary>
        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = base.GetPreferredSize(proposedSize);
            
            // Add padding for radio button
            preferredSize.Width += Font.Height + 5;
            
            return preferredSize;
        }
        
        #endregion
    }
    
    /// <summary>
    /// Predefined radio button styles
    /// </summary>
    public enum RadioButtonStyle
    {
        /// <summary>Default radio button style</summary>
        Default,
        /// <summary>Radio button with primary color</summary>
        Primary,
        /// <summary>Radio button with secondary color</summary>
        Secondary,
        /// <summary>Radio button with accent color</summary>
        Accent,
        /// <summary>Filled radio button with primary color background</summary>
        Filled
    }
}
