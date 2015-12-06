using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Describes the thickness of a frame around a rectangle. Four Double values describe the Left, Top, Right, and Bottom sides of the rectangle, respectively.
    /// </summary>
    [TypeConverter(typeof(ThicknessConverter))]
    public struct Thickness
    {

        /// <summary>
        /// Initializes a new <see cref="Thickness"/> with Left, Top, Right and Bottom properties set to the specified value
        /// </summary>
        /// <param name="all">The width, in pixels, of the left, top, right and left sides of the bounding rectangle</param>
        public Thickness(double all)
        {
            this.Left = all;
            this.Top = all;
            this.Right = all;
            this.Bottom = all;
        }

        /// <summary>
        /// Initializes a new <see cref="Thickness"/> with the specified values for both the Left and Right, and both the Top and Bottom properties
        /// </summary>
        /// <param name="leftAndRight">The width, in pixels, of both the left and top sides of the bounding rectangle</param>
        /// <param name="topAndBottom">The width, in pixels, of both the top and bottom sides of the bounding rectangle</param>
        public Thickness(double leftAndRight, double topAndBottom)
        {
            this.Left = leftAndRight;
            this.Top = topAndBottom;
            this.Right = leftAndRight;
            this.Bottom = topAndBottom;
        }

        /// <summary>
        /// Initializes a new <see cref="Thickness"/> with the specified values for the Left, Top, Right and Bottom properties
        /// </summary>
        /// <param name="left">The width, in pixels, of the left side of the bounding rectangle</param>
        /// <param name="top">The width, in pixels, of the upper side of the bounding rectangle</param>
        /// <param name="right">The width, in pixels, of the right side of the bounding rectangle</param>
        /// <param name="bottom">The width, in pixels, of the upper side of the bounding rectangle</param>
        public Thickness(double left, double top, double right, double bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        /// <summary>
        /// Gets or sets the width, in pixels, of the left side of the bounding rectangle
        /// </summary>
        public double Left { get; private set; }

        /// <summary>
        /// Gets or sets the width, in pixels, of the upper side of the bounding rectangle
        /// </summary>
        public double Top { get; private set; }

        /// <summary>
        /// Gets or sets the width, in pixels, of the right side of the bounding rectangle
        /// </summary>
        public double Right { get; private set; }

        /// <summary>
        /// Gets or sets the width, in pixels, of the upper side of the bounding rectangle
        /// </summary>
        public double Bottom { get; private set; }

        /// <summary>
        /// An empty (default) instance of the thickness struct
        /// </summary>
        /// <returns></returns>
        public static Thickness Empty = new Thickness();

    }

}
