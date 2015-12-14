using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents a collection of <see cref="PathFigure"/> objects that collectively make up the geometry of a <see cref="PathGeometry"/>
    /// </summary>
    public sealed class PathFigureCollection
        : Collection<PathFigure>
    {

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of the <see cref="Point"/>s that define the <see cref="PathFigure"/>s contained by the <see cref="PathFigureCollection"/>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of the <see cref="Point"/>s that define the <see cref="PathFigure"/>s contained by the <see cref="PathFigureCollection"/></returns>
        public IEnumerable<Point> ToPoints()
        {
            List<Point> points;
            points = new List<Point>();
            foreach(PathFigure figure in this)
            {
                points.AddRange(figure.ToPoints());
            }
            return points;
        }

    }

}
