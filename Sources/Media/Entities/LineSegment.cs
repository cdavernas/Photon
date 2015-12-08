using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Creates a line between two points in a <see cref="PathFigure"/>.
    /// </summary>
    public sealed class LineSegment
        : PathSegment
    {

        /// <summary>
        /// Initializes a new instance of the LineSegment class
        /// </summary>
        public LineSegment()
        {

        }

        /// <summary>
        /// Initializes a new instance of the LineSegment class that has the specified end Point and Boolean that determines whether this LineSegment is stroked.
        /// </summary>
        /// <param name="point">The end point of the line segment</param>
        /// <param name="stroked">Indicates whether or not the line segment is stroked</param>
        public LineSegment(Point point, bool stroked)
            : base(stroked)
        {
            this.Point = point;
        }

        /// <summary>
        /// Identifies the LineSegement's Point <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(LineSegment));
        /// <summary>
        /// Gets or sets the end <see cref="Media.Point"/> of the line segment
        /// </summary>
        public Media.Point Point
        {
            get
            {
                return this.GetValue<Media.Point>(LineSegment.PointProperty);
            }
            set
            {
                this.SetValue(LineSegment.PointProperty, value);
            }
        }

    }

}
