using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day25
{
    public class Machine
    {
        public int Location { get; private set; }
        public IDictionary<int, int> Tape { get; }
        public char Current { get; private set; }
        public IDictionary<char, State> States { get; }
        public int Checksum => Tape.Values.Sum();

        public Machine(char initialState, IEnumerable<State> states)
        {
            Current = initialState;
            Tape = new DefaultDictionary<int, int>(_ => 0);
            States = states.ToDictionary(x => x.Id);
        }

        public void Tick()
        {
            var instruction = States[Current].Instructions[Tape[Location]];
            Tape[Location] = instruction.NewValue;
            Location += instruction.Move;
            Current = instruction.NewState;
        }
    }
}