using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Input
{

    /// <summary>
    /// Provides the base class for several derived classes that represents the return value from a hit test
    /// </summary>
    public class HitTestResult
    {

        /// <summary>
        /// Initializes a new <see cref="HitTestResult"/> instance
        /// </summary>
        public HitTestResult()
        {
            
        }

        /// <summary>
        /// Initializes a new <see cref="HitTestResult"/> with the specified <see cref="IUIElement"/>
        /// </summary>
        /// <param name="elementHit">The <see cref="IUIElement"/> that has been hit during the test/></param>
        public HitTestResult(IUIElement elementHit)
        {
            this.ElementHit = elementHit;
        }

        /// <summary>
        /// Gets the <see cref="IUIElement"/> that has been hit during the test/>
        /// </summary>
        public IUIElement ElementHit { get; private set; }

        /// <summary>
        /// Gets a boolean indicating whether or not an <see cref="IUIElement"/> has been hit during the test
        /// </summary>
        public bool HasHit
        {
            get
            {
                if (this.ElementHit == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }

}
