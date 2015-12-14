using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// AnimationTimeline is a base class for core level implementations building on Photon animations and basic animation characteristics.
    /// </summary>
    public abstract class AnimationTimeline
    {

        /// <summary>
        /// This event is fired whenever the <see cref="AnimationTimeline"/> is completed
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Gets the <see cref="AnimationClock"/> associated with the animation timeline
        /// </summary>
        public AnimationClock Clock { get; private set; }

        /// <summary>
        /// Gets/Sets a <see cref="TimeSpan"/> representing the animation's begin time
        /// </summary>
        public TimeSpan? BeginTime { get; set; }

        /// <summary>
        /// Gets/Sets a <see cref="TimeSpan"/> representing the animation's duration
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets/Sets a boolean indicating whether or not to reverse the animation upon completion
        /// <para></para>Use the <see cref="AnimationTimeline.RepeatBehavior"/> property to configure the way the animation repeats
        /// </summary>
        public bool AutoReverse { get; set; }

        /// <summary>
        /// Gets/Sets the <see cref="RepeatBehavior"/> determining how the animation repeats
        /// <para></para>The <see cref="AnimationTimeline.AutoReverse"/> should be set to true for the animation to repeat
        /// </summary>
        public RepeatBehavior RepeatBehavior { get; set; }

        /// <summary>
        /// Gets/Sets a <see cref="FillBehavior"/> representing the way the animation behaves upon completion
        /// </summary>
        public FillBehavior FillBehavior { get; set; }

        /// <summary>
        /// Gets/Sets the animation's <see cref="EasingFunction"/>
        /// </summary>
        public EasingFunction Ease { get; set; }
        
        /// <summary>
        /// Gets a boolean indicating whether or not the animation is running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the animation is reverting
        /// </summary>
        public bool IsReverting { get; private set; }

        /// <summary>
        /// Gets/Sets a <see cref="TimeSpan"/> representing the time at which the animation begun reverting for the last time
        /// </summary>
        private TimeSpan? RevertTime { get; set; }

        /// <summary>
        /// Gets the <see cref="DependencyObject"/> which's <see cref="DependencyProperty"/> is being animated
        /// </summary>
        public DependencyObject Target { get; internal set; }

        /// <summary>
        /// Gets the <see cref="PropertyPath"/> leading to the <see cref="DependencyProperty"/> animated by the <see cref="AnimationTimeline"/>
        /// </summary>
        public PropertyPath TargetProperty { get; internal set; }

        /// <summary>
        /// Gets a <see cref="TimeSpan"/> indicating the time elasped since the <see cref="AnimationTimeline"/> has begun
        /// </summary>
        public TimeSpan Time
        {
            get
            {
                if (this.AutoReverse 
                    && this.RevertTime.HasValue)
                {
                    return this.Clock.Elapsed - this.RevertTime.Value;
                }
                else if (this.BeginTime.HasValue)
                {
                    return this.Clock.Elapsed - this.BeginTime.Value;
                }
                else
                {
                    return this.Clock.Elapsed;
                }
            }
        }

        /// <summary>
        /// Gets/Sets an object representing the value returned by the <see cref="AnimationTimeline.Target"/>'s <see cref="DependencyProperty"/> prior to begining the animation
        /// </summary>
        internal object OriginalValue { get; set; }

        /// <summary>
        /// Gets a double representing the animation timeline's normalized time (ranging from 0.0 to 1.0)
        /// </summary>
        /// <returns></returns>
        internal double GetNormalizedTime()
        {
            TimeSpan animationTime;
            double normalizedTime;
            if (!this.IsRunning)
            {
                return 0;
            }
            animationTime = this.Time;
            if(animationTime.TotalMilliseconds < 0)
            {
                return 0;
            }
            normalizedTime = animationTime.TotalMilliseconds / this.Duration.Value.TotalMilliseconds;
            return normalizedTime;
        }

        /// <summary>
        /// Begins the animation, based on the specified <see cref="AnimationClock"/>
        /// </summary>
        /// <param name="animationClock">The <see cref="AnimationClock"/> the animation belongs to</param>
        public void Begin(AnimationClock animationClock)
        {
            this.Clock = animationClock;
            if (!this.Duration.HasValue)
            {
                return;
            }
            if (this.BeginTime.HasValue)
            {
                if (this.BeginTime.Value.TotalMilliseconds < this.Clock.Elapsed.TotalMilliseconds)
                {
                    return;
                }
            }
            this.OriginalValue = this.TargetProperty.GetValue(this.Target);
            if (!this.OnBegin())
            {
                return;
            }
            this.IsRunning = true;
        }

        /// <summary>
        /// Renders the animation by processing the targeted output value
        /// </summary>
        internal void Render()
        {
            if (this.BeginTime.HasValue
                && !this.IsRunning)
            {
                if (this.Clock.Elapsed.TotalMilliseconds >= this.BeginTime.Value.TotalMilliseconds)
                {
                    this.Begin(this.Clock);
                }
            }
            if(this.Time >= this.Duration)
            {
                this.OnCompleted();
                this.Stop();
                if (this.Completed != null)
                {
                    this.Completed(this, new EventArgs());
                }
            }
            this.OnRender();
        }

        /// <summary>
        /// Stops the animation
        /// </summary>
        public void Stop()
        {
            if (this.AutoReverse 
                && this.RepeatBehavior.RepeatAnimation(this))
            {
                if (this.IsReverting)
                {
                    this.IsReverting = false;
                }
                else
                {
                    this.IsReverting = true;
                }
                this.RevertTime = this.Clock.Elapsed;
                return;
            }
            this.IsRunning = false;
            this.OnStop();
        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the animation begins
        /// </summary>
        /// <returns>A boolean indicating whether or not the animation could begun</returns>
        protected abstract bool OnBegin();

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the animation renders
        /// </summary>
        protected abstract void OnRender();

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the animation stops
        /// </summary>
        protected abstract void OnStop();

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the animation is completed
        /// </summary>
        protected abstract void OnCompleted();

    }

}
