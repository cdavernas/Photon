using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Markup
{

    /// <summary>
    /// Represents the <see cref="XamlParser"/>'s <see cref="IContext"/>
    /// </summary>
    public class XamlParserContext
        : IContext
    {

        /// <summary>
        /// Initializes a new <see cref="XamlParserContext"/>
        /// </summary>
        /// <param name="handler">The <see cref="XamlParser"/> associated with the <see cref="XamlParserContext"/></param>
        public XamlParserContext(XamlParser handler)
        {
            this.Handler = handler;
        }

        /// <summary>
        /// Gets the <see cref="IHandler"/> (in this case a <see cref="XamlParser"/> instance) associated with the <see cref="IContext"/>
        /// </summary>
        public IHandler Handler { get; private set; }

    }

}
