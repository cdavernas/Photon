using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Threading
{

    /// <summary>
    /// Describes the priorities at which <see cref="DispatcherOperation"/>s can be invoked by way of the <see cref="Dispatcher"/>.
    /// </summary>
    public enum DispatcherPriority
    {
        /// <summary>
        /// The <see cref="DispatcherOperation"/> will be executed with the lowest priority
        /// </summary>
        Lowest,
        /// <summary>
        /// The <see cref="DispatcherOperation"/> will be executed with a low priority
        /// </summary>
        Low,
        /// <summary>
        /// The <see cref="DispatcherOperation"/> will be executed with a normal priority
        /// </summary>
        Normal,
        /// <summary>
        /// The <see cref="DispatcherOperation"/> will be executed with a high priority
        /// </summary>
        High,
        /// <summary>
        /// The <see cref="DispatcherOperation"/> will be executed with a the highest priority
        /// </summary>
        Highest
    }

}
