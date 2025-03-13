using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV.GUI.Themes.FraunhoferCI;
using IGCV_GUI_Framework;

namespace IGCV.GUI.Demo
{
    /// <summary>
    /// Launcher form for the various demonstrations of the IGCV.GUI framework
    /// </summary>
    public class DemoLauncher : Form
    {
        private ThemedPanel _mainPanel;
        private ThemedLabel _headerLabel;
        private ThemedLabel _subHeaderLabel;
        
        public DemoLauncher()
        {
            // Enable double buffering
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            
            // Initialize the form
            InitializeComponent();
            
            // Apply theme to all controls
            ThemeManager.ApplyThemeToContainer(this);
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Text = "IGCV GUI Framework Demo Launcher";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Paint += DemoLauncher_Paint;
            
            // Main panel with content
            _mainPanel = new ThemedPanel
            {
                Location = new Point(20, 20),
                Size = new Size(Width - 40, Height - 40),
                PanelStyle = PanelStyle.Light,
                CornerRadius = 5,
                BorderWidth = 1
            };
            this.Controls.Add(_mainPanel);
            
            // Header
            _headerLabel = new ThemedLabel
            {
                Text = "IGCV GUI Framework Demo",
                LabelStyle = LabelStyle.Header,
                Location = new Point(30, 30),
                AutoSize = true
            };
            _mainPanel.Controls.Add(_headerLabel);
            
            // Subheader
            _subHeaderLabel = new ThemedLabel
            {
                Text = "Select a demo to run:",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(30, 70),
                AutoSize = true
            };
            _mainPanel.Controls.Add(_subHeaderLabel);
            
            // Create demo buttons
            CreateDemoButtons();
            
            // Add theme selection
            CreateThemeSelection();
            
            // Add footer
            ThemedLabel footerLabel = new ThemedLabel
            {
                Text = "© Fraunhofer IGCV",
                LabelStyle = LabelStyle.Default,
                Location = new Point(_mainPanel.Width - 120, _mainPanel.Height - 30),
                AutoSize = true
            };
            _mainPanel.Controls.Add(footerLabel);
            
            this.ResumeLayout(false);
        }
        
        private void CreateDemoButtons()
        {
            // Demo button 1 - Controls Demo
            ThemedPanel demoCard1 = new ThemedPanel
            {
                Location = new Point(30, 120),
                Size = new Size(300, 100),
                PanelStyle = PanelStyle.Secondary,
                CornerRadius = 5
            };
            _mainPanel.Controls.Add(demoCard1);
            
            ThemedLabel demoTitle1 = new ThemedLabel
            {
                Text = "Controls Demo",
                LabelStyle = LabelStyle.Default,
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };
            demoCard1.Controls.Add(demoTitle1);
            
            ThemedLabel demoDesc1 = new ThemedLabel
            {
                Text = "Shows all themed UI controls in action",
                LabelStyle = LabelStyle.Default,
                ForeColor = Color.White,
                Location = new Point(20, 40),
                AutoSize = true
            };
            demoCard1.Controls.Add(demoDesc1);
            
            ThemedButton launchBtn1 = new ThemedButton
            {
                Text = "Launch",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 60),
                Size = new Size(100, 30)
            };
            launchBtn1.Click += (s, e) => { LaunchDemo<ControlsDemoForm>(); };
            demoCard1.Controls.Add(launchBtn1);
            
            // Demo button 2 - Integration Demo
            ThemedPanel demoCard2 = new ThemedPanel
            {
                Location = new Point(30, 240),
                Size = new Size(300, 100),
                PanelStyle = PanelStyle.Primary,
                CornerRadius = 5
            };
            _mainPanel.Controls.Add(demoCard2);
            
            ThemedLabel demoTitle2 = new ThemedLabel
            {
                Text = "Integration Demo",
                LabelStyle = LabelStyle.Default,
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };
            demoCard2.Controls.Add(demoTitle2);
            
            ThemedLabel demoDesc2 = new ThemedLabel
            {
                Text = "Application-like demo with different panels",
                LabelStyle = LabelStyle.Default,
                ForeColor = Color.White,
                Location = new Point(20, 40),
                AutoSize = true
            };
            demoCard2.Controls.Add(demoDesc2);
            
            ThemedButton launchBtn2 = new ThemedButton
            {
                Text = "Launch",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 60),
                Size = new Size(100, 30)
            };
            launchBtn2.Click += (s, e) => { LaunchDemo<ThemeIntegrationSample>(); };
            demoCard2.Controls.Add(launchBtn2);
            
            // Demo button 3 - Go to Main App
            ThemedPanel demoCard3 = new ThemedPanel
            {
                Location = new Point(350, 120),
                Size = new Size(300, 100),
                PanelStyle = PanelStyle.Accent,
                CornerRadius = 5
            };
            _mainPanel.Controls.Add(demoCard3);
            
            ThemedLabel demoTitle3 = new ThemedLabel
            {
                Text = "Main Application",
                LabelStyle = LabelStyle.Default,
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };
            demoCard3.Controls.Add(demoTitle3);
            
            ThemedLabel demoDesc3 = new ThemedLabel
            {
                Text = "Launch the main application",
                LabelStyle = LabelStyle.Default,
                ForeColor = Color.White,
                Location = new Point(20, 40),
                AutoSize = true
            };
            demoCard3.Controls.Add(demoDesc3);
            
            ThemedButton launchBtn3 = new ThemedButton
            {
                Text = "Launch",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 60),
                Size = new Size(100, 30)
            };
            launchBtn3.Click += (s, e) => {
                try {
                    // Launch main application
                    var mainForm = new IGCV_GUI_Framework.MainForm();
                    mainForm.Show();
                } catch (Exception ex) {
                    MessageBox.Show("Error launching main application: " + ex.Message, 
                                   "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            demoCard3.Controls.Add(launchBtn3);
        }
        
        private void CreateThemeSelection()
        {
            // Theme selection panel
            ThemedPanel themePanel = new ThemedPanel
            {
                Location = new Point(350, 240),
                Size = new Size(300, 100),
                PanelStyle = PanelStyle.Light,
                CornerRadius = 5,
                BorderWidth = 1
            };
            _mainPanel.Controls.Add(themePanel);
            
            ThemedLabel themeLabel = new ThemedLabel
            {
                Text = "Theme Selection",
                LabelStyle = LabelStyle.Secondary,
                Location = new Point(20, 15),
                AutoSize = true
            };
            themePanel.Controls.Add(themeLabel);
            
            // Fraunhofer Theme option
            ThemedRadioButton fraunhoferRadio = new ThemedRadioButton
            {
                Text = "Fraunhofer Theme",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(20, 45),
                Checked = true
            };
            fraunhoferRadio.CheckedChanged += (s, e) => {
                if (fraunhoferRadio.Checked)
                {
                    ThemeManager.SetTheme("Fraunhofer CI");
                    ThemeManager.ApplyThemeToContainer(this);
                    this.Invalidate(); // Refresh the form
                }
            };
            themePanel.Controls.Add(fraunhoferRadio);
            
            // Dark Theme option
            ThemedRadioButton darkRadio = new ThemedRadioButton
            {
                Text = "Dark Theme",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(20, 70),
                Checked = false
            };
            darkRadio.CheckedChanged += (s, e) => {
                if (darkRadio.Checked)
                {
                    ThemeManager.SetTheme("Dark Theme");
                    ThemeManager.ApplyThemeToContainer(this);
                    this.Invalidate(); // Refresh the form
                }
            };
            themePanel.Controls.Add(darkRadio);
        }
        
        private void LaunchDemo<T>() where T : Form, new()
        {
            try
            {
                var form = new T();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error launching demo: " + ex.Message, 
                               "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DemoLauncher_Paint(object sender, PaintEventArgs e)
        {
            // Apply the theme gradient background
            ThemeManager.ApplyGradientBackground(this, e);
        }
    }
}
