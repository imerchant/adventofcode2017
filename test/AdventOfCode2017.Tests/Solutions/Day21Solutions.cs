using System.Collections.Generic;
using AdventOfCode2017.Day21;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day21Solutions : TestBase
    {
        public Day21Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact(Skip = "Adds a couple seconds to the test runs")]
        public void Puzzle2_CountLitPixels_After18Iterations()
        {
            var replacements = Input.Day21Parse(Input.Day21);
            var grid = new Grid(replacements);

            for (var k = 0; k < 18; ++k)
            {
                grid.Iterate();
                // Output.WriteLine($"After iteration {k+1}:");
                // Output.WriteLine($"{grid.Content.JoinStrings("\n")}");
                // Output.WriteLine(string.Empty);
            }

            grid.LitPixels.Should().Be(1277716);
        }

        [Fact]
        public void Puzzle1_CountLitPixels_After5Iterations()
        {
            var replacements = Input.Day21Parse(Input.Day21);
            var grid = new Grid(replacements);

            for (var k = 0; k < 5; ++k)
            {
                grid.Iterate();
                // Output.WriteLine($"After iteration {k+1}:");
                // Output.WriteLine($"{grid.Content.JoinStrings("\n")}");
                // Output.WriteLine(string.Empty);
            }

            grid.LitPixels.Should().Be(110);
        }

        public const string Puzzle1Example =
@"../.# => ##./#../...
.#./..#/### => #..#/..../..../#..#";

        [Fact]
        public void Grid_IterateOnce_ExpandsContentCorrectly()
        {
            var replacements = Input.Day21Parse(Puzzle1Example);
            var grid = new Grid(replacements);

            grid.Iterate();

            grid.Content.JoinStrings("/").Should().Be("#..#/..../..../#..#");
        }

        [Fact]
        public void Grid_IterateTwice_ExpandsContentCorrectly()
        {
            var replacements = Input.Day21Parse(Puzzle1Example);
            var grid = new Grid(replacements);

            grid.Iterate();
            grid.Iterate();

            grid.Content.JoinStrings("/").Should().Be("##.##./#..#../....../##.##./#..#../......");
            grid.LitPixels.Should().Be(12);
        }

        [Fact]
        public void Grid_SplitAndReplace_SplitsAndReplaces4x4Correctly()
        {
            var content = new List<string>
            {
                "#..#",
                "....",
                "....",
                "#..#"
            };
            var replacements = Input.Day21Parse(Puzzle1Example);
            var grid = new Grid(replacements);

            var output = grid.SplitAndReplace(2, content);

            output.JoinStrings("/").Should().Be("##.##./#..#../....../##.##./#..#../......");
        }

        [Fact]
        public void Grid_Splits2x2_Correctly()
        {
            var content = new List<string>
            {
                "#..#",
                "....",
                "....",
                "#..#"
            };

            const int cardinality = 2;
            var splitContent = new List<string>[2][];
            for (var k = 0; k < splitContent.Length; ++k)
                splitContent[k] = new List<string>[2];

            for (var splitRow = 0; splitRow < 2; ++splitRow)
            {
                for (var splitCol = 0; splitCol < 2; ++splitCol)
                {
                    var output = new List<string>();
                    for (var row = splitRow * cardinality; row < splitRow * cardinality + cardinality; ++row)
                    {
                        var rowOutput = "";
                        for (var col = splitCol * cardinality; col < splitCol * cardinality + cardinality; ++col)
                        {
                            rowOutput += content[row][col];
                        }
                        output.Add(rowOutput);
                    }
                    splitContent[splitRow][splitCol] = output;
                }
            }

            splitContent[0][0].JoinStrings("/").Should().Be("#./..");
        }

        [Fact]
        public void Grid_Collapse2x2_OperatesCorrectly()
        {
            var input = new List<string>[][]
            {
                new List<string>[] { new List<string> { "#.", ".." }, new List<string>{ ".#", ".." } },
                new List<string>[] { new List<string> { "..", "#." }, new List<string>{ "..", ".#" } }
            };

            var collapsed = Grid.Collapse(input);

            collapsed.JoinStrings("/").Should().Be("#..#/..../..../#..#");
        }

        [Fact]
        public void Grid_Collapse3x3_OperatesCorrectly()
        {
            var input = new List<string>[][]
            {
                new List<string>[] { new List<string> { "##.", "#..", "..." }, new List<string>{ "##.", "#..", "..." } },
                new List<string>[] { new List<string> { "##.", "#..", "..." }, new List<string>{ "##.", "#..", "..." } }
            };

            var collapsed = Grid.Collapse(input);

            collapsed.JoinStrings("/").Should().Be("##.##./#..#../....../##.##./#..#../......");
        }

        [Fact]
        public void Grid_Rotate_RotatesCorrectly()
        {
            var input = new List<string>()
            {
                "123",
                "456",
                "789"
            };

            var output = Grid.Rotate(input);

            output.JoinStrings(".").Should().Be("741.852.963");
        }

        [Fact]
        public void Grid_FlipHorizontal_FlipsCorrect()
        {
            var input = new List<string>()
            {
                "123",
                "456",
                "789"
            };

            var output = Grid.FlipHorizontal(input);

            output.JoinStrings(".").Should().Be("321.654.987");
        }

        [Fact]
        public void Grid_FlipVertical_FlipsCorrect()
        {
            var input = new List<string>()
            {
                "123",
                "456",
                "789"
            };

            var output = Grid.FlipVertical(input);

            output.JoinStrings(".").Should().Be("789.456.123");
        }
    }
}