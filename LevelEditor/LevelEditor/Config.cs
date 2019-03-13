using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LevelEditor
{
    public struct LevelEditorConfig
    {
        /// <summary>
        /// Window Position X
        /// </summary>
        public float WindowPositionX;
        /// <summary>
        /// Window Position Y
        /// </summary>
        public float WindowPositionY;
        /// <summary>
        /// Window Size Width
        /// </summary>
        public float WindowSizeWidth;
        /// <summary>
        /// Window Size Height
        /// </summary>
        public float WindowSizeHeight;
        /// <summary>
        /// Ref. to Window Startup Location
        /// </summary>
        public WindowStartupLocation StartupLocation;
        /// <summary>
        /// Ref. to Window State
        /// </summary>
        public WindowState WindowState;
        /// <summary>
        /// Ref. to Level Editor Language
        /// </summary>
        public LevelEditorLanguage Language;

        /// <summary>
        /// List of the levels
        /// </summary>
        public List<string> RecentLevels;

        public LevelEditorConfig(float _PosX, float _PosY, float _Width, float _Height, WindowStartupLocation _StartupLocation, WindowState _WindowState, LevelEditorLanguage _Language)
        {
            WindowPositionX = _PosX;
            WindowPositionY = _PosY;
            WindowSizeWidth = _Width;
            WindowSizeHeight = _Height;
            StartupLocation = _StartupLocation;
            WindowState = _WindowState;
            Language = _Language;
            RecentLevels = new List<string>();
        }
    }
}
