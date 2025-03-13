using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV_GUI_Framework.Common;

namespace IGCV_GUI_Framework.Pages
{
    /// <summary>
    /// Sample placeholder page implementation for pages that haven't been fully implemented yet
    /// </summary>
    public class SamplePage : PageBase
    {
        public SamplePage(string title, string subtitle, string navigationName, Image icon, int order)
            : base(title, subtitle, navigationName, icon, order)
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            // Create a placeholder label showing page information
            Label placeholderLabel = new Label
            {
                Text = $"This is the {this.Title} page.\nImplementation coming soon.",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 100),
                TextAlign = ContentAlignment.MiddleCenter
            };
            
            // Create a placeholder panel for future controls
            Panel placeholderPanel = new Panel
            {
                BackColor = FraunhoferTheme.DarkPanel,
                Size = new Size(500, 300),
                Location = new Point(50, 150)
            };
            
            // Add a "Coming Soon" label to the panel
            Label comingSoonLabel = new Label
            {
                Text = "Under Development",
                Font = new Font("Segoe UI", 14f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(150, 130)
            };
            placeholderPanel.Controls.Add(comingSoonLabel);
            
            // Add a simulated action button
            Button demoButton = CreatePrimaryButton("Demo Button", 150, 200, 180, 40);
            demoButton.Click += (s, e) => MessageBox.Show($"This is a demo of the {this.Title} page functionality.\nActual implementation will be added in a future update.", "Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            placeholderPanel.Controls.Add(demoButton);
            
            // Add controls to the page
            this.Controls.Add(placeholderLabel);
            this.Controls.Add(placeholderPanel);
        }
        
        public override void OnActivated()
        {
            base.OnActivated();
            Console.WriteLine($"Sample page '{Title}' activated");
        }
    }
}
