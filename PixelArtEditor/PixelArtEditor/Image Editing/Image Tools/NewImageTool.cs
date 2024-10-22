using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools
{
    public class NewImageTool : BaseImageTool
    {
        Bitmap? UneditedImage { get; set; }

        Color? BackgroundColor { get; set; }

        public override void UseTool(Bitmap OriginalImage, OptionalImageParameters parameters)
        {
            if (parameters.BackgroundColor.HasValue)
            {
                BackgroundColor = parameters.BackgroundColor.Value;
                UneditedImage = new(OriginalImage);

                using Graphics imageGraphics = Graphics.FromImage(OriginalImage);
                imageGraphics.Clear(parameters.BackgroundColor.Value);
            }
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            NewImageCommand command = new(new(UneditedImage!), BackgroundColor!.Value);
            ClearProperties();
            return command;
        }

        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            BackgroundColor = null;
        }
    }
}
