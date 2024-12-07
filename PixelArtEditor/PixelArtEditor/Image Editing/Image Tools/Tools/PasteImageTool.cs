using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class PasteImageTool : IImageTool, IUndoRedoCreator
    {
        private Bitmap? UneditedImage { get; set; }
        private Bitmap? EditedImage { get; set; }
        private Point? EditLocation { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.PasteLocation.HasValue && parameters.ClipboardImageSize.HasValue && parameters.ImageSize.HasValue && parameters.PasteImage is not null)
            {
                int pasteImageWidth = parameters.ClipboardImageSize.Value.Width;
                int pasteImageheight = parameters.ClipboardImageSize.Value.Height;

                if (parameters.PasteLocation.Value.X + parameters.ClipboardImageSize.Value.Width > parameters.ImageSize.Value.Width)
                {
                    pasteImageWidth = parameters.ImageSize.Value.Width - parameters.PasteLocation.Value.X;
                }

                if (parameters.PasteLocation.Value.Y + parameters.ClipboardImageSize.Value.Height > parameters.ImageSize.Value.Height)
                {
                    pasteImageheight = parameters.ImageSize.Value.Height- parameters.PasteLocation.Value.Y;
                }

                Rectangle clipboardArea = new(parameters.PasteLocation.Value, new Size(pasteImageWidth, pasteImageheight));

                UneditedImage = originalImage.Clone(clipboardArea, PixelFormat.Format32bppArgb);
                EditLocation = parameters.PasteLocation.Value;

                parameters.PasteImage(parameters.PasteLocation.Value);

                EditedImage = originalImage.Clone(clipboardArea, PixelFormat.Format32bppArgb);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (UneditedImage is not null && EditedImage is not null && EditLocation.HasValue)
            {
                PasteImageCommand command = new(new(UneditedImage), new(EditedImage), EditLocation.Value);
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

            EditLocation = null;
        }
    }
}
