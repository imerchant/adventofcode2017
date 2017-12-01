using Xunit.Abstractions;

namespace AdventOfCode2017.Tests
{
    public abstract class TestBase
    {
        protected ITestOutputHelper Output { get; }

        protected TestBase(ITestOutputHelper output)
        {
            this.Output = output;
        }
    }
}