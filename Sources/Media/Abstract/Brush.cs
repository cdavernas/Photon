using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Defines objects used to paint graphical objects. Classes that derive from Brush describe how the area is painted
    /// </summary>
    [TypeConverter(typeof(BrushConverter))]
    public abstract class Brush
        : DependencyObject
    {

        /// <summary>
        /// Describes the <see cref="Brush.Opacity"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty OpacityProperty = DependencyProperty.Register("Opacity", typeof(Brush), 1.0);
        /// <summary>
        /// Gets/Sets the opacity of the Brush
        /// </summary>
        public double Opacity
        {
            get
            {
                return this.GetValue<double>(Brush.OpacityProperty);
            }
            set
            {
                this.SetValue(Brush.OpacityProperty, value);
            }
        }

        /// <summary>
        /// Uses the brush to paint a geometry
        /// </summary>
        /// <param name="geometryBounds">The <see cref="Media.Rectangle"/> representing the geometry's bounds</param>
        internal abstract void Use(Rectangle geometryBounds);

        /// <summary>
        /// Clones the brush
        /// </summary>
        /// <returns>The clone of the <see cref="Brush"/></returns>
        public abstract Brush Clone();

    }

}
