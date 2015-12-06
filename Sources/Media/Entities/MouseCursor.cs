using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// This class wraps the <see cref="OpenTK.MouseCursor"/> class<para></para>
    /// Its sole purpose is to provide conversion mechanisms to the wrapped class
    /// </summary>
    [TypeConverter(typeof(MouseCursorConverter))]
    public class MouseCursor
    {

        /// <summary>
        /// The default constructor for the <see cref="MouseCursor"/> class
        /// </summary>
        /// <param name="cursorObject"></param>
        public MouseCursor(OpenTK.MouseCursor cursorObject)
        {
            this.CursorObject = cursorObject;
        }

        /// <summary>
        /// Gets the wrapped <see cref="OpenTK.MouseCursor"/> object
        /// </summary>
        public OpenTK.MouseCursor CursorObject { get; private set; }

        /// <summary>
        /// Creates a <see cref="MouseCursor"/> based on the specified image
        /// </summary>
        /// <param name="cursorUri">The <see cref="Uri"/> of the image</param>
        /// <returns>A <see cref="MouseCursor"/></returns>
        public static MouseCursor FromUri(Uri cursorUri)
        {
            Stream bitmapStream;
            Bitmap bitmap;
            IntPtr hIcon;
            OpenTK.MouseCursor cursorObject;
            MouseCursor cursor;
            bitmapStream = Application.GetResourceStream(cursorUri);
            bitmap = new Bitmap(bitmapStream);
            var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, 32, 32), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            cursorObject = new OpenTK.MouseCursor(0,0,32, 32, data.Scan0);
            bitmap.UnlockBits(data);
            cursor = new MouseCursor(cursorObject);
            return cursor;
        }

        /// <summary>
        /// Gets the default <see cref="MouseCursor"/>
        /// </summary>
        public static MouseCursor Default = new MouseCursor(OpenTK.MouseCursor.Default);

    }

}
