using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Effects
{

    /// <summary>
    /// Represents an <see cref="Effect"/> that taints a <see cref="Visual"/> with the specified <see cref="Color"/>
    /// </summary>
    public class ColorizeEffect
        : ShaderEffect
    {

        /// <summary>
        /// Gets the name of the color <see cref="Shader"/>'s uniform
        /// </summary>
        private const string UNIFORM_COLOR = "color";
        /// <summary>
        /// Gets the name of the intensity multiplier <see cref="Shader"/>'s uniform
        /// </summary>
        private const string UNIFORM_INTENSITY = "intensity";
        /// <summary>
        /// Gets the <see cref="ColorizeEffect"/> GLSL source file/resource path
        /// </summary>
        private const string FRAGSHADER_GLSLFILE_PATH = "/Photon;component/Resources/Shaders/Colorize/colorize.frag";

        /// <summary>
        /// Describes the <see cref="ColorizeEffect.Color"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(ColorizeEffect));
        /// <summary>
        /// Gets/sets the <see cref="Color"/> used to shade a <see cref="Visual"/>
        /// </summary>
        public Color Color
        {
            get
            {
                return this.GetValue<Color>(ColorizeEffect.ColorProperty);
            }
            set
            {
                this.SetValue(ColorizeEffect.ColorProperty, value);
            }
        }

        /// <summary>
        /// Describes the <see cref="ColorizeEffect.Intensity"/>
        /// </summary>
        public static DependencyProperty IntensityProperty = DependencyProperty.Register("Intensity", typeof(ColorizeEffect));
        /// <summary>
        /// Gets/sets a double (ranging from 0.0 to 1.0) representing the <see cref="ColorizeEffect"/>'s intensity
        /// </summary>
        public double Intensity
        {
            get
            {
                return this.GetValue<double>(ColorizeEffect.IntensityProperty);
            }
            set
            {
                this.SetValue(ColorizeEffect.IntensityProperty, value);
            }
        }

        private float IntensityF
        {
            get
            {
                return Convert.ToSingle(this.Intensity);
            }
        }
        
        /// <summary>
        /// Allows the execution of code whenever the <see cref="Effect"/> has been loaded
        /// </summary>
        protected override void OnLoaded()
        {
            Stream glslStream;
            Shader shader;
            base.OnLoaded();
            this.ShaderProgram = new ShaderProgram();
            glslStream = ResourceManager.GetResourceStream(new Uri(ColorizeEffect.FRAGSHADER_GLSLFILE_PATH, UriKind.Relative));
            shader = new Shader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader, glslStream);
            this.ShaderProgram.Shaders.Add(shader);
            this.ShaderProgram.SetUniform(ColorizeEffect.UNIFORM_COLOR, this.Color);
            this.ShaderProgram.SetUniform(ColorizeEffect.UNIFORM_COLOR, this.Color);
            this.ShaderProgram.SetUniform(ColorizeEffect.UNIFORM_INTENSITY, this.IntensityF);
            this.ShaderProgram.SetUniform(ColorizeEffect.UNIFORM_INTENSITY, this.IntensityF);
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
            if (propertyName == ColorizeEffect.ColorProperty.Name)
            {
                if(!this.IsLoaded)
                {
                    return;
                }
                this.ShaderProgram.SetUniform(ColorizeEffect.UNIFORM_COLOR, (Color)value);
                return;
            }
            if (propertyName == ColorizeEffect.IntensityProperty.Name)
            {
                if (!this.IsLoaded)
                {
                    return;
                }
                this.ShaderProgram.SetUniform(ColorizeEffect.UNIFORM_INTENSITY, this.IntensityF);
                return;
            }
        }

        /// <summary>
        /// Disposes of the <see cref="ColorizeEffect"/>
        /// </summary>
        public override void Dispose()
        {
            if(this.ShaderProgram != null)
            {
                this.ShaderProgram.Dispose();
            }
            base.Dispose();
        }

    }

}
