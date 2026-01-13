using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {

        Matrix m;
        public Form1()
        {
            InitializeComponent();

            m = new Matrix(new int[,]
            {
                {1, 2, 3}
                ,{4, 5, 6}
                ,{7, 8, 9}
            });
            for (int i = 0; i < 3; i++)
            {
                string buf = "";
                for (int j = 0; j < 3; j++)
                {
                    buf += " " + m[i, j].ToString();
                }
                listBox1.Items.Add(buf);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



    }
}
