using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    public class BrushConverter
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
            Color color;
            string[] temp;
            byte a, r, g, b;
            Brush brush;
            Uri uri;
            Bitmap bitmap;
            str = (string)value;
            if (str.IsHexColorString())
            {
                color = ColorExtensions.FromHex(str.Substring(1));
                brush = new SolidColorBrush(color);
                return brush;
            }
            if (str.IsArgbColorString())
            {
                temp = str.Replace(" ", "").Split(',');
                if(temp.Length == 3)
                {
                    a = 255;
                    r = byte.Parse(temp[0]);
                    g = byte.Parse(temp[1]);
                    b = byte.Parse(temp[2]);
                }
                else
                {
                    a = byte.Parse(temp[0]);
                    r = byte.Parse(temp[1]);
                    g = byte.Parse(temp[2]);
                    b = byte.Parse(temp[3]);
                }
                color = Color.FromArgb(a, r, g, b);
                brush = new SolidColorBrush(color);
                return brush;
            }
            if(Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out uri))
            {
                bitmap = BitmapExtensions.FromUri(uri);
                brush = new ImageBrush(bitmap);
                return brush;
            }
            throw new Exception("Could not convert the string '" + str + "' to an instance of the 'Brush' type");
        }

    }

}
