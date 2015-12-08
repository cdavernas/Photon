using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents a set of line segments defined by a <see cref="PointCollection"/> with each <see cref="Media.Point"/> specifying the end point of a line segment
    /// </summary>
    public sealed class PolyLineSegment
    {

        /// <summary>
        /// Initializes a new <see cref="PolyLineSegment"/> instance
        /// </summary>
        public PolyLineSegment()
        {
            this.Points = new PointCollection();
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="Media.Point"/> structures that defines this <see cref="PolyLineSegment"/> object
        /// </summary>
        public PointCollection Points { get; private set; }

    }

}
