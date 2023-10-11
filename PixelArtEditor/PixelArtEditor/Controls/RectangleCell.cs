namespace PixelArtEditor.Controls
{
    public partial class RectangleCell : UserControl
    {
        private int cellSize;
        private bool visibleSelection = true;
        private bool defaultColor = true;

        public RectangleCell(int cellSize, Color color, bool selectable)
        {
            this.cellSize = cellSize;
            this.Dock = DockStyle.Fill;
            this.Size = new Size(cellSize, cellSize);
            this.Margin = new Padding(0, 0, 0, 0);
            this.BackColor = color;
            this.visibleSelection = selectable;

            InitializeComponent();
        }

        public void SelectCell()
        {
            if (visibleSelection)
            {
                this.Size = new Size(cellSize - 4, cellSize - 4);
                this.Margin = new Padding(2, 2, 2, 2);
            }
        }

        public void DeselectCell()
        {
            if (visibleSelection)
            {
                this.Size = new Size(cellSize, cellSize);
                this.Margin = new Padding(0, 0, 0, 0);
            }
        }

        public bool IsColorDefault()
        {
            return defaultColor;
        }

        public void SetIfDefaultColor(bool isDefault)
        {
            defaultColor = isDefault;
        }
    }
}
