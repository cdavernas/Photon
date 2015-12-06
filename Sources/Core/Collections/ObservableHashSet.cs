using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents a <see cref="HashSet{T}"/> that notifies changes thanks to the <see cref="INotifyCollectionChanged"/> interface
    /// </summary>
    /// <typeparam name="TElement">The <see cref="HashSet{T}"/>'s element type</typeparam>
    public class ObservableHashSet<TElement>
        : HashSet<TElement>, INotifyCollectionChanged
    {

        /// <summary>
        /// This event is fired whenever a change occurs in the <see cref="HashSet{T}"/>
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Tries to add the specified element into the hashset, and returns a boolean indicating the operation's success
        /// </summary>
        /// <param name="element">The element to add</param>
        /// <returns>A boolean indicating whether or not the specified element could be added to the hashset</returns>
        public new bool Add(TElement element)
        {
            bool success;
            success = base.Add(element);
            if (success)
            {
                if(this.CollectionChanged != null)
                {
                    this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, element));
                }
            }
            return success;
        }

        /// <summary>
        /// Tries to remove the specified element from the hashset, and returns a boolean indicating the operation's success
        /// </summary>
        /// <param name="element">The element to remove</param>
        /// <returns>A boolean indicating whether or not the specified element could be removed from the hashset</returns>
        public new bool Remove(TElement element)
        {
            bool success;
            success = base.Remove(element);
            if (success)
            {
                if (this.CollectionChanged != null)
                {
                    this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, element));
                }
            }
            return success;
        }

    }

}
