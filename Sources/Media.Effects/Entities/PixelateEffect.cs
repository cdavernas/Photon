using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Effects
{

    /// <summary>
    /// This <see cref="ShaderEffect"/> pixelates a <see cref="Visual"/>
    /// </summary>
    public class PixelateEffect
        : ShaderEffect
    {

        /// <summary>
        /// Gets the name of the threshold <see cref="Shader"/>'s uniform
        /// </summary>
        private const string UNIFORM_PIXELTHRESHOLD = "threshold";
        /// <summary>
        /// Gets the <see cref="PixelateEffect"/>'s <see cref="Shader"/> GLSL file/resource path
        /// </summary>
        private const string SHADER_GLSLFILE_PATH = "/Photon;component/Resources/Shaders/Pixelate/pixelate.frag";
        
        /// <summary>
        /// Initializes a new <see cref="PixelateEffect"/>
        /// </summary>
        public PixelateEffect()
            : base()
        {

        }

        /// <summary>
        /// Describes the <see cref="PixelateEffect.PixelThreshold"/> <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty PixelThresholdProperty = DependencyProperty.Register("PixelThreshold", typeof(PixelateEffect));
        /// <summary>
        /// Gets/sets the <see cref="PixelateEffect"/>'s pixel threshold
        /// </summary>
        public float PixelThreshold
        {
            get
            {
                return this.GetValue<float>(PixelateEffect.PixelThresholdProperty);
            }
            set
            {
                this.SetValue(PixelateEffect.PixelThresholdProperty, value);
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
            glslStream = ResourceManager.GetResourceStream(new Uri(PixelateEffect.SHADER_GLSLFILE_PATH, UriKind.Relative));
            shader = new Shader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader, glslStream);
            this.ShaderProgram.Shaders.Add(shader);
            this.ShaderProgram.SetUniform(PixelateEffect.UNIFORM_PIXELTHRESHOLD, (float)this.PixelThreshold);
        }

        /// <summary>
        /// Provides means to run code whenever a <see cref="DependencyProperty"/> has changed
        /// </summary>
        /// <param name="propertyName">The case sensitive name of the modified property</param>
        /// <param name="originalValue">An object representing the property's value before the suffered change(s)</param>
        /// <param name="value">An object representing the property's actual (new) value</param>
        protected override void OnPropertyChanged(string propertyName, object originalValue, object value)
        {
            base.OnPropertyChanged(propertyName, originalValue, value);
            if(propertyName == PixelateEffect.PixelThresholdProperty.Name)
            {
                if (!this.IsLoaded)
                {
                    return;
                }
                this.ShaderProgram.SetUniform(PixelateEffect.UNIFORM_PIXELTHRESHOLD, (float)this.PixelThreshold);
                return;
            }
        }

    }

}
