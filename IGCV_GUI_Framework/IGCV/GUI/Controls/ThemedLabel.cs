using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A label control that supports themes with styling options
    /// </summary>
    public class ThemedLabel : Label, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 0;
        private Color _borderColor = Color.Transparent;
        private int _borderWidth = 0;
        private LabelStyle _labelStyle = LabelStyle.Default;
        private LabelSize _labelSize = LabelSize.Normal;
        private bool _useGradientText = false;
        private Color _gradientTextStartColor = Color.Empty;
        private Color _gradientTextEndColor = Color.Empty;
        private LinearGradientMode _gradientTextMode = LinearGradientMode.Vertical;
        
        /// <summary>
        /// Initializes a new instance of the ThemedLabel control
        /// </summary>
        public ThemedLabel()
        {
            // Enable custom painting
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            
            // Set initial properties
            AutoSize = true;
            BackColor = Color.Transparent;
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius for the label (if background is drawn)
        /// </summary>
        [Category("Appearance")]
        [Description("The radius of the label corners when background is visible")]
        [DefaultValue(0)]
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
        /// Gets or sets the border color for the label
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the label border")]
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
        /// Gets or sets the border width for the label
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the label border")]
        [DefaultValue(0)]
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
        /// Gets or sets the label style
        /// </summary>
        [Category("Appearance")]
        [Description("The predefined style for the label")]
        [DefaultValue(LabelStyle.Default)]
        public LabelStyle LabelStyle
        {
            get => _labelStyle;
            set
            {
                if (_labelStyle != value)
                {
                    _labelStyle = value;
                    ApplyTheme();
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the label size
        /// </summary>
        [Category("Appearance")]
        [Description("The predefined size for the label")]
        [DefaultValue(LabelSize.Normal)]
        public LabelSize LabelSize
        {
            get => _labelSize;
            set
            {
                if (_labelSize != value)
                {
                    _labelSize = value;
                    ApplyTheme();
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets whether to use a gradient for the text
        /// </summary>
        [Category("Appearance")]
        [Description("Whether to use a gradient for the text")]
        [DefaultValue(false)]
        public bool UseGradientText
        {
            get => _useGradientText;
            set
            {
                if (_useGradientText != value)
                {
                    _useGradientText = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the gradient start color for the text
        /// </summary>
        [Category("Appearance")]
        [Description("The starting color of the text gradient")]
        public Color GradientTextStartColor
        {
            get => _gradientTextStartColor;
            set
            {
                if (_gradientTextStartColor != value)
                {
                    _gradientTextStartColor = value;
                    if (_useGradientText) Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the gradient end color for the text
        /// </summary>
        [Category("Appearance")]
        [Description("The ending color of the text gradient")]
        public Color GradientTextEndColor
        {
            get => _gradientTextEndColor;
            set
            {
                if (_gradientTextEndColor != value)
                {
                    _gradientTextEndColor = value;
                    if (_useGradientText) Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the gradient mode for the text
        /// </summary>
        [Category("Appearance")]
        [Description("The direction of the text gradient")]
        [DefaultValue(LinearGradientMode.Vertical)]
        public LinearGradientMode GradientTextMode
        {
            get => _gradientTextMode;
            set
            {
                if (_gradientTextMode != value)
                {
                    _gradientTextMode = value;
                    if (_useGradientText) Invalidate();
                }
            }
        }
        
        #endregion
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this label based on LabelStyle
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use ThemeManager's current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            
            if (theme == null) return;
            
            // Apply the appropriate style based on LabelStyle
            switch (_labelStyle)
            {
                case LabelStyle.Default:
                    ForeColor = theme.TextOnLightColor;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.Header:
                    theme.ApplyHeaderLabelStyle(this);
                    break;
                
                case LabelStyle.SubHeader:
                    theme.ApplySubHeaderLabelStyle(this);
                    break;
                
                case LabelStyle.Primary:
                    ForeColor = theme.PrimaryColor;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.Secondary:
                    ForeColor = theme.SecondaryColor;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.Accent:
                    ForeColor = theme.AccentColor;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.Inverted:
                    BackColor = theme.TextOnLightColor;
                    ForeColor = Color.White;
                    _cornerRadius = 3;
                    Padding = new Padding(5);
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.Success:
                    ForeColor = theme.SuccessColor;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.Warning:
                    ForeColor = theme.WarningColor;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.Error:
                    ForeColor = theme.ErrorColor;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
                
                case LabelStyle.GradientText:
                    _useGradientText = true;
                    _gradientTextStartColor = theme.PrimaryColor;
                    _gradientTextEndColor = theme.SecondaryColor;
                    _gradientTextMode = LinearGradientMode.Horizontal;
                    BackColor = Color.Transparent;
                    _borderWidth = 0;
                    break;
            }
            
            // Apply font size based on LabelSize
            switch (_labelSize)
            {
                case LabelSize.XSmall:
                    Font = theme.SmallFont;
                    break;
                
                case LabelSize.Small:
                    Font = new Font(theme.SmallFont.FontFamily, theme.SmallFont.Size + 1f);
                    break;
                
                case LabelSize.Normal:
                    Font = theme.BodyFont;
                    break;
                
                case LabelSize.Medium:
                    Font = theme.SubHeaderFont;
                    break;
                
                case LabelSize.Large:
                    Font = new Font(theme.SubHeaderFont.FontFamily, theme.SubHeaderFont.Size + 2f);
                    break;
                
                case LabelSize.XLarge:
                    Font = theme.HeaderFont;
                    break;
            }
        }
        
        #endregion
        
        #region Painting
        
        protected override void OnPaint(PaintEventArgs e)
        {
            // Enable anti-aliasing for smoother text and shapes
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            Rectangle rect = ClientRectangle;
            
            // Draw background if not transparent
            if (BackColor != Color.Transparent)
            {
                using (GraphicsPath path = CreateRoundedRectangle(rect, _cornerRadius))
                {
                    using (SolidBrush brush = new SolidBrush(BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    
                    // Draw border if needed
                    if (_borderWidth > 0 && _borderColor != Color.Transparent)
                    {
                        using (Pen pen = new Pen(_borderColor, _borderWidth))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                }
            }
            
            // Draw the text
            if (!string.IsNullOrEmpty(Text))
            {
                // Adjust rectangle for padding
                Rectangle textRect = new Rectangle(
                    rect.X + Padding.Left,
                    rect.Y + Padding.Top,
                    rect.Width - Padding.Horizontal,
                    rect.Height - Padding.Vertical);
                
                if (_useGradientText && _gradientTextStartColor != Color.Empty && _gradientTextEndColor != Color.Empty)
                {
                    // Draw gradient text
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        textRect,
                        _gradientTextStartColor,
                        _gradientTextEndColor,
                        _gradientTextMode))
                    {
                        using (StringFormat format = CreateStringFormat())
                        {
                            e.Graphics.DrawString(Text, Font, brush, textRect, format);
                        }
                    }
                }
                else
                {
                    // Draw normal text
                    using (SolidBrush brush = new SolidBrush(ForeColor))
                    {
                        using (StringFormat format = CreateStringFormat())
                        {
                            e.Graphics.DrawString(Text, Font, brush, textRect, format);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Creates a string format based on the current text alignment
        /// </summary>
        private StringFormat CreateStringFormat()
        {
            StringFormat format = new StringFormat();
            
            // Set horizontal alignment
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    format.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    format.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    format.Alignment = StringAlignment.Far;
                    break;
            }
            
            // Set vertical alignment
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    format.LineAlignment = StringAlignment.Far;
                    break;
            }
            
            return format;
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
            
            // Add padding
            preferredSize.Width += Padding.Horizontal;
            preferredSize.Height += Padding.Vertical;
            
            // Add space for border
            if (_borderWidth > 0)
            {
                preferredSize.Width += _borderWidth * 2;
                preferredSize.Height += _borderWidth * 2;
            }
            
            return preferredSize;
        }
        
        #endregion
    }
    
    /// <summary>
    /// Predefined label styles
    /// </summary>
    public enum LabelStyle
    {
        /// <summary>Default label style with normal text</summary>
        Default,
        /// <summary>Header style with larger, bold text</summary>
        Header,
        /// <summary>Subheader style with medium-sized text</summary>
        SubHeader,
        /// <summary>Label with primary color</summary>
        Primary,
        /// <summary>Label with secondary color</summary>
        Secondary,
        /// <summary>Label with accent color</summary>
        Accent,
        /// <summary>Inverted colors (dark background, light text)</summary>
        Inverted,
        /// <summary>Success message style (typically green)</summary>
        Success,
        /// <summary>Warning message style (typically yellow/amber)</summary>
        Warning,
        /// <summary>Error message style (typically red)</summary>
        Error,
        /// <summary>Text with gradient effect</summary>
        GradientText
    }
    
    /// <summary>
    /// Predefined label sizes
    /// </summary>
    public enum LabelSize
    {
        /// <summary>Extra small text</summary>
        XSmall,
        /// <summary>Small text</summary>
        Small,
        /// <summary>Normal text size</summary>
        Normal,
        /// <summary>Medium text size</summary>
        Medium,
        /// <summary>Large text size</summary>
        Large,
        /// <summary>Extra large text size</summary>
        XLarge
    }
}
