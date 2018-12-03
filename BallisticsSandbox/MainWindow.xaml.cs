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

        // temp variables
        private double newVelocityX;
        private double newVelocityY;
        private double range;
        private double terminalVelocity;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(double velocity, double weight, double diameter, double gravity, double airDensity, double dragCoefficient, double angleOfFire, double time)
        {
            InitializeComponent();

            Velocity.Text = velocity.ToString();
            Weight.Text = weight.ToString();
            Diameter.Text = diameter.ToString();
            Gravity.Text = gravity.ToString();
            AirDensity.Text = airDensity.ToString();
            DragCoefficient.Text = dragCoefficient.ToString();
            Angle.Text = angleOfFire.ToString();
            Time.Text = time.ToString();
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

            CalculateTerminalVelocity(Double.Parse(Weight.Text), Double.Parse(Gravity.Text), Double.Parse(DragCoefficient.Text));

            CalculateVelocityChange(Double.Parse(Time.Text), Double.Parse(Velocity.Text), Double.Parse(Weight.Text), Double.Parse(Angle.Text), Double.Parse(Gravity.Text), Double.Parse(DragCoefficient.Text));

            UserControl output = new Output(kineticEnergy, momentum, penetration, drag, newVelocityX, newVelocityY);

            Switcher.Switch(output);
        }

        public void CalculateTerminalVelocity(double weight, double gravity, double dragCoefficient)
        {
            terminalVelocity = (weight * gravity / dragCoefficient);
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

        public void CalculateVelocityChange(double time, double velocity, double weight, double angle, double gravity, double dragCoefficient)
        {
            newVelocityX = velocity * Math.Cos(angle) * Math.Pow(Math.E, -gravity * time / terminalVelocity);
            newVelocityY = velocity * Math.Sin(angle) * Math.Pow(Math.E, -gravity * time / terminalVelocity) - terminalVelocity * (1 - Math.Pow(Math.E, -gravity * time / terminalVelocity));
        }

        public void CalculateRange(double weight, double dragCoefficient, double velocity, double gravity, double angle)
        {
            if (velocity * Math.Sin(angle) > terminalVelocity)
            {
                range = Math.Pow(velocity, 2) * Math.Sin(2 * angle) / gravity;
            }
            else if (velocity * Math.Sin(angle) < terminalVelocity)
            {
                range = velocity * terminalVelocity * Math.Cos(angle) / gravity;
            }
        }
    }
}
