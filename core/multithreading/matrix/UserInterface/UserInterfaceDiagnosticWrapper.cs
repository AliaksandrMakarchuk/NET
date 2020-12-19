using System;
using System.Diagnostics;
using System.Numerics;
using matrix.Models;
using matrix.MatrixProcessor;

namespace matrix.UserInterface
{
    public class UserInterfaceDiagnosticWrapper : IUserInterface
    {
        private IUserInterface _userInterface;

        public UserInterfaceDiagnosticWrapper(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public void PrintMatrix(Matrix matrix)
        {
            _userInterface.PrintMatrix(matrix);
        }

        public void LogProcess(Matrix matrix, IMatrixProcessor processor, Func<Matrix, IMatrixProcessor, BigInteger> processMatrix)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _userInterface.LogProcess(matrix, processor, processMatrix);

            stopwatch.Stop();

            Console.WriteLine($"Time spent: {stopwatch.Elapsed}");
        }
    }
}