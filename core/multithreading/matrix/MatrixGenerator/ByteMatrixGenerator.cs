using System;
using System.IO;
using System.Text;
using matrix.UserInterface;

namespace matrix.MatrixGenerator
{
    public class ByteMatrixGenerator : IMatrixGenerator
    {
        private readonly string _fileNamePattern;

        public ByteMatrixGenerator(string fileNamePattern)
        {
            this._fileNamePattern = fileNamePattern;
        }

        public void Generate(int rows, int columns, IUserInterface userInterface)
        {
            var bufferSize = 1024;
            var fileStream = new FileStream($"byte_{_fileNamePattern}_{rows}_{columns}", FileMode.CreateNew);
            var bufferedStream = new BufferedStream(fileStream, bufferSize);

            using (var bw = new BinaryWriter(bufferedStream, Encoding.ASCII))
            {
                for (var i = 0; i < rows; i++)
                {
                    for (var c = 0; c < columns; c++)
                    {
                        var value = new Random((int)DateTime.Now.Ticks).Next(0, 1000 * 1000);
                        Console.WriteLine($"Generated: {value}");
                        bw.Write(value);
                    }
                }
                bw.Flush();
            }
        }
    }
}