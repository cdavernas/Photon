using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents an event setter in a <see cref="Style"/>. Event setters invoke the specified event handlers in response to events
    /// </summary>
    public class EventSetter
        : SetterBase
    {

        /// <summary>
        /// Initializes a new <see cref="EventSetter"/> instance
        /// </summary>
        public EventSetter()
        {

        }

        /// <summary>
        /// Gets or sets the particular <see cref="RoutedEvent"/> that the <see cref="EventSetter"/> responds to
        /// </summary>
        public RoutedEvent Event { get; set; }

        /// <summary>
        /// Gets or sets the reference to a handler for a routed event in the <see cref="Setter"/>
        /// </summary>
        public Delegate Handler { get; set; }

        /// <summary>
        /// Applies the <see cref="EventSetter"/> to the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to apply the <see cref="EventSetter"/> to</param>
        internal override void Set(DependencyObject dependencyObject)
        {
            EventInfo eventInfo;
            eventInfo = dependencyObject.GetType().GetEvent(this.Event.Name);
            if(eventInfo == null)
            {
                throw new MissingMemberException("The specified event '" + this.Event.Name + "' does not exist or could not be found in the type '" + dependencyObject.GetType().FullName + "'");
            }
            eventInfo.AddEventHandler(dependencyObject, this.Handler);
        }

    }

}
