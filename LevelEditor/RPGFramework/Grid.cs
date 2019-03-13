using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGFramework
{
    [Serializable]
    public struct Grid
    {
        /// <summary>
        /// Integer for the X Dimension
        /// </summary>
        public int DimensionsX;
        /// <summary>
        /// Integer for the Y Dimension
        /// </summary>
        public int DimensionsY;
        /// <summary>
        /// A public List of the Tiles
        /// </summary>
        public List<Tile> Tiles;

        /// <summary>
        /// Function for the Grid which includes the Tiles,X and Y Dimensions
        /// </summary>
        /// <param name="_tiles"></param>
        /// <param name="_dimensionsX"></param>
        /// <param name="_dimensionsY"></param>
        public Grid(List<Tile> _tiles, int _dimensionsX, int _dimensionsY)
        {
            Tiles = _tiles;
            DimensionsX = _dimensionsX;
            DimensionsY = _dimensionsY;

            for (int y = 0; y < DimensionsY; y++)
            {
                for (int x = 0; x < DimensionsX; x++)
                {
                    Tiles.Add(new Tile(x, y, 0, TileType.Default));
                }
            }
        }
    }
}
