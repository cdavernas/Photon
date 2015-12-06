using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="System.Drawing.PointF"/> type
    /// </summary>
    public static class PointFExtensions
    {

        /// <summary>
        /// Retrieves the <see cref="Media.Size"/> equivalency of the <see cref="System.Drawing.SizeF"/>
        /// </summary>
        /// <param name="extended">The extended <see cref="System.Drawing.SizeF"/></param>
        /// <returns>The <see cref="Media.Size"/> equivalency of the SizeF</returns>
        public static Media.Size ToMediaSize(this SizeF extended)
        {
            return new Media.Size(extended.Width, extended.Height);
        }

    }

}
