using System;
using System.Drawing;
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

        public void GenerateColorGrid(int colorRows, int colorColumns, EventHandler cellClick)
        {
            // The size of the table is 30 (cell size) x amount of cells + 1 pixel per cell (to account for the margin) + 1 pixel for the final margin
            // Thus is becomes 31 * amount of cells + 1
            this.Size = new Size(31 * colorColumns + 1, 31 * colorRows + 1);

            this.RowStyles.Clear();
            this.ColumnStyles.Clear();
            this.RowCount = colorRows;
            this.ColumnCount = colorColumns;
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int y = 0; y < colorRows; y++)
            {
                for (int x = 0; x < colorColumns; x++)
                {
                    RectangleCell cellTemplate = new RectangleCell();
                    cellTemplate.Click += cellClick;
                    this.Controls.Add(cellTemplate, x, y);
                }
            }

            this.Refresh();
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
    }
}
