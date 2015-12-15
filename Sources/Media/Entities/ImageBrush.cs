using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// This class represents a brush that paints an area with an image
    /// </summary>
    public sealed class ImageBrush
        : Brush
    {

        /// <summary>
        /// Gets the drawing that the <see cref="ImageBrush"/> is currently used to render
        /// </summary>
        private Drawing _ActiveDrawing;

        /// <summary>
        /// Initializes a new <see cref="ImageBrush"/>
        /// </summary>
        public ImageBrush()
        {

        }

        /// <summary>
        /// The default constructor for the <see cref="ImageBrush"/> class
        /// </summary>
        /// <param name="image">A <see cref="Bitmap"/> representing the image to paint</param>
        public ImageBrush(Bitmap image)
        {
            this.Image = image;
        }

        /// <summary>
        /// Describes the <see cref="ImageBrush.Image"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageBrush));
        /// <summary>
        /// Gets/Sets the <see cref="Bitmap"/> reprenting the image to paint
        /// </summary>
        public Bitmap Image
        {
            get
            {
                return this.GetValue<Bitmap>(ImageBrush.ImageProperty);
            }
            set
            {
                this.SetValue(ImageBrush.ImageProperty, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="Media.Texture"/> created based on the <see cref="Bitmap"/> returned by the <see cref="ImageBrush.Image"/> 
        /// </summary>
        public Texture Texture { get; private set; }

        /// <summary>
        /// When overriden in a class, this method provides means to run code whenever a <see cref="DependencyProperty"/> has changed
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the modified property</param>
        /// <param name="originalValue">An object representing the property's value before the suffered change(s)</param>
        /// <param name="value">An object representing the property's actual (new) value</param>
        protected override void OnPropertyChanged(string propertyName, object originalValue, object value)
        {
            base.OnPropertyChanged(propertyName, originalValue, value);
            if (propertyName == ImageBrush.ImageProperty.Name)
            {
                if (this.Texture != null)
                {
                    this.Texture.Dispose();
                }
                if (value != null)
                {
                    this.Texture = Texture.FromBitmap((Bitmap)value);
                }
                return;
            }
        }

        /// <summary>
        /// Begins using the <see cref="Brush"/> to paint a geometry. Must be followed by a call to the <see cref="Brush.EndUse"/> method
        /// </summary>
        /// <param name="drawing">The <see cref="Drawing"/> to render</param>
        public override void BeginUse(Drawing drawing)
        {
            Rectangle bounds;
            //Sets the drawing as the active drawing
            this._ActiveDrawing = drawing;
            //Make sure that the texture exists
            if (this.Texture == null)
            {
                return;
            }
            //Load the texture if it hasn't already been done
            this.Texture.Load();
            //Gets the bounds of the geometry
            bounds = drawing.Bounds;
            //Clears the buffer
            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //Sets the color, that is white with its alpha multiplied by the brush's opacity
            GL.Color4(Color.FromArgb((byte)(255 * this.Opacity), 255, 255, 255));
            //Clears the stencil
            GL.ClearStencil(0);
            //Sets the stencil mask to 1
            GL.StencilMask(1);
            //Enables stencil test
            GL.Enable(EnableCap.StencilTest);
            //Sets the stencil function to draw the mask
            GL.StencilFunc(StencilFunction.Always, 1, 1);
            //Sets the stencil operation to draw the mask
            GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
        }

        /// <summary>
        /// Ends using the <see cref="Brush"/>
        /// </summary>
        public override void EndUse()
        {
            //Sets the stencil function to draw the masked geometry
            GL.StencilFunc(StencilFunction.Equal, 1, 1);
            //Sets the stencil operation to draw the masked geometry
            GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
            //Enable 2D texturing
            GL.Enable(EnableCap.Texture2D);
            //Binds the TextureId
            GL.BindTexture(TextureTarget.Texture2D, this.Texture.Id);
            //Begins rendering the texture
            GL.Begin(PrimitiveType.Quads);
            //Top left texture coordinate and vertex
            GL.TexCoord2(0, 0);
            GL.Vertex2(this._ActiveDrawing.Bounds.Left, this._ActiveDrawing.Bounds.Top);
            //Top right texture coordinate and vertex
            GL.TexCoord2(1, 0);
            GL.Vertex2(this._ActiveDrawing.Bounds.Right, this._ActiveDrawing.Bounds.Top);
            //Bottom right texture coordinate and vertex
            GL.TexCoord2(1, 1);
            GL.Vertex2(this._ActiveDrawing.Bounds.Right, this._ActiveDrawing.Bounds.Bottom);
            //Bottom left texture coordinate and vertex
            GL.TexCoord2(0, 1);
            GL.Vertex2(this._ActiveDrawing.Bounds.Left, this._ActiveDrawing.Bounds.Bottom);
            //Ends rendering the texture
            GL.End();
            //Unbinds the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
            //Disable 2D texturing
            GL.Disable(EnableCap.Texture2D);
            //Disable the stencil test
            GL.Disable(EnableCap.StencilTest);
            //Clears the active drawing
            this._ActiveDrawing = null;
        }

        /// <summary>
        /// Clones the brush
        /// </summary>
        /// <returns>The clone of the <see cref="Brush"/></returns>
        public override Brush Clone()
        {
            return new ImageBrush(this.Image) { Opacity = this.Opacity };
        }

        /// <summary>
        /// Disposes of the <see cref="ImageBrush"/> and its resources
        /// </summary>
        public override void Dispose()
        {
            this.Image.Dispose();
            this.Texture.Dispose();
            base.Dispose();
        }

    }

}
