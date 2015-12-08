using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Controls
{

    /// <summary>
    /// This interface is implemented by all <see cref="IUIElement"/> that decorates a single child
    /// </summary>
    public interface IDecorator
        : IContentPresenter
    {

        /// <summary>
        /// Gets the element's child <see cref="UIElement"/>
        /// </summary>
        UIElement Child { get; set; }

    }

}
