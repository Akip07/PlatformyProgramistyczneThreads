using System.Diagnostics.SymbolStore;
using System.Numerics;
using static PlatformyProgramistyczneWatki.MatrixOperations;

namespace PlatformyProgramistyczneWatki
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List <List<long>> results = new List<List<long>>();
            for(int i =0; i < 3; i++)
            {
                results.Add(new List<long>());
            }


            for (int i = 0; i < 15; i++)
            {
                Matrix m1 = new Matrix(200, 200);
                Matrix m2 = new Matrix(200, 200);

                results[0].Add(AsyncMultiply(m1, m2, 1).Item2);
                results[1].Add(AsyncMultiply(m1, m2, 10).Item2);
                results[2].Add(ParallelMultiply(m1, m2, 10).Item2);
            }
            List<double> avg = new List<double>();
            foreach (List<long> result in results)
            {
                avg.Add(result.Average());
            }
            Console.WriteLine($"Average time for synchronous matrix multiplication: {avg[0]:N2}");
            Console.WriteLine($"Average time for asynchronous matrix multiplication: {avg[1]:N2}");
            Console.WriteLine($"Average time for Parallel matrix multiplication: {avg[2]:N2}");


        }
    }
}
