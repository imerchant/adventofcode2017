using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day07
{
    public class ProgramTower
    {
        public IDictionary<string, TowerProgram> Programs { get; }

        public TowerProgram Head { get; }

        public ProgramTower(IList<string> input)
        {
            Programs = new Dictionary<string, TowerProgram>();
            foreach (var item in input)
            {
                var program = TowerProgram.Parse(item);
                Programs[program.Id] = program;
            }

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

    public class TowerProgram
    {
        public string Id { get; }
        public int Weight { get; }
        public IList<string> ChildIds { get; }
        public IList<TowerProgram> Children { get; }
        public TowerProgram Parent { get; set; }
        public string ParentId => Parent?.Id;

        private int? _discWeight;
        public int DiscWeight => _discWeight ?? (_discWeight = GetDiscWeight()).Value;
        private TowerProgram _childOutOfBalance;
        public TowerProgram ChildOutOfBalance => _childOutOfBalance ?? (_childOutOfBalance = GetChildOutOfBalance());

        public string SubTowerWeight(int levels = 1)
        {
            var tabs = Enumerable.Range(0, levels).Select(_ => "\t").JoinStrings(string.Empty);

            return Children.HasAny()
                ? $"{DiscWeight}:({Weight}\n{tabs}+ {string.Join($"\n{tabs}+ ", Children.Select(x => $"{x.Id}:{x.SubTowerWeight(levels + 1)}"))})"
                : $"({Weight})";
        }

        public TowerProgram(string id, int weight, IList<string> children)
        {
            Id = id;
            Weight = weight;
            ChildIds = children ?? new List<string>();
            Children = new List<TowerProgram>();
        }

        private int GetDiscWeight()
        {
            if (!Children.HasAny())
                return Weight;

            return Weight + Children.Sum(x => x.DiscWeight);
        }

        private TowerProgram GetChildOutOfBalance()
        {
            if (!Children.HasAny())
                return null;

            var byWeight = Children.GroupBy(x => x.DiscWeight);

            return byWeight.FirstOrDefault(group => group.Count() == 1)?.FirstOrDefault();
        }

        public static readonly Regex NoChildren = new Regex(@"(?'id'\w*?) \((?'weight'\d*?)\)", RegexOptions.Compiled);
        public static readonly Regex WithChildren = new Regex(@"(?'id'\w*?) \((?'weight'\d*?)\) -> (?'children'.*)", RegexOptions.Compiled);
        public static TowerProgram Parse(string input)
        {
            var withChildrenMatch = WithChildren.Match(input);
            if (withChildrenMatch.Success)
            {
                var id = withChildrenMatch.Groups["id"].Value;
                var weight = int.Parse(withChildrenMatch.Groups["weight"].Value);
                var children = withChildrenMatch.Groups["children"].Value.SplitOn(',').TrimEach().ToList();
                return new TowerProgram(id, weight, children);
            }

            var noChildrenMatch = NoChildren.Match(input);
            if (noChildrenMatch.Success)
            {
                var id = noChildrenMatch.Groups["id"].Value;
                var weight = int.Parse(noChildrenMatch.Groups["weight"].Value);
                return new TowerProgram(id, weight, new List<string>());
            }

            throw new Exception("bad input");
        }
    }
}
