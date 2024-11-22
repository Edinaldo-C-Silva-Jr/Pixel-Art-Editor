using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.PenTool
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by Pen type tools.
    /// </summary>
    public class PenCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap UneditedImage { get; }

        /// <summary>
        /// The portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap EditedImage { get; }

        /// <summary>
        /// The location of the original image where the edit was done.
        /// </summary>
        private Point EditLocation { get; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="oldImage">The part of the image that was drawn on, before it was edited.</param>
        /// <param name="newImage">The part of the image that was drawn on, after it was edited.</param>
        /// <param name="location">The location of the original image where the edit was done.</param>
        public PenCommand(Bitmap oldImage, Bitmap newImage, Point location)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            EditLocation = location;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, EditLocation);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.SetClip(new Rectangle(EditLocation, UneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(UneditedImage, EditLocation);
        }
    }
}
