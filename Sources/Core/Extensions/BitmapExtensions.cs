using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="Bitmap"/> type
    /// </summary>
    public static class BitmapExtensions
    {

        /// <summary>
        /// Creates a new <see cref="Bitmap"/> instance based on the specified <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> thanks to which to create the new <see cref="Bitmap"/></param>
        /// <returns>A new instance of the <see cref="Bitmap"/> class</returns>
        public static Bitmap FromUri(Uri uri)
        {
            Stream stream;
            Bitmap bitmap;
            try
            {
                stream = Application.GetResourceStream(uri);
                bitmap = new Bitmap(stream);
                return bitmap;
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured while loading the bitmap from the specified uri '" + uri.ToString() + "'. See inner exception for more details", ex);
            }
        }

    }

}
