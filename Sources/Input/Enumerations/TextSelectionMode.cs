using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Input
{

    /// <summary>
    /// Describes the way a portion of text is selected
    /// </summary>
    public enum TextSelectionMode
    {
        /// <summary>
        /// The text is selected from left to right (forward)
        /// </summary>
        LeftHanded,
        /// <summary>
        /// The text is selected from right to left (backward)
        /// </summary>
        RightHanded
    }

}
