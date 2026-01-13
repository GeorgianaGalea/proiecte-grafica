using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6
{
    public partial class Form1 : Form
    {
        private readonly List<Point> controlPoints = new List<Point>();
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && controlPoints.Count < 4)
            {
                controlPoints.Add(e.Location);
                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                controlPoints.Clear();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var p in controlPoints)
                g.FillEllipse(Brushes.Red, p.X - 4, p.Y - 4, 8, 8);

            if (controlPoints.Count > 1)
                g.DrawLines(Pens.Gray, controlPoints.ToArray());

            if (controlPoints.Count == 4)
                DrawBezier(g, controlPoints.ToArray());
        }

        private void DrawBezier(Graphics g, Point[] pts)
        {
            int steps = 200;
            PointF prev = BezierPoint(pts, 0);

            for (int i = 1; i <= steps; i++)
            {
                float t = i / (float)steps;
                PointF next = BezierPoint(pts, t);
                g.DrawLine(Pens.Blue, prev, next);
                prev = next;
            }
        }

        private PointF BezierPoint(Point[] pts, float t)
        {
            float x =
                (float)(Math.Pow(1 - t, 3) * pts[0].X +
                3 * Math.Pow(1 - t, 2) * t * pts[1].X +
                3 * (1 - t) * Math.Pow(t, 2) * pts[2].X +
                Math.Pow(t, 3) * pts[3].X);

            float y =
                (float)(Math.Pow(1 - t, 3) * pts[0].Y +
                3 * Math.Pow(1 - t, 2) * t * pts[1].Y +
                3 * (1 - t) * Math.Pow(t, 2) * pts[2].Y +
                Math.Pow(t, 3) * pts[3].Y);

            return new PointF(x, y);
        }
    }
}
