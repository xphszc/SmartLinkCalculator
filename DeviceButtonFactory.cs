using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLinkCalculator
{
    class DeviceButtonFactory
    {
        public string Code { set; get; }
        public string Tag1 { set; get; }
        public string Tag2 { set; get; }
        public string Tag3 { set; get; }
        public string Tag4 { set; get; }
        public string Tag5 { set; get; }
        public int Power { set; get; }

        public DeviceButtonAsAdapter AsAdapter()
        {
            DeviceButtonAsAdapter adp = new DeviceButtonAsAdapter();
            adp.Code.Content = this.Code;
            adp.Tag1.Content = this.Tag1;
            adp.Tag2.Content = this.Tag2;
            adp.Tag3.Content = this.Tag3;
            adp.Tag4.Content = this.Tag4;
            adp.Tag5.Content = this.Tag5;
            adp.DevicePower = this.Power;
            adp.Power.Content = this.Power.ToString();
            return adp;
        }

        public DeviceButtonAsIO AsIO()
        {
            DeviceButtonAsIO dev = new DeviceButtonAsIO();
            dev.Code.Content = this.Code;
            dev.Tag1.Content = this.Tag1;
            dev.Tag2.Content = this.Tag2;
            dev.Tag3.Content = this.Tag3;
            dev.Tag4.Content = this.Tag4;
            dev.Tag5.Content = this.Tag5;
            dev.DevicePower = this.Power;
            dev.Power.Content = this.Power.ToString();
            return dev;
        }

        public DeviceButtonAsFunction AsFun()
        {
            DeviceButtonAsFunction dev = new DeviceButtonAsFunction();
            dev.Code.Content = this.Code;
            dev.Tag1.Content = this.Tag1;
            dev.Tag2.Content = this.Tag2;
            dev.Tag3.Content = this.Tag3;
            dev.Tag4.Content = this.Tag4;
            dev.Tag5.Content = this.Tag5;
            dev.DevicePower = this.Power;
            dev.Power.Content = this.Power.ToString();
            return dev;
        }

        public DeviceButtonAsAuxiliary AsAux()
        {
            DeviceButtonAsAuxiliary dev = new DeviceButtonAsAuxiliary();
            dev.Code.Content = this.Code;
            dev.Tag1.Content = this.Tag1;
            dev.Tag2.Content = this.Tag2;
            dev.Tag3.Content = this.Tag3;
            dev.Tag4.Content = this.Tag4;
            dev.Tag5.Content = this.Tag5;
            dev.DevicePower = this.Power;
            dev.Power.Content = this.Power.ToString();
            return dev;
        }
    }
}
