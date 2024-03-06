namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A button control that is used to select the Drawing Tools to use in the editor.
    /// It has several properties to define the behavior of each tool.
    /// </summary>
    public partial class ToolButton : Button
    {
        /// <summary>
        /// The numeric value of the tool tied to this button, which defines which tool will be used when this button is clicked.
        /// </summary>
        public byte ToolValue { get; set; }

        /// <summary>
        /// Defines whether or not the tool will show a preview when moving the mouse on the Drawing Box.
        /// </summary>
        public bool PreviewOnMove { get; set; }
        /// <summary>
        /// Defines whether or not the tool will show a preview when clicking and holding on the Drawing Box.
        /// </summary>
        public bool PreviewOnHold { get; set; }

        /// <summary>
        /// Defines if the tool will require the BeginPoint parameter to be used.
        /// </summary>
        public bool UseClickLocation { get; set; }
        /// <summary>
        /// Defines if the tool will require the ImageSize parameter to be used.
        /// </summary>
        public bool UseImageSize { get; set; }
        /// <summary>
        /// Defines if the tool will require the Transparency parameter to be used.
        /// </summary>
        public bool UseTransparency { get; set; }
        /// <summary>
        /// Defines if the tool will require the BackgroundColor parameter to be used.
        /// </summary>
        public bool UseBackgroundColor { get; set; }
        /// <summary>
        /// Defines if the tool will require the PixelSize parameter to be used.
        /// </summary>
        public bool UsePixelSize { get; set; }

        /// <summary>
        /// Default constructor, defines a standard size for the button.
        /// </summary>
        public ToolButton()
        {
            Size = new(40, 40);
            InitializeComponent();
        }
    }
}
