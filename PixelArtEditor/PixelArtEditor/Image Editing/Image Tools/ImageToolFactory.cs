using PixelArtEditor.Image_Editing.Image_Tools.Tools;

namespace PixelArtEditor.Image_Editing.Image_Tools
{
    /// <summary>
    /// A Factory that generates Image Tools.
    /// </summary>
    public class ImageToolFactory
    {
        /// <summary>
        /// The currently generated tool. It is stored to be reused throughout the application for as long as no new tool is selected.
        /// </summary>
        private IImageTool Tool { get; set; }

        /// <summary>
        /// Default constructor. The default tool chosen is a New Image Tool.
        /// </summary>
        public ImageToolFactory()
        {
            Tool = new NewImageTool();
        }

        /// <summary>
        /// Generates a new Image Tool based on the value passed.
        /// </summary>
        /// <param name="toolValue">The value of the tool to be used.</param>
        /// <returns>A new instance of the tool that corresponds to the tool value.</returns>
        public IImageTool ChangeCurrentTool(int toolValue)
        {
            Tool = toolValue switch
            {
                4 => new ResizeImageTool(),
                3 => new LoadImageTool(),
                2 => new SaveImageTool(),
                1 => new ReplaceColorTool(),
                _ => new NewImageTool()
            };

            return Tool;
        }
    }
}
