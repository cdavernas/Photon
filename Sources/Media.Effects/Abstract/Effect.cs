using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Effects
{

    /// <summary>
    /// Represents the base class for all graphical effects that can be applied at render time to a <see cref="Visual"/>
    /// </summary>
    public abstract class Effect
        : DependencyObject, IDisposable
    {

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="Effect"/> has already been loaded
        /// </summary>
        public bool IsLoaded { get; protected set; }

        /// <summary>
        /// Loads the <see cref="Effect"/> if it hasn't already been done
        /// </summary>
        public void Load()
        {
            if (this.IsLoaded)
            {
                return;
            }
            this.IsLoaded = true;
            this.OnLoaded();
        }

        /// <summary>
        /// Begins using the <see cref="Effect"/>. Must be followed by a call to the <see cref="Effect.EndUse"/> method
        /// </summary>
        public abstract void BeginUse();

        /// <summary>
        /// Ends using the <see cref="Effect"/>
        /// </summary>
        public abstract void EndUse();

        /// <summary>
        /// When overriden in a class, allows the execution of code whenever the <see cref="Effect"/> has been loaded
        /// </summary>
        protected virtual void OnLoaded()
        {

        }

        /// <summary>
        /// Disposes of the <see cref="Effect"/> and all its resources
        /// </summary>
        public virtual void Dispose()
        {
           
        }

    }

}
