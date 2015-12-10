using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Media;

namespace Photon.Controls
{

    /// <summary>
    /// Provides a base class for elements that apply effects onto or around a single child element, such as <see cref="Border"/>
    /// </summary>
    public abstract class Decorator
        : UIElement, IDecorator
    {

        /// <summary>
        /// Describes the <see cref="Decorator"/>'s Child <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty ChildProperty = DependencyProperty.Register("Child", typeof(Decorator));
        /// <summary>
        /// Gets/sets the <see cref="Decorator"/>'s child <see cref="UIElement"/>
        /// </summary>
        public UIElement Child
        {
            get
            {
                return this.GetValue<UIElement>(Decorator.ChildProperty);
            }
            set
            {
                this.SetValue(Decorator.ChildProperty, value);
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Decorator"/>'s contents affect layout
        /// </summary>
        public bool ContentsAffectsLayout
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Decorator"/>'s content can align horizontally
        /// </summary>
        public bool ContentsAlignHorizontally
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Decorator"/>'s content can align vertically
        /// </summary>
        public bool ContentsAlignVertically
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Adds the specified child object
        /// </summary>
        /// <param name="child">An object representing the child to add</param>
        public void AddChild(object child)
        {
            if (this.Child != null)
            {
                throw new NotSupportedException("An element of type '" + this.GetType().FullName + "' does not support multiple contents");
            }
            this.Child = (UIElement)child;
        }

        /// <summary>
        /// Adds the specified text content
        /// </summary>
        /// <param name="text">A string representing the text to add</param>
        public void AddText(string text)
        {
            throw new NotSupportedException("An element of type '" + this.GetType().FullName + "' does not support direct text content");
        }

        /// <summary>
        /// Computes the specified <see cref="UIElement"/>'s offset
        /// </summary>
        /// <param name="child">The <see cref="UIElement"/> to compute the offset of</param>
        /// <returns>The <see cref="Media.Point"/> representing the specified <see cref="UIElement"/>'s offset</returns>
        public Point ComputeChildOffset(UIElement child)
        {
            double x, y;
            x = y = 0;
            if (!this.Width.HasValue)
            {
                x = -child.Margin.Left;
            }
            if (!this.Height.HasValue)
            {
                y = -child.Margin.Top;
            }
            return new Point(x, y);
        }

        /// <summary>
        /// Measures the <see cref="Decorator"/>'s contents
        /// </summary>
        /// <returns>The <see cref="Media.Size"/> of the <see cref="Decorator"/>'s contents</returns>
        public Size MeasureContents()
        {
            if(this.Child == null)
            {
                return Size.Empty;
            }
            this.Child.InvalidateLayout();
            return this.Child.LayoutSize;
        }

        /// <summary>
        /// When overriden in a class, this method provides means to run code whenever a <see cref="DependencyProperty"/> has changed
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the modified property</param>
        /// <param name="originalValue">An object representing the property's value before the suffered change(s)</param>
        /// <param name="value">An object representing the property's actual (new) value</param>
        protected override void OnPropertyChanged(string propertyName, object originalValue, object value)
        {
            base.OnPropertyChanged(propertyName, originalValue, value);
            if(propertyName == Decorator.ChildProperty.Name)
            {
                UIElement child;
                if (originalValue != null)
                {
                    child = (UIElement)originalValue;
                    child.Parent = null;
                }
                if(value != null)
                {
                    child = (UIElement)value;
                    child.Parent = this;
                }
            }
        }

        /// <summary>
        /// When overriden in a class, renders the visual
        /// </summary>
        /// <param name="drawingContext">The <see cref="DrawingContext"/> in whihc to render the visual</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.Background != null)
            {
                drawingContext.DrawRectangle(this.RenderTarget, Thickness.Empty, this.Background, null);
            }
            if(this.Child != null)
            {
                this.Child.Render(drawingContext);
            }
        }

    }

}
