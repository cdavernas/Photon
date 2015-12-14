using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Defines extensions methods for the <see cref="IEnumerable{T}"/> type
    /// </summary>
    public static class IEnumerableExtensions
    {

        /// <summary>
        /// Determines the <see cref="Rectangle"/> bounds of the geometry the <see cref="Point"/> instances held by this enumerable constitute
        /// </summary>
        /// <param name="extended">The extended <see cref="IEnumerable{T}"/> of <see cref="Point"/></param>
        /// <returns>A <see cref="Rectangle"/> representing the bounds of the geometry the <see cref="Point"/> instances held by this enumerable constitute</returns>
        public static Rectangle GetBounds(this IEnumerable<Point> extended)
        {
            double x1, y1, x2, y2;
            x1 = extended.Min(v => v.X);
            y1 = extended.Min(v => v.Y);
            x2 = extended.Max(v => v.X);
            y2 = extended.Max(v => v.Y);
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Returns an array of <see cref="Vertex"/> based on the <see cref="Point"/>s contained by the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="extended">The extended <see cref="IEnumerable{T}"/> of <see cref="Point"/></param>
        /// <returns>An array of <see cref="Vertex"/> based on the <see cref="Point"/>s contained by the <see cref="IEnumerable{T}"/></returns>
        public static Vertex[] ToVertexArray(this IEnumerable<Point> extended)
        {
            Vertex[] vertices;
            vertices = new Vertex[extended.Count()];
            for(int index = 0; index < vertices.Length; index++)
            {
                vertices[index] = extended.ElementAt(index);
            }
            return vertices;
        }

    }

}
