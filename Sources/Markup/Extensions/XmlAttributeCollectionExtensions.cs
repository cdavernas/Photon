using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon.Markup
{

    /// <summary>
    /// Defines extensions methods for the <see cref="XmlAttributeCollection"/> type
    /// </summary>
    public static class XmlAttributeCollectionExtensions
    {

        /// <summary>
        /// Returns the first <see cref="XmlAttribute"/> matching the specified predicate or returns null
        /// </summary>
        /// <param name="extended">The extended <see cref="XmlAttributeCollection"/></param>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>The first <see cref="XmlAttribute"/> matching the specified predicate</returns>
        public static XmlAttribute FirstOrDefault(this XmlAttributeCollection extended, Func<XmlAttribute, bool> predicate)
        {
            foreach(XmlAttribute attribute in extended)
            {
                if (predicate.Invoke(attribute))
                {
                    return attribute;
                }
            }
            return null;
        }

    }

}
