using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Common.Controls
{
    /// <summary>
    /// Container for page content with header - simplified implementation
    /// </summary>
    public class PageContainer : UserControl
    {
        // Use a fixed layout with anchored controls instead of docking for stability
        private ThemedLabel _titleLabel;
        private ThemedLabel _subtitleLabel;
        private Panel _underline;
        private Panel _contentPanel;

        // Active page
        private IModulePage _activePage;

        // Constructor
        public PageContainer()
        {
            // Set double buffered to reduce flicker
            this.DoubleBuffered = true;
            InitializeComponent();
            ConfigureHeaderControls();
            
            // Register for theme changes
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }

        private void InitializeComponent()
        {
            _titleLabel = new ThemedLabel();
            _subtitleLabel = new ThemedLabel();
            _underline = new Panel();
            _contentPanel = new Panel();
            SuspendLayout();
            // 
            // _titleLabel
            // 
            _titleLabel.AutoSize = true;
            _titleLabel.Location = new Point(50, 20);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Size = new Size(100, 23);
            _titleLabel.TabIndex = 0;
            _titleLabel.Text = "Title";
            // 
            // _subtitleLabel
            // 
            _subtitleLabel.AutoSize = true;
            _subtitleLabel.Location = new Point(50, 90);
            _subtitleLabel.Name = "_subtitleLabel";
            _subtitleLabel.Size = new Size(100, 23);
            _subtitleLabel.TabIndex = 1;
            _subtitleLabel.Text = "Subtitle";
            // 
            // _underline
            // 
            _underline.BackColor = Color.White;
            _underline.Location = new Point(50, 120);
            _underline.Name = "_underline";
            _underline.Size = new Size(70, 3);
            _underline.TabIndex = 2;
            // 
            // _contentPanel
            // 
            _contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _contentPanel.Location = new Point(0, 140);
            _contentPanel.Name = "_contentPanel";
            _contentPanel.Size = new Size(Width, Height - 140);
            _contentPanel.TabIndex = 3;
            // 
            // PageContainer
            // 
            BackColor = Color.Transparent;
            Controls.Add(_contentPanel);
            Controls.Add(_underline);
            Controls.Add(_subtitleLabel);
            Controls.Add(_titleLabel);
            Name = "PageContainer";
            Resize += PageContainer_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>
        /// Configures the header controls with proper fonts, colors, etc.
        /// This method is called after InitializeComponent to ensure proper configuration
        /// </summary>
        private void ConfigureHeaderControls()
        {
            // Configure title label
            _titleLabel.LabelSize = LabelSize.XLarge;
            _titleLabel.ForeColor = ThemeManager.CurrentTheme.TextOnDarkColor;
            _titleLabel.AutoSize = true;
            _titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            _titleLabel.ApplyTheme();

            // Configure subtitle label
            _subtitleLabel.LabelSize = LabelSize.Medium;
            _subtitleLabel.ForeColor = ThemeManager.CurrentTheme.TextOnDarkColor;
            _subtitleLabel.AutoSize = true;
            _subtitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            _subtitleLabel.ApplyTheme();

            // Configure underline
            _underline.BackColor = ThemeManager.CurrentTheme.PrimaryColor;
            _underline.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Configure content panel
            _contentPanel.BackColor = Color.Transparent;
            _contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Update content panel size
            UpdateContentPanelSize();
        }
        
        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            // Update theme-dependent elements
            _titleLabel.ApplyTheme();
            _subtitleLabel.ApplyTheme();
            _underline.BackColor = ThemeManager.CurrentTheme.PrimaryColor;
            
            // Apply theme to content if any
            ThemeManager.ApplyThemeToContainer(_contentPanel);
        }

        private void PageContainer_Resize(object sender, EventArgs e)
        {
            // Update content panel size when container is resized
            UpdateContentPanelSize();
        }

        private void UpdateContentPanelSize()
        {
            _contentPanel.Width = this.Width;
            _contentPanel.Height = this.Height - 140;
        }

        /// <summary>
        /// Sets the active page
        /// </summary>
        public void SetPage(IModulePage page)
        {
            // Deactivate current page if exists
            if (_activePage != null)
            {
                _activePage.OnDeactivated();
                _contentPanel.Controls.Clear();
            }

            // Set new page
            _activePage = page;

            if (page != null)
            {
                // Update header
                _titleLabel.Text = page.Title;
                _subtitleLabel.Text = page.Subtitle;

                // Add page content
                var content = page.GetPageContent();
                content.Dock = DockStyle.Fill;
                _contentPanel.Controls.Add(content);
                
                // Apply theme to the new content
                ThemeManager.ApplyThemeToContainer(content);

                // Activate page
                page.OnActivated();
            }
        }

        /// <summary>
        /// Refreshes the header - simplified to just update text
        /// </summary>
        public void RefreshHeader()
        {
            if (_activePage != null)
            {
                _titleLabel.Text = _activePage.Title;
                _subtitleLabel.Text = _activePage.Subtitle;
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unregister from theme changed event
                ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
}