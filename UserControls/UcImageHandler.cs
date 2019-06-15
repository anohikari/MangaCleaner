using System;
using System.Drawing;
using System.IO;
//using ImageProcessor;
using System.Windows.Forms;
using ImageHandler.Klassen;

namespace ImageHandler
{
    public partial class UcImageHandler : UserControl
    {
        private ImageHandler ImageHandler = new ImageHandler();
        double ScalingFactor = 1;

        public delegate void  ChangeBufferlabel(int size);

        public UcImageHandler()
        {
            InitializeComponent();
            ImageBuffer.BufferSizeChanged += CallMainThread;
            SpeechBubble.ImageHandler = ImageHandler;
            ImageBuffer.ImageHandler = ImageHandler;
            LblNoFile.Text = "Click here to load an image! \n Or Drag and Drop it onto the Form!";
            LblControlLevel.Text = "Note: You can also click left/right on any pixel to set it´s brightness as the lower/upper threshhold!";
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (DialogResult.OK == result)
            {
                Directory.SetCurrentDirectory(Path.GetDirectoryName(openFileDialog.FileName));
            }
            if (result == DialogResult.OK)
            {
                try
                {
                    ImageHandler.Init(openFileDialog.FileName);
                    LblNoFile.Visible = false;
                    setImage(ImageHandler.ResultImage.source);
                }
                catch (Exception) { }       // TODO: Output an error 
            }
        }

        private void Level_CheckedChanged(object sender, EventArgs e)
        {
            if(ImageHandler.ResultImage != null)
            {
                if (Level.Checked)
                {
                    ImageHandler.ResultImage.LockBits();
                    LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible =  NumThreshholdLower.Visible = NumThreshholdUp.Visible = LblControlLevel.Visible = true;
                    Level.Text = "Reenable Bubble Deletion";
                }
                else
                {
                    ImageHandler.ResultImage.UnlockBits();
                    LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible = NumThreshholdLower.Visible = NumThreshholdUp.Visible = LblControlLevel.Visible = false;
                    Level.Text = "Level Image";
                }
            }
            else
            {
                Level.Checked = false;
            }
        }

        private void PbDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (Level.Checked && ImageHandler.ResultImage != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    ImageHandler.LowerBound = Convert.ToByte(ImageHandler.ResultImage.GetPixel((int)(e.X / ScalingFactor), (int)(e.Y / ScalingFactor)).GetBrightness() * 255);
                    NumThreshholdLower.Value = ImageHandler.LowerBound;

                }
                if (e.Button == MouseButtons.Right)
                {
                    ImageHandler.UpperBound = Convert.ToByte(ImageHandler.ResultImage.GetPixel((int)(e.X / ScalingFactor), (int)(e.Y / ScalingFactor)).GetBrightness() * 255);
                    NumThreshholdUp.Value = ImageHandler.UpperBound;
                }
            }
            else if (ImageHandler.CurrentImage != null)
            {
                ImageHandler.ChangeBuffer = new LockBitmap(ImageHandler.ResultImage.source);
                SpeechBubble newBubble = new SpeechBubble((int) ((double)e.X / ScalingFactor), (int)((double)e.Y / ScalingFactor));
                newBubble.CleanBubble();
                setImage(ImageHandler.ResultImage.source);
            }
            else
            {
                loadDataToolStripMenuItem_Click(sender, e);
            }
                
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (ImageHandler.LowerBound != 0 && ImageHandler.UpperBound != 0)
            {
                ImageHandler.Level();
            }
            Level.Checked = false;
            setImage(ImageHandler.ResultImage.source);
        }


        private void CmdUndo_Click(object sender, EventArgs e)
        {
            ImageHandler.ResultImage = ImageHandler.ChangeBuffer;
            setImage(ImageHandler.ResultImage.source);
            SpeechBubble.CurrentImage = ImageHandler.CurrentImage;
        }

        private void CmdReload_Click(object sender, EventArgs e)
        {
            ImageHandler.Reload();
            setImage(ImageHandler.ResultImage.source);
        }

        private void CmdNextImage_Click(object sender, EventArgs e)
        {
            ImageHandler.LoadNextimageFromBuffer();
            setImage(ImageHandler.ResultImage.source);
        }

        public void setImage(Bitmap image)
        {
            Bitmap FormattedImage = new Bitmap(image);
            ScalingFactor = Math.Min(PbDisplay.Height / (double)image.Height, PbDisplay.Width / (double)image.Width);
            int newWidth = (int)(image.Width * ScalingFactor);
            int newHeight = (int)(image.Height * ScalingFactor);
            Size Size = new Size(newWidth, newHeight);
            Bitmap ResizedImage = new Bitmap(FormattedImage, Size);
            PbDisplay.Image = ResizedImage;

        }

        private void splitContainer1_Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            String[] droppedFiles = (String[])e.Data.GetData(DataFormats.FileDrop);
            Directory.SetCurrentDirectory(Path.GetDirectoryName(droppedFiles[0]));
            ImageHandler.Init(droppedFiles[0]);
            setImage(ImageHandler.ResultImage.source);
        }

        private void CheckDebug_CheckedChanged(object sender, EventArgs e)
        {
            if(ImageHandler.CurrentImage != null)
            {
                if (CheckDebug.Checked && ImageHandler.CurrentImage != null)
                {
                    setImage(ImageHandler.CurrentImage.source);
                }
                else
                {
                    setImage(ImageHandler.ResultImage.source);
                }
            }

        }

        private void NumThreshholdLower_ValueChanged(object sender, EventArgs e)
        {
            ImageHandler.LowerBound = (byte)NumThreshholdLower.Value;
        }

        private void NumThreshholdUp_ValueChanged(object sender, EventArgs e)
        {
            ImageHandler.UpperBound = (byte)NumThreshholdUp.Value;
        }
        public void CallMainThread(int size)
        {
            ChangeBufferlabel mainThreadDelegate = new ChangeBufferlabel(setBufferLabel);
            Invoke(mainThreadDelegate, size);
        }

        public void setBufferLabel(int size)
        {
            LblBufferSize.Text = "Current Buffersize = " + size.ToString();
        }

    }
}
