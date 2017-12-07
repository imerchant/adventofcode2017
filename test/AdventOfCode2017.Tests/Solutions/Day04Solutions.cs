using System.Linq;
using AdventOfCode2017.Day04;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day04Solutions : TestBase
    {
        public Day04Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_CountValidPassphrases()
        {
            var phrases = Input.Day04Parse(Input.Day04).Select(x => new Passphrase(x));

            phrases.Count(x => x.IsValid).Should().Be(455);
        }

        private const string Puzzle1Example =
@"aa bb cc dd ee
aa bb cc dd aa
aa bb cc dd aaa";

        [Fact]
        public void Puzzle1_ExamplesPass()
        {
            var phrases = Input.Day04Parse(Puzzle1Example).Select(x => new Passphrase(x));

            phrases.Count(x => x.IsValid).Should().Be(2);
        }
    }
}
