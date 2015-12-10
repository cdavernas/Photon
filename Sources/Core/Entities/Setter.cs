using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents a setter that applies a property value
    /// </summary>
    public class Setter
        : SetterBase
    {

        /// <summary>
        /// Initializes a new <see cref="Setter"/> instance
        /// </summary>
        public Setter()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="Setter"/> instance with the specified property and value
        /// </summary>
        /// <param name="property">The <see cref="DependencyProperty"/> to which the Value will be applied</param>
        /// <param name="value">The value to apply to the specified property</param>
        public Setter(DependencyProperty property, object value)
        {
            this.Property = property;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new <see cref="Setter"/> instance with the specified property, value and target name
        /// </summary>
        /// <param name="property">The <see cref="DependencyProperty"/> to which the Value will be applied</param>
        /// <param name="value">The value to apply to the specified property</param>
        /// <param name="targetName">The name of the object the <see cref="Setter"/> is intended for</param>
        public Setter(DependencyProperty property, object value, string targetName)
        {
            this.Property = property;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="DependencyProperty"/> to which the Value will be applied
        /// </summary>
        public DependencyProperty Property { get; set; }

        /// <summary>
        /// Gets or sets the name of the object the <see cref="Setter"/> is intended for
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Gets or sets the value to apply to the property that is specified by the <see cref="Setter"/>
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Applies the <see cref="Setter"/> to the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to apply the <see cref="Setter"/> to</param>
        internal override void Set(DependencyObject dependencyObject)
        {
            switch (this.Property.Type)
            {
                case DependencyPropertyType.Property:
                    if (!dependencyObject.DependencyProperties.ContainsKey(this.Property))
                    {
                        throw new MissingMemberException("The specified DependencyProperty does not exist or could not be found in type '" + dependencyObject.GetType().FullName + "'");
                    }
                    dependencyObject.SetValue(this.Property, this.Value);
                    break;
                case DependencyPropertyType.AttachedProperty:
                    dependencyObject.DependencyProperties.Add(this.Property, this.Value);
                    break;
            }
        }

    }

}
