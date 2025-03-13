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
    public class LayoutControlsPage : PageBase
    {
        // Panels for organizing layouts
        private Panel _panelsPanel;
        private Panel _cardLayoutPanel;
        private Panel _gridLayoutPanel;
        private Panel _dashboardLayoutPanel;
        
        /// <summary>
        /// Creates a new LayoutControlsPage
        /// </summary>
        public LayoutControlsPage() 
            : base("Layout Controls", "Demonstration of layout components", "Layouts", 
                  CreatePlaceholderImage(),
                  3) // Fourth page in navigation
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create panels examples
            _panelsPanel = CreateStandardPanel(20, 20, 370, 230);
            
            // Add panel title
            Label panelsTitle = new Label
            {
                Text = "Panel Variants",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _panelsPanel.Controls.Add(panelsTitle);
            
            // Create panel examples
            CreatePanelExamples();
            
            // Create card layout examples
            _cardLayoutPanel = CreateStandardPanel(410, 20, 370, 230);
            
            // Add panel title
            Label cardLayoutTitle = new Label
            {
                Text = "Card Layouts",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _cardLayoutPanel.Controls.Add(cardLayoutTitle);
            
            // Create card layout examples
            CreateCardLayoutExamples();
            
            // Create grid layout examples
            _gridLayoutPanel = CreateStandardPanel(800, 20, 370, 230);
            
            // Add panel title
            Label gridLayoutTitle = new Label
            {
                Text = "Grid Layouts",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _gridLayoutPanel.Controls.Add(gridLayoutTitle);
            
            // Create grid layout examples
            CreateGridLayoutExamples();
            
            // Create dashboard layout
            _dashboardLayoutPanel = CreateStandardPanel(20, 270, 1150, 260);
            
            // Add panel title
            Label dashboardLayoutTitle = new Label
            {
                Text = "Dashboard Layout Example",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _dashboardLayoutPanel.Controls.Add(dashboardLayoutTitle);
            
            // Create dashboard layout
            CreateDashboardLayout();
            
            // Add panels to page
            this.Controls.Add(_panelsPanel);
            this.Controls.Add(_cardLayoutPanel);
            this.Controls.Add(_gridLayoutPanel);
            this.Controls.Add(_dashboardLayoutPanel);
            
            this.ResumeLayout(false);
        }
        
        private void CreatePanelExamples()
        {
            // Standard panel
            Label standardPanelLabel = new Label
            {
                Text = "Standard Panel:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };
            _panelsPanel.Controls.Add(standardPanelLabel);
            
            ThemedPanel standardPanel = new ThemedPanel
            {
                Location = new Point(150, 50),
                Size = new Size(200, 40),
                CornerRadius = ThemeManager.CurrentTheme.CornerRadius
            };
            standardPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            Label standardPanelText = new Label
            {
                Text = "Standard Panel",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            standardPanel.Controls.Add(standardPanelText);
            
            _panelsPanel.Controls.Add(standardPanel);
            
            // Rounded corners panel
            Label roundedPanelLabel = new Label
            {
                Text = "Rounded Corners:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 100)
            };
            _panelsPanel.Controls.Add(roundedPanelLabel);
            
            ThemedPanel roundedPanel = new ThemedPanel
            {
                Location = new Point(150, 100),
                Size = new Size(200, 40),
                CornerRadius = 20 // Highly rounded corners
            };
            roundedPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            Label roundedPanelText = new Label
            {
                Text = "Rounded Panel",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            roundedPanel.Controls.Add(roundedPanelText);
            
            _panelsPanel.Controls.Add(roundedPanel);
            
            // Bordered panel
            Label borderedPanelLabel = new Label
            {
                Text = "With Border:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 150)
            };
            _panelsPanel.Controls.Add(borderedPanelLabel);
            
            ThemedPanel borderedPanel = new ThemedPanel
            {
                Location = new Point(150, 150),
                Size = new Size(200, 40),
                CornerRadius = ThemeManager.CurrentTheme.CornerRadius,
                BorderWidth = 2,
                BorderColor = ThemeManager.CurrentTheme.AccentColor
            };
            borderedPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            Label borderedPanelText = new Label
            {
                Text = "Bordered Panel",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            borderedPanel.Controls.Add(borderedPanelText);
            
            _panelsPanel.Controls.Add(borderedPanel);
            
            // Colored panel
            Label coloredPanelLabel = new Label
            {
                Text = "Colored Panel:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 200)
            };
            _panelsPanel.Controls.Add(coloredPanelLabel);
            
            ThemedPanel coloredPanel = new ThemedPanel
            {
                Location = new Point(150, 200),
                Size = new Size(200, 40),
                CornerRadius = ThemeManager.CurrentTheme.CornerRadius,
                BackColor = Color.FromArgb(60, ThemeManager.CurrentTheme.AccentColor)
            };
            coloredPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            Label coloredPanelText = new Label
            {
                Text = "Colored Panel",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            coloredPanel.Controls.Add(coloredPanelText);
            
            _panelsPanel.Controls.Add(coloredPanel);
        }
        
        private void CreateCardLayoutExamples()
        {
            // Simple card
            ThemedPanel simpleCard = new ThemedPanel
            {
                Location = new Point(20, 50),
                Size = new Size(330, 80),
                CornerRadius = 5,
                BorderWidth = 1,
                BorderColor = Color.FromArgb(100, 255, 255, 255)
            };
            simpleCard.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Add title
            Label simpleCardTitle = new Label
            {
                Text = "Simple Card Title",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 10)
            };
            simpleCard.Controls.Add(simpleCardTitle);
            
            // Add content
            Label simpleCardContent = new Label
            {
                Text = "This is a simple card with a title and content. It demonstrates a basic card layout.",
                ForeColor = Color.White,
                Size = new Size(300, 40),
                Location = new Point(15, 35)
            };
            simpleCard.Controls.Add(simpleCardContent);
            
            _cardLayoutPanel.Controls.Add(simpleCard);
            
            // Card with header
            ThemedPanel headerCard = new ThemedPanel
            {
                Location = new Point(20, 140),
                Size = new Size(330, 80),
                CornerRadius = 5,
                BorderWidth = 0
            };
            headerCard.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Create colored header
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = ThemeManager.CurrentTheme.PrimaryColor
            };
            
            Label headerText = new Label
            {
                Text = "Card With Colored Header",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 5)
            };
            headerPanel.Controls.Add(headerText);
            
            headerCard.Controls.Add(headerPanel);
            
            // Add content
            Label headerCardContent = new Label
            {
                Text = "This card has a colored header section and content in the main body.",
                ForeColor = Color.White,
                Size = new Size(300, 40),
                Location = new Point(15, 35)
            };
            headerCard.Controls.Add(headerCardContent);
            
            _cardLayoutPanel.Controls.Add(headerCard);
        }
        
        private void CreateGridLayoutExamples()
        {
            // Create a 2x2 grid layout
            Panel gridContainer = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(330, 160),
                BackColor = Color.Transparent
            };
            
            // Define grid dimensions
            int rows = 2;
            int cols = 2;
            int cellWidth = (330 - ((cols - 1) * 10)) / cols;
            int cellHeight = (160 - ((rows - 1) * 10)) / rows;
            
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
                    Label cellLabel = new Label
                    {
                        Text = $"Cell {r+1},{c+1}",
                        Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                        ForeColor = Color.White,
                        AutoSize = true,
                        Location = new Point(10, 10)
                    };
                    cell.Controls.Add(cellLabel);
                    
                    // Add different content based on position
                    if (r == 0 && c == 0)
                    {
                        // Top-left: Add icon
                        Label iconLabel = new Label
                        {
                            Text = "📊",
                            Font = new Font("Segoe UI", 20f, FontStyle.Regular),
                            ForeColor = Color.White,
                            AutoSize = true,
                            Location = new Point(50, 30)
                        };
                        cell.Controls.Add(iconLabel);
                    }
                    else if (r == 0 && c == 1)
                    {
                        // Top-right: Add value
                        Label valueLabel = new Label
                        {
                            Text = "85%",
                            Font = new Font("Segoe UI", 20f, FontStyle.Bold),
                            ForeColor = Color.White,
                            AutoSize = true,
                            Location = new Point(50, 30)
                        };
                        cell.Controls.Add(valueLabel);
                    }
                    else if (r == 1 && c == 0)
                    {
                        // Bottom-left: Add trend
                        Label trendLabel = new Label
                        {
                            Text = "↗ +12%",
                            Font = new Font("Segoe UI", 14f, FontStyle.Regular),
                            ForeColor = ThemeManager.CurrentTheme.SuccessColor,
                            AutoSize = true,
                            Location = new Point(40, 30)
                        };
                        cell.Controls.Add(trendLabel);
                    }
                    else if (r == 1 && c == 1)
                    {
                        // Bottom-right: Add button
                        ThemedButton button = new ThemedButton
                        {
                            Text = "View",
                            ButtonStyle = ButtonStyle.Primary,
                            Location = new Point(25, 30),
                            Size = new Size(100, 30)
                        };
                        button.ApplyTheme(ThemeManager.CurrentTheme);
                        cell.Controls.Add(button);
                    }
                    
                    gridContainer.Controls.Add(cell);
                }
            }
            
            _gridLayoutPanel.Controls.Add(gridContainer);
        }
        
        private void CreateDashboardLayout()
        {
            // Create a full dashboard layout with multiple sections
            
            // KPI Cards (Key Performance Indicators)
            Panel kpiPanel = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(1110, 90),
                BackColor = Color.Transparent
            };
            
            // Create 4 KPI cards
            int cardWidth = (1110 - (3 * 20)) / 4;
            string[] titles = { "Users", "Revenue", "Projects", "Tasks" };
            string[] values = { "1,254", "$8,459", "32", "75%" };
            string[] trends = { "↗ +5.7%", "↗ +12.4%", "↘ -2.8%", "↗ +15.2%" };
            Color[] trendColors = { ThemeManager.CurrentTheme.SuccessColor, ThemeManager.CurrentTheme.SuccessColor, 
                ThemeManager.CurrentTheme.ErrorColor, ThemeManager.CurrentTheme.SuccessColor };
            
            for (int i = 0; i < 4; i++)
            {
                int x = i * (cardWidth + 20);
                
                ThemedPanel card = new ThemedPanel
                {
                    Location = new Point(x, 0),
                    Size = new Size(cardWidth, 90),
                    CornerRadius = 5,
                    BorderWidth = 0
                };
                card.ApplyTheme(ThemeManager.CurrentTheme);
                
                // Add icon - using emoji as placeholder for a real icon
                Label iconLabel = new Label
                {
                    Text = new string[] { "👥", "💰", "📂", "✓" }[i],
                    Font = new Font("Segoe UI", 24f, FontStyle.Regular),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(15, 25)
                };
                card.Controls.Add(iconLabel);
                
                // Add title
                Label titleLabel = new Label
                {
                    Text = titles[i],
                    Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(60, 15)
                };
                card.Controls.Add(titleLabel);
                
                // Add value
                Label valueLabel = new Label
                {
                    Text = values[i],
                    Font = new Font("Segoe UI", 20f, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(60, 40)
                };
                card.Controls.Add(valueLabel);
                
                // Add trend
                Label trendLabel = new Label
                {
                    Text = trends[i],
                    Font = new Font("Segoe UI",
										9f, FontStyle.Regular),
                    ForeColor = trendColors[i],
                    AutoSize = true,
                    Location = new Point(titleLabel.Right + 10, 18)
                };
                card.Controls.Add(trendLabel);
                
                kpiPanel.Controls.Add(card);
            }
            
            _dashboardLayoutPanel.Controls.Add(kpiPanel);
            
            // Create charts section - left side
            ThemedPanel chartPanel = new ThemedPanel
            {
                Location = new Point(20, 150),
                Size = new Size(530, 90),
                CornerRadius = 5
            };
            chartPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Chart title
            Label chartTitle = new Label
            {
                Text = "Revenue Trend (Last 12 Months)",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            chartPanel.Controls.Add(chartTitle);
            
            // Mock chart - just a visual representation
            chartPanel.Paint += (sender, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Chart dimensions
                int chartX = 15;
                int chartY = 45;
                int chartWidth = 500;
                int chartHeight = 30;
                
                // Draw baseline
                using (Pen linePen = new Pen(Color.FromArgb(80, 255, 255, 255), 1))
                {
                    e.Graphics.DrawLine(linePen, chartX, chartY + chartHeight, chartX + chartWidth, chartY + chartHeight);
                }
                
                // Create points for the chart line
                Point[] points = new Point[12];
                Random rnd = new Random(42); // Fixed seed for consistent random numbers
                
                for (int i = 0; i < 12; i++)
                {
                    int x = chartX + (i * (chartWidth / 11));
                    int y = chartY + chartHeight - rnd.Next(5, chartHeight);
                    points[i] = new Point(x, y);
                }
                
                // Draw the line
                using (Pen chartPen = new Pen(ThemeManager.CurrentTheme.AccentColor, 3))
                {
                    e.Graphics.DrawLines(chartPen, points);
                }
                
                // Draw points
                foreach (Point p in points)
                {
                    using (SolidBrush pointBrush = new SolidBrush(ThemeManager.CurrentTheme.AccentColor))
                    {
                        e.Graphics.FillEllipse(pointBrush, p.X - 3, p.Y - 3, 6, 6);
                    }
                    
                    using (Pen pointPen = new Pen(Color.White, 1))
                    {
                        e.Graphics.DrawEllipse(pointPen, p.X - 3, p.Y - 3, 6, 6);
                    }
                }
            };
            
            _dashboardLayoutPanel.Controls.Add(chartPanel);
            
            // Create statistics section - right side
            ThemedPanel statsPanel = new ThemedPanel
            {
                Location = new Point(570, 150),
                Size = new Size(560, 90),
                CornerRadius = 5
            };
            statsPanel.ApplyTheme(ThemeManager.CurrentTheme);
            
            // Stats title
            Label statsTitle = new Label
            {
                Text = "Activity Overview",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 15)
            };
            statsPanel.Controls.Add(statsTitle);
            
            // Add stats
            string[] statLabels = { "Active Users", "New Signups", "Conversion Rate" };
            string[] statValues = { "728", "142", "8.2%" };
            
            for (int i = 0; i < 3; i++)
            {
                int x = 15 + (i * 180);
                
                Label statLabel = new Label
                {
                    Text = statLabels[i],
                    Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(x, 45)
                };
                statsPanel.Controls.Add(statLabel);
                
                Label statValue = new Label
                {
                    Text = statValues[i],
                    Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(x, 65)
                };
                statsPanel.Controls.Add(statValue);
            }
            
            _dashboardLayoutPanel.Controls.Add(statsPanel);
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
