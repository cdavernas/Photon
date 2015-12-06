using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Maintains the run-time state of an AnimationTimeline and processes its output values
    /// </summary>
    public abstract class AnimationClock
    {

        /// <summary>
        /// This event is fired whenever the animation is completed
        /// </summary>
        public EventHandler Completed;

        /// <summary>
        /// Initializes a new <see cref="AnimationClock"/>
        /// </summary>
        protected AnimationClock()
        {

        }

        /// <summary>
        /// Gets a boolean indicating whether or not the animation clock is running
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// Gets a <see cref="TimeSpan"/> representing the amount of time elapsed since the animation clock has beun
        /// </summary>
        public abstract TimeSpan Elapsed { get; }

        /// <summary>
        /// When implemented in a class, this method starts the animation clock
        /// </summary>
        public abstract void Begin();

        /// <summary>
        /// When implemented in a class, this method processes the animation clock's output values
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// When implemented in a class, this method stops the animation clock
        /// </summary>
        public abstract void Stop();

    }

}
