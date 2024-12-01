using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ResizeImageCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Bitmap EditedImage { get; set; }

        public Action<Bitmap> UpdateOriginalImage { get; set; }

        public Action<int, int> ChangeOriginalImageSize { get; set; }

        public Action ChangeViewNumberBoxes { get; set; }

        public ResizeImageCommand(Bitmap oldImage, Bitmap newImage, Action<Bitmap> updateOriginalImage, Action<int, int> changeOriginalImageSize, Action changeViewNumberBoxes)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            UpdateOriginalImage = updateOriginalImage;
            ChangeOriginalImageSize = changeOriginalImageSize;
            ChangeViewNumberBoxes = changeViewNumberBoxes;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            ChangeOriginalImageSize(EditedImage.Width, EditedImage.Height);
            UpdateOriginalImage(EditedImage);
            ChangeViewNumberBoxes();
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            ChangeOriginalImageSize(UneditedImage.Width, UneditedImage.Height);
            UpdateOriginalImage(UneditedImage);
            ChangeViewNumberBoxes();
        }
    }
}
