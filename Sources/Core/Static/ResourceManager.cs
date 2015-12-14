using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Photon
{

    /// <summary>
    /// This class defines methods to manipulate resources
    /// </summary>
    public static class ResourceManager
    {

        /// <summary>
        /// Gets the <see cref="Stream"/> of the specified resource
        /// </summary>
        /// <param name="resourceUri">The resource's <see cref="Uri"/></param>
        /// <returns>The specified resource's <see cref="Stream"/></returns>
        public static Stream GetResourceStream(Uri resourceUri)
        {
            Stream stream;
            string path, assemblyName, resourceName;
            string[] temp;
            Assembly assembly;
            if (resourceUri.IsAbsoluteUri)
            {
                if (resourceUri.IsFile)
                {
                    path = resourceUri.OriginalString;
                    if (!File.Exists(path))
                    {
                        throw new FileNotFoundException("The file '" + path + "' does not exist and/or cannot be found");
                    }
                    return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                throw new Exception("'" + resourceUri.ToString() + "' is not a valid resource uri");
            }
            else
            {
                if (resourceUri.ToString().Contains(";component/"))
                {
                    temp = resourceUri.ToString().Replace("component/", "").Split(';');
                    assemblyName = temp[0].Replace("/", "").Replace(@"\", "");
                    resourceName = assemblyName + "." + temp[1].Replace("/", "").Replace(@"\", "");
                    assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == assemblyName);
                    if (assembly == null)
                    {
                        throw new Exception("The assembly '" + assemblyName + "' does not exist and/or cannot be found");
                    }
                    stream = assembly.GetManifestResourceStream(resourceName);
                    if (stream == null)
                    {
                        throw new Exception("The resource '" + resourceName + "' does not exist and/or cannot be found in assembly '" + assembly.GetName().Name);
                    }
                    return stream;
                }
                else
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + @"\" + resourceUri.OriginalString;
                    path = path.Replace("/", @"\").Replace(@"\\", @"\");
                    if (!File.Exists(path))
                    {
                        assembly = Application.Current.GetType().Assembly;
                        resourceName = assembly.GetName().Name + "." + resourceUri.OriginalString;
                        if (resourceName.StartsWith("/"))
                        {
                            resourceName = resourceName.Substring(1);
                        }
                        stream = assembly.GetManifestResourceStream(resourceName);
                        if (stream == null)
                        {
                            throw new Exception("The resource '" + resourceName + "' does not exist and/or cannot be found");
                        }
                        return stream;
                    }
                    return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
            }
        }

    }

}
