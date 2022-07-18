using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Atto.LinearWrap;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse input values
            var inputValues = args.Select(a =>
            {
                var tuple = a.Split(",").Select(v =>
                {
                    if (int.TryParse(v, out var s))
                        return s;
                    throw new ArgumentException($"Cannot parse number {v} in {a}");
                }).ToArray();
                if (tuple.Length != 3)
                    throw new ArgumentException($"Invalid input tuple {a}");
                return (tuple[0], tuple[1], tuple[2]);
            });

            // Solve Sudoku
            var (solution, feasible) = SolveSudoku(inputValues);
            if (!feasible)
            {
                Console.WriteLine("Infeasible!");
                return;
            }

            // Print solution
            for (var r = 0; r < 9; r++)
            {
                if (r % 3 == 0)
                    Console.WriteLine(new string('-', 13));
                for (var c = 0; c < 9; c++)
                {
                    if (c % 3 == 0)
                        Console.Write("|");
                    Console.Write(solution[r, c]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(new string('-', 13));
        }

        /// <summary>
        /// Solves a Sudoku puzzle with the given input values.
        /// </summary>
        /// <param name="input">All fixed values given per row/col index. Indices are 0-based.</param>
        /// <returns>The solution to the puzzle alongside a boolean value indicating whether it was possible to solve it.</returns>
        private static (int[,], bool) SolveSudoku(IEnumerable<(int row, int col, int val)> input)
        {
            // Init a model using Gurobi
            var model = new LinearModel(SolverType.Gurobi, Console.Write);
            Console.WriteLine("Setting up model and solving it with " + model.Type);
            var choices = new VariableCollection<int, int, int>(model, VariableType.Binary, 0, 1, (r, c, v) => $"cell-{r}{c}-value-{v}");

            // Make sure only one value is used per cell
            for (var r = 0; r < 9; r++)
                for (var c = 0; c < 9; c++)
                    model.AddConstr(LinearExpression.Sum(Enumerable.Range(0, 9).Select(v => choices[r, c, v])) == 1, $"value-integrity-{r}{c}");

            // Make sure every value is only used once per row and column
            for (var v = 0; v < 9; v++)
                for (var r = 0; r < 9; r++)
                    model.AddConstr(LinearExpression.Sum(Enumerable.Range(0, 9).Select(c => choices[r, c, v])) == 1, $"row-integrity-{r}-{v}");
            for (var v = 0; v < 9; v++)
                for (var c = 0; c < 9; c++)
                    model.AddConstr(LinearExpression.Sum(Enumerable.Range(0, 9).Select(r => choices[r, c, v])) == 1, $"column-integrity-{c}-{v}");

            // Make sure every value is only used once per block
            for (var v = 0; v < 9; v++)
                for (var br = 0; br < 3; br++)
                    for (var bc = 0; bc < 3; bc++)
                        model.AddConstr(
                            LinearExpression.Sum(Enumerable.Range(0, 3).SelectMany(r => Enumerable.Range(0, 3).Select(c => choices[br * 3 + r, bc * 3 + c, v]))) == 1,
                            $"block-integrity-{br}-{bc}"
                        );

            // Apply input values
            foreach (var (row, col, val) in input)
                model.AddConstr(choices[row, col, val - 1] == 1, $"fix-{row}{col}-{val}");

            // Submit changes and optimize
            model.Update();
            model.Optimize();

            // Quit on no solution
            if (!model.HasSolution())
                return (null, false);

            // Get solution
            var solution = new int[9, 9];
            for (var r = 0; r < 9; r++)
                for (var c = 0; c < 9; c++)
                    for (var v = 0; v < 9; v++)
                        if (choices[r, c, v].GetValue() == 1)
                            solution[r, c] = v + 1;
            return (solution, true);
        }
    }
}
