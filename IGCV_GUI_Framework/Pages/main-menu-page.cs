using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV_GUI_Framework.Common;

namespace IGCV_GUI_Framework.Pages
{
    /// <summary>
    /// Main menu page implementation
    /// </summary>
    public class MainMenuPage : PageBase
    {
        // Dots and tiles
        private Panel _dotsPanel;
        private TableLayoutPanel _tilesLayout;

        public MainMenuPage()
            : base("Main Menu", "Choose your next task", "Main Menu", null, 0)
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Create a welcome heading
            Label welcomeLabel = new Label
            {
                Text = "IGCV GUI Framework Demo",
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 20)
            };
            this.Controls.Add(welcomeLabel);

            // Use TableLayoutPanel for better tile arrangement
            _tilesLayout = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(0),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                Dock = DockStyle.None,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Set column and row styles
            _tilesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            _tilesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            _tilesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            _tilesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));

            // Add spacing between rows and columns
            _tilesLayout.Margin = new Padding(0);
            _tilesLayout.Padding = new Padding(0);
            _tilesLayout.RowStyles[0].SizeType = SizeType.Absolute;
            _tilesLayout.RowStyles[0].Height = 200;
            _tilesLayout.RowStyles[1].SizeType = SizeType.Absolute;
            _tilesLayout.RowStyles[1].Height = 200;

            // Add tiles to the layout
            // Create Fraunhofer blue placeholder images
            Bitmap placeholderImage = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(placeholderImage))
            {
                g.Clear(Color.FromArgb(0, 103, 172)); // Fraunhofer blue
            }
            
            // Moving tile
            _tilesLayout.Controls.Add(CreateFeatureTile("Moving", placeholderImage, 0), 0, 0);

            // Controlling tile
            _tilesLayout.Controls.Add(CreateFeatureTile("Controlling", placeholderImage, 1), 1, 0);

            // Printing tile
            _tilesLayout.Controls.Add(CreateFeatureTile("Printing", placeholderImage, 2), 0, 1);

            // Sensing tile
            _tilesLayout.Controls.Add(CreateFeatureTile("Sensing", placeholderImage, 3), 1, 1);

            // Position the layout in the center
            _tilesLayout.Location = new Point(
                (this.ClientSize.Width - _tilesLayout.Width) / 2,
                (this.ClientSize.Height - _tilesLayout.Height) / 2 + 30); // Add 30px offset from center to account for header

            // Create decorative dots around the tiles
            _dotsPanel = CreateDecorativeDots(
                _tilesLayout.Location.X - 30,
                _tilesLayout.Location.Y - 30,
                _tilesLayout.Width + 60,
                _tilesLayout.Height + 60);

            this.Controls.Add(_dotsPanel);
            this.Controls.Add(_tilesLayout);

            // Handle resize
            this.Resize += MainMenuPage_Resize;

            this.ResumeLayout(false);
        }

        private void MainMenuPage_Resize(object sender, EventArgs e)
        {
            // Recenter the tiles when the page resizes
            if (_tilesLayout != null)
            {
                _tilesLayout.Location = new Point(
                    (this.ClientSize.Width - _tilesLayout.Width) / 2,
                    (this.ClientSize.Height - _tilesLayout.Height) / 2 + 30);

                if (_dotsPanel != null)
                {
                    _dotsPanel.Location = new Point(
                        _tilesLayout.Location.X - 30,
                        _tilesLayout.Location.Y - 30);
                    _dotsPanel.Size = new Size(_tilesLayout.Width + 60, _tilesLayout.Height + 60);
                    _dotsPanel.Invalidate();
                }
            }
        }

        private Panel CreateFeatureTile(string title, Image image, int pageIndex)
        {
            // Create tile with margin for spacing
            Panel tile = new Panel
            {
                Margin = new Padding(15),
                Size = new Size(220, 170),
                BackColor = Color.White
            };

            PictureBox pb = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Margin = new Padding(10)
            };

            Label lbl = new Label
            {
                Text = title,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 40,
                Font = FraunhoferTheme.TileFont,
                ForeColor = FraunhoferTheme.DarkTextColor
            };

            tile.Controls.Add(pb);
            tile.Controls.Add(lbl);

            // Store the click handler
            EventHandler clickHandler = (s, e) => {
                // Find the parent MainForm
                Form parentForm = this.FindForm();
                if (parentForm is MainForm mainForm)
                {
                    // Use reflection to call the private ShowPage method
                    var method = typeof(MainForm).GetMethod("ShowPage",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    method?.Invoke(mainForm, new object[] { pageIndex + 1 }); // +1 since index 0 is main menu
                }
            };

            // Add click handler to panel and child controls
            tile.Click += clickHandler;
            pb.Click += clickHandler;
            lbl.Click += clickHandler;

            return tile;
        }

        private Panel CreateDecorativeDots(int x, int y, int width, int height)
        {
            // Create a grid of small square dots around the tiles
            Panel dotsPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.Transparent
            };

            dotsPanel.Paint += (s, e) => {
                // Draw dots on all four sides of the rectangle
                int dotSpacing = 20;
                int dotSize = 4;

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
                {
                    // Top and bottom rows
                    for (int i = 0; i <= width; i += dotSpacing)
                    {
                        e.Graphics.FillRectangle(brush, i, 0, dotSize, dotSize);
                        e.Graphics.FillRectangle(brush, i, height - dotSize, dotSize, dotSize);
                    }

                    // Left and right columns
                    for (int i = dotSpacing; i < height - dotSpacing; i += dotSpacing)
                    {
                        e.Graphics.FillRectangle(brush, 0, i, dotSize, dotSize);
                        e.Graphics.FillRectangle(brush, width - dotSize, i, dotSize, dotSize);
                    }
                }
            };

            return dotsPanel;
        }
    }
}
