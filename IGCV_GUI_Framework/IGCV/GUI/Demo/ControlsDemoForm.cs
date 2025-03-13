using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV.GUI.Themes.FraunhoferCI;
using ThemedStyles = IGCV.GUI.Controls; // Alias to avoid ambiguity

namespace IGCV.GUI.Demo
{
    /// <summary>
    /// Demo form to showcase IGCV.GUI themed controls with Fraunhofer CI
    /// </summary>
    public class ControlsDemoForm : Form
    {
        private System.Windows.Forms.Timer _demoTimer;
        private ThemedProgressBar _demoProgressBar;
        
        public ControlsDemoForm()
        {
            // Enable double buffering to reduce flickering
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            
            // Initialize the form
            InitializeComponent();
            
            // Subscribe to the theme changed event
            ThemeManager.ThemeChanged += (s, e) => ThemeManager.ApplyThemeToContainer(this);
            
            // Apply theme to the form
            ThemeManager.ApplyThemeToContainer(this);
            
            // Start the demo timer
            _demoTimer.Start();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Text = "IGCV.GUI Controls Demo - Fraunhofer Theme";
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.BackColor = Color.White;
            
            // Add paint event for gradient background
            this.Paint += ControlsDemoForm_Paint;
            
            // Create form layout
            CreateDemoLayout();
            
            // Initialize demo timer for progress bar animation
            _demoTimer = new System.Windows.Forms.Timer();
            _demoTimer.Interval = 50;
            _demoTimer.Tick += DemoTimer_Tick;
            
            this.ResumeLayout(false);
        }
        
        private void CreateDemoLayout()
        {
            // Main content panel
            ThemedPanel mainPanel = new ThemedPanel
            {
                Location = new Point(20, 20),
                Size = new Size(Width - 40, Height - 40),
                PanelStyle = PanelStyle.Light,
                CornerRadius = 5,
                BorderWidth = 1
            };
            Controls.Add(mainPanel);
            
            // Header
            ThemedLabel headerLabel = new ThemedLabel
            {
                Text = "Fraunhofer Corporate Design Component Library",
                LabelStyle = LabelStyle.Header,
                Location = new Point(20, 20),
                AutoSize = true
            };
            mainPanel.Controls.Add(headerLabel);
            
            ThemedLabel subHeaderLabel = new ThemedLabel
            {
                Text = "IGCV.GUI Framework Controls Demo",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(20, 60),
                AutoSize = true
            };
            mainPanel.Controls.Add(subHeaderLabel);
            
            // Horizontal separator
            Panel separator = new Panel
            {
                Location = new Point(20, 100),
                Size = new Size(mainPanel.Width - 40, 1),
                BackColor = Color.LightGray
            };
            mainPanel.Controls.Add(separator);
            
            // Create categorized sections
            CreateButtonsSection(mainPanel, 20, 120);
            CreateLabelsSection(mainPanel, 20, 290);
            CreateInputControlsSection(mainPanel, 20, 460);
            CreateOtherControlsSection(mainPanel, mainPanel.Width / 2 + 10, 120);
            
            // Footer with info
            ThemedLabel footerLabel = new ThemedLabel
            {
                Text = "IGCV GUI Toolkit - Fraunhofer Design",
                LabelStyle = LabelStyle.Default,
                Location = new Point(mainPanel.Width - 260, mainPanel.Height - 40),
                AutoSize = true
            };
            mainPanel.Controls.Add(footerLabel);
        }
        
        private void CreateButtonsSection(Panel parent, int x, int y)
        {
            // Section header
            ThemedLabel sectionLabel = new ThemedLabel
            {
                Text = "Button Controls",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(sectionLabel);
            
            // Primary buttons
            ThemedButton primaryButton = new ThemedButton
            {
                Text = "Primary Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(x, y + 30),
                Size = new Size(150, 40)
            };
            parent.Controls.Add(primaryButton);
            
            ThemedButton primaryButtonSmall = new ThemedButton
            {
                Text = "Primary Small",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(x + 170, y + 30),
                Size = new Size(120, 30)
            };
            parent.Controls.Add(primaryButtonSmall);
            
            // Secondary buttons
            ThemedButton secondaryButton = new ThemedButton
            {
                Text = "Secondary Button",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(x, y + 80),
                Size = new Size(150, 40)
            };
            parent.Controls.Add(secondaryButton);
            
            ThemedButton secondaryButtonSmall = new ThemedButton
            {
                Text = "Secondary Small",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(x + 170, y + 80),
                Size = new Size(120, 30)
            };
            parent.Controls.Add(secondaryButtonSmall);
            
            // Tertiary buttons
            ThemedButton tertiaryButton = new ThemedButton
            {
                Text = "Tertiary Button",
                ButtonStyle = ButtonStyle.Tertiary,
                Location = new Point(x, y + 130),
                Size = new Size(150, 40)
            };
            parent.Controls.Add(tertiaryButton);
            
            ThemedButton tertiaryButtonSmall = new ThemedButton
            {
                Text = "Tertiary Small",
                ButtonStyle = ButtonStyle.Tertiary,
                Location = new Point(x + 170, y + 130),
                Size = new Size(120, 30)
            };
            parent.Controls.Add(tertiaryButtonSmall);
        }
        
        private void CreateLabelsSection(Panel parent, int x, int y)
        {
            // Section header
            ThemedLabel sectionLabel = new ThemedLabel
            {
                Text = "Label Controls",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(sectionLabel);
            
            // Header label
            ThemedLabel headerLabel = new ThemedLabel
            {
                Text = "Header Style Label",
                LabelStyle = LabelStyle.Header,
                Location = new Point(x, y + 30),
                AutoSize = true
            };
            parent.Controls.Add(headerLabel);
            
            // Subheader label
            ThemedLabel subHeaderLabel = new ThemedLabel
            {
                Text = "Subheader Style Label",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(x, y + 70),
                AutoSize = true
            };
            parent.Controls.Add(subHeaderLabel);
            
            // Primary label
            ThemedLabel primaryLabel = new ThemedLabel
            {
                Text = "Primary Style Label",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(x, y + 100),
                AutoSize = true
            };
            parent.Controls.Add(primaryLabel);
            
            // Secondary label
            ThemedLabel secondaryLabel = new ThemedLabel
            {
                Text = "Secondary Style Label",
                LabelStyle = LabelStyle.Secondary,
                Location = new Point(x, y + 125),
                AutoSize = true
            };
            parent.Controls.Add(secondaryLabel);
            
            // Accent label
            ThemedLabel accentLabel = new ThemedLabel
            {
                Text = "Accent Style Label",
                LabelStyle = LabelStyle.Accent,
                Location = new Point(x + 200, y + 30),
                AutoSize = true
            };
            parent.Controls.Add(accentLabel);
            
            // Inverted label
            ThemedLabel invertedLabel = new ThemedLabel
            {
                Text = "Inverted Style Label",
                LabelStyle = LabelStyle.Inverted,
                Location = new Point(x + 200, y + 55),
                AutoSize = true
            };
            parent.Controls.Add(invertedLabel);
            
            // Success label
            ThemedLabel successLabel = new ThemedLabel
            {
                Text = "Success Style Label",
                LabelStyle = LabelStyle.Success,
                Location = new Point(x + 200, y + 90),
                AutoSize = true
            };
            parent.Controls.Add(successLabel);
            
            // Warning label
            ThemedLabel warningLabel = new ThemedLabel
            {
                Text = "Warning Style Label",
                LabelStyle = LabelStyle.Warning,
                Location = new Point(x + 200, y + 115),
                AutoSize = true
            };
            parent.Controls.Add(warningLabel);
            
            // Error label
            ThemedLabel errorLabel = new ThemedLabel
            {
                Text = "Error Style Label",
                LabelStyle = LabelStyle.Error,
                Location = new Point(x + 200, y + 140),
                AutoSize = true
            };
            parent.Controls.Add(errorLabel);
        }
        
        private void CreateInputControlsSection(Panel parent, int x, int y)
        {
            // Section header
            ThemedLabel sectionLabel = new ThemedLabel
            {
                Text = "Input Controls",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(sectionLabel);
            
            // Text box
            ThemedTextBox textBox = new ThemedTextBox
            {
                Location = new Point(x, y + 30),
                Size = new Size(250, 30),
                Text = "Basic Text Box",
                ShowLabel = false
            };
            parent.Controls.Add(textBox);
            
            // Text box with label
            ThemedTextBox labeledTextBox = new ThemedTextBox
            {
                Location = new Point(x, y + 70),
                Size = new Size(250, 30),
                Text = "Text Box with Label",
                ShowLabel = true,
                LabelText = "Input Label:"
            };
            parent.Controls.Add(labeledTextBox);
            
            // Check boxes
            ThemedCheckBox defaultCheckBox = new ThemedCheckBox
            {
                Text = "Default Check Box",
                CheckBoxStyle = CheckBoxStyle.Default,
                Location = new Point(x, y + 120),
                Checked = true
            };
            parent.Controls.Add(defaultCheckBox);
            
            ThemedCheckBox primaryCheckBox = new ThemedCheckBox
            {
                Text = "Primary Check Box",
                CheckBoxStyle = CheckBoxStyle.Primary,
                Location = new Point(x, y + 145),
                Checked = true
            };
            parent.Controls.Add(primaryCheckBox);
            
            ThemedCheckBox secondaryCheckBox = new ThemedCheckBox
            {
                Text = "Secondary Check Box",
                CheckBoxStyle = CheckBoxStyle.Secondary,
                Location = new Point(x, y + 170),
                Checked = true
            };
            parent.Controls.Add(secondaryCheckBox);
            
            ThemedCheckBox accentCheckBox = new ThemedCheckBox
            {
                Text = "Accent Check Box",
                CheckBoxStyle = CheckBoxStyle.Accent,
                Location = new Point(x, y + 195),
                Checked = true
            };
            parent.Controls.Add(accentCheckBox);
            
            ThemedCheckBox filledCheckBox = new ThemedCheckBox
            {
                Text = "Filled Check Box",
                CheckBoxStyle = CheckBoxStyle.Filled,
                Location = new Point(x, y + 220),
                Checked = true
            };
            parent.Controls.Add(filledCheckBox);
            
            // Radio buttons
            ThemedRadioButton defaultRadioButton = new ThemedRadioButton
            {
                Text = "Default Radio Button",
                RadioButtonStyle = RadioButtonStyle.Default,
                Location = new Point(x + 200, y + 30),
                Checked = true
            };
            parent.Controls.Add(defaultRadioButton);
            
            ThemedRadioButton primaryRadioButton = new ThemedRadioButton
            {
                Text = "Primary Radio Button",
                RadioButtonStyle = RadioButtonStyle.Primary,
                Location = new Point(x + 200, y + 55),
                Checked = false
            };
            parent.Controls.Add(primaryRadioButton);
            
            ThemedRadioButton secondaryRadioButton = new ThemedRadioButton
            {
                Text = "Secondary Radio Button",
                RadioButtonStyle = RadioButtonStyle.Secondary,
                Location = new Point(x + 200, y + 80),
                Checked = false
            };
            parent.Controls.Add(secondaryRadioButton);
            
            ThemedRadioButton accentRadioButton = new ThemedRadioButton
            {
                Text = "Accent Radio Button",
                RadioButtonStyle = RadioButtonStyle.Accent,
                Location = new Point(x + 200, y + 105),
                Checked = false
            };
            parent.Controls.Add(accentRadioButton);
            
            ThemedRadioButton filledRadioButton = new ThemedRadioButton
            {
                Text = "Filled Radio Button",
                RadioButtonStyle = RadioButtonStyle.Filled,
                Location = new Point(x + 200, y + 130),
                Checked = false
            };
            parent.Controls.Add(filledRadioButton);
        }
        
        private void CreateOtherControlsSection(Panel parent, int x, int y)
        {
            // Section header
            ThemedLabel sectionLabel = new ThemedLabel
            {
                Text = "Other Controls",
                LabelStyle = LabelStyle.Primary,
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(sectionLabel);
            
            // Panels
            ThemedLabel panelsLabel = new ThemedLabel
            {
                Text = "Panel Controls:",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(x, y + 30),
                AutoSize = true
            };
            parent.Controls.Add(panelsLabel);
            
            ThemedPanel defaultPanel = new ThemedPanel
            {
                Location = new Point(x, y + 60),
                Size = new Size(150, 80),
                PanelStyle = PanelStyle.Default
            };
            parent.Controls.Add(defaultPanel);
            defaultPanel.Controls.Add(new Label 
            { 
                Text = "Default Panel", 
                AutoSize = true, 
                Location = new Point(10, 10),
                ForeColor = Color.White
            });
            
            ThemedPanel primaryPanel = new ThemedPanel
            {
                Location = new Point(x + 170, y + 60),
                Size = new Size(150, 80),
                PanelStyle = PanelStyle.Primary
            };
            parent.Controls.Add(primaryPanel);
            primaryPanel.Controls.Add(new Label 
            { 
                Text = "Primary Panel", 
                AutoSize = true, 
                Location = new Point(10, 10),
                ForeColor = Color.White
            });
            
            ThemedPanel secondaryPanel = new ThemedPanel
            {
                Location = new Point(x, y + 150),
                Size = new Size(150, 80),
                PanelStyle = PanelStyle.Secondary
            };
            parent.Controls.Add(secondaryPanel);
            secondaryPanel.Controls.Add(new Label 
            { 
                Text = "Secondary Panel", 
                AutoSize = true, 
                Location = new Point(10, 10),
                ForeColor = Color.White
            });
            
            ThemedPanel accentPanel = new ThemedPanel
            {
                Location = new Point(x + 170, y + 150),
                Size = new Size(150, 80),
                PanelStyle = PanelStyle.Accent
            };
            parent.Controls.Add(accentPanel);
            accentPanel.Controls.Add(new Label 
            { 
                Text = "Accent Panel", 
                AutoSize = true, 
                Location = new Point(10, 10),
                ForeColor = Color.White
            });
            
            ThemedPanel gradientPanel = new ThemedPanel
            {
                Location = new Point(x + 85, y + 240),
                Size = new Size(150, 80),
                PanelStyle = PanelStyle.Gradient
            };
            parent.Controls.Add(gradientPanel);
            gradientPanel.Controls.Add(new Label 
            { 
                Text = "Gradient Panel", 
                AutoSize = true, 
                Location = new Point(10, 10),
                ForeColor = Color.White
            });
            
            // Combo Box
            ThemedLabel comboLabel = new ThemedLabel
            {
                Text = "Combo Box Controls:",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(x, y + 340),
                AutoSize = true
            };
            parent.Controls.Add(comboLabel);
            
            ThemedComboBox comboBox = new ThemedComboBox
            {
                Location = new Point(x, y + 370),
                Size = new Size(200, 30),
                ShowLabel = false
            };
            comboBox.Items.AddRange(new string[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
            comboBox.SelectedIndex = 0;
            parent.Controls.Add(comboBox);
            
            ThemedComboBox labeledComboBox = new ThemedComboBox
            {
                Location = new Point(x + 220, y + 370),
                Size = new Size(200, 30),
                ShowLabel = true,
                LabelText = "Select an option:"
            };
            labeledComboBox.Items.AddRange(new string[] { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" });
            labeledComboBox.SelectedIndex = 0;
            parent.Controls.Add(labeledComboBox);
            
            // Progress Bars
            ThemedLabel progressLabel = new ThemedLabel
            {
                Text = "Progress Bar Controls:",
                LabelStyle = LabelStyle.SubHeader,
                Location = new Point(x, y + 420),
                AutoSize = true
            };
            parent.Controls.Add(progressLabel);
            
            ThemedProgressBar defaultProgressBar = new ThemedProgressBar
            {
                Location = new Point(x, y + 450),
                Size = new Size(200, 20),
                Value = 75,
                ProgressBarStyle = IGCV.GUI.Controls.ThemedProgressBarStyle.Default
            };
            parent.Controls.Add(defaultProgressBar);
            
            ThemedProgressBar primaryProgressBar = new ThemedProgressBar
            {
                Location = new Point(x, y + 480),
                Size = new Size(200, 20),
                Value = 60,
                ProgressBarStyle = IGCV.GUI.Controls.ThemedProgressBarStyle.Primary
            };
            parent.Controls.Add(primaryProgressBar);
            
            ThemedProgressBar secondaryProgressBar = new ThemedProgressBar
            {
                Location = new Point(x, y + 510),
                Size = new Size(200, 20),
                Value = 45,
                ProgressBarStyle = IGCV.GUI.Controls.ThemedProgressBarStyle.Secondary
            };
            parent.Controls.Add(secondaryProgressBar);
            
            ThemedProgressBar accentProgressBar = new ThemedProgressBar
            {
                Location = new Point(x + 220, y + 450),
                Size = new Size(200, 20),
                Value = 80,
                ProgressBarStyle = IGCV.GUI.Controls.ThemedProgressBarStyle.Accent
            };
            parent.Controls.Add(accentProgressBar);
            
            ThemedProgressBar successProgressBar = new ThemedProgressBar
            {
                Location = new Point(x + 220, y + 480),
                Size = new Size(200, 20),
                Value = 90,
                ProgressBarStyle = IGCV.GUI.Controls.ThemedProgressBarStyle.Success
            };
            parent.Controls.Add(successProgressBar);
            
            ThemedProgressBar gradientProgressBar = new ThemedProgressBar
            {
                Location = new Point(x + 220, y + 510),
                Size = new Size(200, 20),
                Value = 60,
                ProgressBarStyle = IGCV.GUI.Controls.ThemedProgressBarStyle.Gradient,
                ShowPercentage = true
            };
            parent.Controls.Add(gradientProgressBar);
            
            // Animated progress bar for demo
            _demoProgressBar = new ThemedProgressBar
            {
                Location = new Point(x, y + 550),
                Size = new Size(420, 25),
                Value = 0,
                ProgressBarStyle = IGCV.GUI.Controls.ThemedProgressBarStyle.Gradient,
                ShowPercentage = true
            };
            parent.Controls.Add(_demoProgressBar);
        }
        
        // Gradient background paint
        private void ControlsDemoForm_Paint(object sender, PaintEventArgs e)
        {
            // Apply the Fraunhofer CI theme gradient background
            ThemeManager.ApplyGradientBackground(this, e);
        }
        
        // Demo timer for animated progress bar
        private void DemoTimer_Tick(object sender, EventArgs e)
        {
            if (_demoProgressBar != null)
            {
                // Increment value and reset when reaching maximum
                _demoProgressBar.Value = (_demoProgressBar.Value + 1) % (_demoProgressBar.Maximum + 1);
                
                // If zero, pause briefly
                if (_demoProgressBar.Value == 0)
                {
                    _demoTimer.Interval = 500;
                }
                else
                {
                    _demoTimer.Interval = 50;
                }
            }
        }
    }
}
