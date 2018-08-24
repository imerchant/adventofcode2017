using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day16
{
    public class Dancers
    {
        private char[] _line;
        public string Line => _line.AsString();

        public Dancers(string useThisInstead = null)
        {
            _line = useThisInstead?.ToCharArray() ?? Enumerable.Range('a', 16).Select(x => (char) x).ToArray();
        }

        public void Dance(IEnumerable<Operation> steps)
        {
            foreach (var step in steps)
            {
                step.Op(this);
            }
        }

        public void Spin(int count)
        {
            var other = new char[_line.Length];
            var start = _line.Length - count;

            for (var k = start; k < start + _line.Length; ++k)
            {
                other[k - start] = _line[k % _line.Length];
            }

            _line = other;
        }

        public void Exchange(int position1, int position2)
        {
            _line[position1] ^= _line[position2];
            _line[position2] ^= _line[position1];
            _line[position1] ^= _line[position2];
        }

        public void Partner(char one, char two)
        {
            var pos1 = Line.IndexOf(one);
            var pos2 = Line.IndexOf(two);

            Exchange(pos1, pos2);
        }
    }
}
