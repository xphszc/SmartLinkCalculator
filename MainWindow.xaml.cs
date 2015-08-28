using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SmartLinkCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        static string fileName = "SmartLinkDevices.xml";
        
        public event PropertyChangedEventHandler PropertyChanged;

        private int surplusPower;
        public int SurplusPower
        {
            get
            {
                return surplusPower;
            }
            set
            {
                surplusPower = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SurplusPower"));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DeviceNumberBoxOfAdapter adpNum = new DeviceNumberBoxOfAdapter();
            numPanel.Children.Add(adpNum);

            for (int i = 0; i < 32; i++)
            {
                DeviceNumberBoxOfModule modNum = new DeviceNumberBoxOfModule();
                modNum.Content = string.Format("[ {0} ]", i + 1);
                numPanel.Children.Add(modNum);
            }
            


            this.label.SetBinding(Label.ContentProperty, new Binding("SurplusPower") { Source = this, Path = new PropertyPath("SurplusPower") });

            XElement root = XElement.Load(fileName);

            var devicesQuery =
                from device in root.Elements("Device")
                select device;

            foreach (var dev in devicesQuery)
            {
                DeviceButtonFactory btn = new DeviceButtonFactory();
                btn.Code = dev.Element("Code").Value.ToString();
                btn.Tag1 = dev.Element("Tags").Attribute("tag1").Value;
                btn.Tag2 = dev.Element("Tags").Attribute("tag2").Value;
                btn.Tag3 = dev.Element("Tags").Attribute("tag3").Value;
                btn.Tag4 = dev.Element("Tags").Attribute("tag4").Value;
                btn.Tag5 = dev.Element("Tags").Attribute("tag5").Value;
                btn.Power = Convert.ToInt32(dev.Element("OutCurrent").Value) - Convert.ToInt32(dev.Element("InCurrent").Value);


                switch (dev.Element("Type").Value)
                {
                    case "adapter":
                        DeviceButtonAsAdapter adp =  btn.AsAdapter();
                        adp.Margin = new Thickness(5, 0, 0, 0);
                        adp.Click += new RoutedEventHandler(add_adp_Click);
                        this.adapterPanel.Children.Add(adp);
                        break;
                    case "IO":
                        DeviceButtonAsIO IO = btn.AsIO();
                        IO.Margin = new Thickness(5, 0, 0, 0);
                        IO.Click += new RoutedEventHandler(add_IO_Click);
                        IO.MouseRightButtonUp += new  MouseButtonEventHandler(right_IO_Click);
                        this.IOPanel.Children.Add(IO);
                        break;
                    case "function":
                        DeviceButtonAsFunction fun = btn.AsFun();
                        fun.Margin = new Thickness(5, 0, 0, 0);
                        fun.Click += new RoutedEventHandler(add_fun_Click);
                        fun.MouseRightButtonUp += new MouseButtonEventHandler(right_fun_Click);
                        this.functionPanel.Children.Add(fun);
                        break;
                    case "auxiliary":
                        DeviceButtonAsAuxiliary aux = btn.AsAux();
                        aux.Margin = new Thickness(5, 0, 0, 0);
                        aux.Click += new RoutedEventHandler(add_aux_Click);
                        aux.MouseRightButtonUp += new MouseButtonEventHandler(right_aux_Click);
                        this.auxiliaryPanel.Children.Add(aux);
                        break;
                    default:
                        break;
                }
            }
        }

        /*
        private void add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(e.Source.ToString());
        }
        */

        private void add_adp_Click(object sender,RoutedEventArgs e)
        {
            if (IsAdapterOnFirst())
            {
                MessageBox.Show("只能有一个适配器");
                return;
            }
            DeviceButtonAsAdapter adp = (sender as DeviceButtonAsAdapter).Clone();
            devicesPanel.Children.Add(adp);
            SurplusPower += Convert.ToInt32(adp.Power.Content);
            PowerCheck();
        }

        private void add_IO_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdapterOnFirst())
            {
                if (devicesPanel.Children.Count > 32)
                {
                    MessageBox.Show("模块不能超过32个");
                    return;
                }
                DeviceButtonAsIO IO = (sender as DeviceButtonAsIO).Clone();
                IO.MouseRightButtonUp += new MouseButtonEventHandler(remove_single_Click);
                devicesPanel.Children.Add(IO);
                SurplusPower += Convert.ToInt32(IO.Power.Content);
                PowerCheck();
                return;
            }
            MessageBox.Show("请先装配适配器");
        }
        
        private void right_IO_Click(object sender,MouseButtonEventArgs e)
        {
            if (IsAdapterOnFirst())
            {
                if (devicesPanel.Children.Count > 32)
                {
                    MessageBox.Show("模块不能超过32个");
                    return;
                }
                DeviceDialog DD = new DeviceDialog(devicesPanel.Children.Count);
                DD.Title = string.Format("自定义添加 {0}", (sender as DeviceButtonAsIO).Code.Content.ToString());
                DD.ShowDialog();
                if (DD.pos != 0 && DD.cunt != 0)
                {
                    for (int i = 0; i < DD.cunt; i++)
                    {
                        DeviceButtonAsIO IO = (sender as DeviceButtonAsIO).Clone();
                        IO.MouseRightButtonUp += new MouseButtonEventHandler(remove_single_Click);
                        devicesPanel.Children.Insert(DD.pos+i, IO);
                        SurplusPower += Convert.ToInt32(IO.Power.Content);
                        PowerCheck();
                    }
                }
                return;
            }
            MessageBox.Show("请先装配适配器");
        }

        private void add_fun_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdapterOnFirst())
            {
                if (devicesPanel.Children.Count > 32)
                {
                    MessageBox.Show("模块不能超过32个");
                    return;
                }
                DeviceButtonAsFunction fun = (sender as DeviceButtonAsFunction).Clone();
                fun.MouseRightButtonUp += new MouseButtonEventHandler(remove_single_Click);
                devicesPanel.Children.Add(fun);
                SurplusPower += Convert.ToInt32(fun.Power.Content);
                PowerCheck();
                return;
            }
            MessageBox.Show("请先装配适配器");
        }

        private void right_fun_Click(object sender, MouseButtonEventArgs e)
        {
            if (IsAdapterOnFirst())
            {
                if (devicesPanel.Children.Count > 32)
                {
                    MessageBox.Show("模块不能超过32个");
                    return;
                }
                DeviceDialog DD = new DeviceDialog(devicesPanel.Children.Count);
                DD.Title = string.Format("自定义添加 {0}", (sender as DeviceButtonAsFunction).Code.Content.ToString());
                DD.ShowDialog();
                Console.WriteLine("{0},{1}", DD.pos, DD.cunt);
                if (DD.pos != 0 && DD.cunt != 0)
                {
                    for (int i = 0; i < DD.cunt; i++)
                    {
                        DeviceButtonAsFunction fun = (sender as DeviceButtonAsFunction).Clone();
                        fun.MouseRightButtonUp += new MouseButtonEventHandler(remove_single_Click);
                        devicesPanel.Children.Insert(DD.pos + i, fun);
                        SurplusPower += Convert.ToInt32(fun.Power.Content);
                        PowerCheck();
                    }
                }
                return;
            }
            MessageBox.Show("请先装配适配器");
        }

        private void add_aux_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdapterOnFirst())
            {
                if (devicesPanel.Children.Count > 32)
                {
                    MessageBox.Show("模块不能超过32个");
                    return;
                }
                DeviceButtonAsAuxiliary aux = (sender as DeviceButtonAsAuxiliary).Clone();
                aux.MouseRightButtonUp += new MouseButtonEventHandler(remove_single_Click);
                devicesPanel.Children.Add(aux);
                SurplusPower += Convert.ToInt32(aux.Power.Content);
                PowerCheck();
                return;
            }
            MessageBox.Show("请先装配适配器");
        }

        private void right_aux_Click(object sender, MouseButtonEventArgs e)
        {
            if (IsAdapterOnFirst())
            {
                if (devicesPanel.Children.Count > 32)
                {
                    MessageBox.Show("模块不能超过32个");
                    return;
                }
                DeviceDialog DD = new DeviceDialog(devicesPanel.Children.Count);
                DD.Title = string.Format("自定义添加 {0}", (sender as DeviceButtonAsAuxiliary).Code.Content.ToString());
                DD.ShowDialog();
                Console.WriteLine("{0},{1}", DD.pos, DD.cunt);
                if (DD.pos != 0 && DD.cunt != 0)
                {
                    for (int i = 0; i < DD.cunt; i++)
                    {
                        DeviceButtonAsAuxiliary aux = (sender as DeviceButtonAsAuxiliary).Clone();
                        aux.MouseRightButtonUp += new MouseButtonEventHandler(remove_single_Click);
                        devicesPanel.Children.Insert(DD.pos + i, aux);
                        SurplusPower += Convert.ToInt32(aux.Power.Content);
                        PowerCheck();
                    }
                }
                return;
            }
            MessageBox.Show("请先装配适配器");
        }

        private void clear_Click(object sender,RoutedEventArgs e)
        {
            devicesPanel.Children.Clear();
            SurplusPower = 0;
            PowerCheck();
        }

        private void remove_single_Click(object sender, MouseButtonEventArgs e)
        {
            SurplusPower -= (sender as IDeviveButton).DevicePower;
            devicesPanel.Children.RemoveAt(devicesPanel.Children.IndexOf(sender as UIElement));
            PowerCheck();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (devicesPanel.Children.Count>0)
            {
                SurplusPower -= (devicesPanel.Children[devicesPanel.Children.Count - 1] as IDeviveButton).DevicePower;
                devicesPanel.Children.RemoveAt(devicesPanel.Children.Count - 1);
                PowerCheck();
            }
        }

        public void PowerCheck()
        {
            if (SurplusPower > 0)
            {
                label.Foreground = Brushes.Green;
            }
            else
            {
                if (SurplusPower == 0)
                {
                    label.Foreground = Brushes.Black;
                }
                else
                {
                    label.Foreground = Brushes.Red;
                }
            }
        }

        public bool IsAdapterOnFirst()
        {
            if (devicesPanel.Children.Count != 0)
            {
                if ((devicesPanel.Children[0] as DeviceButtonAsAdapter).Tag1.Content.ToString() == "适配器")
                {
                    return true;
                }
            }
            return false;
        }

        private void clearM_Click(object sender, RoutedEventArgs e)
        {
            while (devicesPanel.Children.Count > 1)
            {
                SurplusPower -= (devicesPanel.Children[devicesPanel.Children.Count - 1] as IDeviveButton).DevicePower;
                devicesPanel.Children.RemoveAt(devicesPanel.Children.Count - 1);
                PowerCheck();
            }
        }
    }
}
