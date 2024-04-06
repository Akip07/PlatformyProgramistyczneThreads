using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformyProgramistyczneWatki
{
    internal class MatrixOperations
    {
        public static readonly object locker = new object();
        public static void ThreadMultiply(int start, int end, Matrix m1, Matrix m2, Matrix result)
        {
            for (int i = start; i < end; i++)
            {
                int row = (int)Math.Floor((double)i / (double)result.w);
                int col = i % result.w;
                int? temp = 0;

                for (int j = 0; j < m1.w; j++)
                {
                    temp += m1.m[j + row*m1.w] * m2.m[j * m2.w + col];
                }

                result.m[i] = temp;
            }
        }
        public static Matrix Multiply(Matrix m1, Matrix m2, int threadNum)
        {
            Matrix result = new Matrix(m2.w, m1.h, false);
            Thread[] threads = new Thread[threadNum];

            for (int i = 0; i < threadNum; i++)
            {
                int start = i * result.size / threadNum;
                int end = (i + 1) * result.size / threadNum;
                

                threads[i] = new Thread(() => ThreadMultiply(start, end, m1, m2, result));
                threads[i].Name = String.Format("Thread: {0}", i);
                
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds} ms.");


            //    for (int j = 0; j < result.w; j++)
            //    {
            //        int? elem = 0;
            //        for (int k = 0; k < m1.w; k++)
            //        {
            //            elem += m1.m[j*m1.h+k] * m2.m[m2.h*k+i];
            //        }
            //        result.m[j*result.h+i] = elem;
            //    }
            //}
            return result;
        }


    }
}
