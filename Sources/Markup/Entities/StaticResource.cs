using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Markup
{

    /// <summary>
    /// Implements a <see cref="MarkupExtension"/> that supports static (XAML load time) resource references made from XAML
    /// </summary>
    public class StaticResource
        : MarkupExtension
    {

        /// <summary>
        /// Initializes a new <see cref="StaticResource"/> instance based on the specified resource key
        /// </summary>
        /// <param name="resourceKey">The key of the resource referenced by the <see cref="StaticResource"/></param>
        public StaticResource(string resourceKey)
        {
            this.ResourceKey = resourceKey;
        }

        /// <summary>
        /// Gets the key of the resource referenced by the <see cref="StaticResource"/>
        /// </summary>
        public string ResourceKey { get; private set; }

        /// <summary>
        /// Returns the value provided by the <see cref="StaticResource"/> in the specified <see cref="IContext"/>
        /// </summary>
        /// <param name="context">The <see cref="IContext"/> for which to provide the value</param>
        /// <returns>An object representing the value provided by the <see cref="StaticResource"/> in the specified <see cref="IContext"/></returns>
        public override object ProvideValue(IContext context)
        {
            return ((IUIElement)context.Handler.ElementTree.Root).Resources[this.ResourceKey];
        }

        /// <summary>
        /// Try to parse the specified string, and return a boolean indicating whether or not the attempt was successfull
        /// </summary>
        /// <param name="value">The string to parse</param>
        /// <param name="staticResource">The <see cref="StaticResource"/> returned in case the specified string could be parsed</param>
        /// <returns>A boolean indicating whether or not the conversion attempt was successfull</returns>
        public static bool TryParse(string value, out StaticResource staticResource)
        {
            string resourceKey;
            if(!value.StartsWith("{StaticResource ") ||
                !value.EndsWith("}"))
            {
                staticResource = null;
                return false;
            }
            resourceKey = value.Split(new string[] { "{StaticResource " }, StringSplitOptions.RemoveEmptyEntries).Last();
            resourceKey = resourceKey.Substring(0, resourceKey.Length - 1);
            staticResource = new StaticResource(resourceKey);
            return true;
        }

    }

}
