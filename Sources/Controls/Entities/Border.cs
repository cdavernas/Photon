using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Media;

namespace Photon.Controls
{

    /// <summary>
    /// Draws a border, background, or both around another element
    /// </summary>
    public class Border
        : Decorator, IPaddedElement, IBorderedElement
    {

        /// <summary>
        /// Describes the <see cref="Border.BorderBrush"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Border));
        /// <summary>
        /// Gets/sets the <see cref="Media.Brush"/> used to paint the element's borders
        /// </summary>
        public Brush BorderBrush
        {
            get
            {
                return this.GetValue<Brush>(Border.BorderBrushProperty);
            }

            set
            {
                this.SetValue(Border.BorderBrushProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="Border.BorderThickness"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Border));
        /// <summary>
        /// Gets/sets the <see cref="Media.Thickness"/> of the element's borders
        /// </summary>
        public Thickness BorderThickness

        {
            get
            {
                return this.GetValue<Thickness>(Border.BorderThicknessProperty);
            }

            set
            {
                this.SetValue(Border.BorderThicknessProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="Border.Padding"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Border));
        /// <summary>
        /// Gets/sets the <see cref="Media.Thickness"/> representing the <see cref="Border"/>'s padding
        /// </summary>
        public Thickness Padding
        {
            get
            {
                return this.GetValue<Thickness>(Border.PaddingProperty);
            }

            set
            {
                this.SetValue(Border.PaddingProperty, value);
            }
        }

        /// <summary>
        /// When overriden in a class, renders the visual
        /// </summary>
        /// <param name="drawingContext">The <see cref="DrawingContext"/> in whihc to render the visual</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if(this.Background != null || this.BorderBrush != null)
            {
                drawingContext.DrawRectangle(this.RenderTarget, this.BorderThickness, this.Background, this.BorderBrush);
            }
            if (this.Child != null)
            {
                this.Child.Render(drawingContext);
            }
        }

    }

}
