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
    /// The <see cref="TypeConverter"/> dedicated to the <see cref="Media.Rectangle"/> type
    /// </summary>
    public class RectangleConverter
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
        /// Try to convert the specified value into a <see cref="Media.Rectangle"/>
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/> associated with the request</param>
        /// <param name="culture">The <see cref="CultureInfo"/> associated with the request</param>
        /// <param name="value">The value to convert</param>
        /// <returns>An <see cref="object"/> representing the converted value</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str;
            string[] temp;
            double x, y, width, height;
            str = (string)value;
            temp = str.Replace(" ", "").Split(',');
            if (temp.Length != 4)
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Rectangle type");
            }
            if (!double.TryParse(temp[0], out x))
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Rectangle type");
            }
            if (!double.TryParse(temp[1], out y))
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Rectangle type");
            }
            if (!double.TryParse(temp[1], out width))
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Rectangle type");
            }
            if (!double.TryParse(temp[1], out height))
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Rectangle type");
            }
            return new Rectangle(x, y, width, height);
        }

    }

}
