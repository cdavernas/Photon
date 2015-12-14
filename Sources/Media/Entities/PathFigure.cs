using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents a subsection of a geometry, a single connected series of two-dimensional geometric segments
    /// </summary>
    public sealed class PathFigure
    {

        /// <summary>
        /// Initializes a new <see cref="PathFigure"/> instance
        /// </summary>
        public PathFigure()
        {
            this.Segments = new PathSegmentCollection();
        }

        /// <summary>
        /// Gets/Sets the <see cref="Media.Point"/> at which the <see cref="PathFigure"/> starts
        /// </summary>
        public Media.Point StartPoint { get; private set; }

        /// <summary>
        /// Gets a collection of the <see cref="PathSegment"/>s the <see cref="PathFigure"/> is made of
        /// </summary>
        public PathSegmentCollection Segments { get; private set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of the <see cref="Point"/>s that define the <see cref="PathSegment"/>s contained by the <see cref="PathFigure"/>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of the <see cref="Point"/>s that define the <see cref="PathSegment"/>s contained by the <see cref="PathFigure"/></returns>
        public IEnumerable<Point> ToPoints()
        {
            return this.Segments.ToPoints();
        }

    }

}
