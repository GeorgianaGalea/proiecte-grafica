using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        private TrackBar depthBar;
        private int maxDepth = 5;
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(500, 500);
            this.BackColor = Color.SkyBlue;
            this.DoubleBuffered = true;

            depthBar = new TrackBar();
            depthBar.Minimum = 1;
            depthBar.Maximum = 8;
            depthBar.Value = 5;
            depthBar.Dock = DockStyle.Bottom;
            depthBar.TickFrequency = 1;
            depthBar.Scroll += DepthBar_Scroll;

            this.Controls.Add(depthBar);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void DepthBar_Scroll(object sender, EventArgs e)
        {
            maxDepth = depthBar.Value;
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            DrawCloud(g, 250, 250, 100, maxDepth);
        }
        private void DrawCloud( Graphics g, int cx,int cy, int r,  int depth)
        {
            if (depth == 0) return;

            g.FillEllipse(Brushes.White, cx - r, cy - r, r * 2, r * 2);

            int branches = rnd.Next(3, 6);

            for (int i = 0; i < branches; i++)
            {
                double ang = rnd.NextDouble() * 2 * Math.PI;

                int newR = (int)(r * 0.6);
                int nx = cx + (int)(Math.Cos(ang) * r * 0.8);
                int ny = cy + (int)(Math.Sin(ang) * r * 0.8);

                DrawCloud(g, nx, ny, newR, depth - 1);
            }
        }
     

       
    }
}
