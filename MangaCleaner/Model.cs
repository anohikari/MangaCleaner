using MangaCleaner.Classes;
using MangaCleaner.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MangaCleaner
{
    class Model : ViewModelBase
    {
        private ImagePreprocessor ImagePreProcessor = new ImagePreprocessor();
        private OpenFileDialog OpenfileDialog = new OpenFileDialog();
        private UndoRedoStack UndoRedoStack = new UndoRedoStack();
        private FileManager FileManager = null;

        private string LastLoadedImagePath = string.Empty;
        private List<SpeechBubble> speechBubbles = new List<SpeechBubble>();

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
                        currentImageInternal = new WriteableBitmap(new FormatConvertedBitmap(currentImageInternal, PixelFormats.Bgr32, null, 0));
                    }
                    ImagePreProcessor.FlattenImage(currentImageInternal);
                    ImageVisible = Visibility.Visible;
                    OnPropertyChanged(nameof(VisibleImage));
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
                    OnPropertyChanged(nameof(VisibleImage));
            }
        }

        private bool showInternalImage = false;
        public bool ShowInternalImage
        {
            get => showInternalImage;
            set
            {
                SetProperty(ref showInternalImage, value);
                OnPropertyChanged(nameof(VisibleImage));
            }
        }

        private bool imageLoaded = false;
        public Visibility ImageVisible
        {
            get => imageLoaded ? Visibility.Visible : Visibility.Hidden;
            set
            {
                imageLoaded = value == Visibility.Visible;
                OnPropertyChanged(nameof(ImageVisible));
            }
        }
        public ICommand NextImage { get; init; }
        public ICommand PreviousImage { get; private set; }
        public ICommand LoadImages { get; private set; }
        public ICommand Undo { get; private set; }
        public ICommand Redo { get; private set; }
        public ICommand Reload { get; private set; }
        public ICommand ShowResults { get; private set; }
        public ICommand OCR { get; private set; }

        public Model()
        {
            NextImage = new RelayCommand(NextImage_Click);
            PreviousImage = new RelayCommand(PreviousImage_Click);
            LoadImages = new RelayCommand(LoadImage);
            Undo = new RelayCommand(Undo_Click);
            Redo = new RelayCommand(RedoCommand);
            Reload = new RelayCommand(Reload_Click);
            ShowResults = new RelayCommand(ShowResults_Click);
            OCR = new AsyncCommand(CallOCRAPI);
        }

        private void Reload_Click()
        {
            if (LastLoadedImagePath != string.Empty)
                CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));

            UndoRedoStack.Clear();
        }

        private void Undo_Click()
        {
            UndoRedoStack.Undo();
            OnPropertyChanged(nameof(VisibleImage));
        }

        private void RedoCommand()
        {
            UndoRedoStack.Redo();
            OnPropertyChanged(nameof(VisibleImage));
        }

        private async void LoadImage()
        {
            if (OpenfileDialog.ShowDialog() == true)
            {
                LoadImage(OpenfileDialog.FileName);
            }
        }

        internal void LoadImage(string FileName)
        {
            CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(FileName)));

            FileManager = new FileManager(FileName);
            LastLoadedImagePath = FileName;
        }

        internal void Image_Click(Point AbsolutePoint)
        {
            if (!imageLoaded)
                return; 

            var newBubble = new SpeechBubble(currentImageInternal, currentImage, AbsolutePoint);
            var backup = new List<ObjectBackup>
            {
                new ObjectBackup(
                    x => this.currentImage = x as WriteableBitmap,
                    y => newBubble.CleanBubble(),
                    currentImage.Clone()),
                new ObjectBackup(
                    x => this.currentImageInternal = x as WriteableBitmap,
                    y => newBubble.CleanBubble(),
                    currentImageInternal.Clone()
                )
            };
            var undoBackup = new UndoAble(backup);
            UndoRedoStack.Push(undoBackup);
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
            FileManager.Save(currentImage);
            if (FileManager is null)
                return;
            LastLoadedImagePath = FileManager.getNextFile();
            CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));
        }
        private void PreviousImage_Click()
        {
            if (FileManager is null)
                return;
            LastLoadedImagePath = FileManager.getPreviousFile();
            CurrentImage = new WriteableBitmap(new BitmapImage(new Uri(LastLoadedImagePath)));
        }

        private void ShowResults_Click()
        {
            Process.Start("explorer.exe", FileManager.GetSaveDirectory());
        }

        private async Task CallOCRAPI()
        {
            // Compresses image size to < 1MB
            var compressedImage = await ImageCompression.CompressImageFile(LastLoadedImagePath, 1000000);
            var points = await FreeOCRWrapper.OCR(compressedImage);
            foreach (var point in points)
            {
                var newBubble = new SpeechBubble(currentImageInternal, currentImage, point);
                newBubble.CleanBubble();
            }
        }
    }
}
