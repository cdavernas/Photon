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
    /// Stores a set of four double numbers that represent the location and size of a rectangle
    /// </summary>
    [TypeConverter(typeof(RectangleConverter))]
    public struct Rectangle
    {

        /// <summary>
        /// Initializes a new <see cref="Rectangle"/> instance base on the specified x, y, width and height
        /// </summary>
        /// <param name="x">The horizontal position of the rectangle</param>
        /// <param name="y">The vertical position of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        public Rectangle(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Initializes a new <see cref="Rectangle"/> instance base on the specified x, y and size
        /// </summary>
        /// <param name="x">The horizontal position of the rectangle</param>
        /// <param name="y">The vertical position of the rectangle</param>
        /// <param name="size">A <see cref="Size"/> instance representing the size of the rectangle</param>
        public Rectangle(double x, double y, Size size)
        {
            this.X = x;
            this.Y = y;
            this.Width = size.Width;
            this.Height = size.Height;
        }

        /// <summary>
        /// Initializes a new <see cref="Rectangle"/> instance base on the specified position and size
        /// </summary>
        /// <param name="position">A <see cref="Point"/> instance representing the position of the rectangle</param>
        /// <param name="size">A <see cref="Media.Size"/> instance representing the size of the rectangle</param>
        public Rectangle(Point position, Size size)
        {
            this.X = position.X;
            this.Y = position.Y;
            this.Width = size.Width;
            this.Height = size.Height;
        }

        /// <summary>
        /// Gets the horizontal position of the rectangle
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the vertical position of the rectangle
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the rectangle's width
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// Gets the rectangle's height
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// Gets the rectangle's left, which is the horizontal position of the rectangle
        /// </summary>
        public double Left
        {
            get
            {
                return this.X;
            }
        }

        /// <summary>
        /// Gets the rectangle's top, which is the vertical position of the rectangle
        /// </summary>
        public double Top
        {
            get
            {
                return this.Y;
            }
        }

        /// <summary>
        /// Gets the the rectangle's right, which is equals to the <see cref="Rectangle.Left"/> property added to the <see cref="Rectangle.Width"/> property
        /// </summary>
        public double Right
        {
            get
            {
                return this.Left + this.Width;
            }
        }

        /// <summary>
        /// Gets the the rectangle's bottom, which is equals to the <see cref="Rectangle.Top"/> property added to the <see cref="Rectangle.Height"/> property
        /// </summary>
        public double Bottom
        {
            get
            {
                return this.Top + this.Height;
            }
        }

        /// <summary>
        /// Gets a <see cref="Media.Point"/> representing the rectangle's position
        /// </summary>
        public Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
        }

        /// <summary>
        /// Gets a <see cref="Media.Size"/> representing the rectangle's size
        /// </summary>
        public Size Size
        {
            get
            {
                return new Size(this.Width, this.Height);
            }
        }

        /// <summary>
        /// Determines whether or not the specified point is within the rectangle's bound
        /// </summary>
        /// <param name="point">The <see cref="Media.Point"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified point is within the rectangle's bound</returns>
        public bool Contains(Point point)
        {
            if (point.X < this.Left 
                || point.X > this.Right 
                || point.Y < this.Top 
                || point.Y > this.Bottom)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the <see cref="System.Drawing.RectangleF"/> equivalency of the rectangle
        /// </summary>
        /// <returns></returns>
        public RectangleF ToRectangleF()
        {
            float x, y, width, height;
            x = Convert.ToSingle(this.X);
            y = Convert.ToSingle(this.Y);
            width = Convert.ToSingle(this.Width);
            height = Convert.ToSingle(this.Height);
            return new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// An empty (default) instance of the rectangle struct
        /// </summary>
        /// <returns></returns>
        public static Rectangle Empty()
        {
            return new Rectangle();
        }

    }

}
