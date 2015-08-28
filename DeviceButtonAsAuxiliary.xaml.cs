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
    /// Interaction logic for DeviceButtonAsAuxiliary.xaml
    /// </summary>
    public partial class DeviceButtonAsAuxiliary : Button,IDeviveButton
    {
        public int DevicePower { set; get; }
        public DeviceButtonAsAuxiliary()
        {
            InitializeComponent();
        }

        public DeviceButtonAsAuxiliary Clone()
        {
            DeviceButtonAsAuxiliary aux = new DeviceButtonAsAuxiliary();
            aux.Code.Content = this.Code.Content;
            aux.Tag1.Content = this.Tag1.Content;
            aux.Tag2.Content = this.Tag2.Content;
            aux.Tag3.Content = this.Tag3.Content;
            aux.Tag4.Content = this.Tag4.Content;
            aux.Tag5.Content = this.Tag5.Content;
            aux.DevicePower = this.DevicePower;
            aux.Power.Content = this.Power.Content;
            return aux;
        }
    }
}
