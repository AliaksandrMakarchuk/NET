using System;
using System.Numerics;
using matrix.Models;
using matrix.MatrixProcessor;

namespace matrix.UserInterface
{
    public interface IUserInterface
    {
        void PrintMatrix(Matrix matrix);
        void LogProcess(Matrix matrix, IMatrixProcessor processor, Func<Matrix, IMatrixProcessor, BigInteger> processMatrix);
    }
}