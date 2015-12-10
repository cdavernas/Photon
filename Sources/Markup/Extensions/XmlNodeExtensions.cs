using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon.Markup
{

    /// <summary>
    /// Defines extension methods for the <see cref="XmlNode"/> type
    /// </summary>
    public static class XmlNodeExtensions
    {

        /// <summary>
        /// Determines whether or not the <see cref="XmlNode"/> is a property node of the specified parent
        /// </summary>
        /// <param name="extended">The extended <see cref="XmlNode"/></param>
        /// <param name="parentNode">The parent <see cref="XmlNode"/></param>
        /// <returns>A boolean indicating whether or not the <see cref="XmlNode"/> is a property node of the specified parent</returns>
        public static bool IsPropertyNodeOf(this XmlNode extended, XmlNode parentNode)
        {
            if(extended.Name.StartsWith(parentNode.Name + "."))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
