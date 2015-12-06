using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Provides the base class for all the easing functions
    /// </summary>
    public abstract class EasingFunction
    {

        /// <summary>
        /// Initializes a new <see cref="EasingFunction"/>
        /// </summary>
        protected EasingFunction()
        {

        }

        /// <summary>
        /// Gets/Sets the function's <see cref="EasingMode"/>
        /// </summary>
        public EasingMode Mode { get; set; }

        /// <summary>
        /// Provides the logic portion of the easing function that you can override to produce the EaseIn mode of the custom easing function
        /// </summary>
        /// <param name="normalizedTime">A double representing the animation's normalized time (ranging from 0.0 to 1.0)</param>
        /// <returns></returns>
        internal abstract double EasingCore(double normalizedTime);

    }

}
