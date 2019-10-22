using System;
using System.Collections.Generic;
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

namespace Kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_onClick(object sender, RoutedEventArgs e)
        {
            tbOperation.Text += "1";
        }

        private void button2_onClick(object sender, RoutedEventArgs e)
        {
            tbOperation.Text += "2";
        }

        private void button3_onClick(object sender, RoutedEventArgs e)
        {
            tbOperation.Text += "3";
		}
		
		private void button4_onClick(object sender, RoutedEventArgs e)
        {
            tbOperation.Text += "4";
		}
    }
}
