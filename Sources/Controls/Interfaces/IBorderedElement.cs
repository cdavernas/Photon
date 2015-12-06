using Photon.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This interface is implemented by all <see cref="IUIElement"/>s that define a border
    /// </summary>
    public interface IBorderedElement
        : IUIElement
    {

        /// <summary>
        /// Gets/sets the <see cref="Brush"/> with which to paint the element's borders
        /// </summary>
        Brush BorderBrush { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="Thickness"/> of the element's border
        /// </summary>
        Thickness BorderThickness { get; set; }

    }

}
