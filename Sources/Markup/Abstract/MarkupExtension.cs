using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Markup
{

    /// <summary>
    /// Provides a base class for XAML markup extension implementations that can be supported by Photon XAML Services and other XAML readers and XAML writers
    /// </summary>
    public abstract class MarkupExtension
    {

        /// <summary>
        /// Returns the value provided by the <see cref="MarkupExtension"/> in the specified <see cref="IContext"/>
        /// </summary>
        /// <param name="context">The <see cref="IContext"/> for which to provide the value</param>
        /// <returns>An object representing the value provided by the <see cref="MarkupExtension"/> in the specified <see cref="IContext"/></returns>
        public abstract object ProvideValue(IContext context);

    }

}
