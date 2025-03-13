using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV.GUI.Demo;

namespace IGCV_GUI_Framework
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // Create and run the main application
            MainForm mainForm = new MainForm();
            
            // Add demo launcher panel
            Panel demoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };
            mainForm.Controls.Add(demoPanel);
            
            // Create label
            Label demoLabel = new Label
            {
                Text = "IGCV GUI Demo Options:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            demoPanel.Controls.Add(demoLabel);
            
            // Add demo launcher button
            Button launchDemoButton = new Button
            {
                Text = "Demo Launcher",
                Location = new Point(10, 30),
                Size = new Size(120, 25)
            };
            launchDemoButton.Click += (s, e) => {
                var demoLauncher = new DemoLauncher();
                demoLauncher.Show();
            };
            demoPanel.Controls.Add(launchDemoButton);
            
            // Add controls demo button
            Button controlsDemoButton = new Button
            {
                Text = "Controls Demo",
                Location = new Point(140, 30),
                Size = new Size(120, 25)
            };
            controlsDemoButton.Click += (s, e) => {
                var controlsDemo = new ControlsDemoForm();
                controlsDemo.Show();
            };
            demoPanel.Controls.Add(controlsDemoButton);
            
            // Add integration demo button
            Button integrationDemoButton = new Button
            {
                Text = "Integration Demo",
                Location = new Point(270, 30),
                Size = new Size(120, 25)
            };
            integrationDemoButton.Click += (s, e) => {
                var integrationDemo = new ThemeIntegrationSample();
                integrationDemo.Show();
            };
            demoPanel.Controls.Add(integrationDemoButton);
            
            // Run the application
            Application.Run(mainForm);
        }
    }
}
