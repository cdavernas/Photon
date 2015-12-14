using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents a <see cref="Dictionary{TKey, TValue}"/> of a pair of a <see cref="DependencyProperty"/> and an object representing the property's value for the owner <see cref="DependencyObject"/>
    /// </summary>
    public class DependencyPropertyCollection
        : Dictionary<DependencyProperty, object>
    {

        /// <summary>
        /// Gets a boolean indicating whether or not the specified property full name (as returned by the <see cref="DependencyProperty.ToString"/> method) is found within the <see cref="DependencyPropertyCollection"/>
        /// </summary>
        /// <param name="propertyFullName">the specified property full name (as returned by the <see cref="DependencyProperty.ToString"/> method) to search for within the <see cref="DependencyPropertyCollection"/></param>
        /// <returns>A boolean indicating whether or not the specified property full name (as returned by the <see cref="DependencyProperty.ToString"/> method) is found within the <see cref="DependencyPropertyCollection"/></returns>
        public bool Contains(string propertyFullName)
        {
            if(this.Keys.FirstOrDefault(dp => dp.ToString() == propertyFullName) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
