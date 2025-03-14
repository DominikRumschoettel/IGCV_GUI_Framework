using System;
using System.Drawing;
using System.Windows.Forms;
using IGCV.GUI.Controls;
using IGCV.GUI.Themes;
using IGCV_GUI_Framework.Interfaces;

namespace IGCV_GUI_Framework.Pages
{
    /// <summary>
    /// Base class for all module pages
    /// </summary>
    public abstract class PageBase : UserControl, IModulePage
    {
        private string _title;
        private string _subtitle;
        private string _navigationName;
        private Image _icon;
        private int _order;

        /// <summary>
        /// Gets the title of the page to display in the header
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// Gets the subtitle of the page to display in the header
        /// </summary>
        public string Subtitle => _subtitle;

        /// <summary>
        /// Gets the navigation name to display in the menu bar
        /// </summary>
        public string NavigationName => _navigationName;

        /// <summary>
        /// Gets the icon to display in the menu and tiles
        /// </summary>
        public Image Icon => _icon;

        /// <summary>
        /// Gets the order of the page in the navigation
        /// </summary>
        public int Order => _order;

        /// <summary>
        /// Creates a new page
        /// </summary>
        protected PageBase(string title, string subtitle, string navigationName, Image icon, int order)
        {
            _title = title;
            _subtitle = subtitle;
            _navigationName = navigationName;
            _icon = icon;
            _order = order;

            // Set control properties
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Gets the UserControl instance for this page
        /// </summary>
        public UserControl GetPageContent()
        {
            return this;
        }

        /// <summary>
        /// Called when the page is activated/displayed
        /// </summary>
        public virtual void OnActivated()
        {
            // Can be overridden in derived classes
        }

        /// <summary>
        /// Called when the page is deactivated/hidden
        /// </summary>
        public virtual void OnDeactivated()
        {
            // Can be overridden in derived classes
        }

        /// <summary>
        /// Creates a standard panel with the themed panel style
        /// </summary>
        protected Panel CreateStandardPanel(int x, int y, int width, int height)
        {
            // Create a ThemedPanel instead of regular Panel
            ThemedPanel panel = new ThemedPanel
            {
                Location = new Point(x, y),
                Size = new Size(width, height)
            };
            
            // Apply theme
            panel.ApplyTheme();
            
            return panel;
        }

        /// <summary>
        /// Creates a standard section header
        /// </summary>
        protected Label CreateSectionHeader(string text, int x, int y)
        {
            // Create a ThemedLabel instead
            ThemedLabel header = new ThemedLabel
            {
                Text = text,
                AutoSize = true,
                Location = new Point(x, y)
            };
            
            // Apply theme
            header.ApplyTheme();
            return header;
        }

        /// <summary>
        /// Creates a standard primary action button
        /// </summary>
        protected Button CreatePrimaryButton(string text, int x, int y, int width, int height)
        {
            // Create a ThemedButton with primary style
            ThemedButton button = new ThemedButton
            {
                Text = text,
                Size = new Size(width, height),
                Location = new Point(x, y),
                ButtonStyle = ButtonStyle.Primary
            };
            
            // Apply theme
            button.ApplyTheme();
            
            return button;
        }

        /// <summary>
        /// Creates a standard secondary button
        /// </summary>
        protected Button CreateSecondaryButton(string text, int x, int y, int width, int height)
        {
            // Create a ThemedButton with secondary style
            ThemedButton button = new ThemedButton
            {
                Text = text,
                Size = new Size(width, height),
                Location = new Point(x, y),
                ButtonStyle = ButtonStyle.Secondary
            };
            
            // Apply theme
            button.ApplyTheme();
            
            return button;
        }
    }
}
