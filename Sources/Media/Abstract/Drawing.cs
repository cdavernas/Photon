using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Abstract class that describes a 2-D drawing. This class cannot be inherited by your code.
    /// </summary>
    public abstract class Drawing
        : DependencyObject, IDisposable
    {

        /// <summary>
        /// Gets a <see cref="Rectangle"/> representing the <see cref="Drawing"/>'s bounds
        /// </summary>
        public abstract Rectangle Bounds { get; }

        /// <summary>
        /// Genereates the <see cref="Drawing"/>'s <see cref="Media.VertexBufferObject"/>
        /// </summary>
        protected abstract void GenerateBuffer();

        /// <summary>
        /// Gets the <see cref="Media.VertexBufferObject"/> associated with the <see cref="Drawing"/>
        /// </summary>
        protected VertexBufferObject VertexBufferObject { get; set; }

        /// <summary>
        /// Forces the <see cref="Drawing"/>'s <see cref="Media.VertexBufferObject"/> to render
        /// </summary>
        internal void Render()
        {
            if(this.VertexBufferObject == null)
            {
                this.GenerateBuffer();
            }
            if (this.VertexBufferObject == null)
            {
                return;
            }
            this.VertexBufferObject.Render();
        }

        /// <summary>
        /// Disposes of the <see cref="Drawing"/> and of all its resources
        /// </summary>
        public virtual void Dispose()
        {
            if(this.VertexBufferObject != null)
            {
                this.VertexBufferObject.Dispose();
            }
        }

    }

}
