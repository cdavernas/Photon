using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents the geometry of a line
    /// </summary>
    public class LineGeometry
        : Geometry
    {

        /// <summary>
        /// Initializes a new instance of the LineGeometry class that has no length
        /// </summary>
        public LineGeometry()
        {

        }

        /// <summary>
        /// 	
        ///Initializes a new instance of the LineGeometry class that has the specified start and end points
        /// </summary>
        /// <param name="startPoint">The <see cref="Media.Point"/> at which the line starts</param>
        /// <param name="endPoint">The <see cref="Media.Point"/> at which the line ends</param>
        public LineGeometry(Media.Point startPoint, Media.Point endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = EndPoint;
        }

        /// <summary>
        /// Gets a <see cref="Rectangle"/> representing the <see cref="LineGeometry"/>'s bounds
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                double x1, y1, x2, y2;
                if (this.Points.Count() < 1)
                {
                    return Rectangle.Empty;
                }
                x1 = this.Points.Min(p => p.X);
                y1 = this.Points.Min(p => p.Y);
                x2 = this.Points.Max(p => p.X);
                y2 = this.Points.Max(p => p.X);
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> containing the <see cref="Point"/>s that define the <see cref="Geometry"/>
        /// </summary>
        public override IEnumerable<Point> Points
        {
            get
            {
                return new Point[] { this.StartPoint, this.EndPoint };
            }
        }

        /// <summary>
        /// Gets/sets the start point of the line
        /// </summary>
        public Media.Point StartPoint { get; private set; }

        /// <summary>
        /// Gets/sets the end point of the line
        /// </summary>
        public Media.Point EndPoint { get; private set; }

        /// <summary>
        /// Determines whether or not the specifiec point is on the line defined by the geometry
        /// </summary>
        /// <param name="point">The <see cref="Media.Point"/> to check</param>
        /// <returns>A boolean indicating whether or not the specifiec point is on the line defined by the geometry</returns>
        public override bool FillContains(Point point)
        {
            if(this.StartPoint == Point.Empty && this.EndPoint == Point.Empty)
            {
                return false;
            }
            return (point.Y - this.StartPoint.Y) / (point.X - this.StartPoint.X) == (this.EndPoint.Y - this.StartPoint.Y) / (this.EndPoint.X - this.StartPoint.X);
        }

    }

}
