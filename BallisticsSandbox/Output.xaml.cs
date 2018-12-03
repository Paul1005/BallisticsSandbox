﻿using BallisticsSandbox.Objects;
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

        public Output(double Ek, double P, double Pen, double drag, double newVelX, double newVelY)
        {
            InitializeComponent();

            kineticEnergy = KineticEnergy.Text = Ek.ToString();
            momentum = Recoil.Text = P.ToString();
            penetration = Penetration.Text = Pen.ToString();
            this.drag = Drag.Text = drag.ToString();
            NewVelocityX.Text = newVelX.ToString();
            NewVelocityY.Text = newVelY.ToString();
            NewVelocity.Text = Math.Sqrt(Math.Pow(newVelX, 2) + Math.Pow(newVelY, 2)).ToString();
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
