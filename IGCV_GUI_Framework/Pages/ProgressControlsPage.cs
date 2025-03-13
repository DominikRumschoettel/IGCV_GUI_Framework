using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV_GUI_Framework.Common;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Pages
{
    /// <summary>
    /// Page for demonstrating progress indicators
    /// </summary>
    public class ProgressControlsPage : PageBase
    {
        // Panels for organizing controls
        private Panel _progressBarsPanel;
        private Panel _stepIndicatorsPanel;
        private Panel _usageExamplesPanel;
        
        // Progress demo controls
        private ThemedProgressBar _dynamicProgressBar;
        private System.Windows.Forms.Timer _progressTimer;
        private int _progressValue = 0;
        
        // Progress-related UI elements made into fields for timer access
        private Label _percentageLabel;
        private ThemedButton _startButton;
        
        /// <summary>
        /// Creates a new ProgressControlsPage
        /// </summary>
        public ProgressControlsPage() 
            : base("Progress Controls", "Demonstration of progress indicators", "Progress", 
                  CreatePlaceholderImage(),
                  2) // Third page in navigation
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create progress bars panel
            _progressBarsPanel = CreateStandardPanel(20, 20, 560, 230);
            
            // Add panel title
            Label progressBarsTitle = new Label
            {
                Text = "Progress Bars",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _progressBarsPanel.Controls.Add(progressBarsTitle);
            
            // Create progress bars
            CreateProgressBars();
            
            // Create step indicators panel
            _stepIndicatorsPanel = CreateStandardPanel(600, 20, 570, 230);
            
            // Add panel title
            Label stepIndicatorsTitle = new Label
            {
                Text = "Step Indicators",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _stepIndicatorsPanel.Controls.Add(stepIndicatorsTitle);
            
            // Create step indicators
            CreateStepIndicators();
            
            // Create usage examples panel
            _usageExamplesPanel = CreateStandardPanel(20, 270, 1150, 260);
            
            // Add panel title
            Label usageExamplesTitle = new Label
            {
                Text = "Progress Control Usage Examples",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _usageExamplesPanel.Controls.Add(usageExamplesTitle);
            
            // Create usage examples
            CreateUsageExamples();
            
            // Add panels to page
            this.Controls.Add(_progressBarsPanel);
            this.Controls.Add(_stepIndicatorsPanel);
            this.Controls.Add(_usageExamplesPanel);
            
            // Initialize progress timer
            _progressTimer = new System.Windows.Forms.Timer();
            _progressTimer.Interval = 100;
            _progressTimer.Tick += ProgressTimer_Tick;
            
            this.ResumeLayout(false);
        }
        
        private void CreateProgressBars()
        {
            // Create different progress bar states
            
            // Empty progress bar
            Label emptyLabel = new Label
            {
                Text = "Empty (0%):",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };
            _progressBarsPanel.Controls.Add(emptyLabel);
            
            ThemedProgressBar emptyProgressBar = new ThemedProgressBar
            {
                Value = 0,
                Location = new Point(130, 50),
                Size = new Size(300, 25)
            };
            emptyProgressBar.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(emptyProgressBar);
            
            // Quarter progress bar
            Label quarterLabel = new Label
            {
                Text = "Quarter (25%):",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 85)
            };
            _progressBarsPanel.Controls.Add(quarterLabel);
            
            ThemedProgressBar quarterProgressBar = new ThemedProgressBar
            {
                Value = 25,
                Location = new Point(130, 85),
                Size = new Size(300, 25)
            };
            quarterProgressBar.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(quarterProgressBar);
            
            // Half progress bar
            Label halfLabel = new Label
            {
                Text = "Half (50%):",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 120)
            };
            _progressBarsPanel.Controls.Add(halfLabel);
            
            ThemedProgressBar halfProgressBar = new ThemedProgressBar
            {
                Value = 50,
                Location = new Point(130, 120),
                Size = new Size(300, 25)
            };
            halfProgressBar.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(halfProgressBar);
            
            // Three quarters progress bar
            Label threeQuartersLabel = new Label
            {
                Text = "Three Quarters (75%):",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 155)
            };
            _progressBarsPanel.Controls.Add(threeQuartersLabel);
            
            ThemedProgressBar threeQuartersProgressBar = new ThemedProgressBar
            {
                Value = 75,
                Location = new Point(130, 155),
                Size = new Size(300, 25)
            };
            threeQuartersProgressBar.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(threeQuartersProgressBar);
            
            // Complete progress bar
            Label completeLabel = new Label
            {
                Text = "Complete (100%):",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 190)
            };
            _progressBarsPanel.Controls.Add(completeLabel);
            
            ThemedProgressBar completeProgressBar = new ThemedProgressBar
            {
                Value = 100,
                Location = new Point(130, 190),
                Size = new Size(300, 25)
            };
            completeProgressBar.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(completeProgressBar);
            
            // Add progress control buttons
            ThemedButton increaseBtn = new ThemedButton
            {
                Text = "+10%",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(450, 85),
                Size = new Size(90, 30)
            };
            increaseBtn.Click += (s, e) => {
                // Increase all progress bars by 10%
                if (quarterProgressBar.Value < 100) quarterProgressBar.Value += 10;
                if (halfProgressBar.Value < 100) halfProgressBar.Value += 10;
                if (threeQuartersProgressBar.Value < 100) threeQuartersProgressBar.Value += 10;
                if (completeProgressBar.Value < 100) completeProgressBar.Value += 10;
            };
            increaseBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(increaseBtn);
            
            ThemedButton decreaseBtn = new ThemedButton
            {
                Text = "-10%",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(450, 125),
                Size = new Size(90, 30)
            };
            decreaseBtn.Click += (s, e) => {
                // Decrease all progress bars by 10%
                if (quarterProgressBar.Value > 0) quarterProgressBar.Value -= 10;
                if (halfProgressBar.Value > 0) halfProgressBar.Value -= 10;
                if (threeQuartersProgressBar.Value > 0) threeQuartersProgressBar.Value -= 10;
                if (completeProgressBar.Value > 0) completeProgressBar.Value -= 10;
            };
            decreaseBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(decreaseBtn);
            
            ThemedButton resetBtn = new ThemedButton
            {
                Text = "Reset",
                ButtonStyle = ButtonStyle.Tertiary,
                Location = new Point(450, 165),
                Size = new Size(90, 30)
            };
            resetBtn.Click += (s, e) => {
                // Reset all progress bars
                quarterProgressBar.Value = 25;
                halfProgressBar.Value = 50;
                threeQuartersProgressBar.Value = 75;
                completeProgressBar.Value = 100;
            };
            resetBtn.ApplyTheme(ThemeManager.CurrentTheme);
            _progressBarsPanel.Controls.Add(resetBtn);
        }
        
        private void CreateStepIndicators()
        {
            // Create different step indicators
            
            // 3-step indicator (first step active)
            Label threeStepLabel = new Label
            {
                Text = "3-Step Process:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };
            _stepIndicatorsPanel.Controls.Add(threeStepLabel);
            
            Panel threeStepPanel = new Panel
            {
                Location = new Point(150, 50),
                Size = new Size(400, 40),
                BackColor = Color.Transparent
            };
            _stepIndicatorsPanel.Controls.Add(threeStepPanel);
            
            CreateStepIndicator(threeStepPanel, 3, 0);
            
            // 5-step indicator (third step active)
            Label fiveStepLabel = new Label
            {
                Text = "5-Step Process:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 100)
            };
            _stepIndicatorsPanel.Controls.Add(fiveStepLabel);
            
            Panel fiveStepPanel = new Panel
            {
                Location = new Point(150, 100),
                Size = new Size(400, 40),
                BackColor = Color.Transparent
            };
            _stepIndicatorsPanel.Controls.Add(fiveStepPanel);
            
            CreateStepIndicator(fiveStepPanel, 5, 2);
            
            // 7-step indicator (completed)
            Label sevenStepLabel = new Label
            {
                Text = "7-Step Process:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 150)
            };
            _stepIndicatorsPanel.Controls.Add(sevenStepLabel);
            
            Panel sevenStepPanel = new Panel
            {
                Location = new Point(150, 150),
                Size = new Size(400, 40),
                BackColor = Color.Transparent
            };
            _stepIndicatorsPanel.Controls.Add(sevenStepPanel);
            
            CreateStepIndicator(sevenStepPanel, 7, 7);
        }
        
        private void CreateStepIndicator(Panel parent, int steps, int currentStep)
        {
            parent.Paint += (sender, e) => {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Calculate dimensions
                int width = parent.Width;
                int height = parent.Height;
                int dotSize = 24;
                int stepSpacing = width / steps;
                int lineHeight = 4;
                
                // Draw connecting lines
                using (Pen linePen = new Pen(Color.FromArgb(100, 255, 255, 255), lineHeight))
                {
                    e.Graphics.DrawLine(linePen, 
                        dotSize/2, height/2, 
                        width - dotSize/2, height/2);
                }
                
                // Draw step dots
                for (int i = 0; i < steps; i++)
                {
                    int x = (i * stepSpacing) + (stepSpacing / 2) - (dotSize / 2);
                    int y = (height / 2) - (dotSize / 2);
                    
                    // Different colors based on step status
                    Color dotColor;
                    Color textColor;
                    
                    if (i < currentStep)
                    {
                        // Completed step
                        dotColor = ThemeManager.CurrentTheme.SuccessColor;
                        textColor = Color.White;
                    }
                    else if (i == currentStep && currentStep < steps)
                    {
                        // Current step
                        dotColor = ThemeManager.CurrentTheme.AccentColor;
                        textColor = Color.White;
                    }
                    else
                    {
                        // Future step
                        dotColor = Color.FromArgb(80, 255, 255, 255);
                        textColor = Color.FromArgb(180, 255, 255, 255);
                    }
                    
                    // Draw dot
                    using (SolidBrush dotBrush = new SolidBrush(dotColor))
                    {
                        e.Graphics.FillEllipse(dotBrush, x, y, dotSize, dotSize);
                    }
                    
                    // Draw step number
                    using (Font font = new Font("Segoe UI", 8f, FontStyle.Bold))
                    using (SolidBrush textBrush = new SolidBrush(textColor))
                    {
                        string stepText = (i + 1).ToString();
                        SizeF textSize = e.Graphics.MeasureString(stepText, font);
                        e.Graphics.DrawString(
                            stepText, 
                            font, 
                            textBrush, 
                            x + (dotSize - textSize.Width) / 2, 
                            y + (dotSize - textSize.Height) / 2);
                    }
                }
            };
        }
        
        private void CreateUsageExamples()
        {
            // File Upload Example
            ThemedPanel uploadPanel = new ThemedPanel
            {
                Location = new Point(20, 50),
                Size = new Size(350, 190),
                CornerRadius = 5
            };
            uploadPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add title
            Label uploadTitle = new Label
            {
                Text = "File Upload Example",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            uploadPanel.Controls.Add(uploadTitle);
            
            // Add file information
            Label fileInfoLabel = new Label
            {
                Text = "Uploading: project_data.xlsx (2.5 MB)",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 50)
            };
            uploadPanel.Controls.Add(fileInfoLabel);
            
            // Add progress bar
            _dynamicProgressBar = new ThemedProgressBar
            {
                Value = 0,
                Location = new Point(15, 80),
                Size = new Size(320, 25)
            };
            _dynamicProgressBar.ApplyTheme(ThemeManager.CurrentTheme);
            uploadPanel.Controls.Add(_dynamicProgressBar);
            
            // Add percentage label
            _percentageLabel = new Label
            {
                Text = "0%",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 110)
            };
            uploadPanel.Controls.Add(_percentageLabel);
            
            // Add start/cancel buttons
            _startButton = new ThemedButton
            {
                Text = "Start Upload",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(15, 140),
                Size = new Size(120, 35)
            };
            _startButton.Click += (s, e) => {
                if (!_progressTimer.Enabled)
                {
                    _progressValue = 0;
                    _dynamicProgressBar.Value = 0;
                    _percentageLabel.Text = "0%";
                    _progressTimer.Start();
                    _startButton.Text = "Uploading...";
                    _startButton.Enabled = false;
                }
            };
            _startButton.ApplyTheme(ThemeManager.CurrentTheme);
            uploadPanel.Controls.Add(_startButton);
            
            ThemedButton cancelButton = new ThemedButton
            {
                Text = "Cancel",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(145, 140),
                Size = new Size(120, 35)
            };
            cancelButton.Click += (s, e) => {
                if (_progressTimer.Enabled)
                {
                    _progressTimer.Stop();
                    _progressValue = 0;
                    _dynamicProgressBar.Value = 0;
                    _percentageLabel.Text = "Cancelled";
                    _startButton.Text = "Start Upload";
                    _startButton.Enabled = true;
                }
            };
            cancelButton.ApplyTheme(ThemeManager.CurrentTheme);
            uploadPanel.Controls.Add(cancelButton);
            
            _usageExamplesPanel.Controls.Add(uploadPanel);
            
            // Multi-Step Form Example
            ThemedPanel formPanel = new ThemedPanel
            {
                Location = new Point(390, 50),
                Size = new Size(350, 190),
                CornerRadius = 5
            };
            formPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add title
            Label formTitle = new Label
            {
                Text = "Multi-Step Form Example",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            formPanel.Controls.Add(formTitle);
            
            // Create step container
            Panel stepContainer = new Panel
            {
                Location = new Point(15, 45),
                Size = new Size(320, 40),
                BackColor = Color.Transparent
            };
            formPanel.Controls.Add(stepContainer);
            
            // Add step indicator with 4 steps
            int currentFormStep = 1;
            CreateStepIndicator(stepContainer, 4, currentFormStep);
            
            // Add step content placeholder
            ThemedPanel stepContent = new ThemedPanel
            {
                Location = new Point(15, 90),
                Size = new Size(320, 60),
                CornerRadius = 3,
                BorderWidth = 1,
                BorderColor = Color.FromArgb(100, 255, 255, 255)
            };
            stepContent.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add step label
            Label stepLabel = new Label
            {
                Text = $"Step {currentFormStep + 1}: Personal Information",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            stepContent.Controls.Add(stepLabel);
            
            // Add instruction
            Label stepInstruction = new Label
            {
                Text = "Please fill in your personal details to continue.",
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                AutoSize = true,
                Location = new Point(10, 35)
            };
            stepContent.Controls.Add(stepInstruction);
            
            formPanel.Controls.Add(stepContent);
            
            // Add navigation buttons
            ThemedButton prevButton = new ThemedButton
            {
                Text = "Previous",
                ButtonStyle = ButtonStyle.Secondary,
                Location = new Point(15, 155),
                Size = new Size(100, 30),
                Enabled = false
            };
            prevButton.ApplyTheme(ThemeManager.CurrentTheme);
            formPanel.Controls.Add(prevButton);
            
            ThemedButton nextButton = new ThemedButton
            {
                Text = "Next Step",
                ButtonStyle = ButtonStyle.Primary,
                Location = new Point(235, 155),
                Size = new Size(100, 30)
            };
            
            // Step navigation logic
            nextButton.Click += (s, e) => {
                currentFormStep++;
                if (currentFormStep > 3) currentFormStep = 3;
                
                // Update step indicator
                stepContainer.Invalidate();
                
                // Update step content
                switch (currentFormStep)
                {
                    case 1:
                        stepLabel.Text = "Step 2: Contact Information";
                        stepInstruction.Text = "Please provide your contact details.";
                        prevButton.Enabled = true;
                        nextButton.Text = "Next Step";
                        break;
                    case 2:
                        stepLabel.Text = "Step 3: Payment Information";
                        stepInstruction.Text = "Enter your payment details securely.";
                        nextButton.Text = "Next Step";
                        break;
                    case 3:
                        stepLabel.Text = "Step 4: Review & Submit";
                        stepInstruction.Text = "Review your information and submit.";
                        nextButton.Text = "Submit";
                        break;
                }
            };
            
            prevButton.Click += (s, e) => {
                currentFormStep--;
                if (currentFormStep < 0) currentFormStep = 0;
                
                // Update step indicator
                stepContainer.Invalidate();
                
                // Update step content
                switch (currentFormStep)
                {
                    case 0:
                        stepLabel.Text = "Step 1: Personal Information";
                        stepInstruction.Text = "Please fill in your personal details to continue.";
                        prevButton.Enabled = false;
                        nextButton.Text = "Next Step";
                        break;
                    case 1:
                        stepLabel.Text = "Step 2: Contact Information";
                        stepInstruction.Text = "Please provide your contact details.";
                        nextButton.Text = "Next Step";
                        break;
                    case 2:
                        stepLabel.Text = "Step 3: Payment Information";
                        stepInstruction.Text = "Enter your payment details securely.";
                        nextButton.Text = "Next Step";
                        break;
                }
            };
            
            nextButton.ApplyTheme(ThemeManager.CurrentTheme);
            formPanel.Controls.Add(nextButton);
            
            _usageExamplesPanel.Controls.Add(formPanel);
            
            // Processing Example
            ThemedPanel processingPanel = new ThemedPanel
            {
                Location = new Point(760, 50),
                Size = new Size(350, 190),
                CornerRadius = 5
            };
            processingPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add title
            Label processingTitle = new Label
            {
                Text = "Data Processing Example",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            processingPanel.Controls.Add(processingTitle);
            
            // Create task progress panels
            Panel task1Panel = new Panel
            {
                Location = new Point(15, 50),
                Size = new Size(320, 30),
                BackColor = Color.Transparent
            };
            
            Label task1Label = new Label
            {
                Text = "Data Validation:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(0, 5)
            };
            task1Panel.Controls.Add(task1Label);
            
            ThemedProgressBar task1Progress = new ThemedProgressBar
            {
                Value = 100,
                Location = new Point(120, 5),
                Size = new Size(150, 20)
            };
            task1Progress.ApplyTheme(ThemeManager.CurrentTheme);
            task1Panel.Controls.Add(task1Progress);
            
            Label task1Status = new Label
            {
                Text = "Complete",
                ForeColor = ThemeManager.CurrentTheme.SuccessColor,
                AutoSize = true,
                Location = new Point(280, 5)
            };
            task1Panel.Controls.Add(task1Status);
            
            processingPanel.Controls.Add(task1Panel);
            
            // Task 2
            Panel task2Panel = new Panel
            {
                Location = new Point(15, 85),
                Size = new Size(320, 30),
                BackColor = Color.Transparent
            };
            
            Label task2Label = new Label
            {
                Text = "Data Processing:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(0, 5)
            };
            task2Panel.Controls.Add(task2Label);
            
            ThemedProgressBar task2Progress = new ThemedProgressBar
            {
                Value = 65,
                Location = new Point(120, 5),
                Size = new Size(150, 20)
            };
            task2Progress.ApplyTheme(ThemeManager.CurrentTheme);
            task2Panel.Controls.Add(task2Progress);
            
            Label task2Status = new Label
            {
                Text = "In Progress",
                ForeColor = ThemeManager.CurrentTheme.AccentColor,
                AutoSize = true,
                Location = new Point(280, 5)
            };
            task2Panel.Controls.Add(task2Status);
            
            processingPanel.Controls.Add(task2Panel);
            
            // Task 3
            Panel task3Panel = new Panel
            {
                Location = new Point(15, 120),
                Size = new Size(320, 30),
                BackColor = Color.Transparent
            };
            
            Label task3Label = new Label
            {
                Text = "Report Generation:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(0, 5)
            };
            task3Panel.Controls.Add(task3Label);
            
            ThemedProgressBar task3Progress = new ThemedProgressBar
            {
                Value = 0,
                Location = new Point(120, 5),
                Size = new Size(150, 20)
            };
            task3Progress.ApplyTheme(ThemeManager.CurrentTheme);
            task3Panel.Controls.Add(task3Progress);
            
            Label task3Status = new Label
            {
                Text = "Pending",
                ForeColor = Color.Silver,
                AutoSize = true,
                Location = new Point(280, 5)
            };
            task3Panel.Controls.Add(task3Status);
            
            processingPanel.Controls.Add(task3Panel);
            
            // Overall progress
            Label overallLabel = new Label
            {
                Text = "Overall Progress:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 155)
            };
            processingPanel.Controls.Add(overallLabel);
            
            ThemedProgressBar overallProgress = new ThemedProgressBar
            {
                Value = 55,
                Location = new Point(135, 155),
                Size = new Size(150, 20)
            };
            overallProgress.ApplyTheme(ThemeManager.CurrentTheme);
            processingPanel.Controls.Add(overallProgress);
            
            Label overallPercent = new Label
            {
                Text = "55%",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(295, 155)
            };
            processingPanel.Controls.Add(overallPercent);
            
            _usageExamplesPanel.Controls.Add(processingPanel);
        }
        
        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            // Update progress value
            _progressValue += 2;
            if (_progressValue > 100)
            {
                _progressValue = 100;
            }
            
            // Update progress bar
            _dynamicProgressBar.Value = _progressValue;
            
            // Update percentage label
            _percentageLabel.Text = $"{_progressValue}%";
            
            // When complete
            if (_progressValue >= 100)
            {
                _progressTimer.Stop();
                _percentageLabel.Text = "Complete!";
                _startButton.Text = "Start Upload";
                _startButton.Enabled = true;
            }
        }
        
        public override void OnActivated()
        {
            base.OnActivated();
            
            // Restart progress demo
            _progressValue = 0;
            if (_dynamicProgressBar != null)
            {
                _dynamicProgressBar.Value = 0;
            }
        }
        
        public override void OnDeactivated()
        {
            base.OnDeactivated();
            
            // Stop any running demos
            if (_progressTimer != null && _progressTimer.Enabled)
            {
                _progressTimer.Stop();
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
