using System.Threading.Tasks;
using System;
using System.IO;
using System.Numerics;
using System.Linq;

namespace matrix
{
    public class MatrixProcessor<TElement> : IMatrixProcessor<TElement>
    where TElement : struct
    {
        public BigInteger CalculateSum(Matrix<TElement> matrix)
        {
            var taskCount = Environment.ProcessorCount > matrix.Rows.Length ? matrix.Rows.Length : Environment.ProcessorCount;
            var tasks = new Task<BigInteger>[taskCount];

            for (var i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run<BigInteger>(() => Calculate(matrix.Rows[i]));
            }

            var continuation = Task.WhenAll<BigInteger>(tasks);

            continuation.Wait();

            Console.WriteLine($"Concurrent calculations completed");

            BigInteger sum = new BigInteger(0);

            foreach (var result in continuation.Result)
            {
                Console.WriteLine($"Intermediate result: {result}");
                sum = BigInteger.Add(sum, result);
            }

            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Result: {sum}");

            return sum;
        }

        private BigInteger Calculate(MatrixRow<TElement> row)
        {
            BigInteger sum = new BigInteger(0);

            for (var i = 0; i < row.Elements.Length; i++)
            {
                sum = BigInteger.Add(sum, new BigInteger(row.Elements[i]));
            }

            return sum;
        }
    }
}