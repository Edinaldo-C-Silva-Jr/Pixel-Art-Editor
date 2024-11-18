using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ResizeImageCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Bitmap EditedImage { get; set; }

        public Action<Bitmap> UpdateOriginalImage { get; set; }

        public Action<int, int> ChangeOriginalImageSize { get; set; }

        public ResizeImageCommand(Bitmap oldImage, Bitmap newImage, Action<Bitmap> updateOriginalImage, Action<int, int> changeOriginalImageSize)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            UpdateOriginalImage = updateOriginalImage;
            ChangeOriginalImageSize = changeOriginalImageSize;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            ChangeOriginalImageSize(EditedImage.Width, EditedImage.Height);
            UpdateOriginalImage(EditedImage);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            ChangeOriginalImageSize(UneditedImage.Width, UneditedImage.Height);
            UpdateOriginalImage(UneditedImage);
        }
    }
}
