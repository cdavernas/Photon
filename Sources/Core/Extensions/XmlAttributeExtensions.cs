using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="XmlAttribute"/> type
    /// </summary>
    public static class XmlAttributeExtensions
    {

        /// <summary>
        /// Determines whether or not the attribute is a markup attribute
        /// </summary>
        /// <param name="extended">The extended <see cref="XmlAttribute"/></param>
        /// <returns>A boolean indicating whether or not the attribute is a markup attribute</returns>
        public static bool IsMarkupAttribute(this XmlAttribute extended)
        {
            if(extended.Name == Markup.XamlParser.PREFIX_MARKUP
                || extended.Name == Markup.XamlParser.PREFIX_XML_NAMESPACE)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(extended.Prefix))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

}
