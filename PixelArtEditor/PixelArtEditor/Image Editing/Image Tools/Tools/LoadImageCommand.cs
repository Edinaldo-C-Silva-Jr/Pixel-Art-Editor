using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    internal class LoadImageCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Bitmap EditedImage { get; set; }

        private Color BackgroundColor { get; set; }

        public LoadImageCommand(Bitmap oldImage, Bitmap newImage, Color backgroundColor)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            BackgroundColor = backgroundColor;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, 0, 0);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.Clear(BackgroundColor);
            imageGraphics.DrawImage(UneditedImage, 0, 0);
        }
    }
}
