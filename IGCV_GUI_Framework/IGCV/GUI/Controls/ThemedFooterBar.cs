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
    /// A themed footer bar for application navigation and status display
    /// </summary>
    public class ThemedFooterBar : UserControl, IThemeableControl
    {
        #region Fields
        
        private List<Button> _navigationButtons = new List<Button>();
        private Panel _activeIndicator;
        private Label _logoLabel;
        private Label _subLogoLabel;
        private PictureBox _logoImage;
        private Panel _divider;
        
        private int _cornerRadius = 0;
        private Color _borderColor = Color.Silver;
        private int _borderWidth = 0;
        private Color _activeIndicatorColor;
        private int _activeButtonIndex = -1;
        
        /// <summary>
        /// Event fired when a navigation button is clicked
        /// </summary>
        public event EventHandler<int> NavigationButtonClicked;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a new ThemedFooterBar
        /// </summary>
        public ThemedFooterBar()
        {
            InitializeComponent();
            
            // Register for theme changes
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
            
            // Initialize with current theme
            ApplyTheme();
        }
        
        /// <summary>
        /// Initialize the component
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Default properties
            this.Height = 60;
            this.Dock = DockStyle.Bottom;
            this.BackColor = Color.FromArgb(240, 240, 240); // Light gray
            
            // Create active indicator
            _activeIndicator = new Panel
            {
                Size = new Size(0, 3),
                BackColor = ThemeManager.CurrentTheme?.PrimaryColor ?? Color.FromArgb(0, 120, 215),
                Visible = false
            };
            
            // Create logo labels
            _logoLabel = new Label
            {
                Text = "Fraunhofer",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 129, 120),
                Size = new Size(100, 20),
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right
            };
            
            _subLogoLabel = new Label
            {
                Text = "IGCV",
                Font = new Font("Segoe UI", 8f, FontStyle.Regular),
                ForeColor = Color.Gray,
                Size = new Size(100, 16),
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right
            };
            
            // Create logo image placeholder (can be customized by application)
            _logoImage = new PictureBox
            {
                Size = new Size(32, 32),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Right
            };
            
            // Create divider
            _divider = new Panel
            {
                Height = 1,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(210, 210, 210)
            };
            
            // Add controls
            this.Controls.Add(_logoLabel);
            this.Controls.Add(_subLogoLabel);
            this.Controls.Add(_logoImage);
            this.Controls.Add(_activeIndicator);
            this.Controls.Add(_divider);
            
            // Event handlers
            this.Resize += ThemedFooterBar_Resize;
            
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
        /// Gets or sets the corner radius of the footer bar
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
        /// Gets or sets the border color of the footer bar
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
        /// Gets or sets the border width of the footer bar
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
        /// Gets or sets the active button index
        /// </summary>
        public int ActiveButtonIndex
        {
            get => _activeButtonIndex;
            set
            {
                if (_activeButtonIndex != value && value >= -1 && value < _navigationButtons.Count)
                {
                    _activeButtonIndex = value;
                    UpdateActiveButton();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the logo text
        /// </summary>
        public string LogoText
        {
            get => _logoLabel.Text;
            set => _logoLabel.Text = value;
        }
        
        /// <summary>
        /// Gets or sets the sub-logo text
        /// </summary>
        public string SubLogoText
        {
            get => _subLogoLabel.Text;
            set => _subLogoLabel.Text = value;
        }
        
        /// <summary>
        /// Gets or sets the logo image
        /// </summary>
        public Image LogoImage
        {
            get => _logoImage.Image;
            set => _logoImage.Image = value;
        }
        
        #endregion
        
        #region Public Methods
        
        /// <summary>
        /// Sets the navigation buttons
        /// </summary>
        public void SetNavigationButtons(IEnumerable<string> buttonTexts)
        {
            // Clear existing buttons
            foreach (var button in _navigationButtons)
            {
                this.Controls.Remove(button);
                button.Dispose();
            }
            _navigationButtons.Clear();
            
            // Create new buttons
            if (buttonTexts != null)
            {
                int index = 0;
                foreach (var text in buttonTexts)
                {
                    var button = CreateNavigationButton(text, index);
                    _navigationButtons.Add(button);
                    this.Controls.Add(button);
                    index++;
                }
            }
            
            // Update layout
            RecalculateLayout();
            
            // Update active button
            if (_activeButtonIndex >= _navigationButtons.Count)
            {
                _activeButtonIndex = _navigationButtons.Count > 0 ? 0 : -1;
            }
            UpdateActiveButton();
        }
        
        /// <summary>
        /// Apply theme to the footer bar
        /// </summary>
        public void ApplyTheme(ITheme theme = null)
        {
            // Use current theme if none specified
            theme = theme ?? ThemeManager.CurrentTheme;
            if (theme == null) return;
            
            // Update active indicator color
            _activeIndicatorColor = theme.PrimaryColor;
            _activeIndicator.BackColor = _activeIndicatorColor;
            
            // Update divider
            _divider.BackColor = theme.BorderColor;
            
            // Update navigation buttons
            for (int i = 0; i < _navigationButtons.Count; i++)
            {
                bool isActive = i == _activeButtonIndex;
                UpdateButtonAppearance(_navigationButtons[i], theme, isActive);
            }
            
            // Update indicator visibility
            UpdateActiveButton();
            
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
        /// Create a navigation button
        /// </summary>
        private Button CreateNavigationButton(string text, int index)
        {
            Button button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(60, 60, 60),
                BackColor = Color.Transparent,
                Font = ThemeManager.CurrentTheme?.ButtonFont ?? new Font("Segoe UI", 10),
                FlatAppearance = { BorderSize = 0 },
                Tag = index,
                Cursor = Cursors.Hand
            };
            
            // Add click handler
            button.Click += NavigationButton_Click;
            
            return button;
        }
        
        /// <summary>
        /// Click handler for navigation buttons
        /// </summary>
        private void NavigationButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int index)
            {
                // Update active button
                _activeButtonIndex = index;
                UpdateActiveButton();
                
                // Raise event
                NavigationButtonClicked?.Invoke(this, index);
            }
        }
        
        /// <summary>
        /// Update the appearance of the active button and indicator
        /// </summary>
        private void UpdateActiveButton()
        {
            // Update button styles
            for (int i = 0; i < _navigationButtons.Count; i++)
            {
                bool isActive = i == _activeButtonIndex;
                UpdateButtonAppearance(_navigationButtons[i], ThemeManager.CurrentTheme, isActive);
            }
            
            // Update indicator
            if (_activeButtonIndex >= 0 && _activeButtonIndex < _navigationButtons.Count)
            {
                Button activeButton = _navigationButtons[_activeButtonIndex];
                
                // Position the indicator under the active button
                _activeIndicator.Width = activeButton.Width;
                _activeIndicator.Left = activeButton.Left;
                _activeIndicator.Top = 0; // At the top of the footer bar
                _activeIndicator.Visible = true;
                _activeIndicator.BringToFront();
            }
            else
            {
                _activeIndicator.Visible = false;
            }
        }
        
        /// <summary>
        /// Update a button's appearance based on its active state
        /// </summary>
        private void UpdateButtonAppearance(Button button, ITheme theme, bool isActive)
        {
            if (button == null || theme == null) return;
            
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            
            if (isActive)
            {
                // Active button style
                button.BackColor = Color.FromArgb(220, 230, 240);
                button.ForeColor = theme.TextOnLightColor;
                button.Font = new Font(theme.ButtonFont, FontStyle.Bold);
            }
            else
            {
                // Inactive button style
                button.BackColor = Color.Transparent;
                button.ForeColor = theme.TextOnLightColor;
                button.Font = theme.ButtonFont;
            }
            
            // Mouse hover effects
            button.FlatAppearance.MouseOverBackColor = isActive
                ? button.BackColor
                : Color.FromArgb(230, 240, 250);
        }
        
        /// <summary>
        /// Resize event handler
        /// </summary>
        private void ThemedFooterBar_Resize(object sender, EventArgs e)
        {
            RecalculateLayout();
        }
        
        /// <summary>
        /// Recalculate the layout of controls
        /// </summary>
        private void RecalculateLayout()
        {
            if (this.Width <= 0) return;
            
            // Position logo elements in the right corner
            int logoAreaWidth = 150;
            int rightMargin = 20;
            
            _logoImage.Location = new Point(this.Width - logoAreaWidth - rightMargin, (this.Height - _logoImage.Height) / 2);
            _logoLabel.Location = new Point(this.Width - logoAreaWidth - rightMargin - 15, 15);
            _subLogoLabel.Location = new Point(this.Width - logoAreaWidth - rightMargin - 15, 35);
            
            // Calculate available space for navigation buttons
            int availableWidth = this.Width - logoAreaWidth - 40; // Allow margin on left
            int buttonCount = _navigationButtons.Count;
            
            if (buttonCount > 0)
            {
                // Calculate button width and spacing
                int buttonWidth = Math.Min(150, availableWidth / buttonCount); // Max width of 150px
                int spacing = 10; // Space between buttons
                int totalButtonWidth = (buttonWidth * buttonCount) + (spacing * (buttonCount - 1));
                int startX = 20; // Left margin
                
                // Position buttons
                for (int i = 0; i < _navigationButtons.Count; i++)
                {
                    Button button = _navigationButtons[i];
                    button.Location = new Point(startX + (i * (buttonWidth + spacing)), (this.Height - button.Height) / 2);
                    button.Size = new Size(buttonWidth, 30);
                }
                
                // Update active button indicator
                UpdateActiveButton();
            }
        }
        
        #endregion
    }
}
