using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV_GUI_Framework.Common;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Pages
{
    /// <summary>
    /// Page for demonstrating layout capabilities
    /// </summary>
    public class LayoutDemoPage : PageBase
    {
        // Panels for organizing layouts
        private Panel _mainPanel;
        private ThemedPanel _splitPanel1;
        private ThemedPanel _splitPanel2;
        private Panel _footerPanel;
        
        /// <summary>
        /// Creates a new LayoutDemoPage
        /// </summary>
        public LayoutDemoPage() 
            : base("Layout Demo", "Demonstration of layout capabilities", "Layout", 
                  CreatePlaceholderImage(),
                  2) // Third page in navigation (order 2)
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create page title
            Label pageTitle = new Label
            {
                Text = "Layout Demonstration",
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 30)
            };
            this.Controls.Add(pageTitle);
            
            Label pageSubtitle = new Label
            {
                Text = "This page demonstrates different layout capabilities of the IGCV GUI Framework",
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 70)
            };
            this.Controls.Add(pageSubtitle);
            
            // Create main panel that will contain our demo layouts
            _mainPanel = CreateStandardPanel(75, 120, 1005, 300);
            
            // Add header to main panel
            Label mainHeader = new ThemedLabel
            {
                Text = "Main Content Area",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _mainPanel.Controls.Add(mainHeader);
            
            // Create side-by-side layout with split panels
            _splitPanel1 = new ThemedPanel
            {
                Location = new Point(20, 50),
                Size = new Size(480, 230),
                CornerRadius = 8,
                BorderWidth = 1,
                BorderColor = Color.FromArgb(100, 255, 255, 255)
            };
            _splitPanel1.ApplyTheme(ThemeManager.CurrentTheme);
            _mainPanel.Controls.Add(_splitPanel1);
            
            _splitPanel2 = new ThemedPanel
            {
                Location = new Point(510, 50),
                Size = new Size(480, 230),
                CornerRadius = 8,
                BorderWidth = 1,
                BorderColor = Color.FromArgb(100, 255, 255, 255)
            };
            _splitPanel2.ApplyTheme(ThemeManager.CurrentTheme);
            _mainPanel.Controls.Add(_splitPanel2);
            
            // Add header to first split panel
            Label split1Header = new ThemedLabel
            {
                Text = "Left Panel",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            _splitPanel1.Controls.Add(split1Header);
            
            // Add card-style layouts to first split panel
            CreateCardLayout(_splitPanel1, "Card Title 1", "This is a sample card with content. It demonstrates a card-style layout with rounded corners and a subtle border.", 15, 50, 450, 80);
            CreateCardLayout(_splitPanel1, "Card Title 2", "Cards can be stacked vertically and arranged in various ways to create different UI patterns.", 15, 140, 450, 80);
            
            // Add header to second split panel
            Label split2Header = new ThemedLabel
            {
                Text = "Right Panel",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            _splitPanel2.Controls.Add(split2Header);
            
            // Add grid layout to second split panel
            CreateGridLayout(_splitPanel2, 15, 50, 450, 170);
            
            // Create footer panel for additional layout examples
            _footerPanel = CreateStandardPanel(75, 440, 1005, 200);
            
            // Add header to footer panel
            Label footerHeader = new ThemedLabel
            {
                Text = "Footer Content Area",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _footerPanel.Controls.Add(footerHeader);
            
            // Create a step indicator at the bottom
            CreateStepIndicator(_footerPanel, 20, 60, 950, 40, 5, 2);
            
            // Create a dashboard card layout
            CreateDashboardLayout(_footerPanel, 20, 110, 965, 80);
            
            // Add panels to page
            this.Controls.Add(_mainPanel);
            this.Controls.Add(_footerPanel);
            
            this.ResumeLayout(false);
        }
        
        private void CreateCardLayout(Control parent, string title, string content, int x, int y, int width, int height)
        {
            // Create card panel
            ThemedPanel cardPanel = new ThemedPanel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                CornerRadius = 6,
                BorderWidth = 1,
                BorderColor = ThemeManager.CurrentTheme.BorderColor,
                BackColor = Color.FromArgb(30, 255, 255, 255)
            };
            cardPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add title
            ThemedLabel titleLabel = new ThemedLabel
            {
                Text = title,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 10)
            };
            cardPanel.Controls.Add(titleLabel);
            
            // Add content
            ThemedLabel contentLabel = new ThemedLabel
            {
                Text = content,
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(width - 30, height - 40),
                Location = new Point(15, 35)
            };
            cardPanel.Controls.Add(contentLabel);
            
            parent.Controls.Add(cardPanel);
        }
        
        private void CreateGridLayout(Control parent, int x, int y, int width, int height)
        {
            // Create grid container
            Panel gridContainer = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.Transparent
            };
            
            // Define grid dimensions
            int rows = 2;
            int cols = 2;
            int cellWidth = (width - ((cols - 1) * 10)) / cols;
            int cellHeight = (height - ((rows - 1) * 10)) / rows;
            
            // Create grid cells
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int cellX = c * (cellWidth + 10);
                    int cellY = r * (cellHeight + 10);
                    
                    ThemedPanel cell = new ThemedPanel
                    {
                        Location = new Point(cellX, cellY),
                        Size = new Size(cellWidth, cellHeight),
                        CornerRadius = 5,
                        BorderWidth = 1,
                        BorderColor = ThemeManager.CurrentTheme.BorderColor,
                        BackColor = Color.FromArgb(40, ThemeManager.CurrentTheme.AccentColor)
                    };
                    cell.ApplyTheme(ThemeManager.CurrentTheme);
                    
                    // Add cell label
                    ThemedLabel cellLabel = new ThemedLabel
                    {
                        Text = $"Grid Cell {r+1},{c+1}",
                        Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                        ForeColor = Color.White,
                        AutoSize = true,
                        Location = new Point(10, 10)
                    };
                    cell.Controls.Add(cellLabel);
                    
                    // Add some content based on position
                    if (r == 0 && c == 0)
                    {
                        // Top-left: Add a button
                        ThemedButton button = new ThemedButton
                        {
                            Text = "Grid Button",
                            ButtonStyle = ButtonStyle.Primary,
                            Location = new Point(10, 40),
                            Size = new Size(cellWidth - 20, 30)
                        };
                        button.ApplyTheme();
                        cell.Controls.Add(button);
                    }
                    else if (r == 0 && c == 1)
                    {
                        // Top-right: Add progress bar
                        ThemedProgressBar progressBar = new ThemedProgressBar
                        {
                            Value = 70,
                            Location = new Point(10, 40),
                            Size = new Size(cellWidth - 20, 20)
                        };
                        progressBar.ApplyTheme();
                        cell.Controls.Add(progressBar);
                        
                        ThemedLabel progressLabel = new ThemedLabel
                        {
                            Text = "70% Complete",
                            Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                            ForeColor = Color.White,
                            AutoSize = true,
                            Location = new Point(10, 65)
                        };
                        cell.Controls.Add(progressLabel);
                    }
                    else if (r == 1 && c == 0)
                    {
                        // Bottom-left: Add checkboxes
                        ThemedCheckBox checkbox1 = new ThemedCheckBox
                        {
                            Text = "Option 1",
                            Location = new Point(10, 40),
                            AutoSize = true,
                            Checked = true
                        };
                        checkbox1.ApplyTheme();
                        cell.Controls.Add(checkbox1);
                        
                        ThemedCheckBox checkbox2 = new ThemedCheckBox
                        {
                            Text = "Option 2",
                            Location = new Point(10, 65),
                            AutoSize = true
                        };
                        checkbox2.ApplyTheme();
                        cell.Controls.Add(checkbox2);
                    }
                    else if (r == 1 && c == 1)
                    {
                        // Bottom-right: Add text
                        ThemedLabel infoLabel = new ThemedLabel
                        {
                            Text = "Grid layouts provide flexible arrangements for organizing content in your UI.",
                            Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                            ForeColor = Color.White,
                            AutoSize = false,
                            Size = new Size(cellWidth - 20, 60),
                            Location = new Point(10, 40)
                        };
                        cell.Controls.Add(infoLabel);
                    }
                    
                    gridContainer.Controls.Add(cell);
                }
            }
            
            parent.Controls.Add(gridContainer);
        }
        
        private void CreateStepIndicator(Control parent, int x, int y, int width, int height, int steps, int currentStep)
        {
            // Create panel for step indicator
            Panel stepPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.Transparent
            };
            
            // Calculate step spacing
            int stepSpacing = width / steps;
            int dotSize = 20;
            int lineHeight = 4;
            
            // Create step indicator with custom painting
            stepPanel.Paint += (sender, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
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
                    
                    // Different colors for completed, current, and future steps
                    Color dotColor;
                    Color textColor;
                    
                    if (i < currentStep)
                    {
                        // Completed step
                        dotColor = ThemeManager.CurrentTheme.SuccessColor;
                        textColor = Color.White;
                    }
                    else if (i == currentStep)
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
            
            parent.Controls.Add(stepPanel);
        }
        
        private void CreateDashboardLayout(Control parent, int x, int y, int width, int height)
        {
            // Create panel for dashboard
            Panel dashboardPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.Transparent
            };
            
            // Create dashboard cards
            int cardCount = 4;
            int cardWidth = (width - ((cardCount - 1) * 15)) / cardCount;
            int cardHeight = height;
            
            string[] titles = { "Users", "Revenue", "Projects", "Tasks" };
            string[] values = { "1,254", "$8,459", "32", "75%" };
            Color[] colors = { 
                ThemeManager.CurrentTheme.PrimaryColor, 
                ThemeManager.CurrentTheme.SuccessColor,
                ThemeManager.CurrentTheme.AccentColor, 
                ThemeManager.CurrentTheme.WarningColor 
            };
            
            for (int i = 0; i < cardCount; i++)
            {
                int cardX = i * (cardWidth + 15);
                
                ThemedPanel card = new ThemedPanel
                {
                    Location = new Point(cardX, 0),
                    Size = new Size(cardWidth, cardHeight),
                    CornerRadius = 6,
                    BorderWidth = 0,
                    BackColor = Color.FromArgb(60, colors[i])
                };
                card.ApplyTheme(ThemeManager.CurrentTheme);
                
                // Add title
                ThemedLabel titleLabel = new ThemedLabel
                {
                    Text = titles[i],
                    Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(15, 15)
                };
                card.Controls.Add(titleLabel);
                
                // Add value
                ThemedLabel valueLabel = new ThemedLabel
                {
                    Text = values[i],
                    Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(15, 40)
                };
                card.Controls.Add(valueLabel);
                
                dashboardPanel.Controls.Add(card);
            }
            
            parent.Controls.Add(dashboardPanel);
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
