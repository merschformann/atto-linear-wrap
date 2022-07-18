# Sudoku solver example

Implements a simple Sudoku solver. A Gurobi license and necessary lib files are required for execution. The solver can be invoked from the command line with custom input values. E.g., use the following call to solve a puzzle with the values 1, 2, 3 in the top left corner (first value of tuple is row, second is column, third is the value to be placed):

```sh
dotnet run 0,0,1 0,1,2 0,2,3
```
