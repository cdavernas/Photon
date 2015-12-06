using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents an object that participates in the dependency property system
    /// </summary>
    public abstract class DependencyObject
        : IDependencyElement
    {

        /// <summary>
        /// This event is fired every time one of the DepencyObject's property has been changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The parameterless constructor for the <see cref="DependencyObject"/> type
        /// </summary>
        protected DependencyObject()
        {
            this.DependencyProperties = DependencyObject.GetDepencyProperties(this.GetType());
            this.AnimationClocks = new HashSet<Media.Animations.AnimationClock>();
        }

        /// <summary>
        /// Gets a dictionary containing a list of all the <see cref="DependencyProperty"/> contained by the <see cref="DependencyObject"/>
        /// </summary>
        public Dictionary<DependencyProperty, object> DependencyProperties { get; private set; }

        /// <summary>
        /// Gets a list containing a list of all the <see cref="Media.Animations.AnimationClock"/> attached to the <see cref="DependencyObject"/>
        /// </summary>
        public HashSet<Media.Animations.AnimationClock> AnimationClocks { get; private set; }

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the property to get</param>
        /// <returns>An object representing the property's value</returns>
        public object GetValue(string propertyName)
        {
            DependencyProperty dependencyProperty;
            dependencyProperty = this.DependencyProperties.Keys.FirstOrDefault(p => p.Name == propertyName);
            if (dependencyProperty == null)
            {
                throw new MissingMemberException("The specified property '" + propertyName + "' cannot be found in type '" + this.GetType().ToString() + "'");
            }
            return this.DependencyProperties[dependencyProperty];
        }

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="dependencyProperty">The <see cref="DependencyProperty"/> to get</param>
        /// <returns>An object representing the property's value</returns>
        public object GetValue(DependencyProperty dependencyProperty)
        {
            return this.GetValue(dependencyProperty.Name);
        }

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the property to get</param>
        /// <typeparam name="TResult">The expected type of the returned value</typeparam>
        /// <returns>An object representing the property's value</returns>
        public TResult GetValue<TResult>(string propertyName)
        {
            object result;
            result = this.GetValue(propertyName);
            if (result == null)
            {
                return default(TResult);
            }
            if (!typeof(TResult).IsAssignableFrom(result.GetType()))
            {
                throw new InvalidCastException("The type '" + result.GetType().Name + "' cannot be cast to type '" + typeof(TResult).Name + "'");
            }
            return (TResult)result;
        }

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="dependencyProperty">The <see cref="DependencyProperty"/> to get</param>
        /// <typeparam name="TResult">The expected type of the returned value</typeparam>
        /// <returns>An object representing the property's value</returns>
        public TResult GetValue<TResult>(DependencyProperty dependencyProperty)
        {
            return this.GetValue<TResult>(dependencyProperty.Name);
        }

        /// <summary>
        /// Sets the value of the specified property
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the property to get</param>
        /// <param name="value">The value to set the property with</param>
        public void SetValue(string propertyName, object value)
        {
            DependencyProperty dependencyProperty;
            object originalValue;
            dependencyProperty = this.DependencyProperties.Keys.FirstOrDefault(p => p.Name == propertyName);
            if (dependencyProperty == null)
            {
                throw new MissingMemberException("The specified property '" + propertyName + "' cannot be found in type '" + this.GetType().ToString() + "'");
            }
            originalValue = this.DependencyProperties[dependencyProperty];
            if (originalValue == value)
            {
                return;
            }
            if (value != null)
            {
                if (!dependencyProperty.PropertyType.IsAssignableFrom(value.GetType()))
                {
                    throw new InvalidCastException("The type '" + value.GetType().Name + "' cannot be cast to type '" + dependencyProperty.PropertyType.Name + "'");
                }
            }
            this.DependencyProperties[dependencyProperty] = value;
            this.NotifyPropertyChanged(propertyName, originalValue, value);
        }

        /// <summary>
        /// Sets the value of the specified property
        /// </summary>
        /// <param name="dependencyProperty">The <see cref="DependencyProperty"/> to get</param>
        /// <param name="value">The value to set the property with</param>
        public void SetValue(DependencyProperty dependencyProperty, object value)
        {
            this.SetValue(dependencyProperty.Name, value);
        }

        /// <summary>
        /// This method notifies any changes suffered by a given <see cref="DependencyProperty"/>
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the modified property</param>
        /// <param name="originalValue">An object representing the property's value before the suffered change(s)</param>
        /// <param name="newValue">An object representing the property's actual (new) value</param>
        private void NotifyPropertyChanged(string propertyName, object originalValue, object newValue)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            this.OnPropertyChanged(propertyName, originalValue, newValue);
        }

        /// <summary>
        /// When overriden in a class, this method provides means to run code whenever a <see cref="DependencyProperty"/> has changed
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the modified property</param>
        /// <param name="originalValue">An object representing the property's value before the suffered change(s)</param>
        /// <param name="value">An object representing the property's actual (new) value</param>
        protected virtual void OnPropertyChanged(string propertyName, object originalValue, object value)
        {
          
        }

        /// <summary>
        /// This static method searches the specified type for all <see cref="DependencyProperty"/>
        /// </summary>
        /// <param name="depencyObjectType">The type of the <see cref="DependencyObject"/> to search for <see cref="DependencyProperty"/></param>
        /// <returns>A dictionary of <see cref="DependencyProperty"/></returns>
        private static Dictionary<DependencyProperty, object> GetDepencyProperties(Type depencyObjectType)
        {
            Dictionary<DependencyProperty, object> dependencyProperties;
            DependencyProperty dependencyProperty;
            dependencyProperties = new Dictionary<DependencyProperty, object>();
            foreach (FieldInfo field in depencyObjectType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (field.FieldType == typeof(DependencyProperty))
                {
                    dependencyProperty = (DependencyProperty)field.GetValue(null);
                    dependencyProperties.Add(dependencyProperty, dependencyProperty.DefaultValue);
                }
            }
            return dependencyProperties;
        }

    }

}
