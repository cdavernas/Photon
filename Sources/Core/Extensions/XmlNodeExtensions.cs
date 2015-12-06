using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="XmlNode"/> type
    /// </summary>
    public static class XmlNodeExtensions
    {

        /// <summary>
        /// Retrieves all the siblings of the specified <see cref="XmlNode"/>
        /// </summary>
        /// <param name="extended">The extended <see cref="XmlNode"/></param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all of the <see cref="XmlNode"/> siblings</returns>
        public static IEnumerable<XmlNode> GetAllSiblings(this XmlNode extended)
        {
            HashSet<XmlNode> siblings;
            siblings = new HashSet<XmlNode>();
            if(extended.ChildNodes != null)
            {
                foreach (XmlNode node in extended.ChildNodes)
                {
                    siblings.Add(node);
                    siblings.AddRange(node.ChildNodes);
                }
            }
            return siblings;
        }

        /// <summary>
        /// Parses the xmlnode into its equivalent <see cref="IDependencyElement"/>
        /// </summary>
        /// <param name="extended">The extended <see cref="XmlNode"/></param>
        /// <returns>The parsed <see cref="IDependencyElement"/></returns>
        public static IDependencyElement ParseXaml(this XmlNode extended)
        {
            IDependencyElement element;
            element = XamlParser.ParseElement(extended);
            return element;
        }

    }

}
