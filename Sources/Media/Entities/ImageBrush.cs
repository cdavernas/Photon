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
        /// Uses the brush to paint a geometry
        /// </summary>
        /// <param name="geometryBounds">The <see cref="Media.Rectangle"/> representing the geometry's bounds</param>
        internal override void Use(Rectangle geometryBounds)
        {

        }

        /// <summary>
        /// Clones the brush
        /// </summary>
        /// <returns>The clone of the <see cref="Brush"/></returns>
        public override Brush Clone()
        {
            return new ImageBrush(this.Image) { Opacity = this.Opacity };
        }

    }

}
