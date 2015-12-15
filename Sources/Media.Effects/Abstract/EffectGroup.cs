using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Effects
{

    /// <summary>
    /// Represents a group of <see cref="Effect"/>s that can be applied at render time to a <see cref="Visual"/>
    /// </summary>
    public sealed class EffectGroup
        : Effect
    {

        /// <summary>
        /// Initializes a new <see cref="EffectGroup"/>
        /// </summary>
        public EffectGroup()
        {
            this.Effects = new EffectCollection();
        }

        /// <summary>
        /// Gets a collection of the <see cref="Effect"/>s contained by the <see cref="EffectGroup"/>
        /// </summary>
        public EffectCollection Effects { get; private set; }

        /// <summary>
        /// Begins using the <see cref="EffectGroup"/>
        /// </summary>
        public override void BeginUse()
        {
            foreach(Effect effect in this.Effects)
            {
                effect.BeginUse();
            }
        }

        /// <summary>
        /// Ends using the <see cref="EffectGroup"/>
        /// </summary>
        public override void EndUse()
        {
            foreach (Effect effect in this.Effects)
            {
                effect.EndUse();
            }
        }

        /// <summary>
        /// Allows the execution of code whenever the <see cref="EffectGroup"/> has been loaded
        /// </summary>
        protected override void OnLoaded()
        {
            base.OnLoaded();
            foreach(Effect effect in this.Effects)
            {
                effect.Load();
            }
        }

    }

}
