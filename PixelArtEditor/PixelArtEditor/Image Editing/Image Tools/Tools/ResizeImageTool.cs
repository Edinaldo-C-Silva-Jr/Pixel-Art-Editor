using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ResizeImageTool : IImageTool, IUndoRedoCreator
    {
        private Bitmap? UneditedImage { get; set; }
        private Bitmap? EditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.OriginalImageSize.HasValue && parameters.BackgroundColor.HasValue && parameters.UpdateOriginalImage is not null)
            {
                UneditedImage = new(originalImage);
                // Creates a new image with the currently defined size.
                using Bitmap imageWithNewSize = new(parameters.OriginalImageSize.Value.Width, parameters.OriginalImageSize.Value.Height);
                using Graphics newSizeGraphics = Graphics.FromImage(imageWithNewSize);

                // Draws the Original Image on top of the new image, then assigns it to the Original Image.
                newSizeGraphics.Clear(parameters.BackgroundColor.Value);
                newSizeGraphics.DrawImage(originalImage, 0, 0);

                parameters.UpdateOriginalImage(imageWithNewSize);
                EditedImage = new(imageWithNewSize);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.UpdateOriginalImage is not null && parameters.ChangeOriginalImageSize is not null && parameters.ChangeViewNumberBoxes is not null
                && UneditedImage is not null && EditedImage is not null)
            {
                ResizeImageCommand command = new(new(UneditedImage), new(EditedImage), parameters.UpdateOriginalImage, parameters.ChangeOriginalImageSize, parameters.ChangeViewNumberBoxes);
                ClearProperties();
                return command;
            }
            else
            {
                return null;
            }
        }

        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            EditedImage?.Dispose();
            EditedImage = null;
        }
    }
}
