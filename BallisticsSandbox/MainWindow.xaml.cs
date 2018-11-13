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

namespace BallisticsSandbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateKineticEnergy();
            CalculateMomentum();

            this.Content = new Output();
        }

        private double CalculateKineticEnergy()
        {
            return 0.5 * Double.Parse(Weight.Text) * Math.Pow(Double.Parse(Velocity.Text), 2);
        }

        private double CalculateMomentum()
        {
            return Double.Parse(Weight.Text) * Double.Parse(Velocity.Text);
        }
    }
}
