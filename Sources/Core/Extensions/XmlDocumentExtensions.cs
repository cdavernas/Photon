using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="XmlDocument"/> type
    /// </summary>
    public static class XmlDocumentExtensions
    {

        /// <summary>
        /// Retrieves all of the document's siblings
        /// </summary>
        /// <param name="extended">The extended <see cref="XmlDocument"/></param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all the document's sibling <see cref="XmlNode"/>s</returns>
        public static IEnumerable<XmlNode> GetAllSiblings(this XmlDocument extended)
        {
            HashSet<XmlNode> siblings;
            siblings = new HashSet<XmlNode>();
            siblings.Add(extended.FirstChild);
            siblings.AddRange(extended.FirstChild.GetAllSiblings());
            return siblings;
        }

    }

}
