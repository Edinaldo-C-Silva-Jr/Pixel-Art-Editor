namespace PixelArtEditor.Image_Editing.Drawing_Tools
{
    /// <summary>
    /// The optional parameters for Drawing Tools that not all of them will need to use.
    /// </summary>
    public class DrawingToolParameters
    {
        /// <summary>
        /// The size of each pixel in the image.
        /// </summary>
        public int? PixelSize { get; set; }

        /// <summary>
        /// The position where the mouse clicked when the tool is used.
        /// </summary>
        public Point? ClickLocation { get; set; }

        /// <summary>
        /// The size of the image.
        /// </summary>
        public Size? ImageSize { get; set; }

        /// <summary>
        /// The background color of the image.
        /// </summary>
        public Color? BackgroundColor { get; set; }

        /// <summary>
        /// If the image is using a transparent background or not.
        /// </summary>
        public bool? Transparency { get; set; }
    }
}
