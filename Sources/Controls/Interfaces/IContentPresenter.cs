using Photon.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Controls
{

    /// <summary>
    /// This interface is implemented by all <see cref="IUIElement"/> that presents contents, such as other <see cref="UIElement"/>s or text
    /// </summary>
    public interface IContentPresenter
        : IUIElement, Markup.IAddChild
    {

        /// <summary>
        /// Gets a boolean indicating whether or not the element's content can align horizontally
        /// </summary>
        bool ContentsAlignHorizontally { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the element's content can align vertically
        /// </summary>
        bool ContentsAlignVertically { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the contents affects the element's layout
        /// </summary>
        bool ContentsAffectsLayout { get; }

        /// <summary>
        /// Computes the x and y offset for the specified child <see cref="UIElement"/>
        /// </summary>
        /// <param name="child">The child for which to compute the offset</param>
        /// <returns>A <see cref="Point"/> representing the x and y offset of the specified child <see cref="UIElement"/></returns>
        Point ComputeChildOffset(UIElement child);

        /// <summary>
        /// Measures the <see cref="IContentPresenter"/>'s contents
        /// </summary>
        /// <returns>The <see cref="Media.Size"/> of the <see cref="IContentPresenter"/>'s contents</returns>
        Size MeasureContents();

    }

}
