namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A control that implements a Table Layout Panel to allow picking the colors used in the Pixel Art Editor.
    /// This table uses RectangleCell controls as individual cells.
    /// </summary>
    public partial class ColorTable : TableLayoutPanel
    {
        /// <summary>
        /// The row number of the currently selected cell.
        /// </summary>
        private int CurrentCellRow { get; set; }

        /// <summary>
        /// The column number of the currently selected cell.
        /// </summary>
        private int CurrentCellColumn { get; set; }

        /// <summary>
        /// The cell size to be used for all cells in this Color Table.
        /// </summary>
        public int CellSize { get; set; }

        /// <summary>
        /// Defines if the cells in this Color Table will change appearance when selected.
        /// </summary>
        public bool CellVisibleSelection { get; set; }

        /// <summary>
        /// Defines the maximum amount of cells this Color Table can have.
        /// </summary>
        public int MaximumCellAmount { get; set; }

        /// <summary>
        /// The list of cells used in the Color Table.
        /// </summary>
        private List<RectangleCell> CellList { get; set; }

        /// <summary>
        /// Constructor method of the Color Table, which enables the DoubleBuffered property.
        /// </summary>
        public ColorTable()
        {
            DoubleBuffered = true;
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            InitializeComponent();

            CurrentCellColumn = CurrentCellRow = 0;
            CellList = new();
        }

        /// <summary>
        /// Generates a grid of RectangleCells into the Color Table, which allows selecting colors.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <param name="cellClick">The Click event handler to use on each cell's click event.</param>
        /// <param name="defaultCellColor">The color set as the default color on all cells.</param>
        public void GenerateColorGrid(int colorAmount, EventHandler cellClick, Color defaultCellColor)
        {
            GenerateCellGrid(colorAmount, cellClick, defaultCellColor);
        }

        /// <summary>
        /// Generates a grid of RectangleCells into the Color Table, which allows selecting colors.
        /// This method defines the color of all cells as white.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <param name="cellClick">The Click event handler to use on each cell's click event.</param>
        public void GenerateColorGrid(int colorAmount, EventHandler cellClick)
        {
            GenerateCellGrid(colorAmount, cellClick, Color.White);
        }

        /// <summary>
        /// Generates the grid, selecting the cells from the Cell List and adding them onto the Color Table's Control list.
        /// </summary>
        /// <param name="colorAmount">The amount of color cells that will be in the Color Table.</param>
        /// <param name="cellClick">The Click event handler to use on each cell's click event.</param>
        /// <param name="defaultCellColor">The color set as the default color on all cells.</param>
        private void GenerateCellGrid(int colorAmount, EventHandler cellClick, Color defaultCellColor)
        {
            SuspendLayout();

            // If the Cells List hasn't yet been created, then create it.
            if (CellList.Count == 0)
            {
                CreateCellList(cellClick, defaultCellColor);
            }

            int rows = DefineRows(colorAmount);
            int columns = DefineColumns(colorAmount);

            // Checkes if the currently selected cell will be outside the bounds of the new color table.
            if (colorAmount < CurrentCellRow * 16 + CurrentCellColumn)
            {
                RectangleCell? oldCell = GetControlFromPosition(CurrentCellColumn, CurrentCellRow) as RectangleCell;
                oldCell!.DeselectCell();

                // Changes the currently selected cell to the last cell of the newly generated color table.
                CurrentCellRow = rows - 1;
                CurrentCellColumn = columns - 1;

                RectangleCell? newCell = GetControlFromPosition(CurrentCellColumn, CurrentCellRow) as RectangleCell;
                newCell!.SelectCell();
            }

            // The size of the table is equal to:
            // The size of all cells combined + 1 pixel for each cell, to account for the margin + 1 pixel for the final margin.
            // Thus it becomes: (cell size + 1) * amount of cells + 1
            Size = new Size((CellSize + 1) * columns + 1, (CellSize + 1) * rows + 1);

            // Clears the previous table and defines the new amount of rows and columns.
            Controls.Clear();
            RowStyles.Clear();
            ColumnStyles.Clear();
            RowCount = rows;
            ColumnCount = columns;

            // Rebuilds the table, selecting the specific amount of cells from the Cells List.
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Controls.Add(CellList[y * columns + x], x, y);
                }
            }

            ResumeLayout();
            Refresh();
        }

        /// <summary>
        /// Builds the Cells List with the amount of cells equal to the table's maximum cell amount.
        /// Every cell is built equally with its properties based on the parameters passed to the method and the table's properties.
        /// </summary>
        /// <param name="cellClick">The Click event handler to use on each cell's click event.</param>
        /// <param name="defaultCellColor">The color set as the default color on all cells.</param>
        private void CreateCellList(EventHandler cellClick, Color defaultCellColor)
        {
            // Creates the cells, defining their default properties and the Click Event Handler, then adds them to the Cell List.
            for (int i = 0; i < MaximumCellAmount; i++)
            {
                RectangleCell cellTemplate = new(CellSize, defaultCellColor, CellVisibleSelection);
                cellTemplate.Click += cellClick;
                CellList.Add(cellTemplate);
            }
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
            RectangleCell? oldCell = GetControlFromPosition(CurrentCellColumn, CurrentCellRow) as RectangleCell;
            oldCell!.DeselectCell();

            // Then changes the selected values to those of the new cell.
            CurrentCellRow = GetRow(newCell);
            CurrentCellColumn = GetColumn(newCell);

            newCell.SelectCell();

            ResumeLayout();
        }

        /// <summary>
        /// Method that returns the color from the currently selected ReactangleCell.
        /// </summary>
        /// <returns>A Color, equivalent to the BackColor of the currently selected RectangleCell.</returns>
        public Color GetCurrentColor()
        {
            return GetControlFromPosition(CurrentCellColumn, CurrentCellRow).BackColor;
        }

        public string GetAllColorValues()
        {
            string paletteColors = String.Empty;

            for (int i = 0; i < MaximumCellAmount; i++)
            {
                paletteColors += $"{CellList[i].BackColor.ToArgb()}|";
            }

            return paletteColors;
        }

        public void SetAllColorValues(string paletteColors)
        {
            string[] colorValues = paletteColors.Split('|');

            for (int i = 0; i < MaximumCellAmount; i++)
            {
                if (int.TryParse(colorValues[i], out int colorARGB))
                {
                    CellList[i].BackColor = Color.FromArgb(colorARGB);
                }
            }
        }
    }
}
