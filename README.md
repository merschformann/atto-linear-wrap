# atto-linear-wrap

A lightweight CPLEX and Gurobi wrapper.

## Nuget package

Add the package to your project:

```sh
dotnet add package Atto.LinearWrap
```

In order to run it, you need to have the library defined in the second `PropertyGroup` of the [Atto.LinearWrap.csproj](Atto.LinearWrap\Atto.LinearWrap.csproj) available for dynamic linking (notice Gurobi vs. CPLEX and win64 vs. linux64).

## Example

A Sudoku solver using the Atto.LinearWrap library lives [here](examples/Sudoku/README.md).

## Remarks

The package is a wrapper for the [CPLEX](https://www.ibm.com/analytics/cplex-optimizer) and [Gurobi](https://www.gurobi.com/) solvers. It was written for specific projects back while I was at the university. For example, this package is used by [SardineCan](https://github.com/merschformann/sardine-can).

This is a very lightweight package tied to specific solver versions. It was not intended to be used in more generic ways. However, maybe it is useful as a template or starting point.

Unfortunately, I cannot include the proprietary libraries / binaries of CPLEX and Gurobi. Check the paths in [Atto.LinearWrap.csproj](Atto.LinearWrap\Atto.LinearWrap.csproj) to see where they need to go.
