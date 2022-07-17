using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atto.LinearWrap
{
    /// <summary>
    /// Defines the solver to use.
    /// </summary>
    public enum SolverType
    {
        /// <summary>
        /// Indicates that CPLEX will be used to optimize the model.
        /// </summary>
        CPLEX,
        /// <summary>
        /// Indicates that Gurobi will be used to optimize the model.
        /// </summary>
        Gurobi
    }
}
