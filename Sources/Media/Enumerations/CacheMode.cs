using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Describes the way a <see cref="Visual"/> is cached by Photon
    /// </summary>
    public enum CacheMode
    {
        /// <summary>
        /// The <see cref="Visual"/> is not cached
        /// </summary>
        NoCache,
        /// <summary>
        /// The <see cref="Visual"/> generates a unique <see cref="Drawing"/> instance that is never updated
        /// </summary>
        Cache,
        /// <summary>
        /// The <see cref="Visual"/> generates a new <see cref="Drawing"/> only when the <see cref="Visual.InvalidateVisual"/> method is called
        /// </summary>
        DynamicCache
    }

}
