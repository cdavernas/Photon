using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Photon.Threading
{

    /// <summary>
    /// Represents an object that is used to interact with an operation that has been posted to the <see cref="Dispatcher"/> queue
    /// </summary>
    public sealed class DispatcherOperation
    {

        /// <summary>
        /// Initializes a new <see cref="DispatcherOperation"/> with the specified <see cref="DispatcherPriority"/>, callback delegate and callback arguments
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the <see cref="DispatcherOperation"/></param>
        /// <param name="callback">The <see cref="Delegate"/> to execute</param>
        /// <param name="arguments">An array of objects representing the ordered arguments of the callback to invoke</param>
        public DispatcherOperation(DispatcherPriority priority, Delegate callback, params object[] arguments)
        {
            this.Priority = priority;
            this.Callback = callback;
            this.Arguments = arguments;
        }

        /// <summary>
        /// Gets the <see cref="DispatcherPriority"/> at which to execute the <see cref="DispatcherOperation"/>
        /// </summary>
        public DispatcherPriority Priority { get; private set; }

        /// <summary>
        /// Gets the <see cref="Delegate"/> to execute
        /// </summary>
        public Delegate Callback { get; private set; }

        /// <summary>
        /// Gets an array of objects representing the ordered arguments of the callback to invoke
        /// </summary>
        public object[] Arguments { get; private set; }

        /// <summary>
        /// Gets/sets the <see cref="ManualResetEvent"/> associated with the <see cref="DispatcherOperation"/>
        /// </summary>
        internal ManualResetEvent HandledEvent { get; set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="DispatcherOperation"/> is handled thanks to a <see cref="ManualResetEvent"/>
        /// </summary>
        internal bool IsHandled
        {
            get
            {
                if (this.HandledEvent == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Executes the <see cref="DispatcherOperation"/>
        /// </summary>
        internal void Execute()
        {
            this.Callback.DynamicInvoke(this.Arguments);
            if (this.IsHandled)
            {
                this.HandledEvent.Set();
            }
        }

    }

}
