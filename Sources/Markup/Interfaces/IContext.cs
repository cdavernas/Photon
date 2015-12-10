using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Markup
{

    /// <summary>
    /// Defines the context in which markup code is handled
    /// </summary>
    public interface IContext
    {

        /// <summary>
        /// When implemented in a class, gets the <see cref="IHandler"/> associated with the <see cref="IContext"/>
        /// </summary>
        IHandler Handler { get;  }

    }

}
