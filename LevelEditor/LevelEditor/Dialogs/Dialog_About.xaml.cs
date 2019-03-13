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
    /// Interaction logic for Dialog_About.xaml
    /// </summary>
    public partial class Dialog_About : Window
    {
        public Dialog_About()
        {
            InitializeComponent();
        }

        private void Button_Okay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
