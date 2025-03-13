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
    /// Page for demonstrating themed controls
    /// </summary>
    public class ThemeControlsPage : PageBase
    {
        // Panels for organizing controls
        private Panel _buttonsPanel;
        private Panel _inputControlsPanel;
        private Panel _progressPanel;
        private Panel _layoutPanel;
        
        /// <summary>
        /// Creates a new ThemeControlsPage
        /// </summary>
        public ThemeControlsPage() 
            : base("UI Controls", "Demonstration of themed UI controls", "UI Controls", 
                  CreatePlaceholderImage(),
                  1) // Second page in navigation (order 1)
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create page title
            Label pageTitle = new Label
            {
                Text = "UI Controls Demonstration",
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 30)
            };
            this.Controls.Add(pageTitle);
            
            Label pageSubtitle = new Label
            {
                Text = "This page demonstrates the various themed controls available in the IGCV GUI Framework",
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 70)
            };
            this.Controls.Add(pageSubtitle);
            
            // Create Buttons panel
            _buttonsPanel = CreateStandardPanel(75, 120, 480, 200);
            
            // Add panel title
            Label buttonTitle = new Label
            {
                Text = "Button Controls",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _buttonsPanel.Controls.Add(buttonTitle);
            
            // Add button controls
            CreateButtonControls();
            
            // Create Input Controls panel
            _inputControlsPanel = CreateStandardPanel(580, 120, 500, 200);
            
            // Add panel title
            Label inputTitle = new Label
            {
                Text = "Input Controls",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _inputControlsPanel.Controls.Add(inputTitle);
            
            // Add input controls
            CreateInputControls();
            
            // Create Progress panels
            _progressPanel = CreateStandardPanel(75, 340, 480, 200);
            
            // Add panel title
            Label progressTitle = new Label
            {
                Text = "Progress Indicators",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _progressPanel.Controls.Add(progressTitle);
            
            // Add progress controls
            CreateProgressControls();
            
            // Create Layout panel
            _layoutPanel = CreateStandardPanel(580, 340, 500, 200);
            
            // Add panel title
            Label layoutTitle = new Label
            {
                Text = "Layout Controls",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _layoutPanel.Controls.Add(layoutTitle);
            
            // Add layout controls
            CreateLayoutControls();
            
            // Add panels to page
            this.Controls.Add(_buttonsPanel);
            this.Controls.Add(_inputControlsPanel);
            this.Controls.Add(_progressPanel);
            this.Controls.Add(_layoutPanel);
            
            this.ResumeLayout(false);
        }
        
        private void CreateButtonControls()
        {
            // Create primary button
            ThemedButton primaryButton = new ThemedButton
            {
                Text = "Primary Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 60),
                Size = new Size(150, 40)
            };
            primaryButton.Click += (s, e) => MessageBox.Show("Primary button clicked!", "Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            primaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(primaryButton);
            
            // Create secondary button
            ThemedButton secondaryButton = new ThemedButton
            {
                Text = "Secondary Button",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(20, 110),
                Size = new Size(150, 40)
            };
            secondaryButton.Click += (s, e) => MessageBox.Show("Secondary button clicked!", "Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            secondaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(secondaryButton);
            
            // Create tertiary button
            ThemedButton tertiaryButton = new ThemedButton
            {
                Text = "Tertiary Button",
                ButtonStyle = ButtonStyle.Tertiary,
                Location = new Point(20, 160),
                Size = new Size(150, 40)
            };
            tertiaryButton.Click += (s, e) => MessageBox.Show("Tertiary button clicked!", "Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tertiaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(tertiaryButton);
            
            // Create disabled button
            ThemedButton disabledButton = new ThemedButton
            {
                Text = "Disabled Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 60),
                Size = new Size(150, 40),
                Enabled = false
            };
            disabledButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(disabledButton);
            
            // Create icon button
            ThemedButton iconButton = new ThemedButton
            {
                Text = "✓ Icon Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 110),
                Size = new Size(150, 40)
            };
            iconButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(iconButton);
            
            // Create danger button (using error color)
            ThemedButton dangerButton = new ThemedButton
            {
                Text = "Danger Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 160),
                Size = new Size(150, 40),
                BackColor = ThemeManager.CurrentTheme.ErrorColor
            };
            dangerButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(dangerButton);
            
            // Create success button
            ThemedButton successButton = new ThemedButton
            {
                Text = "Success Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(340, 60),
                Size = new Size(120, 40),
                BackColor = ThemeManager.CurrentTheme.SuccessColor
            };
            successButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(successButton);
            
            // Create warning button
            ThemedButton warningButton = new ThemedButton
            {
                Text = "Warning",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(340, 110),
                Size = new Size(120, 40),
                BackColor = ThemeManager.CurrentTheme.WarningColor
            };
            warningButton.ApplyTheme(ThemeManager.CurrentTheme);
            _buttonsPanel.Controls.Add(warningButton);
        }
        
        private void CreateInputControls()
        {
            // Create text box
            ThemedTextBox textBox = new ThemedTextBox
            {
                PlaceholderText = "Enter text here...",
                Location = new Point(20, 60),
                Size = new Size(200, 30)
            };
            textBox.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(textBox);
            
            // Create disabled text box
            ThemedTextBox disabledTextBox = new ThemedTextBox
            {
                Text = "Disabled text box",
                Location = new Point(230, 60),
                Size = new Size(200, 30),
                Enabled = false
            };
            disabledTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(disabledTextBox);
            
            // Create password text box
            ThemedTextBox passwordTextBox = new ThemedTextBox
            {
                PlaceholderText = "Enter password...",
                Location = new Point(20, 100),
                Size = new Size(200, 30),
                PasswordChar = '•'
            };
            passwordTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(passwordTextBox);
            
            // Create combo box
            ThemedComboBox comboBox = new ThemedComboBox
            {
                Location = new Point(230, 100),
                Size = new Size(200, 30)
            };
            comboBox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
            comboBox.SelectedIndex = 0;
            comboBox.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(comboBox);
            
            // Create check boxes
            ThemedCheckBox checkBox1 = new ThemedCheckBox
            {
                Text = "Option 1",
                Location = new Point(20, 145),
                AutoSize = true
            };
            checkBox1.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(checkBox1);
            
            ThemedCheckBox checkBox2 = new ThemedCheckBox
            {
                Text = "Option 2",
                Location = new Point(120, 145),
                AutoSize = true,
                Checked = true
            };
            checkBox2.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(checkBox2);
            
            ThemedCheckBox checkBox3 = new ThemedCheckBox
            {
                Text = "Disabled",
                Location = new Point(220, 145),
                AutoSize = true,
                Enabled = false
            };
            checkBox3.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(checkBox3);
            
            // Create radio buttons
            ThemedRadioButton radioButton1 = new ThemedRadioButton
            {
                Text = "Radio 1",
                Location = new Point(320, 145),
                AutoSize = true,
                Checked = true
            };
            radioButton1.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(radioButton1);
            
            ThemedRadioButton radioButton2 = new ThemedRadioButton
            {
                Text = "Radio 2",
                Location = new Point(400, 145),
                AutoSize = true
            };
            radioButton2.ApplyTheme(ThemeManager.CurrentTheme);
            _inputControlsPanel.Controls.Add(radioButton2);
        }
        
        private void CreateProgressControls()
        {
            // Create progress bars with different values
            ThemedProgressBar progressBar1 = new ThemedProgressBar
            {
                Value = 25,
                Location = new Point(20, 60),
                Size = new Size(300, 25)
            };
            progressBar1.ApplyTheme(ThemeManager.CurrentTheme);
            _progressPanel.Controls.Add(progressBar1);
            
            ThemedProgressBar progressBar2 = new ThemedProgressBar
            {
                Value = 50,
                Location = new Point(20, 95),
                Size = new Size(300, 25)
            };
            progressBar2.ApplyTheme(ThemeManager.CurrentTheme);
            _progressPanel.Controls.Add(progressBar2);
            
            ThemedProgressBar progressBar3 = new ThemedProgressBar
            {
                Value = 75,
                Location = new Point(20, 130),
                Size = new Size(300, 25)
            };
            progressBar3.ApplyTheme(ThemeManager.CurrentTheme);
            _progressPanel.Controls.Add(progressBar3);
            
            ThemedProgressBar progressBar4 = new ThemedProgressBar
            {
                Value = 100,
                Location = new Point(20, 165),
                Size = new Size(300, 25)
            };
            progressBar4.ApplyTheme(ThemeManager.CurrentTheme);
            _progressPanel.Controls.Add(progressBar4);
            
            // Add buttons to manipulate progress
            ThemedButton increaseBtn = new ThemedButton
            {
                Text = "Increase",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(340, 60),
                Size = new Size(120, 35)
            };
            increaseBtn.Click += (s, e) => {
                // Increase all progress bars by 10%
                if (progressBar1.Value < 100) progressBar1.Value += 10;
                if (progressBar2.Value < 100) progressBar2.Value += 10;
                if (progressBar3.Value < 100) progressBar3.Value += 10;
                if (progressBar4.Value < 100) progressBar4.Value += 10;
            };
            increaseBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _progressPanel.Controls.Add(increaseBtn);
            
            ThemedButton decreaseBtn = new ThemedButton
            {
                Text = "Decrease",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(340, 105),
                Size = new Size(120, 35)
            };
            decreaseBtn.Click += (s, e) => {
                // Decrease all progress bars by 10%
                if (progressBar1.Value > 0) progressBar1.Value -= 10;
                if (progressBar2.Value > 0) progressBar2.Value -= 10;
                if (progressBar3.Value > 0) progressBar3.Value -= 10;
                if (progressBar4.Value > 0) progressBar4.Value -= 10;
            };
            decreaseBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _progressPanel.Controls.Add(decreaseBtn);
            
            ThemedButton resetBtn = new ThemedButton
            {
                Text = "Reset",
                ButtonStyle = ButtonStyle.Tertiary,
                Location = new Point(340, 150),
                Size = new Size(120, 35)
            };
            resetBtn.Click += (s, e) => {
                // Reset all progress bars
                progressBar1.Value = 25;
                progressBar2.Value = 50;
                progressBar3.Value = 75;
                progressBar4.Value = 100;
            };
            resetBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _progressPanel.Controls.Add(resetBtn);
        }
        
        private void CreateLayoutControls()
        {
            // Create nested panels to demonstrate layouts
            ThemedPanel nestedPanel1 = new ThemedPanel
            {
                Location = new Point(20, 50),
                Size = new Size(460, 60),
                CornerRadius = 5
            };
            nestedPanel1.ApplyTheme(ThemeManager.CurrentTheme);
            _layoutPanel.Controls.Add(nestedPanel1);
            
            // Add header to nested panel
            Label nestedHeader = new ThemedLabel
            {
                Text = "Panel Header",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            nestedPanel1.Controls.Add(nestedHeader);
            
            // Add content to nested panel
            Label nestedContent = new ThemedLabel
            {
                Text = "This demonstrates the panel layout capabilities with rounded corners and borders.",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 35)
            };
            nestedPanel1.Controls.Add(nestedContent);
            
            // Create a panel with different border and corner settings
            ThemedPanel nestedPanel2 = new ThemedPanel
            {
                Location = new Point(20, 120),
                Size = new Size(220, 70),
                CornerRadius = 10,
                BorderWidth = 2,
                BorderColor = ThemeManager.CurrentTheme.AccentColor
            };
            nestedPanel2.ApplyTheme(ThemeManager.CurrentTheme);
            _layoutPanel.Controls.Add(nestedPanel2);
            
            Label customBorderLabel = new ThemedLabel
            {
                Text = "Custom Border & Corners",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            nestedPanel2.Controls.Add(customBorderLabel);
            
            // Create a panel with no corners
            ThemedPanel nestedPanel3 = new ThemedPanel
            {
                Location = new Point(250, 120),
                Size = new Size(230, 70),
                CornerRadius = 0,
                BorderWidth = 1,
                BorderColor = ThemeManager.CurrentTheme.BorderColor
            };
            nestedPanel3.ApplyTheme(ThemeManager.CurrentTheme);
            _layoutPanel.Controls.Add(nestedPanel3);
            
            Label noCornersLabel = new ThemedLabel
            {
                Text = "Panel with No Rounded Corners",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            nestedPanel3.Controls.Add(noCornersLabel);
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
