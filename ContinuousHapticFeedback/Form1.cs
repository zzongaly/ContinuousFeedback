using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuousHapticFeedback
{
    public partial class Form1 : Form
    {
        int mode;
        const int idleMode = 0, zoomInMode = 1, zoomOutMode = 2;

        int initMouseX;
        int prevMouseX;
        int threshold = 10;
        double coef = 0.001;
        Size size;

        Bitmap currentBitmap, stoneBitmap;

        public Form1()
        {
            mode = idleMode;
            initMouseX = -1;
            prevMouseX = -1;
            
            stoneBitmap = new Bitmap(ContinuousHapticFeedback.Properties.Resources.stone);
            size = stoneBitmap.Size;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = stoneBitmap;
        }

        private void zoomIn(int factor)
        {
            zoom(factor);
        }
        private void zoomOut(int factor)
        {
            zoom(-factor);
        }

        private void zoom(int factor)
        {
            int zoomFactor = factor > 0 ? 1 : -1;
            size = new Size((int)(size.Width + zoomFactor), (int)(size.Height + zoomFactor));
            if(currentBitmap != null)
                currentBitmap.Dispose();
            currentBitmap = new Bitmap(stoneBitmap, size);
            this.pictureBox1.Image = currentBitmap;
        }        
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                this.pictureBox1_MouseUp(sender, e);
                return;
            }
        
            if (initMouseX == -1)
                initMouseX = e.X;

            switch (mode)
            {
                case idleMode:
                    if (e.X - initMouseX > threshold)
                    {
                        mode = zoomInMode;
                        prevMouseX = e.X;
                        textBox1.Text = "ZoomIn";
                    }
                    else if (e.X - initMouseX < -threshold)
                    {
                        mode = zoomOutMode;
                        prevMouseX = e.X;
                        textBox1.Text = "ZoomOut";
                    }
                    break;
                case zoomInMode:
                    if (e.X > prevMouseX)
                    {
                        zoomIn(e.X - prevMouseX);
                    }
                    prevMouseX = e.X;
                    break;
                case zoomOutMode:
                    if (e.X < prevMouseX)
                    {
                        zoomOut(prevMouseX - e.X);
                    }
                    prevMouseX = e.X;
                    break;
                default:
                    break;
            }   
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            initMouseX = -1;
            mode = idleMode;
            textBox1.Text = "Idle";
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            initMouseX = -1;
            mode = idleMode;
            textBox1.Text = "Idle";
        }

       

    }
}
