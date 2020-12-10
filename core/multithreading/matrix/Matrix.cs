namespace matrix
{
    public class Matrix<TElement>
    where TElement : struct
    {
        public MatrixRow<TElement>[] Rows { get; set; }
    }

    public class MatrixRow<TElement>
    where TElement : struct
    {
        public TElement[] Elements { get; set; }
    }
}