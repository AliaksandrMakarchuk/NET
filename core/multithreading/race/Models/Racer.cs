namespace Multithreading.Race.Models
{
    public class Racer
    {
        private string _mark;

        public string Mark => _mark;
        public int Velocity { get; set; }

        public Racer(string mark)
        {
            _mark = mark;
        }

        public Racer(int mark) : this(mark.ToString()) { }
    }
}