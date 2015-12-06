using Photon.Media;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// UIElement is a base class for core level implementations building on Photon elements and basic presentation characteristics.
    /// </summary>
    public abstract class UIElement
        : DependencyObject, IUIElement
    {

        /// <summary>
        /// This event is fired every time the element's layout is invalidated
        /// </summary>
        internal event EventHandler LayoutInvalidated;
        /// <summary>
        /// This event is fired every time the element's visual is invalidated
        /// </summary>
        internal event EventHandler VisualInvalidated;
        /// <summary>
        /// This event is fired every time the mouse moves over the element
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> MouseMove;
        /// <summary>
        /// This event is fired every time the mouse enters the element
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseEnter;
        /// <summary>
        /// This event is fired every time the mouse leaves the element
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseLeave;
        /// <summary>
        /// This event is fired every time a mouse button is down over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonDown;
        /// <summary>
        /// This event is fired every time a mouse button is up over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonUp;
        /// <summary>
        /// This event is fired every time the left mouse button is down over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseLeftButtonDown;
        /// <summary>
        /// This event is fired every time the left mouse button is up over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseLeftButtonUp;
        /// <summary>
        /// This event is fired every time the middle mouse button is down over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseMiddleButtonDown;
        /// <summary>
        /// This event is fired every time the middle mouse button is up over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseMiddleButtonUp;
        /// <summary>
        /// This event is fired every time the right mouse button is down over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseRightButtonDown;
        /// <summary>
        /// This event is fired every time the right mouse button is down over the element
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseRightButtonUp;
        /// <summary>
        /// This event is fired every time the mouse wheel is used over the element
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> MouseWheel;
        /// <summary>
        /// This event is fired every time a key is down over the element
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyDown;
        /// <summary>
        /// This event is fired every time a key is up over the element
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyUp;
        /// <summary>
        /// This event is fired every time a key is pressed over the element
        /// </summary>
        public event EventHandler<KeyPressEventArgs> KeyPressed;
        /// <summary>
        /// This event is fired every time the element's visibility has changed
        /// </summary>
        public event EventHandler VisibilityChanged;
        /// <summary>
        /// This event is fired every time the element gets focus
        /// </summary>
        public event EventHandler GotFocus;
        /// <summary>
        /// This event is fired every time the element looses focus
        /// </summary>
        public event EventHandler LostFocus;

        /// <summary>
        /// The default, parameterless constructor for the <see cref="UIElement"/> type
        /// </summary>
        protected UIElement()
        {

        }

        /// <summary>
        /// Gets/Sets the <see cref="UIElement"/>'s logical parent 
        /// </summary>
        public IUIElement Parent { get; set; }

        public static DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register("HorizontalAlignment", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's <see cref="HorizontalAlignment"/>
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return this.GetValue<HorizontalAlignment>(UIElement.HorizontalAlignmentProperty);
            }
            set
            {
                this.SetValue(UIElement.HorizontalAlignmentProperty, value);
            }
        }

        public static DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register("VerticalAlignment", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's <see cref="VerticalAlignment"/>
        /// </summary>
        public VerticalAlignment VerticalAlignment
        {
            get
            {
                return this.GetValue<VerticalAlignment>(UIElement.VerticalAlignmentProperty);
            }
            set
            {
                this.SetValue(UIElement.VerticalAlignmentProperty, value);
            }
        }

        public static DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's width
        /// </summary>
        public double? Width
        {
            get
            {
                return this.GetValue<double?>(UIElement.WidthProperty);
            }
            set
            {
                this.SetValue(UIElement.WidthProperty, value);
            }
        }

        public static DependencyProperty MinWidthProperty = DependencyProperty.Register("MinWidth", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's minimal width
        /// </summary>
        public double? MinWidth
        {
            get
            {
                return this.GetValue<double?>(UIElement.MinWidthProperty);
            }
            set
            {
                this.SetValue(UIElement.MinWidthProperty, value);
            }
        }

        public static DependencyProperty MaxWidthProperty = DependencyProperty.Register("MaxWidth", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's maximal width
        /// </summary>
        public double? MaxWidth
        {
            get
            {
                return this.GetValue<double?>(UIElement.MaxWidthProperty);
            }
            set
            {
                this.SetValue(UIElement.MaxWidthProperty, value);
            }
        }

        public static DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's height
        /// </summary>
        public double? Height
        {
            get
            {
                return this.GetValue<double?>(UIElement.HeightProperty);
            }
            set
            {
                this.SetValue(UIElement.HeightProperty, value);
            }
        }

        public static DependencyProperty MinHeightProperty = DependencyProperty.Register("MinHeight", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's minimal height
        /// </summary>
        public double? MinHeight
        {
            get
            {
                return this.GetValue<double?>(UIElement.MinHeightProperty);
            }
            set
            {
                this.SetValue(UIElement.MinHeightProperty, value);
            }
        }

        public static DependencyProperty MaxHeightProperty = DependencyProperty.Register("MaxHeight", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's maximal height
        /// </summary>
        public double? MaxHeight
        {
            get
            {
                return this.GetValue<double?>(UIElement.MaxHeightProperty);
            }
            set
            {
                this.SetValue(UIElement.MaxHeightProperty, value);
            }
        }

        public static DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's background
        /// </summary>
        public Brush Background
        {
            get
            {
                return this.GetValue<Brush>(UIElement.BackgroundProperty);
            }
            set
            {
                this.SetValue(UIElement.BackgroundProperty, value);
            }
        }

        public static DependencyProperty MarginProperty = DependencyProperty.Register("Margin", typeof(UIElement), new Thickness());
        /// <summary>
        /// Gets/Sets the element's margin
        /// </summary>
        public Thickness Margin
        {
            get
            {
                return this.GetValue<Thickness>(UIElement.MarginProperty);
            }
            set
            {
                this.SetValue(UIElement.MarginProperty, value);
            }
        }

        public static DependencyProperty CursorProperty = DependencyProperty.Register("Cursor", typeof(UIElement), Media.MouseCursor.Default);
        /// <summary>
        /// Gets/Sets the mouse cursor when over the element
        /// </summary>
        public Media.MouseCursor Cursor
        {
            get
            {
                return this.GetValue<Media.MouseCursor>(UIElement.CursorProperty);
            }
            set
            {
                this.SetValue(UIElement.CursorProperty, value);
            }
        }

        public static DependencyProperty OpacityProperty = DependencyProperty.Register("Opacity", typeof(UIElement), 1.0);
        /// <summary>
        /// Gets/Sets the element's opacity
        /// </summary>
        public double Opacity
        {
            get
            {
                return this.GetValue<double>(UIElement.OpacityProperty);
            }
            set
            {
                this.SetValue(UIElement.OpacityProperty, value);
            }
        }

        public static DependencyProperty VisibilityProperty = DependencyProperty.Register("Visibility", typeof(UIElement));
        /// <summary>
        /// Gets/Sets the element's <see cref="Visibility"/>
        /// </summary>
        public Visibility Visibility
        {
            get
            {
                return this.GetValue<Visibility>(UIElement.VisibilityProperty);
            }
            set
            {
                this.SetValue(UIElement.VisibilityProperty, value);
            }
        }

        public static DependencyProperty IsMouseOverProperty = DependencyProperty.Register("IsMouseOver", typeof(UIElement));
        /// <summary>
        /// Gets/Sets a value indicating whether or not the mouse is currently over the element
        /// </summary>
        public bool IsMouseOver
        {
            get
            {
                return this.GetValue<bool>(UIElement.IsMouseOverProperty);
            }
            set
            {
                this.SetValue(UIElement.IsMouseOverProperty, value);
            }
        }

        /// <summary>
        /// Gets/Sets a value indicating whether or not the element can be focused
        /// </summary>
        public bool IsFocusable
        {
            get
            {
                return Input.FocusManager.GetIsFocusable(this);
            }
            set
            {
                Input.FocusManager.SetIsFocusable(this, value);
            }
        }

        public static DependencyProperty IsHitTestVisibleProperty = DependencyProperty.Register("IsHitTestVisible", typeof(UIElement), true);
        /// <summary>
        /// Gets/Sets a value indicating whether or not the element is hit test visible
        /// </summary>
        public bool IsHitTestVisible
        {
            get
            {
                return this.GetValue<bool>(UIElement.IsHitTestVisibleProperty);
            }
            set
            {
                this.SetValue(UIElement.IsHitTestVisibleProperty, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the element has a parent and is within a visual tree
        /// </summary>
        public bool HasParent
        {
            get
            {
                if(this.Parent == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets a <see cref="Media.Point"/> representing the element's layout position
        /// </summary>
        public Point LayoutPosition { get; private set; }

        /// <summary>
        /// Gets a <see cref="Media.Size"/> representing the element's layout size
        /// </summary>
        public Size LayoutSize { get; private set; }

        /// <summary>
        /// Gets a <see cref="Media.Rectangle"/> representing the element's layout target
        /// </summary>
        public Rectangle LayoutTarget
        {
            get
            {
                return new Rectangle(this.LayoutPosition, this.LayoutSize);
            }
        }

        /// <summary>
        /// Gets a <see cref="Media.Point"/> representing the element's render position
        /// </summary>
        public Point RenderPosition { get; private set; }

        /// <summary>
        /// Gets a <see cref="Media.Size"/> representing the element's render size
        /// </summary>
        public Size RenderSize { get; private set; }

        /// <summary>
        /// Gets a <see cref="Media.Rectangle"/> representing the element's render target
        /// </summary>
        public Rectangle RenderTarget
        {
            get
            {
                return new Rectangle(this.RenderPosition, this.RenderSize);
            }
        }

        /// <summary>
        /// Gets the element's actual width
        /// </summary>
        public double ActualWidth
        {
            get
            {
                this.Measure();
                return this.RenderSize.Width;
            }
        }

        /// <summary>
        /// Gets the element's actual height
        /// </summary>
        public double ActualHeight
        {
            get
            {
                this.Measure();
                return this.RenderSize.Height;
            }
        }

        /// <summary>
        /// Gets/Sets the date and time at which the element has last been invalidated
        /// </summary>
        internal DateTime LastInvalidated { get; set; }

        /// <summary>
        /// Gets/Sets the date and time at which the element has last been located
        /// </summary>
        internal DateTime LastLocated { get; set; }

        /// <summary>
        /// Gets/Sets the date and time at which the element has last been measured
        /// </summary>
        internal DateTime LastMeasured { get; set; }

        /// <summary>
        /// This method is fired whenever the element's layout has been invalidated
        /// </summary>
        public void InvalidateLayout()
        {
            Controls.IDecorator decorator;
            Controls.IPanel panel;
            this.LastInvalidated = DateTime.UtcNow;
            this.Locate();
            this.Measure();
            if (typeof(Controls.IDecorator).IsAssignableFrom(this.GetType()))
            {
                decorator = (Controls.IDecorator)this;
                if(decorator.Child != null)
                {
                    decorator.Child.InvalidateLayout();
                }
            }
            else if (typeof(Controls.IPanel).IsAssignableFrom(this.GetType()))
            {
                panel = (Controls.IPanel)this;
                foreach(UIElement element in panel.Children)
                {
                    element.InvalidateLayout();
                }
            }
            //Check if the LayoutInvalidated event is being handled. If it is, fire it
            if (this.LayoutInvalidated != null)
            {
                this.LayoutInvalidated(this, new EventArgs());
            }
            this.OnInvalidateLayout();
        }

        /// <summary>
        /// This method is fired whenever the element's visual has been invalidated
        /// </summary>
        public void InvalidateVisual()
        {

            //Set the date and time at which the last invalidation occured
            this.LastInvalidated = DateTime.UtcNow;
        }

        /// <summary>
        /// This method is fired whenever the element needs to be located
        /// </summary>
        private void Locate()
        {
            Point parentPosition, offset, position;
            //Check whether or not the last position was computed after the last invalidation
            if (this.LastLocated.CompareTo(this.LastInvalidated) != -1)
            {
                //No need to recompute the position
                return;
            }
            //Determine whether or not the control is within a visual tree
            if (this.Parent == null)
            {
                //Because the position is not relative to anything, it is set to 0,0 by default
                position = new Point();
                this.LayoutPosition = position;
                //Modify the position to include the element's left and top margin values
                this.RenderPosition = position;
                return;
            }
            //Determine whether the parent is an UIElement or the visual tree root (such as a Window)
            if (typeof(UIElement).IsAssignableFrom(this.Parent.GetType()))
            {
                //Retrieve the parent's position
                parentPosition = ((UIElement)this.Parent).RenderPosition;
            }
            else
            {
                //Set the default position
                parentPosition = new Point(0, 0);
            }
            //Determine whether or not the parent implements the IPaddedElement interface
            if (typeof(Controls.IPaddedElement).IsAssignableFrom(this.Parent.GetType()))
            {
                //Modify the parent's position by including the parent's padding values
                parentPosition = new Point(parentPosition.X + ((Controls.IPaddedElement)this.Parent).Padding.Left, parentPosition.Y + ((Controls.IPaddedElement)this.Parent).Padding.Top);
            }
            //Retrieve the element's offset, as computed by its parent
            offset = ((Controls.IElementPresenter)this.Parent).ComputeChildOffset(this);
            //Create the element's position
            position = new Point(parentPosition.X + offset.X, parentPosition.Y + offset.Y);
            //Set the element's LayoutPosition
            this.LayoutPosition = position;
            //Modify the position to include the element's left and top margin values
            position = new Point(position.X + this.Margin.Left, position.Y + this.Margin.Top);
            ////Determine whether or not the element implements the IPaddedElement interface
            //if (typeof(Controls.IPaddedElement).IsAssignableFrom(this.GetType()))
            //{
            //    //Modify the offset by including the element's padding values
            //    position = new Point(position.X + ((Controls.IPaddedElement)this).Padding.Left, position.Y + ((Controls.IPaddedElement)this).Padding.Top);
            //}
            //Set the element's RenderPosition 
            this.RenderPosition = position;
            //Set the date and time at which the last measure occured
            this.LastLocated = DateTime.UtcNow;
        }

        /// <summary>
        /// This method is fired whenever the element needs to be measured
        /// </summary>
        private void Measure()
        {
            Controls.IElementPresenter container;
            Size contentsSize, size;
            double? width, height;
            Controls.IPaddedElement paddedElement;
            double horizontalPadding, verticalPadding;
            container = this as Controls.IElementPresenter;
            width = null;
            height = null;
            if (container == null)
            {
                contentsSize = new Size();
            }
            else
            {
                contentsSize = container.ContentsSize;
            }
            //Check whether or not the last measure was made after the last invalidation
            if(this.LastMeasured.CompareTo(this.LastInvalidated) != -1)
            {
                //No need to recompute the measurements
                return;
            }
            //Check whether or not the element stretches horizontally
            if(this.HorizontalAlignment == HorizontalAlignment.Stretch)
            {
                width = null;
            }
            else
            {
                //Determine whether or not the element's layout is affected by content modifications
                if(container != null ? container.ContentsAffectsLayout : false)
                {
                    if (this.Width.HasValue)
                    {
                        width = this.Width.Value;
                    }
                    if (contentsSize.Width > (this.Width.HasValue ? this.Width.Value : 0))
                    {
                        width = contentsSize.Width;
                    }
                }
                else
                {
                    if (this.Width.HasValue)
                    {
                        width = this.Width.Value;
                    }
                    else
                    {
                        width = contentsSize.Width;
                    }
                }
            }
            //Check whether or not the element stretches vertically
            if (this.HorizontalAlignment == HorizontalAlignment.Stretch)
            {
                height = null;
            }
            else
            {
                //Determine whether or not the element's layout is affected by content modifications
                if (container != null ? container.ContentsAffectsLayout : false)
                {
                    if (this.Height.HasValue)
                    {
                        height = this.Height.Value;
                    }
                    if (contentsSize.Height > (this.Height.HasValue ? this.Height.Value : 0))
                    {
                        height = contentsSize.Height;
                    }
                }
                else
                {
                    if (this.Height.HasValue)
                    {
                        height = this.Height.Value;
                    }
                    else
                    {
                        height = contentsSize.Height;
                    }
                }
            }
            //Check whether a width value has been provided
            if (!width.HasValue)
            {
                width = contentsSize.Width;
            }
            //Check whether a height value has been provided
            if (!height.HasValue)
            {
                height = contentsSize.Height;
            }
            //Check whether or not the element implements the IPaddedElement interface
            if (typeof(Controls.IPaddedElement).IsAssignableFrom(this.GetType()))
            {
                paddedElement = (Controls.IPaddedElement)this;
                horizontalPadding = paddedElement.Padding.Left + paddedElement.Padding.Right;
                verticalPadding = paddedElement.Padding.Top + paddedElement.Padding.Bottom;
                width += horizontalPadding;
                height += verticalPadding;
            }
            //Check whether or not width value falls between the range specified by the MinWidth et MaxWidth properties
            if (width.Value < (this.MinWidth.HasValue ? this.MinWidth.Value : 0))
            {
                width = this.MinWidth;
            }
            else if (width.Value > (this.MaxWidth.HasValue ? this.MaxWidth.Value : int.MaxValue))
            {
                width = this.MaxWidth;
            }
            //Check whether or not height value falls between the range specified by the MinHeight et MaxHeight properties
            if (height.Value < (this.MinHeight.HasValue ? this.MinHeight.Value : 0))
            {
                height = this.MinHeight;
            }
            else if (height.Value > (this.MaxHeight.HasValue ? this.MaxHeight.Value : int.MaxValue))
            {
                height = this.MaxHeight;
            }
            //Create the new size
            size = new Size(width.Value, height.Value);
            //Set the RenderSize
            this.RenderSize = size;
            //Modify the size to include margin values
            size = new Size(this.Margin.Left + width.Value + this.Margin.Right, this.Margin.Top + height.Value + this.Margin.Bottom);
            //Set the LayoutSize
            this.LayoutSize = size;
            //Set the date and time at which the last measure occured
            this.LastMeasured = DateTime.UtcNow;
        }

        /// <summary>
        /// This method executes when the control is loading, meaning when it is first being rendered on screen
        /// </summary>
        internal void Load()
        {
            this.InvalidateLayout();
            if (typeof(Controls.IDecorator).IsAssignableFrom(this.GetType()))
            {
                ((Controls.IDecorator)this).Child.Load();
            }
            else if (typeof(Controls.IPanel).IsAssignableFrom(this.GetType()))
            {
                foreach(UIElement element in ((Controls.IPanel)this).Children)
                {
                    element.Load();
                }
            }
            this.OnLoaded();
        }

        /// <summary>
        /// This method sets the focus on the element
        /// </summary>
        public void Focus()
        {
           if(this.GotFocus != null)
            {
                this.GotFocus(this, new EventArgs());
            }
        }

        /// <summary>
        /// If the element is focused, this method unfocuses it
        /// </summary>
        internal void Unfocus()
        {
            if (this.LostFocus != null)
            {
                this.LostFocus(this, new EventArgs());
            }
        }

        /// <summary>
        /// Renders the element
        /// </summary>
        /// <param name="drawingContext">The <see cref="DrawingContext"/> in which to render the element</param>
        internal void Render(DrawingContext drawingContext)
        {
            foreach(Media.Animations.AnimationClock clock in this.AnimationClocks.Where(c => c.IsRunning))
            {
                clock.Render();
            }
            this.OnRender(drawingContext);
        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element has been loaded
        /// </summary>
        protected virtual void OnLoaded()
        {

        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element's layout has been invalidated
        /// </summary>
        protected virtual void OnInvalidateLayout()
        {
            Rectangle parentRenderTarget;
            double left, top, width, height;
            Controls.IPaddedElement paddedElement;
            Point offset;
            //Check if the UIElement is within a visual tree
            if (this.Parent == null)
            {
                //The UIElement is not within a visual tree and will therefore not be rendered
                return;
            }
            //Retrieve the parent's render target
            parentRenderTarget = this.Parent.RenderTarget;
            //Set the left and top according to the parent's render target
            left = parentRenderTarget.Left;
            top = parentRenderTarget.Top;
            //Determine if the parent implements the IPaddedElement. if it does, increment the left and top values by the equivalent padding values
            if (typeof(Controls.IPaddedElement).IsAssignableFrom(this.Parent.GetType()))
            {
                paddedElement = (Controls.IPaddedElement)this.Parent;
                left += paddedElement.Padding.Left;
                top += paddedElement.Padding.Top;
            }
            //Compute the element's offset according to the parent
            offset = ((Controls.IDecorator)this.Parent).ComputeChildOffset(this);
            //Increment the left and top values by the equivalent offset values
            left += offset.X;
            top += offset.Y;
            //Modify the left and top to include margin values
            left += this.Margin.Left;
            top += this.Margin.Top;
            //Set the width and height
            width = this.ActualWidth;
            height = this.ActualHeight;
            //Create the new layout target
            //this.LayoutTarget = new Rectangle(left, top, width, height);
            //Make sure the visual will get redrawn
            this.InvalidateVisual();
        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element has been rendered
        /// </summary>
        ///<param name="drawingContext">The <see cref="DrawingContext"/> in which the element has been rendered</param>
        protected abstract void OnRender(DrawingContext drawingContext);

        /// <summary>
        /// When overriden in a class, this method provides means to run code whenever a <see cref="DependencyProperty"/> has changed
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the modified property</param>
        /// <param name="originalValue">An object representing the property's value before the suffered change(s)</param>
        /// <param name="value">An object representing the property's actual (new) value</param>
        protected override void OnPropertyChanged(string propertyName, object originalValue, object value)
        {
            if (propertyName == UIElement.IsMouseOverProperty.Name)
            {
                bool isOver;
                isOver = (bool)value;
                if (isOver && this.MouseEnter != null)
                {
                    this.MouseEnter(this, new MouseEventArgs());
                }
                else if (this.MouseLeave != null)
                {
                    this.MouseLeave(this, new MouseEventArgs());
                }
                return;
            }
            if(propertyName == UIElement.VisibilityProperty.Name)
            {
                if(this.VisibilityChanged != null)
                {
                    this.VisibilityChanged(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// This method is fired when a key is down over the element
        /// </summary>
        /// <param name="e">The <see cref="KeyboardKeyEventArgs"/> associated with the KeyDown event</param>
        internal void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if(this.KeyDown != null)
            {
                this.KeyDown(this, e);
            }
        }

        /// <summary>
        /// This method is fired when a key is up over the element
        /// </summary>
        /// <param name="e">The <see cref="KeyboardKeyEventArgs"/> associated with the KeyUp event</param>
        internal void OnKeyUp(KeyboardKeyEventArgs e)
        {
            if (this.KeyUp != null)
            {
                this.KeyUp(this, e);
            }
        }

        /// <summary>
        /// This method is fired when a key is pressed over the element
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> associated with the KeyPress event</param>
        internal void OnKeyPressed(KeyPressEventArgs e)
        {
            if(this.KeyPressed != null)
            {
                this.KeyPressed(this, e);
            }
        }

        /// <summary>
        /// Determines whether or not the control is hit by the specified <see cref="PointHitTestParameters"/>
        /// </summary>
        /// <param name="parameters">The <see cref="PointHitTestParameters"/> configuring the element</param>
        /// <returns>A <see cref="HitTestResult"/> representing the result of the hit test</returns>
        public Input.HitTestResult HitTest(Input.PointHitTestParameters parameters)
        {
            Input.HitTestResult result;
            if (typeof(Controls.IElementPresenter).IsAssignableFrom(this.GetType()))
            {
                if (typeof(Controls.IPanel).IsAssignableFrom(this.GetType()))
                {
                    foreach(UIElement element in ((Controls.IPanel)this).Children)
                    {
                        result = element.HitTest(parameters);
                        if (result.HasHit)
                        {
                            this.IsMouseOver = false;
                            return result;
                        }
                    }
                }
                else if (typeof(Controls.IDecorator).IsAssignableFrom(this.GetType()))
                {
                    if(((Controls.IDecorator)this).Child != null)
                    {
                        result = ((Controls.IDecorator)this).Child.HitTest(parameters);
                        if (result.HasHit)
                        {
                            this.IsMouseOver = false;
                            return result;
                        }
                    }
                }
            }
            if (this.IsHitTestVisible)
            {
                if (this.RenderTarget.Contains(parameters.HitPoint))
                {
                    this.IsMouseOver = true;
                    return new Input.HitTestResult(this);
                }
            }
            this.IsMouseOver = false;
            return new Input.HitTestResult();
        }

        /// <summary>
        /// This method is used to process any ui event handled by the element's logicial parent
        /// </summary>
        /// <param name="e">The <see cref="UIEventArgs"/> associated with the event</param>
        public void ProcessUIEvent(Input.UIEventArgs e)
        {
            Input.HitTestResult hitTestResult;
            if (typeof(Controls.IElementPresenter).IsAssignableFrom(this.GetType()))
            {
                if (typeof(Controls.IPanel).IsAssignableFrom(this.GetType()))
                {
                    foreach (UIElement element in ((Controls.IPanel)this).Children)
                    {
                        element.ProcessUIEvent(e);
                        if (e.IsHandled)
                        {
                            return;
                        }
                    }
                }
                else if (typeof(Controls.IDecorator).IsAssignableFrom(this.GetType()))
                {
                    if (((Controls.IDecorator)this).Child != null)
                    {
                        ((Controls.IDecorator)this).Child.ProcessUIEvent(e);
                        if (e.IsHandled)
                        {
                            return;
                        }
                    }
                }
            }
            if (typeof(MouseEventArgs).IsAssignableFrom(e.SourceEventArgs.GetType()))
            {
                Point mousePosition;
                mousePosition = ((MouseEventArgs)e.SourceEventArgs).Position.ToMediaPoint();
                hitTestResult = this.HitTest(new Input.PointHitTestParameters(mousePosition));
                if (!hitTestResult.HasHit)
                {
                    return;
                }
                switch (e.SourceEvent)
                {
                    case Input.UIEvent.MouseMove:
                        if (this.MouseMove != null)
                        {
                            this.MouseMove(this, (MouseMoveEventArgs)e.SourceEventArgs);
                        }
                        e.IsHandled = true;
                        break;
                    case Input.UIEvent.MouseButtonDown:
                        if (this.MouseButtonDown != null)
                        {
                            this.MouseButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                        }
                        e.IsHandled = true;
                        break;
                    case Input.UIEvent.MouseLeftButtonDown:
                        if (this.MouseLeftButtonDown != null)
                        {
                            this.MouseLeftButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                        }
                        e.IsHandled = true;
                        break;
                    case Input.UIEvent.MouseMiddleButtonDown:
                        if (this.MouseMiddleButtonDown != null)
                        {
                            this.MouseMiddleButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                        }
                        e.IsHandled = true;
                        break;
                    case Input.UIEvent.MouseRightButtonDown:
                        if (this.MouseRightButtonDown != null)
                        {
                            this.MouseRightButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                        }
                        e.IsHandled = true;
                        break;
                }
            }
        }

    }

}
