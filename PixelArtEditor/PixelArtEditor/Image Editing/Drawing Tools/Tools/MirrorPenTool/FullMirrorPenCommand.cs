using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the FullMirrorPen tool.
    /// </summary>
    public class FullMirrorPenCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The top left portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap TopLeftUneditedImage { get; }

        /// <summary>
        /// The bottom right portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap BottomRightUneditedImage { get; }

        /// <summary>
        /// The top left portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap TopLeftEditedImage { get; }

        /// <summary>
        /// The bottom right portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap BottomRightEditedImage { get; }

        /// <summary>
        /// The four locations of the original image where the edit was done.
        /// </summary>
        private (Point topLeft, Point bottomRight) EditLocations { get; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="topLeftOldImage">The top left part of the image that was drawn on, before it was edited.</param>
        /// <param name="bottomRightOldImage">The bottom right part of the image that was drawn on, before it was edited.</param>
        /// <param name="newImage">The part of the image that was drawn on, after it was edited.</param>
        /// <param name="topLeft">The location where the top left edit was done.</param>
        /// <param name="bottomRight">The location where the bottom right edit was done.</param>
        public FullMirrorPenCommand(Bitmap topLeftOldImage, Bitmap bottomRightOldImage, Bitmap topLeftNewImage, Bitmap bottomRightNewImage, Point topLeft, Point bottomRight)
        {
            TopLeftUneditedImage = topLeftOldImage;
            BottomRightUneditedImage = bottomRightOldImage;
            TopLeftEditedImage = topLeftNewImage;
            BottomRightEditedImage = bottomRightNewImage;
            EditLocations = (topLeft, bottomRight);
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Draws each edited image to their respective locations.
            imageGraphics.DrawImage(TopLeftEditedImage, EditLocations.topLeft);
            imageGraphics.DrawImage(BottomRightEditedImage, EditLocations.bottomRight);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Draws each unedited image to their respective locations.
            imageGraphics.SetClip(new Rectangle(EditLocations.topLeft, TopLeftUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(TopLeftUneditedImage, EditLocations.topLeft);

            imageGraphics.SetClip(new Rectangle(EditLocations.bottomRight, BottomRightUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(BottomRightUneditedImage, EditLocations.bottomRight);
        }
    }
}
