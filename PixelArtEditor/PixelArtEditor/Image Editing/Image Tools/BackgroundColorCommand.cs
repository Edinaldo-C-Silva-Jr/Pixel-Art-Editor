using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools
{
    public class BackgroundColorCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Bitmap EditedImage { get; set; }

        public BackgroundColorCommand(Bitmap oldImage, Bitmap newImage)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, 0, 0);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(UneditedImage, 0, 0);
        }
    }
}
