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
        /// Gets a <see cref="Rectangle"/> representing the <see cref="Geometry"/>'s bounds
        /// </summary>
        public abstract Rectangle Bounds { get; }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> containing the <see cref="Point"/>s that define the <see cref="Geometry"/>
        /// </summary>
        public abstract IEnumerable<Point> Points { get; }

        /// <summary>
        /// Returns a boolean indicating whether or not the specified <see cref="Media.Point"/> is contained by the <see cref="Geometry"/>
        /// </summary>
        /// <param name="point">The <see cref="Media.Point"/> to test</param>
        /// <returns></returns>
        public abstract bool FillContains(Point point);

    }

}
