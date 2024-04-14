using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformyProgramistyczneWatki
{
    internal class Matrix
    {
        public int h {  get; set; }
        public int w { get; set; }
        public int size { get; set; }
        public List<int?> m = new List<int?>();
        public Matrix(int h, int w, bool random=true)
        {
            this.h = h;
            this.w = w;
            this.size = h * w;
            if (random)
                Generate();
            else
                FillZeros();
        }
        void FillZeros()
        {
            for (int i = 0; i < size; i++)
                m.Add(0);
        }
        void Generate()
        {
            Random rnd = new Random();
            for (int i = 0; i <size; i++)
                m.Add(rnd.Next(0,10)); 
        }
        override public string ToString()
        {
            string result = string.Empty;
            for (int i = 0;i < h;i++)
            {
                for (int j = 0; j < w; j++)
                {
                    result += "[" + m[i*h+j].ToString() + "] ";
                }
                result += "\n";
            }
            return result;
        }
}
