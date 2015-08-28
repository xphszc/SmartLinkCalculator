using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartLinkCalculator
{
    /// <summary>
    /// Interaction logic for DeviceButtonAsIO.xaml
    /// </summary>
    public partial class DeviceButtonAsIO : Button,IDeviveButton
    {
        public int DevicePower { set; get; }
        
        public DeviceButtonAsIO()
        {
            InitializeComponent();
        }

        public DeviceButtonAsIO Clone()
        {
            DeviceButtonAsIO IO = new DeviceButtonAsIO();
            IO.Code.Content = this.Code.Content;
            IO.Tag1.Content = this.Tag1.Content;
            IO.Tag2.Content = this.Tag2.Content;
            IO.Tag3.Content = this.Tag3.Content;
            IO.Tag4.Content = this.Tag4.Content;
            IO.Tag5.Content = this.Tag5.Content;
            IO.DevicePower = this.DevicePower;
            IO.Power.Content = this.Power.Content;
            return IO;
        }
    }
}
