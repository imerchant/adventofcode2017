using System.Numerics;

namespace AdventOfCode2017.Day15
{
    public class Generator
    {
        public BigInteger Last { get; private set; }
        public int Factor { get; }

        public Generator(int seed, int factor)
        {
            Last = seed;
            Factor = factor;
        }

        public BigInteger Next()
        {
            Last = (Last * Factor) % 2147483647;
            return Last;
        }
    }

    public static class BigIntegerExtensions
    {
        public static int Low16(this BigInteger i)
        {
            return (int) (i & 0xFFFF);
        }
    }
}
