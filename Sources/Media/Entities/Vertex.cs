using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// The <see cref="Vertex"/> structure describes the position of a point in 2D or 3D space
    /// </summary>
    public struct Vertex
    {

        /// <summary>
        /// Initializes a new <see cref="Vertex"/> based on both the specified x and y values
        /// </summary>
        /// <param name="x">An object representing the horizontal axis value of the position wrapped by the <see cref="Vertex"/></param>
        /// <param name="y">An object representing the vertical axis value of the position wrapped by the <see cref="Vertex"/></param>
        public Vertex(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }

        /// <summary>
        /// Initializes a new <see cref="Vertex"/> based on both the specified x and y values
        /// </summary>
        /// <param name="x">An object representing the horizontal axis value of the position wrapped by the <see cref="Vertex"/></param>
        /// <param name="y">An object representing the vertical axis value of the position wrapped by the <see cref="Vertex"/></param>
        /// <param name="z">An object representing the depth value of the position wrapped by the <see cref="Vertex"/></param>
        public Vertex(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Gets an double representing the horizontal axis value of the position wrapped by the <see cref="Vertex"/>
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets an double representing the vertical axis value of the position wrapped by the <see cref="Vertex"/>
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets an double representing the depth value of the position wrapped by the <see cref="Vertex"/>
        /// </summary>
        public double Z { get; private set; }

        /// <summary>
        /// Returns a new <see cref="Vertex"/> instance based on the specified <see cref="Point"/>
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to convert to a <see cref="Vertex"/></param>
        public static implicit operator Vertex(Point point)
        {
            return new Vertex(point.X, point.Y);
        }

        /// <summary>
        /// Gets an int representing the <see cref="Vertex"/>'s size, in bytes 
        /// </summary>
        public static int Stride
        {
            get
            {
                return Marshal.SizeOf(new Vertex());
            }
        }

        /// <summary>
        /// Gets the <see cref="VertexPointerType"/> corresponding to the <see cref="Vertex"/> generic type argument
        /// </summary>
        public static VertexPointerType PointerType
        {
            get
            {
                return VertexPointerType.Double; 
            }
        }

    }

}
