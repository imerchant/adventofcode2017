namespace AdventOfCode2017.Day08
{
    public interface IOperation
    {
        int Value { get; }
        int OperateOn(int otherValue);
    }
}

