using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Effects
{

    /// <summary>
    /// Wrapping an OpenGL (GLSL) shader, it is used to calculate rendering effects on graphics hardware with a high degree of flexibility
    /// </summary>
    public class Shader
        : IDisposable
    {

        /// <summary>
        /// Initializes a new <see cref="Shader"/> based on the specified GLSL file/resource <see cref="Uri"/>
        /// </summary>
        /// <param name="type">The <see cref="Shader"/>'s <see cref="ShaderType"/></param>
        /// <param name="glslUri">The file/resource <see cref="Uri"/> of the GLSL code to compile in order to generate the <see cref="Shader"/></param>
        public Shader(ShaderType type, Uri glslUri)
        {
            this.Type = type;
            using(Stream resourceStream = ResourceManager.GetResourceStream(glslUri))
            {
                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    this.GlslCode = reader.ReadToEnd();
                }
            }
            this.Compile();
        }

        /// <summary>
        /// Initializes a new <see cref="Shader"/> based on the specified GLSL file path
        /// </summary>
        /// <param name="type">The <see cref="Shader"/>'s <see cref="ShaderType"/></param>
        /// <param name="glslStream">The <see cref="Stream"/> containing the GLSL code to compile in order to generate the <see cref="Shader"/></param>
        public Shader(ShaderType type, Stream glslStream)
        {
            this.Type = type;
            using (StreamReader reader = new StreamReader(glslStream))
            {
                this.GlslCode = reader.ReadToEnd();
            }
            this.Compile();
        }

        /// <summary>
        /// Gets the <see cref="Shader"/>'s <see cref="ShaderType"/>
        /// </summary>
        public ShaderType Type { get;  private set; }

        /// <summary>
        /// Gets an integer representing the Id of the <see cref="Shader"/>
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets a string representing the <see cref="Shader"/>'s uncompiled GLSL code
        /// </summary>
        public string GlslCode { get; private set; }

        /// <summary>
        /// Compiles the <see cref="Shader.GlslCode"/> into an OpenGL shader
        /// </summary>
        private void Compile()
        {
            int status;
            string shaderInfoLog;
            this.Id = GL.CreateShader(this.Type);
            GL.ShaderSource(this.Id, this.GlslCode);
            GL.CompileShader(this.Id);
            GL.GetShader(this.Id, ShaderParameter.CompileStatus, out status);
            GL.GetShaderInfoLog(this.Id, out shaderInfoLog);
            if (status == 0)
            {
                GL.DeleteShader(this.Id);
                this.Id = 0;
                throw new Exception("An error occured while compiling the specified GLSL source code" + Environment.NewLine + "Shader info log:" + shaderInfoLog);
            }
        }

        /// <summary>
        /// Disposes of the <see cref="Shader"/> and of all its resources
        /// </summary>
        public void Dispose()
        {
            if(this.Id == 0)
            {
                return;
            }
            GL.DeleteShader(this.Id);
        }

    }

}
