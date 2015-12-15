using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon;
using Photon.Controls;
using Photon.Media.Animations;
using Photon.Media.Effects;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace SampleApp
{

    public class MainWindow
        : Photon.Window
    {

        private Storyboard ColorAnimation;

        public MainWindow()
        {
            this.Loaded += this.OnLoaded;
        }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ColorAnimation colorAnim;
            this.ColorAnimation = Storyboard.Register(this);
            colorAnim = new ColorAnimation();
            colorAnim.From = Color.Blue;
            colorAnim.To = Color.Magenta;
            colorAnim.Duration = TimeSpan.FromSeconds(2);
            colorAnim.AutoReverse = true;
            colorAnim.RepeatBehavior = RepeatBehavior.Forever;
            this.ColorAnimation.Children.Add(colorAnim);
            Storyboard.SetTarget(colorAnim, this);
            Storyboard.SetTargetProperty(colorAnim, new PropertyPath(new DependencyProperty[] { Window.BackgroundProperty, Photon.Media.SolidColorBrush.ColorProperty }));
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            this.ColorAnimation.Begin();
        }

    }

}
