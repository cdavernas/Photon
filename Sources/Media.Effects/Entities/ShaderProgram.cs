using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Effects
{

    /// <summary>
    /// A <see cref="ShaderProgram"/> is a wrapper for an OpenGL program object, which represents fully processed executable code, in the OpenGL Shading Language, for one or more Shader stages
    /// </summary>
    public class ShaderProgram
        : IDisposable
    {

        /// <summary>
        /// Initializes a new <see cref="ShaderProgram"/>
        /// </summary>
        public ShaderProgram()
        {
            this.Shaders = new ShaderCollection();
            this.Shaders.CollectionChanged += this.OnShadersChanged;
            this.LoadedUniforms = new Dictionary<string, int>();
            this.Load();
        }

        /// <summary>
        /// Gets an integer representing the <see cref="ShaderProgram"/>'s Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets a collection of the <see cref="Shader"/>s contained by the <see cref="ShaderProgram"/>
        /// </summary>
        public ShaderCollection Shaders { get; private set; }

        /// <summary>
        /// Gets a dictionary of KeyValuePairs made of both the loaded <see cref="Shader"/>'s uniforms and of the uniforms addresses
        /// </summary>
        public Dictionary<string, int> LoadedUniforms { get; private set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="ShaderProgram"/> is loaded
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Gets an integer representing the address of the specified <see cref="Shader"/>'s uniform
        /// </summary>
        /// <param name="uniformName">The name of the <see cref="Shader"/> uniform to retrieve the address of</param>
        /// <returns>An integer representing the address of the specified <see cref="Shader"/>'s uniform</returns>
        private int GetUniformAddress(string uniformName)
        {
            int address;
            if (this.LoadedUniforms.ContainsKey(uniformName))
            {
                return this.LoadedUniforms[uniformName];
            }
            GL.UseProgram(this.Id);
            address = GL.GetUniformLocation(this.Id, uniformName);
            GL.UseProgram(0);
            if (address != -1)
            {
                this.LoadedUniforms.Add(uniformName, address);
                return address;
            }
            throw new Exception("The specified uniform '" + uniformName + "' cannot be found in the ShaderProgram");
        }

        /// <summary>
        /// Gets the value of the specified <see cref="Shader"/>'s uniform
        /// </summary>
        /// <param name="uniformName">The name of the <see cref="Shader"/>'s uniform to retrieve</param>
        /// <param name="value">The float used to hold the returned value</param>
        public void GetUniform(string uniformName, out float value)
        {
            int address;
            address = this.GetUniformAddress(uniformName);
            GL.GetUniform(this.Id, address, out value);
        }

        /// <summary>
        /// Sets the specified uniform
        /// </summary>
        /// <param name="uniformName">The name of the <see cref="Shader"/> uniform to set</param>
        /// <param name="value">The value to set to the <see cref="Shader"/> uniform</param>
        public void SetUniform(string uniformName, float value)
        {
            int address;
            bool success;
            GL.UseProgram(this.Id);
            address = this.GetUniformAddress(uniformName);
            success = false;
            if (address != -1)
            {
                success = true;
                GL.Uniform1(address, value);
            }
            GL.UseProgram(0);
            if (!success)
            {
                throw new Exception("The specified uniform '" + uniformName + "' could not be found in the ShaderProgram");
            }
        }

        /// <summary>
        /// Sets the specified uniform
        /// </summary>
        /// <param name="uniformName">The name of the <see cref="Shader"/> uniform to set</param>
        /// <param name="value1">The second value to set to the <see cref="Shader"/> uniform</param>
        /// <param name="value2">The first value to set to the <see cref="Shader"/> uniform</param>
        public void SetUniform(string uniformName, float value1, float value2)
        {
            int address;
            bool success;
            GL.UseProgram(this.Id);
            address = this.GetUniformAddress(uniformName);
            success = false;
            if (address != -1)
            {
                success = true;
                GL.Uniform2(address, value1, value2);
            }
            GL.UseProgram(0);
            if (!success)
            {
                throw new Exception("The specified uniform '" + uniformName + "' could not be found in the ShaderProgram");
            }
        }

        /// <summary>
        /// Sets the specified uniform
        /// </summary>
        /// <param name="uniformName">The name of the <see cref="Shader"/> uniform to set</param>
        /// <param name="value1">The first value to set to the <see cref="Shader"/> uniform</param>
        /// <param name="value2">The second value to set to the <see cref="Shader"/> uniform</param>
        /// <param name="value3">The third value to set to the <see cref="Shader"/> uniform</param>
        public void SetUniform(string uniformName, float value1, float value2, float value3)
        {
            int address;
            bool success;
            GL.UseProgram(this.Id);
            address = this.GetUniformAddress(uniformName);
            success = false;
            if (address != -1)
            {
                success = true;
                GL.Uniform3(address, value1, value2, value3);
            }
            GL.UseProgram(0);
            if (!success)
            {
                throw new Exception("The specified uniform '" + uniformName + "' could not be found in the ShaderProgram");
            }
        }

        /// <summary>
        /// Sets the specified uniform
        /// </summary>
        /// <param name="uniformName">The name of the <see cref="Shader"/> uniform to set</param>
        /// <param name="value1">The first value to set to the <see cref="Shader"/> uniform</param>
        /// <param name="value2">The second value to set to the <see cref="Shader"/> uniform</param>
        /// <param name="value3">The third value to set to the <see cref="Shader"/> uniform</param>
        /// <param name="value4">The fourth value to set to the <see cref="Shader"/> uniform</param>
        public void SetUniform(string uniformName, float value1, float value2, float value3, float value4)
        {
            int address;
            bool success;
            GL.UseProgram(this.Id);
            address = this.GetUniformAddress(uniformName);
            success = false;
            if (address != -1)
            {
                success = true;
                GL.Uniform4(address, value1, value2, value3, value4);
            }
            GL.UseProgram(0);
            if (!success)
            {
                throw new Exception("The specified uniform '" + uniformName + "' could not be found in the ShaderProgram");
            }
        }

        /// <summary>
        /// Sets the specified uniform with the specified <see cref="System.Drawing.Color"/>
        /// </summary>
        /// <param name="uniformName">The name of the <see cref="Shader"/> uniform to set</param>
        /// <param name="color">The <see cref="System.Drawing.Color"/> value to set to the <see cref="Shader"/> uniform</param>
        public void SetUniform(string uniformName, System.Drawing.Color color)
        {
            this.SetUniform(uniformName, color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        /// <summary>
        /// Push the specified <see cref="AttribMask"/> on the shader
        /// </summary>
        /// <param name="attribute">The <see cref="AttribMask"/> to push</param>
        public void PushAttribute(AttribMask attribute)
        {
            GL.PushAttrib(attribute);
        }

        /// <summary>
        /// Pops the last pushed <see cref="AttribMask"/>
        /// </summary>
        public void PopAttribute()
        {
            GL.PopAttrib();
        }

        /// <summary>
        /// Loads the <see cref="ShaderProgram"/>
        /// </summary>
        private void Load()
        {
            this.Id = GL.CreateProgram();
            this.IsLoaded = true;
            foreach(Shader shader in this.Shaders)
            {
                this.Attach(shader);
            }
            this.Link();
        }

        /// <summary>
        /// Attaches the specified <see cref="Shader"/> to the <see cref="ShaderProgram"/>
        /// </summary>
        /// <param name="shader">The <see cref="Shader"/> to attach to the <see cref="ShaderProgram"/></param>
        private void Attach(Shader shader)
        {
            GL.AttachShader(this.Id, shader.Id);
        }

        /// <summary>
        /// Detaches the specified <see cref="Shader"/> from the <see cref="ShaderProgram"/>
        /// </summary>
        /// <param name="shader">The <see cref="Shader"/> to detach from the <see cref="ShaderProgram"/></param>
        private void Detach(Shader shader)
        {
            GL.DetachShader(this.Id, shader.Id);
        }

        /// <summary>
        /// Links the <see cref="ShaderProgram"/>
        /// </summary>
        private void Link()
        {
            string programInfoLog;
            int success;
            if (!this.IsLoaded
                || this.Shaders.Count < 1)
            {
                return;
            }
            GL.LinkProgram(this.Id);
            GL.GetProgramInfoLog(this.Id, out programInfoLog);
            GL.GetProgram(this.Id, GetProgramParameterName.LinkStatus, out success);
            if (success != 1)
            {
                GL.DeleteProgram(this.Id);
                this.Id = 0;
                throw new Exception("The ShaderProgram could not be linked");
            }
        }

        /// <summary>
        /// Begins using the <see cref="ShaderProgram"/>. Must be followed by a call to the <see cref="ShaderProgram.EndUse"/> method
        /// </summary>
        public void BeginUse()
        {
            if (this.Id <= 0)
            {
                throw new Exception("The ShaderProgram has not been linked");
            }
            GL.Enable(EnableCap.Texture2D);
            GL.UseProgram(this.Id);
        }

        /// <summary>
        /// Ends using the <see cref="ShaderProgram"/>
        /// </summary>
        public void EndUse()
        {
            GL.UseProgram(0);
            GL.Disable(EnableCap.Texture2D);
        }

        /// <summary>
        /// Handles the <see cref="ShaderProgram.Shaders"/>'s <see cref="ObservableHashSet{TElement}.CollectionChanged"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> associated with the event</param>
        private void OnShadersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Shader shader;
            IEnumerable<Shader> shaders;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    shader = (Shader)e.NewItems[0];
                    this.Attach(shader);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    shader = (Shader)e.NewItems[0];
                    GL.DetachShader(this.Id, shader.Id);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    shaders = (IEnumerable<Shader>)e.OldItems;
                    foreach(Shader shaderToDetach in shaders)
                    {
                        this.Detach(shaderToDetach);
                    }
                    break;
            }
            if (this.IsLoaded)
            {
                this.Link();
            }
        }

        /// <summary>
        /// Disposes of the <see cref="ShaderProgram"/> and all its resources
        /// </summary>
        public void Dispose()
        {
            foreach(Shader shader in this.Shaders)
            {
                shader.Dispose();
            }
        }

    }

}
