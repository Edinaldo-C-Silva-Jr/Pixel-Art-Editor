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
        /// The original image's dimensions.
        /// </summary>
        public Size? OriginalImagesize { get; set; }

        /// <summary>
        /// A color value, representing an old color that will be replaced in the image.
        /// </summary>
        public Color? OldColor { get; set; }

        /// <summary>
        /// A color value, representing a color that will replace the Old Color property.
        /// </summary>
        public Color? NewColor { get; set; }
    }
}
