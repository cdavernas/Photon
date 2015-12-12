using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Photon.Threading
{

    /// <summary>
    /// Provides services for managing the queue of work items for a <see cref="Thread"/>
    /// </summary>
    public sealed class Dispatcher
    {

        private static DispatcherContext ActiveContext;

        /// <summary>
        /// Initializes a new <see cref="Dispatcher"/>
        /// </summary>
        public Dispatcher()
        {
            Dispatcher.ActiveContext = new DispatcherContext();
            SynchronizationContext.SetSynchronizationContext(Dispatcher.ActiveContext);
        }

        /// <summary>
        /// Gets the <see cref="DispatcherContext"/> associated with the <see cref="Dispatcher"/>
        /// </summary>
        public DispatcherContext Context
        {
            get
            {
                return (DispatcherContext)SynchronizationContext.Current;
            }
        }

        /// <summary>
        /// Executes the specified Action synchronously on the thread the <see cref="Dispatcher"/> is associated with
        /// </summary>
        /// <param name="delegateMethod">The <see cref="Delegate"/> to execute</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="args">An array of object containg the ordered parameters of the <see cref="Action"/> to execute</param>
        public void Invoke(Delegate delegateMethod, DispatcherPriority priority, params object[] args)
        {
            if (this.Context == null)
            {
                SynchronizationContext.SetSynchronizationContext(Dispatcher.ActiveContext);
            }
            this.Context.Send(new SendOrPostCallback(DispatcherContext.ExecuteOperation), new DispatcherOperation(priority, delegateMethod, args));
        }

        /// <summary>
        /// Executes the specified <see cref="Action"/> asynchronously with the specified arguments, at the specified <see cref="DispatcherPriority"/>, on the thread that the <see cref="Dispatcher"/> was created on
        /// </summary>
        /// <param name="delegateMethod">The <see cref="Delegate"/> to execute</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="args">An array of object containg the ordered parameters of the <see cref="Action"/> to execute</param>
        public void BeginInvoke(Delegate delegateMethod, DispatcherPriority priority, params object[] args)
        {
            if(this.Context == null)
            {
                SynchronizationContext.SetSynchronizationContext(Dispatcher.ActiveContext);
            }
            this.Context.Post(new SendOrPostCallback(DispatcherContext.ExecuteOperation), new DispatcherOperation(priority, delegateMethod, args));
        }

    }

}
