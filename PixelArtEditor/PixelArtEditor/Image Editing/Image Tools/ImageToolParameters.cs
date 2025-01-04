using PixelArtEditor.Image_Editing.Undo_Redo;

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
        /// A color value, representing an old color that will be replaced in the image.
        /// </summary>
        public Color? OldColor { get; set; }

        /// <summary>
        /// A color value, representing a color that will replace the Old Color property.
        /// </summary>
        public Color? NewColor { get; set; }

        /// <summary>
        /// The Original Image's dimensions.
        /// </summary>
        public Size? OriginalImageSize { get; set; }

        /// <summary>
        /// A method that can be called to update the Original Image, It receives a Bitmap that is the new image to use.
        /// </summary>
        public Action<Bitmap>? UpdateOriginalImage { get; set; }

        /// <summary>
        /// A rectangle that represents the selected area in the image.
        /// </summary>
        public Rectangle? SelectedArea { get; set; }

        /// <summary>
        /// A method that copies an area of an image.
        /// </summary>
        public Action<Rectangle>? CopyImage { get; set; }

        /// <summary>
        /// The location to paste a previously copied portion into the image.
        /// </summary>
        public Point? PasteLocation { get; set; }

        /// <summary>
        /// The method that pastes a previously copied area into an image.
        /// </summary>
        public Action<Point>? PasteImage { get; set; }

        /// <summary>
        /// The size of the copied portion of the image.
        /// </summary>
        public Size? ClipboardImageSize { get; set; }

        /// <summary>
        /// The size of the image.
        /// </summary>
        public Size? ImageSize { get; set; }

        /// <summary>
        /// Defines whether the image will be made transparent or not.
        /// </summary>
        public bool? MakeImageTransparent { get; set; }

        /// <summary>
        /// A method that uses an Image Tool. It receives several parameters:
        /// The ImageTool to use, two arrays of string, that define the imageParameters and undoParameters to use.
        /// And 3 nullable parameters: An existing ImageToolParameters, an existing UndoParameters and the image to use the tool on.
        /// </summary>
        public Action<int, string[], string[], ImageToolParameters?, UndoParameters?, Bitmap?>? UseImageTool { get; set; }

        /// <summary>
        /// A function that returns a reference to the Original Image.
        /// </summary>
        public Func<Bitmap>? GetImageReference { get; set; }

        /// <summary>
        /// a method to change the size of the Original Image.
        /// </summary>
        public Action<int, int>? ChangeOriginalImageSize { get; set; }

        /// <summary>
        /// A method that changes the values of the Number Boxes in the editor.
        /// </summary>
        public Action? ChangeViewNumberBoxes { get; set; }
    }
}
