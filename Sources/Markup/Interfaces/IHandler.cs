using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon.Markup
{

    /// <summary>
    /// Defines an object that can handle markup code; that is read and write to xaml
    /// </summary>
    public interface IHandler
    {

        /// <summary>
        /// Gets the <see cref="XmlDocument"/> containing the markup code
        /// </summary>
        XmlDocument Document { get; }

        /// <summary>
        /// Gets a collection of all the <see cref="NamespaceDeclaration"/> in the <see cref="IHandler"/>'s scope
        /// </summary>
        NamespaceDeclarationCollection NamespaceDeclarations { get; }

        /// <summary>
        /// Gets the xaml document's <see cref="Photon.DependencyElementTree"/>
        /// </summary>
        DependencyElementTree ElementTree { get; }

        /// <summary>
        /// Gets a dictionary containing the both the key and hashcode of registered element
        /// </summary>
        Dictionary<int, string> ElementKeys { get; }

        /// <summary>
        /// Determines the type equivalency based on both the specified namespace prefix and type name
        /// </summary>
        /// <param name="namespacePrefix">The prefix of the referenced type's namespace</param>
        /// <param name="typeName">The name of the referenced type</param>
        /// <returns>The <see cref="Type"/> equivalency based on both the specified namespace prefix and type name</returns>
        Type ElementTypeOf(string namespacePrefix, string typeName);

    }

}
