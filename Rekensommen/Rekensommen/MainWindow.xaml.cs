using System.Diagnostics;
using System.Globalization;
using System.Reflection;
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
using System.Windows.Threading;

namespace Rekensommen
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            eventCalendar.Visibility = Visibility.Hidden;

            //timer
            _stopwatch = new Stopwatch();
            _uiTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            _uiTimer.Tick += UiTimer_Tick;



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
            if (optellen.IsChecked == true && aftrekken.IsChecked == false)
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


            MessageBox.Show($"{DateTime.Now.ToString()}");

        }


        //timer
        private readonly DispatcherTimer _uiTimer;
        private readonly Stopwatch _stopwatch;
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!_stopwatch.IsRunning)
                _stopwatch.Start();

            if (!_uiTimer.IsEnabled)
                _uiTimer.Start();
        }
        private void UiTimer_Tick(object sender, EventArgs e)
        {
            var elapsed = _stopwatch.Elapsed;
            timerLabel.Content = elapsed.ToString(@"mm\:ss\.fff", CultureInfo.InvariantCulture);
        }




        private void equalsClick(object sender, RoutedEventArgs e)
        {
            int min1 = int.Parse(van1.Text);
            int min2 = int.Parse(van2.Text);
            int max1 = int.Parse(tot1.Text);
            int max2 = int.Parse(tot2.Text);

            _num1 = _rnd.Next(min1, max1);
            _num2 = _rnd.Next(min2, max2);

            firstNumberLabel.Content = _num1.ToString();
            secondNumberLabel.Content = _num2.ToString();

            var op = operatorLabel?.Content?.ToString();
            var reText = resultTextBox?.Text ?? "";

            if (!_firstClickDone)
            {
                _stopwatch.Reset();                         // reset tijd
                timerLabel.Content = "00:00.000";           // optioneel: reset label
                Start_Click(sender, e);                     // start timer
                _firstClickDone = true;

            }
            else
            {
                // Bereken resultaat op basis van operator
                int expected;
                switch (op)
                {
                    case "+":
                        expected = _num1 + _num2;
                        break;
                    case "-":
                        expected = _num1 - _num2;
                        break;
                    default:
                        expected = _num1 + _num2; // fallback
                        break;
                }

                if (int.TryParse(reText, out var userVal))
                {
                    if (userVal == expected)
                    {
                        resultTextBox.Background = Brushes.Green;
                        if (_stopwatch.IsRunning)
                            _stopwatch.Stop();

                        if (_uiTimer.IsEnabled)
                            _uiTimer.Stop();


                    }
                    else
                    {
                        resultTextBox.Background = Brushes.Red;
                    }
                }
                
            }
        }


        private readonly Random _rnd = new Random();
        private int _num1;
        private int _num2;
        private bool _firstClickDone; // flag


        private void NegatieveJa(object sender, RoutedEventArgs e)
        {




        }
    }
}