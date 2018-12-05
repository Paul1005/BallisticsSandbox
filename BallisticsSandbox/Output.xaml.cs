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

        public Output(double Ek, double P, double Pen, double drag, double newVel, double posX, double posY, double flightTime, double range, double terminalVelocity)
        {
            InitializeComponent();
            FinalVelocity.Text = newVel.ToString();
            FlightTime.Text = flightTime.ToString();
            Range.Text = range.ToString();
            TerminalVelocity.Text = terminalVelocity.ToString();

            maxPenetration = Pen;
            maxKineticEnergy = Ek;
            maxMomentum = P;

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
            Debug.WriteLine(maxKineticEnergy);
            double ratio = (maxKineticEnergy * 3) / Double.Parse(Range.Text);
            Debug.WriteLine(ratio);
            double j = 0;
            for (int i = 0; i < Double.Parse(Range.Text); i++)
            {
                j += ratio;
                Line myLine = new Line();
                myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                myLine.X1 = i * 3;
                myLine.X2 = (i + 1) * 3;
                myLine.Y1 = j;
                myLine.Y2 = j + ratio;
                myLine.StrokeThickness = 1;
                canvas.Children.Add(myLine);
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
    }
}
