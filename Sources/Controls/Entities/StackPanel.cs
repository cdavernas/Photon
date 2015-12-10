using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Media;

namespace Photon.Controls
{

    /// <summary>
    /// Arranges child elements into a single line that can be oriented horizontally or vertically
    /// </summary>
    public class StackPanel
        : Panel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StackPanel"/> type
        /// </summary>
        public StackPanel()
        {

        }

        /// <summary>
        /// Describes the <see cref="StackPanel.OffsetProperty"/> attached <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty OffsetProperty = DependencyProperty.RegisterAttached("Offset", typeof(Media.Point), typeof(StackPanel));
        /// <summary>
        /// Gets a <see cref="Media.Point"/> representing the specified <see cref="UIElement"/>'s offset
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to get the offset of</param>
        /// <returns>A <see cref="Media.Point"/> representing the specified <see cref="UIElement"/>'s offset</returns>
        public static Media.Point GetOffset(UIElement element)
        {
            if (!element.DependencyProperties.ContainsKey(StackPanel.OffsetProperty))
            {
                throw new ArgumentException("The UIElement passed as 'element' argument is not the child of a StackPanel");
            }
            return element.GetValue<Media.Point>(StackPanel.OffsetProperty);
        }
        /// <summary>
        /// Sets the offset of the specified <see cref="UIElement"/>
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to set the offset of</param>
        /// <param name="offset">A <see cref="Media.Point"/> representing the <see cref="UIElement"/>'s offset</param>
        public static void SetOffset(UIElement element, Media.Point offset)
        {
            if (!element.DependencyProperties.ContainsKey(StackPanel.OffsetProperty))
            {
                throw new ArgumentException("The UIElement passed as 'element' argument is not the child of a StackPanel");
            }
            element.SetValue(StackPanel.OffsetProperty, offset);
        }

        /// <summary>
        /// Describes the <see cref="StackPanel.Orientation"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(StackPanel));
        /// <summary>
        /// Gets/sets the <see cref="StackPanel"/>'s content <see cref="Controls.Orientation"/>
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                return this.GetValue<Orientation>(StackPanel.OrientationProperty);
            }
            set
            {
                this.SetValue(StackPanel.OrientationProperty, value);
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="StackPanel"/>'s contents affect its layout
        /// </summary>
        public override bool ContentsAffectsLayout
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="StackPanel"/>'s content can align horizontally
        /// </summary>
        public override bool ContentsAlignHorizontally
        {
            get
            {
                switch (this.Orientation)
                {
                    case Orientation.Horizontal:
                        return false;
                    case Orientation.Vertical:
                        return true;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="StackPanel"/>'s content can align vertically
        /// </summary>
        public override bool ContentsAlignVertically
        {
            get
            {
                switch (this.Orientation)
                {
                    case Orientation.Horizontal:
                        return true;
                    case Orientation.Vertical:
                        return false;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Computes the specified <see cref="UIElement"/>'s offset
        /// </summary>
        /// <param name="child">The <see cref="UIElement"/> to compute the offset of</param>
        /// <returns>The <see cref="Media.Point"/> representing the specified <see cref="UIElement"/>'s offset</returns>
        public override Media.Point ComputeChildOffset(UIElement child)
        {
            Media.Point offset;
            double x, y;
            offset = StackPanel.GetOffset(child);
            x = offset.X;
            y = offset.Y;
            if (!this.Width.HasValue 
                && this.Orientation != Orientation.Horizontal)
            {
                x -= child.Margin.Left;
            }
            if (!this.Height.HasValue
                && this.Orientation != Orientation.Vertical)
            {
                y -= child.Margin.Top;
            }
            return new Point(x, y);
        }

        /// <summary>
        /// Measures the <see cref="Panel"/>'s contents
        /// </summary>
        /// <returns>The <see cref="Media.Size"/> of the <see cref="Panel"/>'s contents</returns>
        public override Size MeasureContents()
        {
            UIElement lastElement;
            Media.Point lastElementOffset;
            double width, height;
            lastElement = this.Children.Last();
            lastElementOffset = StackPanel.GetOffset(lastElement);
            if (this.Children.Count < 1)
            {
                return Size.Empty;
            }
            switch (this.Orientation)
            {
                case Orientation.Horizontal:
                    width = lastElementOffset.X + lastElement.LayoutSize.Width;
                    height = this.Children.Max(c => c.LayoutSize.Height);
                    break;
                case Orientation.Vertical:
                    width = this.Children.Max(c => c.LayoutSize.Width); 
                    height = lastElementOffset.Y + lastElement.LayoutSize.Height;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return new Size(width, height);
        }

        /// <summary>
        /// When overriden in a class, allows the execution of code whenever a child <see cref="UIElement"/> has been added to the <see cref="StackPanel"/>
        /// </summary>
        /// <param name="child">The <see cref="UIElement"/> that has been added</param>
        protected override void OnChildAdded(UIElement child)
        {
            UIElement previousElement;
            Media.Point offset;
            int childIndex;
            base.OnChildAdded(child);
            childIndex = this.Children.IndexOf(child);
            if (childIndex == 0)
            {
                offset = Media.Point.Empty;
            }
            else
            {
                previousElement = this.Children.ElementAt(childIndex - 1);
                previousElement.InvalidateLayout();
                offset = StackPanel.GetOffset(previousElement);
                switch (this.Orientation)
                {
                    case Orientation.Horizontal:
                        offset += new Point(previousElement.LayoutSize.Width, 0);
                        break;
                    case Orientation.Vertical:
                        offset += new Point(0, previousElement.LayoutSize.Height);
                        break;
                }
            }
            child.DependencyProperties.Add(StackPanel.OffsetProperty, offset);
            if (this.IsLoaded)
            {
                this.InvalidateLayout();
            }
        }

        /// <summary>
        /// When overriden in a class, allows the execution of code whenever a child <see cref="UIElement"/> has been removed from the <see cref="StackPanel"/>
        /// </summary>
        /// <param name="child">The <see cref="UIElement"/> that has been removed</param>
        protected override void OnChildRemoved(UIElement child)
        {
            base.OnChildRemoved(child);
            child.DependencyProperties.Remove(StackPanel.OffsetProperty);
            this.InvalidateLayout();
        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element has been rendered
        /// </summary>
        ///<param name="drawingContext">The <see cref="DrawingContext"/> in which the element has been rendered</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if(this.Background != null)
            {
                drawingContext.DrawRectangle(this.RenderTarget, Thickness.Empty, this.Background, null);
            }
            foreach(UIElement element in this.Children)
            {
                element.Render(drawingContext);
            }
        }

    }

}
