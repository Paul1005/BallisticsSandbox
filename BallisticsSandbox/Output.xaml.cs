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
        private double terminalVelocity;

        private Calculator calculator;
        private Graphing graphing;

        public Output(string velocity, string weight, string diameter, string gravity, string airDensity, string dragCoefficient, string angle)
        {
            InitializeComponent();
            calculator = new Calculator();
            graphing = new Graphing();

            area = calculator.CalculateArea(Double.Parse(diameter));
            terminalVelocity = calculator.CalculateTerminalVelocity(Double.Parse(weight), Double.Parse(gravity), Double.Parse(dragCoefficient), Double.Parse(airDensity), area);

            TerminalVelocity.Text = terminalVelocity.ToString();
            Range.Text = calculator.CalculateRange(Double.Parse(velocity), Double.Parse(gravity), Double.Parse(angle), terminalVelocity).ToString();
            FlightTime.Text = calculator.CalculateTimeInFlight(Double.Parse(velocity), Double.Parse(angle), Double.Parse(gravity) , terminalVelocity).ToString();
            Recoil.Text = calculator.CalculateMomentum(Double.Parse(weight), Double.Parse(velocity)).ToString();

            //DrawBulletParabolaGraph();
            graphing.DrawKineticEnergyGraph(canvas);
            graphing.DrawPenetrationGraph(canvas2);
            //DrawMomentumGraph();
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
    }
}
