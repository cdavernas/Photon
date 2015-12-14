using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media
{

    /// <summary>
    /// Renders a <see cref="Geometry"/> using the specified fill brush, stroke brush and stroke thickness
    /// </summary>
    public sealed class GeometryDrawing
        : Drawing
    {

        /// <summary>
        /// Initializes a new <see cref="GeometryDrawing"/>
        /// </summary>
        /// <param name="primitiveType">The <see cref="OpenTK.Graphics.OpenGL.PrimitiveType"/> with which to render the <see cref="GeometryDrawing"/></param>
        /// <param name="geometry">The <see cref="Media.Geometry"/> to draw</param>
        public GeometryDrawing(PrimitiveType primitiveType, Geometry geometry)
        {
            this.PrimitiveType = primitiveType;
            this.Geometry = geometry;
        }

        /// <summary>
        /// Gets a <see cref="Rectangle"/> representing the bounds of the <see cref="GeometryDrawing"/>
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                return this.Geometry.Bounds;
            }
        }

        /// <summary>
        /// Describes the <see cref="GeometryDrawing.PrimitiveType"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty PrimitiveTypeProperty = DependencyProperty.Register("PrimitiveType", typeof(GeometryDrawing));
        /// <summary>
        /// Gets/sets the <see cref="OpenTK.Graphics.OpenGL.PrimitiveType"/> with which to render the <see cref="GeometryDrawing"/>
        /// </summary>
        public PrimitiveType PrimitiveType
        {
            get
            {
                return this.GetValue<PrimitiveType>(GeometryDrawing.PrimitiveTypeProperty);
            }
            set
            {
                this.SetValue(GeometryDrawing.PrimitiveTypeProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="GeometryDrawing.Geometry"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty GeometryProperty = DependencyProperty.Register("Geometry", typeof(GeometryDrawing));
        /// <summary>
        /// Gets/sets he <see cref="Geometry"/> to render
        /// </summary>
        public Geometry Geometry
        {
            get
            {
                return this.GetValue<Geometry>(GeometryDrawing.GeometryProperty);
            }
            set
            {
                this.SetValue(GeometryDrawing.GeometryProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="GeometryDrawing.FillBrush"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty FillBrushProperty = DependencyProperty.Register("FillBrush", typeof(GeometryDrawing));
        /// <summary>
        /// Gets/sets the <see cref="Media.Brush"/> used to render the <see cref="Media.Geometry"/>
        /// </summary>
        public Brush FillBrush
        {
            get
            {
                return this.GetValue<Brush>(GeometryDrawing.FillBrushProperty);
            }
            set
            {
                this.SetValue(GeometryDrawing.FillBrushProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="GeometryDrawing.StrokeBrush"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty StrokeBrushProperty = DependencyProperty.Register("StrokeBrush", typeof(GeometryDrawing));
        /// <summary>
        /// Gets/sets the <see cref="Media.Brush"/> used to render the <see cref="Media.Geometry"/>'s contour
        /// </summary>
        public Brush StrokeBrush
        {
            get
            {
                return this.GetValue<Brush>(GeometryDrawing.StrokeBrushProperty);
            }
            set
            {
                this.SetValue(GeometryDrawing.StrokeBrushProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="GeometryDrawing.StrokeThickness"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(GeometryDrawing));
        /// <summary>
        /// Gets/sets a double representing the thickness of the <see cref="Media.Geometry"/>'s countour
        /// </summary>
        public double StrokeThickness
        {
            get
            {
                return this.GetValue<double>(GeometryDrawing.StrokeThicknessProperty);
            }
            set
            {
                this.SetValue(GeometryDrawing.StrokeThicknessProperty, value);
            }
        }

        /// <summary>
        /// Genereates the <see cref="Drawing"/>'s <see cref="Media.VertexBufferObject"/>
        /// </summary>
        protected override void GenerateBuffer()
        {
            ushort[] indices;
            if(this.VertexBufferObject != null)
            {
                this.VertexBufferObject.Dispose();
            }
            this.VertexBufferObject = new VertexBufferObject(this.Geometry.Points.Count(), this.Geometry.Points.Count(), this.PrimitiveType);
            indices = new ushort[this.Geometry.Points.Count()];
            for(ushort indice = 0; indice < indices.Length; indice++)
            {
                indices[indice] = indice;
            }
            this.VertexBufferObject.SetIndices(indices);
            this.VertexBufferObject.SetVertices(this.Geometry.Points.ToVertexArray());
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
            if(propertyName == GeometryDrawing.GeometryProperty.Name)
            {
                this.GenerateBuffer();
            }
        }

    }

}
