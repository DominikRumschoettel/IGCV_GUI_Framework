using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A combo box control that supports themes with custom styling options
    /// </summary>
    public class ThemedComboBox : ComboBox, IThemeableControl
    {
        #region Fields and Constructor
        
        private int _cornerRadius = 3;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private bool _showLabel = false;
        private string _labelText = "";
        private Font _labelFont;
        private Color _labelColor = Color.Gray;
        
        // For custom drawing
        private Panel _containerPanel;
        private TextBox _displayTextBox;
        private Button _dropDownButton;
        
        /// <summary>
        /// Initializes a new instance of the ThemedComboBox control
        /// </summary>
        public ThemedComboBox()
        {
            // Initialize label font
            _labelFont = new Font(Font.FontFamily, Font.Size * 0.8f);
            
            // Create container panel for custom drawing
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
                _displayTextBox?.Dispose();
                _dropDownButton?.Dispose();
            }
            
            base.Dispose(disposing);
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius for the combo box
        /// </summary>
        [Category("Appearance")]
        [Description("The radius of the combo box corners")]
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
        /// Gets or sets the border color for the combo box
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the combo box border")]
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
        /// Gets or sets the border width for the combo box
        /// </summary>
        [Category("Appearance")]
        [Description("The width of the combo box border")]
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
        /// Gets or sets whether to show a label above the combo box
        /// </summary>
        [Category("Appearance")]
        [Description("Whether to show a label above the combo box")]
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
        
        #endregion
        
        #region Container Panel Implementation
        
        /// <summary>
        /// Initializes the container panel for custom drawing
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
            
            // Set parent and add to parent controls if possible
            if (Parent != null)
            {
                Parent.Controls.Add(_containerPanel);
                Parent.Controls.SetChildIndex(_containerPanel, Parent.Controls.GetChildIndex(this));
                Parent.Controls.Remove(this);
            }
            
            // Add the combo box to the container
            _containerPanel.Controls.Add(this);
            
            // Configure the combo box
            FlatStyle = FlatStyle.Flat;
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            BackColor = Color.White;
            
            // Update container positioning
            UpdateContainer();
            
            // Handle events
            _containerPanel.Paint += ContainerPanel_Paint;
            _containerPanel.Resize += ContainerPanel_Resize;
            _containerPanel.LocationChanged += ContainerPanel_LocationChanged;
            this.DrawItem += ThemedComboBox_DrawItem;
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
                
                // Adjust height if showing label
                if (_showLabel)
                {
                    int labelHeight = TextRenderer.MeasureText("Tg", _labelFont).Height;
                    _containerPanel.Height = Height + labelHeight + 5;
                    Location = new Point(_cornerRadius, labelHeight + 5);
                }
                else
                {
                    _containerPanel.Height = Height;
                    Location = new Point(_cornerRadius, _cornerRadius);
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
            
            // Draw label if needed
            if (_showLabel && !string.IsNullOrEmpty(_labelText))
            {
                using (SolidBrush brush = new SolidBrush(_labelColor))
                {
                    e.Graphics.DrawString(_labelText, _labelFont, brush, new Point(5, 0));
                }
            }
            
            // Create rounded rectangle for the combo box
            Rectangle boxRect = new Rectangle(
                _borderWidth,
                _showLabel ? TextRenderer.MeasureText("Tg", _labelFont).Height + 5 : _borderWidth,
                _containerPanel.Width - (_borderWidth * 2),
                Height);
            
            using (GraphicsPath path = CreateRoundedRectangle(boxRect, _cornerRadius))
            {
                // Draw background
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
                
                // Draw border
                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
            
            // Draw dropdown arrow
            int arrowSize = 8;
            int arrowX = boxRect.Right - arrowSize - 8;
            int arrowY = boxRect.Y + (boxRect.Height - arrowSize) / 2;
            
            Point[] arrowPoints = new Point[]
            {
                new Point(arrowX, arrowY),
                new Point(arrowX + arrowSize, arrowY),
                new Point(arrowX + arrowSize / 2, arrowY + arrowSize)
            };
            
            using (SolidBrush arrowBrush = new SolidBrush(_borderColor))
            {
                e.Graphics.FillPolygon(arrowBrush, arrowPoints);
            }
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
        /// Draw item event handler for the combo box
        /// </summary>
        private void ThemedComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            
            // Draw background
            e.DrawBackground();
            
            // Draw selected item highlight
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (SolidBrush brush = new SolidBrush(SystemColors.Highlight))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            
            // Draw item text
            string itemText = Items[e.Index].ToString();
            using (SolidBrush brush = new SolidBrush((e.State & DrawItemState.Selected) == DrawItemState.Selected
                    ? SystemColors.HighlightText : ForeColor))
            {
                e.Graphics.DrawString(itemText, Font, brush,
                    new Rectangle(e.Bounds.X + 3, e.Bounds.Y, e.Bounds.Width - 6, e.Bounds.Height),
                    StringFormat.GenericDefault);
            }
            
            // Draw focus rectangle if needed
            e.DrawFocusRectangle();
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
        
        #region Theming Methods
        
        /// <summary>
        /// Applies the current theme to this combo box
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
            _labelColor = theme.TextOnLightColor;
            ForeColor = theme.TextOnLightColor;
            
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
                    base.Size = new Size(value.Width - (_borderWidth + _cornerRadius) * 2,
                                        value.Height - (_borderWidth + _cornerRadius) * 2);
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
