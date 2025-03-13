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
    /// Page for demonstrating input controls
    /// </summary>
    public class InputControlsPage : PageBase
    {
        // Panels for organizing controls
        private Panel _textInputPanel;
        private Panel _dropdownPanel;
        private Panel _selectionPanel;
        private Panel _usageExamplesPanel;
        
        /// <summary>
        /// Creates a new InputControlsPage
        /// </summary>
        public InputControlsPage() 
            : base("Input Controls", "Demonstration of input controls", "Inputs", 
                  CreatePlaceholderImage(),
                  1) // Second page in navigation
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create text input panel
            _textInputPanel = CreateStandardPanel(20, 20, 560, 230);
            
            // Add panel title
            Label textInputTitle = new Label
            {
                Text = "Text Input Controls",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _textInputPanel.Controls.Add(textInputTitle);
            
            // Create text input controls
            CreateTextInputControls();
            
            // Create dropdown panel
            _dropdownPanel = CreateStandardPanel(600, 20, 570, 230);
            
            // Add panel title
            Label dropdownTitle = new Label
            {
                Text = "Dropdown Controls",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _dropdownPanel.Controls.Add(dropdownTitle);
            
            // Create dropdown controls
            CreateDropdownControls();
            
            // Create selection panel
            _selectionPanel = CreateStandardPanel(20, 270, 560, 260);
            
            // Add panel title
            Label selectionTitle = new Label
            {
                Text = "Selection Controls",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _selectionPanel.Controls.Add(selectionTitle);
            
            // Create selection controls
            CreateSelectionControls();
            
            // Create usage examples panel
            _usageExamplesPanel = CreateStandardPanel(600, 270, 570, 260);
            
            // Add panel title
            Label usageExamplesTitle = new Label
            {
                Text = "Input Usage Examples",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _usageExamplesPanel.Controls.Add(usageExamplesTitle);
            
            // Create usage examples
            CreateUsageExamples();
            
            // Add panels to page
            this.Controls.Add(_textInputPanel);
            this.Controls.Add(_dropdownPanel);
            this.Controls.Add(_selectionPanel);
            this.Controls.Add(_usageExamplesPanel);
            
            this.ResumeLayout(false);
        }
        
        private void CreateTextInputControls()
        {
            // Regular text box
            Label regularTextBoxLabel = new Label
            {
                Text = "Regular Text Box:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };
            _textInputPanel.Controls.Add(regularTextBoxLabel);
            
            ThemedTextBox regularTextBox = new ThemedTextBox
            {
                PlaceholderText = "Enter text here...",
                Location = new Point(150, 50),
                Size = new Size(250, 30)
            };
            regularTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            _textInputPanel.Controls.Add(regularTextBox);
            
            // Password text box
            Label passwordTextBoxLabel = new Label
            {
                Text = "Password:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 90)
            };
            _textInputPanel.Controls.Add(passwordTextBoxLabel);
            
            ThemedTextBox passwordTextBox = new ThemedTextBox
            {
                PlaceholderText = "Enter password...",
                PasswordChar = '•',
                Location = new Point(150, 90),
                Size = new Size(250, 30)
            };
            passwordTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            _textInputPanel.Controls.Add(passwordTextBox);
            
            // Disabled text box
            Label disabledTextBoxLabel = new Label
            {
                Text = "Disabled:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 130)
            };
            _textInputPanel.Controls.Add(disabledTextBoxLabel);
            
            ThemedTextBox disabledTextBox = new ThemedTextBox
            {
                Text = "Disabled text box",
                Enabled = false,
                Location = new Point(150, 130),
                Size = new Size(250, 30)
            };
            disabledTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            _textInputPanel.Controls.Add(disabledTextBox);
            
            // Text box with validation
            Label validationTextBoxLabel = new Label
            {
                Text = "With Validation:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 170)
            };
            _textInputPanel.Controls.Add(validationTextBoxLabel);
            
            ThemedTextBox validationTextBox = new ThemedTextBox
            {
                PlaceholderText = "Enter email address...",
                Location = new Point(150, 170),
                Size = new Size(250, 30)
            };
            
            Label validationLabel = new Label
            {
                Text = "Please enter a valid email",
                ForeColor = ThemeManager.CurrentTheme.ErrorColor,
                AutoSize = true,
                Location = new Point(150, 200),
                Visible = false
            };
            _textInputPanel.Controls.Add(validationLabel);
            
            validationTextBox.TextChanged += (s, e) => {
                if (string.IsNullOrWhiteSpace(validationTextBox.Text))
                {
                    validationLabel.Visible = false;
                }
                else if (!validationTextBox.Text.Contains("@") || !validationTextBox.Text.Contains("."))
                {
                    validationLabel.Visible = true;
                }
                else
                {
                    validationLabel.Visible = false;
                }
            };
            
            validationTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            _textInputPanel.Controls.Add(validationTextBox);
        }
        
        private void CreateDropdownControls()
        {
            // Regular combo box
            Label regularComboBoxLabel = new Label
            {
                Text = "Regular ComboBox:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };
            _dropdownPanel.Controls.Add(regularComboBoxLabel);
            
            ThemedComboBox regularComboBox = new ThemedComboBox
            {
                Location = new Point(150, 50),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDown
            };
            regularComboBox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
            regularComboBox.SelectedIndex = 0;
            regularComboBox.ApplyTheme(ThemeManager.CurrentTheme);
            _dropdownPanel.Controls.Add(regularComboBox);
            
            // Dropdown list
            Label dropdownListLabel = new Label
            {
                Text = "Dropdown List:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 90)
            };
            _dropdownPanel.Controls.Add(dropdownListLabel);
            
            ThemedComboBox dropdownList = new ThemedComboBox
            {
                Location = new Point(150, 90),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            dropdownList.Items.AddRange(new object[] { "Option A", "Option B", "Option C", "Option D", "Option E" });
            dropdownList.SelectedIndex = 0;
            dropdownList.ApplyTheme(ThemeManager.CurrentTheme);
            _dropdownPanel.Controls.Add(dropdownList);
            
            // Disabled combo box
            Label disabledComboBoxLabel = new Label
            {
                Text = "Disabled:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 130)
            };
            _dropdownPanel.Controls.Add(disabledComboBoxLabel);
            
            ThemedComboBox disabledComboBox = new ThemedComboBox
            {
                Location = new Point(150, 130),
                Size = new Size(250, 30),
                Enabled = false
            };
            disabledComboBox.Items.AddRange(new object[] { "Disabled Item 1", "Disabled Item 2", "Disabled Item 3" });
            disabledComboBox.SelectedIndex = 0;
            disabledComboBox.ApplyTheme(ThemeManager.CurrentTheme);
            _dropdownPanel.Controls.Add(disabledComboBox);
            
            // ComboBox with label
            Label comboBoxWithLabel = new Label
            {
                Text = "Countries:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 170)
            };
            _dropdownPanel.Controls.Add(comboBoxWithLabel);
            
            ThemedComboBox countriesComboBox = new ThemedComboBox
            {
                Location = new Point(150, 170),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            countriesComboBox.Items.AddRange(new object[] { 
                "Germany", "United States", "France", "United Kingdom", 
                "Japan", "China", "Australia", "Brazil", "Canada", 
                "India", "Italy", "South Korea", "Russia" 
            });
            countriesComboBox.SelectedIndex = 0;
            countriesComboBox.ApplyTheme(ThemeManager.CurrentTheme);
            _dropdownPanel.Controls.Add(countriesComboBox);
            
            // Selected value label
            Label selectedValueLabel = new Label
            {
                Text = "Selected: Germany",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(420, 170)
            };
            _dropdownPanel.Controls.Add(selectedValueLabel);
            
            countriesComboBox.SelectedIndexChanged += (s, e) => {
                selectedValueLabel.Text = $"Selected: {countriesComboBox.SelectedItem}";
            };
        }
        
        private void CreateSelectionControls()
        {
            // Checkboxes
            Label checkboxesLabel = new Label
            {
                Text = "Checkboxes:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };
            _selectionPanel.Controls.Add(checkboxesLabel);
            
            ThemedCheckBox checkbox1 = new ThemedCheckBox
            {
                Text = "Option 1",
                Location = new Point(150, 50),
                AutoSize = true
            };
            checkbox1.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(checkbox1);
            
            ThemedCheckBox checkbox2 = new ThemedCheckBox
            {
                Text = "Option 2",
                Location = new Point(250, 50),
                AutoSize = true,
                Checked = true
            };
            checkbox2.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(checkbox2);
            
            ThemedCheckBox checkbox3 = new ThemedCheckBox
            {
                Text = "Option 3",
                Location = new Point(350, 50),
                AutoSize = true
            };
            checkbox3.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(checkbox3);
            
            ThemedCheckBox disabledCheckbox = new ThemedCheckBox
            {
                Text = "Disabled",
                Location = new Point(450, 50),
                AutoSize = true,
                Enabled = false
            };
            disabledCheckbox.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(disabledCheckbox);
            
            // Radio buttons
            Label radioButtonsLabel = new Label
            {
                Text = "Radio Buttons:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 90)
            };
            _selectionPanel.Controls.Add(radioButtonsLabel);
            
            ThemedRadioButton radioButton1 = new ThemedRadioButton
            {
                Text = "Option A",
                Location = new Point(150, 90),
                AutoSize = true,
                Checked = true
            };
            radioButton1.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(radioButton1);
            
            ThemedRadioButton radioButton2 = new ThemedRadioButton
            {
                Text = "Option B",
                Location = new Point(250, 90),
                AutoSize = true
            };
            radioButton2.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(radioButton2);
            
            ThemedRadioButton radioButton3 = new ThemedRadioButton
            {
                Text = "Option C",
                Location = new Point(350, 90),
                AutoSize = true
            };
            radioButton3.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(radioButton3);
            
            ThemedRadioButton disabledRadioButton = new ThemedRadioButton
            {
                Text = "Disabled",
                Location = new Point(450, 90),
                AutoSize = true,
                Enabled = false
            };
            disabledRadioButton.ApplyTheme(ThemeManager.CurrentTheme);
            _selectionPanel.Controls.Add(disabledRadioButton);
            
            // Grouped radio buttons
            Label groupedRadioButtonsLabel = new Label
            {
                Text = "Radio Groups:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 130)
            };
            _selectionPanel.Controls.Add(groupedRadioButtonsLabel);
            
            // Group 1
            GroupBox group1 = new GroupBox
            {
                Text = "Group 1",
                Location = new Point(150, 130),
                Size = new Size(150, 110),
                ForeColor = Color.White
            };
            _selectionPanel.Controls.Add(group1);
            
            ThemedRadioButton group1Radio1 = new ThemedRadioButton
            {
                Text = "Small",
                Location = new Point(10, 20),
                AutoSize = true,
                Checked = true
            };
            group1Radio1.ApplyTheme(ThemeManager.CurrentTheme);
            group1.Controls.Add(group1Radio1);
            
            ThemedRadioButton group1Radio2 = new ThemedRadioButton
            {
                Text = "Medium",
                Location = new Point(10, 50),
                AutoSize = true
            };
            group1Radio2.ApplyTheme(ThemeManager.CurrentTheme);
            group1.Controls.Add(group1Radio2);
            
            ThemedRadioButton group1Radio3 = new ThemedRadioButton
            {
                Text = "Large",
                Location = new Point(10, 80),
                AutoSize = true
            };
            group1Radio3.ApplyTheme(ThemeManager.CurrentTheme);
            group1.Controls.Add(group1Radio3);
            
            // Group 2
            GroupBox group2 = new GroupBox
            {
                Text = "Group 2",
                Location = new Point(320, 130),
                Size = new Size(190, 110),
                ForeColor = Color.White
            };
            _selectionPanel.Controls.Add(group2);
            
            ThemedRadioButton group2Radio1 = new ThemedRadioButton
            {
                Text = "Red",
                Location = new Point(10, 20),
                AutoSize = true
            };
            group2Radio1.ApplyTheme(ThemeManager.CurrentTheme);
            group2.Controls.Add(group2Radio1);
            
            ThemedRadioButton group2Radio2 = new ThemedRadioButton
            {
                Text = "Green",
                Location = new Point(10, 50),
                AutoSize = true,
                Checked = true
            };
            group2Radio2.ApplyTheme(ThemeManager.CurrentTheme);
            group2.Controls.Add(group2Radio2);
            
            ThemedRadioButton group2Radio3 = new ThemedRadioButton
            {
                Text = "Blue",
                Location = new Point(10, 80),
                AutoSize = true
            };
            group2Radio3.ApplyTheme(ThemeManager.CurrentTheme);
            group2.Controls.Add(group2Radio3);
        }
        
        private void CreateUsageExamples()
        {
            // Create a search example
            Panel searchPanel = new ThemedPanel
            {
                Location = new Point(20, 50),
                Size = new Size(530, 60),
                CornerRadius = 5
            };
            ((ThemedPanel)searchPanel).ApplyTheme(ThemeManager.CurrentTheme);
            
            ThemedTextBox searchBox = new ThemedTextBox
            {
                PlaceholderText = "Search...",
                Location = new Point(10, 15),
                Size = new Size(400, 30)
            };
            searchBox.ApplyTheme(ThemeManager.CurrentTheme);
            searchPanel.Controls.Add(searchBox);
            
            ThemedButton searchButton = new ThemedButton
            {
                Text = "Search",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(420, 15),
                Size = new Size(100, 30)
            };
            searchButton.ApplyTheme(ThemeManager.CurrentTheme);
            searchPanel.Controls.Add(searchButton);
            
            _usageExamplesPanel.Controls.Add(searchPanel);
            
            // Create a filter example
            Panel filterPanel = new ThemedPanel
            {
                Location = new Point(20, 120),
                Size = new Size(530, 60),
                CornerRadius = 5
            };
            ((ThemedPanel)filterPanel).ApplyTheme(ThemeManager.CurrentTheme);
            
            Label filterLabel = new Label
            {
                Text = "Filter by:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 20)
            };
            filterPanel.Controls.Add(filterLabel);
            
            ThemedComboBox filterComboBox = new ThemedComboBox
            {
                Location = new Point(70, 15),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            filterComboBox.Items.AddRange(new object[] { "All Items", "Category A", "Category B", "Category C" });
            filterComboBox.SelectedIndex = 0;
            filterComboBox.ApplyTheme(ThemeManager.CurrentTheme);
            filterPanel.Controls.Add(filterComboBox);
            
            Label dateLabel = new Label
            {
                Text = "Date:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(230, 20)
            };
            filterPanel.Controls.Add(dateLabel);
            
            ThemedComboBox dateComboBox = new ThemedComboBox
            {
                Location = new Point(270, 15),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            dateComboBox.Items.AddRange(new object[] { "All Time", "Today", "This Week", "This Month", "This Year" });
            dateComboBox.SelectedIndex = 0;
            dateComboBox.ApplyTheme(ThemeManager.CurrentTheme);
            filterPanel.Controls.Add(dateComboBox);
            
            ThemedButton applyButton = new ThemedButton
            {
                Text = "Apply",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(430, 15),
                Size = new Size(90, 30)
            };
            applyButton.ApplyTheme(ThemeManager.CurrentTheme);
            filterPanel.Controls.Add(applyButton);
            
            _usageExamplesPanel.Controls.Add(filterPanel);
            
            // Create a settings example
            Panel settingsPanel = new ThemedPanel
            {
                Location = new Point(20, 190),
                Size = new Size(530, 60),
                CornerRadius = 5
            };
            ((ThemedPanel)settingsPanel).ApplyTheme(ThemeManager.CurrentTheme);
            
            ThemedCheckBox notificationCheckbox = new ThemedCheckBox
            {
                Text = "Enable notifications",
                Location = new Point(10, 20),
                AutoSize = true,
                Checked = true
            };
            notificationCheckbox.ApplyTheme(ThemeManager.CurrentTheme);
            settingsPanel.Controls.Add(notificationCheckbox);
            
            ThemedCheckBox darkModeCheckbox = new ThemedCheckBox
            {
                Text = "Dark mode",
                Location = new Point(180, 20),
                AutoSize = true
            };
            darkModeCheckbox.ApplyTheme(ThemeManager.CurrentTheme);
            settingsPanel.Controls.Add(darkModeCheckbox);
            
            ThemedCheckBox autoSaveCheckbox = new ThemedCheckBox
            {
                Text = "Auto-save",
                Location = new Point(300, 20),
                AutoSize = true,
                Checked = true
            };
            autoSaveCheckbox.ApplyTheme(ThemeManager.CurrentTheme);
            settingsPanel.Controls.Add(autoSaveCheckbox);
            
            ThemedButton saveSettingsButton = new ThemedButton
            {
                Text = "Save",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(430, 15),
                Size = new Size(90, 30)
            };
            saveSettingsButton.ApplyTheme(ThemeManager.CurrentTheme);
            settingsPanel.Controls.Add(saveSettingsButton);
            
            _usageExamplesPanel.Controls.Add(settingsPanel);
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
