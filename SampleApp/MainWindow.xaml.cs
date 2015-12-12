using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon;
using Photon.Controls;

namespace SampleApp
{

    public class MainWindow
        : Photon.Window
    {

        public Photon.Media.VertexBufferObject<double> Buffer { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Loaded += this.OnLoaded;
        }

        protected void OnLoaded(object sender, EventArgs e)
        {
            this.Buffer = new Photon.Media.VertexBufferObject<double>(3, 3);
            this.Buffer.PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType.TriangleStrip;
            this.Buffer.SetIndices(new ushort[] { 0, 1, 2 });
            this.Buffer.SetVertices(new Photon.Media.Vertex<double>[] { new Photon.Media.Vertex<double>(0, 0), new Photon.Media.Vertex<double>(250, 0), new Photon.Media.Vertex<double>(250, 250) });
        }

        private Photon.Media.Vertex<double> FirstPoint;
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.FirstPoint = new Photon.Media.Vertex<double>(this.FirstPoint.X + 0.5, this.FirstPoint.Y + 0.5);
            this.Buffer.SetVertex(0, this.FirstPoint);
            //this.Buffer.Render();
        }

        private void Window_MouseLeftButtonDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            this.Background = new Photon.Media.SolidColorBrush(System.Drawing.Color.DeepPink);
        }

        private void Border_MouseEnter(object sender, OpenTK.Input.MouseEventArgs e)
        {
            ((Border)sender).Background = new Photon.Media.SolidColorBrush(System.Drawing.Color.Indigo);
        }

        private void Border_MouseLeave(object sender, OpenTK.Input.MouseEventArgs e)
        {
            ((Border)sender).Background = new Photon.Media.SolidColorBrush(System.Drawing.Color.Black);
        }

    }

}
