using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Controls
{

    /// <summary>
    /// This interface is implemented by all <see cref="UIElement"/> presenting multiple childs
    /// </summary>
    public interface IPanel
        : IContentPresenter
    {

        /// <summary>
        /// Gets a <see cref="UIElementCollection"/> containing all of the element's children
        /// </summary>
        UIElementCollection Children { get; }

    }

}
