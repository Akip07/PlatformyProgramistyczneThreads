using System.Numerics;
using static PlatformyProgramistyczneWatki.MatrixOperations;

namespace PlatformyProgramistyczneWatki
{
    internal class Program
    {

        static void Main(string[] args)
        {

            Matrix m1 = new Matrix(1000,1000);
            Matrix m2 = new Matrix(1000, 1000);
            //Console.WriteLine(m1.ToString());
           // Console.WriteLine(m2.ToString());

            Matrix m3 = Multiply(m1, m2,1);
            //Console.WriteLine(m3.ToString());


        }
    }
}
