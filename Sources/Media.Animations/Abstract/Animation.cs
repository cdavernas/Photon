using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Represents the base class for all animations
    /// </summary>
    /// <typeparam name="T">The type of the animated value</typeparam>
    public abstract class Animation<T>
        : AnimationTimeline
        where T : struct
    {

        /// <summary>
        /// Gets/Sets the value from which to begin animating
        /// </summary>
        public T? From { get; set; }

        /// <summary>
        /// Gets/Sets the value until which to animate
        /// </summary>
        public T? To { get; set; }

        /// <summary>
        /// Gets the current value
        /// </summary>
        private T CurrentValue { get; set; }

        /// <summary>
        /// When overriden in a class, this method allows execution of code when the animation begins
        /// </summary>
        /// <returns>A boolean indicating whether or not the animation could begin</returns>
        protected override bool OnBegin()
        {
            if(!this.From.HasValue 
                || !this.To.HasValue)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// When overriden in a class, this method allows execution of code when the animation begins
        /// </summary>
        protected override void OnStop()
        {
            switch (this.FillBehavior)
            {
                case FillBehavior.Default:
                    break;
                case FillBehavior.OriginalValue:
                    this.TargetProperty.SetValue(this.Target, this.OriginalValue);
                    break;
                case FillBehavior.Reset:
                    this.TargetProperty.SetValue(this.Target, this.From.Value);
                    break;
            }
        }

        /// <summary>
        /// When overriden in a class, this method allows execution of code when the animation is completed
        /// </summary>
        protected override void OnCompleted()
        {
            
        }

    }

}
