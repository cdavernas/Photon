using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    public class SizeConverter
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
            double width, height;
            str = (string)value;
            temp = str.Replace(" ", "").Split(',');
            if (temp.Length != 2)
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Size type");
            }
            if (!double.TryParse(temp[1], out width))
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Size type");
            }
            if (!double.TryParse(temp[1], out height))
            {
                throw new Exception("The specified string '" + str + "' cannot be parsed into a instance of the Size type");
            }
            return new Size(width, height);
        }

    }

}
