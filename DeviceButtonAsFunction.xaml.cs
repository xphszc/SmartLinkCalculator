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
    /// Interaction logic for DeviceButtonAsFunction.xaml
    /// </summary>
    public partial class DeviceButtonAsFunction : Button,IDeviveButton
    {
        public int DevicePower { set; get; }
        public DeviceButtonAsFunction()
        {
            InitializeComponent();
        }

        public DeviceButtonAsFunction Clone()
        {
            DeviceButtonAsFunction fun = new DeviceButtonAsFunction();
            fun.Code.Content = this.Code.Content;
            fun.Tag1.Content = this.Tag1.Content;
            fun.Tag2.Content = this.Tag2.Content;
            fun.Tag3.Content = this.Tag3.Content;
            fun.Tag4.Content = this.Tag4.Content;
            fun.Tag5.Content = this.Tag5.Content;
            fun.DevicePower = this.DevicePower;
            fun.Power.Content = this.Power.Content;
            return fun;
        }
    }
}
