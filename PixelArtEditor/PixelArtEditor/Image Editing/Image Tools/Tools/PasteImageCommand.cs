using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the PasteImage Tool.
    /// </summary>
    public class PasteImageCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The portion of the image that was edited, before being pasted on.
        /// </summary>
        private Bitmap UneditedImage { get; set; }

        /// <summary>
        /// The portion of the image that was edited, after being pasted on.
        /// </summary>
        private Bitmap EditedImage { get; set; }

        /// <summary>
        /// The location where the image was edited.
        /// </summary>
        private Point EditLocation { get; set; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="oldImage">The part of the image that was edited, before the edit.</param>
        /// <param name="newImage">The part of the image that was edited, after the edit.</param>
        /// <param name="editLocation">The location of the image where the edit was done.</param>
        public PasteImageCommand(Bitmap oldImage, Bitmap newImage, Point editLocation)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            EditLocation = editLocation;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, EditLocation);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Clears the rollback area to add transparency in case it is needed.
            imageGraphics.SetClip(new Rectangle(EditLocation, UneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(UneditedImage, EditLocation);
        }
    }
}
