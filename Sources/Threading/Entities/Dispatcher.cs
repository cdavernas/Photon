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

        /// <summary>
        /// The active <see cref="DispatcherContext"/>
        /// </summary>
        internal static DispatcherContext ActiveContext;

        /// <summary>
        /// Initializes a new <see cref="Dispatcher"/>
        /// </summary>
        public Dispatcher()
        {
            Dispatcher.ActiveContext = new DispatcherContext(this);
            SynchronizationContext.SetSynchronizationContext(Dispatcher.ActiveContext);
        }

        /// <summary>
        /// Gets the <see cref="DispatcherContext"/> associated with the <see cref="Dispatcher"/>
        /// </summary>
        public DispatcherContext Context
        {
            get
            {
                return DispatcherContext.Current;
            }
        }

        /// <summary>
        /// Executes the specified <see cref="Delegate"/> synchronously on the thread the <see cref="Dispatcher"/> is associated with
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Delegate"/></param>
        /// <param name="delegateMethod">The <see cref="Delegate"/> to execute</param>
        /// <param name="args">An array of object containg the ordered parameters of the <see cref="Delegate"/> to execute</param>
        public void Invoke(DispatcherPriority priority, Delegate delegateMethod, params object[] args)
        {
            if (this.Context == null)
            {
                SynchronizationContext.SetSynchronizationContext(Dispatcher.ActiveContext);
            }
            this.Context.Send(new SendOrPostCallback(DispatcherContext.ExecuteOperation), new DispatcherOperation(priority, delegateMethod, args));
        }

        /// <summary>
        /// Executes the specified <see cref="Action"/> synchronously on the thread the <see cref="Dispatcher"/> is associated with
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Delegate"/> to execute</param>
        public void Invoke(DispatcherPriority priority, Action action)
        {
            this.Invoke(priority, action, null);
        }

        /// <summary>
        /// Executes the specified <see cref="Action"/> synchronously on the thread the <see cref="Dispatcher"/> is associated with
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Delegate"/> to execute</param>
        /// <param name="argument">The <see cref="Action{T}"/>'s argument</param>
        /// <typeparam name="T">The type of the <see cref="Action{T}"/> parameter</typeparam>
        public void Invoke<T>(DispatcherPriority priority, Action<T> action, T argument)
        {
            this.Invoke(priority, action, new object[] { argument });
        }

        /// <summary>
        /// Executes the specified <see cref="Action{T1, T2}"/> synchronously on the thread the <see cref="Dispatcher"/> is associated with
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Delegate"/> to execute</param>
        /// <param name="argument1">The <see cref="Action{T1, T2}"/>'s first argument</param>
        /// <param name="argument2">The <see cref="Action{T1, T2}"/>'s second argument</param>
        /// <typeparam name="T1">The type of the <see cref="Action{T1, T2}"/>'s first parameter</typeparam>
        /// <typeparam name="T2">The type of the <see cref="Action{T1, T2}"/>'s second parameter</typeparam>
        public void Invoke<T1, T2>(DispatcherPriority priority, Action<T1, T2> action, T1 argument1, T2 argument2)
        {
            this.Invoke(priority, action, new object[] { argument1, argument2 });
        }

        /// <summary>
        /// Executes the specified <see cref="Action{T1, T2, T3}"/> synchronously on the thread the <see cref="Dispatcher"/> is associated with
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Delegate"/> to execute</param>
        /// <param name="argument1">The <see cref="Action{T1, T2, T3}"/>'s first argument</param>
        /// <param name="argument2">The <see cref="Action{T1, T2, T3}"/>'s second argument</param>
        /// <param name="argument3">The <see cref="Action{T1, T2, T3}"/>'s third argument</param>
        /// <typeparam name="T1">The type of the <see cref="Action{T1, T2, T3}"/>'s first parameter</typeparam>
        /// <typeparam name="T2">The type of the <see cref="Action{T1, T2, T3}"/>'s second parameter</typeparam>
        /// <typeparam name="T3">The type of the <see cref="Action{T1, T2, T3}"/>'s third parameter</typeparam>
        public void Invoke<T1, T2, T3>(DispatcherPriority priority, Action<T1, T2, T3> action, T1 argument1, T2 argument2, T3 argument3)
        {
            this.Invoke(priority, action, new object[] { argument1, argument2, argument3 });
        }

        /// <summary>
        /// Executes the specified <see cref="Delegate"/> asynchronously with the specified arguments, at the specified <see cref="DispatcherPriority"/>, on the thread that the <see cref="Dispatcher"/> was created on
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Delegate"/></param>
        /// <param name="delegateMethod">The <see cref="Delegate"/> to execute</param>
        /// <param name="args">An array of object containg the ordered parameters of the <see cref="Delegate"/> to execute</param>
        public void BeginInvoke(DispatcherPriority priority, Delegate delegateMethod, params object[] args)
        {
            if(this.Context == null)
            {
                SynchronizationContext.SetSynchronizationContext(Dispatcher.ActiveContext);
            }
            this.Context.Post(new SendOrPostCallback(DispatcherContext.ExecuteOperation), new DispatcherOperation(priority, delegateMethod, args));
        }

        /// <summary>
        /// Executes the specified <see cref="Action"/> asynchronously at the specified <see cref="DispatcherPriority"/>, on the thread that the <see cref="Dispatcher"/> was created on
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Action"/> to execute</param>
        public void BeginInvoke(DispatcherPriority priority, Action action)
        {
            this.BeginInvoke(priority, action, null);
        }

        /// <summary>
        /// Executes the specified <see cref="Action{T}"/> asynchronously at the specified <see cref="DispatcherPriority"/>, on the thread that the <see cref="Dispatcher"/> was created on
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Action{T}"/> to execute</param>
        /// <param name="argument">The <see cref="Action{T}"/>'s argument</param>
        /// <typeparam name="T">The type of the <see cref="Action{T}"/>'s parameter</typeparam>
        public void BeginInvoke<T>(DispatcherPriority priority, Action<T> action, T argument)
        {
            this.BeginInvoke(priority, action, new object[] { argument });
        }

        /// <summary>
        /// Executes the specified <see cref="Action{T1, T2}"/> asynchronously at the specified <see cref="DispatcherPriority"/>, on the thread that the <see cref="Dispatcher"/> was created on
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Action{T1, T2}"/> to execute</param>
        /// <param name="argument1">The <see cref="Action{T1, T2}"/>'s first argument</param>
        /// <param name="argument2">The <see cref="Action{T1, T2}"/>'s second argument</param>
        /// <typeparam name="T1">The type of the <see cref="Action{T1, T2}"/>'s first parameter</typeparam>
        /// <typeparam name="T2">The type of the <see cref="Action{T1, T2}"/>'s second parameter</typeparam>
        public void BeginInvoke<T1, T2>(DispatcherPriority priority, Action<T1, T2> action, T1 argument1, T2 argument2)
        {
            this.BeginInvoke(priority, action, new object[] { argument1, argument2 });
        }

        /// <summary>
        /// Executes the specified <see cref="Action{T1, T2, T3}"/> asynchronously at the specified <see cref="DispatcherPriority"/>, on the thread that the <see cref="Dispatcher"/> was created on
        /// </summary>
        /// <param name="priority">The <see cref="DispatcherPriority"/> at which to execute the specified <see cref="Action"/></param>
        /// <param name="action">The <see cref="Action{T1, T2, T3}"/> to execute</param>
        /// <param name="argument1">The <see cref="Action{T1, T2, T3}"/>'s first argument</param>
        /// <param name="argument2">The <see cref="Action{T1, T2, T3}"/>'s second argument</param>
        /// <param name="argument3">The <see cref="Action{T1, T2, T3}"/>'s third argument</param>
        /// <typeparam name="T1">The type of the <see cref="Action{T1, T2, T3}"/>'s first parameter</typeparam>
        /// <typeparam name="T2">The type of the <see cref="Action{T1, T2, T3}"/>'s second parameter</typeparam>
        /// <typeparam name="T3">The type of the <see cref="Action{T1, T2, T3}"/>'s third parameter</typeparam>
        public void BeginInvoke<T1, T2, T3>(DispatcherPriority priority, Action<T1, T2, T3> action, T1 argument1, T2 argument2, T3 argument3)
        {
            this.BeginInvoke(priority, action, new object[] { argument1, argument2, argument3 });
        }

    }

}
