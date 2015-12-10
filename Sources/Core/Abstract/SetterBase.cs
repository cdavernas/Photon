using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents the base class for value setters
    /// </summary>
    public abstract class SetterBase
    {

        /// <summary>
        /// Gets the <see cref="SetterBase"/>'s parent <see cref="Photon.Style"/>
        /// </summary>
        public Style Style { get; internal set; }

        /// <summary>
        /// Gets a boolean indicating whether or not this object is in an immutable state
        /// </summary>
        public bool IsSealed { get; private set; }

        /// <summary>
        /// Applies the <see cref="SetterBase"/> to the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to apply the <see cref="SetterBase"/> to</param>
        internal abstract void Set(DependencyObject dependencyObject);

    }

}
