using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Photon.Markup
{

    /// <summary>
    /// Provides means to parse xaml markup into Photon <see cref="DependencyObject"/>s
    /// </summary>
    public sealed class XamlParser
        : IHandler
    {

        /// <summary>
        /// The prefix corresponding to the xml namespace
        /// </summary>
        internal const string PREFIX_XML_NAMESPACE = "xmlns";
        /// <summary>
        /// The prefix corresponding to the markup namespace
        /// </summary>
        internal const string PREFIX_MARKUP = "x";

        /// <summary>
        /// The name of the class attribute
        /// </summary>
        private const string ATTRIBUTE_CLASS_NAME = "Class";
        /// <summary>
        /// The name of the key attribute
        /// </summary>
        private const string ATTRIBUTE_KEY_NAME = "Key";
        /// <summary>
        /// The full name of the class attribute (including prefix)
        /// </summary>
        private static string ATTRIBUTE_CLASS_FULLNAME = XamlParser.PREFIX_MARKUP + ":" + XamlParser.ATTRIBUTE_CLASS_NAME;
        /// <summary>
        /// The full name of the key attribute (including prefix)
        /// </summary>
        private static string ATTRIBUTE_KEY_FULLNAME = XamlParser.PREFIX_MARKUP + ":" + XamlParser.ATTRIBUTE_KEY_NAME;

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlParser"/> class
        /// </summary>
        /// <param name="xmlDocument"></param>
        private XamlParser(XmlDocument xmlDocument)
        {
            this.Document = xmlDocument;
            this.Initialize();
        }

        /// <summary>
        /// Gets the <see cref="XamlParser"/>'s underlying <see cref="XmlDocument"/>
        /// </summary>
        public XmlDocument Document { get; private set; }

        /// <summary>
        /// Gets the default <see cref="NamespaceDeclaration"/> in the <see cref="IHandler"/>'s scope
        /// </summary>
        public NamespaceDeclaration DefaultNamespaceDeclaration { get; private set; }

        /// <summary>
        /// Gets a collection of all the <see cref="NamespaceDeclaration"/> in the <see cref="IHandler"/>'s scope
        /// </summary>
        public NamespaceDeclarationCollection NamespaceDeclarations { get; private set; }

        /// <summary>
        /// Gets the xaml document's <see cref="Photon.DependencyElementTree"/>
        /// </summary>
        public DependencyElementTree ElementTree { get; private set; }

        /// <summary>
        /// Gets a dictionary containing the both the hashcode and key of registered element
        /// </summary>
        public Dictionary<int, string> ElementKeys { get; private set; }

        /// <summary>
        /// Initializes the <see cref="XamlParser"/>
        /// </summary>
        private void Initialize()
        {
            NamespaceDeclaration namespaceDeclaration;
            this.NamespaceDeclarations = new NamespaceDeclarationCollection();
            foreach(XmlAttribute attribute in this.Document.FirstChild.Attributes)
            {
                if (!attribute.IsMarkupAttribute())
                {
                    continue;
                }
                namespaceDeclaration = new NamespaceDeclaration(attribute.LocalName, attribute.Value);
                if (attribute.Name == XamlParser.PREFIX_XML_NAMESPACE)
                {
                    this.DefaultNamespaceDeclaration = namespaceDeclaration;
                }
                else
                {
                    this.NamespaceDeclarations.Add(namespaceDeclaration);
                }
            }
            this.ElementKeys = new Dictionary<int, string>();
        }

        /// <summary>
        /// Parses the xaml document
        /// </summary>
        /// <returns>The root <see cref="IDependencyElement"/> of the parsed xaml document</returns>
        private IDependencyElement Parse()
        {
            XmlNode rootNode;
            Type elementType;
            DependencyObject rootElement;
            XamlParserContext context;
            object parsedChild;
            rootNode = this.Document.FirstChild;
            elementType = this.ElementTypeOf(rootNode);
            rootElement = (DependencyObject)Activator.CreateInstance(elementType, new object[] { });
            this.ElementTree = new DependencyElementTree(rootElement);
            context = new XamlParserContext(this);
            Markup.Context.Current = context;
            foreach (XmlAttribute attribute in rootNode.Attributes)
            {
                this.ParseNodeAttribute(context, attribute, rootElement);
            }
            foreach(XmlNode propertyNode in rootNode.ChildNodes.Where(n => n.IsPropertyNodeOf(rootNode)))
            {
                this.ParsePropertyNode(context, rootElement, propertyNode);
            }
            if (typeof(IAddChild).IsAssignableFrom(rootElement.GetType()))
            {
                foreach (XmlNode childNode in rootNode.ChildNodes.Where(n => !n.IsPropertyNodeOf(rootNode)))
                {
                    switch (childNode.NodeType)
                    {
                        case XmlNodeType.Text:
                            ((IAddChild)rootElement).AddText(childNode.InnerText);
                            break;
                        default:
                            parsedChild = this.ParseNode(context, childNode, (IAddChild)rootElement);
                            break;
                    }
                }
            }
            return rootElement;
        }

        /// <summary>
        /// Parses the specified <see cref="XmlNode"/>
        /// </summary>
        /// <param name="context">The <see cref="XamlParserContext"/> for which to parse the <see cref="XmlNode"/></param>
        /// <param name="xmlNode">The <see cref="XmlNode"/> to parse</param>
        /// <param name="parentNode">The parsed parent element of the <see cref="XmlNode"/> to parse</param>
        /// <returns>The parsed <see cref="IDependencyElement"/></returns>
        private object ParseNode(XamlParserContext context, XmlNode xmlNode, IAddChild parentNode)
        {
            Type elementType;
            object parsedElement;
            XmlAttribute keyAttribute;
            object parsedChild;
            elementType = this.ElementTypeOf(xmlNode);
            parsedElement = Activator.CreateInstance(elementType, new object[] { });
            keyAttribute = xmlNode.Attributes.FirstOrDefault(a => a.Name == XamlParser.ATTRIBUTE_KEY_FULLNAME);
            if(keyAttribute != null)
            {
                this.ElementKeys.Add(parsedElement.GetHashCode(), keyAttribute.Value);
            }
            if(parentNode != null)
            {
                parentNode.AddChild(parsedElement);
            }
            foreach(XmlAttribute attribute in xmlNode.Attributes)
            {
                this.ParseNodeAttribute(context, attribute, parsedElement);
            }
            foreach (XmlNode propertyNode in xmlNode.ChildNodes.Where(n => n.IsPropertyNodeOf(xmlNode)))
            {
                this.ParsePropertyNode(context, parsedElement, propertyNode);
            }
            if (typeof(IAddChild).IsAssignableFrom(parsedElement.GetType()))
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes.Where(n => !n.IsPropertyNodeOf(xmlNode)))
                {
                    switch (childNode.NodeType)
                    {
                        case XmlNodeType.Text:
                            ((IAddChild)parsedElement).AddText(childNode.InnerText);
                            break;
                        default:
                            parsedChild = this.ParseNode(context, childNode, (IAddChild)parsedElement);
                            break;
                    }
                }
            }
            return parsedElement;
        }

        /// <summary>
        /// Parses the specified property <see cref="XmlNode"/>
        /// </summary>
        /// <param name="context">The <see cref="XamlParserContext"/> for which to parse the property <see cref="XmlNode"/></param>
        /// <param name="parsedElement">The <see cref="IDependencyElement"/> to which the property to parse belongs to</param>
        /// <param name="xmlNode">The <see cref="XmlNode"/> to parse</param>
        private void ParsePropertyNode(XamlParserContext context, object parsedElement, XmlNode xmlNode)
        {
            string propertyName;
            object propertyValue;
            PropertyInfo property;
            ICollection collection;
            propertyName = xmlNode.Name.Split('.').Last();
            property = parsedElement.GetType().GetProperty(propertyName);
            if(property == null)
            {
                throw new NullReferenceException("The specified DependencyProperty '" + propertyName + "' does not exist in type '" + parsedElement.GetType().FullName + "'");
            }
            propertyValue = this.ParseNode(context, xmlNode.FirstChild, null);
            if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
            {
                collection = (ICollection)property.GetValue(parsedElement);
                if(collection == null)
                {
                    throw new NullReferenceException("The ICollection returned by the '" + parsedElement.GetType().FullName + "." + property.Name + "' property was null");
                }
                typeof(ICollection<>).MakeGenericType(new Type[] { collection.GetType().GetGenericArguments().First() }).GetMethod("Add").Invoke(collection, new object[] { propertyValue });
            }
            else
            {
                property.SetValue(parsedElement, propertyValue);
            }
        }

        /// <summary>
        /// Parses the specified <see cref="XmlAttribute"/>
        /// </summary>
        /// <param name="context">The <see cref="XamlParserContext"/> for which to parse the <see cref="XmlAttribute"/></param>
        /// <param name="attribute">The <see cref="XmlAttribute"/> to parse</param>
        /// <param name="parsedElement">The parsed object for which to parse the specified <see cref="XmlAttribute"/></param>
        private void ParseNodeAttribute(XamlParserContext context, XmlAttribute attribute, object parsedElement)
        {
            MemberInfo[] members;
            MemberInfo memberInfo;
            PropertyInfo propertyInfo;
            object propertyValue;
            EventInfo eventInfo;
            MethodInfo delegateMethod;
            IUIElement rootElement;
            if (attribute.IsMarkupAttribute())
            {
                if(attribute.Name == XamlParser.ATTRIBUTE_KEY_FULLNAME)
                {
                    return;
                }
                return;
            }
            members = parsedElement.GetType().GetMember(attribute.LocalName);
            if(members.Length < 1)
            {
                throw new MissingMemberException("The member '" + attribute.LocalName + "' cannot be found in type '" + parsedElement.GetType().FullName + "'");
            }
            memberInfo = members[0];
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Property:
                    propertyInfo = parsedElement.GetType().GetProperty(attribute.Name);
                    if (propertyInfo == null)
                    {
                        throw new Exception("The specified property '" + attribute.Name + "' cannot be found in type '" + parsedElement.GetType().FullName + "'");
                    }
                    if (typeof(Setter).IsAssignableFrom(parsedElement.GetType()))
                    {
                        if (typeof(DependencyProperty).IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            propertyValue = this.ParseDependencyProperty(context, parsedElement, attribute.Value);
                        }
                        else
                        {
                            propertyValue = this.ParseValue(context, ((Setter)parsedElement).Property.PropertyType, attribute.Value);
                        }
                    }
                    else
                    {
                        propertyValue = this.ParseValue(context, propertyInfo.PropertyType, attribute.Value);
                    }
                    propertyInfo.SetValue(parsedElement, propertyValue);
                    break;
                case MemberTypes.Event:
                    eventInfo = (EventInfo)memberInfo;
                    delegateMethod = parsedElement.GetType().GetMethod(attribute.Value, BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    rootElement = null;
                    if (delegateMethod == null)
                    {
                        rootElement = (IUIElement)this.ElementTree.Root;
                        delegateMethod = rootElement.GetType().GetMethod(attribute.Value, BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    }
                    if(delegateMethod == null)
                    {
                        throw new MissingMemberException("Could not find the delegate method '" + attribute.Value + "' assigned to the '" + parsedElement.GetType().FullName + "." + eventInfo.Name + "' event");
                    }
                    if(rootElement == null)
                    {
                        eventInfo.AddEventHandler(parsedElement, delegateMethod.CreateDelegate(eventInfo.EventHandlerType, parsedElement));
                    }
                    else
                    {
                        eventInfo.AddEventHandler(parsedElement, delegateMethod.CreateDelegate(eventInfo.EventHandlerType, rootElement));
                    }
                    break;
                default:
                    throw new NotSupportedException("The specified member type ('" + memberInfo.MemberType.ToString() + "') is not supported");
            }
        }

        /// <summary>
        /// Parses the specified string into the expected type
        /// </summary>
        /// <param name="context">The <see cref="XamlParserContext"/> for which to parse the value</param>
        /// <param name="expectedType">The expected <see cref="Type"/> of the returned value</param>
        /// <param name="valueString">The value to parse</param>
        /// <returns>An <see cref="object"/> representing the parsed value</returns>
        private object ParseValue(XamlParserContext context, Type expectedType, string valueString)
        {
            StaticResource staticResource;
            TypeMarkupExtension typeExtension;
            TypeConverterAttribute typeConverterAttribute;
            TypeConverter typeConverter;
            if (StaticResource.TryParse(valueString, out staticResource))
            {
                return staticResource.ProvideValue(context);
            }
            if(TypeMarkupExtension.TryParse(valueString, out typeExtension))
            {
                return typeExtension.ProvideValue(context);
            }
            if (expectedType.IsEnum)
            {
                object parsed;
                try
                {
                    parsed = Enum.Parse(expectedType, valueString);
                }
                catch (Exception ex)
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + expectedType.FullName + "'", ex);
                }
                return parsed;
            }
            if (expectedType.IsGenericType)
            {
                if (typeof(Nullable<>).IsAssignableFrom(expectedType.GetGenericTypeDefinition()))
                {
                    expectedType = expectedType.GetGenericArguments().First();
                }
            }
            if (expectedType == typeof(string))
            {
                return valueString;
            }
            if (expectedType == typeof(int))
            {
                int parsed;
                if (!int.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + expectedType.FullName + "'");
                }
                return parsed;
            }
            if (expectedType == typeof(double))
            {
                double parsed;
                if (!double.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + expectedType.FullName + "'");
                }
                return parsed;
            }
            if (expectedType == typeof(decimal))
            {
                decimal parsed;
                if (!decimal.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + expectedType.FullName + "'");
                }
                return parsed;
            }
            if (expectedType == typeof(float))
            {
                float parsed;
                if (!float.TryParse(valueString, out parsed))
                {
                    throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + expectedType.FullName + "'");
                }
                return parsed;
            }
            typeConverterAttribute = expectedType.GetCustomAttribute<TypeConverterAttribute>();
            if (typeConverterAttribute != null)
            {
                object parsed;
                typeConverter = (TypeConverter)Activator.CreateInstance(Type.GetType(typeConverterAttribute.ConverterTypeName));
                if (!typeConverter.CanConvertFrom(typeof(string)))
                {
                    throw new Exception("The specified TypeConverter for the expected type '" + expectedType.FullName + "' cannot convert from values of type 'System.String'");
                }
                parsed = typeConverter.ConvertFrom(valueString);
                return parsed;
            }
            if (typeof(Nullable<>).IsAssignableFrom(expectedType))
            {
                return null;
            }
            throw new FormatException("The value '" + valueString + "' could not be parsed into the expected type '" + expectedType.FullName + "'");
        }

        /// <summary>
        /// Parses the specified string into a <see cref="DependencyProperty"/>
        /// </summary>
        /// <param name="context">The <see cref="XamlParserContext"/> for which to parse the value</param>
        /// <param name="parsedElement">The parsed element to parse the <see cref="DependencyProperty"/> for</param>
        /// <param name="valueString">The value to parse</param>
        /// <returns>The parsed <see cref="DependencyProperty"/></returns>
        private DependencyProperty ParseDependencyProperty(XamlParserContext context, object parsedElement, string valueString)
        {
            SetterBase styleSetter;
            Type targetType;
            DependencyProperty dependencyProperty;
            if (!typeof(SetterBase).IsAssignableFrom(parsedElement.GetType()))
            {
                throw new NotSupportedException("Cannot parse the specified value into a DependencyProperty");
            }
            styleSetter = (SetterBase)parsedElement;
            if(styleSetter.Style.TargetType == null)
            {
                targetType = typeof(UIElement);
            }
            else
            {
                targetType = styleSetter.Style.TargetType;
            }
            valueString += "Property";
            dependencyProperty = (DependencyProperty)targetType.GetField(valueString, BindingFlags.Default | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);
            return dependencyProperty;
        }

        /// <summary>
        /// Determines the type equivalency based on both the specified namespace prefix and type name
        /// </summary>
        /// <param name="namespacePrefix">The prefix of the referenced type's namespace</param>
        /// <param name="typeName">The name of the referenced type</param>
        /// <returns>The <see cref="Type"/> equivalency based on both the specified namespace prefix and type name</returns>
        public Type ElementTypeOf(string namespacePrefix, string typeName)
        {
            NamespaceDeclaration namespaceDeclaration;
            if (string.IsNullOrWhiteSpace(namespacePrefix))
            {
                namespaceDeclaration = this.DefaultNamespaceDeclaration;
            }
            else
            {
                namespaceDeclaration = this.NamespaceDeclarations.FirstOrDefault(ns => ns.Prefix == namespacePrefix);
            }
            if (namespaceDeclaration == null)
            {
                throw new NullReferenceException("The specified namespace prefix '" + namespacePrefix + "' has not been registered into the IHandler's scope");
            }
            typeName = namespaceDeclaration.GetReferenceNamespace() + "." + typeName;
            typeName += ", " + namespaceDeclaration.GetReferencedAssembly().GetName().Name;
            return Type.GetType(typeName);
        }

        /// <summary>
        /// Determines the <see cref="DependencyObject"/>'s type of the specified <see cref="XmlNode"/>
        /// </summary>
        /// <param name="xmlNode">The <see cref="XmlNode"/> to retrieve the <see cref="DependencyObject"/>'s equivalency for</param>
        /// <returns>The <see cref="Type"/> equivalency of the specified <see cref="XmlNode"/></returns>
        private Type ElementTypeOf(XmlNode xmlNode)
        {
            XmlAttribute classAttribute;
            NamespaceDeclaration namespaceDeclaration;
            string typeName;
            Type elementType;
            classAttribute = null;
            try
            {
                classAttribute = xmlNode.Attributes[XamlParser.ATTRIBUTE_CLASS_FULLNAME];
            }
            catch { }
            if(classAttribute == null)
            {
                if (string.IsNullOrWhiteSpace(xmlNode.Prefix))
                {
                    namespaceDeclaration = this.DefaultNamespaceDeclaration;
                }
                else
                {
                    namespaceDeclaration = this.NamespaceDeclarations.FirstOrDefault(ns => ns.Prefix == xmlNode.Prefix);
                }
                if (namespaceDeclaration == null)
                {
                    throw new NullReferenceException("The specified namespace prefix '" + xmlNode.Prefix + "' has not been registered into the IHandler's scope");
                }
                typeName = namespaceDeclaration.GetReferenceNamespace() + "." + xmlNode.LocalName;
                typeName += ", " + namespaceDeclaration.GetReferencedAssembly().GetName().Name;
                elementType = Type.GetType(typeName);
            }
            else
            {
                typeName = classAttribute.Value;
                elementType = null;
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    elementType = assembly.GetTypes().FirstOrDefault(t => t.FullName == typeName);
                    if(elementType != null)
                    {
                        break;
                    }
                }
            }
            return elementType;
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
            XamlParser xamlParser;
            IDependencyElement element;
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xamlStream);
            xamlStream.Dispose();
            xamlParser = new XamlParser(xmlDocument);
            element = xamlParser.Parse();
            if (element == null)
            {
                return default(TElement);
            }
            if (!typeof(TElement).IsAssignableFrom(element.GetType()))
            {
                throw new InvalidCastException("Cannot cast type '" + element.GetType().Name + "' to type '" + typeof(TElement).Name + "'");
            }
            return (TElement)element;
        }

    }

}
