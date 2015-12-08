using Photon.Media;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// UIElement is a base interface for core level implementations building on Photon elements and basic presentation characteristics.
    /// </summary>
    public interface IUIElement
        : IDependencyElement
    {

        /// <summary>
        /// This event is fired every time the mouse moves over the element
        /// </summary>
        event EventHandler<MouseMoveEventArgs> MouseMove;
        /// <summary>
        /// This event is fired every time a mouse button is down over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseButtonDown;
        /// <summary>
        /// This event is fired every time a mouse button is up over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseButtonUp;
        /// <summary>
        /// This event is fired every time the left mouse button is down over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseLeftButtonDown;
        /// <summary>
        /// This event is fired every time the left mouse button is up over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseLeftButtonUp;
        /// <summary>
        /// This event is fired every time the middle mouse button is down over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseMiddleButtonDown;
        /// <summary>
        /// This event is fired every time the middle mouse button is up over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseMiddleButtonUp;
        /// <summary>
        /// This event is fired every time the right mouse button is down over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseRightButtonDown;
        /// <summary>
        /// This event is fired every time the right mouse button is up over the element
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseRightButtonUp;
        /// <summary>
        /// This event is fired every time the mouse wheel is used over the element
        /// </summary>
        event EventHandler<MouseWheelEventArgs> MouseWheel;
        /// <summary>
        /// This event is fired every time a key is down over the element
        /// </summary>
        event EventHandler<KeyboardKeyEventArgs> KeyDown;
        /// <summary>
        /// This event is fired every time a key is up over the element
        /// </summary>
        event EventHandler<KeyboardKeyEventArgs> KeyUp;
        /// <summary>
        /// This event is fired every time a key is pressed over the element
        /// </summary>
        event EventHandler<KeyPressEventArgs> KeyPressed;
        /// <summary>
        /// This event is fired every time the element's visibility has changed
        /// </summary>
        event EventHandler VisibilityChanged;
        /// <summary>
        /// This event is fired every time the element gets focus
        /// </summary>
        event EventHandler GotFocus;
        /// <summary>
        /// This event is fired every time the element looses focus
        /// </summary>
        event EventHandler LostFocus;

        /// <summary>
        /// Gets/Sets the element's width
        /// </summary>
        double? Width { get; set; }

        /// <summary>
        /// Gets/Sets the element's height
        /// </summary>
        double? Height { get; set; }

        /// <summary>
        /// Gets/Sets a value indicating whether or not the element is hit test visible
        /// </summary>
        bool IsHitTestVisible { get; set; }

        /// <summary>
        /// Gets a <see cref="Media.Rectangle"/> representing the element's layout target
        /// </summary>
        Rectangle LayoutTarget { get; }

        /// <summary>
        /// Gets a <see cref="Media.Rectangle"/> representing the element's render target
        /// </summary>
        Rectangle RenderTarget { get; }

        /// <summary>
        /// This method is fired whenever the element's visual has been invalidated
        /// </summary>
        void InvalidateVisual();

        /// <summary>
        /// This method is fired whenever the element's layout has been invalidated
        /// </summary>
        void InvalidateLayout();

        /// <summary>
        /// This method is used to process any ui event handled by the element's logicial parent
        /// </summary>
        /// <param name="e">The <see cref="Input.UIEventArgs"/> associated with the event</param>
        void ProcessUIEvent(Input.UIEventArgs e);

    }

}
