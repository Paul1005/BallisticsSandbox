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
        public double velocity;
        public double weight;
        public double diameter;
        public double gravity;
        public double airDensity;
        public double dragCoefficient;
        public double angle;

        public Output output;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(double velocity, double weight, double diameter, double gravity, double airDensity, double dragCoefficient, double angle)
        {
            InitializeComponent();

            Velocity.Text = velocity.ToString();
            Weight.Text = weight.ToString();
            Diameter.Text = diameter.ToString();
            Gravity.Text = gravity.ToString();
            AirDensity.Text = airDensity.ToString();
            DragCoefficient.Text = dragCoefficient.ToString();
            Angle.Text = angle.ToString();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            velocity = Double.Parse(Velocity.Text);
            weight = Double.Parse(Weight.Text);
            diameter = Double.Parse(Diameter.Text);
            gravity = Double.Parse(Gravity.Text);
            airDensity = Double.Parse(AirDensity.Text);
            dragCoefficient = Double.Parse(DragCoefficient.Text);
            angle = Double.Parse(Angle.Text) * (Math.PI / 180);

            output = new Output(velocity, weight, diameter, gravity, airDensity, dragCoefficient, angle);

            Switcher.Switch(output);
        }

        public void Test_Calculate_Click()
        {
            velocity = Double.Parse(Velocity.Text);
            weight = Double.Parse(Weight.Text);
            diameter = Double.Parse(Diameter.Text);
            gravity = Double.Parse(Gravity.Text);
            airDensity = Double.Parse(AirDensity.Text);
            dragCoefficient = Double.Parse(DragCoefficient.Text);
            angle = Double.Parse(Angle.Text) * (Math.PI / 180);

            output = new Output(velocity, weight, diameter, gravity, airDensity, dragCoefficient, angle);
        }
    }
}
