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

            var (_, val) = GetRegister(char.Parse(part));
            return val;
        }

        private (char id, long value) GetRegister(char id)
        {
            if (Registers.TryGetValue(id, out var value))
            {
                return (id, value);
            }

            value = 0;
            Registers[id] = value;
            return (id, value);
        }

        public void Run()
        {
            for (int k = 0; k >= 0 && k < Instructions.Count;)
            {
                var parts = Instructions[k].SplitOn(' ');
                if (parts[0] == "set")
                {
                    var (target, _) = GetRegister(char.Parse(parts[1]));
                    Registers[target] = GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "sub")
                {
                    var (target, _) = GetRegister(char.Parse(parts[1]));
                    Registers[target] -= GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "mul")
                {
                    MulInvoked++;
                    var (target, _) = GetRegister(char.Parse(parts[1]));
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