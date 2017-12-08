using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day06
{
    public class MemoryBanks
    {
        public IList<int> Banks { get; }
        public HashSet<string> SeenOrganizations { get; }

        public MemoryBanks(IList<int> banks)
        {
            Banks = banks;
            SeenOrganizations = new HashSet<string>();
        }

        public int LocateFirstDuplicateOrganization()
        {
            var steps = 0;
            do
            {
                Reorganize();
                ++steps;
            } while (SeenOrganizations.Add(ToString()));

            return steps;
        }

        public override string ToString()
        {
            return string.Join(" ", Banks);
        }

        internal void Reorganize()
        {
            var (index, max) = Banks.MaxOf();
            Banks[index] = 0;
            while (max > 0)
            {
                index++;
                if (index >= Banks.Count)
                    index = 0;
                Banks[index]++;
                max--;
            }
        }
    }
}
