using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the VerticalMirrorPen tool.
    /// </summary>
    public class VerticalMirrorPenCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The top portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap UpperUneditedImage { get; }

        /// <summary>
        /// The bottom portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap LowerUneditedImage { get; }

        /// <summary>
        /// The portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap EditedImage { get; }

        /// <summary>
        /// The two locations of the original image where the edit was done.
        /// </summary>
        private (Point upper, Point lower) EditLocations { get; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="upperOldImage">The upper part of the image that was drawn on, before it was edited.</param>
        /// <param name="lowerOldImage">The lower part of the image that was drawn on, before it was edited.</param>
        /// <param name="newImage">The part of the image that was drawn on, after it was edited.</param>
        /// <param name="upperLocation">The location where the upper edit was done.</param>
        /// <param name="lowerLocation">The location where the lower edit was done.</param>
        public VerticalMirrorPenCommand(Bitmap upperOldImage, Bitmap lowerOldImage, Bitmap newImage, Point upperLocation, Point lowerLocation)
        {
            UpperUneditedImage = upperOldImage;
            LowerUneditedImage = lowerOldImage;
            EditedImage = newImage;
            EditLocations = (upperLocation, lowerLocation);
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Draws the edited image on the upper edit point.
            imageGraphics.DrawImage(EditedImage, EditLocations.upper);

            // Flips the edited image vertically.
            using Bitmap mirroredImage = new(EditedImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

            // Draws the mirrored edited image on the lower edit point.
            imageGraphics.DrawImage(mirroredImage, EditLocations.lower);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Draws each unedited image to their respective locations.
            imageGraphics.SetClip(new Rectangle(EditLocations.upper, UpperUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(UpperUneditedImage, EditLocations.upper);

            imageGraphics.SetClip(new Rectangle(EditLocations.lower, LowerUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(LowerUneditedImage, EditLocations.lower);
        }
    }
}
