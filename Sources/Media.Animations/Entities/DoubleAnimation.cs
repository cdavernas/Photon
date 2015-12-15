using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Represents a <see cref="double"/> <see cref="Animation{T}"/>
    /// </summary>
    public class DoubleAnimation
        : Animation<double>
    {

        /// <summary>
        /// The <see cref="double"/>'s difference between the values provided by the From and To properties
        /// </summary>
        public double Length
        {
            get
            {
                if (!this.To.HasValue || !this.To.HasValue)
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
            double normalizedTime, multiplier, value;
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
                value = this.To.Value - (multiplier * this.Length);
            }
            else
            {
                value = this.From.Value + (multiplier * this.Length);
            }
            this.TargetProperty.SetValue(this.Target, value);
        }

    }

}
