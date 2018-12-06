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
        public double newVelocity;
        public double range;
        public double terminalVelocity;
        public double positionX;
        public double positionY;
        public double flightTime;
        public double area;

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
            UserControl output = new Output(Velocity.Text, Weight.Text, Diameter.Text, Gravity.Text, AirDensity.Text, DragCoefficient.Text, Angle.Text);

            Switcher.Switch(output);
        }
    }
}
