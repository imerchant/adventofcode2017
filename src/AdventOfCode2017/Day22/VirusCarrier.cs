using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Day22
{
    public class VirusCarrier
    {
        private static IDictionary<NodeState, NodeState> StateMutations = new Dictionary<NodeState, NodeState>
        {
            { NodeState.Clean, NodeState.Weakened },
            { NodeState.Weakened, NodeState.Infected },
            { NodeState.Infected, NodeState.Flagged },
            { NodeState.Flagged, NodeState.Clean }
        };

        public IDictionary<(int X, int Y), NodeState> VisitedNodes { get; }
        public Direction Facing { get; private set; }
        public (int X, int Y) Location { get; private set; }
        public int InfectionsCaused { get; private set; }

        public VirusCarrier(IList<string> input)
        {
            var west = new Direction("West", 0, -1);
            var south = new Direction("South", 1, 0, right: west);
            var east = new Direction("East", 0, 1, right: south);
            var north = new Direction("North", -1, 0, left: west, right: east);
            west.Left = south;
            west.Right = north;
            south.Left = east;
            east.Left = north;

            Facing = north;
            Location = ((int)Math.Floor(input.Count / 2.0), (int)Math.Floor(input[0].Length / 2.0));
            InfectionsCaused = 0;

            VisitedNodes = new DefaultDictionary<(int, int), NodeState>(() => NodeState.Clean);

            for (var row = 0; row < input.Count; ++row)
            {
                for (var col = 0; col < input[row].Length; ++col)
                {
                    if (input[row][col] == '#')
                        VisitedNodes[(row, col)] = NodeState.Infected;
                }
            }
        }

        public void Tick()
        {
            var locationIsInfected = VisitedNodes[Location] == NodeState.Infected;

            // turn
            Facing = locationIsInfected
                ? Facing.Right
                : Facing.Left;

            // infect or clean
            if (locationIsInfected)
            {
                VisitedNodes[Location] = NodeState.Clean;
            }
            else
            {
                InfectionsCaused++;
                VisitedNodes[Location] = NodeState.Infected;
            }

            // move
            Location = (Location.X + Facing.MutateX, Location.Y + Facing.MutateY);
        }

        public void EvolvedTick()
        {
            var state = VisitedNodes[Location];

            //move
            switch(state)
            {
                case NodeState.Clean:
                    Facing = Facing.Left;
                    break;
                case NodeState.Weakened:
                    // don't change direction
                    break;
                case NodeState.Infected:
                    Facing = Facing.Right;
                    break;
                case NodeState.Flagged:
                    Facing = Facing.Left.Left; // turn around
                    break;
            }

            // act
            var nextState = StateMutations[state];
            if (nextState == NodeState.Infected)
                InfectionsCaused++;
            VisitedNodes[Location] = nextState;

            // move
            Location = (Location.X + Facing.MutateX, Location.Y + Facing.MutateY);
        }
    }
}