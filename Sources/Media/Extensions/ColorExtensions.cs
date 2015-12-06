using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// This class defines extention methods for the <see cref="System.Drawing.Color"/> type
    /// </summary>
    public static class ColorExtensions
    {

        /// <summary>
        /// Returns a new <see cref="System.Drawing.Color"/> instance based on the specified hex color string
        /// </summary>
        /// <param name="hexColorString">A string containing an hexadecimal color code</param>
        /// <returns>A new <see cref="System.Drawing.Color"/> instance based on the specified hex color string</returns>
        public static Color FromHex(string hexColorString)
        {
            hexColorString = hexColorString.Replace("#", "");
            int argb = Int32.Parse(hexColorString, NumberStyles.HexNumber);
            return Color.FromArgb(argb);
        }

    }

}
