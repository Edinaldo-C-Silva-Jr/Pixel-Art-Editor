using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class PasteImageCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Bitmap EditedImage { get; set; }

        private Point EditLocation { get; set; }

        public PasteImageCommand(Bitmap oldImage, Bitmap newImage, Point editLocation)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            EditLocation = editLocation;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, EditLocation);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.SetClip(new Rectangle(EditLocation, UneditedImage.Size));
            imageGraphics.Clear(Color.Transparent);
            imageGraphics.DrawImage(UneditedImage, EditLocation);
        }
    }
}
