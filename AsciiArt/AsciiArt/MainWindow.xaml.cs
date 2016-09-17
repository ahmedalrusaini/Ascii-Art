using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
namespace Study
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            textBox.TextWrapping = TextWrapping.NoWrap;
            textBox.FontSize = 5;
        }
   
        string link;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "이미지를 선택하세요";
            op.Filter = "All supported graphics |*.jpg;*.jpeg;*.png";
            if (op.ShowDialog() == true)
            {
                Loaded_Image.Source = new BitmapImage(new Uri(op.FileName));
                this.link = op.FileName;
                mHeight.Text =((int)Loaded_Image.Source.Height).ToString();
                mWidth.Text = ((int)Loaded_Image.Source.Width).ToString();
            } 
        }
        public void start()
        {
            textBox.Text = "";
            System.Drawing.Image item = System.Drawing.Image.FromFile(@"" + this.link);
            Bitmap Picture = new Bitmap(item, Int32.Parse(mWidth.Text), Int32.Parse(mHeight.Text));
            string line = "";
            char[] Chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };

            for (int i = 0x0; i < Picture.Height; i++)
            {
                for (int x = 0x0; x < Picture.Width; x++)
                {
                    Color color = ((Bitmap)Picture).GetPixel(x, i);
                    int Gray = (color.R + color.G + color.B) / 0x3;
                    int Index = (Gray * (Chars.Length - 0x1)) / 0xFF;
                    line += Chars[Index];
                }
                textBox.AppendText(line);
                textBox.AppendText(Environment.NewLine);
                line = "";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {     
            if (this.link != null)
            {
                start();
            }
        }

        private void mHeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!char.IsDigit(e.Text,e.Text.Length-1))
            {
                e.Handled = true;
            }
        }

        private void mWidth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!char.IsDigit(e.Text,e.Text.Length-1))
            {
                e.Handled = true;
            }
        }
    }
}
