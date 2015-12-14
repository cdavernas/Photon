using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Threading
{

    /// <summary>
    /// Represents an object that is associated with a <see cref="Threading.Dispatcher"/>
    /// </summary>
    public abstract class DispatcherObject
    {

        /// <summary>
        /// Initializes a new <see cref="DispatcherObject"/> associated with a new <see cref="Threading.Dispatcher"/>
        /// </summary>
        protected DispatcherObject()
        {
            if(this.Dispatcher == null)
            {
                this.Dispatcher = new Dispatcher();
            }
        }

        /// <summary>
        /// Gets the <see cref="Threading.Dispatcher"/> associated with the <see cref="DispatcherObject"/>
        /// </summary>
        public Dispatcher Dispatcher
        {
            get
            {
                DispatcherContext dispatcherContext;
                dispatcherContext = DispatcherContext.Current;
                if (dispatcherContext == null)
                {
                    return null;
                }
                return dispatcherContext.Owner;
            }
            private set { }
        }

    }

}
