using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Provides rendering support in Photon, which includes hit testing, coordinate transformation, and bounding box calculations
    /// </summary>
    public abstract class Visual
        : DependencyObject
    {

        /// <summary>
        /// Initialies a new <see cref="Visual"/>
        /// </summary>
        protected Visual()
        {
            this.VisualCacheMode = Media.CacheMode.DynamicCache;
        }

        /// <summary>
        /// Describes the <see cref="Visual.Effect"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty EffectProperty = DependencyProperty.Register("Effect", typeof(Visual));
        /// <summary>
        /// Gets/sets the <see cref="Media.Effects.Effect"/> associated with the <see cref="Visual"/>
        /// </summary>
        public Media.Effects.Effect Effect
        {
            get
            {
                return this.GetValue<Media.Effects.Effect>(Visual.EffectProperty);
            }
            set
            {
                this.SetValue(Visual.EffectProperty, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="Visual"/>'s <see cref="Media.CacheMode"/>
        /// </summary>
        public Media.CacheMode VisualCacheMode { get; protected set; }

        /// <summary>
        /// Gets the <see cref="Media.Drawing"/> of the <see cref="Visual"/>
        /// </summary>
        public Media.Drawing VisualDrawing { get; protected set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Visual"/> has been loaded
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Gets/Sets the date and time at which the element has last been invalidated
        /// </summary>
        internal DateTime LastInvalidated { get; set; }

        /// <summary>
        /// Invalidates the <see cref="Visual"/> and, depending on its <see cref="Visual.VisualCacheMode"/>, generates a new <see cref="Media.Drawing"/>
        /// </summary>
        public void InvalidateVisual()
        {
            Controls.IDecorator decorator;
            Controls.IPanel panel;
            if (!this.IsLoaded)
            {
                return;
            }
            //Check the VisualCacheMode and act accordingly
            switch (this.VisualCacheMode)
            {
                case Media.CacheMode.NoCache:
                case Media.CacheMode.DynamicCache:
                    this.OnInvalidateVisual();
                    if (typeof(Controls.IDecorator).IsAssignableFrom(this.GetType()))
                    {
                        decorator = (Controls.IDecorator)this;
                        if (decorator.Child != null)
                        {
                            decorator.Child.InvalidateVisual();
                        }
                    }
                    else if (typeof(Controls.IPanel).IsAssignableFrom(this.GetType()))
                    {
                        panel = (Controls.IPanel)this;
                        foreach (UIElement element in panel.Children)
                        {
                            element.InvalidateVisual();
                        }
                    }
                    break;
                case Media.CacheMode.Cache:
                    return;
            }
            //Set the date and time at which the last invalidation occured
            this.LastInvalidated = DateTime.UtcNow;
        }

        /// <summary>
        /// This method executes when the control is loading, meaning when it is first being rendered on screen
        /// </summary>
        internal void Load()
        {
            this.InvalidateVisual();
            if (typeof(Controls.IDecorator).IsAssignableFrom(this.GetType()))
            {
                if (((Controls.IDecorator)this).Child != null)
                {
                    ((Controls.IDecorator)this).Child.Load();
                }
            }
            else if (typeof(Controls.IPanel).IsAssignableFrom(this.GetType()))
            {
                foreach (UIElement element in ((Controls.IPanel)this).Children)
                {
                    element.Load();
                }
            }
            if(this.Effect != null)
            {
                this.Effect.Load();
            }
            this.IsLoaded = true;
            this.OnLoaded();
        }

        /// <summary>
        /// Renders the <see cref="Visual"/>
        /// </summary>
        /// <param name="drawingContext">The <see cref="DrawingContext"/> in which to render the <see cref="Visual"/></param>
        internal void Render(DrawingContext drawingContext)
        {
            foreach (Media.Animations.AnimationClock clock in this.AnimationClocks.Where(c => c.IsRunning))
            {
                clock.Render();
            }
            if(this.Effect != null)
            {
                this.Effect.BeginUse();
            }
            this.OnRender(drawingContext);
            if (this.Effect != null)
            {
                this.Effect.EndUse();
            }
        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element has been loaded
        /// </summary>
        protected virtual void OnLoaded()
        {

        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the <see cref="Visual"/> is being rendered
        /// </summary>
        ///<param name="drawingContext">The <see cref="DrawingContext"/> in which the element has been rendered</param>
        protected abstract void OnRender(DrawingContext drawingContext);

        /// <summary>
        /// When inherited by a class, generates a new <see cref="Media.Drawing"/> of the <see cref="Visual"/>
        /// </summary>
        protected virtual void OnInvalidateVisual()
        {

        }

    }

}
