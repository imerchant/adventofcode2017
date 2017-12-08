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

        public TowerProgram(string id, int weight, IList<string> children)
        {
            Id = id;
            Weight = weight;
            ChildIds = children ?? new List<string>();
            Children = new List<TowerProgram>();
        }

        public static Regex NoChildren = new Regex(@"(?'id'\w*?) \((?'weight'\d*?)\)", RegexOptions.Compiled);
        public static Regex WithChildren = new Regex(@"(?'id'\w*?) \((?'weight'\d*?)\) -> (?'children'.*)", RegexOptions.Compiled);
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
