using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Indicates the type a <see cref="DependencyProperty"/>
    /// </summary>
    public enum DependencyPropertyType
    {
        /// <summary>
        /// The <see cref="DependencyProperty"/> is a standard property
        /// </summary>
        Property,
        /// <summary>
        /// The <see cref="DependencyProperty"/> is an attached property
        /// </summary>
        AttachedProperty
    }

}
