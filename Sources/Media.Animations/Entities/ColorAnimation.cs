using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Media.Animations
{

    /// <summary>
    /// Represents a <see cref="Color"/> <see cref="Animation{T}"/>
    /// </summary>
    public class ColorAnimation
        : Animation<Color>
    {

        /// <summary>
        /// The <see cref="ColorAnimation"/>'s alpha channel difference between the values provided by the From and To properties
        /// </summary>
        public int AlphaLength
        {
            get
            {
                if(!this.To.HasValue || !this.To.HasValue)
                {
                    return 0;
                }
                return this.To.Value.A - this.From.Value.A;
            }
        }

        /// <summary>
        /// The <see cref="ColorAnimation"/>'s red channel difference between the values provided by the From and To properties
        /// </summary>
        public int RedLength
        {
            get
            {
                if (!this.To.HasValue || !this.To.HasValue)
                {
                    return 0;
                }
                return this.To.Value.R - this.From.Value.R;
            }
        }

        /// <summary>
        /// The <see cref="ColorAnimation"/>'s green channel difference between the values provided by the From and To properties
        /// </summary>
        public int GreenLength
        {
            get
            {
                if (!this.To.HasValue || !this.To.HasValue)
                {
                    return 0;
                }
                return this.To.Value.G - this.From.Value.G;
            }
        }

        /// <summary>
        /// The <see cref="ColorAnimation"/>'s blue channel difference between the values provided by the From and To properties
        /// </summary>
        public int BlueLength
        {
            get
            {
                if (!this.To.HasValue || !this.To.HasValue)
                {
                    return 0;
                }
                return this.To.Value.B - this.From.Value.B;
            }
        }

        /// <summary>
        /// Allows the execution of code whenever the animation renders
        /// </summary>
        protected override void OnRender()
        {
            double normalizedTime, multiplier;
            int alpha, red, green, blue;
            Color color;
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
                alpha = (byte)(this.To.Value.A - (multiplier * this.AlphaLength));
                red = (byte)(this.To.Value.R - (multiplier * this.RedLength));
                green = (byte)(this.To.Value.G - (multiplier * this.GreenLength));
                blue = (byte)(this.To.Value.B - (multiplier * this.BlueLength));
            }
            else
            {
                alpha = (byte)(this.From.Value.A + (multiplier * this.AlphaLength));
                red = (byte)(this.From.Value.R + (multiplier * this.RedLength));
                green = (byte)(this.From.Value.G + (multiplier * this.GreenLength));
                blue = (byte)(this.From.Value.B + (multiplier * this.BlueLength));
            }
            color = Color.FromArgb(alpha, red, green, blue);
            this.TargetProperty.SetValue(this.Target, color);
        }

    }

}
