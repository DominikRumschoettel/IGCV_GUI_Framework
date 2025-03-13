using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV.GUI.Themes.FraunhoferCI;

namespace IGCV.GUI.Demo
{
    /// <summary>
    /// Sample form that shows how to integrate the IGCV.GUI framework
    /// with an existing application
    /// </summary>
    public class ThemeIntegrationSample : Form
    {
        // Main UI components
        private ThemedPanel _mainPanel;
        private ThemedPanel _sidePanel;
        private ThemedLabel _headerLabel;
        private ThemedPanel _contentPanel;
        
        // Navigation buttons
        private List<ThemedButton> _navButtons = new List<ThemedButton>();
        
        // Active content index
        private int _activeContentIndex = 0;
        
        // Content panels
        private List<ThemedPanel> _contentPanels = new List<ThemedPanel>();
        
        public ThemeIntegrationSample()
        {
            // Enable double buffering to reduce flickering
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            
            // Initialize the form
            InitializeComponent();
            
            // Apply theme to the form
            ThemeManager.ApplyThemeToContainer(this);
            
            // Show first content panel
            ShowContentPanel(0);
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Text = "IGCV.GUI Theme Integration Sample";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Paint += ThemeIntegrationSample_Paint;
            
            // Create main panel
            _mainPanel = new ThemedPanel
            {
                Location = new Point(0, 0),
                Size = new Size(Width, Height),
                PanelStyle = PanelStyle.Light,
                CornerRadius = 0,
                BorderWidth = 0,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(_mainPanel);
            
            // Create side panel - use the specific style from Fraunhofer theme
            var fraunhoferTheme = ThemeManager.CurrentTheme as FraunhoferTheme;
            if (fraunhoferTheme != null)
            {
                _sidePanel = new ThemedPanel
                {
                    Location = new Point(0, 0),
                    Size = new Size(220, _mainPanel.Height),
                    BackColor = fraunhoferTheme.SteelBlue,  // Use the Fraunhofer steel blue color
                    ForeColor = fraunhoferTheme.TextOnDarkColor,
                    CornerRadius = 0,
                    Dock = DockStyle.Left
                };
            }
            else
            {
                _sidePanel = new ThemedPanel
                {
                    Location = new Point(0, 0),
                    Size = new Size(220, _mainPanel.Height),
                    PanelStyle = PanelStyle.Primary,
                    CornerRadius = 0,
                    Dock = DockStyle.Left
                };
            }
            _mainPanel.Controls.Add(_sidePanel);
            
            // Create header label with gradient text that better matches the original app
            _headerLabel = new ThemedLabel
            {
                Text = "Fraunhofer IGCV",
                LabelStyle = LabelStyle.Header,
                Location = new Point(20, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                AutoSize = true
            };
            _sidePanel.Controls.Add(_headerLabel);
            
            // Create content panel - use blue gradient background
            _contentPanel = new ThemedPanel
            {
                Location = new Point(_sidePanel.Width, 0),
                Size = new Size(_mainPanel.Width - _sidePanel.Width, _mainPanel.Height),
                PanelStyle = PanelStyle.Light,
                CornerRadius = 0,
                BorderWidth = 0,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            
            // Add paint handler for gradient background
            _contentPanel.Paint += (s, e) => {
                if (ThemeManager.CurrentTheme is FraunhoferTheme fraunhoferTheme)
                {
                    fraunhoferTheme.ApplyGradientBackground(e, _contentPanel.ClientRectangle);
                }
            };
            _mainPanel.Controls.Add(_contentPanel);
            
            // Create navigation buttons
            CreateNavigationButtons();
            
            // Create content panels
            CreateContentPanels();
            
            this.ResumeLayout(false);
        }
        
        private void CreateNavigationButtons()
        {
            string[] buttonTexts = new string[]
            {
                "Dashboard",
                "Controls",
                "Settings",
                "Help"
            };
            
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                int index = i; // Capture for lambda
                
                ThemedButton navButton = new ThemedButton
                {
                    Text = buttonTexts[i],
                    Location = new Point(10, 80 + (i * 50)),
                    Size = new Size(200, 40)
                };
                
                // Apply custom styling to match the original app
                navButton.FlatStyle = FlatStyle.Flat;
                navButton.BackColor = i == 0 ? 
                    Color.FromArgb(0, 103, 172) :  // Active button - Fraunhofer blue
                    Color.FromArgb(0, 169, 144);   // Inactive button - Fraunhofer teal
                navButton.ForeColor = Color.White;
                navButton.TextAlign = ContentAlignment.MiddleCenter;
                navButton.Font = new Font(navButton.Font, FontStyle.Regular);
                navButton.FlatAppearance.BorderSize = 0;
                navButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 123, 192); // Lighter blue for hover
                
                navButton.Click += (s, e) => 
                {
                    // Update active index
                    _activeContentIndex = index;
                    
                    // Update button styles to match original app
                    foreach (var btn in _navButtons)
                    {
                        btn.BackColor = Color.FromArgb(0, 169, 144);  // Inactive - Fraunhofer teal
                        btn.ForeColor = Color.White;
                    }
                    navButton.BackColor = Color.FromArgb(0, 103, 172);  // Active - Fraunhofer blue
                    
                    // Show corresponding content panel
                    ShowContentPanel(index);
                };
                
                _sidePanel.Controls.Add(navButton);
                _navButtons.Add(navButton);
            }
            
            // Add a version label at the bottom
            ThemedLabel versionLabel = new ThemedLabel
            {
                Text = "Version 1.0",
                LabelStyle = LabelStyle.Default,
                ForeColor = Color.White,
                Location = new Point(10, _sidePanel.Height - 40),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            _sidePanel.Controls.Add(versionLabel);
        }
        
        private void CreateContentPanels()
        {
            // Dashboard panel
            ThemedPanel dashboardPanel = CreateDashboardPanel();
            _contentPanels.Add(dashboardPanel);
            _contentPanel.Controls.Add(dashboardPanel);
            
            // Controls panel
            ThemedPanel controlsPanel = CreateControlsPanel();
            _contentPanels.Add(controlsPanel);
            _contentPanel.Controls.Add(controlsPanel);
            
            // Settings panel
            ThemedPanel settingsPanel = CreateSettingsPanel();
            _contentPanels.Add(settingsPanel);
            _contentPanel.Controls.Add(settingsPanel);
            
            // Help panel
            ThemedPanel helpPanel = CreateHelpPanel();
            _contentPanels.Add(helpPanel);
            _contentPanel.Controls.Add(helpPanel);
        }
        
        private ThemedPanel CreateDashboardPanel()
        {
            ThemedPanel panel = new ThemedPanel
            {
                Dock = DockStyle.Fill,
                PanelStyle = PanelStyle.Light,
                BorderWidth = 0
            };
            
            // Header
            ThemedLabel headerLabel = new ThemedLabel
            {
                Text = "Dashboard",
                LabelStyle = LabelStyle.Header,
                Location = new Point(20, 20),
                AutoSize = true
            };
            panel.Controls.Add(headerLabel);
            
            // Subheader
            ThemedLabel subheaderLabel = new ThemedLabel
            {
                Text = "System Overview",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(20, 60),
                AutoSize = true
            };
            panel.Controls.Add(subheaderLabel);
            
            // Status cards layout with Fraunhofer colors - Green, Blue, and Orange with proper contrast
            // Use the official Fraunhofer colors from the CI guidelines
            ThemedPanel card1 = CreateStatusCard("System Status", "Online", "#179c7d", new Point(20, 100)); // Fraunhofer Green
            ThemedPanel card2 = CreateStatusCard("CPU Usage", "32%", "#005b7f", new Point(240, 100));  // Fraunhofer Steel Blue
            ThemedPanel card3 = CreateStatusCard("Memory", "2.4 GB", "#f58220", new Point(460, 100)); // Fraunhofer Orange
            ThemedPanel card4 = CreateStatusCard("Temperature", "28°C", "#1c3f52", new Point(680, 100)); // Fraunhofer Graphit
            
            panel.Controls.Add(card1);
            panel.Controls.Add(card2);
            panel.Controls.Add(card3);
            panel.Controls.Add(card4);
            
            // Progress section
            ThemedLabel progressLabel = new ThemedLabel
            {
                Text = "Progress Indicators",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(20, 250),
                AutoSize = true
            };
            panel.Controls.Add(progressLabel);
            
            // Progress bars
            ThemedProgressBar progressBar1 = new ThemedProgressBar
            {
                Location = new Point(20, 290),
                Size = new Size(400, 20),
                Value = 75,
                ProgressBarStyle = ThemedProgressBarStyle.Primary,
                ShowPercentage = true
            };
            panel.Controls.Add(progressBar1);
            
            ThemedProgressBar progressBar2 = new ThemedProgressBar
            {
                Location = new Point(20, 330),
                Size = new Size(400, 20),
                Value = 45,
                ProgressBarStyle = ThemedProgressBarStyle.Secondary,
                ShowPercentage = true
            };
            panel.Controls.Add(progressBar2);
            
            ThemedProgressBar progressBar3 = new ThemedProgressBar
            {
                Location = new Point(20, 370),
                Size = new Size(400, 20),
                Value = 90,
                ProgressBarStyle = ThemedProgressBarStyle.Success,
                ShowPercentage = true
            };
            panel.Controls.Add(progressBar3);
            
            return panel;
        }
        
        private ThemedPanel CreateControlsPanel()
        {
            ThemedPanel panel = new ThemedPanel
            {
                Dock = DockStyle.Fill,
                PanelStyle = PanelStyle.Light,
                BorderWidth = 0
            };
            
            // Header
            ThemedLabel headerLabel = new ThemedLabel
            {
                Text = "Controls",
                LabelStyle = LabelStyle.Header,
                Location = new Point(20, 20),
                AutoSize = true
            };
            panel.Controls.Add(headerLabel);
            
            // Form layout
            ThemedLabel formLabel = new ThemedLabel
            {
                Text = "Form Elements",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(20, 60),
                AutoSize = true
            };
            panel.Controls.Add(formLabel);
            
            // Text input
            ThemedTextBox nameTextBox = new ThemedTextBox
            {
                Location = new Point(20, 100),
                Size = new Size(300, 30),
                ShowLabel = true,
                LabelText = "Name:"
            };
            panel.Controls.Add(nameTextBox);
            
            ThemedTextBox emailTextBox = new ThemedTextBox
            {
                Location = new Point(20, 150),
                Size = new Size(300, 30),
                ShowLabel = true,
                LabelText = "Email:"
            };
            panel.Controls.Add(emailTextBox);
            
            // Dropdown
            ThemedComboBox roleComboBox = new ThemedComboBox
            {
                Location = new Point(20, 200),
                Size = new Size(300, 30),
                ShowLabel = true,
                LabelText = "Role:"
            };
            roleComboBox.Items.AddRange(new string[] { "Administrator", "Operator", "User", "Guest" });
            roleComboBox.SelectedIndex = 0;
            panel.Controls.Add(roleComboBox);
            
            // Checkbox options
            ThemedCheckBox checkbox1 = new ThemedCheckBox
            {
                Text = "Send email notifications",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(20, 250),
                Checked = true
            };
            panel.Controls.Add(checkbox1);
            
            ThemedCheckBox checkbox2 = new ThemedCheckBox
            {
                Text = "Enable advanced features",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(20, 280),
                Checked = false
            };
            panel.Controls.Add(checkbox2);
            
            // Radio options
            ThemedLabel optionsLabel = new ThemedLabel
            {
                Text = "Display Options:",
                LabelStyle = LabelStyle.Default,
                Location = new Point(20, 320),
                AutoSize = true
            };
            panel.Controls.Add(optionsLabel);
            
            ThemedRadioButton radio1 = new ThemedRadioButton
            {
                Text = "Compact View",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(20, 350),
                Checked = true
            };
            panel.Controls.Add(radio1);
            
            ThemedRadioButton radio2 = new ThemedRadioButton
            {
                Text = "Detailed View",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(20, 380),
                Checked = false
            };
            panel.Controls.Add(radio2);
            
            ThemedRadioButton radio3 = new ThemedRadioButton
            {
                Text = "Grid View",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(20, 410),
                Checked = false
            };
            panel.Controls.Add(radio3);
            
            // Submit button
            ThemedButton submitButton = new ThemedButton
            {
                Text = "Save Changes",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 460),
                Size = new Size(150, 40)
            };
            panel.Controls.Add(submitButton);
            
            // Cancel button
            ThemedButton cancelButton = new ThemedButton
            {
                Text = "Cancel",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(180, 460),
                Size = new Size(100, 40)
            };
            panel.Controls.Add(cancelButton);
            
            return panel;
        }
        
        private ThemedPanel CreateSettingsPanel()
        {
            ThemedPanel panel = new ThemedPanel
            {
                Dock = DockStyle.Fill,
                PanelStyle = PanelStyle.Light,
                BorderWidth = 0
            };
            
            // Header
            ThemedLabel headerLabel = new ThemedLabel
            {
                Text = "Settings",
                LabelStyle = LabelStyle.Header,
                Location = new Point(20, 20),
                AutoSize = true
            };
            panel.Controls.Add(headerLabel);
            
            // Settings sections
            ThemedPanel generalSection = CreateSettingsSection("General Settings", new Point(20, 80), new Size(400, 200));
            ThemedPanel displaySection = CreateSettingsSection("Display Settings", new Point(20, 300), new Size(400, 200));
            ThemedPanel advancedSection = CreateSettingsSection("Advanced Settings", new Point(440, 80), new Size(400, 420));
            
            panel.Controls.Add(generalSection);
            panel.Controls.Add(displaySection);
            panel.Controls.Add(advancedSection);
            
            return panel;
        }
        
        private ThemedPanel CreateHelpPanel()
        {
            ThemedPanel panel = new ThemedPanel
            {
                Dock = DockStyle.Fill,
                PanelStyle = PanelStyle.Light,
                BorderWidth = 0
            };
            
            // Header
            ThemedLabel headerLabel = new ThemedLabel
            {
                Text = "Help & Support",
                LabelStyle = LabelStyle.Header,
                Location = new Point(20, 20),
                AutoSize = true
            };
            panel.Controls.Add(headerLabel);
            
            // Help content
            ThemedLabel helpLabel = new ThemedLabel
            {
                Text = "Getting Started Guide",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(20, 80),
                AutoSize = true
            };
            panel.Controls.Add(helpLabel);
            
            ThemedLabel helpText = new ThemedLabel
            {
                Text = "This application demonstrates the IGCV.GUI framework with Fraunhofer CI theme.\n" +
                       "Use the navigation buttons on the left to explore different sections.",
                LabelStyle = LabelStyle.Default,
                Location = new Point(20, 110),
                Size = new Size(700, 40)
            };
            panel.Controls.Add(helpText);
            
            // FAQ section
            ThemedLabel faqLabel = new ThemedLabel
            {
                Text = "Frequently Asked Questions",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(20, 180),
                AutoSize = true
            };
            panel.Controls.Add(faqLabel);
            
            // FAQ items
            string[] questions = new string[]
            {
                "How do I apply the theme to my own application?",
                "Can I create custom controls with the theme?",
                "How do I change the theme colors?",
                "Where can I find more documentation?"
            };
            
            string[] answers = new string[]
            {
                "Use ThemeManager.ApplyThemeToContainer(myContainer) to apply the theme to all controls in a container.",
                "Yes, you can create custom controls by implementing the IThemeableControl interface.",
                "You can extend the ThemeBase class to create a custom theme with your own colors.",
                "Check the official documentation in the project repository for more details."
            };
            
            for (int i = 0; i < questions.Length; i++)
            {
                ThemedLabel questionLabel = new ThemedLabel
                {
                    Text = "Q: " + questions[i],
                    LabelStyle = LabelStyle.Secondary,
                    Location = new Point(20, 220 + (i * 70)),
                    AutoSize = true
                };
                panel.Controls.Add(questionLabel);
                
                ThemedLabel answerLabel = new ThemedLabel
                {
                    Text = "A: " + answers[i],
                    LabelStyle = LabelStyle.Default,
                    Location = new Point(40, 245 + (i * 70)),
                    Size = new Size(700, 20)
                };
                panel.Controls.Add(answerLabel);
            }
            
            // Contact info
            ThemedLabel contactLabel = new ThemedLabel
            {
                Text = "Contact Information",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(20, 500),
                AutoSize = true
            };
            panel.Controls.Add(contactLabel);
            
            ThemedLabel contactInfo = new ThemedLabel
            {
                Text = "For support, please contact: support@igcv.fraunhofer.de",
                LabelStyle = LabelStyle.Default,
                Location = new Point(20, 530),
                AutoSize = true
            };
            panel.Controls.Add(contactInfo);
            
            return panel;
        }
        
        private ThemedPanel CreateStatusCard(string title, string value, string colorHex, Point location)
        {
            Color cardColor = ColorTranslator.FromHtml(colorHex);
            
            ThemedPanel card = new ThemedPanel
            {
                Location = location,
                Size = new Size(200, 120),
                CornerRadius = 3,
                BorderWidth = 0,
                BackColor = cardColor
            };
            
            ThemedLabel titleLabel = new ThemedLabel
            {
                Text = title,
                LabelStyle = LabelStyle.Default,
                Location = new Point(15, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            card.Controls.Add(titleLabel);
            
            ThemedLabel valueLabel = new ThemedLabel
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(15, 50),
                AutoSize = true,
                ForeColor = Color.White
            };
            card.Controls.Add(valueLabel);
            
            return card;
        }
        
        private ThemedPanel CreateSettingsSection(string title, Point location, Size size)
        {
            ThemedPanel section = new ThemedPanel
            {
                Location = location,
                Size = size,
                PanelStyle = PanelStyle.Light,
                CornerRadius = 5,
                BorderWidth = 1,
                BorderColor = Color.LightGray
            };
            
            // Section header
            ThemedLabel headerLabel = new ThemedLabel
            {
                Text = title,
                LabelStyle = LabelStyle.Secondary,
                Location = new Point(15, 15),
                AutoSize = true
            };
            section.Controls.Add(headerLabel);
            
            // Separator
            Panel separator = new Panel
            {
                Location = new Point(15, 45),
                Size = new Size(size.Width - 30, 1),
                BackColor = Color.LightGray
            };
            section.Controls.Add(separator);
            
            // Add sample settings based on section title
            if (title.Contains("General"))
            {
                CreateGeneralSettings(section);
            }
            else if (title.Contains("Display"))
            {
                CreateDisplaySettings(section);
            }
            else if (title.Contains("Advanced"))
            {
                CreateAdvancedSettings(section);
            }
            
            return section;
        }
        
        private void CreateGeneralSettings(ThemedPanel panel)
        {
            ThemedCheckBox checkbox1 = new ThemedCheckBox
            {
                Text = "Enable automatic updates",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(15, 60),
                Checked = true
            };
            panel.Controls.Add(checkbox1);
            
            ThemedCheckBox checkbox2 = new ThemedCheckBox
            {
                Text = "Send anonymous usage data",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(15, 90),
                Checked = false
            };
            panel.Controls.Add(checkbox2);
            
            ThemedComboBox languageCombo = new ThemedComboBox
            {
                Location = new Point(15, 130),
                Size = new Size(300, 30),
                ShowLabel = true,
                LabelText = "Language:"
            };
            languageCombo.Items.AddRange(new string[] { "English", "German", "French", "Spanish" });
            languageCombo.SelectedIndex = 0;
            panel.Controls.Add(languageCombo);
        }
        
        private void CreateDisplaySettings(ThemedPanel panel)
        {
            ThemedRadioButton radio1 = new ThemedRadioButton
            {
                Text = "Light Theme",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(15, 60),
                Checked = false
            };
            panel.Controls.Add(radio1);
            
            ThemedRadioButton radio2 = new ThemedRadioButton
            {
                Text = "Dark Theme",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(15, 90),
                Checked = false
            };
            panel.Controls.Add(radio2);
            
            ThemedRadioButton radio3 = new ThemedRadioButton
            {
                Text = "Fraunhofer Theme",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(15, 120),
                Checked = true
            };
            panel.Controls.Add(radio3);
            
            ThemedCheckBox checkbox = new ThemedCheckBox
            {
                Text = "Show tooltips",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(15, 160),
                Checked = true
            };
            panel.Controls.Add(checkbox);
        }
        
        private void CreateAdvancedSettings(ThemedPanel panel)
        {
            ThemedTextBox serverTextBox = new ThemedTextBox
            {
                Location = new Point(15, 60),
                Size = new Size(350, 30),
                ShowLabel = true,
                LabelText = "Server Address:",
                Text = "https://api.fraunhofer.de"
            };
            panel.Controls.Add(serverTextBox);
            
            ThemedTextBox portTextBox = new ThemedTextBox
            {
                Location = new Point(15, 110),
                Size = new Size(150, 30),
                ShowLabel = true,
                LabelText = "Port:",
                Text = "8080"
            };
            panel.Controls.Add(portTextBox);
            
            ThemedComboBox protocolCombo = new ThemedComboBox
            {
                Location = new Point(15, 160),
                Size = new Size(200, 30),
                ShowLabel = true,
                LabelText = "Protocol:"
            };
            protocolCombo.Items.AddRange(new string[] { "HTTP", "HTTPS", "FTP", "Custom" });
            protocolCombo.SelectedIndex = 1;
            panel.Controls.Add(protocolCombo);
            
            ThemedCheckBox checkbox1 = new ThemedCheckBox
            {
                Text = "Enable secure connection",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(15, 210),
                Checked = true
            };
            panel.Controls.Add(checkbox1);
            
            ThemedCheckBox checkbox2 = new ThemedCheckBox
            {
                Text = "Enable debug logging",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(15, 240),
                Checked = false
            };
            panel.Controls.Add(checkbox2);
            
            ThemedCheckBox checkbox3 = new ThemedCheckBox
            {
                Text = "Use custom configuration",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(15, 270),
                Checked = false
            };
            panel.Controls.Add(checkbox3);
            
            ThemedButton testButton = new ThemedButton
            {
                Text = "Test Connection",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(15, 310),
                Size = new Size(150, 40)
            };
            panel.Controls.Add(testButton);
            
            ThemedButton resetButton = new ThemedButton
            {
                Text = "Reset to Defaults",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(175, 310),
                Size = new Size(150, 40)
            };
            panel.Controls.Add(resetButton);
        }
        
        private void ShowContentPanel(int index)
        {
            // Hide all panels
            foreach (var panel in _contentPanels)
            {
                panel.Visible = false;
            }
            
            // Show the selected panel
            if (index >= 0 && index < _contentPanels.Count)
            {
                _contentPanels[index].Visible = true;
                _contentPanels[index].BringToFront();
            }
        }
        
        // Gradient background paint
        private void ThemeIntegrationSample_Paint(object sender, PaintEventArgs e)
        {
            // Apply the Fraunhofer CI theme gradient background
            ThemeManager.ApplyGradientBackground(this, e);
        }
    }
}
