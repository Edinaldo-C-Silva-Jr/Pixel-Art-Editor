namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    /// <summary>
    /// The optional parameters used in the Undo function of the tools.
    /// </summary>
    public class UndoParameters
    {
        /// <summary>
        /// The location where the Drawing Image was taken from the Original Image.
        /// </summary>
        public Point? DrawingImageLocation { get; set; }

        /// <summary>
        /// The background color of the image.
        /// </summary>
        public Color? BackgroundColor { get; set; }

        /// <summary>
        /// The old color that is going to be replaced.
        /// </summary>
        public Color? OldColor { get; set; }

        /// <summary>
        /// The new color that will replace the old color.
        /// </summary>
        public Color? NewColor { get; set; }

        /// <summary>
        /// A method that changes the color of a ColorTable cell.
        /// </summary>
        public Action<Color>? ChangeCellColor { get; set; }

        /// <summary>
        /// A method that updates the original image with a new image.
        /// </summary>
        public Action<Bitmap>? UpdateOriginalImage { get; set; }

        /// <summary>
        /// A method that changes the size of the original image.
        /// </summary>
        public Action<int, int>? ChangeOriginalImageSize { get; set; }

        /// <summary>
        /// A method that changes the values inside the viewing number boxes.
        /// </summary>
        public Action? ChangeViewNumberBoxes { get; set; }

        /// <summary>
        /// The location to paste a previously copied image.
        /// </summary>
        public Point? PasteLocation { get; set; }
    }
}
