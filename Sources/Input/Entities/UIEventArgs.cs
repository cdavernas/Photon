using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Input
{

    /// <summary>
    /// Represents the <see cref="EventArgs"/> associated with the <see cref="UIElement.ProcessUIEvent(UIEventArgs)"/> method
    /// </summary>
    public class UIEventArgs
        : EventArgs
    {

        /// <summary>
        /// Initializes the <see cref="UIEventArgs"/> with the specified <see cref="UIEvent"/> and source <see cref="EventArgs"/>
        /// </summary>
        /// <param name="sourceEvent">The <see cref="UIEvent"/> for which to create the <see cref="UIEventArgs"/></param>
        /// <param name="sourceEventArgs">The <see cref="EventArgs"/> associated with the triggering event</param>
        public UIEventArgs(UIEvent sourceEvent, EventArgs sourceEventArgs)
        {
            this.SourceEvent = sourceEvent;
            this.SourceEventArgs = sourceEventArgs;
        }

        /// <summary>
        /// Gets the <see cref="UIEvent"/> for which to create the <see cref="UIEventArgs"/>
        /// </summary>
        public UIEvent SourceEvent { get; private set; }

        /// <summary>
        /// Gets the <see cref="EventArgs"/> associated with the triggering event
        /// </summary>
        public EventArgs SourceEventArgs{ get; private set; }
        
        /// <summary>
        /// Gets/Sets a boolean indicating whether or not the <see cref="UIEventArgs"/> has been handled
        /// </summary>
        public bool IsHandled { get; set; }

    }

}
