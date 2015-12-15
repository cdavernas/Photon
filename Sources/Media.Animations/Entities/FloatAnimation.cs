using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Represents a <see cref="float"/> <see cref="Animation{T}"/>
    /// </summary>
    public class FloatAnimation
        : Animation<float>
    {

        /// <summary>
        /// The <see cref="float"/>'s difference between the values provided by the From and To properties
        /// </summary>
        public float Length
        {
            get
            {
                if(!this.From.HasValue || !this.To.HasValue)
                {
                    return 0;
                }
                return this.To.Value - this.From.Value;
            }
        }

        /// <summary>
        /// Allows the execution of code whenever the animation renders
        /// </summary>
        protected override void OnRender()
        {
            double normalizedTime, multiplier;
            float value;
            normalizedTime = this.GetNormalizedTime();
            if (this.Ease != null)
            {
                multiplier = this.Ease.EasingCore(normalizedTime);
            }
            else
            {
                multiplier = normalizedTime;
            }
            if (this.IsReverting)
            {
                value = (float)(this.To.Value - (multiplier * this.Length));
            }
            else
            {
                value = (float)(this.From.Value + (multiplier * this.Length));
            }
            this.TargetProperty.SetValue(this.Target, value);
        }

    }
}
