using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents the <see cref="DependencyObject"/>s tree of a given <see cref="IUIElement"/>
    /// </summary>
    public class DependencyElementTree
    {

        /// <summary>
        /// Initializes a new <see cref="DependencyElementTree"/> instance based on the specified root <see cref="IUIElement"/>
        /// </summary>
        /// <param name="root">The root <see cref="IUIElement"/> of the <see cref="DependencyElementTree"/></param>
        public DependencyElementTree(DependencyObject root)
        {
            this.Root = root;
        }

        /// <summary>
        /// Gets the <see cref="DependencyElementTree"/>'s root <see cref="DependencyObject"/>
        /// </summary>
        public DependencyObject Root { get; private set; }

    }

}
