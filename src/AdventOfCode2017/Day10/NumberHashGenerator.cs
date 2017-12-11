using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day10
{
    public class NumberHashGenerator
    {
        public static readonly IList<int> DefaultNumbers = Enumerable.Range(0, 256).ToList();

        public IList<int> Numbers { get; }
        public int CurrentIndex { get; private set; }
        public int SkipSize { get; private set; }

        public int Checksum => Numbers[0] * Numbers[1];

        public NumberHashGenerator(IList<int> numbers = null)
        {
            Numbers = numbers ?? DefaultNumbers;
            CurrentIndex = 0;
            SkipSize = 0;
        }

        public void TwistSet(IList<int> lengths, int rounds = 1)
        {
            if (rounds < 0)
                throw new Exception("Can't do that.");

            for (var round = 0; round < rounds; ++round)
            {
                foreach (var length in lengths)
                {
                    Twist(CurrentIndex, length);

                    CurrentIndex += length + SkipSize++;

                    if (CurrentIndex >= Numbers.Count)
                        CurrentIndex -= Numbers.Count;
                }
            }
        }

        internal void Twist(int index, int length)
        {
            if (length > Numbers.Count)
                throw new Exception("Can't do that");

            var sublist = GetSublist(index, length).Reverse().ToList();
            ReplaceSublist(index, sublist);
        }

        internal IEnumerable<int> GetSublist(int index, int length)
        {
            for (var k = 0; k < length; ++k, ++index)
            {
                if (index >= Numbers.Count)
                    index = 0;

                yield return Numbers[index];
            }
        }

        internal void ReplaceSublist(int index, IList<int> sublist)
        {
            foreach (var item in sublist)
            {
                if (index >= Numbers.Count)
                    index = 0;

                Numbers[index++] = item;
            }
        }
    }
}