using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using LevelEditor.Dialogs;
using RPGFramework;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LevelEditor_Controller Controller;

        public MainWindow()
        {
            InitializeComponent();

            Controller = new LevelEditor_Controller();
            Controller.View = this;

            Controller.LoadConfig();

            string[] CMDArguments = Environment.GetCommandLineArgs();

            foreach (string s in CMDArguments)
            {
                if (s.Contains("fullscreen"))
                {
                    Controller.Model.Config.WindowState = WindowState.Maximized;
                }
            }

            Controller.ApplyConfig();

            Controller.RefreshView();

            Controller.LogMessage("Level Editor initialized...");
        }

        private void MenuItem_Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_NewLevel_Click(object sender, RoutedEventArgs e)
        {
            Dialog_NewLevel NewLevelDialog = new Dialog_NewLevel(Controller);

            NewLevelDialog.ShowDialog();
        }

        private void MenuItem_LoadLevel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory + @"\Levels\";

            if (openFileDialog.ShowDialog() == true)
            {
                Controller.LoadLevel(openFileDialog.FileName);
            }
        }

        private void MenuItem_SaveLevel_Click(object sender, RoutedEventArgs e)
        {
            Controller.SaveLevel();
        }

        private void MenuItem_SaveLevelAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                Controller.SaveLevelAs(saveFileDialog.SafeFileName);
            }
        }

        private void MenuItem_Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://google.de");
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://steamusercontent-a.akamaihd.net/ugc/955216255488299586/3B30A3FEE287F5E602C116C3571430B1E4593473/");
        }

        private void Button_ImportTexture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Dialog_ImportTexture = new OpenFileDialog();
            Dialog_ImportTexture.Multiselect = true;
            if (Dialog_ImportTexture.ShowDialog() == true)
            {
                foreach (string path in Dialog_ImportTexture.FileNames)
                {
                    Controller.ImportTexture(path);
                }

                Controller.RefreshView();
            }
        }

        private void TypeSelected(TileType _type)
        {
            TextBox_Attributes_TriggerAction.Background = Brushes.LightGray;
            TextBox_Attributes_PortalDestination.Background = Brushes.LightGray;
            TextBox_Attributes_NPCID.Background = Brushes.LightGray;
            TextBox_Attributes_ObjectID.Background = Brushes.LightGray;

            TextBox_Attributes_TriggerAction.IsReadOnly = true;
            TextBox_Attributes_PortalDestination.IsReadOnly = true;
            TextBox_Attributes_NPCID.IsReadOnly = true;
            TextBox_Attributes_ObjectID.IsReadOnly = true;

            switch (_type)
            {
                default:
                case TileType.Default:
                    break;
                case TileType.Collision:
                    break;
                case TileType.Trigger:
                    TextBox_Attributes_TriggerAction.IsReadOnly = false;
                    TextBox_Attributes_TriggerAction.Background = Brushes.White;
                    break;
                case TileType.PlayerStart:
                    break;
                case TileType.Portal:
                    TextBox_Attributes_PortalDestination.IsReadOnly = false;
                    TextBox_Attributes_PortalDestination.Background = Brushes.White;
                    break;
                case TileType.NPCSpot:
                    TextBox_Attributes_NPCID.IsReadOnly = false;
                    TextBox_Attributes_NPCID.Background = Brushes.White;
                    break;
                case TileType.ObjectSpot:
                    TextBox_Attributes_ObjectID.IsReadOnly = false;
                    TextBox_Attributes_ObjectID.Background = Brushes.White;
                    break;
            }

        }

        private void MenuItem_Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsDialog optionsDlg = new OptionsDialog(Controller);
            optionsDlg.ShowDialog();
        }

        private void Brush(object sender, RoutedEventArgs e)
        {
            try
            {
                LevelEditorTool Tool = LevelEditorTool.Brush;
                Controller.Model.Tool = Tool;
            }
            catch { }
        }

        private void Collision(object sender, RoutedEventArgs e)
        {
            try
            {
                LevelEditorTool Tool = LevelEditorTool.Collision;
                Controller.Model.Tool = Tool;
            }
            catch { }
        }

        private void MenuItem_SpriteCutter_Click(object sender, RoutedEventArgs e)
        {
            SpriteCutter spriteCutter = new SpriteCutter();
            spriteCutter.ShowDialog();
        }
    }
}