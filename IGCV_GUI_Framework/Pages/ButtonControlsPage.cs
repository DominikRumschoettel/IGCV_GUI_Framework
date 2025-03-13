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
    /// Page for demonstrating button controls
    /// </summary>
    public class ButtonControlsPage : PageBase
    {
        // Panels for organizing controls
        private Panel _primaryButtonsPanel;
        private Panel _secondaryButtonsPanel;
        private Panel _specialButtonsPanel;
        
        /// <summary>
        /// Creates a new ButtonControlsPage
        /// </summary>
        public ButtonControlsPage() 
            : base("Button Controls", "Demonstration of button controls", "Buttons", 
                  CreatePlaceholderImage(),
                  0) // First page in navigation
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create primary buttons panel
            _primaryButtonsPanel = CreateStandardPanel(20, 20, 370, 260);
            
            // Add panel title
            Label primaryButtonsTitle = new Label
            {
                Text = "Primary Buttons",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _primaryButtonsPanel.Controls.Add(primaryButtonsTitle);
            
            // Create primary buttons
            CreatePrimaryButtons();
            
            // Create secondary buttons panel
            _secondaryButtonsPanel = CreateStandardPanel(410, 20, 370, 260);
            
            // Add panel title
            Label secondaryButtonsTitle = new Label
            {
                Text = "Secondary Buttons",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _secondaryButtonsPanel.Controls.Add(secondaryButtonsTitle);
            
            // Create secondary buttons
            CreateSecondaryButtons();
            
            // Create special buttons panel
            _specialButtonsPanel = CreateStandardPanel(800, 20, 370, 260);
            
            // Add panel title
            Label specialButtonsTitle = new Label
            {
                Text = "Special Buttons",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _specialButtonsPanel.Controls.Add(specialButtonsTitle);
            
            // Create special buttons
            CreateSpecialButtons();
            
            // Second row of content - Button usage examples
            Panel usageExamplesPanel = CreateStandardPanel(20, 300, 1150, 230);
            
            // Add panel title
            Label usageExamplesTitle = new Label
            {
                Text = "Button Usage Examples",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            usageExamplesPanel.Controls.Add(usageExamplesTitle);
            
            // Create usage examples
            CreateButtonUsageExamples(usageExamplesPanel);
            
            // Add panels to page
            this.Controls.Add(_primaryButtonsPanel);
            this.Controls.Add(_secondaryButtonsPanel);
            this.Controls.Add(_specialButtonsPanel);
            this.Controls.Add(usageExamplesPanel);
            
            this.ResumeLayout(false);
        }
        
        private void CreatePrimaryButtons()
        {
            // Create regular primary button
            ThemedButton primaryButton = new ThemedButton
            {
                Text = "Primary Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 60),
                Size = new Size(150, 40)
            };
            primaryButton.Click += (s, e) => MessageBox.Show("Primary button clicked!", "Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            primaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _primaryButtonsPanel.Controls.Add(primaryButton);
            
            // Create large primary button
            ThemedButton largePrimaryButton = new ThemedButton
            {
                Text = "Large Primary Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 110),
                Size = new Size(200, 50),
                Font = new Font(ThemeManager.CurrentTheme.ButtonFont.FontFamily, 12f, FontStyle.Bold)
            };
            largePrimaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _primaryButtonsPanel.Controls.Add(largePrimaryButton);
            
            // Create small primary button
            ThemedButton smallPrimaryButton = new ThemedButton
            {
                Text = "Small",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(230, 60),
                Size = new Size(100, 30)
            };
            smallPrimaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _primaryButtonsPanel.Controls.Add(smallPrimaryButton);
            
            // Create disabled primary button
            ThemedButton disabledPrimaryButton = new ThemedButton
            {
                Text = "Disabled Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 170),
                Size = new Size(150, 40),
                Enabled = false
            };
            disabledPrimaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _primaryButtonsPanel.Controls.Add(disabledPrimaryButton);
            
            // Create icon primary button
            ThemedButton iconPrimaryButton = new ThemedButton
            {
                Text = "✓ With Icon",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 170),
                Size = new Size(150, 40)
            };
            iconPrimaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _primaryButtonsPanel.Controls.Add(iconPrimaryButton);
        }
        
        private void CreateSecondaryButtons()
        {
            // Create regular secondary button
            ThemedButton secondaryButton = new ThemedButton
            {
                Text = "Secondary Button",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(20, 60),
                Size = new Size(150, 40)
            };
            secondaryButton.Click += (s, e) => MessageBox.Show("Secondary button clicked!", "Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            secondaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _secondaryButtonsPanel.Controls.Add(secondaryButton);
            
            // Create large secondary button
            ThemedButton largeSecondaryButton = new ThemedButton
            {
                Text = "Large Secondary Button",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(20, 110),
                Size = new Size(200, 50),
                Font = new Font(ThemeManager.CurrentTheme.ButtonFont.FontFamily, 12f, FontStyle.Regular)
            };
            largeSecondaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _secondaryButtonsPanel.Controls.Add(largeSecondaryButton);
            
            // Create small secondary button
            ThemedButton smallSecondaryButton = new ThemedButton
            {
                Text = "Small",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(230, 60),
                Size = new Size(100, 30)
            };
            smallSecondaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _secondaryButtonsPanel.Controls.Add(smallSecondaryButton);
            
            // Create disabled secondary button
            ThemedButton disabledSecondaryButton = new ThemedButton
            {
                Text = "Disabled Button",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(20, 170),
                Size = new Size(150, 40),
                Enabled = false
            };
            disabledSecondaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _secondaryButtonsPanel.Controls.Add(disabledSecondaryButton);
            
            // Create icon secondary button
            ThemedButton iconSecondaryButton = new ThemedButton
            {
                Text = "✓ With Icon",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(180, 170),
                Size = new Size(150, 40)
            };
            iconSecondaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _secondaryButtonsPanel.Controls.Add(iconSecondaryButton);
        }
        
        private void CreateSpecialButtons()
        {
            // Create tertiary button
            ThemedButton tertiaryButton = new ThemedButton
            {
                Text = "Tertiary Button",
                ButtonStyle = ButtonStyle.Tertiary,
                Location = new Point(20, 60),
                Size = new Size(150, 40)
            };
            tertiaryButton.Click += (s, e) => MessageBox.Show("Tertiary button clicked!", "Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tertiaryButton.ApplyTheme(ThemeManager.CurrentTheme);
            _specialButtonsPanel.Controls.Add(tertiaryButton);
            
            // Create danger button
            ThemedButton dangerButton = new ThemedButton
            {
                Text = "Danger Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 110),
                Size = new Size(150, 40),
                BackColor = ThemeManager.CurrentTheme.ErrorColor
            };
            dangerButton.ApplyTheme(ThemeManager.CurrentTheme);
            _specialButtonsPanel.Controls.Add(dangerButton);
            
            // Create success button
            ThemedButton successButton = new ThemedButton
            {
                Text = "Success Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 160),
                Size = new Size(150, 40),
                BackColor = ThemeManager.CurrentTheme.SuccessColor
            };
            successButton.ApplyTheme(ThemeManager.CurrentTheme);
            _specialButtonsPanel.Controls.Add(successButton);
            
            // Create warning button
            ThemedButton warningButton = new ThemedButton
            {
                Text = "Warning Button",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(20, 210),
                Size = new Size(150, 40),
                BackColor = ThemeManager.CurrentTheme.WarningColor
            };
            warningButton.ApplyTheme(ThemeManager.CurrentTheme);
            _specialButtonsPanel.Controls.Add(warningButton);
            
            // Create custom colored button
            ThemedButton customButton = new ThemedButton
            {
                Text = "Custom Color",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 60),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(138, 43, 226) // BlueViolet
            };
            customButton.ApplyTheme(ThemeManager.CurrentTheme);
            _specialButtonsPanel.Controls.Add(customButton);
            
            // Create rounded corners button
            ThemedButton roundedButton = new ThemedButton
            {
                Text = "Rounded Corners",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(180, 110),
                Size = new Size(150, 40),
                CornerRadius = 20
            };
            roundedButton.ApplyTheme(ThemeManager.CurrentTheme);
            _specialButtonsPanel.Controls.Add(roundedButton);
        }
        
        private void CreateButtonUsageExamples(Panel parent)
        {
            // Create a form submission example
            ThemedPanel formPanel = new ThemedPanel
            {
                Location = new Point(20, 50),
                Size = new Size(350, 160),
                CornerRadius = 5
            };
            formPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add form title
            Label formTitle = new Label
            {
                Text = "Form Submission Example",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            formPanel.Controls.Add(formTitle);
            
            // Add form fields
            ThemedTextBox nameTextBox = new ThemedTextBox
            {
                PlaceholderText = "Enter your name",
                Location = new Point(15, 50),
                Size = new Size(320, 30)
            };
            nameTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            formPanel.Controls.Add(nameTextBox);
            
            ThemedTextBox emailTextBox = new ThemedTextBox
            {
                PlaceholderText = "Enter your email",
                Location = new Point(15, 90),
                Size = new Size(320, 30)
            };
            emailTextBox.ApplyTheme(ThemeManager.CurrentTheme);
            formPanel.Controls.Add(emailTextBox);
            
            // Add form buttons
            ThemedButton submitButton = new ThemedButton
            {
                Text = "Submit",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(15, 130),
                Size = new Size(100, 35)
            };
            submitButton.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(emailTextBox.Text))
                {
                    MessageBox.Show("Please fill in all fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Form submitted for {nameTextBox.Text}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nameTextBox.Text = "";
                    emailTextBox.Text = "";
                }
            };
            submitButton.ApplyTheme(ThemeManager.CurrentTheme);
            formPanel.Controls.Add(submitButton);
            
            ThemedButton resetButton = new ThemedButton
            {
                Text = "Reset",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(125, 130),
                Size = new Size(100, 35)
            };
            resetButton.Click += (s, e) => {
                nameTextBox.Text = "";
                emailTextBox.Text = "";
            };
            resetButton.ApplyTheme(ThemeManager.CurrentTheme);
            formPanel.Controls.Add(resetButton);
            
            // Add to parent panel
            parent.Controls.Add(formPanel);
            
            // Create confirmation dialog example
            ThemedPanel dialogPanel = new ThemedPanel
            {
                Location = new Point(390, 50),
                Size = new Size(350, 160),
                CornerRadius = 5
            };
            dialogPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add dialog title
            Label dialogTitle = new Label
            {
                Text = "Confirmation Dialog Example",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            dialogPanel.Controls.Add(dialogTitle);
            
            // Add dialog content
            Label dialogContent = new Label
            {
                Text = "This example demonstrates how buttons can be used\nin dialog boxes for user confirmations.",
                Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 45)
            };
            dialogPanel.Controls.Add(dialogContent);
            
            // Add dialog buttons
            ThemedButton confirmButton = new ThemedButton
            {
                Text = "Delete Item",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(15, 100),
                Size = new Size(150, 40),
                BackColor = ThemeManager.CurrentTheme.ErrorColor
            };
            confirmButton.Click += (s, e) => {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this item?\nThis action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            confirmButton.ApplyTheme(ThemeManager.CurrentTheme);
            dialogPanel.Controls.Add(confirmButton);
            
            ThemedButton cancelButton = new ThemedButton
            {
                Text = "Cancel",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(175, 100),
                Size = new Size(150, 40)
            };
            cancelButton.ApplyTheme(ThemeManager.CurrentTheme);
            dialogPanel.Controls.Add(cancelButton);
            
            // Add to parent panel
            parent.Controls.Add(dialogPanel);
            
            // Create action buttons example
            ThemedPanel actionsPanel = new ThemedPanel
            {
                Location = new Point(760, 50),
                Size = new Size(350, 160),
                CornerRadius = 5
            };
            actionsPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add actions title
            Label actionsTitle = new Label
            {
                Text = "Action Buttons Example",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            actionsPanel.Controls.Add(actionsTitle);
            
            // Add action buttons
            ThemedButton saveButton = new ThemedButton
            {
                Text = "✓ Save",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(15, 60),
                Size = new Size(100, 35),
                BackColor = ThemeManager.CurrentTheme.SuccessColor
            };
            saveButton.ApplyTheme(ThemeManager.CurrentTheme);
            actionsPanel.Controls.Add(saveButton);
            
            ThemedButton editButton = new ThemedButton
            {
                Text = "✎ Edit",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(125, 60),
                Size = new Size(100, 35)
            };
            editButton.ApplyTheme(ThemeManager.CurrentTheme);
            actionsPanel.Controls.Add(editButton);
            
            ThemedButton deleteButton = new ThemedButton
            {
                Text = "✕ Delete",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(235, 60),
                Size = new Size(100, 35),
                BackColor = ThemeManager.CurrentTheme.ErrorColor
            };
            deleteButton.ApplyTheme(ThemeManager.CurrentTheme);
            actionsPanel.Controls.Add(deleteButton);
            
            ThemedButton printButton = new ThemedButton
            {
                Text = "⎙ Print",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(15, 105),
                Size = new Size(100, 35)
            };
            printButton.ApplyTheme(ThemeManager.CurrentTheme);
            actionsPanel.Controls.Add(printButton);
            
            ThemedButton exportButton = new ThemedButton
            {
                Text = "↓ Export",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(125, 105),
                Size = new Size(100, 35)
            };
            exportButton.ApplyTheme(ThemeManager.CurrentTheme);
            actionsPanel.Controls.Add(exportButton);
            
            ThemedButton refreshButton = new ThemedButton
            {
                Text = "↻ Refresh",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(235, 105),
                Size = new Size(100, 35)
            };
            refreshButton.ApplyTheme(ThemeManager.CurrentTheme);
            actionsPanel.Controls.Add(refreshButton);
            
            // Add to parent panel
            parent.Controls.Add(actionsPanel);
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
