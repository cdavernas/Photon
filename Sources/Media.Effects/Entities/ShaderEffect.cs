using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Effects
{

    /// <summary>
    /// Represents an <see cref="Effect"/> generated thanks to a <see cref="ShaderProgram"/>
    /// </summary>
    public class ShaderEffect
        : Effect
    {

        /// <summary>
        /// Initializes a new <see cref="ShaderEffect"/>
        /// </summary>
        public ShaderEffect()
        {

        }

        /// <summary>
        /// Gets the <see cref="Effects.ShaderProgram"/> associated 
        /// </summary>
        public ShaderProgram ShaderProgram { get; protected set; }

        /// <summary>
        /// Begins using the <see cref="ShaderEffect"/>. Must be followed by a call to the <see cref="ShaderEffect.EndUse"/> method
        /// </summary>
        public override void BeginUse()
        {
            this.ShaderProgram.BeginUse();
        }

        /// <summary>
        /// Ends using the <see cref="ShaderEffect"/>
        /// </summary>
        public override void EndUse()
        {
            this.ShaderProgram.EndUse();
        }

    }

}
