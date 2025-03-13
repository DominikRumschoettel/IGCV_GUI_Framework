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
        // Main UI components
        private ThemedFooterBar _footerBar;
        private StatusPanel _statusPanel;
        private PageContainer _pageContainer;
        private ThemedNavigationMenu _topMenu;

        // Pages collection
        private List<IModulePage> _pages = new List<IModulePage>();

        // Printer controller
        private IPrinterController _printerController;

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
            
            // Create top menu
            _topMenu = new ThemedNavigationMenu();
            _topMenu.Height = 40;
            _topMenu.Dock = DockStyle.Top;
            _topMenu.MenuBackColor = Color.FromArgb(235, 235, 235);
            _topMenu.BorderWidth = 0;
            _topMenu.MenuItemSelected += TopMenu_MenuItemSelected;
            
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
            this.Controls.Add(_topMenu);
            this.Controls.Add(_footerBar);
            
            // Form properties
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "IGCV GUI Framework Demo";
            this.Paint += MainForm_Paint;
            this.Resize += MainForm_Resize;
            
            this.ResumeLayout(false);
        }

        private void InitializePages()
        {
            // Create printer controller
            _printerController = new SamplePrinterController();

            // Set printer controller to status panel
            _statusPanel.SetPrinterController(_printerController);

            Bitmap placeholderImage = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(placeholderImage))
            {
                g.Clear(Color.FromArgb(0, 103, 172)); // Fraunhofer blue
            }

            // Create all pages including main menu
            _pages.Add(new MainMenuPage());  // First page is main menu
            _pages.Add(new AxesPage(_printerController));
            _pages.Add(new SamplePage("Actuators", "Manipulate actuators and send basic commands", "Actuators",
                                    placeholderImage, 2));
            _pages.Add(new SamplePage("Test", "Automation via Python Code", "Test",
                                    placeholderImage, 3));
            _pages.Add(new SamplePage("Vision", "Get sensor data and camera controls", "Vision",
                                    placeholderImage, 4));

            // Set navigation menu items
            _topMenu.SetMenuItems(new[] { "Demo Launcher", "Controls Demo", "Integration Demo" });
            
            // Set footer navigation buttons
            _footerBar.SetNavigationButtons(_pages.Select(p => p.NavigationName));

            // Start with main menu
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
        
        private void TopMenu_MenuItemSelected(object sender, int index)
        {
            // Handle top menu item selections
            switch (index)
            {
                case 0: // Demo Launcher
                    ShowPage(0); // Main menu/demo launcher
                    break;
                case 1: // Controls Demo
                    ShowPage(1); // Axes page as control demo
                    break;
                case 2: // Integration Demo
                    ShowPage(2); // Actuators page as integration demo
                    break;
            }
        }

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
            
            // Update top menu selection based on page
            if (pageIndex == 0) _topMenu.ActiveButtonIndex = 0;
            else if (pageIndex == 1) _topMenu.ActiveButtonIndex = 1;
            else if (pageIndex >= 2) _topMenu.ActiveButtonIndex = 2;
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

    /// <summary>
    /// Sample implementation of a printer controller
    /// In a real application, this would be implemented to communicate with the actual printer
    /// </summary>
    public class SamplePrinterController : IPrinterController
    {
        public bool IsConnected { get; private set; } = false;

        public event EventHandler<bool> ConnectionStatusChanged;

        public string ModelName => "Fraunhofer SBP-2000";

        public string SerialNumber => "SBP2000-0001";

        public void Connect()
        {
            // Simulate connecting to printer
            IsConnected = true;
            ConnectionStatusChanged?.Invoke(this, IsConnected);
        }

        public void Disconnect()
        {
            // Simulate disconnecting from printer
            IsConnected = false;
            ConnectionStatusChanged?.Invoke(this, IsConnected);
        }

        public void SendCommand(string command)
        {
            // Simulate sending command to printer
            Console.WriteLine($"Sending command: {command}");
        }
    }
}
