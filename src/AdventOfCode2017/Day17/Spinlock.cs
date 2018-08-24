using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day17
{
    public class Spinlock
    {
        public int Jumps { get; }
        public int Round { get; private set; }
        public int Index { get; private set; }
        public List<int> Buffer { get; }

        public Spinlock(int jumps)
        {
            Jumps = jumps;
            Round = 0;
            Index = 0;
            Buffer = new List<int>(2020)
            {
                0
            };
        }

        public int ValueAfterInsertions(int insertions)
        {
            for (var k = 0; k < insertions; ++k)
            {
                Step();
            }

            return Buffer[(Index + 1) % Buffer.Count];
        }

        internal void Step()
        {
            Index = (Index + Jumps) % Buffer.Count;
            Buffer.Insert(Index + 1, ++Round);
            Index = (Index + 1) % Buffer.Count;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var k = 0; k < Buffer.Count; ++k)
            {
                if (k == Index)
                    builder.Append('(').Append(Buffer[k]).Append(')');
                else
                    builder.Append(Buffer[k]);
                builder.Append(' ');
            }
            return builder.ToString().Trim();
        }
    }
}
