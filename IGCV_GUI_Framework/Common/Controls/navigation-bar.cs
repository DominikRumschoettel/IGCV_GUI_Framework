using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Common.Controls
{
    /// <summary>
    /// Navigation bar control for the bottom of the application
    /// </summary>
    public class NavigationBar : UserControl
    {
        private const int BUTTON_HEIGHT = 40;
        private const int INDICATOR_WIDTH = 5;
        private const int NAV_HEIGHT = 80;
        
        // Event raised when a page is selected
        public event EventHandler<int> PageSelected;
        
        // Current active page index
        private int _activePageIndex = 0;
        
        // Collection of buttons
        private List<Button> _navButtons = new List<Button>();
        
        // Status indicator panel
        private Panel _statusLight;
        
        // Green indicator for active button
        private Panel _activeIndicator;
        
        // Fraunhofer logo and labels
        private Label _logoLabel;
        private Label _igcvLabel;
        
        // Constructor
        public NavigationBar()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Set control properties
            this.Dock = DockStyle.Bottom;
            this.Height = NAV_HEIGHT;
            this.BackColor = FraunhoferTheme.FooterBackground;
            
            // Create status light
            _statusLight = new Panel
            {
                Size = new Size(30, 30),
                Location = new Point(this.Width - 120, 25),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Right
            };
            _statusLight.Paint += (s, e) => {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (SolidBrush brush = new SolidBrush(_activePageIndex >= 0 ? Color.LimeGreen : Color.Red))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, 30, 30);
                }
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    e.Graphics.DrawEllipse(pen, 0, 0, 30, 30);
                }
            };
            
            // Create Fraunhofer logo
            _logoLabel = new Label
            {
                Text = "Fraunhofer",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 129, 120),
                Size = new Size(80, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Right
            };

            _igcvLabel = new Label
            {
                Text = "IGCV",
                Font = new Font("Segoe UI", 8f, FontStyle.Regular),
                ForeColor = Color.Gray,
                Size = new Size(50, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Right
            };
            
            // Create active indicator
            _activeIndicator = new Panel
            {
                BackColor = FraunhoferTheme.Green,
                Size = new Size(INDICATOR_WIDTH, BUTTON_HEIGHT),
                Location = new Point(0, 20),
            };
            
            // Add controls
            this.Controls.Add(_statusLight);
            this.Controls.Add(_logoLabel);
            this.Controls.Add(_igcvLabel);
            this.Controls.Add(_activeIndicator);
            
            // Event handlers
            this.Resize += NavigationBar_Resize;
            
            this.ResumeLayout(false);
            
            // Initial positioning
            UpdateControlPositions();
        }
        
        private void NavigationBar_Resize(object sender, EventArgs e)
        {
            // Update control positions when resized
            UpdateControlPositions();
            
            // Update buttons layout
            RecalculateButtonPositions();
            
            // Update active indicator position
            UpdateActiveIndicator();
        }
        
        private void UpdateControlPositions()
        {
            // Position status indicator and logo on resize
            _statusLight.Location = new Point(this.Width - 120, 25);
            _logoLabel.Location = new Point(this.Width - 90, 20);
            _igcvLabel.Location = new Point(this.Width - 90, 40);
        }
        
        private void RecalculateButtonPositions()
        {
            // Adjust button positions based on current width
            int buttonCount = _navButtons.Count;
            if (buttonCount == 0) return;
            
            // Calculate max available width (allowing for status light and logo)
            int availableWidth = this.Width - 200; // Reserve space for Fraunhofer logo
            
            // Calculate button width and spacing
            int totalButtons = buttonCount;
            int buttonWidth = Math.Min(135, availableWidth / totalButtons);
            int spacing = 1; // Minimal spacing
            
            // Position buttons evenly
            for (int i = 0; i < buttonCount; i++)
            {
                Button button = _navButtons[i];
                button.Size = new Size(buttonWidth, BUTTON_HEIGHT);
                button.Location = new Point(i * (buttonWidth + spacing), 20);
            }
        }
        
        /// <summary>
        /// Sets the navigation pages
        /// </summary>
        public void SetPages(IEnumerable<IModulePage> pages)
        {
            // Remove existing nav buttons
            foreach (var button in _navButtons)
            {
                this.Controls.Remove(button);
            }
            _navButtons.Clear();
            
            // Sort pages by order
            var orderedPages = pages.OrderBy(p => p.Order).ToList();
            
            // Create buttons for each page
            for (int i = 0; i < orderedPages.Count; i++)
            {
                var page = orderedPages[i];
                int pageIndex = i; // Index in the pages collection
                
                Button button = new Button
                {
                    Text = page.NavigationName,
                    TabIndex = i + 1,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                
                FraunhoferTheme.StyleSecondaryButton(button);
                
                // Add click handler
                button.Click += (s, e) => OnPageSelected(pageIndex);
                
                _navButtons.Add(button);
                this.Controls.Add(button);
            }
            
            // Update button positions
            RecalculateButtonPositions();
            
            // Update UI
            UpdateActiveIndicator();
        }
        
        /// <summary>
        /// Sets the connection status for the status light
        /// </summary>
        public void SetConnectionStatus(bool isConnected)
        {
            _statusLight.Invalidate(); // Force redraw with new status
        }
        
        /// <summary>
        /// Sets the active page
        /// </summary>
        public void SetActivePage(int pageIndex)
        {
            _activePageIndex = pageIndex;
            UpdateActiveIndicator();
        }
        
        private void UpdateActiveIndicator()
        {
            if (_activePageIndex >= 0 && _activePageIndex < _navButtons.Count)
            {
                // Position the indicator next to the active button
                var activeButton = _navButtons[_activePageIndex];
                _activeIndicator.Location = new Point(activeButton.Left - INDICATOR_WIDTH, 20);
                _activeIndicator.BringToFront();
            }
        }
        
        private void OnPageSelected(int pageIndex)
        {
            _activePageIndex = pageIndex;
            UpdateActiveIndicator();
            PageSelected?.Invoke(this, pageIndex);
        }
    }
}
