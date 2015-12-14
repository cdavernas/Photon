using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents a <see cref="Bitmap"/> that can be painted to a geometry
    /// </summary>
    public class Texture
        : IDisposable
    {

        /// <summary>
        /// Initializes a new <see cref="Texture"/> based on the specified file/resource <see cref="Uri"/>
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap"/>to create the <see cref="Texture"/> from</param>
        private Texture(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
            this.Target = TextureTarget.Texture2D;
            this.WrapFilter = TextureWrapFilter.Clamp;
            this.MinFilter = TextureMinFilter.Linear;
            this.MagFilter = TextureMagFilter.Linear;
        }

        /// <summary>
        /// Gets an integer representing the id assigned by OpenGL to the <see cref="Texture"/>
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the <see cref="Texture"/>'s <see cref="TextureTarget"/>
        /// </summary>
        public TextureTarget Target { get; private set; }

        /// <summary>
        /// Gets the <see cref="Texture"/>'s <see cref="TextureMinFilter"/>
        /// </summary>
        public TextureMinFilter MinFilter { get; private set; }

        /// <summary>
        /// Gets the <see cref="Texture"/>'s <see cref="TextureMagFilter"/>
        /// </summary>
        public TextureMagFilter MagFilter { get; private set; }

        /// <summary>
        /// Gets the <see cref="Texture"/>'s <see cref="TextureWrapFilter"/>
        /// </summary>
        public TextureWrapFilter WrapFilter { get; private set; }

        /// <summary>
        /// Gets the <see cref="Bitmap"/> the <see cref="Texture"/> has beeen created from
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Texture"/> has been loaded
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Initializes the texture if is hasn't already been done
        /// </summary>
        public void Load()
        {
            System.Drawing.Imaging.BitmapData textureData;
            if (this.IsLoaded)
            {
                return;
            }
            //Generates the texture and retrieve its id
            this.Id = GL.GenTexture();
            //Binds the texture to a its TextureTarget
            GL.BindTexture(this.Target, this.Id);
            //Sets the texture's min filter
            GL.TexParameter(this.Target, TextureParameterName.TextureMinFilter, (int)this.MinFilter);
            //Sets the texture's mag filter
            GL.TexParameter(this.Target, TextureParameterName.TextureMagFilter, (int)this.MagFilter);
            //Sets the texture's wrap filter
            switch (this.WrapFilter)
            {
                case TextureWrapFilter.Clamp:
                    GL.TexParameter(this.Target, TextureParameterName.TextureWrapS, (int)All.Clamp);
                    GL.TexParameter(this.Target, TextureParameterName.TextureWrapT, (int)All.Clamp);
                    break;
                case TextureWrapFilter.ClampToEdge:
                    GL.TexParameter(this.Target, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
                    GL.TexParameter(this.Target, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
                    break;
                case TextureWrapFilter.Repeat:
                    GL.TexParameter(this.Target, TextureParameterName.TextureWrapS, (int)All.Repeat);
                    GL.TexParameter(this.Target, TextureParameterName.TextureWrapT, (int)All.Repeat);
                    break;
            }
            //Creates the 2D texture based on the texture bitmap
            GL.TexImage2D(this.Target, 0, PixelInternalFormat.Rgba, this.Bitmap.Width, this.Bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            //Locks the texture's pixel data in memory
            textureData = this.Bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height), 
                System.Drawing.Imaging.ImageLockMode.ReadOnly, 
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //Uploads the texture's pixel data locked in memory to the texture
            GL.TexSubImage2D(this.Target, 0, 0, 0, this.Bitmap.Width, this.Bitmap.Height, PixelFormat.Bgra, PixelType.UnsignedByte, textureData.Scan0);
            //Releases the texture's pixel data from memory
            this.Bitmap.UnlockBits(textureData);
            //Unbinds the texture
            GL.BindTexture(this.Target, 0);
            //Notifies that the texture has already been loaded
            this.IsLoaded = true;
        }

        /// <summary>
        /// Disposes of the <see cref="Texture"/>
        /// </summary>
        public void Dispose()
        {
            GL.DeleteTexture(this.Id);
        }

        /// <summary>
        /// Returns a <see cref="Texture"/> based on the specified <see cref="Bitmap"/>
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap"/> to create a <see cref="Texture"/> from</param>
        /// <returns>The loaded <see cref="Texture"/></returns>
        public static Texture FromBitmap(Bitmap bitmap)
        {
            Texture texture;
            texture = new Texture(bitmap);
            return texture;
        }

        /// <summary>
        /// Returns a new <see cref="Texture"/> based on the specified <see cref="System.Drawing.Bitmap"/>'s file/resource <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">The file/resource <see cref="Uri"/> of the <see cref="System.Drawing.Bitmap"/> to create the <see cref="Texture"/> from</param>
        /// <returns></returns>
        public static Texture FromUri(Uri uri)
        {
            Stream bitmapStream;
            Bitmap bitmap;
            bitmapStream = ResourceManager.GetResourceStream(uri);
            bitmap = new Bitmap(bitmapStream);
            return Texture.FromBitmap(bitmap);
        }

    }

}
