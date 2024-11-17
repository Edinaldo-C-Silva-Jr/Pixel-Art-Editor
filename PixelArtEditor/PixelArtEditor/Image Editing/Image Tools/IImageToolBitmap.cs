namespace PixelArtEditor.Image_Editing.Image_Tools
{
    /// <summary>
    /// An interface that defines methods used by Full Image Tools.
    /// These tools affect the entire Original Image rather than just the Drawing Image.
    /// </summary>
    public interface IImageToolBitmap
    {
        /// <summary>
        /// Implementes the function of the current tool.
        /// </summary>
        /// <param name="originalImage">The original image, that will be affected by the tool.</param>
        public Bitmap UseTool(Bitmap originalImage, ImageToolParameters parameters);
    }
}
