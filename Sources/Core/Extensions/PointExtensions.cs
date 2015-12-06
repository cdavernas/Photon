using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This static class defines extensions methods for the <see cref="System.Drawing.Point"/> type
    /// </summary>
    public static class PointExtensions
    {

        /// <summary>
        /// Retrieves the <see cref="Media.Point"/> equivalency of the <see cref="System.Drawing.Point"/>
        /// </summary>
        /// <param name="extended">The extended <see cref="System.Drawing.Point"/></param>
        /// <returns>The <see cref="Media.Point"/> equivalency of the point</returns>
        public static Media.Point ToMediaPoint(this System.Drawing.Point extended)
        {
            return new Media.Point(extended.X, extended.Y);
        }

    }

}
