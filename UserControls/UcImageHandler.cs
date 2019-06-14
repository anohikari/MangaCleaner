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

        public UcImageHandler()
        {
            InitializeComponent();
            LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible = false;
            SpeechBubble.ImageHandler = ImageHandler;
            ImageBuffer.ImageHandler = ImageHandler;
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
                    setImage(ImageHandler.ResultImage.source);
                }
                catch (Exception) { }       // TODO: Output an error 
            }
        }

        private void Level_CheckedChanged(object sender, EventArgs e)
        {
            if (Level.Checked)
            {
                ImageHandler.ResultImage.LockBits();
                LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible = true;
            }
            else
            {
                ImageHandler.ResultImage.UnlockBits();
                LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible = false;
            }
        }

        private void PbDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (Level.Checked && ImageHandler.CurrentImage != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    ImageHandler.LowerBound = Convert.ToByte(ImageHandler.ResultImage.GetPixel((int)(e.X / ScalingFactor), (int)(e.Y / ScalingFactor)).GetBrightness() * 255);
                    LblDarkest.Text = "Lower Brightness Threshhold:\n" + ImageHandler.LowerBound.ToString();

                }
                if (e.Button == MouseButtons.Right)
                {
                    ImageHandler.UpperBound = Convert.ToByte(ImageHandler.ResultImage.GetPixel((int)(e.X / ScalingFactor), (int)(e.Y / ScalingFactor)).GetBrightness() * 255);
                    LblBrightest.Text = "Upper Brightness Threshhold:\n" + ImageHandler.UpperBound.ToString();
                }
            }
            else if (ImageHandler.CurrentImage != null)
            {
                ImageHandler.ChangeBuffer = new LockBitmap(ImageHandler.ResultImage.source);
                SpeechBubble newBubble = new SpeechBubble((int) ((double)e.X / ScalingFactor), (int)((double)e.Y / ScalingFactor));
                newBubble.CleanBubble();
                setImage(ImageHandler.ResultImage.source);
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
    }
}
