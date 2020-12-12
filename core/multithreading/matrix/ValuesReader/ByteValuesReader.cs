using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Text.RegularExpressions;

namespace matrix.ValuesReader
{
    public class ByteValuesReader : IValuesReader
    {
        private readonly string _path;
        private readonly Stream _stream;
        private readonly BufferedStream _bufferedStream;
        private readonly BinaryReader _reader;
        private readonly int _rows;
        private readonly int _columns;
        private readonly BigInteger _totalElements;
        private BigInteger _hasBeenRead;

        public ByteValuesReader(string path)
        {
            this._path = path;
            _stream = new FileStream(path, FileMode.Open);
            _bufferedStream = new BufferedStream(_stream, 1024);
            _reader = new BinaryReader(_bufferedStream, Encoding.ASCII);

            var match = new Regex("(?>_([0-9]+))", RegexOptions.None).Match(path);
            if (match.Success)
            {

                _rows = int.Parse(match.Groups[1].Value);
                match = match.NextMatch();
                _columns = int.Parse(match.Groups[1].Value);
            }
            _totalElements = BigInteger.Multiply(_rows, _columns);
        }

        public (int, int) GetMatrixSize()
        {
            return (_rows, _columns);
        }

        public int ReadNext()
        {
            var bytes = _reader.ReadBytes(4);
            ++_hasBeenRead;
            return BitConverter.ToInt32(bytes);
        }

        public bool HasNext()
        {
            return BigInteger.Compare(_hasBeenRead, _totalElements) < 0;
        }

        public void Dispose()
        {
            _reader.Dispose();
            _bufferedStream.Dispose();
            _stream.Dispose();
        }
    }
}