using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the HorizontalMirrorPen tool.
    /// </summary>
    public class HorizontalMirrorPenCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The left portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap LeftUneditedImage { get; }

        /// <summary>
        /// The right portion of the drawing image that was drawn on, before it was edited.
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
        /// <param name="leftLocation">The location where the left edit was done.</param>
        /// <param name="rightLocation">The location where the right edit was done.</param>
        public HorizontalMirrorPenCommand(Bitmap leftOldImage, Bitmap rightOldImage, Bitmap newImage, Point leftLocation, Point rightLocation)
        {
            LeftUneditedImage = leftOldImage;
            RightUneditedImage = rightOldImage;
            EditedImage = newImage;
            EditLocations = (leftLocation, rightLocation);
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Draws the edited image on the left edit point.
            imageGraphics.DrawImage(EditedImage, EditLocations.left);

            // Flips the edited image horizontally.
            using Bitmap mirroredImage = new(EditedImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            // Draws the mirrored edited image on the right edit point.
            imageGraphics.DrawImage(mirroredImage, EditLocations.right);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Draws each unedited image to their respective locations.
            imageGraphics.SetClip(new Rectangle(EditLocations.left, LeftUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(LeftUneditedImage, EditLocations.left);


            imageGraphics.SetClip(new Rectangle(EditLocations.right, RightUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(RightUneditedImage, EditLocations.right);
        }
    }
}
