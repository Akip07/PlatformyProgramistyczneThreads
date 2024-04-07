using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformyProgramistyczneWatki
{
    internal class MatrixOperations
    {
        
        public static void ThreadTaskMultiplication(int start, int end, Matrix m1, Matrix m2, Matrix result)
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



        public static Tuple<Matrix, long> AsyncMultiply(Matrix m1, Matrix m2, int threadNum)
        {
            Matrix result = new Matrix(m2.w, m1.h, false);
            Thread[] threads = new Thread[threadNum];

            for (int i = 0; i < threadNum; i++)
            {
                int start = i * result.size / threadNum;
                int end = (i + 1) * result.size / threadNum;
                

                threads[i] = new Thread(() => ThreadTaskMultiplication(start, end, m1, m2, result));
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

            return Tuple.Create(result, watch.ElapsedMilliseconds);
        }



        public static Tuple<Matrix, long> ParallelMultiply(Matrix m1, Matrix m2, int threadNum)
        {
            Matrix result = new Matrix(m2.w, m1.h, false);
            ParallelOptions opt = new ParallelOptions(){MaxDegreeOfParallelism = threadNum};

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Parallel.For(0, threadNum, opt, x =>
            {
                int start = x * result.size / threadNum;
                int end = (x + 1) * result.size / threadNum;
                ThreadTaskMultiplication(start, end, m1, m2, result);
            });
            watch.Stop();
            
            return Tuple.Create(result, watch.ElapsedMilliseconds);
        }


    }
}
