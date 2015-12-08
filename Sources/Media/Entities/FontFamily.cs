using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// This class is a wrapper for the <see cref="System.Drawing.FontFamily"/> class.<para></para>
    /// Its sole purpose is to provide conversion mechanisms to the wrapped class
    /// </summary>
    [TypeConverter(typeof(FontFamilyConverter))]
    public class FontFamily
    {

        /// <summary>
        /// The default constructor for the <see cref="FontFamily"/> type
        /// </summary>
        /// <param name="name">The name of the font family</param>
        public FontFamily(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the font family's name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Converts the <see cref="FontFamily"/> to its <see cref="System.Drawing.FontFamily"/> counterpart
        /// </summary>
        /// <returns>The GDI+ <see cref="System.Drawing.FontFamily"/></returns>
        public System.Drawing.FontFamily ToGdiFontFamily()
        {
            return new System.Drawing.FontFamily(this.Name); 
        }

        /// <summary>
        /// Gets the default <see cref="Media.FontFamily"/> (Arial)
        /// </summary>
        public static Media.FontFamily Default
        {
            get
            {
                return new FontFamily("Arial");
            }
        }

    }

}
