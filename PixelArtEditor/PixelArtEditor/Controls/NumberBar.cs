namespace PixelArtEditor.Controls
{
    public partial class NumberBar : Control
    {
        private int _value = 1;
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
        public int MinimumValue
        {
            get { return _minimumValue; }
            set
            {
                _minimumValue = value;
                ValidateSize();
            }
        }

        private int _valueChangeAmount = 1;
        public int ValueChangeAmount
        {
            get { return _valueChangeAmount; }
            set
            {
                _valueChangeAmount = value > 0 ? value : 1;
                ValidateSize();
            }
        }

        private int AmountOfValues
        {
            get
            {
                return 1 + ((MaximumValue - MinimumValue) / (ValueChangeAmount));
            }
        }

        private int ValueLocationInterval
        {
            get
            {
                return Width / AmountOfValues;
            }
        }

        private int MouseValuePosition { get; set; }

        public event EventHandler? ValueChanged;
        private void OnValueChanged()
        {
            MouseValuePosition = (Value - MinimumValue) / ValueChangeAmount;
            Invalidate();
            ValueChanged?.Invoke(this, new EventArgs());
        }

        public NumberBar()
        {
            InitializeComponent();
            Size = new(100, 30);
        }

        private void ValidateSize()
        {
            if (ValueLocationInterval != 0 && Width % ValueLocationInterval != 0)
            {
                Width = ValueLocationInterval * AmountOfValues;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X > 0 && e.X < Width)
                {
                    MouseValuePosition = e.X / ValueLocationInterval;
                    Invalidate();
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X > 0 && e.X < Width)
                {
                    MouseValuePosition = e.X / ValueLocationInterval;
                    Invalidate();
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X > 0 && e.X < Width)
                {
                    MouseValuePosition = e.X / ValueLocationInterval;
                }
                ChangeValueOnClick();
            }
            base.OnMouseUp(e);
        }

        private void ChangeValueOnClick()
        {
            Value = MinimumValue + MouseValuePosition * ValueChangeAmount;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            e.Graphics.FillRectangle(Brushes.Black, 3, 18, Width - 6, 6);
            e.Graphics.FillRectangle(Brushes.White, 4, 19, Width - 8, 4);

            e.Graphics.FillRectangle(Brushes.Black, 3 + MouseValuePosition * (ValueLocationInterval - 1), 16, 10, 10);
            e.Graphics.FillRectangle(Brushes.White, 4 + MouseValuePosition * (ValueLocationInterval - 1), 17, 8, 8);

            int newDrawnValue = MinimumValue + MouseValuePosition * ValueChangeAmount;
            string textValue = newDrawnValue > 10 ? $"{newDrawnValue}" : $"0{newDrawnValue}";
            e.Graphics.DrawString(textValue, new("Segoe UI", 8), Brushes.Black, MouseValuePosition * (ValueLocationInterval - 1), 0);
        }
    }
}
