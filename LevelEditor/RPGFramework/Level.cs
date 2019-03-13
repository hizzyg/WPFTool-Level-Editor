using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGFramework
{
    [Serializable]
    public struct Level
    {
        /// <summary>
        /// String for the Name
        /// </summary>
        public string Name;
        /// <summary>
        /// Ref. to the Texture Pallet
        /// </summary>
        public TexturePalette Palette;
        /// <summary>
        /// Ref. to the Grid
        /// </summary>
        public Grid Grid;

        /// <summary>
        /// Function for the Level which includes the name, a texture and a grid
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_palette"></param>
        /// <param name="_grid"></param>
        public Level(string _name, TexturePalette _palette, Grid _grid)
        {
            Name = _name;
            Palette = _palette;
            Grid = _grid;
        }
    }
}
