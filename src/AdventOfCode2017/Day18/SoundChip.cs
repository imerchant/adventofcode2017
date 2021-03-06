﻿using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Day18
{
    public class SoundChip
    {
        public IDictionary<char, long> Registers { get; }

        public SoundChip()
        {
            Registers = new DefaultDictionary<char, long>(() => 0);
        }

        public void FollowInstructions(IList<string> instructions)
        {
            long lastPlayedSound = 0;

            for (var k = 0; k >= 0 && k < instructions.Count;)
            {
                var parts = instructions[k].SplitOn(' ');
                var (id, value) = GetRegister(char.Parse(parts[1]));
                if (parts[0] == "snd")
                {
                    if (value != 0)
                        lastPlayedSound = value;
                    ++k;
                }
                else if (parts[0] == "set")
                {
                    Registers[id] = GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "add")
                {
                    Registers[id] += GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "mul")
                {
                    Registers[id] *= GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "mod")
                {
                    Registers[id] %= GetValue(parts[2]);
                    ++k;
                }
                else if (parts[0] == "rcv")
                {
                    if (value != 0)
                        throw new Exception($"recovered: {lastPlayedSound}");
                    ++k;
                }
                else if (parts[0] == "jgz")
                {
                    if (value > 0)
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
            return (id, Registers[id]);
        }
    }
}
