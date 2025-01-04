using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the ResizeImage Tool.
    /// </summary>
    public class ResizeImageCommand : IUndoRedoCommand
    {
        /// <summary>
        /// A copy of the image before it was edited.
        /// </summary>
        private Bitmap UneditedImage { get; set; }

        /// <summary>
        /// A copy of the image after it was edited.
        /// </summary>
        private Bitmap EditedImage { get; set; }

        /// <summary>
        /// A method used to update the Original Image to a new image.
        /// This is used in order to replace the old image with the resized one in the ImageHandler class.
        /// </summary>
        public Action<Bitmap> UpdateOriginalImage { get; set; }

        /// <summary>
        /// A method used to change the Original Image size values in the ImageHandler class.
        /// </summary>
        public Action<int, int> ChangeOriginalImageSize { get; set; }

        /// <summary>
        /// A method used to change the size values in the View Number Boxes to the values after undoing/redoing the edit.
        /// </summary>
        public Action ChangeViewNumberBoxes { get; set; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="oldImage">The old image, before resizing.</param>
        /// <param name="newImage">The new image, after resizing.</param>
        /// <param name="updateOriginalImage">The method used to change the Original Image in the ImageHandler class.</param>
        /// <param name="changeOriginalImageSize">The method used to change the Original Image size in the ImageHandler class.</param>
        /// <param name="changeViewNumberBoxes">The method used to change the size values in the View Number Boxes.</param>
        public ResizeImageCommand(Bitmap oldImage, Bitmap newImage, Action<Bitmap> updateOriginalImage, Action<int, int> changeOriginalImageSize, Action changeViewNumberBoxes)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            UpdateOriginalImage = updateOriginalImage;
            ChangeOriginalImageSize = changeOriginalImageSize;
            ChangeViewNumberBoxes = changeViewNumberBoxes;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Changes the image size, replaces the image with the Edited Image, then changes the number boxes to the new value.
            ChangeOriginalImageSize(EditedImage.Width, EditedImage.Height);
            UpdateOriginalImage(EditedImage);
            ChangeViewNumberBoxes();
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Changes the image size, replaces the image with the Unedited Image, then changes the number boxes to the new value.
            ChangeOriginalImageSize(UneditedImage.Width, UneditedImage.Height);
            UpdateOriginalImage(UneditedImage);
            ChangeViewNumberBoxes();
        }
    }
}
