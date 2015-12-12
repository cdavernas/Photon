using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Photon.Threading
{

    /// <summary>
    /// The <see cref="SynchronizationContext"/> used by the <see cref="Dispatcher"/> to dispatch operations.<para></para> 
    /// It contains a <see cref="PriorityQueue{TValue}"/> of <see cref="DispatcherOperation"/>s representing the work items to dispatch
    /// </summary>
    public sealed class DispatcherContext
        : SynchronizationContext, IDisposable
    {

        /// <summary>
        /// The <see cref="Dispatcher"/>'s working <see cref="Thread"/>
        /// </summary>
        private Thread _WorkingThread;
        /// <summary>
        /// The <see cref="PriorityQueue{TValue}"/> of <see cref="DispatcherOperation"/>s queued in the <see cref="DispatcherContext"/>
        /// </summary>
        private PriorityQueue<DispatcherOperation> _OperationsQueue
            = new PriorityQueue<DispatcherOperation>((operation) => { return (int)operation.Priority; }, (priority1, priority2) => { return priority1 - priority2; });

        /// <summary>
        /// Initializes a new <see cref="DispatcherContext"/> synchronized with the current <see cref="Thread"/><para></para>
        /// When using this constructor, the <see cref="DispatcherContext.DispatchOperations"/> method will have to be explicitly called by the <see cref="Thread"/> it is synchronized with
        /// </summary>
        public DispatcherContext()
        {
            this._WorkingThread = Thread.CurrentThread;
        }

        /// <summary>
        /// Initialies a new <see cref="DispatcherContext"/> synchronized with a new <see cref="Thread"/> instance
        /// </summary>
        /// <param name="threadApartmentState">The <see cref="ApartmentState"/> of the <see cref="Dispatcher"/>'s <see cref="Thread"/></param>
        public DispatcherContext(ApartmentState threadApartmentState)
        {
            Thread dispatcherThread;
            dispatcherThread = new Thread(new ParameterizedThreadStart(this.ExecuteOperations));
            dispatcherThread.SetApartmentState(threadApartmentState);
        }

        /// <summary>
        /// Initialies a new <see cref="DispatcherContext"/> synchronized based on the specified <see cref="DispatcherContext"/> instance
        /// </summary>
        /// <param name="sourceContext">The source <see cref="DispatcherContext"/> the <see cref="DispatcherContext"/> to create is based on</param>
        public DispatcherContext(DispatcherContext sourceContext)
        {
            this._WorkingThread = sourceContext._WorkingThread;
        }

        /// <summary>
        /// When overridden in a derived class, dispatches a synchronous message to a <see cref="SynchronizationContext"/>
        /// </summary>
        /// <param name="callback">The <see cref="SendOrPostCallback"/> to dispatch</param>
        /// <param name="state">The state argument of the specified <see cref="SendOrPostCallback"/>, that is a <see cref="DispatcherOperation"/></param>
        public override void Send(SendOrPostCallback callback, object state)
        {
            DispatcherOperation operation;
            operation = (DispatcherOperation)state;
            using (operation.HandledEvent = new ManualResetEvent(false))
            {
                this.Post(DispatcherContext.ExecuteOperation, operation);
                operation.HandledEvent.WaitOne();
            }
        }

        /// <summary>
        /// When overridden in a derived class, dispatches an asynchronous message to a <see cref="SynchronizationContext"/>
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public override void Post(SendOrPostCallback callback, object state)
        {
            DispatcherOperation dispatcherOperation;
            if (state == null)
            {
                throw new ArgumentNullException("state");
            }
            if (!typeof(DispatcherOperation).IsAssignableFrom(state.GetType()))
            {
                throw new NotSupportedException("The DispatcherContext.Post method only supports DispatcherOperation instances as state object");
            }
            dispatcherOperation = (DispatcherOperation)state;
            this._OperationsQueue.Enqueue(dispatcherOperation);
        }

        /// <summary>
        /// Executes all the <see cref="DispatcherOperation"/>s residing in the <see cref="DispatcherOperation"/>'s <see cref="PriorityQueue{TValue}"/>
        /// </summary>
        /// <param name="state">The state of the method call, that is the current <see cref="DispatcherContext"/></param>
        public void ExecuteOperations(object state)
        {
            DispatcherOperation dispatcherOperation;
            SynchronizationContext.SetSynchronizationContext((SynchronizationContext)state);
            try
            {
                while (this._OperationsQueue.TryToDequeue(out dispatcherOperation))
                {
                    dispatcherOperation.Execute();
                }
            }
            catch (ObjectDisposedException) { }
        }

        /// <summary>
        /// Disposes of the <see cref="DispatcherContext"/> and underlying components
        /// </summary>
        public void Dispose()
        {
            this._OperationsQueue = null;
        }

        /// <summary>
        /// Executes the specified <see cref="DispatcherOperation"/>
        /// </summary>
        /// <param name="state">The state associated with the <see cref="DispatcherContext.ExecuteOperation(object)"/> method call, that is a <see cref="DispatcherOperation"/></param>
        internal static void ExecuteOperation(object state)
        {
            DispatcherOperation operation;
            if(state == null)
            {
                throw new ArgumentNullException("state");
            }
            if (typeof(DispatcherOperation).IsAssignableFrom(state.GetType()))
            {
                throw new NotSupportedException("The DispatcherContext.ExecuteOperation(state) only supports DispactherOperation as 'state' argument");
            }
            operation = (DispatcherOperation)state;
            try
            {
                operation.Execute();
            }
            finally
            {
                if (operation.IsHandled)
                {
                    operation.HandledEvent.Set();
                }
            }
        }

    }

}
