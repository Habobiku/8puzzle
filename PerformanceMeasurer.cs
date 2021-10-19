using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _8PuzzleGame
{
    public class PerformanceMeasurer
    {
        public PerformanceMeasurer(int[,] arr)
        {
            this.StopWatch = new Stopwatch();
            this.startstate = arr;

        }

        public Stopwatch StopWatch { get; private set; }
        public Process Process { get; private set; }
        public int[,] startstate {get;set;}
        public int [,] GoalState = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8, } };
    public void StartMeasuring()
        {
            this.StopWatch.Start();
            this.Process = Process.GetCurrentProcess();
        }

        public void StopMeasuring()
        {
            this.StopWatch.Stop();
        }
        public string H1()
        {

            List<int> goallist = GoalState.Cast<int>().ToList();
            List<int> startlist = startstate.Cast<int>().ToList();
            List<string> h = new List<string>();
            for (int i = 0; i < startlist.Count; i++)
            {
                if (goallist[i] != startlist[i])
                {
                    h.Add($"{startlist[i]}");
                }
            }
            var sb = new StringBuilder();
            sb.Append("[");

            for (int i = h.Count - 1; i >= 0; i--)
            {
                 
                if (h[i] != "0")
                {
                    sb.Append("'");
                    sb.Append(h[i]);
                    sb.Append("'");
                    sb.Append(", ");
                }
                    
            }

            var h1 = sb.ToString().TrimEnd(new[] { ',', ' ' });
            h1 += "]";
            return h1;
        }
        public void PrintResults()
        {

            Console.WriteLine($"H1: {H1()} ");
            Console.WriteLine($"Running time: {this.StopWatch.Elapsed.TotalSeconds} s");
            Console.WriteLine($"Max ram usage: {this.Process.PeakWorkingSet64} kb");
        }
    }
}
