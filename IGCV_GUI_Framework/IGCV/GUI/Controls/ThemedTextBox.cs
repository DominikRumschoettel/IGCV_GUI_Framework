using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A text box control that supports themes with rounded corners and other styling options
    /// </summary>
    public class ThemedTextBox : TextBox, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 3;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private Color _focusBorderColor = Color.Empty;
        private bool _showLabel = false;
        private string _labelText = "";
        private Font _labelFont;
        private Color _labelColor = Color.Gray;
        private bool _useIconButton = false;
        private Image _buttonImage;
        private EventHandler _buttonClickHandler;
        
        // For custom border drawing
        private Panel _containerPanel;
        private Button _iconButton;
        
        /// <summary>
        /// Initializes a new instance of the ThemedTextBox control
        /// </summary>
        public ThemedTextBox()
        {
            // Initialize label font
            _labelFont = new Font(Font.FontFamily, Font.Size * 0.8f);
            
            // Create container panel for custom border drawing
            InitializeContainer();
        }
        
        /// <summary>
        /// Dispose of resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _labelFont?.Dispose();
                _containerPanel?.Dispose();
                _iconButton?.Dispose();
            }
            
            base.Dispose(disposing);
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius for the text box
        /// </summary>
        [Category("Appearance")]
        [Description("The radius of the text box corners")]
        [DefaultValue(3)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                if (_cornerRadius != value)
                {
                    _cornerRadius = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the border color for the text box
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the text box border")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the border width for the text box
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the text box border")]
        [DefaultValue(1)]
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                if (_borderWidth != value)
                {
                    _borderWidth = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the border color when the text box has focus
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the text box border when focused")]
        public Color FocusBorderColor
        {
            get => _focusBorderColor;
            set
            {
                if (_focusBorderColor != value)
                {
                    _focusBorderColor = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets whether to show a label above the text box
        /// </summary>
        [Category("Appearance")]
        [Description("Whether to show a label above the text box")]
        [DefaultValue(false)]
        public bool ShowLabel
        {
            get => _showLabel;
            set
            {
                if (_showLabel != value)
                {
                    _showLabel = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the label text
        /// </summary>
        [Category("Appearance")]
        [Description("The text to display in the label")]
        public string LabelText
        {
            get => _labelText;
            set
            {
                if (_labelText != value)
                {
                    _labelText = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the label font
        /// </summary>
        [Category("Appearance")]
        [Description("The font for the label")]
        public Font LabelFont
        {
            get => _labelFont;
            set
            {
                if (_labelFont != value)
                {
                    _labelFont?.Dispose();
                    _labelFont = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the label color
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the label text")]
        public Color LabelColor
        {
            get => _labelColor;
            set
            {
                if (_labelColor != value)
                {
                    _labelColor = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets whether to show an icon button on the right side of the text box
        /// </summary>
        [Category("Appearance")]
        [Description("Whether to show an icon button on the right side of the text box")]
        [DefaultValue(false)]
        public bool UseIconButton
        {
            get => _useIconButton;
            set
            {
                if (_useIconButton != value)
                {
                    _useIconButton = value;
                    UpdateContainer();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the image for the icon button
        /// </summary>
        [Category("Appearance")]
        [Description("The image to display in the icon button")]
        public Image ButtonImage
        {
            get => _buttonImage;
            set
            {
                if (_buttonImage != value)
                {
                    _buttonImage = value;
                    if (_iconButton != null)
                    {
                        _iconButton.Image = value;
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the event handler for the icon button click
        /// </summary>
        [Category("Action")]
        [Description("Event that occurs when the icon button is clicked")]
        public event EventHandler ButtonClick
        {
            add
            {
                _buttonClickHandler += value;
                if (_iconButton != null)
                {
                    _iconButton.Click += value;
                }
            }
            remove
            {
                _buttonClickHandler -= value;
                if (_iconButton != null)
                {
                    _iconButton.Click -= value;
                }
            }
        }
        
        #endregion
        
        #region Container Panel Implementation
        
        /// <summary>
        /// Initializes the container panel for custom border drawing
        /// </summary>
        private void InitializeContainer()
        {
            // Create container panel
            _containerPanel = new Panel
            {
                Size = new Size(Width, Height),
                Location = Location,
                Padding = new Padding(_borderWidth + _cornerRadius),
                BackColor = Color.Transparent
            };
            
            // Set parent and add to parent controls
            if (Parent != null)
            {
                Parent.Controls.Add(_containerPanel);
                Parent.Controls.SetChildIndex(_containerPanel, Parent.Controls.GetChildIndex(this));
            }
            
            // Move the text box to the container
            Parent?.Controls.Remove(this);
            _containerPanel.Controls.Add(this);
            
            // Configure the text box
            BorderStyle = BorderStyle.None;
            Location = new Point(_cornerRadius, _showLabel ? 20 : _cornerRadius);
            Dock = DockStyle.Fill;
            
            // Handle events
            _containerPanel.Paint += ContainerPanel_Paint;
            _containerPanel.Resize += ContainerPanel_Resize;
            _containerPanel.LocationChanged += ContainerPanel_LocationChanged;
            _containerPanel.EnabledChanged += ContainerPanel_EnabledChanged;
            _containerPanel.VisibleChanged += ContainerPanel_VisibleChanged;
            
            // Initialize icon button if needed
            if (_useIconButton)
            {
                CreateIconButton();
            }
        }
        
        /// <summary>
        /// Creates the icon button
        /// </summary>
        private void CreateIconButton()
        {
            if (_iconButton == null)
            {
                _iconButton = new Button
                {
                    Size = new Size(20, 20),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    ForeColor = Color.Gray,
                    Cursor = Cursors.Hand,
                    Image = _buttonImage,
                    Dock = DockStyle.Right,
                    FlatAppearance = { BorderSize = 0 }
                };
                
                // Add event handler
                if (_buttonClickHandler != null)
                {
                    _iconButton.Click += _buttonClickHandler;
                }
                
                _containerPanel.Controls.Add(_iconButton);
                _containerPanel.Controls.SetChildIndex(_iconButton, 0);
            }
        }
        
        /// <summary>
        /// Updates the container panel properties
        /// </summary>
        private void UpdateContainer()
        {
            if (_containerPanel != null)
            {
                // Update padding
                _containerPanel.Padding = new Padding(_borderWidth + (_cornerRadius / 2));
                
                // Update icon button
                if (_useIconButton)
                {
                    if (_iconButton == null)
                    {
                        CreateIconButton();
                    }
                }
                else if (_iconButton != null)
                {
                    _containerPanel.Controls.Remove(_iconButton);
                    _iconButton.Dispose();
                    _iconButton = null;
                }
                
                // Recalculate text box position
                if (_showLabel)
                {
                    this.Location = new Point(_cornerRadius, 20);
                }
                else
                {
                    this.Location = new Point(_cornerRadius, _cornerRadius);
                }
                
                // Force redraw
                _containerPanel.Invalidate();
            }
        }
        
        /// <summary>
        /// Paint event handler for the container panel
        /// </summary>
        private void ContainerPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            Rectangle rect = new Rectangle(
                _borderWidth, 
                _borderWidth, 
                _containerPanel.Width - (_borderWidth * 2), 
                _containerPanel.Height - (_borderWidth * 2));
            
            // Draw background
            using (GraphicsPath path = CreateRoundedRectangle(rect, _cornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
                
                // Draw border
                Color borderColor = (Focused && _focusBorderColor != Color.Empty) ? _focusBorderColor : _borderColor;
                using (Pen pen = new Pen(borderColor, _borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
            
            // Draw label if needed
            if (_showLabel && !string.IsNullOrEmpty(_labelText))
            {
                using (SolidBrush brush = new SolidBrush(_labelColor))
                {
                    e.Graphics.DrawString(_labelText, _labelFont, brush, new Point(5, 0));
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
        
        /// <summary>
        /// Resize event handler for the container panel
        /// </summary>
        private void ContainerPanel_Resize(object sender, EventArgs e)
        {
            // Invalidate to redraw
            _containerPanel.Invalidate();
        }
        
        /// <summary>
        /// Location changed event handler for the container panel
        /// </summary>
        private void ContainerPanel_LocationChanged(object sender, EventArgs e)
        {
            // Update the container location
            if (Parent != null)
            {
                _containerPanel.Location = Location;
            }
        }
        
        /// <summary>
        /// Enabled changed event handler for the container panel
        /// </summary>
        private void ContainerPanel_EnabledChanged(object sender, EventArgs e)
        {
            Enabled = _containerPanel.Enabled;
        }
        
        /// <summary>
        /// Visible changed event handler for the container panel
        /// </summary>
        private void ContainerPanel_VisibleChanged(object sender, EventArgs e)
        {
            Visible = _containerPanel.Visible;
        }
        
        #endregion
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this text box
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use ThemeManager's current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            
            if (theme == null) return;
            
            // Apply theme properties
            Font = theme.BodyFont;
            _labelFont = new Font(theme.BodyFont.FontFamily, theme.BodyFont.Size * 0.8f);
            _cornerRadius = theme.CornerRadius;
            _borderColor = theme.BorderColor;
            _borderWidth = theme.BorderWidth;
            _focusBorderColor = theme.PrimaryColor;
            _labelColor = theme.TextOnLightColor;
            
            // Update the container
            UpdateContainer();
        }
        
        #endregion
        
        #region Size and Location Override
        
        /// <summary>
        /// Overrides the location setting to update the container
        /// </summary>
        public new Point Location
        {
            get => _containerPanel?.Location ?? base.Location;
            set
            {
                if (_containerPanel != null)
                {
                    _containerPanel.Location = value;
                }
                else
                {
                    base.Location = value;
                }
            }
        }
        
        /// <summary>
        /// Overrides the size setting to update the container
        /// </summary>
        public new Size Size
        {
            get => _containerPanel?.Size ?? base.Size;
            set
            {
                if (_containerPanel != null)
                {
                    _containerPanel.Size = value;
                }
                else
                {
                    base.Size = value;
                }
            }
        }
        
        #endregion
    }
}
