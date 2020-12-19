using System;
using matrix;
using System.Numerics;
using System.Diagnostics;
using matrix.MatrixGenerator;
using matrix.ValuesReader;
using matrix.Models;
using matrix.UserInterface;
using matrix.MatrixProcessor;

namespace matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInterface = new UserInterfaceDiagnosticWrapper(new ConsoleUserInterface());
            new ByteMatrixGenerator("values").Generate(25000, 25000, userInterface);
            Matrix matrix = null;

            using (var reader = new ByteValuesReader("byte_values_25000_25000"))
            {
                matrix = Helper.ReadMatrix(reader);
                // var matrixSize = reader.GetMatrixSize();

                // Console.WriteLine($"Size: {matrixSize.Item1}x{matrixSize.Item2}");

                // while (reader.HasNext())
                // {
                //     Console.WriteLine($"Read: {reader.ReadNext()}");
                // }

                Console.WriteLine($"Reading complete");
            }

            // userInterface.PrintMatrix(matrix);

            GC.Collect();
            userInterface.LogProcess(matrix, new SimpleMatrixProcessor(), (m, p) => p.CalculateSum(m));

            Console.WriteLine("------------------------------------");

            GC.Collect();
            userInterface.LogProcess(matrix, new TPMMatrixProcessor(), (m, p) => p.CalculateSum(m));
        }
    }
}
