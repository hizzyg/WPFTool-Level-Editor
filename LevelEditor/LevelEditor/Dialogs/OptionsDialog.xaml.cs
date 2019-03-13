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
using System.Windows.Shapes;

namespace LevelEditor.Dialogs
{
    /// <summary>
    /// Interaction logic for OptionsDialog.xaml
    /// </summary>
    public partial class OptionsDialog : Window
    {
        public LevelEditor_Controller Controller;

        public OptionsDialog(LevelEditor_Controller _controller)
        {
            Controller = _controller;
            InitializeComponent();

            FillOptions();
        }

        public void FillOptions()
        {
            TextBox_SizeWidth.Text = Controller.Model.Config.WindowSizeWidth.ToString();
            TextBox_SizeHeight.Text = Controller.Model.Config.WindowSizeHeight.ToString();
            TextBox_CoordX.Text = Controller.Model.Config.WindowPositionX.ToString();
            TextBox_CoordY.Text = Controller.Model.Config.WindowPositionY.ToString();

            ComboBox_StartupLocation.Items.Clear();
            foreach (WindowStartupLocation startupLocation in (WindowStartupLocation[])Enum.GetValues(typeof(WindowStartupLocation)))
            {
                ComboBox_StartupLocation.Items.Add(startupLocation.ToString());
            }
            ComboBox_StartupLocation.SelectedIndex = (byte)Controller.Model.Config.StartupLocation;

            ComboBox_WindowState.Items.Clear();
            foreach (WindowState startupState in (WindowState[])Enum.GetValues(typeof(WindowState)))
            {
                ComboBox_WindowState.Items.Add(startupState.ToString());
            }
            ComboBox_WindowState.SelectedIndex = (byte)Controller.Model.Config.WindowState;

            ComboBox_Language.Items.Clear();
            foreach (LevelEditorLanguage language in (LevelEditorLanguage[])Enum.GetValues(typeof(LevelEditorLanguage)))
            {
                ComboBox_Language.Items.Add(language.ToString());
            }
            ComboBox_Language.SelectedIndex = (byte)Controller.Model.Config.Language;
        }

        public void ApplyOptions()
        {
            Controller.Model.Config.WindowSizeWidth = Convert.ToSingle(TextBox_SizeWidth.Text);
            Controller.Model.Config.WindowSizeHeight = Convert.ToSingle(TextBox_SizeHeight.Text);

            Controller.Model.Config.WindowPositionX = Convert.ToSingle(TextBox_CoordX.Text);
            Controller.Model.Config.WindowPositionY = Convert.ToSingle(TextBox_CoordY.Text);

            Controller.Model.Config.StartupLocation = (WindowStartupLocation)ComboBox_StartupLocation.SelectedIndex;
            Controller.Model.Config.WindowState = (WindowState)ComboBox_WindowState.SelectedIndex;
            Controller.Model.Config.Language = (LevelEditorLanguage)ComboBox_Language.SelectedIndex;

            Controller.SaveConfig();
            Controller.ApplyConfig();
            Controller.ApplyLanguage();
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {
            ApplyOptions();
        }
    }
}
