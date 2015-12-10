using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents the base class for specifying a conditional value within a <see cref="Style"/> object.
    /// </summary>
    public abstract class TriggerBase
    {

        /// <summary>
        /// Initializes a new <see cref="TriggerBase"/> instance
        /// </summary>
        protected TriggerBase()
        {
            this.EnterActions = new TriggerActionCollection();
        }

        /// <summary>
        /// Gets a collection of <see cref="TriggerAction"/> objects to apply when the trigger object becomes active. This property does not apply to the <see cref="EventTrigger"/> class
        /// </summary>
        public TriggerActionCollection EnterActions { get; private set; }

        /// <summary>
        /// Gets the collection of actions to apply when the event occurs
        /// </summary>
        public TriggerActionCollection Actions { get; private set; }

        /// <summary>
        /// Gets a collection of <see cref="TriggerAction"/> objects to apply when the trigger object becomes inactive. This property does not apply to the <see cref="EventTrigger"/> class
        /// </summary>
        public TriggerActionCollection ExitActions { get; private set; }

    }

}
