using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    internal class ImageTransparencyTool : IImageTool, IUndoRedoCreator
    {
        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.MakeImageTransparent.HasValue && parameters.BackgroundColor.HasValue)
            {
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
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            return null;
        }
    }
}
