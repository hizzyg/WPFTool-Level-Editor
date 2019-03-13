using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGFramework;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;

namespace LevelEditor
{
    public class LevelEditor_Controller
    {
        /// <summary>
        /// Ref. to the LeveEditor Model
        /// </summary>
        public LevelEditor_Model Model;
        /// <summary>
        /// Ref. to the MainWindow
        /// </summary>
        public MainWindow View;
        /// <summary>
        /// Ref. to the Language Manager
        /// </summary>
        public LanguageManager LanguageManager;

        /// <summary>
        /// Ref. to the Tile
        /// </summary>
        public Tile SelectedTile;

        /// <summary>
        /// Function of the LevelEditor Controller
        /// </summary>
        public LevelEditor_Controller()
        {
            // Create the Model and Fill it
            Model = new LevelEditor_Model();
            Model.CurrentLevel = new RPGFramework.Level();
            Model.CurrentLevel.Name = "Untitled";
            Model.CurrentLevel.Grid = new RPGFramework.Grid(new List<RPGFramework.Tile>(), 0, 0);
            Model.CurrentLevel.Palette = new RPGFramework.TexturePalette();
            Model.CurrentLevel.Palette.Entries = new Dictionary<int, TextureEntry>();

            Model.Tool = LevelEditorTool.Selection;

            Model.Config = new LevelEditorConfig(0, 0, 1280.0f, 720.0f, WindowStartupLocation.CenterScreen, WindowState.Normal, LevelEditorLanguage.English);

            // After setting up the Model, make sure that the Language Manager Controller is the LevelEditor_Controller 
            LanguageManager = new LanguageManager(this);
            // Load the Dictionary
            LanguageManager.LoadDictionary();
        }


        /// <summary>
        /// This function will refresh the view
        /// </summary>
        public void RefreshView()
        {
            // Clear the TexturePallet
            View.ListBox_TexturePalette.Items.Clear();

            // Set the TexturePallet Items from the CurrentLevel Pallets
            foreach (KeyValuePair<int, TextureEntry> DictEntry in Model.CurrentLevel.Palette.Entries)
            {
                View.ListBox_TexturePalette.Items.Add(DictEntry.Value.Name);
            }

            // Apply the selected Language
            ApplyLanguage();
        }

        /// <summary>
        /// Applys the selected language
        /// </summary>
        public void ApplyLanguage()
        {
            View.MenuItem_File.Header = LanguageManager.GetDictValue("#MenuItem_File");
            View.MenuItem_Help.Header = LanguageManager.GetDictValue("#MenuItem_Help");
            View.MenuItem_NewLevel.Header = LanguageManager.GetDictValue("#MenuItem_NewLevel");
            View.MenuItem_LoadLevel.Header = LanguageManager.GetDictValue("#MenuItem_LoadLevel");
            View.MenuItem_SaveLevel.Header = LanguageManager.GetDictValue("#MenuItem_SaveLevel");
            View.MenuItem_SaveLevelAs.Header = LanguageManager.GetDictValue("#MenuItem_SaveLevelAs");
            View.MenuItem_RecentLevels.Header = LanguageManager.GetDictValue("#MenuItem_RecentLevels");
            View.MenuItem_Quit.Header = LanguageManager.GetDictValue("#MenuItem_Quit");
            View.MenuItem_Help.Header = LanguageManager.GetDictValue("#MenuItem_Help");
            View.MenuItem_Options.Header = LanguageManager.GetDictValue("#MenuItem_Options");
            View.MenuItem_Help_Lower.Header = LanguageManager.GetDictValue("#MenuItem_Help_Lower");
            View.MenuItem_About.Header = LanguageManager.GetDictValue("#MenuItem_About");
            View.Button_ImportTexture.Content = LanguageManager.GetDictValue("#Grid_Viewport");
            View.Button_ImportTexture.ToolTip = LanguageManager.GetDictValue("#Grid_ViewportToolTip");
            View.ComboBox_LayerSelection.ToolTip = LanguageManager.GetDictValue("#ComboBox_LayerSelection");
            View.Inspector.Header = LanguageManager.GetDictValue("#Inspector");
            View.Toolbar.Header = LanguageManager.GetDictValue("#Toolbar");
            View.Brush_Name.Content = LanguageManager.GetDictValue("#Brush");
            View.Collision_Name.Content = LanguageManager.GetDictValue("#Collision");
            View.Viewport.Header = LanguageManager.GetDictValue("#Viewport");
            View.Layer_Selection.Header = LanguageManager.GetDictValue("#Layer_Selection");
            View.Layer1.Content = LanguageManager.GetDictValue("#Layer1");
            View.Layer2.Content = LanguageManager.GetDictValue("#Layer2");
            View.Layer3.Content = LanguageManager.GetDictValue("#Layer3");
            View.Layer4.Content = LanguageManager.GetDictValue("#Layer4");
            View.Layer5.Content = LanguageManager.GetDictValue("Layer5");
            View.Coordinates.Header = LanguageManager.GetDictValue("#Header");
            View.Coordinates_Type.Text = LanguageManager.GetDictValue("#Coordinates_Type");
            View.Attributes.Header = LanguageManager.GetDictValue("#Attributes");
            View.Trigger_Action.Text = LanguageManager.GetDictValue("#Trigger_Action");
            View.Portal_Destination.Text = LanguageManager.GetDictValue("#Portal_Destination");
            View.NPC_ID.Text = LanguageManager.GetDictValue("#NPC_ID");
            View.Object_ID.Text = LanguageManager.GetDictValue("#Object_ID");
            View.TextBox_Attributes_TriggerAction.ToolTip = LanguageManager.GetDictValue("#TextBox_Attributes_TriggerAction");
            View.TextBox_Attributes_PortalDestination.ToolTip = LanguageManager.GetDictValue("#TextBox_Attributes_PortalDestination");
            View.TextBox_Attributes_NPCID.ToolTip = LanguageManager.GetDictValue("#TextBox_Attributes_NPCID");
            View.TextBox_Attributes_ObjectID.ToolTip = LanguageManager.GetDictValue("#TextBox_Attributes_ObjectID");
            View.TexturePallet.Header = LanguageManager.GetDictValue("#TexturePallet");
            View.MenuItem_Help_Copy.Header = LanguageManager.GetDictValue("#SpriteCutter");
        }
        /// <summary>
        /// A boolean which allows the Import of a Texture if true
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ImportTexture(string path)
        {
            //Import Texture
            FileInfo TextureFileInfo = new FileInfo(path);

            // Create a new Texture Entry and fill it up
            TextureEntry NewEntry = new TextureEntry();
            NewEntry.Name = TextureFileInfo.Name;
            NewEntry.Description = "Texture that was imported";
            NewEntry.Shader = Shader.Default;

            // Create a BitmapImage and fill it up
            BitmapImage Texture = new BitmapImage(new Uri(path));
            NewEntry.Texture = getJPGFromImageControl(Texture);

            Model.CurrentLevel.Palette.Entries.Add(Model.CurrentLevel.Palette.Entries.Count, NewEntry);

            return true;
        }

        /// <summary>
        /// byte array for the Image Control
        /// </summary>
        /// <param name="imageC"></param>
        /// <returns></returns>
        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        /// <summary>
        /// Function to Fill the Viewport-Grid
        /// </summary>
        public void FillViewportGrid()
        {
            // Clear Grid
            View.Grid_Viewport.ColumnDefinitions.Clear();
            View.Grid_Viewport.RowDefinitions.Clear();
            View.Grid_Viewport.Children.Clear();

            // Create Row Definitions
            for (int y = 0; y < Model.CurrentLevel.Grid.DimensionsY; y++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new System.Windows.GridLength(64);
                View.Grid_Viewport.RowDefinitions.Add(rowDefinition);
            }

            // Create Column Definitions
            for (int x = 0; x < Model.CurrentLevel.Grid.DimensionsX; x++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new System.Windows.GridLength(64);
                View.Grid_Viewport.ColumnDefinitions.Add(columnDefinition);
            }

            // Fill Buttons to Grid
            foreach(Tile tile in Model.CurrentLevel.Grid.Tiles)
            {
                Button Button_Viewport_Tile = new Button();
                Button_Viewport_Tile.Name = "Button_ViewportTile_" + tile.CoordinateX.ToString() + "_" + tile.CoordinateY.ToString();
                System.Windows.Controls.Grid.SetColumn(Button_Viewport_Tile, tile.CoordinateX);
                System.Windows.Controls.Grid.SetRow(Button_Viewport_Tile, tile.CoordinateY);
                View.Grid_Viewport.Children.Add(Button_Viewport_Tile);
                Button_Viewport_Tile.Click += ViewportGridTileButton_Click;

                Image Image_Viewport_Tile = new Image();
                Button_Viewport_Tile.Content = Image_Viewport_Tile;
            }

            RefreshViewport();
        }

        /// <summary>
        /// Function which will refresh the viewport
        /// </summary>
        public void RefreshViewport()
        {
            foreach(Control c in View.Grid_Viewport.Children)
            {
                Button TileButton = (Button)c;
                Tile Tile = GetTileFromButton(TileButton);
                Image ImageTile = (Image)TileButton.Content;

                if(Tile.CoordinateX == SelectedTile.CoordinateX && Tile.CoordinateY == SelectedTile.CoordinateY)
                {
                    TileButton.BorderThickness = new Thickness(4.0f);
                    TileButton.BorderBrush = Brushes.Gold;
                }
                else
                {
                    TileButton.BorderThickness = new Thickness(1.0f);
                    TileButton.BorderBrush = Brushes.Gray;
                }

                // Check which Type is selected and add this to the Viewport
                switch(Tile.Type)
                {
                    default:
                    case TileType.Default:
                        break;
                    case TileType.Collision:
                        Image ButtonImage = (Image)TileButton.Content;
                        ButtonImage.Opacity = 0.25f;
                        break;
                }

                try
                {       // Making sure, that the pallet is not empty, if its not empty fill the ImageTile Source
                    if (Model.CurrentLevel.Palette.Entries.Count > 0)
                    {
                        ImageTile.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(Model.CurrentLevel.Palette.Entries[Tile.TextureEntryID].Texture);
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Function for getting the tiles from buttons
        /// </summary>
        /// <param name="_tileButton"></param>
        /// <returns></returns>
        public Tile GetTileFromButton(Button _tileButton)
        {
            Tile ReturnTile = new Tile();
            string ButtonName = _tileButton.Name;
            int UnderscoreCounter = 0;

            // Get first Underscore from last Char
            for(int i = ButtonName.Length - 1; i > 0; i--)
            {
                if(ButtonName[i] == '_')
                {
                    break;
                }

                UnderscoreCounter++;
            }

            int YCoordinate = 0;
            YCoordinate = Convert.ToInt32(ButtonName.Substring(ButtonName.Length - UnderscoreCounter));

            ButtonName = ButtonName.Substring(0, ButtonName.Length - UnderscoreCounter - 1);

            UnderscoreCounter = 0;

            // Get second Underscore from last Char
            for (int i = ButtonName.Length - 1; i > 0; i--)
            {
                if (ButtonName[i] == '_')
                {
                    break;
                }

                UnderscoreCounter++;
            }

            int XCoordinate = 0;
            XCoordinate = Convert.ToInt32(ButtonName.Substring(ButtonName.Length - UnderscoreCounter));

            foreach(Tile _tile in Model.CurrentLevel.Grid.Tiles)
            {
                if(_tile.CoordinateX == XCoordinate && _tile.CoordinateY == YCoordinate)
                {
                    ReturnTile = _tile;
                    break;
                }
            }

            return ReturnTile;
        }


        /// <summary>
        /// The Button function for the Viewport
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewportGridTileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a Button
            Button InputButton = (Button)sender;
            SelectedTile = GetTileFromButton(InputButton);

            // Check which Tool has been selected and call the specific function
            switch(Model.Tool)
            {
                default:
                case LevelEditorTool.Selection:
                    RefreshInspector();
                    RefreshViewport();
                    break;
                case LevelEditorTool.Brush:
                    Tile NewTile = new Tile(SelectedTile.CoordinateX,
                        SelectedTile.CoordinateY,
                        SelectedTile.Layer,
                        SelectedTile.Type,
                        SelectedTile.Attribute_TriggerAction,
                        SelectedTile.Attribute_PortalDestination,
                        SelectedTile.Attribute_NPCID,
                        SelectedTile.Attribute_ObjectID,
                        View.ListBox_TexturePalette.SelectedIndex);

                    ReplaceTileInGrid(NewTile);

                    RefreshViewport();
                    break;
                case LevelEditorTool.Collision:
                    Tile CollisionTile;
                    if (SelectedTile.Type == TileType.Collision)
                    {
                        CollisionTile = new Tile(SelectedTile.CoordinateX,
                        SelectedTile.CoordinateY,
                        SelectedTile.Layer,
                        TileType.Default,
                        SelectedTile.Attribute_TriggerAction,
                        SelectedTile.Attribute_PortalDestination,
                        SelectedTile.Attribute_NPCID,
                        SelectedTile.Attribute_ObjectID,
                        SelectedTile.TextureEntryID);
                    }
                    else
                    {
                        CollisionTile = new Tile(SelectedTile.CoordinateX,
                        SelectedTile.CoordinateY,
                        SelectedTile.Layer,
                        TileType.Collision,
                        SelectedTile.Attribute_TriggerAction,
                        SelectedTile.Attribute_PortalDestination,
                        SelectedTile.Attribute_NPCID,
                        SelectedTile.Attribute_ObjectID,
                        SelectedTile.TextureEntryID);
                    }

                    ReplaceTileInGrid(CollisionTile);

                    RefreshViewport();
                    break;
            }
        }

        /// <summary>
        /// Refreshing the Inspector
        /// </summary>
        private void RefreshInspector()
        {
            View.TextBox_CoordX.Text = SelectedTile.CoordinateX.ToString();
            View.TextBox_CoordY.Text = SelectedTile.CoordinateY.ToString();
            // View.ComboBox_TileTypeSelection.SelectedIndex = Convert.ToInt32(SelectedTile.Type);
        }

        /// <summary>
        /// Select the TileType
        /// </summary>
        /// <param name="_type"></param>
        public void SelectedTileTypeChanged(TileType _type)
        {
            SelectedTile.Type = _type;
            ReplaceTileInGrid(SelectedTile);
        }

        /// <summary>
        /// Replace the Tile
        /// </summary>
        /// <param name="NewTile"></param>
        public void ReplaceTileInGrid(Tile NewTile)
        {
            int IndexOfOriginal = 0;

            // Fill the tile with tiles of the Current Level
            foreach(Tile _tile in Model.CurrentLevel.Grid.Tiles)
            {
                if(_tile.CoordinateX == NewTile.CoordinateX && _tile.CoordinateY == NewTile.CoordinateY)
                {
                    break;
                }

                IndexOfOriginal++;
            }

            try
            {
                Model.CurrentLevel.Grid.Tiles[IndexOfOriginal] = NewTile;
            }
            catch
            {

            }

            RefreshInspector();
        }

        /// <summary>
        /// Save Level as..
        /// </summary>
        /// <param name="_name"></param>
        public void SaveLevelAs(string _name)
        {
            Model.CurrentLevel.Name = _name;
            SaveLevel();
        }

        /// <summary>
        /// Save Level
        /// </summary>
        public void SaveLevel()
        {
            // Create a Directory if it's does'nt exists
            if(!Directory.Exists(Environment.CurrentDirectory + @"\Levels\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Levels");
            }

            //writing into the file
            using (var file = File.OpenWrite(Environment.CurrentDirectory + @"\Levels\" + Model.CurrentLevel.Name + ".lvl"))
            {
                var writer = new BinaryFormatter();
                writer.Serialize(file, Model.CurrentLevel); // Writes the entire list.
            }

            if(!Model.Config.RecentLevels.Contains(Model.CurrentLevel.Name + ".lvl"))
            {
                Model.Config.RecentLevels.Add(Model.CurrentLevel.Name + ".lvl");
            }

            SaveConfig();
            ApplyConfig();

            LogMessage("Successfully saved Level!");
        }

        /// <summary>
        /// Loading Level
        /// </summary>
        /// <param name="path"></param>
        public void LoadLevel(string path)
        {
            if(!File.Exists(path))
            {
                LogMessage("Error while Loading Level!");
                return;
            }

            //reading from the file
            using (var file = File.OpenRead(path))
            {
                var reader = new BinaryFormatter();
                Model.CurrentLevel = (Level)reader.Deserialize(file);
            }

            RefreshView();
            FillViewportGrid();
        }

        /// <summary>
        /// Save the Config
        /// </summary>
        public void SaveConfig()
        {
            if(!Directory.Exists(Environment.CurrentDirectory + @"\Config"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Config");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(LevelEditorConfig));
            using (TextWriter writer = new StreamWriter(Environment.CurrentDirectory + @"\Config\Config.xml"))
            {
                serializer.Serialize(writer, Model.Config);
            }
        }

        /// <summary>
        /// Load the Config
        /// </summary>
        public void LoadConfig()
        {
            if(!File.Exists(Environment.CurrentDirectory + @"\Config\Config.xml"))
            {
                SaveConfig();
                return;
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(LevelEditorConfig));
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\Config\Config.xml");
            Model.Config = (LevelEditorConfig)deserializer.Deserialize(reader);
            reader.Close();

            ApplyConfig();
        }

        /// <summary>
        /// Apply the Config
        /// </summary>
        public void ApplyConfig()
        {
            View.Width = Model.Config.WindowSizeWidth;
            View.Height = Model.Config.WindowSizeHeight;
            View.Top = Model.Config.WindowPositionY;
            View.Left = Model.Config.WindowPositionX;
            View.WindowStartupLocation = Model.Config.StartupLocation;
            View.WindowState = Model.Config.WindowState;

            View.MenuItem_RecentLevels.Items.Clear();

            foreach(string recentLevel in Model.Config.RecentLevels)
            {
                MenuItem RecentLevelMenuItem = new MenuItem();
                RecentLevelMenuItem.Header = recentLevel;
                RecentLevelMenuItem.Click += MenuItem_RecentLevel_Click;
                View.MenuItem_RecentLevels.Items.Add(RecentLevelMenuItem);
            }
        }

        /// <summary>
        /// Log Messages
        /// </summary>
        /// <param name="_Message"></param>
        public void LogMessage(string _Message)
        {
            View.TextBlock_StatusBar.Text = _Message;
        }

        /// <summary>
        /// New Level
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_xDimension"></param>
        /// <param name="_yDimension"></param>
        public void NewLevel(string _name, int _xDimension, int _yDimension)
        {
            // Fill the Model
            Model.CurrentLevel.Name = _name;
            Model.CurrentLevel.Grid = new RPGFramework.Grid(new List<RPGFramework.Tile>(), _xDimension, _yDimension);
            Model.CurrentLevel.Palette = new RPGFramework.TexturePalette(new Dictionary<int, RPGFramework.TextureEntry>());

            // Fill the Texture Entry
            TextureEntry FirstEntry = new TextureEntry();
            FirstEntry.Name = "Unassigned";
            FirstEntry.Description = "Unassigned Tile";
            FirstEntry.Shader = Shader.Default;

            // Create a Bitmap
            BitmapImage FirstEntryTexture = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Resources\Unassigned.png"));
            FirstEntry.Texture = getJPGFromImageControl(FirstEntryTexture);

            Model.CurrentLevel.Palette.Entries.Add(0, FirstEntry);

            FillViewportGrid();
        }

        /// <summary>
        /// Buttonfunction for the selected level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_RecentLevel_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ClickedMenuItem = (MenuItem)sender;

            if (File.Exists(Environment.CurrentDirectory + @"\Levels\" + ClickedMenuItem.Header))
            {
                LoadLevel(Environment.CurrentDirectory + @"\Levels\" + ClickedMenuItem.Header);
            }
        }
    }
}
