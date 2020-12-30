using System;
using System.Collections.Generic;
using System.IO;

namespace console_another_in_out
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = Console.In;
            Console.Write("Input >");
            IList<int> _buffer = new List<int>();

            // var inputBuffer = new InputBuffer();

            // Console.SetIn(inputBuffer);

            while (_buffer.Count < 8)
            {
                var key = Console.ReadKey(true);
                _buffer.Add((int)key.KeyChar);
            }

            Console.WriteLine($"Buffer: {string.Join(", ", _buffer)}");
        }
    }
}