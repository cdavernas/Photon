using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Implements a data structure for describing a property as a path below another property, or below an owning type. <para></para>
    /// Property paths are used in data binding to objects, and in storyboards and timelines for animations
    /// </summary>
    public sealed class PropertyPath
    {

        /// <summary>
        /// Initializes a new <see cref="PropertyPath"/> based on the specified chain of <see cref="DependencyProperty"/>
        /// </summary>
        /// <param name="propertyChain">An <see cref="IEnumerable{T}"/> of the ordered <see cref="DependencyProperty"/> instances to navigate</param>
        public PropertyPath(IEnumerable<DependencyProperty> propertyChain)
        {
            this.PropertyChain = propertyChain;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of the ordered <see cref="DependencyProperty"/> instances to navigate
        /// </summary>
        public IEnumerable<DependencyProperty> PropertyChain { get; private set; }

        /// <summary>
        /// Gets an object representing the value returned by the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to
        /// </summary>
        /// <param name="dependencyObject">The root <see cref="DependencyObject"/> for which to get the value returned by the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to</param>
        /// <returns>An object representing the value returned by the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to</returns>
        public object GetValue(DependencyObject dependencyObject)
        {
            object propertyValue;
            propertyValue = dependencyObject;
            foreach (DependencyProperty chainedProperty in this.PropertyChain)
            {
                if (propertyValue == null)
                {
                    throw new NullReferenceException("A null reference occured while navigating the PropertyPath. This typically occurs if one of the chained DepencyProperty returns null before the end of the chain");
                }
                if (!typeof(DependencyObject).IsAssignableFrom(propertyValue.GetType()))
                {
                    throw new NotSupportedException("The value of type '" + propertyValue.GetType().FullName + "' cannot be cast to the expected type '" + typeof(DependencyObject).FullName + "'");
                }
                if (!((DependencyObject)propertyValue).DependencyProperties.Contains(chainedProperty.ToString()))
                {
                    throw new MissingMemberException("The value of type '" + propertyValue.GetType().FullName + "' does not contain expected DependencyProperty '" + chainedProperty.ToString() + "'");
                }
                propertyValue = chainedProperty.GetValue((DependencyObject)propertyValue);
            }
            return propertyValue;
        }

        /// <summary>
        /// Gets an object representing the value returned by the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to
        /// </summary>
        /// <param name="dependencyObject">The root <see cref="DependencyObject"/> for which to get the value returned by the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to</param>
        /// <returns>An object representing the value returned by the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to</returns>
        /// <typeparam name="TResult">The expected type of the returned value</typeparam>
        public TResult GetValue<TResult>(DependencyObject dependencyObject)
        {
            object result;
            result = this.GetValue(dependencyObject);
            if(result == null)
            {
                return default(TResult);
            }
            if (!typeof(TResult).IsAssignableFrom(result.GetType()))
            {
                throw new InvalidCastException("Cannot cast type '" + result.GetType().FullName + "' to expected type '" + typeof(TResult).FullName + "'");
            }
            return (TResult)result;
        }

        /// <summary>
        /// Sets the value of the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to
        /// </summary>
        /// <param name="dependencyObject">The root <see cref="DependencyObject"/> for which to set the value of the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to</param>
        /// <param name="value">The value to set to the <see cref="DependencyProperty"/> the <see cref="PropertyPath"/> leads to</param>
        public void SetValue(DependencyObject dependencyObject, object value)
        {
            DependencyProperty chainedProperty;
            object propertyValue;
            propertyValue = dependencyObject;
            for(int propertyIndex = 0; propertyIndex < this.PropertyChain.Count() - 1; propertyIndex++)
            {
                chainedProperty = this.PropertyChain.ElementAt(propertyIndex);
                if (!typeof(DependencyObject).IsAssignableFrom(propertyValue.GetType()))
                {
                    throw new NotSupportedException("The value of type '" + propertyValue.GetType().FullName + "' cannot be cast to the expected type '" + typeof(DependencyObject).FullName + "'");
                }
                if (!((DependencyObject)propertyValue).DependencyProperties.Contains(chainedProperty.ToString()))
                {
                    throw new MissingMemberException("The value of type '" + propertyValue.GetType().FullName + "' does not contain expected DependencyProperty '" + chainedProperty.ToString() + "'");
                }
                propertyValue = chainedProperty.GetValue((DependencyObject)propertyValue);
                if(propertyValue == null)
                {
                    throw new NullReferenceException("A null reference occured while navigating the PropertyPath. This typically occurs if one of the chained DepencyProperty returns null before the end of the chain");
                }
            }
            if (propertyValue == null)
            {
                throw new NullReferenceException("A null reference occured while navigating the PropertyPath. This typically occurs if one of the chained DepencyProperty returns null before the end of the chain");
            }
            chainedProperty = this.PropertyChain.Last();
            if (!typeof(DependencyObject).IsAssignableFrom(propertyValue.GetType()))
            {
                throw new NotSupportedException("The value of type '" + propertyValue.GetType().FullName + "' cannot be cast to the expected type '" + typeof(DependencyObject).FullName + "'");
            }
            if (!((DependencyObject)propertyValue).DependencyProperties.Contains(chainedProperty.ToString()))
            {
                throw new MissingMemberException("The value of type '" + propertyValue.GetType().FullName + "' does not contain expected DependencyProperty '" + chainedProperty.ToString() + "'");
            }
            chainedProperty.SetValue((DependencyObject)propertyValue, value);
        }

    }

}
