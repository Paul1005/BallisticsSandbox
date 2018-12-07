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
        // input params
        public double velocity;
        public double weight;
        public double diameter;
        public double gravity;
        public double airDensity;
        public double dragCoefficient;
        public double angle;

        // output params
        public double maxPenetration;
        public double maxKineticEnergy;
        public double maxMomentum;
        public double area;
        public double terminalVelocity;
        public double range;
        public double flightTime;

        private Calculator calculator;
        private Graphing graphing;

        public Output(double velocity, double weight, double diameter, double gravity, double airDensity, double dragCoefficient, double angle)
        {
            this.velocity = velocity;
            this.weight = weight;
            this.diameter = diameter;
            this.gravity = gravity;
            this.airDensity = airDensity;
            this.dragCoefficient = dragCoefficient;
            this.angle = angle;

            InitializeComponent();
            calculator = new Calculator();
            graphing = new Graphing();

            area = calculator.CalculateArea(diameter);

            terminalVelocity = calculator.CalculateTerminalVelocity(weight, gravity, dragCoefficient, airDensity, area);
            TerminalVelocity.Text = terminalVelocity.ToString();

            range = calculator.CalculateRange(velocity, gravity, angle, terminalVelocity);
            Range.Text = range.ToString();

            flightTime = calculator.CalculateTimeInFlight(velocity, angle, gravity, terminalVelocity);

            FlightTime.Text = flightTime.ToString();

            maxMomentum = calculator.CalculateMomentum(weight, velocity);
            Recoil.Text = maxMomentum.ToString();

            maxKineticEnergy = calculator.CalculateKineticEnergy(weight, velocity);
            maxPenetration = calculator.CalculatePenetration(maxKineticEnergy, area);

            //DrawBulletParabolaGraph();
            //graphing.DrawKineticEnergyGraph(canvas, maxKineticEnergy, range, flightTime, velocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
            graphing.DrawPenetrationGraph(canvas, maxPenetration, range, flightTime, velocity, weight, area, angle, gravity, dragCoefficient, terminalVelocity);
            graphing.DrawMomentumGraph(canvas2, maxMomentum, range, flightTime, velocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
        }

        public void UtilizeState(object state)
        {
            Switcher.Switch((UserControl)state);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            UserControl mainWindow = new MainWindow(velocity, weight, diameter, gravity, airDensity, dragCoefficient, angle * (180 / Math.PI));

            Switcher.Switch(mainWindow);
        }
    }
}
