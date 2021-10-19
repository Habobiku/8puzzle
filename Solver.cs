using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C5;

namespace _8PuzzleGame.Solvers
{
    public abstract class Solver
    {
        protected int[,] GoalState { get; set; }
        protected int MaxFringeSize { get; set; }
        protected int MaxSearchDepth { get; set; }
        protected int NodesExpanded { get; set; }

        public Solver()
        {
            this.GoalState = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8, } };
        }

        public abstract void Solve(State state);

        protected List<State> GenerateChildrenStates(State currentState, int x, int y)
        {
            var children = new List<State>();

            var rightState = currentState.MoveZeroToTheRight(x, y);
            if (rightState != null)
            {
                rightState.LastMove = "right";
                rightState.Parent = currentState;
                rightState.SearchDepth++;
                children.Add(rightState);
            }

            var leftState = currentState.MoveZeroToTheLeft(x, y);
            if (leftState != null)
            {
                leftState.LastMove = "left";
                leftState.Parent = currentState;
                leftState.SearchDepth++;
                children.Add(leftState);
            }

            var downState = currentState.MoveZeroDown(x, y);
            if (downState != null)
            {
                downState.LastMove = "down";
                downState.Parent = currentState;
                downState.SearchDepth++;
                children.Add(downState);
            }

            var upState = currentState.MoveZeroUp(x, y);
            if (upState != null)
            {
                upState.LastMove = "up";
                upState.Parent = currentState;
                upState.SearchDepth++;
                children.Add(upState);
            }

            return children;
        }

        public void PrintResults(State finalState,int all, int mem)
        {
            var searchDepth = finalState.SearchDepth;
            var path = FindPath(finalState);
            var pathToGoal = this.GetPathAsString(path);
            var costOfPath = path.Count;
            int puff = Closed(finalState);
            if(costOfPath<100)
            Console.WriteLine($"Path to goal: {pathToGoal}");
            Console.WriteLine($"Cost of path: {costOfPath}");
            Console.WriteLine("Search depth : "+searchDepth);
            Console.WriteLine("All states : " + all);
            Console.WriteLine("States int mem : " + mem);
           // Console.WriteLine("Puff: " + puff);




        }


        //private string 
        private List<string> FindPath(State state)
        {
            var path = new List<string>();
            while (state.Parent != null)
            {
                path.Add(state.LastMove);
                state = state.Parent;
            }

            return path;
        }
        
        public int Closed(State state)
        {
            int close = 0;
           List<string> path =FindPath(state);
            for(int i = 0; i<path.Count-1; i++)
            {
                if(path[i+1]==null)
                {
                    break;
                }
                if(path[i]=="up"&& path[i+1]=="down"||path[i]=="right"&&path[i+1]=="left"||path[i]=="down"&&path[i+1]=="up" || path[i] == "left" && path[i + 1] == "right")
                close++;
            }
            
           
            return close;
        }
        

        private string GetPathAsString(List<string> path)
        {
            var sb = new StringBuilder();
            sb.Append("[");

            for (int i = path.Count - 1; i >= 0; i--)
            {
                sb.Append("'");
                sb.Append(path[i]);
                sb.Append("'");
                sb.Append(", ");
            }

            var pathToGoal = sb.ToString().TrimEnd(new[] { ',', ' ' });
            pathToGoal += "]";

            return pathToGoal;
        }
    }
}
