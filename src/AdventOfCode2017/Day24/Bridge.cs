using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day24
{
    public class Bridge
    {
        public ICollection<(int A, int B)> Components { get; }

        public int Strength { get; }

        public Bridge(IEnumerable<(int A, int B)> components)
        {
            Components = new List<(int A, int B)>(components);
            Strength = Components.Sum(x => x.A + x.B);
        }

        public override string ToString()
        {
            return $"{Components.JoinStrings("--")} ({Strength})";
        }
    }
}