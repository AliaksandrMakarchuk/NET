using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace console_another_in_out
{
    public class OutputBuffer : TextWriter
    {
        private IList<string> _buffer;

        public override Encoding Encoding { get; }

        public OutputBuffer()
        {
            _buffer = new List<string>();
        }

        public override void WriteLine()
        {
            _buffer.Add(Environment.NewLine);
        }

        public override void Write(object value)
        {
            _buffer.Add(value.ToString());
        }

        public override void WriteLine(object value)
        {
            _buffer.Add(value.ToString());
            _buffer.Add(Environment.NewLine);
        }

        public override void Write(string value)
        {
            _buffer.Add(value);
        }

        public override void WriteLine(string value)
        {
            _buffer.Add(value);
            _buffer.Add(Environment.NewLine);
        }

        public IList<string> GetBuffer()
        {
            return _buffer;
        }
    }
}