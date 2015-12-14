using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// A <see cref="AnimationClock"/> that provides object and property targeting information for its child animations
    /// </summary>
    public class Storyboard
        : AnimationClock
    {

        /// <summary>
        /// Gets the amount of frames that have been rendered since the <see cref="Storyboard"/> begun
        /// </summary>
        private long RenderedFrames;
        /// <summary>
        /// Gets the amount of <see cref="AnimationTimeline"/>s that have completed since the <see cref="Storyboard"/> begun
        /// </summary>
        private int AnimationsCompleted;

        /// <summary>
        /// This event is fired whenever all the children animation timelines are completed
        /// </summary>
        public override event EventHandler Completed;

        /// <summary>
        /// Initializes a new <see cref="Storyboard"/>
        /// </summary>
        private Storyboard()
        {
            this.Children = new AnimationTimelineCollection();
            this.Children.CollectionChanged += this.OnChildrenChanged;
        }

        /// <summary>
        /// Gets a collection of all the <see cref="Storyboard"/>'s child <see cref="AnimationTimeline"/>
        /// </summary>
        public AnimationTimelineCollection Children { get; private set; }

        /// <summary>
        /// Gets the time elapsed since the <see cref="Storyboard"/> begun
        /// </summary>
        public override TimeSpan Elapsed
        {
            get
            {
                return TimeSpan.FromSeconds(this.RenderedFrames / Window.DEFAULT_FRAMES_PER_SECOND);
            }
        }

        /// <summary>
        /// Begins the <see cref="Storyboard"/> and all its subsequent <see cref="AnimationTimeline"/>
        /// </summary>
        public override void Begin()
        {
            this.IsRunning = true;
            foreach (AnimationTimeline animation in this.Children)
            {
                animation.Begin(this);
            }
        }

        /// <summary>
        /// Renders the <see cref="AnimationTimeline"/>s owned by the <see cref="Storyboard"/>
        /// </summary>
        public override void Render()
        {
            foreach (AnimationTimeline animation in this.Children)
            {
                animation.Render();
            }
            this.RenderedFrames++;
        }

        /// <summary>
        /// Stops the <see cref="Storyboard"/> and all its subsequent <see cref="AnimationTimeline"/>
        /// </summary>
        public override void Stop()
        {
            foreach (AnimationTimeline animation in this.Children)
            {
                animation.Stop();
            }
            this.RenderedFrames = 0;
            this.IsRunning = false;
        }

        /// <summary>
        /// Handles the <see cref="ObservableHashSet{TElement}.CollectionChanged"/> event
        /// </summary>
        /// <param name="sender">The event's sender</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> associated with the event</param>
        private void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AnimationTimeline animation;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    animation = (AnimationTimeline)e.NewItems[0];
                    animation.Completed += this.OnAnimationCompleted;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    animation = (AnimationTimeline)e.OldItems[0];
                    animation.Completed -= this.OnAnimationCompleted;
                    break;
            }
        }

        /// <summary>
        /// Handles the <see cref="AnimationTimeline.Completed"/> event raised by child <see cref="AnimationTimeline"/>s
        /// </summary>
        /// <param name="sender">The event's sender</param>
        /// <param name="e">The <see cref="EventArgs"/> associated with the event</param>
        private void OnAnimationCompleted(object sender, EventArgs e)
        {
            if (((AnimationTimeline)sender).IsRunning)
            {
                return;
            }
            Interlocked.Increment(ref this.AnimationsCompleted);
            if (this.AnimationsCompleted == this.Children.Count)
            {
                this.Stop();
                if (this.Completed != null)
                {
                    this.Completed(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Creates the register a new <see cref="Storyboard"/> for the specified <see cref="IUIElement"/>
        /// </summary>
        /// <param name="element">The <see cref="IUIElement"/> for which to create the <see cref="Storyboard"/></param>
        /// <returns>The <see cref="Storyboard"/> that has been registered into the specified <see cref="IUIElement"/></returns>
        public static Storyboard Register(IUIElement element)
        {
            Storyboard storyboard;
            storyboard = new Storyboard();
            element.AnimationClocks.Add(storyboard);
            return storyboard;
        }

        /// <summary>
        /// Sets the target <see cref="DependencyObject"/> of the specified <see cref="AnimationTimeline"/> 
        /// </summary>
        /// <param name="animation">The <see cref="AnimationTimeline"/> for which to set the specified target</param>
        /// <param name="target">The <see cref="AnimationTimeline"/>'s target <see cref="DependencyObject"/></param>
        public static void SetTarget(AnimationTimeline animation, DependencyObject target)
        {
            animation.Target = target;
        }

        /// <summary>
        /// Sets the targeted <see cref="DependencyProperty"/> of the specified <see cref="AnimationTimeline"/>
        /// </summary>
        /// <param name="animation">The <see cref="AnimationTimeline"/> for which to set the specified targeted property</param>
        /// <param name="property">The <see cref="AnimationTimeline"/>'s targeted <see cref="DependencyProperty"/></param>
        public static void SetTargetProperty(AnimationTimeline animation, DependencyProperty property)
        {
            animation.TargetProperty = new PropertyPath(new DependencyProperty[] { property });
        }

        /// <summary>
        /// Sets the targeted <see cref="DependencyProperty"/> of the specified <see cref="AnimationTimeline"/>
        /// </summary>
        /// <param name="animation">The <see cref="AnimationTimeline"/> for which to set the specified targeted property</param>
        /// <param name="propertyPath">The <see cref="AnimationTimeline"/>'s targeted <see cref="PropertyPath"/></param>
        public static void SetTargetProperty(AnimationTimeline animation, PropertyPath propertyPath)
        {
            animation.TargetProperty = propertyPath;
        }

    }

}
