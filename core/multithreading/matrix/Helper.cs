using System;
using System.IO;
using System.Numerics;
using System.Collections.Generic;
using matrix.Models;
using matrix.ValuesReader;

namespace matrix
{
    public static class Helper
    {
        /// 
        public static Matrix ReadMatrix(IValuesReader reader)
        {
            var size = reader.GetMatrixSize();
            var hasRead = 0;
            var matrixRows = new List<MatrixRow>(size.Item1);
            var elements = new List<int>(size.Item2);

            while (reader.HasNext())
            {
                var element = reader.ReadNext();
                elements.Add(element);
                ++hasRead;

                if (hasRead == size.Item2)
                {
                    matrixRows.Add(new MatrixRow { Elements = elements.ToArray() });
                    elements.Clear();
                    hasRead = 0;
                }
            }

            return new Matrix { Rows = matrixRows.ToArray() };
        }

        public static Matrix GenerateMatrix(int rows, int cols)
        {
            var alg = 1;
            switch (alg)
            {
                case 0:
                    return GenerateValues(rows, cols);
                case 1:
                    return ReadValues();
                default:
                    return null;
            }
        }

        private static Matrix GenerateValues(int rows, int cols)
        {
            Console.Clear();
            var total = BigInteger.Multiply(new BigInteger(rows), new BigInteger(cols));
            var current = new BigInteger(0);
            BigInteger progress = new BigInteger(0);
            Console.WriteLine($"GENERATION. Total: {total}");

            using (var sw = new StreamWriter($"values_{rows}_{cols}", false))
            {
                for (var i = 0; i < rows; i++)
                {
                    for (var c = 0; c < cols; c++)
                    {
                        var value = new Random((int)DateTime.Now.Ticks).Next(0, 1000 * 1000);
                        sw.Write(value);
                        sw.Write(';');


                        current = BigInteger.Add(current, 1);

                        var accuracy = 100000;
                        var currentProgress = BigInteger.Divide(BigInteger.Multiply(current, 100 * accuracy), total);
                        var result = BigInteger.Compare(progress, currentProgress);
                        if (result < 0)
                        {
                            var rem = new BigInteger();
                            BigInteger.DivRem(currentProgress, accuracy, out rem);
                            Console.SetCursorPosition(0, 1);
                            Console.WriteLine($"GENERATION. Progress: {BigInteger.Divide(currentProgress, accuracy)},{rem}");
                        }
                        progress = currentProgress;
                    }
                    sw.Write('_');
                }
                sw.Flush();
            }

            Console.WriteLine("GENERATION. All processes completed");

            return null;
        }

        private static Matrix ReadValues()
        {
            var matrixRows = new List<MatrixRow>();
            var elements = new List<int>();

            using (var sr = new StreamReader("values_50000_50000"))
            {
                var newLine = '_';
                var delimeter = ';';
                var number = new List<char>();
                while (!sr.EndOfStream)
                {
                    var charachter = (char)sr.Read();
                    if (charachter == newLine)
                    {
                        // new line
                        matrixRows.Add(new MatrixRow { Elements = elements.ToArray() });
                        elements.Clear();
                        number.Clear();
                        continue;
                    }

                    if (charachter == delimeter)
                    {
                        // number is read
                        var parsed = int.Parse(string.Join("", number));
                        elements.Add(parsed);
                        // Console.WriteLine($"Read: {parsed}");
                        number.Clear();
                        continue;
                    }

                    number.Add(charachter);
                }
            }

            // Console.WriteLine($"Total rows: {matrixRows.Count}");

            return new Matrix { Rows = matrixRows.ToArray() };
        }
    }
}