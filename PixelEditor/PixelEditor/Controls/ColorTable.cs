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

        public void GenerateColorGrid(int colorRows, int colorColumns, EventHandler cellClick, Color color, bool visiblySelectable)
        {
            int cellSize = DefineCellSize(colorColumns);
            PrepareTableForGrid(colorRows, colorColumns, cellSize);

            for (int y = 0; y < colorRows; y++)
            {
                for (int x = 0; x < colorColumns; x++)
                {
                    RectangleCell cellTemplate = new RectangleCell(color, visiblySelectable);
                    cellTemplate.SetCellSize(cellSize);
                    cellTemplate.Click += cellClick;
                    this.Controls.Add(cellTemplate, x, y);
                }
            }

            this.Refresh();
        }

        public void GenerateColorGrid(int colorRows, int colorColumns, EventHandler cellClick)
        {
            int cellSize = DefineCellSize(colorColumns);
            PrepareTableForGrid(colorRows, colorColumns, cellSize);

            for (int y = 0; y < colorRows; y++)
            {
                for (int x = 0; x < colorColumns; x++)
                {
                    RectangleCell cellTemplate = new RectangleCell();
                    cellTemplate.SetCellSize(cellSize);
                    cellTemplate.Click += cellClick;
                    this.Controls.Add(cellTemplate, x, y);
                }
            }

            this.Refresh();
        }

        private int PrepareTableForGrid(int colorRows, int colorColumns, int cellSize)
        {
            // The size of the table is cell size x amount of cells + 1 pixel per cell (to account for the margin) + 1 pixel for the final margin
            // Thus is becomes (cell size + 1) * amount of cells + 1
            this.Size = new Size((cellSize + 1) * colorColumns + 1, (cellSize + 1) * colorRows + 1);

            this.RowStyles.Clear();
            this.ColumnStyles.Clear();
            this.RowCount = colorRows;
            this.ColumnCount = colorColumns;
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            return cellSize;
        }

        private int DefineCellSize(int columns)
        {
            if (columns < 9)
            {
                return 32;
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
