using BallisticsSandbox.Objects;
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
    public partial class MainWindow : UserControl, ISwitchable
    {
        public double kineticEnergy;
        public double momentum;
        public double penetration;
        public double drag;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// <para/> Will switch the screen to whatever is passed in.
        /// <para/>Input: state - unused.
        /// <para/>Output: none
        /// <para/>Author: Connor Goudie
        /// <para/>Date: March 30, 2017
        /// </summary>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateKineticEnergy(Double.Parse(Weight.Text), Double.Parse(Velocity.Text));
            CalculateMomentum(Double.Parse(Weight.Text), Double.Parse(Velocity.Text));
            CalculatePenetration(kineticEnergy, Double.Parse(Diameter.Text));
            CalculateDrag(Double.Parse(AirDensity.Text), 
                Double.Parse(DragCoefficient.Text), 
                (Double.Parse(Diameter.Text) / 2) * Math.PI, 
                Double.Parse(Velocity.Text));

            UserControl output = new Output(kineticEnergy, momentum, penetration, drag);

            Switcher.Switch(output);
        }

        public void CalculateKineticEnergy(double mass, double velocity)
        {
            kineticEnergy = 0.5 * mass * Math.Pow(velocity, 2);
        }

        public void CalculateMomentum(double mass, double velocity)
        {
            momentum = mass * velocity;
        }

        public void CalculatePenetration(double kineticEnergy, double diameter)
        {
            penetration = kineticEnergy / diameter;
        }

        public void CalculateDrop(double gravity)
        {
            drag = gravity;
        }

        public void CalculateDrag(double airDensity, double dragCoefficient, double area, double velocity)
        {
            drag = (airDensity * dragCoefficient * area / 2) * Math.Pow(velocity, 2);
        }
    }
}
