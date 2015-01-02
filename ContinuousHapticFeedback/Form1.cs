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
        int mode = 0;
        const int idleMode = 0, zoomInMode = 1, zoomOutMode = 2;

        int initMouseX;
        int prevMouseX;
        int threshold = 10;


        public Form1()
        {
            initMouseX = -1;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
                
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case idleMode:
                    if (e.X - initMouseX > threshold)
                    {
                        mode = zoomInMode;
                        prevMouseX = e.X;
                    }
                    else if(e.X - initMouseX < -threshold)
                    {
                        mode = zoomOutMode;
                        prevMouseX = e.X;
                    }
                    break;
                case zoomInMode:
                    if (e.X > prevMouseX)
                        zoomIn(e.X - prevMouseX);
                    break;
                case zoomOutMode:
                    if (e.X < prevMouseX)
                        zoomOut(prevMouseX - e.X);
                    break;
                default:
                    break;
            }   
        }

        private void zoomIn(int factor)
        {

        }

        private void zoomOut(int factor)
        {

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            initMouseX = -1;
            mode = idleMode;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            initMouseX = e.X;            
        }
    }
}
