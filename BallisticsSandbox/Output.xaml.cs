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
using System.Windows.Shapes;

namespace BallisticsSandbox
{
    /// <summary>
    /// Interaction logic for Output.xaml
    /// </summary>
    public partial class Output : UserControl, ISwitchable
    {
        public string kineticEnergy;
        public string momentum;
        public string penetration;
        public string drag;

        private double maxPenetration;
        private double maxKineticEnergy;
        private double maxMomentum;
        private double initialVelocity;
        private double weight;
        private double angle;
        private double gravity;
        private double dragCoefficient;

        public Output(double Ek, double P, double Pen, double drag, double newVel, double posX, double posY, double flightTime, double range, double terminalVelocity, double initialVel, double w, double a, double g, double dc)
        {
            InitializeComponent();
            FinalVelocity.Text = newVel.ToString();
            FlightTime.Text = flightTime.ToString();
            Range.Text = range.ToString();
            TerminalVelocity.Text = terminalVelocity.ToString();

            maxPenetration = Pen;
            maxKineticEnergy = Ek;
            maxMomentum = P;
            initialVelocity = initialVel;
            weight = w;
            angle = a;
            gravity = g;
            dragCoefficient = dc;
            //DrawBulletParabolaGraph();
            //DrawPenetrationGraph();
            DrawKineticEnergyGraph();
            //DrawMomentumGraph();
        }

        private void DrawMomentumGraph()
        {
            for (int i = 0; i < Double.Parse(Range.Text); i++)
            {
                for (int j = 0; j < maxMomentum; i++)
                {

                }
            }
        }

        private void DrawKineticEnergyGraph()
        {
            double size = canvas.Width;
            double ratioX = size / Double.Parse(FlightTime.Text);
            double ratioY = size / maxKineticEnergy;

            //Debug.WriteLine(ratioX);
           // Debug.WriteLine(ratioY);

            double ratio = Math.Max(ratioX, ratioY);

            for (double i = 0; i < size; i += ratioX)
            {
                double velocityNow = CalculateVelocityAtTime(i, initialVelocity, weight, angle, gravity, dragCoefficient);
                double kineticEnergyNow = Math.Abs(CalculateKineticEnergy(weight, velocityNow) - maxKineticEnergy);

                double velocityNext = CalculateVelocityAtTime(i + ratioX, initialVelocity, weight, angle, gravity, dragCoefficient);
                double kineticEnergyNext = Math.Abs(CalculateKineticEnergy(weight, velocityNext) - maxKineticEnergy);

                Line graphSegment = new Line
                {
                    Stroke = System.Windows.Media.Brushes.Black,
                    X1 = i,
                    X2 = i + ratioX,
                    Y1 = ratioY * kineticEnergyNow,
                    Y2 = ratioY * kineticEnergyNext,
                    StrokeThickness = 1
                };

                canvas.Children.Add(graphSegment);
                Debug.WriteLine("ratioY " + ratioY);
                Debug.WriteLine("KineticEnergy " + kineticEnergyNow);
            }
        }

        private void DrawPenetrationGraph()
        {
            for (int i = 0; i < Double.Parse(Range.Text); i++)
            {
                for (int j = 0; j < maxPenetration; i++)
                {

                }
            }
        }

        private void DrawBulletParabolaGraph()
        {
            throw new NotImplementedException();
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
            Switcher.Switch((UserControl)state);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            UserControl mainWindow = new MainWindow();

            Switcher.Switch(mainWindow);
        }

        public double CalculateVelocityAtTime(double time, double velocity, double weight, double angle, double gravity, double dragCoefficient)
        {
            double newVelocityX = velocity * Math.Cos(angle) * Math.Pow(Math.E, -gravity * time / Double.Parse(TerminalVelocity.Text));
            double newVelocityY = velocity * Math.Sin(angle) * Math.Pow(Math.E, -gravity * time / Double.Parse(TerminalVelocity.Text)) - Double.Parse(TerminalVelocity.Text) * (1 - Math.Pow(Math.E, -gravity * time / Double.Parse(TerminalVelocity.Text)));

            return Math.Sqrt(Math.Pow(newVelocityX, 2) + Math.Pow(newVelocityY, 2));
        }

        public double CalculateKineticEnergy(double mass, double velocity)
        {
            return 0.5 * mass * Math.Pow(velocity, 2);
        }
    }
}
