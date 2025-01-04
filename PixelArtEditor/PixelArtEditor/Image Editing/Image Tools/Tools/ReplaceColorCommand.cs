using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A command that has the data and methods necessary to undo and redo actions made by the ReplaceColor Tool.
    /// </summary>
    public class ReplaceColorCommand : IUndoRedoCommand
    {
        /// <summary>
        /// A copy of the image before it was edited.
        /// </summary>
        private Bitmap UneditedImage { get; set; }

        /// <summary>
        /// A copy of the image after it was edited.
        /// </summary>
        private Bitmap EditedImage { get; set; }

        /// <summary>
        /// A method that changes the color of a ColorTable cell.
        /// This is used to change the BackgroundColorTable color when undoing a change made to the background color.
        /// </summary>
        private Action<Color>? ChangeCellColor { get; set; }

        /// <summary>
        /// The old color that was replaced.
        /// </summary>
        private Color OldColor { get; set; }

        /// <summary>
        /// The new color that replaced the old one.
        /// </summary>
        private Color NewColor { get; set; }

        /// <summary>
        /// Default constructor. Receives the necessary data to create the command.
        /// </summary>
        /// <param name="oldImage">The old image, before replacing the color.</param>
        /// <param name="newImage">The new image, after the color was replaced.</param>
        /// <param name="changeCellColor">The method to change the color of the ColorTable cell.</param>
        /// <param name="oldColor">The color that was replaced.</param>
        /// <param name="newColor">The new color that replaced the old one.</param>
        public ReplaceColorCommand(Bitmap oldImage, Bitmap newImage, Action<Color>? changeCellColor, Color oldColor, Color newColor)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            ChangeCellColor = changeCellColor;
            OldColor = oldColor;
            NewColor = newColor;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, 0, 0);
            ChangeCellColor?.Invoke(NewColor);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(UneditedImage, 0, 0);
            ChangeCellColor?.Invoke(OldColor);
        }
    }
}
