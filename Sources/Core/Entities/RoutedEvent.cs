using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents and identifies a routed event and declares its characteristics
    /// </summary>
    public sealed class RoutedEvent
    {

        /// <summary>
        /// Initializes a new <see cref="RoutedEvent"/> instance based on the specified handler type and name
        /// </summary>
        /// <param name="handlerType">The handler type of the <see cref="RoutedEvent"/></param>
        /// <param name="name">The identifying name of the <see cref="RoutedEvent"/></param>
        private RoutedEvent(Type handlerType, string name)
        {
            this.HandlerType = handlerType;
            this.Name = name;
        }

        /// <summary>
        /// Gets the handler type of the <see cref="RoutedEvent"/>
        /// </summary>
        public Type HandlerType { get; private set; }

        /// <summary>
        /// Gets the identifying name of the <see cref="RoutedEvent"/>
        /// </summary>
        public string Name { get; private set; }

    }

}
