using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Provides a dictionary implementation that contains Photon resources used by components and other elements of a Photon application
    /// </summary>
    public class ResourceDictionary
        : IDictionary<string, object>, Markup.IAddChild
    {

        /// <summary>
        /// Initializes a new <see cref="ResourceDictionary"/>
        /// </summary>
        public ResourceDictionary()
        {
            this._Resources = new Dictionary<string, object>();
            this.MergedDictionaries = new Collection<ResourceDictionary>();
        }

        /// <summary>
        /// The dictionary containing all locally registered resources
        /// </summary>
        private Dictionary<string, object> _Resources;

        /// <summary>
        /// Gets/sets the value for the specified key
        /// </summary>
        /// <param name="key">The key of the value to get/set</param>
        /// <returns>An object representing the value for the specified key</returns>
        public object this[string key]
        {
            get
            {
                object result;
                if(this._Resources.TryGetValue(key, out result))
                {
                    return result;
                }
                foreach(ResourceDictionary resourceDictionary in this.MergedDictionaries)
                {
                    if (resourceDictionary.TryGetValue(key, out result))
                    {
                        return result;
                    }
                }
                throw new KeyNotFoundException("The specified key '" + key + "' does not exist in the ResourceDictionary");
            }
            set
            {
                this._Resources.Add(key, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="ResourceDictionary"/>'s element count
        /// </summary>
        public int Count
        {
            get
            {
                return this._Resources.Count;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="ResourceDictionary"/> is read-only
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a collection of the keys contained by the <see cref="ResourceDictionary"/>
        /// </summary>
        public ICollection<string> Keys
        {
            get
            {
                return this._Resources.Keys;
            }
        }

        /// <summary>
        /// Gets a collection of the values contained by the <see cref="ResourceDictionary"/>
        /// </summary>
        public ICollection<object> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a collection of the <see cref="ResourceDictionary"/> instances contained by the <see cref="ResourceDictionary"/>
        /// </summary>
        public Collection<ResourceDictionary> MergedDictionaries { get; private set; }

        /// <summary>
        /// Returns a boolean indicating whether or not the <see cref="ResourceDictionary"/> contains the specified key
        /// </summary>
        /// <param name="key">The key to check for</param>
        /// <returns>A boolean indicating whether or not the <see cref="ResourceDictionary"/> contains the specified key</returns>
        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the <see cref="ResourceDictionary"/> contains the specified item
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/> to check for existence</param>
        /// <returns>A boolean indicating whether or not the <see cref="ResourceDictionary"/> contains the specified item</returns>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.ContainsKey(item.Key);
        }

        /// <summary>
        /// Adds the specified key and value to the <see cref="ResourceDictionary"/>
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value associated with the key</param>
        public void Add(string key, object value)
        {
            this._Resources.Add(key, value);
        }

        /// <summary>
        /// Adds the specified item to the <see cref="ResourceDictionary"/>
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/> to add</param>
        public void Add(KeyValuePair<string, object> item)
        {
            this._Resources.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes the item with the specified key from the <see cref="ResourceDictionary"/>
        /// </summary>
        /// <param name="key">The key of the item to remove</param>
        /// <returns>A boolean indicating whether or not the item with the specified key could be removed</returns>
        public bool Remove(string key)
        {
            return this._Resources.Remove(key);
        }

        /// <summary>
        /// Removes the specified item from the <see cref="ResourceDictionary"/>
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/> to remove</param>
        /// <returns>A boolean indicating whether or not the specified item could be removed</returns>
        public bool Remove(KeyValuePair<string, object> item)
        {
            return this._Resources.Remove(item.Key);
        }

        /// <summary>
        /// Clears the <see cref="ResourceDictionary"/> of its contents
        /// </summary>
        public void Clear()
        {
            this._Resources.Clear();
        }

        /// <summary>
        /// Copies the contents of the <see cref="ResourceDictionary"/> into the specified <see cref="KeyValuePair{TKey, TValue}"/> array, starting at the specified index
        /// </summary>
        /// <param name="array">The <see cref="KeyValuePair{TKey, TValue}"/> array into which to copy the <see cref="ResourceDictionary"/></param>
        /// <param name="arrayIndex">The index starting at which to start copying from the <see cref="ResourceDictionary"/>'s contents</param>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            for(int index = 0; index < this._Resources.Count; index++)
            {
                array[index + arrayIndex] = this._Resources.ElementAt(index);
            }
        }

        /// <summary>
        /// Returns an enumerator for the <see cref="ResourceDictionary"/> contents
        /// </summary>
        /// <returns>An enumerator for the <see cref="ResourceDictionary"/> contents</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this._Resources.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator for the <see cref="ResourceDictionary"/> contents
        /// </summary>
        /// <returns>An enumerator for the <see cref="ResourceDictionary"/> contents</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Attempt to retrieve the value associated with the specified key and return a boolean indicating whether or not the attempt was successfull
        /// </summary>
        /// <param name="key">The key associated with the value to retrieve</param>
        /// <param name="value">The value associated with the specified key, if the key exists</param>
        /// <returns>A boolean indicating whether or not the attempt was successfull</returns>
        public bool TryGetValue(string key, out object value)
        {
            return this._Resources.TryGetValue(key, out value);
        }

        /// <summary>
        /// Adds the specified child object
        /// </summary>
        /// <param name="child">An object representing the child to add</param>
        public void AddChild(object child)
        {
            if (typeof(ResourceDictionary).IsAssignableFrom(child.GetType()))
            {
                this.MergedDictionaries.Add((ResourceDictionary)child);
                return;
            }
            string key;
            if(!Markup.Context.Current.Handler.ElementKeys.TryGetValue(child.GetHashCode(), out key))
            {
                throw new KeyNotFoundException("The key '" + key + "' could not be found in the ResourceDictionary");
            }
            this._Resources.Add(key, child);
        }

        /// <summary>
        /// Adds the specified text content
        /// </summary>
        /// <param name="text">A string representing the text to add</param>
        public void AddText(string text)
        {
            throw new NotSupportedException("An element of type '" + this.GetType().FullName + "' does not support direct text content");
        }

    }

}
