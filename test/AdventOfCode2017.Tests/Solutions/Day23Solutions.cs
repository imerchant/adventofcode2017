using AdventOfCode2017.Day23;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day23Solutions : TestBase
    {
        public Day23Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_CountMulCommands()
        {
            var program = new Program(Input.Day23Parse(Input.Day23));

            program.Run();

            program.MulInvoked.Should().Be(3969);
        }

        [Fact]
        public void Puzzle2_CountPrimes_Between106500_And123501()
        {
            // the program is counting non-prime numbers between the input values in +17 increments, very inefficiently
            // here's a still inefficient implementation of the same algo
            // would not have solved this without the subreddit

            var count = 0;
            for (var k = 106500; k < 123500 + 1; k += 17)
            {
                for (var x = 2; x < k; ++x)
                {
                    if (k % x == 0)
                    {
                        count++;
                        break;
                    }
                }
            }

            count.Should().Be(917);
        }
    }
}