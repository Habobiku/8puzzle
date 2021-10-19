using _8PuzzleGame.Solvers;
using System;
namespace _8PuzzleGame
{
    public class Startup
    {
        public static void Main()
        {
           

            var arr = new int[3, 3]
            {

                { 1, 5, 2},
                   {0, 8, 7 },
                   { 3, 4, 6 }
            };

            var board = new Board(arr);
            var startingState = new State(board, null, null, 0);
            var bfs = new BfsSolver();
            var Astar = new AStarSolver();
            var measurer = new PerformanceMeasurer(arr);
            measurer.StartMeasuring();
            Astar.Solve(startingState);
            Console.WriteLine("//////////////////////////////");
            bfs.Solve(startingState);
            measurer.StopMeasuring();
            Console.WriteLine("//////////////////////////////");

            measurer.PrintResults();
        }
    }
}
