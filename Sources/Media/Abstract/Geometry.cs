using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Classes that derive from this abstract base class define geometric shapes. Geometry objects can be used for clipping, hit-testing, and rendering 2-D graphic data
    /// </summary>
    public abstract class Geometry
    {

        /// <summary>
        /// Returns a boolean indicating whether or not the specified <see cref="Media.Point"/> is contained by the <see cref="Geometry"/>
        /// </summary>
        /// <param name="point">The <see cref="Media.Point"/> to test</param>
        /// <returns></returns>
        public abstract bool FillContains(Point point);

    }

}
