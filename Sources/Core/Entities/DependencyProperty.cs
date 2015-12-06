using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents a property that can be set through methods such as, styling, data binding, animation, and inheritance.
    /// </summary>
    public sealed class DependencyProperty
    {

        /// <summary>
        /// A constructor for the <see cref="DependencyProperty"/> type
        /// </summary>
        /// <param name="type">The property's <see cref="DependencyPropertyType"/></param>
        /// <param name="name">The case-sensitive name of the property</param>
        /// <param name="propertyType">The property's return type</param>
        /// <param name="declaringType">The type declaring the property</param>
        /// <param name="getMethod">The method used to retrieve the property's value</param>
        /// <param name="setMethod">The method used to set the property's value</param>
        private DependencyProperty(DependencyPropertyType type, string name, Type propertyType, Type declaringType, MethodInfo getMethod, MethodInfo setMethod)
        {
            this.Type = type;
            this.Name = name;
            this.PropertyType = propertyType;
            this.DeclaringType = declaringType;
            this.GetMethod = getMethod;
            this.SetMethod = setMethod;
        }

        /// <summary>
        /// A constructor for the <see cref="DependencyProperty"/> type
        /// </summary>
        /// <param name="type">The property's <see cref="DependencyPropertyType"/></param>
        /// <param name="name">The case-sensitive name of the property</param>
        /// <param name="propertyType">The property's return type</param>
        /// <param name="declaringType">The type declaring the property</param>
        /// <param name="getMethod">The method used to retrieve the property's value</param>
        /// <param name="setMethod">The method used to set the property's value</param>
        /// <param name="defaultValue">The property's default (initial) value</param>
        private DependencyProperty(DependencyPropertyType type, string name, Type propertyType, Type declaringType, MethodInfo getMethod, MethodInfo setMethod, object defaultValue)
        {
            this.Type = type;
            this.Name = name;
            this.PropertyType = propertyType;
            this.DeclaringType = declaringType;
            this.GetMethod = getMethod;
            this.SetMethod = setMethod;
            if (!this.PropertyType.IsAssignableFrom(defaultValue.GetType()))
            {
                throw new ArgumentException("The value of type '" + defaultValue.GetType().Name + "' provided for the 'defaultValue' argument cannot be assigned to the type '" + this.PropertyType.Name + "'");
            }
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/>'s <see cref="DependencyPropertyType"/>
        /// </summary>
        public DependencyPropertyType Type { get; private set; }

        /// <summary>
        /// Gets a string representing the name of the property
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the property's return type
        /// </summary>
        public Type PropertyType { get; private set; }

        /// <summary>
        /// Gets the type declaring the property
        /// </summary>
        public Type DeclaringType { get; private set; }

        /// <summary>
        /// Gets an object representing the property's default value
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Gets a string representing the property's path
        /// </summary>
        public string Path
        {
            get
            {
                return this.DeclaringType.Name + "." + this.Name;
            }
        }

        /// <summary>
        /// Gets/Sets the method used to retrieve the property's value
        /// </summary>
        private MethodInfo GetMethod { get; set; }

        /// <summary>
        /// Gets/Sets the method used to set the property's value
        /// </summary>
        private MethodInfo SetMethod { get; set; }

        /// <summary>
        /// Gets the value returned by the property
        /// </summary>
        /// <param name="target">The <see cref="DependencyObject"/> for which to retrieve the property's value</param>
        /// <returns>An object representing the property's value</returns>
        public object GetValue(DependencyObject target)
        {
            if (!this.DeclaringType.IsAssignableFrom(target.GetType()))
            {
                throw new ArgumentException("The specified UIElement cannot be assigned to expected type '" + this.DeclaringType.FullName + "'");
            }
            switch (this.Type)
            {
                case DependencyPropertyType.Property:
                    return target.DependencyProperties[this];
                case DependencyPropertyType.AttachedProperty:
                    return this.GetMethod.Invoke(null, new object[] { target });
                default:
                    throw new NotSupportedException("The specified DependencyProperty type is not supported");
            }
        }

        /// <summary>
        /// Gets the value returned by the property
        /// </summary>
        /// <param name="target">The <see cref="DependencyObject"/> for which to retrieve the property's value</param>
        /// <typeparam name="TResult">The expected type of the property's value</typeparam>
        /// <returns>An object representing the property's value</returns>
        public TResult GetValue<TResult>(DependencyObject target)
        {
            object result;
            result = this.GetValue(target);
            if(result == null)
            {
                return default(TResult);
            }
            if (typeof(TResult).IsAssignableFrom(result.GetType()))
            {
                return (TResult)result;
            }
            throw new InvalidCastException("A value of type '" + result.GetType().FullName + "' cannot be cast to the expected type '" + typeof(TResult).FullName + "'");
        }

        /// <summary>
        /// Sets the property's value
        /// </summary>
        /// <param name="target">The <see cref="DependencyObject"/> for which to set the property's value</param>
        /// <param name="value">An object representing the value to set</param>
        public void SetValue(DependencyObject target, object value)
        {
            if(value != null)
            {
                if (this.PropertyType.IsGenericType)
                {
                    if (!this.PropertyType.GetGenericArguments().First().IsAssignableFrom(value.GetType()))
                    {
                        throw new ArgumentException("The object passed as the 'value' argument, of type '" + value.GetType().FullName + "' cannot be assign to the targeted property, of type '" + this.PropertyType.GetGenericArguments().First().FullName + "'");
                    }
                }
                else if (!this.PropertyType.IsAssignableFrom(value.GetType()))
                {
                    throw new ArgumentException("The object passed as the 'value' argument, of type '" + value.GetType().FullName + "' cannot be assign to the targeted property, of type '" + this.PropertyType.FullName + "'");
                }
            }
            switch (this.Type)
            {
                case DependencyPropertyType.Property:
                    this.SetMethod.Invoke(target, new object[] { value });
                    break;
                case DependencyPropertyType.AttachedProperty:
                    this.SetMethod.Invoke(null, new object[] { target, value });
                    break;
                default:
                    throw new NotSupportedException("The specified DependencyProperty type is not supported");
            }
        }

        /// <summary>
        /// Returns the property's string representation
        /// </summary>
        /// <returns>A string representing the property</returns>
        public override string ToString()
        {
            return this.Path;
        }

        /// <summary>
        /// Registers a new <see cref="DependencyProperty"/>
        /// </summary>
        /// <param name="propertyName">The case-sensitive name of the property to register</param>
        /// <param name="declaringType">The type declaring the property to register</param>
        /// <returns>The newly registered <see cref="DependencyProperty"/></returns>
        public static DependencyProperty Register(string propertyName, Type declaringType)
        {
            PropertyInfo property;
            MethodInfo getMethod, setMethod;
            property = declaringType.GetProperty(propertyName);
            if (property == null)
            {
                throw new MissingMemberException("The property '" + propertyName + "' cannot be found in type '" + declaringType.FullName + "'");
            }
            getMethod = property.GetGetMethod();
            if (getMethod == null)
            {
                throw new MissingMemberException("The GET accessor method for the property '" + propertyName + "' could not be found in type '" + declaringType.FullName + "'");
            }
            setMethod = property.GetSetMethod();
            if (setMethod == null)
            {
                throw new MissingMemberException("The SET accessor method for the property '" + propertyName + "' could not be found in type '" + declaringType.FullName + "'");
            }
            return new DependencyProperty(DependencyPropertyType.Property, propertyName, property.PropertyType, declaringType, getMethod, setMethod);
        }

        /// <summary>
        /// Registers a new <see cref="DependencyProperty"/>
        /// </summary>
        /// <param name="propertyName">The case-sensitive name of the property to register</param>
        /// <param name="declaringType">The type declaring the property to register</param>
        /// <param name="defaultValue">An object representing the property's initial value</param>
        /// <returns>The newly registered <see cref="DependencyProperty"/></returns>
        public static DependencyProperty Register(string propertyName, Type declaringType, object defaultValue)
        {
            PropertyInfo property;
            MethodInfo getMethod, setMethod;
            property = declaringType.GetProperty(propertyName);
            if (property == null)
            {
                throw new MissingMemberException("The property '" + propertyName + "' cannot be found in type '" + declaringType.FullName + "'");
            }
            getMethod = property.GetGetMethod();
            if (getMethod == null)
            {
                throw new MissingMemberException("The GET accessor method for the property '" + propertyName + "' could not be found in type '" + declaringType.FullName + "'");
            }
            setMethod = property.GetSetMethod();
            if (setMethod == null)
            {
                throw new MissingMemberException("The SET accessor method for the property '" + propertyName + "' could not be found in type '" + declaringType.FullName + "'");
            }
            return new DependencyProperty(DependencyPropertyType.Property, propertyName, property.PropertyType, declaringType, getMethod, setMethod, defaultValue);
        }

        /// <summary>
        /// Register a new attached <see cref="DependencyProperty"/>
        /// </summary>
        /// <param name="propertyName">The case-sensitive name of the property to register</param>
        /// <param name="propertyType">The return type of the property to register</param>
        /// <param name="declaringType">The type declaring the property to register</param>
        /// <returns>The newly registered attached <see cref="DependencyProperty"/></returns>
        public static DependencyProperty RegisterAttached(string propertyName, Type propertyType, Type declaringType)
        {
            MethodInfo getMethod, setMethod;
            DependencyProperty dependencyProperty;
            getMethod = declaringType.GetMethod(DependencyProperty.ResolveAttachedGetMethodName(propertyName), BindingFlags.Default | BindingFlags.Public | BindingFlags.Static);
            if(getMethod == null)
            {
                throw new MissingMemberException("The GET accessor method '" + DependencyProperty.ResolveAttachedGetMethodName(propertyName) + "' for the specified property could not be found in type '" + declaringType.FullName + "'");
            }
            setMethod = declaringType.GetMethod(DependencyProperty.ResolveAttachedSetMethodName(propertyName), BindingFlags.Default | BindingFlags.Public | BindingFlags.Static);
            if (setMethod == null)
            {
                throw new MissingMemberException("The SET accessor method '" + DependencyProperty.ResolveAttachedSetMethodName(propertyName) + "' for the specified property could not be found in type '" + declaringType.FullName + "'");
            }
            dependencyProperty = new DependencyProperty(DependencyPropertyType.AttachedProperty, propertyName, propertyType, declaringType, getMethod, setMethod);
            return dependencyProperty;
        }

        /// <summary>
        /// Resolve the name of the specified attached property Get method
        /// </summary>
        /// <param name="propertyName">The case-sensitive name of the property for which to resolve the Get method name</param>
        /// <returns>A string representing the name of the Get method</returns>
        private static string ResolveAttachedGetMethodName(string propertyName)
        {
            return "Get" + propertyName;
        }

        /// <summary>
        /// Resolve the name of the specified attached property Set method
        /// </summary>
        /// <param name="propertyName">The case-sensitive name of the property for which to resolve the Set method name</param>
        /// <returns>A string representing the name of the Get method</returns>
        private static string ResolveAttachedSetMethodName(string propertyName)
        {
            return "Set" + propertyName;
        }

    }

}
