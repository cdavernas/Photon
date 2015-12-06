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
        /// The default constructor for the <see cref="ImageBrush"/> class
        /// </summary>
        /// <param name="image">A <see cref="Bitmap"/> representing the image to paint</param>
        public ImageBrush(Bitmap image)
        {
            this.Image = image;
        }

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

        internal override void Use(Rectangle geometryBounds)
        {

        }

        public override Brush Clone()
        {
            return new ImageBrush(this.Image) { Opacity = this.Opacity };
        }

    }

}
