using System.IO;
using System;
using System.Numerics;
using matrix.Models;
using matrix.MatrixProcessor;

namespace matrix.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        public void PrintMatrix(Matrix matrix)
        {
            Console.WriteLine($"Matrix [{matrix.Rows.Length}x{matrix.Rows[0].Elements.Length}]");
            foreach (var row in matrix.Rows)
            {
                Console.WriteLine(string.Join(' ', row.Elements));
            }
        }
        public void LogProcess(Matrix matrix, IMatrixProcessor processor, Func<Matrix, IMatrixProcessor, BigInteger> processMatrix)
        {
            Console.WriteLine($"Start Calculattions - {processor.Name}");

            var result = processMatrix(matrix, processor);

            Console.WriteLine($"Result: {result}");
        }
    }
}