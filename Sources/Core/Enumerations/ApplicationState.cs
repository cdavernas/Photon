using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Describes the state of an <see cref="Application"/>
    /// </summary>
    public enum ApplicationState
    {
        /// <summary>
        /// The application is up and running
        /// </summary>
        Running,
        /// <summary>
        /// The application is shutting down
        /// </summary>
        ShuttingDown,
        /// <summary>
        /// The application is not running
        /// </summary>
        NotRunning
    }

}
