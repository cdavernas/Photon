using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// This class defines extensions for the <see cref="string"/> type
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Returns a boolean indicating whether or not the string is numeric
        /// </summary>
        /// <param name="extended">The extended string</param>
        /// <returns>A boolean indicating whether or not the string is numeric</returns>
        public static bool IsNumeric(this string extended)
        {
            foreach(char c in extended)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the string is alphanumeric
        /// </summary>
        /// <param name="extended">The extended string</param>
        /// <returns>A boolean indicating whether or not the string is alphanumeric</returns>
        public static bool IsAlphaNumeric(this string extended)
        {
            foreach(char c in extended)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the string contains an hexadecimal color code
        /// </summary>
        /// <param name="extended">The extended string</param>
        /// <returns>A boolean indicating whether or not the string contains an hexadecimal color code</returns>
        public static bool IsHexColorString(this string extended)
        {
            if (!extended.StartsWith("#"))
            {
                return false;
            }
            switch (extended.Length)
            {
                case 7:
                    if (extended.Substring(1).IsAlphaNumeric())
                    {
                        return true;
                    }
                    break;
                case 9:
                    if (extended.Substring(1).IsAlphaNumeric())
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the string contains an ARGB/RGB color code
        /// </summary>
        /// <param name="extended">The extended string</param>
        /// <returns>A boolean indicating whether or not the string contains an ARGB/RGB color code</returns>
        public static bool IsArgbColorString(this string extended)
        {
            string[] argbValues;
            argbValues = extended.Split(',');
            if (argbValues.Length != 3 && argbValues.Length != 4)
            {
                return false;
            }
            if(!extended.Replace(",", "").Replace(" ", "").IsNumeric())
            {
                return false;
            }
            return true;
        }

    }

}
