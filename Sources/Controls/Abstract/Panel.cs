using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Media;
using System.Collections.Specialized;

namespace Photon.Controls
{

    /// <summary>
    /// Provides a base class for all Panel elements. Use Panel elements to position and arrange child objects in Windows Presentation Foundation (WPF) applications
    /// </summary>
    public abstract class Panel
        : UIElement, IPanel
    {

        /// <summary>
        /// Initializes a new <see cref="Panel"/> instance
        /// </summary>
        public Panel()
        {
            this.Children = new UIElementCollection();
            this.Children.CollectionChanged += this.OnChildrenCollectionChanged;
        }

        /// <summary>
        /// Gets a <see cref="UIElementCollection"/> containing all of the <see cref="Panel"/>'s children
        /// </summary>
        public UIElementCollection Children { get; private set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Panel"/>'s contents affect its layout
        /// </summary>
        public abstract bool ContentsAffectsLayout { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Panel"/>'s content can align horizontally
        /// </summary>
        public abstract bool ContentsAlignHorizontally { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Panel"/>'s content can align vertically
        /// </summary>
        public abstract bool ContentsAlignVertically { get; }

        /// <summary>
        /// Computes the specified <see cref="UIElement"/>'s offset
        /// </summary>
        /// <param name="child">The <see cref="UIElement"/> to compute the offset of</param>
        /// <returns>The <see cref="Media.Point"/> representing the specified <see cref="UIElement"/>'s offset</returns>
        public abstract Point ComputeChildOffset(UIElement child);

        /// <summary>
        /// Measures the <see cref="Panel"/>'s contents
        /// </summary>
        /// <returns>The <see cref="Media.Size"/> of the <see cref="Panel"/>'s contents</returns>
        public abstract Size MeasureContents();

        /// <summary>
        /// Adds the specified child object
        /// </summary>
        /// <param name="child">An object representing the child to add</param>
        public void AddChild(object child)
        {
            this.Children.Add((UIElement)child);
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
        /// Handles the <see cref="Panel.Children"/>'s <see cref="INotifyCollectionChanged.CollectionChanged"/> event
        /// </summary>
        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UIElement child;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    child = (UIElement)e.NewItems[0];
                    child.Parent = this;
                    this.OnChildAdded(child);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    child = (UIElement)e.OldItems[0];
                    child.Parent = null;
                    this.OnChildRemoved(child);
                    break;
            }
        }

        /// <summary>
        /// When overriden in a class, allows the execution of code whenever a child <see cref="UIElement"/> has been added to the <see cref="Panel"/>
        /// </summary>
        /// <param name="child">The <see cref="UIElement"/> that has been added</param>
        protected virtual void OnChildAdded(UIElement child)
        {

        }

        /// <summary>
        /// When overriden in a class, allows the execution of code whenever a child <see cref="UIElement"/> has been removed from the <see cref="Panel"/>
        /// </summary>
        /// <param name="child">The <see cref="UIElement"/> that has been removed</param>
        protected virtual void OnChildRemoved(UIElement child)
        {

        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element has been rendered
        /// </summary>
        ///<param name="drawingContext">The <see cref="DrawingContext"/> in which the element has been rendered</param>
        protected abstract override void OnRender(DrawingContext drawingContext);

    }

}
