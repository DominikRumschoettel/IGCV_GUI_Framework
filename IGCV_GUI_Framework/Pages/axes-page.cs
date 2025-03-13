using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV_GUI_Framework.Common;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Pages
{
    /// <summary>
    /// Page for controlling printer axes
    /// </summary>
    public class AxesPage : PageBase
    {
        // Panels
        private Panel _axisControlPanel;
        private Panel _jogPanel;
        
        // Axis controls
        private TrackBar _xAxisTrackbar;
        private TrackBar _yAxisTrackbar;
        private TrackBar _zAxisTrackbar;
        
        // Jog controls
        private TrackBar _jogSpeedTrackbar;
        private Label _jogSpeedLabel;
        
        // Printer controller
        private IPrinterController _printerController;
        
        /// <summary>
        /// Creates a new AxesPage
        /// </summary>
        public AxesPage(IPrinterController printerController) 
            : base("Moving", "Control printer motion and positioning", "Moving", 
                  CreatePlaceholderImage(), // Create a placeholder image instead of using resources
                  1) // Order in navigation
        {
            _printerController = printerController;
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create axis control panel
            _axisControlPanel = CreateStandardPanel(75, 200, 600, 300);
            
            // Create X axis control
            _xAxisTrackbar = CreateAxisControl("X Axis", 0, 200, 100, 0, 0);
            
            // Create Y axis control 
            _yAxisTrackbar = CreateAxisControl("Y Axis", 0, 200, 80, 0, 60);
            
            // Create Z axis control
            _zAxisTrackbar = CreateAxisControl("Z Axis", 0, 200, 150, 0, 120);
            
            // Create home buttons
            Button homeAllBtn = CreatePrimaryButton("Home All Axes", 0, 180, 150, 40);
            Button homeXBtn = CreateSecondaryButton("Home X", 160, 180, 120, 40);
            Button homeYBtn = CreateSecondaryButton("Home Y", 290, 180, 120, 40);
            Button homeZBtn = CreateSecondaryButton("Home Z", 420, 180, 120, 40);
            
            // Add home buttons to axis panel
            _axisControlPanel.Controls.Add(homeAllBtn);
            _axisControlPanel.Controls.Add(homeXBtn);
            _axisControlPanel.Controls.Add(homeYBtn);
            _axisControlPanel.Controls.Add(homeZBtn);
            
            // Create jog panel
            _jogPanel = CreateStandardPanel(700, 200, 300, 400);
            
            // Create jog speed label
            _jogSpeedLabel = new Label
            {
                Text = "Jog Speed: 50 mm/s",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _jogPanel.Controls.Add(_jogSpeedLabel);
            
            // Create jog direction buttons
            Button jogYPlusBtn = CreateJogButton("+Y", 120, 70, 60, 60);
            Button jogXMinusBtn = CreateJogButton("-X", 50, 140, 60, 60);
            Button jogXPlusBtn = CreateJogButton("+X", 190, 140, 60, 60);
            Button jogYMinusBtn = CreateJogButton("-Y", 120, 210, 60, 60);
            Button jogZPlusBtn = CreateJogButton("+Z", 50, 70, 60, 60);
            Button jogZMinusBtn = CreateJogButton("-Z", 190, 210, 60, 60);
            
            // Add jog buttons to jog panel
            _jogPanel.Controls.Add(jogYPlusBtn);
            _jogPanel.Controls.Add(jogXMinusBtn);
            _jogPanel.Controls.Add(jogXPlusBtn);
            _jogPanel.Controls.Add(jogYMinusBtn);
            _jogPanel.Controls.Add(jogZPlusBtn);
            _jogPanel.Controls.Add(jogZMinusBtn);
            
            // Create jog speed trackbar
            _jogSpeedTrackbar = new TrackBar
            {
                Minimum = 10,
                Maximum = 100,
                Value = 50,
                Location = new Point(10, 320),
                Size = new Size(280, 45),
                BackColor = _jogPanel.BackColor,
                TickStyle = TickStyle.Both,
                TickFrequency = 10
            };
            
            _jogSpeedTrackbar.ValueChanged += (s, e) => {
                _jogSpeedLabel.Text = $"Jog Speed: {_jogSpeedTrackbar.Value} mm/s";
            };
            
            _jogPanel.Controls.Add(_jogSpeedTrackbar);
            
            // Add panels to page
            this.Controls.Add(_axisControlPanel);
            this.Controls.Add(_jogPanel);
            
            // Set up button click handlers
            homeAllBtn.Click += (s, e) => SendHomeCommand("all");
            homeXBtn.Click += (s, e) => SendHomeCommand("x");
            homeYBtn.Click += (s, e) => SendHomeCommand("y");
            homeZBtn.Click += (s, e) => SendHomeCommand("z");
            
            jogXPlusBtn.Click += (s, e) => SendJogCommand("x", _jogSpeedTrackbar.Value);
            jogXMinusBtn.Click += (s, e) => SendJogCommand("x", -_jogSpeedTrackbar.Value);
            jogYPlusBtn.Click += (s, e) => SendJogCommand("y", _jogSpeedTrackbar.Value);
            jogYMinusBtn.Click += (s, e) => SendJogCommand("y", -_jogSpeedTrackbar.Value);
            jogZPlusBtn.Click += (s, e) => SendJogCommand("z", _jogSpeedTrackbar.Value);
            jogZMinusBtn.Click += (s, e) => SendJogCommand("z", -_jogSpeedTrackbar.Value);
            
            this.ResumeLayout(false);
        }
        
        private TrackBar CreateAxisControl(string axisName, int min, int max, int value, int x, int y)
        {
            // Create axis panel
            Panel axisPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(600, 50),
                BackColor = FraunhoferTheme.DarkPanel
            };
            
            // Create axis label
            Label axisLabel = new Label
            {
                Text = axisName,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            
            // Create axis trackbar
            TrackBar axisTrackbar = new TrackBar
            {
                Minimum = min,
                Maximum = max,
                Value = value,
                Location = new Point(100, 10),
                Size = new Size(400, 45),
                BackColor = FraunhoferTheme.DarkPanel
            };
            
            // Create axis value label
            Label axisValue = new Label
            {
                Text = $"{value} mm",
                Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(510, 15)
            };
            
            // Add value changed handler
            axisTrackbar.ValueChanged += (s, e) => {
                axisValue.Text = $"{axisTrackbar.Value} mm";
                SendAxisPositionCommand(axisName.Split(' ')[0].ToLower(), axisTrackbar.Value);
            };
            
            // Add controls to panel
            axisPanel.Controls.Add(axisLabel);
            axisPanel.Controls.Add(axisTrackbar);
            axisPanel.Controls.Add(axisValue);
            
            // Add panel to axis control panel
            _axisControlPanel.Controls.Add(axisPanel);
            
            return axisTrackbar;
        }
        
        private Button CreateJogButton(string text, int x, int y, int width, int height)
        {
            Button btn = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                BackColor = FraunhoferTheme.ButtonBackground,
                ForeColor = FraunhoferTheme.DarkTextColor,
                Size = new Size(width, height),
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 12f, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
        
        private void SendHomeCommand(string axis)
        {
            if (_printerController != null && _printerController.IsConnected)
            {
                _printerController.SendCommand($"Home {axis}");
            }
            else
            {
                MessageBox.Show("Printer not connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void SendJogCommand(string axis, int speed)
        {
            if (_printerController != null && _printerController.IsConnected)
            {
                _printerController.SendCommand($"Jog {axis} {speed}");
            }
            else
            {
                MessageBox.Show("Printer not connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void SendAxisPositionCommand(string axis, int position)
        {
            if (_printerController != null && _printerController.IsConnected)
            {
                _printerController.SendCommand($"Move {axis} {position}");
            }
        }
        
        
        /// <summary>
        /// Called when the page is activated
        /// </summary>
        public override void OnActivated()
        {
            base.OnActivated();
            
            // Refresh current axis positions from printer controller
            if (_printerController != null && _printerController.IsConnected)
            {
                // In a real implementation, we would query the printer for current positions
                // and update the trackbars accordingly
            }
        }
        
        /// <summary>
        /// Creates a placeholder image for demonstration purposes
        /// </summary>
        private static Image CreatePlaceholderImage()
        {
            Bitmap placeholderImage = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(placeholderImage))
            {
                g.Clear(Color.FromArgb(0, 103, 172)); // Fraunhofer blue
            }
            return placeholderImage;
        }
    }
}
