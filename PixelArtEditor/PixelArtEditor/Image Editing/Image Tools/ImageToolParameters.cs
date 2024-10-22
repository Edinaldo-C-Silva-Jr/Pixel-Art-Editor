namespace PixelArtEditor.Image_Editing.Image_Tools
{
    /// <summary>
    /// The optional parameters used by Image Tools.
    /// </summary>
    public class ImageToolParameters
    {
        /// <summary>
        /// The color of the image's background.
        /// </summary>
        public Color? BackgroundColor { get; set; }

        /// <summary>
        /// The new color to use when coloring something.
        /// </summary>
        public Color? NewColor { get; set; }
    }
}
