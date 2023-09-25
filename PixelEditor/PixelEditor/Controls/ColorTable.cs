using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PixelEditor.Controls
{
    public partial class ColorTable : TableLayoutPanel
    {
        private int currentCellRow = 0, currentCellColumn = 0;

        public ColorTable()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        public void GenerateColorGrid(int colorAmount, int size, EventHandler cellClick, Color color, bool visiblySelectable)
        {
            GenerateGrid(colorAmount, size, cellClick, color, visiblySelectable);
        }

        public void GenerateColorGrid(int colorAmount, int size, EventHandler cellClick)
        {
            GenerateGrid(colorAmount, size, cellClick, Color.White, true);
        }

        private void GenerateGrid(int colorAmount, int size, EventHandler cellClick, Color color, bool selectable)
        {
            this.SuspendLayout();

            int rows = DefineRows(colorAmount);
            int columns = DefineColumns(colorAmount);
            int cellSize = size;

            // The size of the table is cell size x amount of cells + 1 pixel per cell (to account for the margin) + 1 pixel for the final margin
            // Thus is becomes (cell size + 1) * amount of cells + 1
            this.Size = new Size((cellSize + 1) * columns + 1, (cellSize + 1) * rows + 1);

            this.Controls.Clear();
            this.RowStyles.Clear();
            this.ColumnStyles.Clear();
            this.RowCount = rows;
            this.ColumnCount = columns;
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    RectangleCell cellTemplate = new RectangleCell(cellSize, color, selectable);
                    cellTemplate.SetCellSize(cellSize);
                    cellTemplate.Click += cellClick;
                    this.Controls.Add(cellTemplate, x, y);
                }
            }

            this.ResumeLayout();
            this.Refresh();
        }

        private int DefineRows(int colorAmount)
        {
            if (colorAmount <= 16)
            {
                return 1;
            }

            return colorAmount / 16;
        }

        private int DefineColumns(int colorAmount)
        {
            if (colorAmount <= 16)
            {
                return colorAmount;
            }

            return 16;
        }

        public void ChangeCurrentCell(RectangleCell newCell)
        {
            this.SuspendLayout();

            RectangleCell oldCell = this.GetControlFromPosition(currentCellColumn, currentCellRow) as RectangleCell;
            oldCell.DeselectCell();

            currentCellRow = this.GetRow(newCell);
            currentCellColumn = this.GetColumn(newCell);

            newCell.SelectCell();

            this.ResumeLayout();
        }

        public Color GetCurrentColor()
        {
            return this.GetControlFromPosition(currentCellColumn, currentCellRow).BackColor;
        }
    }
}
