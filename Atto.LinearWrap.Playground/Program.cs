using System;

namespace Atto.LinearWrap.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Experimental();
        }

        static void Experimental()
        {
            var mod = new LinearModel(SolverType.Gurobi, Console.WriteLine);
        }
    }
}
