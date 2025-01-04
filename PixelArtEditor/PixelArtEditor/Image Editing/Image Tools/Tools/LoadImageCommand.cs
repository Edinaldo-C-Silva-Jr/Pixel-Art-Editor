using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the LoadImage Tool.
    /// </summary>
    public class LoadImageCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The old image, before a new one was loaded.
        /// </summary>
        private Bitmap UneditedImage { get; set; }

        /// <summary>
        /// The new image loaded by the tool.
        /// </summary>
        private Bitmap EditedImage { get; set; }

        /// <summary>
        /// The background color of the image before the edit was done, to restore it when rolling back.
        /// </summary>
        private Color BackgroundColor { get; set; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="oldImage">The old image, before loading a new one.</param>
        /// <param name="newImage">The new image that was loaded..</param>
        /// <param name="backgroundColor">The background color of the old image.</param>
        public LoadImageCommand(Bitmap oldImage, Bitmap newImage, Color backgroundColor)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            BackgroundColor = backgroundColor;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, 0, 0);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Clears the image with the background color to restore it in case the image is transparent.
            imageGraphics.Clear(BackgroundColor);
            imageGraphics.DrawImage(UneditedImage, 0, 0);
        }
    }
}
