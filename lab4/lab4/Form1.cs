using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        List<Point> polygonVertices = new List<Point>()
        {
            new Point(120, 170),
            new Point(250, 130),
            new Point(350, 280),
            new Point(200, 300),
            new Point(160, 270)
        };

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Width = 500;
            this.Height = 450;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            FillPolygonScanline(e.Graphics, polygonVertices, Brushes.LightPink);
        }

        void FillPolygonScanline(Graphics graphics, List<Point> vertices, Brush fillBrush)
        {
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            foreach (Point vertex in vertices)
            {
                if (vertex.Y < minY) minY = vertex.Y;
                if (vertex.Y > maxY) maxY = vertex.Y;
            }

            for (int y = minY; y <= maxY; y++)
            {
                List<int> xIntersections = new List<int>();

                for (int i = 0; i < vertices.Count; i++)
                {
                    Point startVertex = vertices[i];
                    Point endVertex = vertices[(i + 1) % vertices.Count];

                    if (startVertex.Y == endVertex.Y)
                        continue;

                    if (y >= Math.Min(startVertex.Y, endVertex.Y) &&
                        y < Math.Max(startVertex.Y, endVertex.Y))
                    {
                        int xIntersection =
                            startVertex.X +
                            (y - startVertex.Y) *
                            (endVertex.X - startVertex.X) /
                            (endVertex.Y - startVertex.Y);

                        xIntersections.Add(xIntersection);
                    }
                }

                xIntersections.Sort();

                for (int i = 0; i < xIntersections.Count - 1; i += 2)
                {
                    int xStart = xIntersections[i];
                    int xEnd = xIntersections[i + 1];

                    graphics.DrawLine(new Pen(fillBrush), xStart, y, xEnd, y);
                }
            }
        }
    }
}
