using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A button control that supports themes with rounded corners and other styling options
    /// </summary>
    public class ThemedButton : Button, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 3;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private ButtonStyle _buttonStyle = ButtonStyle.Primary;
        
        // To keep track of mouse states
        private bool _isHovering = false;
        private bool _isPressed = false;
        
        /// <summary>
        /// Initializes a new instance of the ThemedButton control
        /// </summary>
        public ThemedButton()
        {
            // Enable custom painting
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            
            // Set base button properties
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            
            // Set the default size
            Size = new Size(120, 40);
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius for the button
        /// </summary>
        [Category("Appearance")]
        [Description("The radius of the button corners")]
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
        /// Gets or sets the border color for the button
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the button border")]
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
        /// Gets or sets the border width for the button
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the button border")]
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
        /// Gets or sets the button style
        /// </summary>
        [Category("Appearance")]
        [Description("The predefined style for the button")]
        [DefaultValue(ButtonStyle.Primary)]
        public ButtonStyle ButtonStyle
        {
            get => _buttonStyle;
            set
            {
                if (_buttonStyle != value)
                {
                    _buttonStyle = value;
                    ApplyTheme();
                    Invalidate();
                }
            }
        }
        
        #endregion
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this button based on ButtonStyle
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use ThemeManager's current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            
            if (theme == null) return;
            
            // Apply the appropriate style based on ButtonStyle
            switch (_buttonStyle)
            {
                case ButtonStyle.Primary:
                    theme.ApplyPrimaryButtonStyle(this);
                    break;
                case ButtonStyle.Secondary:
                    theme.ApplySecondaryButtonStyle(this);
                    break;
                case ButtonStyle.Tertiary:
                    theme.ApplyTertiaryButtonStyle(this);
                    break;
            }
        }
        
        #endregion
        
        #region Mouse Events
        
        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovering = true;
            Invalidate();
            base.OnMouseEnter(e);
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovering = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
        
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            _isPressed = true;
            Invalidate();
            base.OnMouseDown(mevent);
        }
        
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            _isPressed = false;
            Invalidate();
            base.OnMouseUp(mevent);
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
            using (SolidBrush brush = new SolidBrush(GetBackgroundColor()))
            {
                e.Graphics.FillPath(brush, path);
            }
            
            // Draw border if needed
            if (_borderWidth > 0)
            {
                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
            
            // Draw text
            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                ClientRectangle,
                ForeColor,
                GetTextFormatFlags());
            
            // Draw focus rectangle if the button has focus
            if (Focused)
            {
                rect.Inflate(-4, -4);
                ControlPaint.DrawFocusRectangle(e.Graphics, rect);
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
        /// Gets the background color based on the current state (normal, hover, pressed)
        /// </summary>
        private Color GetBackgroundColor()
        {
            if (!Enabled)
            {
                // For disabled state
                return ControlPaint.LightLight(BackColor);
            }
            
            if (_isPressed)
            {
                // For pressed state
                return ControlPaint.Dark(BackColor);
            }
            
            if (_isHovering)
            {
                // For hover state
                return ControlPaint.Light(BackColor);
            }
            
            // Normal state
            return BackColor;
        }
        
        /// <summary>
        /// Gets the appropriate text format flags
        /// </summary>
        private TextFormatFlags GetTextFormatFlags()
        {
            return TextFormatFlags.HorizontalCenter | 
                   TextFormatFlags.VerticalCenter | 
                   TextFormatFlags.WordBreak;
        }
        
        #endregion
    }
    
    /// <summary>
    /// Predefined button styles
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>Primary action button</summary>
        Primary,
        /// <summary>Secondary action button</summary>
        Secondary,
        /// <summary>Tertiary/text button</summary>
        Tertiary
    }
}
