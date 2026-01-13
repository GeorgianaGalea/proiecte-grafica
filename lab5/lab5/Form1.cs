using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form1 : Form
    {
        Bitmap srcImg;
        Bitmap clipImg;

        Rectangle clipRect = new Rectangle(50, 50, 150, 120);

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Width = 700;
            Height = 500;

            srcImg = new Bitmap(@"../../poza.jpg");
            clipImg = ClipImg(srcImg, clipRect);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(srcImg, 0, 0);
            e.Graphics.DrawImage(clipImg, 350, 150);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        Bitmap ClipImg(Bitmap img, Rectangle clip)
        {
            Bitmap outImg = new Bitmap(clip.Width, clip.Height);

            for (int y = 0; y < clip.Height; y++)
            {
                for (int x = 0; x < clip.Width; x++)
                {
                    int sx = clip.X + x;
                    int sy = clip.Y + y;

                    if (sx >= 0 && sx < img.Width &&
                        sy >= 0 && sy < img.Height)
                    {
                        Color col = img.GetPixel(sx, sy);
                        outImg.SetPixel(x, y, col);
                    }
                }
            }

            return outImg;
        }
    }
}
