using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="HashSet{T}"/> type
    /// </summary>
    public static class HashSetExtensions
    {

        /// <summary>
        /// Adds a range of elements to the hashset
        /// </summary>
        /// <typeparam name="TElement">The generic type of the extended <see cref="HashSet{T}"/></typeparam>
        /// <param name="extended">The extended <see cref="HashSet{T}"/></param>
        /// <param name="range">The <see cref="IEnumerable{T}"/> to add to the hashset</param>
        public static void AddRange<TElement>(this HashSet<TElement> extended, IEnumerable<TElement> range)
        {
            foreach (TElement element in range)
            {
                extended.Add(element);
            }
        }

        /// <summary>
        /// Adds a range of elements to the hashset
        /// </summary>
        /// <typeparam name="TElement">The generic type of the extended <see cref="HashSet{T}"/></typeparam>
        /// <param name="extended">The extended <see cref="HashSet{T}"/></param>
        /// <param name="range">The <see cref="IEnumerable{T}"/> to add to the hashset</param>
        public static void AddRange<XmlNode>(this HashSet<XmlNode> extended, XmlNodeList range)
        {
            foreach (XmlNode node in range)
            {
                extended.Add(node);
            }
        }

        /// <summary>
        /// Determines the index of the specified element within the hashset
        /// </summary>
        /// <typeparam name="TElement">The generic type of the extended <see cref="HashSet{T}"/></typeparam>
        /// <param name="extended">The extended <see cref="HashSet{T}"/></param>
        /// <param name="element">The element whose index is to return</param>
        /// <returns>An integer representing the index of the specified element</returns>
        public static int IndexOf<TElement>(this HashSet<TElement> extended, TElement element)
        {
            for (int index = 0; index < extended.Count; index++)
            {
                if (extended.ElementAt(index).Equals(element))
                {
                    return index;
                }
            }
            throw new Exception("The HashSet does not contain the specified element");
        }

    }

}
