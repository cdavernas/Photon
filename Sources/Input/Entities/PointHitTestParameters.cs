using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Input
{

    /// <summary>
    /// Specifies a <see cref="Media.Point"/> as the parameter to be used for hit testing of a visual object
    /// </summary>
    public class PointHitTestParameters
    {

        /// <summary>
        /// Initializes a new <see cref="PointHitTestParameters"/> with the specified <see cref="Media.Point"/>
        /// </summary>
        /// <param name="hitPoint">The <see cref="Media.Point"/> to be used for hit testing of a visual object</param>
        public PointHitTestParameters(Media.Point hitPoint)
        {
            this.HitPoint = hitPoint;
        }

        /// <summary>
        /// Gets the <see cref="Media.Point"/> to be used for hit testing of a visual object
        /// </summary>
        public Media.Point HitPoint { get; private set; }

    }

}
