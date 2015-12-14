using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents a complex shape that may be composed of arcs, curves, ellipses, lines, and rectangles
    /// </summary>
    public class PathGeometry
        : Geometry
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PathGeometry"/> class
        /// </summary>
        public PathGeometry()
        {
            this.Figures = new PathFigureCollection();
        }

        /// <summary>
        /// Gets a <see cref="Rectangle"/> representing the <see cref="RectangleGeometry"/>'s bounds
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                double x1, y1, x2, y2;
                if(this.Points.Count() < 1)
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
                return this.Figures.ToPoints();
            }
        }

        /// <summary>
        /// Gets a collection of the <see cref="PathFigure"/>s the <see cref="PathGeometry"/> is made of
        /// </summary>
        public PathFigureCollection Figures { get; private set; }

        /// <summary>
        /// Returns a boolean indicating whether or not the specified <see cref="Media.Point"/> is contained by the <see cref="PathGeometry"/>
        /// </summary>
        /// <param name="point">The <see cref="Media.Point"/> to test</param>
        /// <returns></returns>
        public override bool FillContains(Point point)
        {
            throw new NotImplementedException();
        }

    }

}
