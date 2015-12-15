using OpenTK.Graphics.OpenGL;
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
        : DependencyObject, IDisposable
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
        /// Begins using the <see cref="Brush"/> to render the specified <see cref="Drawing"/>. Must be followed by a call to the <see cref="Brush.EndUse"/> method
        /// </summary>
        /// <param name="drawing">The <see cref="Drawing"/> to render</param>
        public abstract void BeginUse(Drawing drawing);

        /// <summary>
        /// Ends using the <see cref="Brush"/>
        /// </summary>
        public abstract void EndUse();

        /// <summary>
        /// Clones the brush
        /// </summary>
        /// <returns>The clone of the <see cref="Brush"/></returns>
        public abstract Brush Clone();

        /// <summary>
        /// When overriden in a class, disposes of the <see cref="Brush"/> and its resources
        /// </summary>
        public virtual void Dispose()
        {

        }

    }

}
