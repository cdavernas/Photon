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
