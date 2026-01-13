using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Matrix
    {

        int[,] data;
        int N { get {  return data.GetLength(0); } } 
        int M {  get { return data.GetLength(1); }}

        public Matrix(int[,] data)
        {
            this.data = data;
        }
        
        public int this[int i, int j]
        {
            get => data[i, j];
            set => data[i, j] = value;
        }


    }
}
