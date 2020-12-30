using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace console_another_in_out
{
    public class InputBuffer : TextReader
    {
        public IList<char> _buffer;

        public InputBuffer()
        {
            _buffer = new List<char>();
        }

        public override int Peek()
        {
            var chr = base.Peek();
            _buffer.Add((char) chr);
            using(var sw = File.AppendText("./input.txt"))
            {
                sw.WriteLine($"{(char)chr} - {chr}");
                sw.Flush();
            }
            return chr;
        }

        // public override string ReadLine()
        // {
        //     _buffer.Add(base.ReadLine());
        //     _buffer.Add(Environment.NewLine);
        //     return "Done";
        // }

        public override void Close()
        {
            _buffer.Clear();
            _buffer = null;
            base.Close();
        }

        public IList<char> GetBuffer()
        {
            return _buffer;
        }
    }
}