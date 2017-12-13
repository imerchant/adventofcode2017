namespace AdventOfCode2017.Day08
{
    public interface IComparison
    {
        int Value { get; }
        bool CompareTo(int otherValue);
    }
}

