using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Common.Controls
{
    /// <summary>
    /// Status panel for the left side of the application
    /// </summary>
    public class StatusPanel : UserControl
    {
        // Status labels
        private Label _printerModelLabel;
        private Label _connectionStatusLabel;
        private Label _currentTaskLabel;
        private Label _systemStatusLabel;
        
        // Status values
        private Label _printerModelValue;
        private Label _connectionStatusValue;
        private Label _currentTaskValue;
        private Label _systemStatusValue;
        
        // Status light
        private Panel _statusLight;
        
        // Printer controller
        private IPrinterController _printerController;
        
        // Constructor
        public StatusPanel()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Set control properties
            this.Dock = DockStyle.Left;
            this.Width = 220;
            this.BackColor = Color.FromArgb(20, 60, 100); // Darker blue for left panel
            
            // Create status header using ThemedLabel
            ThemedLabel statusHeader = new ThemedLabel
            {
                Text = "System Status",
                Location = new Point(20, 30),
                AutoSize = true,
                LabelSize = LabelSize.Large,
                ForeColor = ThemeManager.CurrentTheme.TextOnDarkColor
            };
            statusHeader.ApplyTheme();
            
            // Create status light
            _statusLight = new Panel
            {
                Size = new Size(30, 30),
                Location = new Point(170, 30),
                BackColor = Color.Transparent
            };
            _statusLight.Paint += (s, e) => {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (SolidBrush brush = new SolidBrush((_printerController?.IsConnected ?? false) ? 
                      ThemeManager.CurrentTheme.SuccessColor : ThemeManager.CurrentTheme.ErrorColor))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, 30, 30);
                }
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    e.Graphics.DrawEllipse(pen, 0, 0, 30, 30);
                }
            };
            
            // Create header underline
            Panel underline = new Panel
            {
                BackColor = ThemeManager.CurrentTheme.PrimaryColor,
                Location = new Point(20, 60),
                Height = 2,
                Width = 180
            };
            
            // Create status labels and values
            int yPos = 90;
            int spacing = 30;
            
            // Printer model
            _printerModelLabel = CreateStatusLabel("Printer Model:", 20, yPos);
            _printerModelValue = CreateStatusValueLabel("Not Set", 120, yPos);
            yPos += spacing;
            
            // Connection status
            _connectionStatusLabel = CreateStatusLabel("Connection:", 20, yPos);
            _connectionStatusValue = CreateStatusValueLabel("Disconnected", 120, yPos);
            yPos += spacing;
            
            // Current task
            _currentTaskLabel = CreateStatusLabel("Current Task:", 20, yPos);
            _currentTaskValue = CreateStatusValueLabel("None", 120, yPos);
            yPos += spacing;
            
            // System status
            _systemStatusLabel = CreateStatusLabel("System:", 20, yPos);
            _systemStatusValue = CreateStatusValueLabel("Idle", 120, yPos);
            yPos += spacing;
            
            // Create action buttons using ThemedButtons
            ThemedButton connectButton = new ThemedButton
            {
                Text = "Connect",
                Size = new Size(180, 40),
                Location = new Point(20, yPos + 20),
                ButtonStyle = ButtonStyle.Primary
            };
            connectButton.ApplyTheme();
            
            ThemedButton settingsButton = new ThemedButton
            {
                Text = "Settings",
                Size = new Size(180, 40),
                Location = new Point(20, yPos + 70),
                ButtonStyle = ButtonStyle.Secondary
            };
            settingsButton.ApplyTheme();
            
            // Add controls
            this.Controls.Add(statusHeader);
            this.Controls.Add(_statusLight);
            this.Controls.Add(underline);
            this.Controls.Add(_printerModelLabel);
            this.Controls.Add(_printerModelValue);
            this.Controls.Add(_connectionStatusLabel);
            this.Controls.Add(_connectionStatusValue);
            this.Controls.Add(_currentTaskLabel);
            this.Controls.Add(_currentTaskValue);
            this.Controls.Add(_systemStatusLabel);
            this.Controls.Add(_systemStatusValue);
            this.Controls.Add(connectButton);
            this.Controls.Add(settingsButton);
            
            // Connect button handler
            connectButton.Click += (s, e) => {
                if (_printerController != null)
                {
                    if (_printerController.IsConnected)
                        _printerController.Disconnect();
                    else
                        _printerController.Connect();
                        
                    UpdateConnectionStatus();
                }
            };
            
            // Register for theme changes
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
            
            this.ResumeLayout(false);
        }
        
        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            // Update colors when theme changes
            foreach (Control control in this.Controls)
            {
                if (control is ThemedButton button)
                {
                    button.ApplyTheme();
                }
                else if (control is ThemedLabel label)
                {
                    label.ApplyTheme();
                }
                else if (control is Panel panel && panel.Height == 2) // The underline
                {
                    panel.BackColor = ThemeManager.CurrentTheme.PrimaryColor;
                }
            }
            
            // Refresh the status light
            _statusLight.Invalidate();
        }
        
        private Label CreateStatusLabel(string text, int x, int y)
        {
            ThemedLabel label = new ThemedLabel
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                ForeColor = ThemeManager.CurrentTheme.TextOnDarkColor
            };
            
            label.ApplyTheme();
            return label;
        }
        
        private Label CreateStatusValueLabel(string text, int x, int y)
        {
            ThemedLabel label = new ThemedLabel
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font(ThemeManager.CurrentTheme.BodyFont, FontStyle.Bold),
                ForeColor = Color.White
            };
            
            label.ApplyTheme();
            return label;
        }
        
        /// <summary>
        /// Sets the printer controller
        /// </summary>
        public void SetPrinterController(IPrinterController controller)
        {
            // Unsubscribe from old controller events
            if (_printerController != null)
            {
                _printerController.ConnectionStatusChanged -= OnConnectionStatusChanged;
            }
            
            _printerController = controller;
            
            // Subscribe to new controller events
            if (_printerController != null)
            {
                _printerController.ConnectionStatusChanged += OnConnectionStatusChanged;
                _printerModelValue.Text = _printerController.ModelName ?? "Unknown";
                UpdateConnectionStatus();
            }
        }
        
        /// <summary>
        /// Updates the current task display
        /// </summary>
        public void SetCurrentTask(string task)
        {
            _currentTaskValue.Text = task ?? "None";
        }
        
        /// <summary>
        /// Updates the system status display
        /// </summary>
        public void SetSystemStatus(string status)
        {
            _systemStatusValue.Text = status ?? "Unknown";
        }
        
        private void UpdateConnectionStatus()
        {
            bool isConnected = _printerController?.IsConnected ?? false;
            _connectionStatusValue.Text = isConnected ? "Connected" : "Disconnected";
            _statusLight.Invalidate(); // Force redraw of status light
            
            // Update connect button text
            foreach (Control c in this.Controls)
            {
                if (c is Button btn && (btn.Text == "Connect" || btn.Text == "Disconnect"))
                {
                    btn.Text = isConnected ? "Disconnect" : "Connect";
                }
            }
        }
        
        private void OnConnectionStatusChanged(object sender, bool isConnected)
        {
            // Update connection status when the controller reports a change
            UpdateConnectionStatus();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unregister from theme changed event
                ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
}
