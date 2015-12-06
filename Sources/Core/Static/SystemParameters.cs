using Photon.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This static class defines methods to access system variables
    /// </summary>
    public static class SystemParameters
    {

        /// <summary>
        /// Gets the work area's <see cref="Size"/>
        /// </summary>
        public static Size WorkArea
        {
            get
            {
                ManagementScope scope;
                ObjectQuery query;
                ManagementObjectCollection results;
                int x, y;
                scope = new ManagementScope();
                scope.Connect();
                query = new ObjectQuery("SELECT * FROM Win32_VideoController");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    results = searcher.Get();
                    foreach (ManagementBaseObject result in results)
                    {
                        x = Convert.ToInt32(result.GetPropertyValue("CurrentHorizontalResolution"));
                        y = Convert.ToInt32(result.GetPropertyValue("CurrentVerticalResolution"));
                        return new Size(x, y);
                    }
                }
                return new Size();
            }
        }

    }

}
