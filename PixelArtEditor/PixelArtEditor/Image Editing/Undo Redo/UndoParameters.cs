namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    public class UndoParameters
    {
        public Point? DrawingImageLocation { get; set; }

        public Color? BackgroundColor { get; set; }

        public Color? OldColor { get; set; }

        public Color? NewColor { get; set; }

        public Action<Color>? ChangeCellColor { get; set; }

        public Action<Bitmap>? UpdateOriginalImage { get; set; }

        public Action<int, int>? ChangeOriginalImageSize { get; set; }

        public bool? MakeImageTransparent { get; set; }
    }
}
