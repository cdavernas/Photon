using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This interface defines methods and properties for all <see cref="DependencyObject"/> in Photon
    /// </summary>
    public interface IDependencyElement
        : INotifyPropertyChanged
    {

        /// <summary>
        /// Gets a dictionary containing all of the element's <see cref="DependencyProperty"/> 
        /// </summary>
        Dictionary<DependencyProperty, object> DependencyProperties { get; }

        /// <summary>
        /// Gets a list containing all the <see cref="Media.Animations.AnimationClock"/> attached to the <see cref="IDependencyElement"/>
        /// </summary>
        HashSet<Media.Animations.AnimationClock> AnimationClocks { get; }

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the property to get</param>
        /// <returns>An object representing the property's value</returns>
        object GetValue(string propertyName);

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the property to get</param>
        /// <typeparam name="TResult">The expected type of the returned value</typeparam>
        /// <returns>An object representing the property's value</returns>
        TResult GetValue<TResult>(string propertyName);

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="dependencyProperty">The <see cref="DependencyProperty"/> to get</param>
        /// <returns>An object representing the property's value</returns>
        object GetValue(DependencyProperty dependencyProperty);

        /// <summary>
        /// Gets the value of the specified property
        /// </summary>
        /// <param name="dependencyProperty">The <see cref="DependencyProperty"/> to get</param>
        /// <typeparam name="TResult">The expected type of the returned value</typeparam>
        /// <returns>An object representing the property's value</returns>
        TResult GetValue<TResult>(DependencyProperty dependencyProperty);

        /// <summary>
        /// Sets the value of the specified property
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the property to get</param>
        /// <param name="value">The value to set the property with</param>
        void SetValue(string propertyName, object value);

        /// <summary>
        /// Sets the value of the specified property
        /// </summary>
        /// <param name="dependencyProperty">The <see cref="DependencyProperty"/> to get</param>
        /// <param name="value">The value to set the property with</param>
        void SetValue(DependencyProperty dependencyProperty, object value);

    }

}
