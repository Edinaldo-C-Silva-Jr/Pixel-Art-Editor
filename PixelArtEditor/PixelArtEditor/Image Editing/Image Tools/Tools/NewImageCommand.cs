using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the NewImage Tool.
    /// </summary>
    public class NewImageCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The old image, before being replaced by a blank one.
        /// </summary>
        private Bitmap UneditedImage { get; set; }

        /// <summary>
        /// The background color of the image, used to clear the image and create a blank one.
        /// </summary>
        private Color BackgroundColor { get; set; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="oldImage">The old image that was replaced by the tool.</param>
        /// <param name="backgroundColor">The background color of the image.</param>

        public NewImageCommand(Bitmap oldImage, Color backgroundColor)
        {
            UneditedImage = oldImage;
            BackgroundColor = backgroundColor;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.Clear(BackgroundColor);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(UneditedImage, 0, 0);
        }
    }
}
