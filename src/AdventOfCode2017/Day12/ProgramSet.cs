using System.Collections.Generic;

namespace AdventOfCode2017.Day12
{
    public class ProgramSet
    {
        public IDictionary<int, Program> Programs { get; }

        public ProgramSet(IList<string> input)
        {
            Programs = new Dictionary<int, Program>();

            foreach (var line in input)
            {
                var program = Program.Parse(line);
                Programs[program.Id] = program;
            }
        }

        // todo
        // add everything that can reach zero to a set
        // repeatedly check ones not in that set for peers in the set, add if found
        // when one? pass goes with no additions to set, we're done
    }
}