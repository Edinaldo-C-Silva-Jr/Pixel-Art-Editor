namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A control that implements a Table Layout Panel to allow picking the colors used in the Pixel Art Editor.
    /// This table uses RectangleCell controls as individual cells.
    /// </summary>
    public partial class ColorTable : TableLayoutPanel
    {
        private int currentCellRow = 0, currentCellColumn = 0;

        /// <summary>
        /// Constructor method of the Color Table, which enables the DoubleBuffered property.
        /// </summary>
        public ColorTable()
        {
            DoubleBuffered = true;
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            InitializeComponent();
        }

        /// <summary>
        /// Generates a grid of RectangleCells into the Color Table, which allows selecting colors.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <param name="cellSize">The size of each cell in the Color Table.</param>
        /// <param name="cellClick">The Click event handler to use on each cell's click event.</param>
        /// <param name="defaultCellColor">The color set as the default color on all cells.</param>
        /// <param name="visiblySelectable">Whether the cells will change visually when selected or not.</param>
        public void GenerateColorGrid(int colorAmount, int cellSize, EventHandler cellClick, Color defaultCellColor, bool visiblySelectable)
        {
            GenerateCellGrid(colorAmount, cellSize, cellClick, defaultCellColor, visiblySelectable);
        }

        /// <summary>
        /// Generates a grid of RectangleCells into the Color Table, which allows selecting colors.
        /// This method defines the color of all cells as white, and makes the cells change visually when selected.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <param name="cellSize">The size of each cell in the Color Table.</param>
        /// <param name="cellClick">The Click event handler to use on each cell's click event.</param>
        public void GenerateColorGrid(int colorAmount, int cellSize, EventHandler cellClick)
        {
            GenerateCellGrid(colorAmount, cellSize, cellClick, Color.White, true);
        }

        /// <summary>
        /// Generates the grid, creating all cells and defining their properties, then adds them onto the Color Table.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <param name="cellSize">The size of each cell in the Color Table.</param>
        /// <param name="cellClick">The Click event handler to use on each cell's click event.</param>
        /// <param name="defaultCellColor">The color set as the default color on all cells.</param>
        /// <param name="visiblySelectable">Whether the cells will change visually when selected or not.</param>
        private void GenerateCellGrid(int colorAmount, int cellSize, EventHandler cellClick, Color defaultCellColor, bool visiblySelectable)
        {
            SuspendLayout();

            int rows = DefineRows(colorAmount);
            int columns = DefineColumns(colorAmount);

            // The size of the table is equal to:
            // The size of all cells combined + 1 pixel for each cell, to account for the margin + 1 pixel for the final margin.
            // Thus it becomes: (cell size + 1) * amount of cells + 1
            Size = new Size((cellSize + 1) * columns + 1, (cellSize + 1) * rows + 1);

            // Clears the previous table and defines the new amount of rows and columns.
            Controls.Clear();
            RowStyles.Clear();
            ColumnStyles.Clear();
            RowCount = rows;
            ColumnCount = columns;

            // Rebuilds the table using the new cells, creating each new cell with the defined parameters.
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    RectangleCell cellTemplate = new(cellSize, defaultCellColor, visiblySelectable);
                    cellTemplate.Click += cellClick;
                    Controls.Add(cellTemplate, x, y);
                }
            }

            ResumeLayout();
            Refresh();
        }

        /// <summary>
        /// Defines the amount of rows the Color Table can have.
        /// Due to screen space concerns, any row can only have a maximum of 16 cells.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <returns>The amount of rows used for the amount of color cells received.</returns>
        private static int DefineRows(int colorAmount)
        {
            if (colorAmount <= 16)
            {
                return 1;
            }

            return colorAmount / 16;
        }

        /// <summary>
        /// Defines the amount of columns the Color Table can have.
        /// Since each row can have a maximum of 16 cells, the table will have a maximum of 16 columns.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <returns>The amount of columns used for the amount of color cells received.</returns>
        private static int DefineColumns(int colorAmount)
        {
            if (colorAmount <= 16)
            {
                return colorAmount;
            }

            return 16;
        }

        /// <summary>
        /// Changes the currently selected RectangleCell from the color table.
        /// </summary>
        /// <param name="newCell">The new cell that should be marked as selected.</param>
        public void ChangeCurrentCell(RectangleCell newCell)
        {
            SuspendLayout();

            // Gets the cell that is currently selected to deselect it,
            RectangleCell? oldCell = GetControlFromPosition(currentCellColumn, currentCellRow) as RectangleCell;
            oldCell!.DeselectCell();

            // Then changes the selected values to those of the new cell.
            currentCellRow = GetRow(newCell);
            currentCellColumn = GetColumn(newCell);

            newCell.SelectCell();

            ResumeLayout();
        }

        /// <summary>
        /// Method that returns the color from the currently selected ReactangleCell.
        /// </summary>
        /// <returns>A Color, equivalent to the BackColor of the currently selected RectangleCell.</returns>
        public Color GetCurrentColor()
        {
            return GetControlFromPosition(currentCellColumn, currentCellRow).BackColor;
        }
    }
}
