using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Controls
{

    /// <summary>
    /// Defines methods that provide additional information about the layout state of an element
    /// </summary>
    public static class LayoutInformation
    {

        /// <summary>
        /// Returns a <see cref="Media.Geometry"/> that represents the visible region of a <see cref="UIElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to determine the visible region of</param>
        /// <returns>A <see cref="Media.Geometry"/> that represents the visible region of a <see cref="UIElement"/>.</returns>
        public static Media.Geometry GetLayoutClip(UIElement element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a <see cref="Media.Rectangle"/> that represents the layout partition that is reserved for a child element<para></para>
        /// It is equivalent to the rectangle returned by the <see cref="IUIElement.RenderTarget"/> property, modified to include eventual padding values
        /// </summary>
        /// <param name="element">The <see cref="IUIElement"/> instance to compute the layout slot of</param>
        /// <returns>A <see cref="Media.Rectangle"/> that represents the layout partition that is reserved for a child element</returns>
        public static Media.Rectangle GetLayoutSlot(IUIElement element)
        {
            IPaddedElement paddedElement;
            Media.Point layoutSlotPosition;
            Media.Size layoutSlotSize;
            Media.Rectangle layoutSlot;
            layoutSlotPosition = element.RenderTarget.Position;
            layoutSlotSize = element.RenderTarget.Size;
            if (typeof(IPaddedElement).IsAssignableFrom(element.GetType()))
            {
                paddedElement = (IPaddedElement)element;
                layoutSlotPosition += new Media.Point(paddedElement.Padding.Left, paddedElement.Padding.Top);
                layoutSlotSize = new Media.Size(layoutSlotSize.Width - paddedElement.Padding.Left - paddedElement.Padding.Right, layoutSlotSize.Height - paddedElement.Padding.Top - paddedElement.Padding.Bottom );
            }
            layoutSlot = new Media.Rectangle(layoutSlotPosition, layoutSlotSize);
            return layoutSlot;
        }

    }

}
