using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon.Markup
{

    /// <summary>
    /// Represents a namespace declared in a markup document
    /// </summary>
    public class NamespaceDeclaration
    {

        /// <summary>
        /// The text preceeding the reference of an <see cref="Assembly"/>'s namespace
        /// </summary>
        private const string ASSEMBLY_NAMESPACE_PREFIX = "clr-namespace:";

        /// <summary>
        /// Initializes a new <see cref="NamespaceDeclaration"/> based on the specified namespace prefix and <see cref="Uri"/>
        /// </summary>
        /// <param name="prefix">The prefix of the namespace</param>
        /// <param name="uri">A string representing the namespace's uri</param>
        public NamespaceDeclaration(string prefix, string uri)
        {
            if (uri.ToString().StartsWith(NamespaceDeclaration.ASSEMBLY_NAMESPACE_PREFIX))
            {
                this.Type = NamespaceDeclarationType.AssemblyNamespaceReference;
            }
            else
            {
                this.Type = NamespaceDeclarationType.Standard;
            }
            this.Prefix = prefix;
            this.Uri = uri;
        }

        /// <summary>
        /// Gets the <see cref="NamespaceDeclaration"/>'s <see cref="NamespaceDeclarationType"/>
        /// </summary>
        public NamespaceDeclarationType Type { get; private set; }

        /// <summary>
        /// Gets the markup prefix of the namespace
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// Gets a string representing the <see cref="NamespaceDeclaration"/>'s uri 
        /// </summary>
        public string Uri { get; private set; }

        /// <summary>
        /// Returns the namespace referenced by the <see cref="NamespaceDeclaration"/><para></para> 
        /// Works only if the <see cref="NamespaceDeclaration.Type"/> property has been set to <see cref="NamespaceDeclarationType.AssemblyNamespaceReference"/> 
        /// </summary>
        /// <returns>A string representing the namespace referenced by the <see cref="NamespaceDeclaration"/></returns>
        public string GetReferenceNamespace()
        {
            string ns;
            if (this.Type != NamespaceDeclarationType.AssemblyNamespaceReference)
            {
                return null;
            }
            ns = this.Uri.Split(new string[] { ";assembly=" }, StringSplitOptions.RemoveEmptyEntries).First().Split(new string[] { "clr-namespace:" }, StringSplitOptions.RemoveEmptyEntries).First();
            return ns;
        }

        /// <summary>
        /// Returns the <see cref="Assembly"/> referenced by the <see cref="NamespaceDeclaration"/><para></para> 
        /// Works only if the <see cref="NamespaceDeclaration.Type"/> property has been set to <see cref="NamespaceDeclarationType.AssemblyNamespaceReference"/> 
        /// </summary>
        /// <returns>The <see cref="Assembly"/> referenced by the <see cref="NamespaceDeclaration"/></returns>
        public Assembly GetReferencedAssembly()
        {
            string assemblyName;
            Assembly assembly;
            if(this.Type != NamespaceDeclarationType.AssemblyNamespaceReference)
            {
                return null;
            }
            assemblyName = this.Uri.Split(new string[] { "assembly=" }, StringSplitOptions.RemoveEmptyEntries).Last();
            assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm => asm.GetName().Name == assemblyName);
            return assembly;
        }

    }

}
