using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ImageTransparencyCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Bitmap EditedImage { get; set; }

        public ImageTransparencyCommand(Bitmap oldImage, Bitmap newImage)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(EditedImage, 0, 0);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(UneditedImage, 0, 0);
        }
    }
}
