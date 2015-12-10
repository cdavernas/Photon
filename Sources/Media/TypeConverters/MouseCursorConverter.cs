using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// The <see cref="TypeConverter"/> dedicated to the <see cref="Media.MouseCursor"/> type
    /// </summary>
    public class MouseCursorConverter
        : TypeConverter
    {

        /// <summary>
        /// Returns a boolean indicating whether or not the converter can convert from the specified source type
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/> associated with the request</param>
        /// <param name="sourceType">The source type to check</param>
        /// <returns>A boolean indicating whether or not the converter can convert from the specified source type</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (typeof(string).IsAssignableFrom(sourceType))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Try to convert the specified value into a <see cref="Media.MouseCursor"/>
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/> associated with the request</param>
        /// <param name="culture">The <see cref="CultureInfo"/> associated with the request</param>
        /// <param name="value">The value to convert</param>
        /// <returns>An <see cref="object"/> representing the converted value</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Uri uri;
            MouseCursor cursor;
            uri = new Uri((string)value, UriKind.RelativeOrAbsolute);
            cursor = MouseCursor.FromUri(uri);
            return cursor;
        }

    }

}
