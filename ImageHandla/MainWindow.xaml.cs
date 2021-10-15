using System.Windows;
using System.Windows.Controls;

namespace MangaCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new Model();
        }

        private void MainImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var image = (Image)sender;
            var p = e.GetPosition(image);
            var Model = (DataContext as Model);
            var relativeHeight = image.ActualHeight;
            var relativeWidth = image.ActualWidth;
            Model.Image_Click(new Point(
                Model.VisibleImage.PixelHeight / relativeHeight * p.X,
                Model.VisibleImage.PixelWidth / relativeWidth * p.Y));
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            var Files = (string[])e.Data.GetData(DataFormats.FileDrop);
            (DataContext as Model).LoadImage(Files[0]);
        }
    }

}
