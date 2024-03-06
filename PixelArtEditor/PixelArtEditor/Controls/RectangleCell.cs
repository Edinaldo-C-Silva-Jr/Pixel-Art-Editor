namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A control to represent a rectangular cell in the ColorTable controls.
    /// </summary>
    public partial class RectangleCell : Control
    {
        /// <summary>
        /// The current size of the cell.
        /// </summary>
        private int CellSize { get; set; }

        /// <summary>
        /// Whether the cell should change its appearance when it is selected or not.
        /// </summary>
        private bool VisibleSelection { get; set; }

        /// <summary>
        /// Whether the cell is currently set to its default color or not.
        /// The default color is the color the cell is set to when it is instanced.
        /// </summary>
        public bool DefaultColor { get; private set; }

        /// <summary>
        /// Constructor for a Rectangle Cell.
        /// Defines a few default values to the cell's properties: No margin, dock style to fill.
        /// </summary>
        /// <param name="cellSize">The size of the cell to be displayed.</param>
        /// <param name="color">The color used as the BackColor of the cell when it is shown.</param>
        /// <param name="selectable">Whether the cell should change its appearance when selected.</param>
        public RectangleCell(int cellSize, Color color, bool selectable)
        {
            CellSize = cellSize;
            VisibleSelection = selectable;
            DefaultColor = true;

            Dock = DockStyle.Fill;
            Size = new Size(cellSize, cellSize);
            Margin = new Padding(0, 0, 0, 0);
            BackColor = color;
            VisibleSelection = selectable;

            InitializeComponent();
        }

        /// <summary>
        /// Makes a visible change to the cell's appearance once it's selected.
        /// Does nothing if VisibleSelection is set to false.
        /// </summary>
        public void SelectCell()
        {
            if (VisibleSelection)
            {
                Size = new Size(CellSize - 4, CellSize - 4);
                Margin = new Padding(2, 2, 2, 2);
            }
        }

        /// <summary>
        /// Returns the cell to its original appearance once a new cell is selected.
        /// Does nothing if VisibleSelection is set to false.
        /// </summary>
        public void DeselectCell()
        {
            if (VisibleSelection)
            {
                Size = new Size(CellSize, CellSize);
                Margin = new Padding(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Changes the BackColor of a cell to the desired color.
        /// Also sets DefaultColor to false to signal this cell's color has been changed.
        /// </summary>
        /// <param name="color">The new color to be used in the cell.</param>
        public void ChangeCellColor(Color color)
        {
            BackColor = color;
            DefaultColor = false;
        }
    }
}
