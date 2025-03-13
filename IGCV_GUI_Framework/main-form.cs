using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IGCV_GUI_Framework.Common;
using IGCV_GUI_Framework.Common.Controls;
using IGCV_GUI_Framework.Interfaces;
using IGCV_GUI_Framework.Pages;

namespace IGCV_GUI_Framework
{
    public class MainForm : Form
    {
        // Main UI components
        private NavigationBar _navigationBar;
        private StatusPanel _statusPanel;
        private PageContainer _pageContainer;

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

            // Initialize components (designer-generated code)
            InitializeComponent();

            // *** IMPORTANT: Force recreation of PageContainer after designer initialization ***
            ForceRecreatePageContainer();

            // Initialize pages
            InitializePages();
        }

        // Create a dedicated method to force recreation of the PageContainer
        private void ForceRecreatePageContainer()
        {
            // Remove the designer-created PageContainer
            if (_pageContainer != null)
            {
                this.Controls.Remove(_pageContainer);
                _pageContainer.Dispose();
            }

            // Create a new PageContainer with the correct implementation
            _pageContainer = new PageContainer();
            _pageContainer.Dock = DockStyle.Fill;

            // Add the recreated PageContainer to the form
            // Important: Add it in the correct order - it should be added before StatusPanel
            // so that StatusPanel appears on top
            this.Controls.Add(_pageContainer);

            // Fix control order - ensure proper z-order
            this.Controls.SetChildIndex(_pageContainer, 0);  // Bottom-most
            this.Controls.SetChildIndex(_statusPanel, 1);    // Middle
            this.Controls.SetChildIndex(_navigationBar, 2);  // Top-most
        }

        private void InitializeComponent()
        {
            _navigationBar = new NavigationBar();
            _statusPanel = new StatusPanel();
            _pageContainer = new PageContainer();
            SuspendLayout();
            // 
            // _navigationBar
            // 
            _navigationBar.BackColor = Color.FromArgb(240, 240, 240);
            _navigationBar.Dock = DockStyle.Bottom;
            _navigationBar.Location = new Point(0, 773);
            _navigationBar.Name = "_navigationBar";
            _navigationBar.Size = new Size(1582, 80);
            _navigationBar.TabIndex = 2;
            _navigationBar.PageSelected += NavigationBar_PageSelected;
            // 
            // _statusPanel
            // 
            _statusPanel.BackColor = Color.FromArgb(20, 60, 100);
            _statusPanel.Dock = DockStyle.Left;
            _statusPanel.Location = new Point(0, 0);
            _statusPanel.Name = "_statusPanel";
            _statusPanel.Size = new Size(220, 773);
            _statusPanel.TabIndex = 1;
            // 
            // _pageContainer
            // 
            _pageContainer.BackColor = Color.Transparent;
            _pageContainer.Location = new Point(0, 0);
            _pageContainer.Name = "_pageContainer";
            _pageContainer.Size = new Size(1396, 584);
            _pageContainer.TabIndex = 0;
            // 
            // MainForm
            // 
            ClientSize = new Size(1582, 853);
            Controls.Add(_pageContainer);
            Controls.Add(_statusPanel);
            Controls.Add(_navigationBar);
            MinimumSize = new Size(800, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IGCV GUI Framework Demo";
            Paint += MainForm_Paint;
            Resize += MainForm_Resize;
            ResumeLayout(false);
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

            // Set up navigation bar
            _navigationBar.SetPages(_pages);

            // Start with main menu
            ShowPage(0);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // Draw gradient background
            FraunhoferTheme.ApplyGradientBackground(this, e);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Force redraw of gradient
            Invalidate();
        }

        private void NavigationBar_PageSelected(object sender, int pageIndex)
        {
            ShowPage(pageIndex);
        }

        /// <summary>
        /// Shows the specified page
        /// </summary>
        private void ShowPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= _pages.Count) return;

            // Update current page index
            _currentPageIndex = pageIndex;

            // Set the page without excessive redraws
            _pageContainer.SetPage(_pages[pageIndex]);
            _navigationBar.SetActivePage(pageIndex);
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
