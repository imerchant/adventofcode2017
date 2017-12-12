using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace AdventOfCode2017.Day10
{
    public class NumberHashGenerator
    {
        public IList<int> DefaultNumbers => Enumerable.Range(0, 256).ToList();

        public IList<int> Numbers { get; }
        public int CurrentIndex { get; private set; }
        public int Skip { get; private set; }

        public int Checksum => Numbers[0] * Numbers[1];

        public NumberHashGenerator(IList<int> numbers = null)
        {
            Numbers = numbers ?? DefaultNumbers;
            CurrentIndex = 0;
            Skip = 0;
        }

        public string CalculateKnotHash(IList<int> lengths)
        {
            TwistSet(lengths, 64);

            return Numbers
                .Batch(16)
                .Select(batch => batch.Aggregate(0, (cumulative, item) => cumulative ^ item, i => $"{i:x2}"))
                .JoinStrings();
        }

        internal void TwistSet(IList<int> lengths, int rounds = 1)
        {
            if (rounds < 0)
                throw new Exception("Can't do that.");

            for (var round = 0; round < rounds; ++round)
            {
                foreach (var length in lengths)
                {
                    Twist(CurrentIndex, length);

                    CurrentIndex = (CurrentIndex + length + Skip++) % Numbers.Count;
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
            for (var k = 0; k < length; ++k)
            {
                yield return Numbers[index++ % Numbers.Count];
            }
        }

        internal void ReplaceSublist(int index, IList<int> sublist)
        {
            foreach (var item in sublist)
            {
                Numbers[index++ % Numbers.Count] = item;
            }
        }
    }
}