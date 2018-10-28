using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2017.Day21
{
    public class Grid
    {
        public static List<string> Starting => new List<string>
        {
            ".#.",
            "..#",
            "###"
        };

        public static List<Func<IEnumerable<string>, List<string>>> Transforms = new List<Func<IEnumerable<string>, List<string>>>
        {
            input => input.ToList(),
            input => Rotate(input),
            input => FlipHorizontal(FlipVertical(input)),
            input => Rotate(FlipHorizontal(FlipVertical(input))),
            input => FlipHorizontal(input),
            input => Rotate(FlipHorizontal(input)),
            input => FlipVertical(input),
            input => Rotate(FlipVertical(input)),
        };

        public List<string> Content { get; private set; }
        public IDictionary<string, List<string>> Replacements { get; }
        public int LitPixels => Content.Sum(x => x.Count(c => c == '#'));

        public Grid(IList<string> input)
        {
            Content = Starting;
            Replacements = ParseReplacements(input);
        }

        private static IDictionary<string, List<string>> ParseReplacements(IList<string> input)
        {
            return input
                .Select(line => line.SplitOn("=>").TrimEach().ToList())
                .ToDictionary(
                    split => split[0],
                    split => split[1].SplitOn('/').ToList()
                );
        }

        internal static List<string> FlipHorizontal(IEnumerable<string> input)
        {
            return input.Select(x => x.Reverse().AsString()).ToList();
        }

        internal static List<string> FlipVertical(IEnumerable<string> input)
        {
            return input.Reverse().ToList();
        }

        internal static List<string> Rotate(IEnumerable<string> input)
        {
            var inputList = input.ToList();
            var output = new List<string>();
            for (var col = 0; col < inputList.Count; ++col)
            {
                var row = new StringBuilder();
                for (var k = inputList.Count -1; k >= 0; --k)
                {
                    row.Append(inputList[k][col]);
                }
                output.Add(row.ToString());
            }
            return output;
        }

        private static string Flatten(IEnumerable<string> input)
        {
            return input.JoinStrings("/");
        }

        private List<string> FindReplacement(IEnumerable<string> input)
        {
            foreach (var transform in Transforms)
            {
                var result = transform(input);
                if (Replacements.TryGetValue(Flatten(result), out var replacement))
                {
                    return replacement;
                }
            }
            throw new Exception($"Could not locate replacement for {input.JoinStrings("/")}");
        }

        internal static List<string> Collapse(List<string>[][] input)
        {
            var output = new List<string>();
            foreach (var splitRow in input)
            {
                var rowOutput = Enumerable.Range(1, splitRow[0].Count).Select(_ => new StringBuilder()).ToList();
                foreach (var block in splitRow)
                {
                    for (var row = 0; row < block.Count; ++row)
                    {
                        rowOutput[row].Append(block[row]);
                    }
                }
                output.AddRange(rowOutput.Select(x => x.ToString()));
            }
            return output;
        }

        internal List<string> SplitAndReplace(int cardinality, List<string> input)
        {
            var splitCount = input.Count / cardinality;
            var splitContent = new List<string>[splitCount][];
            for (var k = 0; k < splitContent.Length; ++k)
                splitContent[k] = new List<string>[splitCount];

            for (var splitRow = 0; splitRow < splitCount; ++splitRow)
            {
                for (var splitCol = 0; splitCol < splitCount; ++splitCol)
                {
                    var output = new List<string>();
                    for (var row = splitRow * cardinality; row < splitRow * cardinality + cardinality; ++row)
                    {
                        var rowOutput = new StringBuilder();
                        for (var col = splitCol * cardinality; col < splitCol * cardinality + cardinality; ++col)
                        {
                            rowOutput.Append(input[row][col]);
                        }
                        output.Add(rowOutput.ToString());
                    }
                    splitContent[splitRow][splitCol] = FindReplacement(output);
                }
            }

            return Collapse(splitContent);
        }

        public void Iterate()
        {
            if (Content.Count == 3)
            {
                Content = FindReplacement(Content);
            }
            else if (Content.Count % 2 == 0)
            {
                Content = SplitAndReplace(2, Content);
            }
            else if (Content.Count % 3 == 0)
            {
                Content = SplitAndReplace(3, Content);
            }
        }
    }
}