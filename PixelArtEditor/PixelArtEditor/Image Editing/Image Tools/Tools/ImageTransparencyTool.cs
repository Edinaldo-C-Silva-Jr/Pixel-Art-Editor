using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A tool used to change transparency in the image, making its background transparent, or a solid color.
    /// </summary>
    public class ImageTransparencyTool : IImageTool, IUndoRedoCreator
    {
        /// <summary>
        /// A copy of the image before it was edited.
        /// </summary>
        private Bitmap? UneditedImage { get; set; }

        /// <summary>
        /// A copy of the image after it was edited.
        /// </summary>
        private Bitmap? EditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.MakeImageTransparent.HasValue && parameters.BackgroundColor.HasValue)
            {
                UneditedImage = new(originalImage);

                if (parameters.MakeImageTransparent.Value)
                {
                    // Makes the image transparent.
                    originalImage.MakeTransparent(parameters.BackgroundColor.Value);
                }
                else
                {
                    // Draws the image on top of an image with a solid color to remove the transparency.
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

        /// <summary>
        /// Disposes of unmanaged resources and clears properties used by the tool.
        /// </summary>
        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            EditedImage?.Dispose();
            EditedImage = null;
        }
    }
}
