using System.Threading.Tasks;
using System.Numerics;

namespace matrix
{
    public interface IMatrixProcessor<TElement>
    where TElement : struct
    {
        BigInteger CalculateSum(Matrix<TElement> matrix);
    }
}