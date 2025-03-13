using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IGCV.GUI.Themes;

namespace IGCV.GUI.Controls
{
    /// <summary>
    /// A themed navigation menu control for application navigation
    /// </summary>
    public class ThemedNavigationMenu : UserControl, IThemeableControl
    {
        #region Fields
        
        private List<Button> _menuButtons = new List<Button>();
        private Panel _activeIndicator;
        private int _cornerRadius = 0;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 0;
        private int _buttonHeight = 40;
        private int _buttonSpacing = 1;
        private int _activeButtonIndex = -1;
        private Color _menuBackColor = Color.FromArgb(235, 235, 235);
        private MenuStyle _menuStyle = MenuStyle.Horizontal;
        
        /// <summary>
        /// Event fired when a menu item is selected
        /// </summary>
        public event EventHandler<int> MenuItemSelected;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a new ThemedNavigationMenu
        /// </summary>
        public ThemedNavigationMenu()
        {
            InitializeComponent();
            
            // Register for theme changes
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }
        
        /// <summary>
        /// Initialize the control
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Default properties
            this.Size = new Size(600, _buttonHeight);
            this.BackColor = _menuBackColor;
            
            // Create active indicator
            _activeIndicator = new Panel
            {
                Size = new Size(5, _buttonHeight),
                BackColor = ThemeManager.CurrentTheme?.PrimaryColor ?? Color.FromArgb(0, 120, 215),
                Visible = false
            };
            this.Controls.Add(_activeIndicator);
            
            // Event handlers
            this.Resize += ThemedNavigationMenu_Resize;
            
            this.ResumeLayout(false);
        }
        
        /// <summary>
        /// Clean up resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;
            }
            base.Dispose(disposing);
        }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets or sets the corner radius of the menu
        /// </summary>
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
        /// Gets or sets the border color of the menu
        /// </summary>
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
        /// Gets or sets the border width of the menu
        /// </summary>
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
        /// Gets or sets the height of menu buttons
        /// </summary>
        public int ButtonHeight
        {
            get => _buttonHeight;
            set
            {
                if (_buttonHeight != value && value > 0)
                {
                    _buttonHeight = value;
                    RecalculateLayout();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the spacing between menu buttons
        /// </summary>
        public int ButtonSpacing
        {
            get => _buttonSpacing;
            set
            {
                if (_buttonSpacing != value)
                {
                    _buttonSpacing = value;
                    RecalculateLayout();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the background color of the menu
        /// </summary>
        public Color MenuBackColor
        {
            get => _menuBackColor;
            set
            {
                if (_menuBackColor != value)
                {
                    _menuBackColor = value;
                    this.BackColor = value;
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the menu style (orientation)
        /// </summary>
        public MenuStyle MenuStyle
        {
            get => _menuStyle;
            set
            {
                if (_menuStyle != value)
                {
                    _menuStyle = value;
                    RecalculateLayout();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the active button index
        /// </summary>
        public int ActiveButtonIndex
        {
            get => _activeButtonIndex;
            set
            {
                if (_activeButtonIndex != value && value >= -1 && value < _menuButtons.Count)
                {
                    _activeButtonIndex = value;
                    UpdateActiveButton();
                }
            }
        }
        
        #endregion
        
        #region Public Methods
        
        /// <summary>
        /// Sets the menu items
        /// </summary>
        /// <param name="items">The menu item texts</param>
        public void SetMenuItems(IEnumerable<string> items)
        {
            // Clear existing buttons
            foreach (var button in _menuButtons)
            {
                this.Controls.Remove(button);
                button.Dispose();
            }
            _menuButtons.Clear();
            
            // Create new buttons
            if (items != null)
            {
                int index = 0;
                foreach (var item in items)
                {
                    Button button = CreateMenuButton(item, index);
                    _menuButtons.Add(button);
                    this.Controls.Add(button);
                    index++;
                }
            }
            
            // Recalculate layout
            RecalculateLayout();
            
            // Update active button
            if (_activeButtonIndex >= _menuButtons.Count)
            {
                _activeButtonIndex = _menuButtons.Count > 0 ? 0 : -1;
            }
            UpdateActiveButton();
        }
        
        /// <summary>
        /// Apply theme to the menu and its buttons
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            if (theme == null) return;
            
            // Update active indicator
            _activeIndicator.BackColor = theme.PrimaryColor;
            
            // Update buttons
            foreach (var button in _menuButtons)
            {
                bool isActive = _menuButtons.IndexOf(button) == _activeButtonIndex;
                ApplyThemeToButton(button, theme, isActive);
            }
            
            // Update menu properties
            _borderColor = theme.BorderColor;
            
            // Invalidate to trigger repaint
            Invalidate();
        }
        
        #endregion
        
        #region Private Methods
        
        /// <summary>
        /// Theme changed event handler
        /// </summary>
        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            ApplyTheme();
        }
        
        /// <summary>
        /// Creates a menu button with the given text and index
        /// </summary>
        private Button CreateMenuButton(string text, int index)
        {
            Button button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = index,
                Cursor = Cursors.Hand
            };
            
            // Apply theme to the button
            ApplyThemeToButton(button, ThemeManager.CurrentTheme, index == _activeButtonIndex);
            
            // Add click handler
            button.Click += MenuButton_Click;
            
            return button;
        }
        
        /// <summary>
        /// Menu button click handler
        /// </summary>
        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int index)
            {
                // Update active button
                _activeButtonIndex = index;
                UpdateActiveButton();
                
                // Raise event
                MenuItemSelected?.Invoke(this, index);
            }
        }
        
        /// <summary>
        /// Update UI for active button
        /// </summary>
        private void UpdateActiveButton()
        {
            // Update buttons
            for (int i = 0; i < _menuButtons.Count; i++)
            {
                bool isActive = i == _activeButtonIndex;
                ApplyThemeToButton(_menuButtons[i], ThemeManager.CurrentTheme, isActive);
            }
            
            // Update active indicator
            if (_activeButtonIndex >= 0 && _activeButtonIndex < _menuButtons.Count)
            {
                Button activeButton = _menuButtons[_activeButtonIndex];
                
                // Position the indicator based on menu style
                if (_menuStyle == MenuStyle.Horizontal)
                {
                    _activeIndicator.Size = new Size(activeButton.Width, 3);
                    _activeIndicator.Location = new Point(activeButton.Left, this.Height - 3);
                    _activeIndicator.BringToFront();
                }
                else
                {
                    _activeIndicator.Size = new Size(3, activeButton.Height);
                    _activeIndicator.Location = new Point(0, activeButton.Top);
                    _activeIndicator.BringToFront();
                }
                
                _activeIndicator.Visible = true;
            }
            else
            {
                _activeIndicator.Visible = false;
            }
        }
        
        /// <summary>
        /// Apply theme to a menu button
        /// </summary>
        private void ApplyThemeToButton(Button button, ITheme theme, bool isActive)
        {
            if (button == null || theme == null) return;
            
            if (isActive)
            {
                // Active button style
                button.Font = new Font(theme.ButtonFont, FontStyle.Bold);
                button.ForeColor = theme.TextOnDarkColor;
                button.BackColor = Color.FromArgb(0, 103, 172); // Fraunhofer blue
                button.FlatAppearance.BorderSize = 0;
            }
            else
            {
                // Inactive button style
                button.Font = theme.ButtonFont;
                button.ForeColor = theme.TextOnLightColor;
                button.BackColor = _menuBackColor;
                button.FlatAppearance.BorderSize = 0;
            }
            
            // Add mouse hover effects
            button.FlatAppearance.MouseOverBackColor = isActive
                ? ControlPaint.Light(button.BackColor)
                : Color.FromArgb(220, 230, 240);
                
            button.FlatAppearance.MouseDownBackColor = isActive
                ? button.BackColor
                : Color.FromArgb(200, 220, 230);
        }
        
        /// <summary>
        /// Resize event handler
        /// </summary>
        private void ThemedNavigationMenu_Resize(object sender, EventArgs e)
        {
            RecalculateLayout();
        }
        
        /// <summary>
        /// Calculate button positions and sizes
        /// </summary>
        private void RecalculateLayout()
        {
            if (_menuButtons.Count == 0) return;
            
            if (_menuStyle == MenuStyle.Horizontal)
            {
                // Set control height
                this.Height = _buttonHeight;
                
                // Calculate button width
                int availableWidth = this.Width;
                int buttonWidth = (availableWidth - (_buttonSpacing * (_menuButtons.Count - 1))) / _menuButtons.Count;
                
                // Position buttons
                int x = 0;
                foreach (var button in _menuButtons)
                {
                    button.Location = new Point(x, 0);
                    button.Size = new Size(buttonWidth, _buttonHeight);
                    x += buttonWidth + _buttonSpacing;
                }
            }
            else // Vertical
            {
                // Set control width based on widest button text
                int maxTextWidth = _menuButtons.Max(b => TextRenderer.MeasureText(b.Text, b.Font).Width);
                int buttonWidth = Math.Max(100, maxTextWidth + 20); // Add padding
                this.Width = buttonWidth;
                
                // Position buttons
                int y = 0;
                foreach (var button in _menuButtons)
                {
                    button.Location = new Point(0, y);
                    button.Size = new Size(buttonWidth, _buttonHeight);
                    y += _buttonHeight + _buttonSpacing;
                }
            }
            
            // Update active button indicator
            UpdateActiveButton();
        }
        
        /// <summary>
        /// Custom painting for border and background
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw border if needed
            if (_borderWidth > 0 && _borderColor != Color.Transparent)
            {
                if (_cornerRadius > 0)
                {
                    // Draw rounded border
                    using (GraphicsPath path = CreateRoundedRectangle(
                        new Rectangle(0, 0, Width - 1, Height - 1), _cornerRadius))
                    {
                        using (Pen pen = new Pen(_borderColor, _borderWidth))
                        {
                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                }
                else
                {
                    // Draw straight border
                    using (Pen pen = new Pen(_borderColor, _borderWidth))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                    }
                }
            }
        }
        
        /// <summary>
        /// Creates a rounded rectangle path
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
    /// Defines the orientation style of the navigation menu
    /// </summary>
    public enum MenuStyle
    {
        /// <summary>
        /// Horizontal menu with buttons in a row
        /// </summary>
        Horizontal,
        
        /// <summary>
        /// Vertical menu with buttons in a column
        /// </summary>
        Vertical
    }
}
