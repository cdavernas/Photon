using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon.Markup
{

    /// <summary>
    /// Defines extensions methods for the <see cref="XmlNodeList"/> type
    /// </summary>
    public static class XmlNodeListExtensions
    {

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of <see cref="XmlNode"/> matching the specified predicate
        /// </summary>
        /// <param name="extended">The extended <see cref="IEnumerable{T}"/> of <see cref="XmlNode"/></param>
        /// <param name="predicate">The predicate to match</param>
        /// <returns></returns>
        public static IEnumerable<XmlNode> Where(this XmlNodeList extended, Func<XmlNode, bool> predicate)
        {
            HashSet<XmlNode> nodes;
            nodes = new HashSet<XmlNode>();
            foreach(XmlNode node in extended)
            {
                if (predicate.Invoke(node))
                {
                    nodes.Add(node);
                }
            }
            return nodes;
        }

        /// <summary>
        /// Returns the first <see cref="XmlNode"/> that matches the specified predicate or returns null
        /// </summary>
        /// <param name="extended">The extended <see cref="XmlNode"/></param>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>The first <see cref="XmlNode"/> that matches the specified predicate or returns null</returns>
        public static XmlNode FirstOrDefault(this XmlNodeList extended, Func<XmlNode, bool> predicate)
        {
            foreach (XmlNode node in extended)
            {
                if (predicate.Invoke(node))
                {
                    return node;
                }
            }
            return null;
        }

    }

}
