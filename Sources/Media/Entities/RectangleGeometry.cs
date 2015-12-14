using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Describes a two-dimensional rectangle
    /// </summary>
    public sealed class RectangleGeometry
        : Geometry
    {

        /// <summary>
        /// The default constructor for the <see cref="RectangleGeometry"/> class
        /// </summary>
        public RectangleGeometry()
        {

        }

        /// <summary>
        /// Initialies a new <see cref="RectangleGeometry"/> based on the specified <see cref="Rectangle"/>
        /// </summary>
        /// <param name="rect"></param>
        public RectangleGeometry(Rectangle rect)
        {
            this.Rect = rect;
        }

        /// <summary>
        /// Gets a <see cref="Rectangle"/> representing the <see cref="RectangleGeometry"/>'s bounds
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                return this.Rect;
            }
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> containing the <see cref="Point"/>s that define the <see cref="Geometry"/>
        /// </summary>
        public override IEnumerable<Point> Points
        {
            get
            {
                return new Point[] 
                {
                    new Point(this.Rect.Left, this.Rect.Top),
                    new Point(this.Rect.Right, this.Rect.Top),
                    new Point(this.Rect.Right, this.Rect.Bottom),
                    new Point(this.Rect.Left, this.Rect.Bottom)
                };
            }
        }

        /// <summary>
        /// Gets/sets the dimensions if the <see cref="RectangleGeometry"/>
        /// </summary>
        public Rectangle Rect { get; set; }

        /// <summary>
        /// Returns a boolean indicating whether or not the specified <see cref="Media.Point"/> is contained by the <see cref="Geometry"/>
        /// </summary>
        /// <param name="point">The <see cref="Media.Point"/> to test</param>
        /// <returns></returns>
        public override bool FillContains(Point point)
        {
            if(point.X >= this.Rect.Left && point.X <= this.Rect.Right
                && point.Y >= this.Rect.Top && point.Y >= this.Rect.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
