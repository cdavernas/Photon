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
    /// The <see cref="TypeConverter"/> dedicated to the <see cref="Media.Thickness"/> type
    /// </summary>
    public class ThicknessConverter
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
        /// Try to convert the specified value into a <see cref="Media.Thickness"/>
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/> associated with the request</param>
        /// <param name="culture">The <see cref="CultureInfo"/> associated with the request</param>
        /// <param name="value">The value to convert</param>
        /// <returns>An <see cref="object"/> representing the converted value</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str;
            string[] temp;
            double left, top, right, bottom;
            str = (string)value;
            temp = str.Replace(" ", "").Split(',');
            switch (temp.Length)
            {
                case 1:
                    if(!double.TryParse(temp[0], out left))
                    {
                        throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
                    }
                    return new Thickness(left);
                case 2:
                    if (!double.TryParse(temp[0], out left))
                    {
                        throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
                    }
                    if (!double.TryParse(temp[1], out top))
                    {
                        throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
                    }
                    return new Thickness(left, top);
                case 4:
                    if (!double.TryParse(temp[0], out left))
                    {
                        throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
                    }
                    if (!double.TryParse(temp[1], out top))
                    {
                        throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
                    }
                    if (!double.TryParse(temp[2], out right))
                    {
                        throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
                    }
                    if (!double.TryParse(temp[3], out bottom))
                    {
                        throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
                    }
                    return new Thickness(left, top, right, bottom);
                default:
                    throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Thickness type");
            }
        }

    }

}
