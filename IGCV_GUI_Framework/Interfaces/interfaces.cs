using System;
using System.Windows.Forms;

namespace IGCV_GUI_Framework.Interfaces
{
    /// <summary>
    /// Interface for all page modules
    /// </summary>
    public interface IModulePage
    {
        /// <summary>
        /// Gets the title of the page to display in the header
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the subtitle of the page to display in the header
        /// </summary>
        string Subtitle { get; }

        /// <summary>
        /// Gets the navigation name to display in the menu bar
        /// </summary>
        string NavigationName { get; }

        /// <summary>
        /// Gets the icon to display in the menu and tiles
        /// </summary>
        Image Icon { get; }

        /// <summary>
        /// Gets the order of the page in the navigation
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Gets the UserControl instance for this page
        /// </summary>
        UserControl GetPageContent();

        /// <summary>
        /// Called when the page is activated/displayed
        /// </summary>
        void OnActivated();

        /// <summary>
        /// Called when the page is deactivated/hidden
        /// </summary>
        void OnDeactivated();
    }

    /// <summary>
    /// Interface for printer controller
    /// </summary>
    public interface IPrinterController
    {
        /// <summary>
        /// Gets whether the printer is currently connected
        /// </summary>
        bool IsConnected { get; }
        
        /// <summary>
        /// Event raised when the printer connection status changes
        /// </summary>
        event EventHandler<bool> ConnectionStatusChanged;
        
        /// <summary>
        /// Gets the printer model name
        /// </summary>
        string ModelName { get; }
        
        /// <summary>
        /// Gets the printer serial number
        /// </summary>
        string SerialNumber { get; }
        
        /// <summary>
        /// Connects to the printer
        /// </summary>
        void Connect();
        
        /// <summary>
        /// Disconnects from the printer
        /// </summary>
        void Disconnect();
        
        /// <summary>
        /// Sends a raw command to the printer
        /// </summary>
        void SendCommand(string command);
    }
}
