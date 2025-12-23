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

namespace Rekensommen
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            eventCalendar.Visibility = Visibility.Hidden;

        }

        private void Range(object sender, TextChangedEventArgs e)
        {
            // Eerst alles resetten
            van1.ClearValue(TextBox.BackgroundProperty);
            van2.ClearValue(TextBox.BackgroundProperty);
            tot1.ClearValue(TextBox.BackgroundProperty);
            tot2.ClearValue(TextBox.BackgroundProperty);

            ValidateBox(van1);
            ValidateBox(van2);
            ValidateBox(tot1);
            ValidateBox(tot2);

        }

        private void ValidateBox(TextBox box)
        {
            var text = box.Text;

            // Leeg = geen fout
            if (string.IsNullOrWhiteSpace(text))
            {
                box.Background = Brushes.White;
                return;
            }

            // Geen geldig getal
            if (!int.TryParse(text, out int value))
            {
                box.Background = Brushes.Red;
                return;
            }

            // Buiten bereik
            if (value < 0 || value > 100)
            {
                box.Background = Brushes.Red;
                return;
            }

            // Geldig
            box.Background = Brushes.White;

        }

        private void Optellen(object sender, RoutedEventArgs e)
        {
            if(optellen.IsChecked == true && aftrekken.IsChecked == false)
            {
                operatorLabel.Content = "+";
            }
            
        }

        private void Aftrekken(object sender, RoutedEventArgs e)
        {
            if (optellen.IsChecked == false && aftrekken.IsChecked == true)
            {
                operatorLabel.Content = "-";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selected = eventCalendar.SelectedDate;
            eventCalendar.DisplayDate = DateTime.Today;


            MessageBox.Show($"{DateTime.Today}");

        }

        private void equalsClick(object sender, RoutedEventArgs e)
        {
            int num1 = int.Parse(van1.Text);
            int num2 = int.Parse(van2.Text);
            int num3 = int.Parse(tot1.Text);
            int num4 = int.Parse(tot2.Text);


            Random rnd = new Random();
            int Num1 = rnd.Next(num1, num3);  
            int Num2 = rnd.Next(num2, num4);   



            firstNumberLabel.Content = Num1.ToString();
            secondNumberLabel.Content = Num2.ToString();

        }
    }
}