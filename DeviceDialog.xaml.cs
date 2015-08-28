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
    /// Interaction logic for DeviceDialog.xaml
    /// </summary>
    public partial class DeviceDialog : Window
    {
        public int pos { get; set; }
        public int cunt { get; set; }

        public DeviceDialog()
        {
            InitializeComponent();
        }

        public DeviceDialog(int count)
        {
            InitializeComponent();

            for (int i = 0; i < count; i++)
            {
                ComboBoxItem pos = new ComboBoxItem();
                pos.Content = i + 1;
                comboOfPosition.Items.Add(pos);
            }
            for (int i = 0; i < 33-count; i++)
            {
                ComboBoxItem cout = new ComboBoxItem();
                cout.Content = i + 1;
                comboOfCount.Items.Add(cout);
            }
        }

        private void adds_Click(object sender, RoutedEventArgs e)
        {
            this.pos= this.comboOfPosition.SelectedIndex + 1;
            this.cunt = this.comboOfCount.SelectedIndex + 1;
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
