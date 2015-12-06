using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    public class ThicknessConverter
        : TypeConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (typeof(string).IsAssignableFrom(sourceType))
            {
                return true;
            }
            return false;
        }

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
