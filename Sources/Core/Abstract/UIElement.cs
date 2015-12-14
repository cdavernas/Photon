using Photon.Media;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Photon.Threading;

namespace Photon
{

    /// <summary>
    /// UIElement is a base class for core level implementations building on Photon elements and basic presentation characteristics.
    /// </summary>
    public abstract class UIElement
        : Visual, IUIElement
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
            this.Resources = new ResourceDictionary();
        }

        /// <summary>
        /// Gets/Sets the <see cref="Controls.IContentPresenter"/>'s logical parent 
        /// </summary>
        [XmlIgnore]
        public Controls.IContentPresenter Parent { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="UIElement"/>'s <see cref="ResourceDictionary"/>
        /// </summary>
        public ResourceDictionary Resources { get; set; }

        /// <summary>
        /// Describes the <see cref="UIElement.Style"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty StyleProperty = DependencyProperty.Register("Style", typeof(UIElement));
        /// <summary>
        /// Gets/sets the <see cref="UIElement"/>'s style
        /// </summary>
        public Style Style
        {
            get
            {
                return this.GetValue<Style>(UIElement.StyleProperty);
            }
            set
            {
                this.SetValue(UIElement.StyleProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="UIElement.HorizontalAlignment"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.VerticalAlignment"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.Width"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.MinWidth"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.MaxWidth"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.Height"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.MinHeight"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.MaxHeight"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.Background"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.Margin"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.Cursor"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.Opacity"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.Visibility"/> <see cref="DependencyProperty"/>
        /// </summary>
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

        /// <summary>
        /// Describes the <see cref="UIElement.IsMouseOver"/> <see cref="DependencyProperty"/>
        /// </summary>
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
        /// Describes the <see cref="UIElement.IsHitTestVisible"/> <see cref="DependencyProperty"/>
        /// </summary>
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
        /// This method is fired whenever the element needs to be located
        /// </summary>
        private void Locate()
        {
            Media.Rectangle layoutSlot;
            double x, y;
            //Check whether or not the last position was computed after the last invalidation
            if (this.LastLocated.CompareTo(this.LastInvalidated) != -1)
            {
                //No need to recompute the position
                return;
            }
            //Check whether or not the element is within a visual tree
            if (this.Parent == null)
            {
                //The element is not within a visual tree. Set the element's render position to an empty point then return
                this.RenderPosition = Media.Point.Empty;
                return;
            }
            //Retrieve the parent's layout slot
            layoutSlot = Controls.LayoutInformation.GetLayoutSlot(this.Parent);
            //Sets the x value to the layout slot's x position plus the element's left margin value
            x = layoutSlot.Position.X + this.Margin.Left;
            //Modify the x value according to the element's HorizontalAlignment
            if (this.Parent.ContentsAlignHorizontally)
            {
                switch (this.HorizontalAlignment)
                {
                    case HorizontalAlignment.Stretch:
                    case HorizontalAlignment.Left:
                        break;
                    case HorizontalAlignment.Center:
                        x += (layoutSlot.Width / 2) - (this.RenderSize.Width / 2);
                        break;
                    case HorizontalAlignment.Right:
                        x += layoutSlot.Width - this.RenderSize.Width;
                        break;
                }
            }
            //Sets the y value to the layout slot's y position plus the element's top margin value
            y = layoutSlot.Position.Y + this.Margin.Top;
            //Modify the x value according to the element's VerticalAlignment
            if (this.Parent.ContentsAlignVertically)
            {
                switch (this.VerticalAlignment)
                {
                    case VerticalAlignment.Stretch:
                    case VerticalAlignment.Top:
                        break;
                    case VerticalAlignment.Center:
                        y += (layoutSlot.Height / 2) - (this.RenderSize.Height / 2);
                        break;
                    case VerticalAlignment.Bottom:
                        y += layoutSlot.Height - this.RenderSize.Height;
                        break;
                }
            }
            //Set the element's RenderPosition
            this.RenderPosition = new Point(x, y) + ((Controls.IContentPresenter)this.Parent).ComputeChildOffset(this);
            //Set the date and time at which the last measure occured
            this.LastLocated = DateTime.UtcNow;
        }

        /// <summary>
        /// This method is fired whenever the element needs to be measured
        /// </summary>
        private void Measure()
        {
            Media.Rectangle layoutSlot;
            double width, height;
            bool relocate;
            //Check whether or not the last measure was made after the last invalidation
            if (this.LastMeasured.CompareTo(this.LastInvalidated) != -1)
            {
                //No need to recompute the measurements
                return;
            }
            //Check whether or not the element is within a visual tree
            if (this.Parent == null)
            {
                //The element is not within a visual tree. Set the element's render size to an empty size then return
                this.RenderSize = Media.Size.Empty;
                return;
            }
            //Retrieve the parent's layout slot
            layoutSlot = Controls.LayoutInformation.GetLayoutSlot(this.Parent);
            width = height = 0;
            relocate = false;
            //Check whether or not the element has been given a Width value
            if (this.Width.HasValue)
            {
                //The Width has been set
                width = this.Width.Value;
            }
            else
            {
                if ( this.Parent.ContentsAlignHorizontally
                    && this.HorizontalAlignment == HorizontalAlignment.Stretch)
                {
                    //The element's HorizontalAlignment has been set to strecth, therefore its width equals its layout slot's
                    width = layoutSlot.Width - this.Margin.Left  - this.Margin.Right;
                }
                else
                {
                    //Check whether or not the element is an IContentPresenter
                    if (typeof(Controls.IContentPresenter).IsAssignableFrom(this.GetType()))
                    {
                        width = ((Controls.IContentPresenter)this).MeasureContents().Width;
                        if (typeof(Controls.IPaddedElement).IsAssignableFrom(this.GetType()))
                        {
                            width += ((Controls.IPaddedElement)this).Padding.Left + ((Controls.IPaddedElement)this).Padding.Right;
                        }
                        relocate = true;
                    }
                }
                //Check whether or not the MinWidth property has been set
                if (this.MinWidth.HasValue)
                {
                    if (width < this.MinWidth.Value)
                    {
                        width = this.MinWidth.Value;
                    }
                }
                //Check whether or not the MaxWidth property has been set
                if (this.MaxWidth.HasValue)
                {
                    if (width > this.MaxWidth.Value)
                    {
                        width = this.MaxWidth.Value;
                    }
                }
            }
            //Check whether or not the element has been given a Height value
            if (this.Height.HasValue)
            {
                //The Height has been set
                height = this.Height.Value;
            }
            else
            {
                if (this.Parent.ContentsAlignVertically
                    && this.VerticalAlignment == VerticalAlignment.Stretch)
                {
                    //The element's VerticalAlignment has been set to strecth, therefore its height equals its layout slot's
                    height = layoutSlot.Height - this.Margin.Top - this.Margin.Bottom;
                }
                else
                {
                    //Check whether or not the element is an IContentPresenter
                    if (typeof(Controls.IContentPresenter).IsAssignableFrom(this.GetType()))
                    {
                        height = ((Controls.IContentPresenter)this).MeasureContents().Height;
                        if (typeof(Controls.IPaddedElement).IsAssignableFrom(this.GetType()))
                        {
                            height += ((Controls.IPaddedElement)this).Padding.Top + ((Controls.IPaddedElement)this).Padding.Bottom;
                        }
                        relocate = true;
                    }
                }
                //Check whether or not the MinHeight property has been set
                if (this.MinHeight.HasValue)
                {
                    if (height < this.MinHeight.Value)
                    {
                        height = this.MinHeight.Value;
                    }
                }
                //Check whether or not the MaxHeight property has been set
                if (this.MaxHeight.HasValue)
                {
                    if (height > this.MaxHeight.Value)
                    {
                        height = this.MaxHeight.Value;
                    }
                }
            }
            //Set the element's RenderSize
            this.RenderSize = new Size(width, height);
            //Set the element's LayoutSize, which include margin values
            this.LayoutSize = new Size(this.Margin.Left + width + this.Margin.Right, this.Margin.Top + height + this.Margin.Bottom);
            //Set the date and time at which the last measure occured
            this.LastMeasured = DateTime.UtcNow;
            if (relocate)
            {
                this.LastInvalidated = DateTime.UtcNow;
                this.Locate();
            }
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
        /// Generates a new <see cref="Media.Drawing"/> of the <see cref="UIElement"/>
        /// </summary>
        protected override void OnInvalidateVisual()
        {
            base.OnInvalidateVisual();
            this.InvalidateLayout();
        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element's layout has been invalidated
        /// </summary>
        protected virtual void OnInvalidateLayout()
        {
            //Check if the UIElement is within a visual tree
            if (this.Parent == null)
            {
                //The UIElement is not within a visual tree and will therefore not be rendered
                return;
            }
        }

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
            if (propertyName == UIElement.StyleProperty.Name)
            {
                if(value != null)
                {
                    ((Style)value).ApplyTo(this);
                }
                return;
            }
            if (propertyName == UIElement.VisibilityProperty.Name)
            {
                if(this.VisibilityChanged != null)
                {
                    this.VisibilityChanged(this, new EventArgs());
                }
                return;
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
        /// Determines whether or not the control is hit by the specified <see cref="Input.PointHitTestParameters"/>
        /// </summary>
        /// <param name="parameters">The <see cref="Input.PointHitTestParameters"/> configuring the element</param>
        /// <returns>A <see cref="Input.HitTestResult"/> representing the result of the hit test</returns>
        public Input.HitTestResult HitTest(Input.PointHitTestParameters parameters)
        {
            Input.HitTestResult result;
            if (typeof(Controls.IContentPresenter).IsAssignableFrom(this.GetType()))
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
        /// <param name="e">The <see cref="Input.UIEventArgs"/> associated with the event</param>
        public void ProcessUIEvent(Input.UIEventArgs e)
        {
            Input.HitTestResult hitTestResult;
            if (typeof(Controls.IContentPresenter).IsAssignableFrom(this.GetType()))
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
