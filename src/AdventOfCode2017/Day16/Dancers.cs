using System.Linq;

namespace AdventOfCode2017.Day16
{
    public class Dancers
    {
        private char[] _line;
        public string Line => _line.AsString();

        public Dancers(string useThisInstead = null)
        {
            _line = useThisInstead == null
                ? Enumerable.Range('a', 16).Select(x => (char) x).ToArray()
                : useThisInstead.ToCharArray();
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
            var line = Line;
            var pos1 = line.IndexOf(one);
            var pos2 = line.IndexOf(two);

            Exchange(pos1, pos2);
        }
    }
}
