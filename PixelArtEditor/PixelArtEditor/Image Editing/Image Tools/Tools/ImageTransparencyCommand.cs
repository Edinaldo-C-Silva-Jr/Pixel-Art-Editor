using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the ImageTransparency Tool.
    /// </summary>
    public class ImageTransparencyCommand : IUndoRedoCommand
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
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="oldImage">The old image, before changing transparency.</param>
        /// <param name="newImage">The new image, after changing transparency.</param>
        public ImageTransparencyCommand(Bitmap oldImage, Bitmap newImage)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Makes the image transparent before drawing, to ensure the transparency portion gets preserved.
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(EditedImage, 0, 0);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Makes the image transparent before drawing, to ensure the transparency portion gets preserved.
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(UneditedImage, 0, 0);
        }
    }
}
