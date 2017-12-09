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

        public (int stepsToCycle, int stepsInCycle) LocateFirstDuplicateOrganization()
        {
            var organization = string.Empty;
            do
            {
                Reorganize();
                organization = ToString();
            } while (SeenOrganizations.Add(organization));

            var index = SeenOrganizations.ToList().IndexOf(organization);

            return (SeenOrganizations.Count + 1, SeenOrganizations.Count - index);
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
