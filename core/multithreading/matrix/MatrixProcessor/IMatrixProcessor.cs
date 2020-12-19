using System.Threading.Tasks;
using System.Numerics;
using matrix.Models;

namespace matrix.MatrixProcessor
{
    public interface IMatrixProcessor
    {
        string Name { get; }
        BigInteger CalculateSum(Matrix matrix);
    }
}