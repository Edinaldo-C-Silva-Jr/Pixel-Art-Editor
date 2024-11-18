namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ResizeImageTool : IImageTool
    {
        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.OriginalImagesize.HasValue && parameters.UpdateOriginalImage is not null)
            {
                // Creates a new image with the currently defined size.
                using Bitmap imageWithNewSize = new(parameters.OriginalImagesize.Value.Width, parameters.OriginalImagesize.Value.Height);
                using Graphics newSizeGraphics = Graphics.FromImage(imageWithNewSize);

                // Draws the Original Image on top of the new image, then assigns it to the Original Image.
                newSizeGraphics.Clear(Color.White);
                newSizeGraphics.DrawImage(originalImage, 0, 0);

                parameters.UpdateOriginalImage(imageWithNewSize);
            }
        }
    }
}
