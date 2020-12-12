using System.Numerics;
using System;
using System.IO;
using matrix.UserInterface;

namespace matrix.MatrixGenerator
{
    public class FileMatrixGenerator : IMatrixGenerator
    {
        private readonly string _fileNamePattern;

        public FileMatrixGenerator(string fileNamePattern)
        {
            this._fileNamePattern = fileNamePattern;
        }

        public void Generate(int rows, int columns, IUserInterface userInterface)
        {
            // var total = BigInteger.Multiply(new BigInteger(rows), new BigInteger(columns));
            // var current = new BigInteger(0);
            // BigInteger progress = new BigInteger(0);
            // Console.WriteLine($"GENERATION. Total: {total}");

            using (var sw = new StreamWriter($"{_fileNamePattern}_{rows}_{columns}", false))
            {
                for (var i = 0; i < rows; i++)
                {
                    for (var c = 0; c < columns; c++)
                    {
                        var value = new Random((int)DateTime.Now.Ticks).Next(0, 1000 * 1000);
                        sw.Write(value);
                        sw.Write(';');


                        // current = BigInteger.Add(current, 1);

                        // var accuracy = 100000;
                        // var currentProgress = BigInteger.Divide(BigInteger.Multiply(current, 100 * accuracy), total);
                        // var result = BigInteger.Compare(progress, currentProgress);
                        // if (result < 0)
                        // {
                        //     var rem = new BigInteger();
                        //     BigInteger.DivRem(currentProgress, accuracy, out rem);
                        //     Console.SetCursorPosition(0, 1);
                        //     Console.WriteLine($"GENERATION. Progress: {BigInteger.Divide(currentProgress, accuracy)},{rem}");
                        // }
                        // progress = currentProgress;
                    }
                    sw.Write('_');
                }
                sw.Flush();
            }
        }
    }
}