using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the FourMirrorPen tool.
    /// </summary>
    public class FourMirrorPenCommand : IUndoRedoCommand
    {
        /// <summary>
        /// The top left portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap TopLeftUneditedImage { get; }

        /// <summary>
        /// The top right portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap TopRightUneditedImage { get; }

        /// <summary>
        /// The bottom left portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap BottomLeftUneditedImage { get; }

        /// <summary>
        /// The bottom right portion of the drawing image that was drawn on, before it was edited.
        /// </summary>
        private Bitmap BottomRightUneditedImage { get; }

        /// <summary>
        /// The top left portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap TopLeftEditedImage { get; }

        /// <summary>
        /// The top right portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap TopRightEditedImage { get; }

        /// <summary>
        /// The bottom left portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap BottomLeftEditedImage { get; }

        /// <summary>
        /// The bottom right portion of the drawing image that was drawn on, after it was edited.
        /// </summary>
        private Bitmap BottomRightEditedImage { get; }

        /// <summary>
        /// The four locations of the original image where the edit was done.
        /// </summary>
        private (Point topLeft, Point topRight, Point bottomLeft, Point bottomRight) EditLocations { get; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="topLeftOldImage">The top left part of the image that was drawn on, before it was edited.</param>
        /// <param name="topRightOldImage">The top right part of the image that was drawn on, before it was edited.</param>
        /// <param name="bottomLeftOldImage">The bottom left part of the image that was drawn on, before it was edited.</param>
        /// <param name="bottomRightOldImage">The bottom right part of the image that was drawn on, before it was edited.</param>
        /// <param name="newImage">The part of the image that was drawn on, after it was edited.</param>
        /// <param name="topLeft">The location where the top left edit was done.</param>
        /// <param name="topRight">The location where the top right edit was done.</param>
        /// <param name="bottomLeft">The location where the bottom left edit was done.</param>
        /// <param name="bottomRight">The location where the bottom right edit was done.</param>
        public FourMirrorPenCommand(Bitmap topLeftOldImage, Bitmap topRightOldImage, Bitmap bottomLeftOldImage, Bitmap bottomRightOldImage,
            Bitmap topLeftNewImage, Bitmap topRightNewImage, Bitmap bottomLeftNewImage, Bitmap bottomRightNewImage,
            Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            TopLeftUneditedImage = topLeftOldImage;
            TopRightUneditedImage = topRightOldImage;
            BottomLeftUneditedImage = bottomLeftOldImage;
            BottomRightUneditedImage = bottomRightOldImage;
            TopLeftEditedImage = topLeftNewImage;
            TopRightEditedImage = topRightNewImage;
            BottomLeftEditedImage = bottomLeftNewImage;
            BottomRightEditedImage = bottomRightNewImage;
            EditLocations = (topLeft, topRight, bottomLeft, bottomRight);
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            // Draws each edited image to their respective locations.
            imageGraphics.DrawImage(TopLeftEditedImage, EditLocations.topLeft);
            imageGraphics.DrawImage(TopRightEditedImage, EditLocations.topRight);
            imageGraphics.DrawImage(BottomLeftEditedImage, EditLocations.bottomLeft);
            imageGraphics.DrawImage(BottomRightEditedImage, EditLocations.bottomRight);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            // Draws each unedited image to their respective locations.
            imageGraphics.SetClip(new Rectangle(EditLocations.topLeft, TopLeftUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(TopLeftUneditedImage, EditLocations.topLeft);

            imageGraphics.SetClip(new Rectangle(EditLocations.topRight, TopRightUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(TopRightUneditedImage, EditLocations.topRight);

            imageGraphics.SetClip(new Rectangle(EditLocations.bottomLeft, BottomLeftUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(BottomLeftUneditedImage, EditLocations.bottomLeft);

            imageGraphics.SetClip(new Rectangle(EditLocations.bottomRight, BottomRightUneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(BottomRightUneditedImage, EditLocations.bottomRight);
        }
    }
}
