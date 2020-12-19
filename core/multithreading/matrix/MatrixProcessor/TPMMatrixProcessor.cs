using System.Threading.Tasks;
using System;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using matrix.Models;

namespace matrix.MatrixProcessor
{
    public class TPMMatrixProcessor : IMatrixProcessor
    {
        private static object _lock = new object();
        public string Name => "Task-based Programming Model";
        public BigInteger CalculateSum(Matrix matrix)
        {
            var taskCount = Environment.ProcessorCount;
            var tasks = new Task<BigInteger>[taskCount];
            var rowsPerTask = (int)Math.Floor((double)matrix.Rows.Length / taskCount);

            for (var i = 0; i < taskCount - 1; i++)
            {
                var rows = matrix.Rows.Skip(i * rowsPerTask).Take(rowsPerTask);
                var number = i;
                tasks[i] = new Task<BigInteger>(() => Calculate(rows, number));
            }

            var skipped = (taskCount - 1) * rowsPerTask;
            var lastElements = matrix.Rows.Skip(skipped).Take(matrix.Rows.Length - skipped);
            tasks[taskCount - 1] = new Task<BigInteger>(() => Calculate(lastElements, taskCount - 1));

            foreach (var task in tasks)
            {
                task.Start();
            }

            var continuation = Task.WhenAll<BigInteger>(tasks);

            continuation.Wait();

            BigInteger sum = new BigInteger(0);

            foreach (var result in continuation.Result)
            {
                sum = BigInteger.Add(sum, result);
            }

            return sum;
        }

        private BigInteger Calculate(IEnumerable<MatrixRow> rows, int processorNumber)
        {
            BigInteger sum = new BigInteger(0);

            foreach (var row in rows)
            {
                for (var i = 0; i < row.Elements.Length; i++)
                {
                    sum = BigInteger.Add(sum, new BigInteger(row.Elements[i]));

                }
            }

            return sum;
        }
    }
}