using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Pen
{
    internal class PenCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; }
        private Bitmap EditedImage { get; }
        private Point EditLocation { get; }

        public PenCommand(Bitmap oldImage, Bitmap newImage, Point location)
        {
            UneditedImage = oldImage;
            EditedImage = newImage;
            EditLocation = location;
        }

        public void ExecuteChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(EditedImage, EditLocation);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(UneditedImage, EditLocation);
        }
    }
}
