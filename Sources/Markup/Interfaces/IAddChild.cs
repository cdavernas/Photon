using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Markup
{

    /// <summary>
    /// Provides a means to parse elements that permit mixtures of child elements or text
    /// </summary>
    public interface IAddChild
    {

        /// <summary>
        /// Adds a child object
        /// </summary>
        /// <param name="child">An object representing the child to add</param>
        void AddChild(object child);

        /// <summary>
        /// Adds the text content of a node to the object
        /// </summary>
        /// <param name="text">A string representing the text to add</param>
        void AddText(string text);

    }

}
