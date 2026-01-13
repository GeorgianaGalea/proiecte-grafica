using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7
{
    public partial class Form1 : Form
    {
        private Random randomGenerator = new Random();
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(500, 500);
            this.BackColor = Color.SkyBlue;
            this.DoubleBuffered = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;

            DrawCloudRecursive(graphics, 250, 250, 100, 5);
        }

        private void DrawCloudRecursive(
            Graphics graphics,
            int centerX,
            int centerY,
            int circleRadius,
            int recursionDepth)
        {
            if (recursionDepth == 0)
                return;

            using (Brush cloudBrush = new SolidBrush(Color.White))
            {
                graphics.FillEllipse(
                    cloudBrush,
                    centerX - circleRadius,
                    centerY - circleRadius,
                    circleRadius * 2,
                    circleRadius * 2);
            }

            int branchCount = randomGenerator.Next(3, 6);

            for (int i = 0; i < branchCount; i++)
            {
                double randomAngle = randomGenerator.NextDouble() * 2 * Math.PI;

                int childRadius = (int)(circleRadius * 0.6);

                int childCenterX =
                    centerX + (int)(Math.Cos(randomAngle) * circleRadius * 0.8);

                int childCenterY =
                    centerY + (int)(Math.Sin(randomAngle) * circleRadius * 0.8);

                DrawCloudRecursive(
                    graphics,
                    childCenterX,
                    childCenterY,
                    childRadius,
                    recursionDepth - 1);
            }
        }

    }
}
