namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A control used for picking from a range of numbers via clicking on a scrolling bar. Similar to a TrackBar.
    /// The value picked depends on the location clicked on the control.
    /// </summary>
    public partial class NumberBar : Control
    {
        #region Properties
        private int _value = 1;
        /// <summary>
        /// The currently selected numeric value of the NumberBar control.
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnValueChanged();
            }
        }

        private int _maximumValue = 10;
        /// <summary>
        /// The maximum numeric value allowed to be selected on this control.
        /// </summary>
        public int MaximumValue
        {
            get { return _maximumValue; }
            set
            {
                _maximumValue = value;
                ValidateSize();
            }
        }

        private int _minimumValue = 1;
        /// <summary>
        /// The minimum numeric value allowed to be selected on this control.
        /// </summary>
        public int MinimumValue
        {
            get { return _minimumValue; }
            set
            {
                _minimumValue = value;
                ValidateSize();
            }
        }

        private int _incrementAmount = 1;
        /// <summary>
        /// The amount to change the Value property on each increment of the number bar.
        /// </summary>
        public int IncrementAmount
        {
            get { return _incrementAmount; }
            set
            {
                _incrementAmount = value > 0 ? value : 1;
                ValidateSize();
            }
        }

        /// <summary>
        /// The amount of different increments that can be selected in the NumberBar, where each increment represents a value.
        /// </summary>
        private int AmountOfIncrements
        {
            get
            {
                return 1 + ((MaximumValue - MinimumValue) / (IncrementAmount));
            }
        }

        /// <summary>
        /// The amount of pixels equivalent to the area of each value increment on the control.
        /// </summary>
        private int IncrementSize
        {
            get
            {
                if (AmountOfIncrements > Width)
                {
                    return 1;
                }
                else
                {
                    return Width / AmountOfIncrements;
                }
            }
        }

        /// <summary>
        /// The increment currently selected by either a mouse click, or a value change.
        /// </summary>
        private int CurrentIncrement { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// An event handler for the ValueChanged event.
        /// </summary>
        public event EventHandler? ValueChanged;

        /// <summary>
        /// The method to execute when the Value property is changed.
        /// Changes the current increment based on the new value and calls for a redraw of the controller.
        /// </summary>
        private void OnValueChanged()
        {
            CurrentIncrement = (Value - MinimumValue) / IncrementAmount;
            Invalidate();
            ValueChanged?.Invoke(this, new EventArgs());
        }
        #endregion

        /// <summary>
        /// Default constructor. Sets a default size.
        /// </summary>
        public NumberBar()
        {
            InitializeComponent();
            Size = new(100, 30);
        }

        /// <summary>
        /// Validates the Width of the control to ensure all increments have the same size.
        /// </summary>
        private void ValidateSize()
        {
            if (IncrementSize != 0 && Width > IncrementSize * AmountOfIncrements)
            {
                Width = IncrementSize * AmountOfIncrements;
            }
        }

        /// <summary>
        /// Changes the current increment based on the mouse position and calls a redraw of the controller.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > 0 && e.X < Width)
            {
                if (e.X / IncrementSize != CurrentIncrement) // Checks if the clicked increment is different than the current one.
                {
                    CurrentIncrement = e.X / IncrementSize;
                    Invalidate();
                }
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Changes the current increment based on the mouse position and calls a redraw of the controller.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > 0 && e.X < Width)
            {
                if (e.X / IncrementSize != CurrentIncrement)
                {
                    CurrentIncrement = e.X / IncrementSize;
                    Invalidate();
                }
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Changes the current increment and changes the Value property.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ChangeValueOnClick();
            }
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Changes the Value property to a new value based on the current increment.
        /// </summary>
        private void ChangeValueOnClick()
        {
            Value = MinimumValue + CurrentIncrement * IncrementAmount;
        }

        /// <summary>
        /// Draws the control's appearance.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Outline.
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            
            // Number selection bar.
            e.Graphics.FillRectangle(Brushes.Black, 2, 18, Width - 4, 6);
            e.Graphics.FillRectangle(Brushes.White, 3, 19, Width - 6, 4);

            // Number selection cursor. The cursor width adapts to the increment size.
            // The cursor has a minimum size of 4 pixels so it can still be drawn correctly.
            if (IncrementSize < 4)
            {
                // Gets an offset to prevent the cursor from going off the right side of the control.
                int cursorOffset = CurrentIncrement / (MaximumValue / (4 - IncrementSize));
                e.Graphics.FillRectangle(Brushes.Black, CurrentIncrement * IncrementSize - cursorOffset, 16, 3, 10);
                e.Graphics.FillRectangle(Brushes.White, 1 + CurrentIncrement * IncrementSize - cursorOffset, 17, 1, 8);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Black, CurrentIncrement * IncrementSize, 16, IncrementSize - 1, 10);
                e.Graphics.FillRectangle(Brushes.White, 1 + CurrentIncrement * IncrementSize, 17, IncrementSize - 3, 8);
            }

            // Gets the value and its string representation.
            int newDrawnValue = MinimumValue + CurrentIncrement * IncrementAmount;
            string textValue = newDrawnValue.ToString();

            // Displays the value as a string.
            int stringWidth = (int)e.Graphics.MeasureString(textValue, Font).Width;
            if (IncrementSize < stringWidth)
            {
                // Gets an offset to prevent the string from going off the right side of the control.
                int stringOffset = newDrawnValue / (MaximumValue / (stringWidth - IncrementSize));
                e.Graphics.DrawString(textValue, new("Segoe UI", 8), Brushes.Black, CurrentIncrement * IncrementSize - stringOffset, 0);
            }
            else
            {
                e.Graphics.DrawString(textValue, new("Segoe UI", 8), Brushes.Black, CurrentIncrement * IncrementSize, 0);
            }
        }
    }
}
