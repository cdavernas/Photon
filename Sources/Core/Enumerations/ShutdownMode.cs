using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Describes the way an <see cref="Application"/> shuts down
    /// </summary>
    public enum ShutdownMode
    {
        /// <summary>
        /// The <see cref="Application"/> shuts down as soon as its main <see cref="Window"/> closes
        /// </summary>
        OnMainWindowClosed,
        /// <summary>
        /// The <see cref="Application"/> shuts down when the last of its registered, active <see cref="Window"/>s closes
        /// </summary>
        OnLastWindowClosed,
        /// <summary>
        /// The <see cref="Application"/> only shuts down if explicitly asked to
        /// </summary>
        OnExplicitShutdown
    }

}
