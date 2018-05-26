using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2017.Day19
{
    public class RoutingDiagram
    {
        private static readonly List<char> AcceptableCharsOtherThanLetters = new List<char> { '+', '-', '|' };
        public char[][] Grid { get; }
        public (int X, int Y) StartingPoint { get; }
        public (int X, int Y, Direction Moving) Current { get; internal set; }
        public char CurrentLetter => Grid[Current.X][Current.Y];

        public StringBuilder VisitedLetters { get; }

        public static readonly IDictionary<Direction, Mod> Shifts = new Dictionary<Direction, Mod>
        {
            { Direction.N, new Mod(-1,  0) },
            { Direction.E, new Mod( 0,  1) },
            { Direction.S, new Mod( 1,  0) },
            { Direction.W, new Mod( 0, -1) }
        };

        public RoutingDiagram(char[][] grid)
        {
            Grid = grid;
            StartingPoint = (0, Grid[0].AsString().IndexOf('|'));
            Current = (StartingPoint.X, StartingPoint.Y, Direction.S);
            VisitedLetters = new StringBuilder();
        }

        internal void Step()
        {
            var current = CurrentLetter;
            var foundNewLocation = false;
            if(current == '+')
            {
                if(Current.Moving == Direction.N || Current.Moving == Direction.S)
                {
                    var east = Next(Direction.E);
                    if(east.IsGood && Grid[east.X][east.Y] == '-')
                    {
                        Current = (east.X, east.Y, Direction.E);
                        foundNewLocation = true;
                    }
                    else
                    {
                        var west = Next(Direction.W);
                        if(west.IsGood && Grid[west.X][west.Y] == '-')
                        {
                            Current = (west.X, west.Y, Direction.W);
                            foundNewLocation = true;
                        }
                    }
                }
                else
                {
                    var north = Next(Direction.N);
                    if(north.IsGood && Grid[north.X][north.Y] == '|')
                    {
                        Current = (north.X, north.Y, Direction.N);
                        foundNewLocation = true;
                    }
                    else
                    {
                        var south = Next(Direction.S);
                        if(south.IsGood && Grid[south.X][south.Y] == '|')
                        {
                            Current = (south.X, south.Y, Direction.S);
                            foundNewLocation = true;
                        }
                    }
                }

                if(!foundNewLocation)
                    throw new Exception("Could not navigate from '+' to next location");
            }
            else if(char.IsLetter(current))
            {
                VisitedLetters.Append(current);
                MoveToNext();
                if(!AcceptableCharsOtherThanLetters.Contains(CurrentLetter))
                    throw new Exception($"End of path? Visited: {VisitedLetters}");
            }
            else if(current == '-' || current == '|')
            {
                MoveToNext();
            }
            else
            {
                throw new Exception("Hey I don't know where I am");
            }

            (bool IsGood, int X, int Y) Next(Direction direction)
            {
                var mod = Shifts[direction];
                var (nextX, nextY) = (Current.X + mod.X, Current.Y + mod.Y);

                var nextIsBad = nextX >= Grid.Length || nextX < 0 || nextY >= Grid[0].Length || nextY < 0;

                return (!nextIsBad, nextX, nextY);
            }

            void MoveToNext()
            {
                var possibleLocation = Next(Current.Moving);

                if(!possibleLocation.IsGood)
                    throw new Exception("Trying to move beyond the grid!");

                Current = (possibleLocation.X, possibleLocation.Y, Current.Moving);
            }
        }

        public override string ToString()
        {
            return Grid.Select(x => new string(x)).JoinStrings("\n");
        }
    }
}
