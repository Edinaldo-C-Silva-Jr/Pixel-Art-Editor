namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the HorizontalMirrorPen tool.
    /// </summary>
    public class HorizontalMirrorPenCommand
    {
        /// <summary>
        /// The left side portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap LeftUneditedImage { get; }

        /// <summary>
        /// The right side portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap RightUneditedImage { get; }

        /// <summary>
        /// The portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap EditedImage { get; }

        /// <summary>
        /// The two locations of the original image where the edit was done.
        /// </summary>
        private (Point left, Point right) EditLocations { get; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="leftOldImage">The left part of the image that was drawn on, before it was edited.</param>
        /// <param name="rightOldImage">The right part of the image that was drawn on, before it was edited.</param>
        /// <param name="newImage">The part of the image that was drawn on, after it was edited.</param>
        /// <param name="locations">The two locations of the original image where the edits were done.</param>
        public HorizontalMirrorPenCommand(Bitmap leftOldImage, Bitmap rightOldImage, Bitmap newImage, (Point left, Point right) locations)
        {
            LeftUneditedImage = leftOldImage;
            RightUneditedImage = rightOldImage;
            EditedImage = newImage;
            EditLocations = locations;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Draws the edited image on the left edit point.
            imageGraphics.DrawImage(EditedImage, EditLocations.left);

            // Flips the edited image horizontally.
            using Bitmap mirroredImage = new(LeftUneditedImage.Width, LeftUneditedImage.Height);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            // Draws the mirrored edited image on the right edit point.
            imageGraphics.DrawImage(mirroredImage, EditLocations.right);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Draws each unedited image to their respective locations.
            imageGraphics.DrawImage(LeftUneditedImage, EditLocations.left);
            imageGraphics.DrawImage(RightUneditedImage, EditLocations.right);
        }
    }
}
