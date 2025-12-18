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
    }
}