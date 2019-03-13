using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGFramework
{
    [Serializable]
    public struct TexturePalette
    {
        public Dictionary<int, TextureEntry> Entries;

        public TexturePalette(Dictionary<int, TextureEntry> _entries)
        {
            Entries = _entries;
        }
    }
}
