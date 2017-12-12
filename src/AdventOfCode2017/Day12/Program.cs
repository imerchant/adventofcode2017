using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

    public class Program
    {
        public static readonly Regex ProgramRegex = new Regex(@"(?'id'\d+) <-> (?'peers'.+)", RegexOptions.Compiled);

        public int Id { get; }
        public IList<int> Peers { get; }

        public Program(int id, IList<int> peers)
        {
            Id = id;
            Peers = peers;
        }

        public static Program Parse(string input)
        {
            var match = ProgramRegex.Match(input);
            if(!match.Success)
                throw new Exception($"could not parse \"{input}\"");

            var id = int.Parse(match.Groups["id"].Value);
            var peers = match.Groups["peers"].Value.SplitOn(',', ' ').Select(int.Parse).ToList();

            return new Program(id, peers);
        }
    }
}