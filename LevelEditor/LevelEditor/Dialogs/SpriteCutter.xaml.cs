using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LevelEditor.Dialogs
{
    /// <summary>
    /// Interaktionslogik für SpriteCutter.xaml
    /// </summary>
    public partial class SpriteCutter : Window
    {
        BitmapImage CurrentSpritesheet;

        Vector StartPos;
        Vector EndPos;
        double CutoutWidth;
        double CutoutHeight;
        double ColumnsCount;
        double RowsCount;
        double WidthPerColumn;
        double HeightPerRow;

        public SpriteCutter()
        {
            InitializeComponent();
        }
        public void ApplyControls()
        {
            Canvas_Spritesheet.Children.Clear();

            StartPos = new Vector(Convert.ToDouble(TextBox_StartPosX.Text), Convert.ToDouble(TextBox_StartPosY.Text));
            EndPos = new Vector(Convert.ToDouble(TextBox_EndPosX.Text), Convert.ToDouble(TextBox_EndPosY.Text));

            CutoutWidth = EndPos.X - StartPos.X;
            CutoutHeight = EndPos.Y - StartPos.Y;

            ColumnsCount = Convert.ToInt32(TextBox_ColumnsCount.Text);
            RowsCount = Convert.ToInt32(TextBox_RowsCount.Text);

            if (ColumnsCount <= 0 || RowsCount <= 0)
            {
                return;
            }

            WidthPerColumn = CutoutWidth / ColumnsCount;
            HeightPerRow = CutoutHeight / RowsCount;

            // Create a red Brush  
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            for (int x = 0; x < ColumnsCount + 1; x++)
            {
                // Column Lines
                Line newColumnLine = new Line();
                newColumnLine.X1 = (x * WidthPerColumn) + StartPos.X;
                newColumnLine.Y1 = StartPos.Y;
                newColumnLine.X2 = newColumnLine.X1;
                newColumnLine.Y2 = EndPos.Y;

                newColumnLine.Stroke = redBrush;
                newColumnLine.StrokeThickness = 3.0f;

                Canvas_Spritesheet.Children.Add(newColumnLine);
            }

            for (int y = 0; y < RowsCount + 1; y++)
            {
                // Row Lines
                Line newRowLine = new Line();
                newRowLine.X1 = StartPos.X;
                newRowLine.Y1 = (y * HeightPerRow) + StartPos.Y;
                newRowLine.X2 = EndPos.X;
                newRowLine.Y2 = newRowLine.Y1;

                newRowLine.Stroke = redBrush;
                newRowLine.StrokeThickness = 3.0f;

                Canvas_Spritesheet.Children.Add(newRowLine);
            }

            MenuItem_ExportCuts.IsEnabled = true;
        }

        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        public void LoadSpritesheet(string _path)
        {
            CurrentSpritesheet = new BitmapImage(new Uri(_path));
            Image_Spritesheet.Source = CurrentSpritesheet;

            TextBox_StartPosX.IsEnabled = true;
            TextBox_StartPosY.IsEnabled = true;
            TextBox_EndPosX.IsEnabled = true;
            TextBox_EndPosY.IsEnabled = true;
            TextBox_ColumnsCount.IsEnabled = true;
            TextBox_RowsCount.IsEnabled = true;

            TextBox_EndPosX.Text = Convert.ToString(Convert.ToInt32(CurrentSpritesheet.Width));
            TextBox_EndPosY.Text = Convert.ToString(Convert.ToInt32(CurrentSpritesheet.Height));
        }

        private void MenuItem_Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_LoadSpritesheet_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.InitialDirectory = Environment.CurrentDirectory;

            if (FileDialog.ShowDialog() == true)
            {
                LoadSpritesheet(FileDialog.FileName);
            }
        }

        private void MenuItem_ExportCuts_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.InitialDirectory = Environment.CurrentDirectory;

            PngBitmapEncoder pngEncoder;
            FileStream mStream;

            if (FileDialog.ShowDialog() == true)
            {
                int ImageCounter = 0;

                foreach (CroppedBitmap image in CutImages())
                {
                    ImageCounter++;

                    mStream = new FileStream(FileDialog.FileName + "_" + ImageCounter.ToString() + ".png", FileMode.Create);
                    pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(BitmapFrame.Create(image));
                    pngEncoder.Save(mStream);
                    mStream.Close();
                }
            }
        }

        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {
            ApplyControls();
        }

        public List<CroppedBitmap> CutImages()
        {
            List<CroppedBitmap> ReturnList = new List<CroppedBitmap>();

            for (int y = 0; y < RowsCount; y++)
            {
                for (int x = 0; x < ColumnsCount; x++)
                {
                    Vector CropStartPos = new Vector(StartPos.X + (x * WidthPerColumn), StartPos.Y + (y * HeightPerRow));
                    Vector CropEndPos = new Vector(StartPos.X + ((x + 1) * WidthPerColumn), StartPos.Y + ((y + 1) * HeightPerRow));

                    CroppedBitmap CropBitmap = new CroppedBitmap(CurrentSpritesheet,
                        new Int32Rect(
                            Convert.ToInt32(CropStartPos.X),
                            Convert.ToInt32(CropStartPos.Y),
                            Convert.ToInt32(CropEndPos.X - CropStartPos.X),
                            Convert.ToInt32(CropEndPos.Y - CropStartPos.Y)
                            ));

                    ReturnList.Add(CropBitmap);
                }
            }

            return ReturnList;
        }

        private void TextBox_EndPosX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(TextBox_EndPosX.Text) > CurrentSpritesheet.Width)
                {
                    TextBox_EndPosX.Text = Convert.ToString(CurrentSpritesheet.Width);
                }

                if (Convert.ToDouble(TextBox_StartPosX.Text) < 0)
                {
                    TextBox_StartPosX.Text = "0";
                }
                else if (Convert.ToDouble(TextBox_StartPosX.Text) > Convert.ToDouble(TextBox_EndPosX.Text))
                {
                    TextBox_StartPosX.Text = TextBox_EndPosX.Text;
                }
            }
            catch { }
        }

        private void TextBox_EndPosY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(TextBox_EndPosY.Text) > CurrentSpritesheet.Height)
                {
                    TextBox_EndPosY.Text = Convert.ToString(CurrentSpritesheet.Height);
                }

                if (Convert.ToDouble(TextBox_StartPosY.Text) < 0)
                {
                    TextBox_StartPosY.Text = "0";
                }
                else if (Convert.ToDouble(TextBox_StartPosY.Text) > Convert.ToDouble(TextBox_EndPosY.Text))
                {
                    TextBox_StartPosY.Text = TextBox_EndPosY.Text;
                }
            }
            catch { }
        }

        private void TextBox_StartPosX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(TextBox_StartPosX.Text) < 0)
                {
                    TextBox_StartPosX.Text = "0";
                }
                else if (Convert.ToDouble(TextBox_StartPosX.Text) > Convert.ToDouble(TextBox_EndPosX.Text))
                {
                    TextBox_StartPosX.Text = TextBox_EndPosX.Text;
                }
            }
            catch { }
        }

        private void TextBox_StartPosY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(TextBox_StartPosY.Text) < 0)
                {
                    TextBox_StartPosY.Text = "0";
                }
                else if (Convert.ToDouble(TextBox_StartPosY.Text) > Convert.ToDouble(TextBox_EndPosY.Text))
                {
                    TextBox_StartPosY.Text = TextBox_EndPosY.Text;
                }
            }
            catch { }
        }
    }
}
