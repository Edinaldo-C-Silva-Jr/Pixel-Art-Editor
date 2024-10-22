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
        /// The portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap EditedImage { get; }

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
        public FullMirrorPenCommand(Bitmap topLeftOldImage, Bitmap bottomRightOldImage, Bitmap newImage, Point topLeft, Point bottomRight)
        {
            TopLeftUneditedImage = topLeftOldImage;
            BottomRightUneditedImage = bottomRightOldImage;
            EditedImage = newImage;
            EditLocations = (topLeft, bottomRight);
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Draws the edited image on the top left edit point.
            imageGraphics.DrawImage(EditedImage, EditLocations.topLeft);

            using Bitmap mirroredImage = new(EditedImage);

            // Flips the edited image horizontally and draws it on the bottom right edit point.
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            imageGraphics.DrawImage(mirroredImage, EditLocations.bottomRight);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Draws each unedited image to their respective locations.
            imageGraphics.DrawImage(TopLeftUneditedImage, EditLocations.topLeft);
            imageGraphics.DrawImage(BottomRightUneditedImage, EditLocations.bottomRight);
        }
    }
}
