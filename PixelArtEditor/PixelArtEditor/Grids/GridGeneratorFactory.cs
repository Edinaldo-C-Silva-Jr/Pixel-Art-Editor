using PixelArtEditor.Grids.Implementations;

namespace PixelArtEditor.Grids
{
    internal class GridGeneratorFactory
    {
        /// <summary>
        /// The currently generated tool. It is stored to be reused throughout the application for as long as no new tool is selected.
        /// </summary>
        private IGridGenerator Grid { get; set; }

        public GridGeneratorFactory() 
        {
            Grid = new NoGrid();
        }

        public IGridGenerator GetGrid()
        {
            return Grid;
        }

        public void ChangeCurrentGrid(GridType gridType, int imageWidth, int imageHeight, int cellSize, Color gridColor)
        {
            Grid= gridType switch
            {
                GridType.Line => new LineGrid(),
                _ => new NoGrid()
            };

            Grid.GenerateGrid(imageWidth, imageHeight, cellSize, gridColor);
        }
    }
}
