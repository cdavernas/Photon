using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Markup
{

    /// <summary>
    /// Describes the type of a <see cref="NamespaceDeclaration"/>
    /// </summary>
    public enum NamespaceDeclarationType
    {
        /// <summary>
        /// The <see cref="NamespaceDeclaration"/> is a normal xml namespace declaration
        /// </summary>
        Standard,
        /// <summary>
        /// The <see cref="NamespaceDeclaration"/> references a namespace contained within an <see cref="Assembly"/>
        /// </summary>
        AssemblyNamespaceReference
    }

}
