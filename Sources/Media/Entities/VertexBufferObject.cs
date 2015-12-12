using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Also known as VBO, the <see cref="VertexBufferObject{T}"/> provides methods for uploading vertex data (position, normal vector, color, etc.) to the video device for non-immediate-mode rendering. 
    /// VBOs offer substantial performance gains over immediate mode rendering primarily because the data resides in the video device memory rather than the system memory and so it can be rendered directly by the video device
    /// </summary>
    /// <typeparam name="T">The type of the x, y and z values wrapped by the <see cref="Vertex{T}"/> instances to be contained by the <see cref="VertexBufferObject{T}"/></typeparam>
    public class VertexBufferObject<T>
        where T : struct
    {

        /// <summary>
        /// Initializes a new <see cref="VertexBufferObject{T}"/>
        /// </summary>
        public VertexBufferObject(int vertexBufferLength, int elementBufferLength)
        {
            this.VertexBufferLength = vertexBufferLength;
            this.ElementBufferLength = elementBufferLength;
            this.Initialize();
        }

        /// <summary>
        /// Represents the initial length of the VertexBufferObject
        /// </summary>
        private int VertexBufferLength;
        /// <summary>
        /// Represents the initial length of the ElementBufferObject
        /// </summary>
        private int ElementBufferLength;
        /// <summary>
        /// Represents the id of the vertex buffer index
        /// </summary>
        private int VertexBufferObjectId;
        /// <summary>
        /// Represents the id of the element buffer object
        /// </summary>
        private int ElementBufferObjectId;
        /// <summary>
        /// An array of <see cref="Vertex{T}"/> instances contained by the <see cref="VertexBufferObject{T}"/>
        /// </summary>
        private Vertex<T>[] Vertices;
        /// <summary>
        /// Represents the indices contained by the <see cref="VertexBufferObject{T}"/>
        /// </summary>
        private ushort[] Indices;

        /// <summary>
        /// Gets/sets the <see cref="OpenTK.Graphics.OpenGL.PrimitiveType"/> used to render the <see cref="VertexBufferObject{T}"/>
        /// </summary>
        public PrimitiveType PrimitiveType { get; set; }

        /// <summary>
        /// Initializes the <see cref="VertexBufferObject{T}"/>
        /// </summary>
        private void Initialize()
        {
            //Generates the vertex buffer object (VBO)
            GL.GenBuffers(1, out this.VertexBufferObjectId);
            //Binds the VBO, and sets it as a BufferTarget.ArrayBuffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBufferObjectId);
            //Buffers the data associated with the VBO (this.Vertices)
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(this.VertexBufferLength * Vertex<T>.Stride), new Vertex<T>[] { }, BufferUsageHint.StaticDraw);
            //Unbinds the VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //Generate the element buffer object (EBO)
            GL.GenBuffers(1, out this.ElementBufferObjectId);
            //Binds the EBO, and sets it as a BufferTarget.ElementArrayBuffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ElementBufferObjectId);
            //Buffers the data associated with the EBO (this.Indices)
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(this.ElementBufferLength * sizeof(ushort)), new ushort[] { }, BufferUsageHint.StaticDraw);
            //Unbinds the EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        /// <summary>
        /// Replaces the <see cref="Vertex{T}"/> at the specified index by the specified <see cref="Vertex{T}"/>
        /// </summary>
        /// <param name="vertexIndex">The index of the vertex to replace</param>
        /// <param name="vertex">The <see cref="Vertex{T}"/> used to replace the <see cref="Vertex{T}"/> at the specified index</param>
        public void SetVertex(int vertexIndex, Vertex<T> vertex)
        {
            //Updates the local vertex buffer
            this.Vertices[vertexIndex] = vertex;
            //Binds the VertexBufferObject
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBufferObjectId);
            //Updates the VertexBufferObject
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(vertexIndex * Vertex<T>.Stride), new IntPtr(Vertex<T>.Stride), new Vertex<T>[] { vertex });
            //Unbinds the VertexBufferObject
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// Sets the array of <see cref="Vertex{T}"/> associated with the <see cref="VertexBufferObject{T}"/>
        /// </summary>
        /// <param name="vertices">The array of <see cref="Vertex{T}"/> to upload to the <see cref="VertexBufferObject{T}"/></param>
        public void SetVertices(Vertex<T>[] vertices)
        {
            //Updates the local vertex buffer
            this.Vertices = vertices;
            //Binds the VertexBufferObject
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBufferObjectId);
            //Updates the VertexBufferObject
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, new IntPtr(vertices.Length * Vertex<T>.Stride), vertices);
            //Unbinds the VertexBufferObject
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// Replaces the indice at the specified index by the specified value
        /// </summary>
        /// <param name="indiceIndex">The index of the indice to replace</param>
        /// <param name="indice">The indice used to replace the indice at the specified index</param>
        public void SetIndice(int indiceIndex, ushort indice)
        {
            //Updates the local indice buffer
            this.Indices[indiceIndex] = indice;
            //Binds the ElementBufferObject
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ElementBufferObjectId);
            //Updates the ElementBufferObject
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, new IntPtr(this.Indices.Length * sizeof(ushort)), new IntPtr(sizeof(ushort)), new ushort[] { indice });
            //Unbinds the ElementBufferObject
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        /// <summary>
        /// Sets the array of indices associated with the element buffer object
        /// </summary>
        /// <param name="indices">The array of indices to upload to the element buffer object</param>
        public void SetIndices(ushort[] indices)
        {
            //Updates the local indice buffer
            this.Indices = indices;
            //Binds the ElementBufferObject
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ElementBufferObjectId);
            //Updates the ElementBufferObject
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)0, new IntPtr(indices.Length * sizeof(ushort)), indices);
            //Unbinds the ElementBufferObject
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        /// <summary>
        /// Renders the <see cref="VertexBufferObject{T}"/>
        /// </summary>
        public void Render()
        {
            if (this.Vertices == null
                || this.Indices == null)
            {
                return;
            }
            GL.PushMatrix();
            GL.Color4((byte)255, (byte)0, (byte)0, (byte)255);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBufferObjectId);
            GL.VertexPointer(3, Vertex<T>.PointerType, Vertex<T>.Stride, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ElementBufferObjectId);
            GL.DrawElements(this.PrimitiveType, this.Indices.Length, DrawElementsType.UnsignedShort, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.PopMatrix();
        }

    }

}
