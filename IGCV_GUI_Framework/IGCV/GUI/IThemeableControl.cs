using System;
using System.Drawing;

namespace IGCV.GUI
{
    /// <summary>
    /// Interface for controls that support IGCV theming capabilities
    /// </summary>
    public interface IThemeableControl
    {
        /// <summary>
        /// Gets or sets the corner radius for the control
        /// </summary>
        int CornerRadius { get; set; }
        
        /// <summary>
        /// Gets or sets the border color for the control
        /// </summary>
        Color BorderColor { get; set; }
        
        /// <summary>
        /// Gets or sets the border width for the control
        /// </summary>
        int BorderWidth { get; set; }
    }
}
