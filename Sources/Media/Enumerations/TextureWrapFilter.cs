using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Defines the wrap filter to apply to a <see cref="Texture"/>
    /// </summary>
    public enum TextureWrapFilter
    {
        /// <summary>
        /// The <see cref="Texture"/> will be repeated on the surface
        /// </summary>
        Repeat,
        /// <summary>
        /// The <see cref="Texture"/> will be skewed to fit the surface's bounds
        /// </summary>
        Clamp,
        /// <summary>
        /// Same as <see cref="Clamp"/>, but gets rid of some artifacts on the borders
        /// </summary>
        ClampToEdge,
    }

}
