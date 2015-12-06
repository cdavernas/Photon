using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon
{

    /// <summary>
    /// This static class provides means to parse a Xaml document<para></para>
    /// This is a very basic - and poor - implementation which's only purpose is to provide fundamental parsing mechanisms. An entirely revised version of the parser will soon appear
    /// </summary>
    /// <remarks>Temporary implementation. Does not support attached properties, bindings, styling, templating, etc. Only basic element tags and attributes are currently supported</remarks>
    internal static class XamlParser
    {

        /// <summary>
        /// The prefix corresponding to the markup namespace
        /// </summary>
        private const string PREFIX_MARKUP = "x";

        /// <summary>
        /// The name of the class attribute
        /// </summary>
        private const string ATTRIBUTE_CLASS_NAME = "Class";
        /// <summary>
        /// The full name of the class attribute (including prefix)
        /// </summary>
        private static string ATTRIBUTE_CLASS_FULLNAME = XamlParser.PREFIX_MARKUP + ":" + XamlParser.ATTRIBUTE_CLASS_NAME;

        private static HashSet<Type> _DependencyElementTypes;
        /// <summary>
        /// Gets a <see cref="HashSet{T}"/> of all the dependency element types
        /// </summary>
        private static HashSet<Type> DependencyElementTypes
        {
            get
            {
                if (XamlParser._DependencyElementTypes == null)
                {
                    Type uiElementType;
                    uiElementType = typeof(IDependencyElement);
                    XamlParser._DependencyElementTypes = new HashSet<Type>();
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        XamlParser._DependencyElementTypes.AddRange(assembly.GetTypes().Where(t => uiElementType.IsAssignableFrom(t)));
                    }
                }
                return XamlParser._DependencyElementTypes;
            }
        }

        /// <summary>
        /// Loads the specified xaml <see cref="Stream"/> and parses it into the expect type
        /// </summary>
        /// <typeparam name="TElement">The expected type of the parsed <see cref="IUIElement"/></typeparam>
        /// <param name="xamlStream">The <see cref="Stream"/> containing the element's Xaml</param>
        /// <returns></returns>
        internal static TElement LoadDependencyElementFrom<TElement>(Stream xamlStream)
            where TElement : IDependencyElement
        {
            XmlDocument xmlDocument;
            IDependencyElement element;
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xamlStream);
            xamlStream.Dispose();
            element = XamlParser.ParseElement(xmlDocument.FirstChild);
            if(element == null)
            {
                return default(TElement);
            }
            if (!typeof(TElement).IsAssignableFrom(element.GetType()))
            {
                throw new InvalidCastException("Cannot cast type '" + element.GetType().Name + "' to type '" + typeof(TElement).Name + "'");
            }
            return (TElement)element;
        }

        /// <summary>
        /// Parses the specified <see cref="XmlNode"/> into a new <see cref="IDependencyElement"/>
        /// </summary>
        /// <param name="xmlNode">The <see cref="XmlNode"/> to parse</param>
        /// <returns>The parsed <see cref="IDependencyElement"/></returns>
        internal static IDependencyElement ParseElement(XmlNode xmlNode)
        {
            Type elementType;
            IDependencyElement element;
            IDependencyElement childElement;
            PropertyInfo property;
            object value;
            elementType = XamlParser.DetermineElementType(xmlNode);
            element = (IDependencyElement)Activator.CreateInstance(elementType);
            foreach (XmlAttribute attribute in xmlNode.Attributes)
            {
                if (attribute.IsMarkupAttribute())
                {
                    continue;
                }
                property = elementType.GetProperty(attribute.Name);
                if (property == null)
                {
                    throw new Exception("The specified property '" + attribute.Name + "' cannot be found in type '" + elementType.FullName + "'");
                }
                value = XamlParser.ParseValue(property, attribute.Value);
                property.SetValue(element, value);
            }
            if (xmlNode.ChildNodes != null)
            {
                if(xmlNode.ChildNodes.Count > 0)
                {
                    if (typeof(Controls.ITextPresenter).IsAssignableFrom(elementType)
                        && xmlNode.FirstChild.NodeType == XmlNodeType.Text)
                    {
                        ((Controls.ITextPresenter)element).Text = xmlNode.FirstChild.InnerText;
                    }
                    else if (typeof(Controls.IPanel).IsAssignableFrom(elementType))
                    {
                        foreach (XmlNode childNode in xmlNode.ChildNodes)
                        {
                            childElement = XamlParser.ParseElement(childNode);
                            ((Controls.IPanel)element).Children.Add((UIElement)childElement);
                        }
                    }
                    else if (typeof(Controls.IDecorator).IsAssignableFrom(elementType)
                        && xmlNode.ChildNodes.Count == 1)
                    {
                        ((Controls.IDecorator)element).Child = (UIElement)XamlParser.ParseElement(xmlNode.FirstChild);
                    }
                }
            }
            return element;
        }

        /// <summary>
        /// Determines the <see cref="IUIElement"/> type for the specified <see cref="XmlNode"/>
        /// </summary>
        /// <param name="xmlNode">The <see cref="XmlNode"/> for which to determine the <see cref="IUIElement"/> type</param>
        /// <returns>The <see cref="IUIElement"/> type of the specified <see cref="XmlNode"/></returns>
        private static Type DetermineElementType(XmlNode xmlNode)
        {
            XmlAttribute attribute;
            string typeName;
            attribute = xmlNode.Attributes[XamlParser.ATTRIBUTE_CLASS_FULLNAME];
            if (attribute == null)
            {
                typeName = xmlNode.Name;
                return XamlParser.DependencyElementTypes.FirstOrDefault(t => t.Name == typeName);
            }
            else
            {
                typeName = attribute.Value;
                return XamlParser.DependencyElementTypes.FirstOrDefault(t => t.FullName == typeName);
            }
        }

        /// <summary>
        /// Parses the specified string value into the type expected by the property passed as parameter
        /// </summary>
        /// <param name="property">The <see cref="PropertyInfo"/> for which to parse the specified value string</param>
        /// <param name="valueString">The string to parse</param>
        /// <returns>The parsed object</returns>
        private static object ParseValue(PropertyInfo property, string valueString)
        {
            Type propertyType;
            TypeConverterAttribute typeConverterAttribute;
            TypeConverter typeConverter;
            propertyType = property.PropertyType;
            if (propertyType.IsEnum)
            {
                object parsed;
                try
                {
                    parsed = Enum.Parse(propertyType, valueString);
                }
                catch (Exception ex)
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + propertyType.FullName + "'", ex);
                }
                return parsed;
            }
            if (propertyType.IsGenericType)
            {
                if (typeof(Nullable<>).IsAssignableFrom(propertyType.GetGenericTypeDefinition()))
                {
                    propertyType = propertyType.GetGenericArguments().First();
                }
            }
            if (propertyType == typeof(string))
            {
                return valueString;
            }
            if (propertyType == typeof(int))
            {
                int parsed;
                if (!int.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + propertyType.FullName + "'");
                }
                return parsed;
            }
            if (propertyType == typeof(double))
            {
                double parsed;
                if (!double.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + propertyType.FullName + "'");
                }
                return parsed;
            }
            if (propertyType == typeof(decimal))
            {
                decimal parsed;
                if (!decimal.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + propertyType.FullName + "'");
                }
                return parsed;
            }
            if (propertyType == typeof(float))
            {
                float parsed;
                if (!float.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + propertyType.FullName + "'");
                }
                return parsed;
            }
            if (propertyType == typeof(Brush))
            {
                Brush parsed;
                Color color;
                typeConverter = new ColorConverter();
                if (!typeConverter.CanConvertFrom(typeof(string)))
                {
                    throw new Exception("The specified TypeConverter for the expected type '" + propertyType.FullName + "' cannot convert from values of type 'System.String'");
                }
                color = (Color)typeConverter.ConvertFrom(valueString);
                parsed = new SolidBrush(color);
                return parsed;
            }
            typeConverterAttribute = propertyType.GetCustomAttribute<TypeConverterAttribute>();
            if (typeConverterAttribute != null)
            {
                object parsed;
                typeConverter = (TypeConverter)Activator.CreateInstance(Type.GetType(typeConverterAttribute.ConverterTypeName));
                if (!typeConverter.CanConvertFrom(typeof(string)))
                {
                    throw new Exception("The specified TypeConverter for the expected type '" + propertyType.FullName + "' cannot convert from values of type 'System.String'");
                }
                parsed = typeConverter.ConvertFrom(valueString);
                return parsed;
            }
            if (typeof(Nullable<>).IsAssignableFrom(property.PropertyType))
            {
                return null;
            }
            throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + propertyType.FullName + "'");
        }

    }

}
