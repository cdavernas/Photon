using Photon.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Controls
{

    /// <summary>
    /// This interfaces is implements by all <see cref="UIElement"/>s that define a padding
    /// </summary>
    public interface IPaddedElement
        : IUIElement
    {

        /// <summary>
        /// Gets/sets a <see cref="Thickness"/> representing the element's padding
        /// </summary>
        Thickness Padding { get; set; }

    }

}
