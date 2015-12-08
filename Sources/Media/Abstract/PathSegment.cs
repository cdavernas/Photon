using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Represents a segment of a <see cref="PathFigure"/> object
    /// </summary>
    public abstract class PathSegment
        : DependencyObject
    {

        /// <summary>
        /// Initializes a new <see cref="PathSegment"/>
        /// </summary>
        protected PathSegment()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="PathSegment"/>
        /// <param name="isStroked">Indicates wheter or not the <see cref="PathSegment"/> is stroked</param>
        /// </summary>
        protected PathSegment(bool isStroked)
        {
            this.IsStroked = isStroked;
        }

        /// <summary>
        /// Identifies the PathSegment's IsStroked <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty IsStrokedProperty = DependencyProperty.Register("IsStroked", typeof(PathSegment));
        /// <summary>
        /// Gets or sets a value that indicates whether the segment is stroked
        /// </summary>
        public bool IsStroked
        {
            get
            {
                return this.GetValue<bool>(PathSegment.IsStrokedProperty);
            }
            set
            {
                this.SetValue(PathSegment.IsStrokedProperty, value);
            }
        }

    }

}
