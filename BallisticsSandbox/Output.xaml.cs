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
        private double area;

        public Output(double Ek, double P, double Pen, double drag, double newVel, double posX, double posY, double flightTime, double range, double terminalVelocity, double initialVel, double w, double a, double g, double dc, double area)
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
            this.area = area;
            //DrawBulletParabolaGraph();
            DrawKineticEnergyGraph();
            DrawPenetrationGraph();
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

            DrawAxis(canvas, size, maxKineticEnergy, Double.Parse(Range.Text), "Meters", "Joules", "Kinetic Energy/Distance graph");

            double ratioEnergy = size / maxKineticEnergy;
            double ratioPosition = size / Double.Parse(Range.Text);

            for (double i = 0; i < Double.Parse(FlightTime.Text); i++)
            {
                double velocityNow = CalculateVelocityAtTime(i, initialVelocity, weight, angle, gravity, dragCoefficient);
                double kineticEnergyNow = Math.Abs(CalculateKineticEnergy(weight, velocityNow) - maxKineticEnergy);
                double postionNow = CalculatePositionX(initialVelocity, angle, gravity, i);

                double velocityNext = CalculateVelocityAtTime(i + 1, initialVelocity, weight, angle, gravity, dragCoefficient);
                double kineticEnergyNext = Math.Abs(CalculateKineticEnergy(weight, velocityNext) - maxKineticEnergy);
                double postionNext = CalculatePositionX(initialVelocity, angle, gravity, i + 1);

                Line graphSegment = new Line
                {
                    Stroke = Brushes.Blue,
                    X1 = ratioPosition * postionNow,
                    X2 = ratioPosition * postionNext,
                    Y1 = ratioEnergy * kineticEnergyNow,
                    Y2 = ratioEnergy * kineticEnergyNext,
                    StrokeThickness = 1
                };

                canvas.Children.Add(graphSegment);
            }
        }

        private void DrawAxis(Canvas canvas, double size, double x, double y, string xLegend, string yLegend, string title)
        {
            int numOfIncrements = 4;

            double xIncrements = x / numOfIncrements;

            double yIncrements = y / numOfIncrements;

            double incrementsScale = size / numOfIncrements;

            long startX = 0;
            long startY = 0;
            double startScale = 0;

            TextBox graphTitle = new TextBox();
            graphTitle.Text = title;
            graphTitle.Margin = new Thickness(size / 2, 0, 0, 0);
            canvas.Children.Add(graphTitle);

            TextBox legendX = new TextBox();
            legendX.Text = xLegend;
            legendX.Margin = new Thickness(size / 2, 1.0625 * size, 0, 0);
            canvas.Children.Add(legendX);

            TextBox legendY = new TextBox();
            legendY.Text = yLegend;
            legendY.Margin = new Thickness(-size / 8, size - size / 2, 0, 0);
            canvas.Children.Add(legendY);

            for (int i = 0; i <= numOfIncrements; i++)
            {
                TextBox textBoxX = new TextBox();
                textBoxX.Text = startX.ToString();
                textBoxX.Margin = new Thickness(0, size - startScale, 0, 0);
                canvas.Children.Add(textBoxX);

                TextBox textBoxY = new TextBox();
                textBoxY.Text = startY.ToString();
                textBoxY.Margin = new Thickness(startScale, size, 0, 0);
                canvas.Children.Add(textBoxY);

                startX += (long)xIncrements;
                startY += (long)yIncrements;
                startScale += incrementsScale;
            }

            Line XAxis = new Line
            {
                Stroke = Brushes.Black,
                X1 = 0,
                X2 = size,
                Y1 = size,
                Y2 = size,
                StrokeThickness = 2
            };
            canvas.Children.Add(XAxis);

            Line YAxis = new Line
            {
                Stroke = Brushes.Black,
                X1 = 0,
                X2 = 0,
                Y1 = size,
                Y2 = 0,
                StrokeThickness = 2
            };
            canvas.Children.Add(YAxis);
        }

        private void DrawPenetrationGraph()
        {
            double size = canvas2.Width;

            DrawAxis(canvas2, size, maxPenetration, Double.Parse(Range.Text), "Meters", "mm", "Penetration/Distance graph");

            double ratioPenetration = size / maxPenetration;
            double ratioPosition = size / Double.Parse(Range.Text);

            for (double i = 0; i < Double.Parse(FlightTime.Text); i++)
            {
                double velocityNow = CalculateVelocityAtTime(i, initialVelocity, weight, angle, gravity, dragCoefficient);
                double kineticEnergyNow = CalculateKineticEnergy(weight, velocityNow);
                double penetrationNow = Math.Abs(CalculatePenetration(kineticEnergyNow, area) - maxPenetration);
                double postionNow = CalculatePositionX(initialVelocity, angle, gravity, i);

                double velocityNext = CalculateVelocityAtTime(i + 1, initialVelocity, weight, angle, gravity, dragCoefficient);
                double kineticEnergyNext = CalculateKineticEnergy(weight, velocityNext);
                double penetrationNext = Math.Abs(CalculatePenetration(kineticEnergyNext, area) - maxPenetration);
                double postionNext = CalculatePositionX(initialVelocity, angle, gravity, i + 1);

                Line graphSegment = new Line
                {
                    Stroke = Brushes.Blue,
                    X1 = ratioPosition * postionNow,
                    X2 = ratioPosition * postionNext,
                    Y1 = ratioPenetration * penetrationNow,
                    Y2 = ratioPenetration * penetrationNext,
                    StrokeThickness = 1
                };

                canvas2.Children.Add(graphSegment);
            }
        }
        public double CalculatePenetration(double kineticEnergy, double area)
        {
            return kineticEnergy / area /1000000000;
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
        public double CalculatePositionX(double velocity, double angle, double gravity, double time)
        {
            return (velocity * Double.Parse(TerminalVelocity.Text) * Math.Cos(angle)) / gravity * (1 - Math.Pow(Math.E, -gravity * time / Double.Parse(TerminalVelocity.Text)));
        }

        public double CalculatePositionY(double velocity, double angle, double gravity, double time)
        {
            return (Double.Parse(TerminalVelocity.Text) / gravity) * (velocity * Math.Sin(angle) + Double.Parse(TerminalVelocity.Text)) * (1 - Math.Pow(Math.E, -gravity * time / Double.Parse(TerminalVelocity.Text))) - Double.Parse(TerminalVelocity.Text) * time;
        }
    }
}
