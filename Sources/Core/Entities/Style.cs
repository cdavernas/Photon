using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Enables the sharing of properties, resources, and event handlers between instances of a type
    /// </summary>
    public class Style
        : Markup.IAddChild
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class
        /// </summary>
        public Style()
        {
            this.Setters = new SetterBaseCollection();
            this.Triggers = new TriggerBaseCollection();
        }

        /// <summary>
        /// Initializes a new <see cref="Style"/> instance to use on the specified type
        /// </summary>
        /// <param name="targetType">The element type the <see cref="Style"/> targets</param>
        public Style(Type targetType)
        {
            this.TargetType = targetType;
            this.Setters = new SetterBaseCollection();
            this.Triggers = new TriggerBaseCollection();
        }

        /// <summary>
        /// Initializes a new <see cref="Style"/> instance based on the specified <see cref="Style"/>
        /// </summary>
        /// <param name="targetType">The element type the <see cref="Style"/> targets</param>
        /// <param name="style">The <see cref="Style"/>'s base <see cref="Style"/></param>
        public Style(Type targetType, Style style)
        {
            this.TargetType = targetType;
            this.Setters = new SetterBaseCollection();
            this.Triggers = new TriggerBaseCollection();
        }

        /// <summary>
        /// Gets or sets the type for which this style is intended
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// Gets a collection of Setter and EventSetter objects
        /// </summary>
        public SetterBaseCollection Setters { get; private set; }

        /// <summary>
        /// Gets a collection of <see cref="TriggerBase"/> objects that apply property values based on specified conditions
        /// </summary>
        public TriggerBaseCollection Triggers { get; private set; }

        /// <summary>
        /// Applies the <see cref="Style"/> to the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to apply the <see cref="Style"/> to</param>
        internal void ApplyTo(DependencyObject dependencyObject)
        {
            foreach(SetterBase setterBase in this.Setters)
            {
                setterBase.Set(dependencyObject);
            }
        }

        /// <summary>
        /// Adds the specified child object
        /// </summary>
        /// <param name="child">An object representing the child to add</param>
        public void AddChild(object child)
        {
            if (!typeof(SetterBase).IsAssignableFrom(child.GetType()))
            {
                throw new NotSupportedException("A Style only supports SetterBase contents");
            }
            ((SetterBase)child).Style = this;
            this.Setters.Add((SetterBase)child);
        }

        /// <summary>
        /// Adds the specified text content
        /// </summary>
        /// <param name="text">A string representing the text to add</param>
        public void AddText(string text)
        {
            throw new NotSupportedException("A Style does not support direct text content");
        }

    }

}
