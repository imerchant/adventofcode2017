using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day07
{
    public class ProgramTower
    {
        public IDictionary<string, TowerProgram> Programs { get; }

        public TowerProgram Head { get; }

        public ProgramTower(IList<string> input)
        {
            Programs = input.Select(TowerProgram.Parse).ToDictionary(x => x.Id);

            foreach (var program in Programs.Values)
            {
                foreach (var childId in program.ChildIds)
                {
                    var child = Programs[childId];
                    program.Children.Add(child);
                    child.Parent = program;
                }
            }

            Head = FindLeastWeightedProgram();
        }

        internal TowerProgram FindLeastWeightedProgram()
        {
            return Programs.Values.Single(x => x.Parent == null);
        }
    }
}
