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
    /// Interaction logic for DeviceButtonAsAdapter.xaml
    /// </summary>
    public partial class DeviceButtonAsAdapter : Button,IDeviveButton
    {
        public int DevicePower { set; get; }
        public string Type { set; get; }
        public DeviceButtonAsAdapter()
        {
            InitializeComponent();
        }

        public DeviceButtonAsAdapter Clone()
        {
            DeviceButtonAsAdapter adp = new DeviceButtonAsAdapter();
            adp.Code.Content = this.Code.Content;
            adp.Tag1.Content = this.Tag1.Content;
            adp.Tag2.Content = this.Tag2.Content;
            adp.Tag3.Content = this.Tag3.Content;
            adp.Tag4.Content = this.Tag4.Content;
            adp.Tag5.Content = this.Tag5.Content;
            adp.DevicePower = this.DevicePower;
            adp.Power.Content = this.Power.Content;
            return adp;
        }
    }
}
