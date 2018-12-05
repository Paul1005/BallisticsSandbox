using BallisticsSandbox.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private double newVelocity;
        private double range;
        private double terminalVelocity;
        private double positionX;
        private double positionY;
        private double flightTime;
        private double area;

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
            CalculateArea(Double.Parse(Diameter.Text));
            CalculateKineticEnergy(Double.Parse(Weight.Text), Double.Parse(Velocity.Text));
            CalculateMomentum(Double.Parse(Weight.Text), Double.Parse(Velocity.Text));
            CalculatePenetration(kineticEnergy, Double.Parse(Diameter.Text));

            CalculateTerminalVelocity(Double.Parse(Weight.Text), Double.Parse(Gravity.Text), Double.Parse(DragCoefficient.Text), Double.Parse(AirDensity.Text));

            CalculatePosition(Double.Parse(Velocity.Text), Double.Parse(Angle.Text), Double.Parse(Gravity.Text), flightTime);

            CalculateTimeInFlight(Double.Parse(Velocity.Text), Double.Parse(Angle.Text), Double.Parse(Gravity.Text));

            CalculateVelocityAtTime(flightTime, Double.Parse(Velocity.Text), Double.Parse(Weight.Text), Double.Parse(Angle.Text), Double.Parse(Gravity.Text), Double.Parse(DragCoefficient.Text));

            CalculateRange(Double.Parse(Velocity.Text), Double.Parse(Gravity.Text), Double.Parse(Angle.Text));

            UserControl output = new Output(kineticEnergy, momentum, penetration, drag, newVelocity, positionX, positionY, flightTime, range, terminalVelocity);

            Switcher.Switch(output);
        }

        public void CalculateArea(double diameter)
        {
            area = Math.PI * Math.Pow(diameter / 1000 / 2, 2);
        }

        public void CalculateTerminalVelocity(double weight, double gravity, double dragCoefficient, double airDensity)
        {
            terminalVelocity = Math.Sqrt((2 * weight * gravity) / (airDensity * 1000 * area * dragCoefficient));
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

        public void CalculateVelocityAtTime(double time, double velocity, double weight, double angle, double gravity, double dragCoefficient)
        {
            double newVelocityX = velocity * Math.Cos(angle) * Math.Pow(Math.E, -gravity * time / terminalVelocity);
            double newVelocityY = velocity * Math.Sin(angle) * Math.Pow(Math.E, -gravity * time / terminalVelocity) - terminalVelocity * (1 - Math.Pow(Math.E, -gravity * time / terminalVelocity));

            newVelocity = Math.Sqrt(Math.Pow(newVelocityX, 2) + Math.Pow(newVelocityY, 2));
        }

        public void CalculateRange(double velocity, double gravity, double angle)
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

        public void CalculatePosition(double velocity, double angle, double gravity, double time)
        {
            positionX = (velocity * terminalVelocity * Math.Cos(angle)) / gravity * (1 - Math.Pow(Math.E, -gravity * time / terminalVelocity));
            positionY = (terminalVelocity / gravity) * (velocity * Math.Sin(angle) + terminalVelocity) * (1 - Math.Pow(Math.E, -gravity * time / terminalVelocity)) - terminalVelocity * time;
        }

        public void CalculateTimeInFlight(double velocity, double angle, double gravity)
        {
            if (velocity * Math.Sin(angle) > terminalVelocity / gravity)
            {
                flightTime = 2 * velocity * Math.Sin(angle) / gravity;
            }
            else if (velocity * Math.Sin(angle) < terminalVelocity / gravity)
            {
                flightTime = velocity * Math.Sin(angle) / gravity;
            }
        }
    }
}
