namespace matrix.Models
{
    public class Matrix
    {
        public MatrixRow[] Rows { get; set; }
    }

    public class MatrixRow
    {
        public int[] Elements { get; set; }
    }
}