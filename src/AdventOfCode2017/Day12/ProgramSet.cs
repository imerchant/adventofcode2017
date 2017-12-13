using System.Collections.Generic;
using System.Linq;

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

        public HashSet<int> ProgramsThatCanReach(int target)
        {
            var adjacent = new HashSet<int>();
            adjacent.Add(target);

            var lastCount = 1;
            var thisCount = 1;
            do
            {
                lastCount = thisCount;
                var programsNotPresent = Programs.Keys.Except(adjacent);
                foreach (var programId in programsNotPresent)
                {
                    if(adjacent.Overlaps(Programs[programId].Peers))
                        adjacent.Add(programId);
                }
                thisCount = adjacent.Count;
            } while(lastCount != thisCount);

            return adjacent;
        }

        // counting groups
        // get programs that can reach 0
        // get first of remaining set, see which can reach that (even if just self)
        // repeat until no programs remain
        // count number of sets
    }
}