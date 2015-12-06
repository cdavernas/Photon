using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Describes how a <see cref="Animation{T}"/> repeats its simple duration
    /// </summary>
    public struct RepeatBehavior
    {

        /// <summary>
        /// Gets the <see cref="RepeatBehavior"/>'s <see cref="RepeatMode"/>
        /// </summary>
        public RepeatMode Mode { get; private set; }

        /// <summary>
        /// Determines whether or not the animation should be repeated
        /// </summary>
        /// <param name="animation">The <see cref="Animation{T}"/> to check</param>
        /// <returns>A boolean indicating whether or not the animation should be repeated</returns>
        public bool RepeatAnimation(AnimationTimeline animation)
        {
            switch (this.Mode)
            {
                case RepeatMode.Forever:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets a <see cref="RepeatBehavior"/> that specifies an infinite number of repetitions.
        /// </summary>
        public static RepeatBehavior Forever
        {
            get
            {
                return new RepeatBehavior();
            }
        }

    }

}
