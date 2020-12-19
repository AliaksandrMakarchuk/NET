using System;

namespace matrix.ValuesReader
{
    public interface IValuesReader : IDisposable
    {
        (int, int) GetMatrixSize();
        int ReadNext();
        bool HasNext();
    }
}