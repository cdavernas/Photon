using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Markup
{

    /// <summary>
    /// Implements a <see cref="MarkupExtension"/> that supports type references
    /// </summary>
    public class TypeMarkupExtension
        : MarkupExtension
    {

        private const string PREFIX = "{x:Type ";

        /// <summary>
        /// Initializes a new <see cref="TypeMarkupExtension"/>
        /// </summary>
        /// <param name="namespacePrefix">The referenced type's namespace prefix</param>
        /// <param name="typeName">The referenced type name</param>
        public TypeMarkupExtension(string namespacePrefix, string typeName)
        {
            this.NamespacePrefix = namespacePrefix;
            this.TypeName = typeName;
        }

        /// <summary>
        /// Gets the prefix of the namespace of the type referenced by the <see cref="TypeMarkupExtension"/>
        /// </summary>
        public string NamespacePrefix { get; private set; }

        /// <summary>
        /// Gets the name of the type referenced by the <see cref="TypeMarkupExtension"/>
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// Returns the value provided by the <see cref="TypeMarkupExtension"/> in the specified <see cref="IContext"/>
        /// </summary>
        /// <param name="context">The <see cref="IContext"/> for which to provide the value</param>
        /// <returns>The type provided by the <see cref="TypeMarkupExtension"/> in the specified <see cref="IContext"/></returns>
        public override object ProvideValue(IContext context)
        {
            Type type;
            type = context.Handler.ElementTypeOf(this.NamespacePrefix, this.TypeName);
            return type;
        }

        /// <summary>
        /// Try to parse the specified string, and return a boolean indicating whether or not the attempt was successfull
        /// </summary>
        /// <param name="value">The string to parse</param>
        /// <param name="typeExtension">The <see cref="TypeMarkupExtension"/> returned in case the specified string could be parsed</param>
        /// <returns>A boolean indicating whether or not the conversion attempt was successfull</returns>
        public static bool TryParse(string value, out TypeMarkupExtension typeExtension)
        {
            string prefix, typeName;
            string[] temp;
            if (!value.StartsWith(TypeMarkupExtension.PREFIX) ||
                !value.EndsWith("}"))
            {
                typeExtension = null;
                return false;
            }
            typeName = value.Split(new string[] { TypeMarkupExtension.PREFIX }, StringSplitOptions.RemoveEmptyEntries).Last();
            typeName = typeName.Substring(0, typeName.Length - 1);
            temp = typeName.Split(':');
            if(temp.Length == 2)
            {
                prefix = temp[0];
                typeName = temp[1];
            }
            else
            {
                prefix = null;
                typeName = temp[0];
            }
            typeExtension = new TypeMarkupExtension(prefix, typeName);
            return true;
        }

    }

}
