using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents a trigger that applies a set of actions in response to an event
    /// </summary>
    public class EventTrigger
        : TriggerBase
    {

        /// <summary>
        /// Initializes a new <see cref="EventTrigger"/> instance
        /// </summary>
        public EventTrigger()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="EventTrigger"/> for the specified <see cref="RoutedEvent"/>
        /// </summary>
        /// <param name="routedEvent">The <see cref="RoutedEvent"/> that will activate the trigger</param>
        public EventTrigger(RoutedEvent routedEvent)
        {

        }

        /// <summary>
        /// Gets or sets the <see cref="RoutedEvent"/> that will activate the trigger
        /// </summary>
        public RoutedEvent RoutedEvent { get; set; }

    }

}
