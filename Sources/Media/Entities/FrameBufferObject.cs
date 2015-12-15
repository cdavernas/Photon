using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents an OpenGL object which allows for the creation of user-defined buffer thanks to which one can render to non-Default Framebuffer locations, and thus render without disturbing the main screen.
    /// </summary>
    public class FrameBufferObject
        : IDisposable
    {

        /// <summary>
        /// Initializes a new <see cref="FrameBufferObject"/>
        /// </summary>
        public FrameBufferObject(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets an integer representing the <see cref="FrameBufferObject"/> Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets an integer representing the <see cref="FrameBufferObject"/>'s width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets an integer representing the <see cref="FrameBufferObject"/>'s height
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets an integer representing the <see cref="FrameBufferObject"/>'s Id
        /// </summary>
        public int TextureId { get; private set; }

        /// <summary>
        /// Gets an integer representing the <see cref="FrameBufferObject"/>'s render buffer Id
        /// </summary>
        public int RenderBufferId { get; private set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="FrameBufferObject"/> has been loaded
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Loads the <see cref="FrameBufferObject"/>
        /// </summary>
        public void Load()
        {
            //Create a frame buffer object
            this.Id = GL.GenFramebuffer();
            //Create color texture
            this.TextureId = GL.GenTexture();
            //Bind the color texture
            GL.BindTexture(TextureTarget.Texture2D, this.TextureId);
            //Set the texture filtering
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            //Unbind the color texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
            //Create render buffer
            this.RenderBufferId = GL.GenRenderbuffer();
            //Bind the render buffer
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, this.RenderBufferId);
            //Set the render buffer storage
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent32, this.Width, this.Height);
            //Attach the textures
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.Id);
            //Attach the texture to the frame buffer object
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, this.TextureId, 0);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, this.RenderBufferId);
            //Unbind the render buffer
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            //Unbind the buffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        /// <summary>
        /// Begins using the <see cref="FrameBufferObject"/>
        /// </summary>
        public void BeginUse()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.Id);
            //Draw the texture into the buffer
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
        }

        /// <summary>
        /// Ends using the <see cref="FrameBufferObject"/>
        /// </summary>
        public void EndUse()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        /// <summary>
        /// Dispose of the <see cref="FrameBufferObject"/> and all its resources
        /// </summary>
        public void Dispose()
        {
            GL.DeleteFramebuffer(this.Id);
        }

    }

}
