using System.Collections.Generic;

namespace AdventOfCode2017.Day25
{

    public class State
    {
        public char Id { get; }
        public IDictionary<int, (int NewValue, int Move, char NewState)> Instructions { get; }

        public State(char id, (int, int, char) for0, (int, int, char) for1)
        {
            Id = id;
            Instructions = new Dictionary<int, (int, int, char)>
            {
                { 0, for0 },
                { 1, for1 }
            };
        }
    }
}