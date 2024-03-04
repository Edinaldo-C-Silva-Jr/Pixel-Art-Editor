namespace PixelArtEditor.Controls
{
    public partial class ToolButton : Button
    {
        public byte ToolValue { get; set; }
        public bool UseBeginPoint { get; set; }
        public bool UseEndPoint { get; set; }
        public bool UseImageSize { get; set; }
        public bool UseTransparency { get; set; }
        public bool UseBackgroundColor { get; set; }
        public bool UsePixelSize { get; set; }

        public ToolButton()
        {
            Size = new(40, 40);
            InitializeComponent();
        }
    }
}
