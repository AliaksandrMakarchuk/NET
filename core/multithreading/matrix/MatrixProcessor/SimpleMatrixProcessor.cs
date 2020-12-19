using System.Numerics;
using matrix.Models;

namespace matrix.MatrixProcessor
{
    public class SimpleMatrixProcessor : IMatrixProcessor
    {
        public string Name => "Simple";
        
        public BigInteger CalculateSum(Matrix matrix)
        {
            BigInteger sum = new BigInteger(0);

            foreach (var row in matrix.Rows)
            {
                foreach (var element in row.Elements)
                {
                    sum = BigInteger.Add(sum, element);
                }
            }

            return sum;
        }
    }
}