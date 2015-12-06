using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Specifies how a <see cref="AnimationTimeline"/> behaves when it is outside its active period but its parent is inside its active or hold period.
    /// </summary>
    public enum FillBehavior
    {
        /// <summary>
        /// No specific action is taken, and the animated property's value remains the last set by the animation (as defined by the <see cref="Animation{T}.To"/> property)
        /// </summary>
        Default,
        /// <summary>
        /// When the animation completes, the animated property's value is reset to the value returned by the <see cref="Animation{T}.From"/> property)
        /// </summary>
        Reset,
        /// <summary>
        /// When the animation completes, the animated property's value is reset to the value it held before starting the animation
        /// </summary>
        OriginalValue
    }

}
