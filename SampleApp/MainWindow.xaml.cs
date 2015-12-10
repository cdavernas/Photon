using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{

    public class MainWindow
        : Photon.Window
    {

        private void Window_MouseLeftButtonDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            this.Background = new Photon.Media.SolidColorBrush(System.Drawing.Color.Pink);
        }

    }

}
