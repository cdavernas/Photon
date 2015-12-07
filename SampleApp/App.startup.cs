using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{

    public static class Startup
    {

        public static void Main(string[] startupArguments)
        {
            Photon.Application.Start<App>(startupArguments);
        }

    }

}
