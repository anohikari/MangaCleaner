using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MangaCleaner
{
    class Model : INotifyPropertyChanged
    {
        private ImagePreprocessor ImagePreProcessor = new ImagePreprocessor();
        private FileManager ImageBuffer = null;
        private OpenFileDialog openFileDialog = new OpenFileDialog();

        private string LastLoadedImagePath = "";
        List<SpeechBubble> speechBubbles = new List<SpeechBubble>();

        public WriteableBitmap VisibleImage
        {
            get => ShowInternalImage ? currentImageInternal : currentImage;
        }

        private WriteableBitmap currentImage;
        private WriteableBitmap CurrentImage
        {
            set
            {
                currentImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormats.Bgr32, null, 0));
                if (value != null)
                {
                    CurrentImageInternal = currentImage.Clone();
                    if (currentImageInternal.Format != PixelFormats.Bgr32)
                    {
                        currentImageInternal = new WriteableBitmap( new FormatConvertedBitmap(currentImageInternal, PixelFormats.Bgr32, null, 0));
                    }
                    ImagePreProcessor.FlattenImage(currentImageInternal);
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
                if (ShowInternalImage)
                {
                    RaisePropertyChanged("VisibleImage");
                }
            }
        }

        private bool showInternalImage = false;
        public bool ShowInternalImage
        {
            get => showInternalImage;
            set
            {
                showInternalImage = value;
                RaisePropertyChanged("ShowInternalImage");
                RaisePropertyChanged("VisibleImage");
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
        public ICommand NextImage { get; private set; }
        public ICommand PreviousImage { get; private set; }
        public ICommand LoadImages { get; private set; }
        public ICommand Undo { get; private set; }
        public ICommand Reload { get; private set; }

        public Model(MainWindow mainWindow)
        {
            SubscribeEvents(mainWindow);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            NextImage = new RelayCommand(NextImage_Click);
            PreviousImage = new RelayCommand(PreviousImage_Click);
            LoadImages = new RelayCommand(LoadImage);
            Undo = new RelayCommand(Undo_Click);
            Reload = new RelayCommand(Reload_Click);
        }

        private void SubscribeEvents(MainWindow mainWindow)
        {
            // TODO: get the mousebuttoneventargs....
            mainWindow.MainImage.MouseUp += Image_Click;
        }

        private void Reload_Click()
        {
            if(LastLoadedImagePath != "")
            {
                CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));
            }
        }

        private void Undo_Click()
        {
            //TODO: Implement
        }

        private void LoadImage()
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
            var AbsolutePoint = GetAbsoluteClicklocation((sender as Image), e);
            SpeechBubble newBubble = new SpeechBubble(currentImageInternal, currentImage, AbsolutePoint);
            newBubble.CleanBubble();
            speechBubbles.Add(newBubble);
        }

        private Point GetAbsoluteClicklocation(Image image, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(image);
            var relativeHeight = image.ActualHeight;
            var relativeWidth = image.ActualWidth;
            return new Point(
                VisibleImage.PixelHeight / relativeHeight * p.X,
                VisibleImage.PixelWidth / relativeWidth * p.Y);
        }

        private void NextImage_Click()
        {
            ImageBuffer.Save(currentImage);
            if(ImageBuffer != null)
            {
                LastLoadedImagePath = ImageBuffer.getNextFile();
                CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));
            }
        }
        private void PreviousImage_Click()
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
