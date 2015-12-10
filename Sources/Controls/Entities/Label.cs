using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Media;

namespace Photon.Controls
{

    /// <summary>
    /// Represents the text label for a control and provides support for access keys
    /// </summary>
    public class Label
        : UIElement, ITextPresenter
    {

        /// <summary>
        /// Describes the <see cref="Label.FontFamily"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(Label), Media.FontFamily.Default);
        /// <summary>
        /// Gets/sets the <see cref="Media.FontFamily"/> of the <see cref="System.Drawing.Font"/> with which to paint the <see cref="Label"/>'s text
        /// </summary>
        public Media.FontFamily FontFamily
        {
            get
            {
                return this.GetValue<Media.FontFamily>(Label.FontFamilyProperty);
            }

            set
            {
                this.SetValue(Label.FontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="Label.FontSize"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(Label), 10.0);
        /// <summary>
        /// Gets/sets the em size of the <see cref="System.Drawing.Font"/> with which to paint the <see cref="Label"/>'s text
        /// </summary>
        public double FontSize
        {
            get
            {
                return this.GetValue<double>(Label.FontSizeProperty);
            }

            set
            {
                this.SetValue(Label.FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="Label.FontStyle"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty FontStyleProperty = DependencyProperty.Register("FontStyle", typeof(Label), FontStyle.Regular);
        /// <summary>
        /// Gets/sets the <see cref="System.Drawing.FontStyle"/> of the <see cref="System.Drawing.Font"/> with which to paint the <see cref="Label"/>'s text
        /// </summary>
        public FontStyle FontStyle
        {
            get
            {
                return this.GetValue<FontStyle>(Label.FontStyleProperty);
            }

            set
            {
                this.SetValue(Label.FontStyleProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="Label.Foreground"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Label), new SolidColorBrush(Color.Black));
        /// <summary>
        /// Gets/sets the <see cref="Media.Brush"/> with which to paint the <see cref="Label"/>'s text
        /// </summary>
        public Media.Brush Foreground
        {
            get
            {
                return this.GetValue<Media.Brush>(Label.ForegroundProperty);
            }

            set
            {
                this.SetValue(Label.ForegroundProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="Label.Padding"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Label));
        /// <summary>
        /// Gets/sets the <see cref="Media.Thickness"/> representing the <see cref="Label"/>'s padding
        /// </summary>
        public Thickness Padding
        {
            get
            {
                return this.GetValue<Media.Thickness>(Label.PaddingProperty);
            }

            set
            {
                this.SetValue(Label.PaddingProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="Label.Text"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(Label));
        /// <summary>
        /// Gets/sets the text displayed by the <see cref="Label"/>
        /// </summary>
        public string Text

        {
            get
            {
                return this.GetValue<string>(Label.TextProperty);
            }

            set
            {
                this.SetValue(Label.TextProperty, value);
            }
        }

        /// <summary>
        /// Gets the text's <see cref="System.Drawing.Font"/>, based on the <see cref="Label.FontFamily"/>, <see cref="Label.FontSize"/> and <see cref="Label.FontStyle"/> properties
        /// </summary>
        public Font Font
        {
            get
            {
                return new Font(this.FontFamily.ToGdiFontFamily(), Convert.ToSingle(this.FontSize), this.FontStyle);
            }
        }

        /// <summary>
        /// Adds the specified child object
        /// </summary>
        /// <param name="child">An object representing the child to add</param>
        public void AddChild(object child)
        {
            throw new NotSupportedException("An element of type '" + this.GetType().FullName + "' only supports direct text contents");
        }

        /// <summary>
        /// Adds the specified text content
        /// </summary>
        /// <param name="text">A string representing the text to add</param>
        public void AddText(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// When overriden in a class, this method allows the execution of code whenever the element has been rendered
        /// </summary>
        ///<param name="drawingContext">The <see cref="DrawingContext"/> in which the element has been rendered</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            Media.Size textSize;
            Media.Rectangle layoutSlot;
            if (!string.IsNullOrWhiteSpace(this.Text)
                && this.Foreground != null)
            {
                textSize = DrawingContext.MeasureText(this.Text, this.Font);
                layoutSlot = LayoutInformation.GetLayoutSlot(this);
                layoutSlot = new Media.Rectangle(new Media.Point(layoutSlot.Position.X - textSize.Width / 2, layoutSlot.Position.Y - textSize.Height / 2), layoutSlot.Size);
                drawingContext.DrawText(this.Text, layoutSlot.Position, this.Font, this.Foreground);
            }
        }

    }

}
