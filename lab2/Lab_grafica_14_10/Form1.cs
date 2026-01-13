using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_grafica_14_10//lab 2
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Graphics g;
        Bitmap bitmap;
        Bitmap bitmap1;
        Timer timer = new Timer();
        int light = 0;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            bitmap1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g = Graphics.FromImage(bitmap1);

        }
        void DrawCircleExplicit(Graphics g, int x0, int y0, int r, Pen pen)
        {
            for (int x = x0 - r; x <= x0 + r; x++)
            {
                double dx = x - x0;
                double inside = r * r - dx * dx;

                if (inside >= 0)
                {
                    double sqrt = Math.Sqrt(inside);
                    int y1 = y0 + (int)Math.Round(sqrt);
                    int y2 = y0 - (int)Math.Round(sqrt);

                    g.DrawRectangle(pen, x, y1, 1, 1);
                    g.DrawRectangle(pen, x, y2, 1, 1);
                }
            }
        }

        void DrawCirclePolar(Graphics g, int x0, int y0, int r, Pen pen)
        {
            double step = 0.01;
            double theta = 0;

            while (theta <= 2 * Math.PI)
            {
                int x = x0 + (int)(r * Math.Cos(theta));
                int y = y0 + (int)(r * Math.Sin(theta));
                g.DrawRectangle(pen, x, y, 1, 1);
                theta += step;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Random rand1 = new Random();
            int rnd = rand1.Next();
            int x = pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2;
            Pen p = new Pen(Color.DarkGray);
            int r = 60;
            graphics.DrawRectangle(p, x - 100 , y - 220 , 200, 450);
            graphics.FillRectangle(Brushes.Gray, x - 100, y - 220, 200, 450);
       
            DrawCirclePolar(graphics, x, y, r, new Pen(Color.Yellow));
            graphics.FillEllipse(Brushes.LightYellow, x - r, y - r, r*2, r * 2);

            DrawCirclePolar(graphics, x, y - 140, r, new Pen(Color.Red));
            graphics.FillEllipse(Brushes.LightCoral, x - r, y - 200, r * 2, r * 2);

            DrawCirclePolar(graphics, x, y + 140, r, new Pen(Color.Green));
            graphics.FillEllipse(Brushes.LightGreen, x - r, y + 80, r * 2, r * 2);


            timer.Interval = 2000;
            timer.Tick += (s, args) =>
            {
                light = (light + 1) % 3; 
                pictureBox1.Invalidate(); 
            };
            timer.Start();

            pictureBox1.Paint += (s, args) =>
            {
                Graphics g = args.Graphics;

           
                g.FillEllipse(light == 0 ? Brushes.Red : Brushes.LightCoral, x - r, y - r - 140, 2 * r, 2 * r);

            
                g.FillEllipse(light == 1 ? Brushes.Yellow : Brushes.LightYellow, x - r, y - r, 2 * r, 2 * r);

             
                g.FillEllipse(light == 2 ? Brushes.Green : Brushes.LightGreen, x - r, y + 80, 2 * r, 2 * r);
            };

            pictureBox1.Image = bitmap;
        }
        void DrawPoligonRegulat(Graphics g, float x0, float y0, float raza, int n, Pen pen)
        {
            if (n < 3)
                return;
            PointF[] points = new PointF[n];
            float alpha = (float)(Math.PI * 2) / n;
            for (int i = 0; i < n; i++)
            {
                float x = x0 + raza * (float)Math.Cos(alpha * i);
                float y = y0 + raza * (float)Math.Sin(alpha * i);
                points[i] = new PointF(x, y);
            }
            g.DrawPolygon(pen, points);
        }
        void DrawCircleMidpoint(Graphics g, int x0, int y0, int radius, Pen pen)
        {
            int x = 0;
            int y = radius;

            int d = 1 - radius;

            while (x <= y)
            {
                
                DrawSymmetricPoints(g, x0, y0, x, y, pen);
                if (d < 0)
                {
                    
                    d = d + 2 * x + 3;
                }
                else
                {
                   
                    d = d + 2 * (x - y) + 5;
                    y--; 
                }

                x++; 
            }
        }
        void DrawSymmetricPoints(Graphics g, int x0, int y0, int x, int y, Pen pen)
        {
            g.DrawRectangle(pen, x0 + x, y0 + y, 1, 1);
            g.DrawRectangle(pen, x0 - x, y0 + y, 1, 1);
            g.DrawRectangle(pen, x0 + x, y0 - y, 1, 1);
            g.DrawRectangle(pen, x0 - x, y0 - y, 1, 1);
            g.DrawRectangle(pen, x0 + y, y0 + x, 1, 1);
            g.DrawRectangle(pen, x0 - y, y0 + x, 1, 1);
            g.DrawRectangle(pen, x0 + y, y0 - x, 1, 1);
            g.DrawRectangle(pen, x0 - y, y0 - x, 1, 1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Pen p = new Pen(Color.DarkBlue);
            Pen p1 = new Pen(Color.DarkGreen);
            int n = (int)(numericUpDown1.Value);
            int r = (int)(numericUpDown2.Value); // max raza am pus 100 ca sa o pot face mai mare de 100 sa se vada bine si raza trebuie pusa mai mare
            DrawCircleMidpoint(g, pictureBox2.Width / 2, pictureBox2.Height / 2, r, p);
            DrawPoligonRegulat(g, pictureBox2.Width / 2, pictureBox2.Height / 2, r, n, p1);
            pictureBox2.Image = bitmap1;
           
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
