using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents an ordered pair of double x and y coordinates that defines a point in a two-dimensional plane
    /// </summary>
    [TypeConverter(typeof(PointConverter))]
    public struct Point
    {

        /// <summary>
        /// The default constructor for the <see cref="Point"/> class
        /// </summary>
        /// <param name="x">The horizontal position of the point</param>
        /// <param name="y">The vertical position of the point</param>
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets the horizontal position of the point
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the vertical position of the point
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the point's <see cref="System.Drawing.PointF"/> equivalency
        /// </summary>
        /// <returns>The <see cref="System.Drawing.PointF"/> equivalency of the point</returns>
        public System.Drawing.PointF ToPointF()
        {
            float x, y;
            x = Convert.ToSingle(this.X);
            y = Convert.ToSingle(this.Y);
            return new System.Drawing.PointF(x, y);
        }

        /// <summary>
        /// Determines whether the <see cref="Point"/> equals the specified object
        /// </summary>
        /// <param name="obj">The object to check for equality with the <see cref="Point"/></param>
        /// <returns>A boolean indicating whether or not the <see cref="Point"/> equals the specified object</returns>
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if (!typeof(Point).IsAssignableFrom(obj.GetType()))
            {
                return false;
            }
            if(((Point)obj).X != this.X 
                || ((Point)obj).Y != this.Y)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the hashcode for this instance
        /// </summary>
        /// <returns>The instance's hashcode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// A + operator for the <see cref="Point"/> class
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>The <see cref="Point"/> resulting from the addition</returns>
        public static Point operator +(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        /// <summary>
        /// A - operator for the <see cref="Point"/> class
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>The <see cref="Point"/> resulting from the substraction</returns>
        public static Point operator -(Point point1, Point point2)
        {
            return new Point(point1.X - point2.X, point1.Y - point2.Y);
        }

        /// <summary>
        /// A == operator for the <see cref="Point"/> class
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>A boolean indicating whether or not the point1 equals the point2</returns>
        public static bool operator==(Point point1, Point point2)
        {
            if(point1.X == point2.X && point1.Y == point2.Y)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// A != operator for the <see cref="Point"/> class
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>A boolean indicating whether or not the point1 equals the point2</returns>
        public static bool operator !=(Point point1, Point point2)
        {
            if (point1.X == point2.X && point1.Y == point2.Y)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets an empty <see cref="Media.Point"/>
        /// </summary>
        public static Point Empty
        {
            get
            {
                return new Point();
            }
        }

    }

}
