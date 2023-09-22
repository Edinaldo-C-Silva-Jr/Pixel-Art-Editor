﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelEditor.Controls
{
    public partial class RectangleCell : UserControl
    {
        private int cellSize;
        private bool visibleSelection = true;

        public RectangleCell()
        {
            cellSize = 32;
            this.Dock = DockStyle.Fill;
            this.Size = new Size(cellSize, cellSize);
            this.Margin = new Padding(0, 0, 0, 0);
            this.BackColor = Color.White;

            InitializeComponent();
        }

        public RectangleCell(Color color, bool selectable)
        {
            cellSize = 32;
            this.Dock = DockStyle.Fill;
            this.Size = new Size(cellSize, cellSize);
            this.Margin = new Padding(0, 0, 0, 0);
            this.BackColor = color;
            this.visibleSelection = selectable;

            InitializeComponent();
        }

        public void SetCellSize(int size)
        {
            this.cellSize = size;
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
    }
}
