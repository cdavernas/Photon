using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Specifies the display state of an element
    /// </summary>
    public enum Visibility
    {
        /// <summary>
        /// Display the element
        /// </summary>
        Visible,
        /// <summary>
        /// Do not display the element, but reserve space for the element in layout
        /// </summary>
        Hidden,
        /// <summary>
        /// Do not display the element, and do not reserve space for it in layout
        /// </summary>
        Collapsed
    }

}
