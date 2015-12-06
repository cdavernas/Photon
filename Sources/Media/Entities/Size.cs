using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Implements a structure that is used to describe the Size of an object
    /// </summary>
    [TypeConverter(typeof(SizeConverter))]
    public struct Size
    {

        /// <summary>
        /// Initializes a new <see cref="Size"/> with the specified width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Size(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the width of this size instance
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// Gets the height of this size instance
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// Gets the <see cref="SizeF"/> equivalency of the size class
        /// </summary>
        public SizeF ToSizeF()
        {
            return new SizeF(Convert.ToSingle(this.Width), Convert.ToSingle(this.Height));
        }

    }

}
