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
    /// The <see cref="Vertex{T}"/> structure describes the position of a point in 2D or 3D space
    /// </summary>
    /// <typeparam name="T">The type of the x, y and z values wrapped by the <see cref="Vertex{T}"/>.<para></para>
    /// The available types to choose from are short, int, double and float</typeparam>
    public struct Vertex<T>
        where T : struct
    {

        /// <summary>
        /// Initializes a new <see cref="Vertex{T}"/> based on both the specified x and y values
        /// </summary>
        /// <param name="x">An object representing the horizontal axis value of the position wrapped by the <see cref="Vertex{T}"/></param>
        /// <param name="y">An object representing the vertical axis value of the position wrapped by the <see cref="Vertex{T}"/></param>
        public Vertex(T x, T y)
        {
            this.X = x;
            this.Y = y;
            this.Z = default(T);
        }

        /// <summary>
        /// Initializes a new <see cref="Vertex{T}"/> based on both the specified x and y values
        /// </summary>
        /// <param name="x">An object representing the horizontal axis value of the position wrapped by the <see cref="Vertex{T}"/></param>
        /// <param name="y">An object representing the vertical axis value of the position wrapped by the <see cref="Vertex{T}"/></param>
        /// <param name="z">An object representing the depth value of the position wrapped by the <see cref="Vertex{T}"/></param>
        public Vertex(T x, T y, T z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Gets an object representing the horizontal axis value of the position wrapped by the <see cref="Vertex{T}"/>
        /// </summary>
        public T X { get; private set; }

        /// <summary>
        /// Gets an object representing the vertical axis value of the position wrapped by the <see cref="Vertex{T}"/>
        /// </summary>
        public T Y { get; private set; }

        /// <summary>
        /// Gets an object representing the depth value of the position wrapped by the <see cref="Vertex{T}"/>
        /// </summary>
        public T Z { get; private set; }

        /// <summary>
        /// Gets an int representing the <see cref="Vertex{T}"/>'s size, in bytes 
        /// </summary>
        public static int Stride
        {
            get
            {
                return Marshal.SizeOf(new Vertex<T>());
            }
        }

        /// <summary>
        /// Gets the <see cref="VertexPointerType"/> corresponding to the <see cref="Vertex{T}"/> generic type argument
        /// </summary>
        public static VertexPointerType PointerType
        {
            get
            {
                if (typeof(int).IsAssignableFrom(typeof(T)))
                {
                    return VertexPointerType.Int;
                }
                else if (typeof(short).IsAssignableFrom(typeof(T)))
                {
                    return VertexPointerType.Short;
                }
                else if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    return VertexPointerType.Double;
                }
                else if (typeof(float).IsAssignableFrom(typeof(T)))
                {
                    return VertexPointerType.Float;
                }
                throw new NotSupportedException("The specified generic Vertex type '" + typeof(T).FullName + "' is not supported. Consider using a Vertex of int, short double or float");
            }
        }

    }

}
