using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGFramework;

namespace LevelEditor
{
    /// <summary>
    /// Struct for the LevelEditor Model
    /// </summary>
    public struct LevelEditor_Model
    {
        /// <summary>
        /// Ref. to the Level Editor Config
        /// </summary>
        public LevelEditorConfig Config;
        /// <summary>
        /// Ref. to the LevelEditor Tool
        /// </summary>
        public LevelEditorTool Tool;
        /// <summary>
        /// Ref. to the level
        /// </summary>
        public Level CurrentLevel;
    }
}
