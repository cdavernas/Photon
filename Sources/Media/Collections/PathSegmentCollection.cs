using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents a collection of <see cref="PathSegment"/> objects that can be individually accessed by index
    /// </summary>
    public sealed class PathSegmentCollection
           : Collection<PathSegment>
    {

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of the <see cref="Point"/>s that define the <see cref="PathSegment"/>s contained by the <see cref="PathSegmentCollection"/>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of the <see cref="Point"/>s that define the <see cref="PathSegment"/>s contained by the <see cref="PathSegmentCollection"/></returns>
        public IEnumerable<Point> ToPoints()
        {
            List<Point> points;
            points = new List<Point>();
            foreach (PathSegment segment in this)
            {
                points.AddRange(segment.ToPoints());
            }
            return points;
        }

    }

}
