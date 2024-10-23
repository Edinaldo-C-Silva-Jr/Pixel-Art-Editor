using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class NewImageCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Color BackgroundColor { get; set; }

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
            imageGraphics.DrawImage(UneditedImage, 0, 0);
        }
    }
}
