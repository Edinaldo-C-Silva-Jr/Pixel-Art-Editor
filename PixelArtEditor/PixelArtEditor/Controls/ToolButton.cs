namespace PixelArtEditor.Controls
{
    public partial class ToolButton : Button
    {
        public byte ToolValue { get; set; }
        public bool UseStartingPoint { get; set; }
        public bool UseEndPoint { get; set; }
        public bool UseImageSize { get; set; }

        public ToolButton()
        {
            Size = new(40, 40);
            InitializeComponent();
        }
    }
}
