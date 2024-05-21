namespace PixelArtEditor.Controls
{
    public partial class NumberBar : Control
    {
        public int Value { get; set; } = 1;

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

        private int _valueChangeAmount = 2;
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
                    ChangeValueOnClick(e.X);
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
                    ChangeValueOnClick(e.X);
                }
            }
            base.OnMouseMove(e);
        }

        private void ChangeValueOnClick(int xLocation)
        {
            Value = MinimumValue + xLocation / ValueLocationInterval * ValueChangeAmount;
        }

        /*protected override void OnValueChanged(EventArgs e)
        {
            Invalidate();
            base.OnValueChanged(e);
        }*/

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            e.Graphics.FillRectangle(Brushes.Black, 3, 3, Width - 6, 6);
            e.Graphics.FillRectangle(Brushes.White, 4, 4, Width - 8, 4);

            int position = (Value - MinimumValue) / ValueChangeAmount;
            e.Graphics.FillRectangle(Brushes.Black, 3 + position * (ValueLocationInterval - 1), 1, 10, 10);
            e.Graphics.FillRectangle(Brushes.White, 4 + position * (ValueLocationInterval - 1), 2, 8, 8);

            string textValue = Value > 10 ? $"{Value}" : $"0{Value}";
            e.Graphics.DrawString(textValue, new("Segoe UI", 9), Brushes.Black, position * (ValueLocationInterval - 1), 12);
        }
    }
}
