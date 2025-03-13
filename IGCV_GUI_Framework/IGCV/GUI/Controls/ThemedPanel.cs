using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A panel control that supports themes with rounded corners and other styling options
    /// </summary>
    public class ThemedPanel : Panel, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 3;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private PanelStyle _panelStyle = PanelStyle.Default;
        private bool _useGradient = false;
        private Color _gradientStartColor = Color.Empty;
        private Color _gradientEndColor = Color.Empty;
        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;
        
        /// <summary>
        /// Initializes a new instance of the ThemedPanel control
        /// </summary>
        public ThemedPanel()
        {
            // Enable custom painting
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            
            // Transparent background by default
            BackColor = Color.Transparent;
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius for the panel
        /// </summary>
        [Category("Appearance")]
        [Description("The radius of the panel corners")]
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
        /// Gets or sets the border color for the panel
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the panel border")]
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
        /// Gets or sets the border width for the panel
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the panel border")]
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
        /// Gets or sets the panel style
        /// </summary>
        [Category("Appearance")]
        [Description("The predefined style for the panel")]
        [DefaultValue(PanelStyle.Default)]
        public PanelStyle PanelStyle
        {
            get => _panelStyle;
            set
            {
                if (_panelStyle != value)
                {
                    _panelStyle = value;
                    ApplyTheme();
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets whether to use a gradient background
        /// </summary>
        [Category("Appearance")]
        [Description("Whether to use a gradient background")]
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
        /// Gets or sets the gradient start color
        /// </summary>
        [Category("Appearance")]
        [Description("The starting color of the gradient")]
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
        /// Gets or sets the gradient end color
        /// </summary>
        [Category("Appearance")]
        [Description("The ending color of the gradient")]
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
        
        /// <summary>
        /// Gets or sets the gradient mode
        /// </summary>
        [Category("Appearance")]
        [Description("The direction of the gradient")]
        [DefaultValue(LinearGradientMode.Vertical)]
        public LinearGradientMode GradientMode
        {
            get => _gradientMode;
            set
            {
                if (_gradientMode != value)
                {
                    _gradientMode = value;
                    if (_useGradient) Invalidate();
                }
            }
        }
        
        #endregion
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this panel based on PanelStyle
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use ThemeManager's current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            
            if (theme == null) return;
            
            // Apply the appropriate style based on PanelStyle
            switch (_panelStyle)
            {
                case PanelStyle.Default:
                    theme.ApplyPanelStyle(this);
                    break;
                    
                case PanelStyle.Primary:
                    BackColor = theme.PrimaryColor;
                    ForeColor = theme.TextOnDarkColor;
                    _borderWidth = 0;
                    break;
                    
                case PanelStyle.Secondary:
                    BackColor = theme.SecondaryColor;
                    ForeColor = theme.TextOnDarkColor;
                    _borderWidth = 0;
                    break;
                    
                case PanelStyle.Accent:
                    BackColor = theme.AccentColor;
                    ForeColor = theme.TextOnDarkColor;
                    _borderWidth = 0;
                    break;
                    
                case PanelStyle.Light:
                    BackColor = Color.White;
                    ForeColor = theme.TextOnLightColor;
                    _borderColor = theme.BorderColor;
                    _borderWidth = theme.BorderWidth;
                    break;
                    
                case PanelStyle.Gradient:
                    _useGradient = true;
                    _gradientStartColor = theme.PrimaryColor;
                    _gradientEndColor = theme.SecondaryColor;
                    _gradientMode = LinearGradientMode.ForwardDiagonal;
                    ForeColor = theme.TextOnDarkColor;
                    _borderWidth = 0;
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
            
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            
            // Create rounded rectangle path
            GraphicsPath path = CreateRoundedRectangle(rect, _cornerRadius);
            
            // Fill background
            if (_useGradient && _gradientStartColor != Color.Empty && _gradientEndColor != Color.Empty)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    _gradientStartColor,
                    _gradientEndColor,
                    _gradientMode))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
            
            // Draw border if needed
            if (_borderWidth > 0)
            {
                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
            
            // Clean up
            path.Dispose();
        }
        
        /// <summary>
        /// Creates a rounded rectangle graphics path
        /// </summary>
        private GraphicsPath CreateRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            
            if (radius > 0)
            {
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
        /// Sets the region of the control to ensure that any transparent parts don't receive mouse events
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            if (_cornerRadius > 0)
            {
                using (GraphicsPath path = CreateRoundedRectangle(ClientRectangle, _cornerRadius))
                {
                    Region = new Region(path);
                }
            }
            else
            {
                Region = new Region(ClientRectangle);
            }
        }
        
        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate(); // Ensure proper redraw when parent color changes
        }
        
        #endregion
    }
    
    /// <summary>
    /// Predefined panel styles
    /// </summary>
    public enum PanelStyle
    {
        /// <summary>Default panel style from the theme</summary>
        Default,
        /// <summary>Primary color panel</summary>
        Primary,
        /// <summary>Secondary color panel</summary>
        Secondary,
        /// <summary>Accent color panel</summary>
        Accent,
        /// <summary>Light color panel with border</summary>
        Light,
        /// <summary>Gradient background panel</summary>
        Gradient
    }
}
