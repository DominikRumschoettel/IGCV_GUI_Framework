using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A progress bar control that supports themes with custom styling options
    /// </summary>
    public class ThemedProgressBar : Control, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 3;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private Color _progressColor = Color.Empty;
        private Color _backgroundColor = Color.WhiteSmoke;
        private ThemedProgressBarStyle _progressBarStyle = ThemedProgressBarStyle.Default;
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;
        private bool _showPercentage = false;
        private bool _useGradient = false;
        private Color _gradientStartColor = Color.Empty;
        private Color _gradientEndColor = Color.Empty;
        
        /// <summary>
        /// Initializes a new instance of the ThemedProgressBar control
        /// </summary>
        public ThemedProgressBar()
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
            Size = new Size(200, 20);
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius for the progress bar
        /// </summary>
        [Category("Appearance")]
        [Description("The radius of the progress bar corners")]
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
        /// Gets or sets the border color for the progress bar
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the progress bar border")]
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
        /// Gets or sets the border width for the progress bar
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the progress bar border")]
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
        /// Gets or sets the color of the progress indicator
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the progress indicator")]
        public Color ProgressColor
        {
            get => _progressColor;
            set
            {
                if (_progressColor != value)
                {
                    _progressColor = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the background color of the progress bar
        /// </summary>
        [Category("Appearance")]
        [Description("The background color of the progress bar")]
        public Color BarBackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the progress bar style
        /// </summary>
        [Category("Appearance")]
        [Description("The predefined style for the progress bar")]
        [DefaultValue(ThemedProgressBarStyle.Default)]
        public ThemedProgressBarStyle ProgressBarStyle
        {
            get => _progressBarStyle;
            set
            {
                if (_progressBarStyle != value)
                {
                    _progressBarStyle = value;
                    ApplyTheme();
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the minimum value of the progress bar
        /// </summary>
        [Category("Behavior")]
        [Description("The minimum value of the progress bar")]
        [DefaultValue(0)]
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (_minimum != value)
                {
                    _minimum = value;
                    if (_maximum < _minimum)
                        _maximum = _minimum;
                    if (_value < _minimum)
                        _value = _minimum;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the maximum value of the progress bar
        /// </summary>
        [Category("Behavior")]
        [Description("The maximum value of the progress bar")]
        [DefaultValue(100)]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (_maximum != value)
                {
                    _maximum = value;
                    if (_maximum < _minimum)
                        _minimum = _maximum;
                    if (_value > _maximum)
                        _value = _maximum;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the current value of the progress bar
        /// </summary>
        [Category("Behavior")]
        [Description("The current value of the progress bar")]
        [DefaultValue(0)]
        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (_value < _minimum)
                        _value = _minimum;
                    if (_value > _maximum)
                        _value = _maximum;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets whether to show the percentage text
        /// </summary>
        [Category("Appearance")]
        [Description("Whether to show the percentage text")]
        [DefaultValue(false)]
        public bool ShowPercentage
        {
            get => _showPercentage;
            set
            {
                if (_showPercentage != value)
                {
                    _showPercentage = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets whether to use a gradient for the progress indicator
        /// </summary>
        [Category("Appearance")]
        [Description("Whether to use a gradient for the progress indicator")]
        [DefaultValue(false)]
        public bool UseGradient
        {
            get => _useGradient;
            set
            {
                if (_useGradient != value)
                {
                    _useGradient = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the gradient start color for the progress indicator
        /// </summary>
        [Category("Appearance")]
        [Description("The starting color of the progress indicator gradient")]
        public Color GradientStartColor
        {
            get => _gradientStartColor;
            set
            {
                if (_gradientStartColor != value)
                {
                    _gradientStartColor = value;
                    if (_useGradient) Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the gradient end color for the progress indicator
        /// </summary>
        [Category("Appearance")]
        [Description("The ending color of the progress indicator gradient")]
        public Color GradientEndColor
        {
            get => _gradientEndColor;
            set
            {
                if (_gradientEndColor != value)
                {
                    _gradientEndColor = value;
                    if (_useGradient) Invalidate();
                }
            }
        }
        
        #endregion
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this progress bar based on ProgressBarStyle
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use ThemeManager's current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            
            if (theme == null) return;
            
            // Apply common properties
            Font = theme.BodyFont;
            _cornerRadius = theme.CornerRadius;
            _borderColor = theme.BorderColor;
            _borderWidth = theme.BorderWidth;
            
            // Apply the appropriate style based on ProgressBarStyle
            switch (_progressBarStyle)
            {
                case ThemedProgressBarStyle.Default:
                    _progressColor = theme.PrimaryColor;
                    _backgroundColor = Color.WhiteSmoke;
                    _useGradient = false;
                    break;
                
                case ThemedProgressBarStyle.Primary:
                    _progressColor = theme.PrimaryColor;
                    _backgroundColor = Color.WhiteSmoke;
                    _useGradient = false;
                    break;
                
                case ThemedProgressBarStyle.Secondary:
                    _progressColor = theme.SecondaryColor;
                    _backgroundColor = Color.WhiteSmoke;
                    _useGradient = false;
                    break;
                
                case ThemedProgressBarStyle.Accent:
                    _progressColor = theme.AccentColor;
                    _backgroundColor = Color.WhiteSmoke;
                    _useGradient = false;
                    break;
                
                case ThemedProgressBarStyle.Success:
                    _progressColor = theme.SuccessColor;
                    _backgroundColor = Color.WhiteSmoke;
                    _useGradient = false;
                    break;
                
                case ThemedProgressBarStyle.Warning:
                    _progressColor = theme.WarningColor;
                    _backgroundColor = Color.WhiteSmoke;
                    _useGradient = false;
                    break;
                
                case ThemedProgressBarStyle.Error:
                    _progressColor = theme.ErrorColor;
                    _backgroundColor = Color.WhiteSmoke;
                    _useGradient = false;
                    break;
                
                case ThemedProgressBarStyle.Gradient:
                    _useGradient = true;
                    _gradientStartColor = theme.PrimaryColor;
                    _gradientEndColor = theme.SecondaryColor;
                    _backgroundColor = Color.WhiteSmoke;
                    break;
            }
        }
        
        #endregion
        
        #region Painting
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Enable anti-aliasing for smoother edges
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Calculate progress bar bounds
            Rectangle rect = ClientRectangle;
            
            // Draw background
            using (GraphicsPath path = CreateRoundedRectangle(rect, _cornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(_backgroundColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
                
                // Draw border
                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
            
            // Calculate progress width
            int progressWidth = 0;
            if (_maximum > _minimum)
            {
                progressWidth = (int)((float)(_value - _minimum) / (_maximum - _minimum) * (Width - 2 * _borderWidth));
            }
            
            // Draw progress indicator if there's any progress
            if (progressWidth > 0)
            {
                Rectangle progressRect = new Rectangle(
                    _borderWidth,
                    _borderWidth,
                    progressWidth,
                    Height - 2 * _borderWidth);
                
                using (GraphicsPath path = CreateRoundedRectangle(progressRect, _cornerRadius))
                {
                    if (_useGradient && _gradientStartColor != Color.Empty && _gradientEndColor != Color.Empty)
                    {
                        // Draw gradient progress
                        using (LinearGradientBrush brush = new LinearGradientBrush(
                            progressRect,
                            _gradientStartColor,
                            _gradientEndColor,
                            LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }
                    else
                    {
                        // Draw solid progress
                        Color color = _progressColor != Color.Empty ? _progressColor : SystemColors.Highlight;
                        using (SolidBrush brush = new SolidBrush(color))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }
                }
            }
            
            // Draw percentage text if enabled
            if (_showPercentage)
            {
                int percentage = 0;
                if (_maximum > _minimum)
                {
                    percentage = (int)(100f * (_value - _minimum) / (_maximum - _minimum));
                }
                
                string text = percentage.ToString() + "%";
                
                using (StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    // Draw text with a contrast color
                    using (SolidBrush brush = new SolidBrush(ForeColor))
                    {
                        e.Graphics.DrawString(text, Font, brush, rect, format);
                    }
                }
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
        
        #endregion
    }
    
    /// <summary>
    /// Predefined progress bar styles
    /// </summary>
    public enum ThemedProgressBarStyle
    {
        /// <summary>Default progress bar style</summary>
        Default,
        /// <summary>Progress bar with primary color</summary>
        Primary,
        /// <summary>Progress bar with secondary color</summary>
        Secondary,
        /// <summary>Progress bar with accent color</summary>
        Accent,
        /// <summary>Progress bar indicating success (typically green)</summary>
        Success,
        /// <summary>Progress bar indicating warning (typically yellow/amber)</summary>
        Warning,
        /// <summary>Progress bar indicating error (typically red)</summary>
        Error,
        /// <summary>Progress bar with gradient colors</summary>
        Gradient
    }
}
