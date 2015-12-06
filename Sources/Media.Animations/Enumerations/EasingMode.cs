using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Defines the modes in which classes derived from <see cref="EasingFunction"/> perform their easing
    /// </summary>
    public enum EasingMode
    {
        /// <summary>
        /// Interpolation follows the mathematical formula associated with the easing function
        /// </summary>
        EaseIn,
        /// <summary>
        /// Interpolation follows 100% interpolation minus the output of the formula associated with the easing function
        /// </summary>
        EaseOut,
        /// <summary>
        /// Interpolation uses EaseIn for the first half of the animation and EaseOut for the second half
        /// </summary>
        EaseInAndOut
    }

}
