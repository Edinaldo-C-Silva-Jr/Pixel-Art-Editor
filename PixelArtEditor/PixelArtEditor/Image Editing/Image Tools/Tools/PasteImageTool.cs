using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A tool used to paste a copied portion into an image, which can be the DrawingImage or the OriginalImage,
    /// It uses an existing method provided by the Image Handler to paste the corresponding Clipboard Image into the location selected in the image.
    /// </summary>
    public class PasteImageTool : IImageTool, IUndoRedoCreator
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
            if (parameters.PasteLocation.HasValue && parameters.ClipboardImageSize.HasValue && parameters.ImageSize.HasValue && parameters.PasteImage is not null)
            {
                int pasteImageWidth = parameters.ClipboardImageSize.Value.Width;
                int pasteImageheight = parameters.ClipboardImageSize.Value.Height;

                // Checks if the pasted portion would extend outside the width of the image...
                if (parameters.PasteLocation.Value.X + parameters.ClipboardImageSize.Value.Width > parameters.ImageSize.Value.Width)
                {
                    // If it does, reduce the portion width to coincide with the end of the image.
                    pasteImageWidth = parameters.ImageSize.Value.Width - parameters.PasteLocation.Value.X;
                }

                // Checks if the pasted portion would extend outside the height of the image...
                if (parameters.PasteLocation.Value.Y + parameters.ClipboardImageSize.Value.Height > parameters.ImageSize.Value.Height)
                {
                    // If it does, reduce the portion height to coincide with the end of the image.
                    pasteImageheight = parameters.ImageSize.Value.Height- parameters.PasteLocation.Value.Y;
                }

                // Created a new clipboard area that stays within the dimensions of the image.
                // This area is then used to define the portion before and after pasting that will be used by the undo function.
                Rectangle clipboardArea = new(parameters.PasteLocation.Value, new Size(pasteImageWidth, pasteImageheight));

                UneditedImage = originalImage.Clone(clipboardArea, PixelFormat.Format32bppArgb);

                parameters.PasteImage(parameters.PasteLocation.Value);

                EditedImage = originalImage.Clone(clipboardArea, PixelFormat.Format32bppArgb);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (UneditedImage is not null && EditedImage is not null && parameters.PasteLocation.HasValue)
            {
                PasteImageCommand command = new(new(UneditedImage), new(EditedImage), parameters.PasteLocation.Value);
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
