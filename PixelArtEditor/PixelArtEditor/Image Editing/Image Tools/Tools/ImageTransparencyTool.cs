using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ImageTransparencyTool : IImageTool, IUndoRedoCreator
    {
        private Bitmap? UneditedImage { get; set; }
        private Bitmap? EditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.MakeImageTransparent.HasValue && parameters.BackgroundColor.HasValue)
            {
                UneditedImage = new(originalImage);

                if (parameters.MakeImageTransparent.Value)
                {
                    originalImage.MakeTransparent(parameters.BackgroundColor.Value);
                }
                else
                {
                    using Bitmap temporaryImage = originalImage.Clone(new Rectangle(new Point(0, 0), originalImage.Size), PixelFormat.Format32bppArgb);
                    using Graphics imageGraphics = Graphics.FromImage(originalImage);
                    imageGraphics.Clear(parameters.BackgroundColor.Value);
                    imageGraphics.DrawImage(temporaryImage, 0, 0);
                }

                EditedImage = new(originalImage);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (UneditedImage is not null && EditedImage is not null)
            {
                ImageTransparencyCommand command = new(new(UneditedImage), new(EditedImage));
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
