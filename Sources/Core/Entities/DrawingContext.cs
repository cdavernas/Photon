using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// Represents the context into which all <see cref="UIElement"/> are rendered<para></para>
    /// This class defines high-level methods to render basic elements into OpenGL, thanks to the OpenTK framework
    /// </summary>
    public class DrawingContext
    {

        /// <summary>
        /// The <see cref="OpenTK.Graphics.TextPrinter"/> used by <see cref="DrawingContext"/> instances to render text
        /// </summary>
        #pragma warning disable 612,618
        private static OpenTK.Graphics.TextPrinter TextPrinter = new OpenTK.Graphics.TextPrinter();
        #pragma warning restore 612, 618

        /// <summary>
        /// Intializes a new <see cref="DrawingContext"/>
        /// </summary>
        public DrawingContext(Window window)
        {
            this.Window = window;
        }

        /// <summary>
        /// Gets the <see cref="Window"/> the <see cref="DrawingContext"/> belongs to
        /// </summary>
        public Window Window { get; private set; }

        /// <summary>
        /// Begins a new rendering pass
        /// </summary>
        public void BeginRenderPass()
        {
            //Clears the buffers
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //Set the clear color
            GL.ClearColor(0, 0, 0, 0);
            //Disable depth hit test
            GL.Disable(EnableCap.DepthTest);
            //Enable alpha blending
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //Sets the viewport
            GL.Viewport(0, 0, (int)this.Window.RenderTarget.Width, (int)this.Window.RenderTarget.Height);
            //Defines the 2d orthographic projection
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, (int)this.Window.RenderTarget.Width, (int)this.Window.RenderTarget.Height, 0, -1, 1);
        }

        ///// <summary>
        ///// Draws the specified <see cref="Media.Drawing"/>
        ///// </summary>
        ///// <param name="drawing">The <see cref="Media.Drawing"/> to draw</param>
        //public void Draw(Media.Drawing drawing)
        //{
        //    if (typeof(Media.GeometryDrawing).IsAssignableFrom(drawing.GetType()))
        //    {
        //        Media.GeometryDrawing geometryDrawing;
        //        geometryDrawing = (Media.GeometryDrawing)drawing;
        //        if (geometryDrawing.FillBrush != null)
        //        {
        //            geometryDrawing.FillBrush.BeginUse(drawing);
        //            geometryDrawing.Render();
        //            geometryDrawing.FillBrush.EndUse();
        //        }
        //        return;
        //    }
        //}

        ///// <summary>
        ///// Draw the specified <see cref="Media.Geometry"/> with the specified stroke thickness, fill brush and stroke brush
        ///// </summary>
        ///// <param name="geometry">The <see cref="Media.Geometry"/> to draw</param>
        ///// <param name="primitiveType">The <see cref="PrimitiveType"/> used to render the specified <see cref="Media.Geometry"/></param>
        ///// <param name="strokeThickness">The thickness of the specified <see cref="Media.Geometry"/>'s contour</param>
        ///// <param name="fillBrush">The <see cref="Media.Brush"/> used to paint the inside of the specified <see cref="Media.Geometry"/></param>
        ///// <param name="strokeBrush">The <see cref="Media.Brush"/> used to draw the specified <see cref="Media.Geometry"/>'s contour</param>
        //public void DrawGeometry(Media.Geometry geometry, PrimitiveType primitiveType, double strokeThickness, Media.Brush fillBrush, Media.Brush strokeBrush)
        //{
        //    Media.GeometryDrawing geometryDrawing;
        //    geometryDrawing = new Media.GeometryDrawing(primitiveType, geometry) { StrokeThickness = strokeThickness, FillBrush = fillBrush, StrokeBrush = strokeBrush };
        //    this.Draw(geometryDrawing);
        //    geometryDrawing.Dispose();
        //}

        /// <summary>
        /// Draws a rectangle with the specified width, height, border thickness and brushes
        /// </summary>
        /// <param name="rectangle">The <see cref="Media.Rectangle"/> representing the rectangle's width and height</param>
        /// <param name="borderThickness">The <see cref="Media.Thickness"/> representing the rectangle's border thickness</param>
        /// <param name="fillBrush">The <see cref="Media.Brush"/> with which to paint the rectangle's fill</param>
        /// <param name="borderBrush">The <see cref="Media.Brush"/> with which to paaint the rectangle's border brush</param>
        public void DrawRectangle(Media.Rectangle rectangle, Media.Thickness borderThickness, Media.Brush fillBrush, Media.Brush borderBrush)
        {
            Media.Geometry geometry;
            Media.GeometryDrawing geometryDrawing;
            float leftThickness, topThickness, rightThickness, bottomThickness;
            geometry = rectangle.ToGeometry();
            geometryDrawing = new Media.GeometryDrawing(PrimitiveType.Quads, geometry);
            leftThickness = Convert.ToSingle(borderThickness.Left);
            topThickness = Convert.ToSingle(borderThickness.Top);
            rightThickness = Convert.ToSingle(borderThickness.Right);
            bottomThickness = Convert.ToSingle(borderThickness.Bottom);
            //Check whether or not to fill the rectangle
            if (fillBrush != null)
            {
                //Begin using the brush
                fillBrush.BeginUse(geometryDrawing);
                //Draw the rectangle
                geometryDrawing.Render();
                //End using the brush
                fillBrush.EndUse();
            }
            //Check whether or not we are expected to draw a border
            if (borderBrush != null)
            {
                //Begin using the border brush
                borderBrush.BeginUse(geometryDrawing);
                //Create the left border
                if (rectangle.Left > 0)
                {
                    GL.LineWidth(leftThickness);
                    GL.Begin(PrimitiveType.LineStrip);
                    GL.Vertex2(rectangle.Left, rectangle.Bottom);
                    GL.Vertex2(rectangle.Left, rectangle.Top);
                    GL.End();
                }
                //Create the top border
                if (rectangle.Top > 0)
                {
                    GL.LineWidth(topThickness);
                    GL.Begin(PrimitiveType.LineStrip);
                    GL.Vertex2(rectangle.Left - (borderThickness.Left / 2), rectangle.Top);
                    GL.Vertex2(rectangle.Right + (borderThickness.Right / 2), rectangle.Top);
                    GL.End();
                }
                //Create the right border
                if (rectangle.Right > 0)
                {
                    GL.LineWidth(rightThickness);
                    GL.Begin(PrimitiveType.LineStrip);
                    GL.Vertex2(rectangle.Right, rectangle.Top);
                    GL.Vertex2(rectangle.Right, rectangle.Bottom);
                    GL.End();
                }
                //Create the bottom line
                if (rectangle.Bottom > 0)
                {
                    GL.LineWidth(bottomThickness);
                    GL.Begin(PrimitiveType.LineStrip);
                    GL.Vertex2(rectangle.Right + (borderThickness.Right / 2), rectangle.Bottom);
                    GL.Vertex2(rectangle.Left - (borderThickness.Left / 2), rectangle.Bottom);
                    GL.End();
                }
                //End using the border brush
                borderBrush.EndUse();
            }
            geometryDrawing.Dispose();
        }

        /// <summary>
        /// Draws the specified text
        /// </summary>
        /// <param name="text">The text to draw</param>
        /// <param name="position">The position at which to start drawing the text</param>
        /// <param name="font">The <see cref="System.Drawing.Font"/> of the text to draw</param>
        /// <param name="brush">The <see cref="Media.Brush"/> with which to draw the text</param>
        public void DrawText(string text, Media.Point position, Font font, Media.Brush brush)
        {
            Color color;
            Bitmap bitmap;
            Graphics graphics;
            PointF textLocation;
            OpenTK.Graphics.TextExtents textExtents;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            if (!typeof(Media.SolidColorBrush).IsAssignableFrom(brush.GetType()))
            {
                throw new NotSupportedException("Only the SolidColorBrush type is currently supported as brush argument of the DrawingContext's 'DrawText' method");
            }
            color = ((Media.SolidColorBrush)brush).Color;
            bitmap = new Bitmap(10, 10);
            graphics = Graphics.FromImage(bitmap);
            textLocation = position.ToPointF();
            textExtents = DrawingContext.MeasureTextExtents(text, font);
            DrawingContext.TextPrinter.Begin();
            DrawingContext.TextPrinter.Print(text, font, color, new RectangleF(position.ToPointF().X, position.ToPointF().Y, textExtents.BoundingBox.Width * 1.5f, textExtents.BoundingBox.Height * 1.5f), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Near);
            DrawingContext.TextPrinter.End();
        }

        /// <summary>
        /// Ends a rendering pass
        /// </summary>
        public void EndRenderPass()
        {
            //Swap the window's buffers
            this.Window.Hwnd.SwapBuffers();
        }

        /// <summary>
        /// Measures the size of the specified text
        /// </summary>
        /// <param name="text">The text to measure</param>
        /// <param name="font">The font according to which to measure to text</param>
        /// <returns>The text's <see cref="Media.Size"/></returns>
        public static Media.Size MeasureText(string text, Font font)
        {
            OpenTK.Graphics.TextExtents textExtents;
            textExtents = DrawingContext.TextPrinter.Measure(text, font);
            return new Media.Size(textExtents.BoundingBox.Width, textExtents.BoundingBox.Height);
        }

        /// <summary>
        /// Measures the extents of the specified text
        /// </summary>
        /// <param name="text">The text to measure</param>
        /// <param name="font">The font according to which to measure to text</param>
        /// <returns>The text's <see cref="OpenTK.Graphics.TextExtents"/></returns>
        public static OpenTK.Graphics.TextExtents MeasureTextExtents(string text, Font font)
        {
            return DrawingContext.TextPrinter.Measure(text, font);
        }

    }

}
