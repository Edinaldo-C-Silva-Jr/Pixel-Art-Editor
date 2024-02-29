namespace PixelArtEditor.Controls
{
    public partial class ToolButton : Button
    {
        public byte ToolValue { get; set; }

        public ToolButton()
        {
            Size = new(40, 40);
            InitializeComponent();
        }

        private void ToolButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
        }
    }
}
