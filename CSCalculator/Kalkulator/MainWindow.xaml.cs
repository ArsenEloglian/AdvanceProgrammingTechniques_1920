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
        double liczba1 = 0;
        double liczba2 = 0;
		String znak = "";
        Boolean czyZnak = false;
        double wynik = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "1";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "1";
            }
        }

        private void button2_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "2";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "2";
            }
        }

        private void button3_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "3";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "3";
            }
        }
		
		private void button4_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "4";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "4";
            }
        }

        private void button5_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "5";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "5";
            }
        }

		private void button6_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "6";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "6";
            }
        }
        private void button7_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "7";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "7";
            }
        }
        private void button8_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "8";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "8";
            }
        }
        private void button9_onClick(object sender, RoutedEventArgs e)
        {
            if (czyZnak == true)
            {
                tbOperation.Text = "9";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "9";
            }
        }
        private void button0_onClick(object sender, RoutedEventArgs e)
        {
            if(czyZnak == true)
            {
                tbOperation.Text = "0";
                czyZnak = false;
                disableAll();
            }
            else
            {
                tbOperation.Text += "0";
            }      
        }

        private void buttonPlus_onClick(object sender, RoutedEventArgs e)
        {
            Double.TryParse(tbOperation.Text, out liczba1);
            tbOperation.Text = "+";
			znak = "+";
            czyZnak = true;
            
        }
        private void buttonMinus_onClick(object sender, RoutedEventArgs e)
        {
            Double.TryParse(tbOperation.Text, out liczba1);
            tbOperation.Text = "-";
			znak = "-";
            czyZnak = true;
            
        }
        private void buttonMnozenie_onClick(object sender, RoutedEventArgs e)
        {
            Double.TryParse(tbOperation.Text, out liczba1);
            tbOperation.Text = "*";
			znak = "*";
            czyZnak = true;
            
        }
        private void buttonDivide_onClick(object sender, RoutedEventArgs e)
        {
            Double.TryParse(tbOperation.Text, out liczba1);
            tbOperation.Text = "/";
			znak = "/";
            czyZnak = true;
            
        }

        private void buttonPrzecinek_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void buttonPlusMinus_onClick(object sender, RoutedEventArgs e)
        {
            if(znak == "")
            {
                Double.TryParse(tbOperation.Text, out liczba1);
                liczba1 = liczba1 * -1;
                tbOperation.Text = liczba1.ToString();
            }
            else
            {
                Double.TryParse(tbOperation.Text, out liczba2);
                liczba2 = liczba2 * -1;
                tbOperation.Text = liczba2.ToString();
            }
        }

        private void buttonWynik_onClick(object sender, RoutedEventArgs e)
        {
			if(znak == "+")
			{
				Double.TryParse(tbOperation.Text, out liczba2);
                wynik = liczba1 + liczba2;
                tbOperation.Text = wynik.ToString();
            }
			else if(znak == "-")
			{
				Double.TryParse(tbOperation.Text, out liczba2);
                wynik = liczba1 - liczba2;
                tbOperation.Text = wynik.ToString();
            }
			else if(znak == "*")
			{
				Double.TryParse(tbOperation.Text, out liczba2);
                wynik = liczba1 * liczba2;
                tbOperation.Text = wynik.ToString();
            }
			else if(znak == "/")
			{
				Double.TryParse(tbOperation.Text, out liczba2);
                wynik = liczba1 / liczba2;
                tbOperation.Text = wynik.ToString();
            }
            znak = "";
            disableAll();
        }

        private void disableAll()
        {
            buttonPlus.IsEnabled = !buttonPlus.IsEnabled;
            buttonMinus.IsEnabled = !buttonMinus.IsEnabled;
            buttonMnozenie.IsEnabled = !buttonMnozenie.IsEnabled;
            buttonDivide.IsEnabled = !buttonDivide.IsEnabled;
        }
    }
}
