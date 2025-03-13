using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV_GUI_Framework.Common;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Pages
{
    /// <summary>
    /// Page for controlling machine motion
    /// </summary>
    public class MotionControlPage : PageBase
    {
        // Panels
        private Panel _axisControlPanel;
        private Panel _jogPanel;
        private Panel _controlsDemoPanel;
        
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
        /// Creates a new MotionControlPage
        /// </summary>
        public MotionControlPage(IPrinterController printerController) 
            : base("Motion Control", "Machine positioning and movement controls", "Motion", 
                  CreatePlaceholderImage(), // Create a placeholder image instead of using resources
                  0) // First page in navigation (order 0)
        {
            _printerController = printerController;
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create a title for the Controls Demo section
            Label demoTitle = new Label
            {
                Text = "Motion Control System",
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 30)
            };
            this.Controls.Add(demoTitle);
            
            Label demoSubtitle = new Label
            {
                Text = "This page demonstrates themed controls for machine motion and positioning",
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 70)
            };
            this.Controls.Add(demoSubtitle);
            
            // Create axis control panel
            _axisControlPanel = CreateStandardPanel(75, 120, 600, 300);
            
            // Add a panel title
            Label axisTitle = new Label
            {
                Text = "Axis Position Control",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _axisControlPanel.Controls.Add(axisTitle);
            
            // Create X axis control
            _xAxisTrackbar = CreateAxisControl("X Axis", 0, 200, 100, 0, 50);
            
            // Create Y axis control 
            _yAxisTrackbar = CreateAxisControl("Y Axis", 0, 200, 80, 0, 110);
            
            // Create Z axis control
            _zAxisTrackbar = CreateAxisControl("Z Axis", 0, 200, 150, 0, 170);
            
            // Create home buttons
            Button homeAllBtn = CreatePrimaryButton("Home All Axes", 20, 230, 150, 40);
            Button homeXBtn = CreateSecondaryButton("Home X", 180, 230, 120, 40);
            Button homeYBtn = CreateSecondaryButton("Home Y", 310, 230, 120, 40);
            Button homeZBtn = CreateSecondaryButton("Home Z", 440, 230, 120, 40);
            
            // Add home buttons to axis panel
            _axisControlPanel.Controls.Add(homeAllBtn);
            _axisControlPanel.Controls.Add(homeXBtn);
            _axisControlPanel.Controls.Add(homeYBtn);
            _axisControlPanel.Controls.Add(homeZBtn);
            
            // Create jog panel
            _jogPanel = CreateStandardPanel(700, 120, 380, 300);
            
            // Add a panel title
            Label jogTitle = new Label
            {
                Text = "Jog Control",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _jogPanel.Controls.Add(jogTitle);
            
            // Create jog speed label
            _jogSpeedLabel = new Label
            {
                Text = "Jog Speed: 50 mm/s",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };
            _jogPanel.Controls.Add(_jogSpeedLabel);
            
            // Create jog direction buttons
            Button jogYPlusBtn = CreateJogButton("+Y", 160, 100, 60, 60);
            Button jogXMinusBtn = CreateJogButton("-X", 90, 170, 60, 60);
            Button jogXPlusBtn = CreateJogButton("+X", 230, 170, 60, 60);
            Button jogYMinusBtn = CreateJogButton("-Y", 160, 170, 60, 60);
            Button jogZPlusBtn = CreateJogButton("+Z", 300, 100, 60, 60);
            Button jogZMinusBtn = CreateJogButton("-Z", 300, 170, 60, 60);
            
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
                Location = new Point(20, 240),
                Size = new Size(340, 45),
                BackColor = _jogPanel.BackColor, // Already using the correct background color
                TickStyle = TickStyle.Both,
                TickFrequency = 10
            };
            
            _jogSpeedTrackbar.ValueChanged += (s, e) => {
                _jogSpeedLabel.Text = $"Jog Speed: {_jogSpeedTrackbar.Value} mm/s";
            };
            
            _jogPanel.Controls.Add(_jogSpeedTrackbar);
            
            // Create additional controls demo panel
            _controlsDemoPanel = CreateStandardPanel(75, 440, 1005, 200);
            
            // Add a panel title
            Label controlsDemoTitle = new Label
            {
                Text = "Additional Controls Demo",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _controlsDemoPanel.Controls.Add(controlsDemoTitle);
            
            // Create a few themed controls to demonstrate the framework
            ThemedButton primaryBtn = new ThemedButton
            {
                Text = "Primary Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 60),
                Size = new Size(150, 40)
            };
            primaryBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(primaryBtn);
            
            ThemedButton secondaryBtn = new ThemedButton
            {
                Text = "Secondary Button",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(180, 60),
                Size = new Size(150, 40)
            };
            secondaryBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(secondaryBtn);
            
            ThemedButton tertiaryBtn = new ThemedButton
            {
                Text = "Tertiary Button",
                ButtonStyle = ButtonStyle.Tertiary,
                Location = new Point(340, 60),
                Size = new Size(150, 40)
            };
            tertiaryBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(tertiaryBtn);
            
            ThemedTextBox textBox = new ThemedTextBox
            {
                PlaceholderText = "Enter text here...",
                Location = new Point(20, 120),
                Size = new Size(200, 30)
            };
            textBox.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(textBox);
            
            ThemedCheckBox checkBox = new ThemedCheckBox
            {
                Text = "Enable feature",
                Location = new Point(240, 120),
                AutoSize = true
            };
            checkBox.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(checkBox);
            
            ThemedRadioButton radioBtn1 = new ThemedRadioButton
            {
                Text = "Option 1",
                Location = new Point(380, 120),
                AutoSize = true,
                Checked = true
            };
            radioBtn1.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(radioBtn1);
            
            ThemedRadioButton radioBtn2 = new ThemedRadioButton
            {
                Text = "Option 2",
                Location = new Point(480, 120),
                AutoSize = true
            };
            radioBtn2.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(radioBtn2);
            
            ThemedProgressBar progressBar = new ThemedProgressBar
            {
                Value = 75,
                Location = new Point(600, 120),
                Size = new Size(200, 30)
            };
            progressBar.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(progressBar);
            
            ThemedComboBox comboBox = new ThemedComboBox
            {
                Location = new Point(820, 120),
                Size = new Size(150, 30)
            };
            comboBox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3" });
            comboBox.SelectedIndex = 0;
            comboBox.ApplyTheme(ThemeManager.CurrentTheme);
            _controlsDemoPanel.Controls.Add(comboBox);
            
            // Add the themed status indicator
            Label statusLabel = new Label
            {
                Text = "Connection Status:",
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(600, 60)
            };
            _controlsDemoPanel.Controls.Add(statusLabel);
            
            // Add the connection indicator
            Panel statusIndicator = new Panel
            {
                Size = new Size(20, 20),
                Location = new Point(740, 60),
                BackColor = _printerController.IsConnected ? Color.Green : Color.Red
            };
            _controlsDemoPanel.Controls.Add(statusIndicator);
            
            // Update status on connection change
            if (_printerController != null)
            {
                _printerController.ConnectionStatusChanged += (s, e) =>
                {
                    statusIndicator.BackColor = e ? Color.Green : Color.Red;
                };
            }
            
            // Add panels to page
            this.Controls.Add(_axisControlPanel);
            this.Controls.Add(_jogPanel);
            this.Controls.Add(_controlsDemoPanel);
            
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
            
            primaryBtn.Click += (s, e) => MessageBox.Show("Primary button clicked!", "Control Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.ResumeLayout(false);
        }
        
        private TrackBar CreateAxisControl(string axisName, int min, int max, int value, int x, int y)
        {
            // Create axis panel
            Panel axisPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(600, 50),
                BackColor = Color.Transparent
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
                BackColor = _axisControlPanel.BackColor // Use the parent panel's color instead of transparent
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
            ThemedButton btn = new ThemedButton
            {
                Text = text,
                ButtonStyle = ButtonStyle.Primary,
                Size = new Size(width, height),
                Location = new Point(x, y),
            };
            
            btn.ApplyTheme(ThemeManager.CurrentTheme);
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
                MessageBox.Show("Machine not connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Machine not connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
