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

namespace BallisticsSandbox.Objects
{
    class Graphing
    {
        Calculator calculator;

        public Graphing()
        {
            calculator = new Calculator();
        }

        public void DrawKineticEnergyGraph(Canvas canvas, double maxKineticEnergy, double range, double flightTime, double initialVelocity, double weight, double angle, double gravity, double dragCoefficient, double terminalVelocity)
        {
            double size = canvas.Width;

            DrawAxis(canvas, size, maxKineticEnergy, range, "Meters", "Joules", "Kinetic Energy/Distance graph");

            double ratioEnergy = size / maxKineticEnergy;
            double ratioPosition = size / range;

            for (double i = 0; i < flightTime; i++)
            {
                double velocityNow = calculator.CalculateVelocityAtTime(i, initialVelocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
                double kineticEnergyNow = Math.Abs(calculator.CalculateKineticEnergy(weight, velocityNow) - maxKineticEnergy);
                double postionNow = calculator.CalculatePositionX(initialVelocity, angle, gravity, i, terminalVelocity);

                double velocityNext = calculator.CalculateVelocityAtTime(i + 1, initialVelocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
                double kineticEnergyNext = Math.Abs(calculator.CalculateKineticEnergy(weight, velocityNext) - maxKineticEnergy);
                double postionNext = calculator.CalculatePositionX(initialVelocity, angle, gravity, i + 1, terminalVelocity);

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

        public void DrawPenetrationGraph(Canvas canvas, double maxPenetration, double range, double flightTime, double initialVelocity, double weight, double area, double angle, double gravity, double dragCoefficient, double terminalVelocity)
        {
            double size = canvas.Width;

            DrawAxis(canvas, size, maxPenetration, range, "Meters", "mm", "Penetration/Distance graph");

            double ratioPenetration = size / maxPenetration;
            double ratioPosition = size / range;

            for (double i = 0; i < flightTime; i++)
            {
                double velocityNow = calculator.CalculateVelocityAtTime(i, initialVelocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
                double kineticEnergyNow = calculator.CalculateKineticEnergy(weight, velocityNow);
                double penetrationNow = Math.Abs(calculator.CalculatePenetration(kineticEnergyNow, area) - maxPenetration);
                double postionNow = calculator.CalculatePositionX(initialVelocity, angle, gravity, i, terminalVelocity);

                double velocityNext = calculator.CalculateVelocityAtTime(i + 1, initialVelocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
                double kineticEnergyNext = calculator.CalculateKineticEnergy(weight, velocityNext);
                double penetrationNext = Math.Abs(calculator.CalculatePenetration(kineticEnergyNext, area) - maxPenetration);
                double postionNext = calculator.CalculatePositionX(initialVelocity, angle, gravity, i + 1, terminalVelocity);

                Line graphSegment = new Line
                {
                    Stroke = Brushes.Blue,
                    X1 = ratioPosition * postionNow,
                    X2 = ratioPosition * postionNext,
                    Y1 = ratioPenetration * penetrationNow,
                    Y2 = ratioPenetration * penetrationNext,
                    StrokeThickness = 1
                };

                canvas.Children.Add(graphSegment);
            }
        }

        public void DrawMomentumGraph(Canvas canvas, double maxMomentum, double range, double flightTime, double initialVelocity, double weight, double angle, double gravity, double dragCoefficient, double terminalVelocity)
        {
            double size = canvas.Width;

            DrawAxis(canvas, size, maxMomentum, range, "Meters", "Newtons Seconds", "Momentum/Distance graph");

            double ratioMomentum = size / maxMomentum;
            double ratioPosition = size / range;

            for (double i = 0; i < flightTime; i++)
            {
                double velocityNow = calculator.CalculateVelocityAtTime(i, initialVelocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
                double momentumNow = Math.Abs(calculator.CalculateMomentum(weight, velocityNow) - maxMomentum);
                double postionNow = calculator.CalculatePositionX(initialVelocity, angle, gravity, i, terminalVelocity);

                double velocityNext = calculator.CalculateVelocityAtTime(i + 1, initialVelocity, weight, angle, gravity, dragCoefficient, terminalVelocity);
                double momentumNext = Math.Abs(calculator.CalculateMomentum(weight, velocityNext) - maxMomentum);
                double postionNext = calculator.CalculatePositionX(initialVelocity, angle, gravity, i + 1, terminalVelocity);

                Line graphSegment = new Line
                {
                    Stroke = Brushes.Blue,
                    X1 = ratioPosition * postionNow,
                    X2 = ratioPosition * postionNext,
                    Y1 = ratioMomentum * momentumNow,
                    Y2 = ratioMomentum * momentumNext,
                    StrokeThickness = 1
                };

                canvas.Children.Add(graphSegment);
            }
        }
    }
}
