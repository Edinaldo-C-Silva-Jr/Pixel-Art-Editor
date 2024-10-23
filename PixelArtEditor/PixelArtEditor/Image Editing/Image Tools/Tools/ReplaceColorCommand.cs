using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ReplaceColorCommand : IUndoRedoCommand
    {
        private Bitmap UneditedImage { get; set; }

        private Bitmap EditedImage { get; set; }

        private Action<Color> ChangeCellColor { get; set; }

        private Color OldColor { get; set; }

        private Color NewColor { get; set; }

        public ReplaceColorCommand(Bitmap oldImage, Bitmap newImage, Action<Color> changeCellColor, Color oldColor, Color newColor)
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
            ChangeCellColor.Invoke(NewColor);
        }

        public void RollbackChange(Graphics imageGraphics)
        {
            imageGraphics.DrawImage(UneditedImage, 0, 0);
            ChangeCellColor.Invoke(OldColor);
        }
    }
}
