using RPGFramework;
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
    /// Interaction logic for Dialog_NewLevel.xaml
    /// </summary>
    public partial class Dialog_NewLevel : Window
    {
        public LevelEditor_Controller Controller;

        public Dialog_NewLevel(LevelEditor_Controller _controller)
        {
            InitializeComponent();
            Controller = _controller;
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            Controller.Model.CurrentLevel = new RPGFramework.Level();

            int XDimension = 0;
            int YDimension = 0;

            XDimension = System.Convert.ToInt32(TextBlock_DimensionX.Text);
            YDimension = System.Convert.ToInt32(TextBlock_DimensionY.Text);

            Controller.NewLevel(TextBlock_Name.Text, XDimension, YDimension);

            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
