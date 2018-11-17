using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day23
{
    public class Program
    {
        public IDictionary<char, long> Registers { get; }
        public IList<string> Instructions { get; }
        public int CurrentInstruction { get; private set; }
        public int MulInvoked { get; private set; }

        public Program(IList<string> input)
        {
            Instructions = input;
            CurrentInstruction = 0;
            Registers = "abcdefgh".ToDictionary(c => c, _ => 0L);
        }

        private long GetValue(string part)
        {
            if (long.TryParse(part, out var value))
            {
                return value;
            }

            return Registers[char.Parse(part)];
        }

        public void Run()
        {
            for (int k = 0; k >= 0 && k < Instructions.Count;)
            {
                var parts = Instructions[k].SplitOn(' ');
                if (parts[0] == "set")
                {
                    var target = char.Parse(parts[1]);
                    Registers[target] = GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "sub")
                {
                    var target = char.Parse(parts[1]);
                    Registers[target] -= GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "mul")
                {
                    MulInvoked++;
                    var target = char.Parse(parts[1]);
                    Registers[target] *= GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "jnz")
                {
                    var value = GetValue(parts[1]);
                    if (value != 0)
                        k += int.Parse(parts[2]);
                    else
                        ++k;
                }
                else
                {
                    throw new Exception("could not parse instruction");
                }
            }
        }
    }
}