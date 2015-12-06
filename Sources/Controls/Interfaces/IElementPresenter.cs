using Photon.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Controls
{

    /// <summary>
    /// This interface is implemented by all <see cref="IUIElement"/> that presents at least one child <see cref="UIElement"/>
    /// </summary>
    public interface IElementPresenter
        : IUIElement
    {

        /// <summary>
        /// Gets a boolean indicating whether or not the contents affects the element's layout
        /// </summary>
        bool ContentsAffectsLayout { get; }

        /// <summary>
        /// Gets the element's <see cref="Size"/>
        /// </summary>
        Size ContentsSize { get; }

        /// <summary>
        /// Computes the x and y offset for the specified child <see cref="UIElement"/>
        /// </summary>
        /// <param name="child">The child for which to compute the offset</param>
        /// <returns>A <see cref="Point"/> representing the x and y offset of the specified child <see cref="UIElement"/></returns>
        Point ComputeChildOffset(UIElement child);

    }

}
