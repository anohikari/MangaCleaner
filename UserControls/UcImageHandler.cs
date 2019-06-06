using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
//using ImageProcessor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageHandler
{
    public partial class UcImageHandler : UserControl
    {
        private MyImages Images = new MyImages();
        double ScalingFactor = 1;

        public UcImageHandler()
        {
            InitializeComponent();
            LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible = false;
            Klassen.SpeechBubble.Status = Images;
            ImageBuffer.images = Images;
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (DialogResult.OK == result)
            {
                Directory.SetCurrentDirectory(Path.GetDirectoryName(openFileDialog.FileName));
            }
            try
            {
                Images.Init(openFileDialog.FileName);
                setImage(Images.CurrentImage.source);
            }
            catch (Exception) { }       // replace this by proper checks later
            
        }


        private void Level_CheckedChanged(object sender, EventArgs e)
        {
            if (Level.Checked)
            {
                LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible = true;
            }
            else
            {
                LblDarkest.Visible = LblBrightest.Visible = CmdStartLevel.Visible = false;
            }
        }

        private void PbDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (Level.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Images.LowerBound = Convert.ToByte(Images.result.GetPixel((int)(e.X / ScalingFactor), (int)(e.Y / ScalingFactor)).GetBrightness() * 255);
                    LblDarkest.Text = "Lower Brightness Threshhold:\n" + Images.LowerBound.ToString();

                }
                if (e.Button == MouseButtons.Right)
                {
                    Images.UpperBound = Convert.ToByte(Images.result.GetPixel((int)(e.X / ScalingFactor), (int)(e.Y / ScalingFactor)).GetBrightness() * 255);
                    LblBrightest.Text = "Upper Brightness Threshhold:\n" + Images.UpperBound.ToString();
                }
            }
            else if (Images.CurrentImage != null)
            {
                Klassen.SpeechBubble.Images = Images.CurrentImage;
                Images.ChangeBuffer = Images.result.Clone() as Bitmap;
                Klassen.SpeechBubble.addBubble((int) ((double)e.X / ScalingFactor), (int)((double)e.Y / ScalingFactor));
                setImage(Images.result);
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (Images.LowerBound != 0 && Images.UpperBound != 0)
                Images.result = Images.level(Images.result);
            setImage(Images.result);
        }


        private void CmdUndo_Click(object sender, EventArgs e)
        {
            Images.result = Images.ChangeBuffer;
            setImage(Images.result);
            Klassen.SpeechBubble.Images = Images.CurrentImage;
        }

        private void CmdReload_Click(object sender, EventArgs e)
        {
            try
            {
                Images.Init(openFileDialog.FileName);
                PbDisplay.Image = Images.result;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void CmdNextImage_Click(object sender, EventArgs e)
        {
            Images.LoadNextimageFromBuffer();
            setImage(Images.CurrentImage.source);
        }

        public void setImage(Bitmap image)
        {
            Bitmap b = image;
            ScalingFactor = Math.Max(PbDisplay.Height / (double)image.Height, PbDisplay.Width / (double)image.Width);
            int newWidth = (int)(image.Width * ScalingFactor);
            int newHeight = (int)(image.Height * ScalingFactor);
            Size s = new Size(newWidth, newHeight);
            Bitmap resized = new Bitmap(b, s);
            PbDisplay.Image = resized;

        }

    }
}
