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

namespace ImageHandler.UserControls
{
    /// <summary>
    /// Interaction logic for ImageArea.xaml
    /// </summary>
    public partial class ImageArea : UserControl
    {
        public ImageArea()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();
            try
            {
                if ((bool)result)
                {
                    Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(openFileDialog.FileName));
                }
                if ((bool)result)
                {
                    //ImageHandler.Init(openFileDialog.FileName);
                    //LblNoFile.Visible = false;
                    //setImage(ImageHandler.ResultImage.source);
                }
            }
            catch (Exception) { }       // TODO: Output an error 
        }
    }
}
