using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MangaCleaner
{
    class Model : INotifyPropertyChanged
    {

        MainWindow MainWindow;
        ImagePreprocessor ImagePreProcessor = new ImagePreprocessor();
        FileManager ImageBuffer = null;
        OpenFileDialog openFileDialog = new OpenFileDialog();

        private string LastLoadedImagePath = "";
        List<SpeechBubble> speechBubbles = new List<SpeechBubble>();

        public WriteableBitmap VisibleImage
        {
            get
            {
                return MainWindow.InternalImageToggle.IsChecked == true ? currentImageInternal : currentImage;
            }
        }

        private WriteableBitmap currentImage;
        private WriteableBitmap CurrentImage
        {
            set
            {
                currentImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormats.Bgr32, null, 0)); ;
                if (value != null)
                {
                    CurrentImageInternal = currentImage.Clone();
                    if (currentImageInternal.Format != PixelFormats.Bgr32)
                    {
                        currentImageInternal = new WriteableBitmap( new FormatConvertedBitmap(currentImageInternal, PixelFormats.Bgr32, null, 0));
                    }
                    ImagePreProcessor.FlattenImage(ref currentImageInternal);
                    ImageVisible = Visibility.Visible;
                    RaisePropertyChanged("VisibleImage");
                }
            }
        }

        private WriteableBitmap currentImageInternal;
        private WriteableBitmap CurrentImageInternal
        {
            set
            {
                currentImageInternal = value;
                if (MainWindow.InternalImageToggle.IsChecked == true)
                {
                    RaisePropertyChanged("VisibleImage");
                }
            }
        }

        private bool imageLoaded = false;
        public Visibility ImageVisible
        {
            get => imageLoaded ? Visibility.Visible : Visibility.Hidden;
            set
            {
                imageLoaded = value == Visibility.Visible;
                RaisePropertyChanged("ImageVisible");
            }
        }

        public Model(MainWindow mainWindow)
        {
            SubscribeEvents(mainWindow);
        }
        private void SubscribeEvents(MainWindow mainWindow)
        {
            mainWindow.MainImage.MouseUp += Image_Click;
            mainWindow.NextImageButton.Click += NextImage_Click;
            mainWindow.PreviousImageButton.Click += PreviousImage_Click;
            mainWindow.LoadImageButton.Click += LoadImage;
            mainWindow.InternalImageToggle.Click += (object sender, RoutedEventArgs e) => RaisePropertyChanged("VisibleImage");
            mainWindow.Undo.Click += Undo_Click;
            mainWindow.Reload.Click += Reload_Click;
            MainWindow = mainWindow;
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            if(LastLoadedImagePath != "")
            {
                CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            if(openFileDialog.ShowDialog() == true)
            {
                CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(openFileDialog.FileName)));
                ImageBuffer = new FileManager(openFileDialog.FileName);
                LastLoadedImagePath = openFileDialog.FileName;
            }
        }
        private void Image_Click(object sender, MouseButtonEventArgs e)
        {
            if (!imageLoaded) { return; }
            Point p = e.GetPosition(((Image)sender));
            double relativeHeight = ((Image)sender).ActualHeight;
            double relativeWidth = ((Image)sender).ActualWidth;
            Point AbsolutePoint = new Point(
                VisibleImage.PixelHeight / relativeHeight * p.X,
                VisibleImage.PixelWidth / relativeWidth * p.Y);
            SpeechBubble newBubble = new SpeechBubble(ref currentImageInternal, ref currentImage ,AbsolutePoint);
            newBubble.CleanBubble();
            speechBubbles.Add(newBubble);
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            ImageBuffer.Save(currentImage);
            if(ImageBuffer != null)
            {
                LastLoadedImagePath = ImageBuffer.getNextFile();
                CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));
            }
        }
        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (ImageBuffer != null)
            {
                LastLoadedImagePath = ImageBuffer.getPreviousFile();
                CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }
        #endregion
    }
}
