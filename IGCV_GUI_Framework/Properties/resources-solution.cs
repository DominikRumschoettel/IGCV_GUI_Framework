using System.Drawing;

namespace IGCV_GUI_Framework.Properties
{
    /// <summary>
    /// Temporary resources class to replace the auto-generated Resources.Designer.cs
    /// </summary>
    public static class Resources
    {
        // Default placeholder images - replace these with your actual images
        private static readonly Bitmap defaultImage = new Bitmap(100, 100);

        static Resources()
        {
            // Create a simple default image (blue square)
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.FromArgb(0, 103, 172)); // Fraunhofer blue
            }
        }

        // Image resources used in the application
        public static Image MovingImg => new Bitmap(defaultImage);
        public static Image ControllingImg => new Bitmap(defaultImage);
        public static Image PrintingImg => new Bitmap(defaultImage);
        public static Image SensingImg => new Bitmap(defaultImage);
    }
}
