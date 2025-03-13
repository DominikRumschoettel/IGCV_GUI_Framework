using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV_GUI_Framework.Common;
using IGCV_GUI_Framework.Common.Controls;
using IGCV_GUI_Framework.Interfaces;
using IGCV_GUI_Framework.Pages;

namespace IGCV_GUI_Framework
{
    public class MainForm : Form
    {
        // Window size constants
        private const int WINDOW_WIDTH = 1200;
        private const int WINDOW_HEIGHT = 800;
        // Main UI components
        private ThemedFooterBar _footerBar;
        private StatusPanel _statusPanel;
        private PageContainer _pageContainer;

        // Pages collection
        private List<IModulePage> _pages = new List<IModulePage>();

        // Current active page index
        private int _currentPageIndex = 0;

        public MainForm()
        {
            // Enable double buffering to reduce flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint, true);

            // Initialize theme system
            ThemeManager.SetTheme("Fraunhofer CI");

            // Initialize components
            InitializeComponent();

            // Initialize pages
            InitializePages();
            
            // Apply theme to all controls
            ThemeManager.ApplyThemeToContainer(this);
        }

        private void InitializeComponent()
        {
            // Create status panel
            _statusPanel = new StatusPanel();
            _statusPanel.Dock = DockStyle.Left;
            _statusPanel.Width = 220;
            
            // Create page container
            _pageContainer = new PageContainer();
            _pageContainer.Dock = DockStyle.Fill;
            
            // Top menu removed as requested
            
            // Create footer bar
            _footerBar = new ThemedFooterBar();
            _footerBar.Dock = DockStyle.Bottom;
            _footerBar.Height = 60;
            _footerBar.LogoText = "Fraunhofer";
            _footerBar.SubLogoText = "IGCV";
            _footerBar.NavigationButtonClicked += FooterBar_NavigationButtonClicked;
            
            // Add controls to form
            this.SuspendLayout();
            
            this.Controls.Add(_pageContainer);
            this.Controls.Add(_statusPanel);
            this.Controls.Add(_footerBar);
            
            // Form properties
            this.Size = new Size(WINDOW_WIDTH, WINDOW_HEIGHT);
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "IGCV GUI Framework Demo";
            this.Paint += MainForm_Paint;
            this.Resize += MainForm_Resize;
            
            this.ResumeLayout(false);
        }

        private void InitializePages()
        {
            Bitmap placeholderImage = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(placeholderImage))
            {
                g.Clear(Color.FromArgb(0, 103, 172)); // Fraunhofer blue
            }

            // Create all pages with Controls demos distributed across specialized pages
            _pages.Add(new ButtonControlsPage());       // Button controls demo
            _pages.Add(new InputControlsPage());        // Input controls demo
            _pages.Add(new ProgressControlsPage());     // Progress indicators demo
            _pages.Add(new LayoutControlsPage());       // Layout capabilities demo

            // Set footer navigation buttons
            _footerBar.SetNavigationButtons(_pages.Select(p => p.NavigationName));

            // Start with first page
            ShowPage(0);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // Draw gradient background using the theme system
            ThemeManager.CurrentTheme.ApplyGradientBackground(e, this.ClientRectangle);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Force redraw of gradient
            Invalidate();
        }

        private void FooterBar_NavigationButtonClicked(object sender, int pageIndex)
        {
            ShowPage(pageIndex);
        }
        
        // TopMenu_MenuItemSelected method removed as the top menu has been removed

        /// <summary>
        /// Shows the specified page
        /// </summary>
        private void ShowPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= _pages.Count) return;

            // Update current page index
            _currentPageIndex = pageIndex;

            // Set the page
            _pageContainer.SetPage(_pages[pageIndex]);
            
            // Update navigation indicators
            _footerBar.ActiveButtonIndex = pageIndex;
            
            // Top menu selection update removed
        }

        /// <summary>
        /// Handles form activation to ensure UI is properly initialized
        /// </summary>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            // Simple refresh without excessive redraws
            if (_currentPageIndex >= 0 && _currentPageIndex < _pages.Count)
            {
                _pageContainer.RefreshHeader();
            }
        }
    }
}
