using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Day18
{
    public class Duet
    {
        public DuetProgram First { get; }
        public DuetProgram Second { get; }
        public IList<string> Instructions { get; }
        public Queue<long> FirstToSecond { get; }
        public Queue<long> SecondToFirst { get; }

        public Duet(IList<string> instructions)
        {
            First = new DuetProgram(0);
            Second = new DuetProgram(1);
            Instructions = instructions;
            FirstToSecond = new Queue<long>();
            SecondToFirst = new Queue<long>();
        }

        public int CountMessagesSentBySecond()
        {
            var firstHasSent = 0;
            var secondHasSent = 0;

            while (true)
            {
                if (First.OnInstruction < 0 || First.OnInstruction >= Instructions.Count)
                    First.IsDone = true;

                if (Second.OnInstruction < 0 || Second.OnInstruction >= Instructions.Count)
                    Second.IsDone = true;

                if (((First.IsWaiting && !SecondToFirst.HasAny()) || First.IsDone) && 
                    ((Second.IsWaiting && !FirstToSecond.HasAny()) || Second.IsDone))
                    break;

                var firstOperated = false;
                if (First.IsWaiting && SecondToFirst.HasAny())
                {
                    var received = SecondToFirst.Dequeue();
                    First.Registers[First.StoreReceivedIn.Value] = received;
                    First.OnInstruction++;
                    First.IsWaiting = false;
                    First.StoreReceivedIn = null;
                    firstOperated = true;
                }

                var secondOperated = false;
                if (Second.IsWaiting && FirstToSecond.HasAny())
                {
                    var received = FirstToSecond.Dequeue();
                    Second.Registers[Second.StoreReceivedIn.Value] = received;
                    Second.OnInstruction++;
                    Second.IsWaiting = false;
                    Second.StoreReceivedIn = null;
                    secondOperated = true;
                }

                if (!First.IsWaiting && !firstOperated && !First.IsDone)
                {
                    Operate(First, FirstToSecond, ref firstHasSent);
                }

                if (!Second.IsWaiting && !secondOperated && !Second.IsDone)
                {
                    Operate(Second, SecondToFirst, ref secondHasSent);
                }

            }

            return secondHasSent;
        }

        private void Operate(DuetProgram program, Queue<long> sendToOther, ref int hasSent)
        {
            var instruction = Instructions[program.OnInstruction].SplitOn(' ');
            var (id, value) = program.GetRegister(char.Parse(instruction[1]));
            if (instruction[0] == "snd")
            {
                sendToOther.Enqueue(value);
                hasSent++;
                program.OnInstruction++;
            }
            else if (instruction[0] == "set")
            {
                program.Registers[id] = program.GetValue(instruction[2]);
                program.OnInstruction++;
            }
            else if (instruction[0] == "add")
            {
                program.Registers[id] += program.GetValue(instruction[2]);
                program.OnInstruction++;
            }
            else if (instruction[0] == "mul")
            {
                program.Registers[id] *= program.GetValue(instruction[2]);
                program.OnInstruction++;
            }
            else if (instruction[0] == "mod")
            {
                program.Registers[id] %= program.GetValue(instruction[2]);
                program.OnInstruction++;
            }
            else if (instruction[0] == "rcv")
            {
                program.IsWaiting = true;
                program.StoreReceivedIn = id;
            }
            else if (instruction[0] == "jgz")
            {
                var guardValue = program.GetValue(instruction[1]);
                if (guardValue > 0)
                    program.OnInstruction += (int) program.GetValue(instruction[2]);
                else
                    program.OnInstruction++;
            }
            else
            {
                throw new Exception("could not parse instruction");
            }
        }
    }

    public class DuetProgram
    {
        public int Id { get; }
        public IDictionary<char, long> Registers { get; }
        public bool IsWaiting { get; set; }
        public char? StoreReceivedIn { get; set; }
        public bool IsDone { get; set; }
        public int OnInstruction { get; set; }

        public DuetProgram(int id)
        {
            Id = id;
            Registers = new Dictionary<char, long>();
            Registers['p'] = id;
            IsWaiting = false;
            IsDone = false;
            StoreReceivedIn = null;
        }

        public long GetValue(string part)
        {
            if (long.TryParse(part, out var value))
            {
                return value;
            }

            var (_, val) = GetRegister(char.Parse(part));
            return val;
        }

        public (char id, long value) GetRegister(char id)
        {
            if (!char.IsLetter(id))
                return ('A', -1);

            if (Registers.TryGetValue(id, out var value))
            {
                return (id, value);
            }

            value = 0;
            Registers[id] = value;
            return (id, value);
        }
    }
}
