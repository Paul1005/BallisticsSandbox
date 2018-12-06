using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallisticsSandbox.Objects
{
    public class Calculator
    {

        public double CalculateArea(double diameter)
        {
            return Math.PI * Math.Pow(diameter / 1000 / 2, 2);
        }

        public double CalculateTerminalVelocity(double weight, double gravity, double dragCoefficient, double airDensity, double area)
        {
            return Math.Sqrt((2 * weight * gravity) / (airDensity * 1000 * area * dragCoefficient));
        }

        public double CalculateKineticEnergy(double mass, double velocity)
        {
            return 0.5 * mass * Math.Pow(velocity, 2);
        }

        public double CalculateMomentum(double mass, double velocity)
        {
            return mass * velocity;
        }

        public double CalculatePenetration(double kineticEnergy, double area)
        {
            return kineticEnergy / area / 1000000000;
        }

        public double CalculateVelocityAtTime(double time, double velocity, double weight, double angle, double gravity, double dragCoefficient, double terminalVelocity)
        {
            double newVelocityX = velocity * Math.Cos(angle) * Math.Pow(Math.E, -gravity * time / terminalVelocity);
            double newVelocityY = velocity * Math.Sin(angle) * Math.Pow(Math.E, -gravity * time / terminalVelocity) - terminalVelocity * (1 - Math.Pow(Math.E, -gravity * time / terminalVelocity));

            return Math.Sqrt(Math.Pow(newVelocityX, 2) + Math.Pow(newVelocityY, 2));
        }

        public double CalculateRange(double velocity, double gravity, double angle, double terminalVelocity)
        {
            if (velocity * Math.Sin(angle) > terminalVelocity)
            {
                return (velocity * terminalVelocity * Math.Cos(angle)) / gravity;
            }
            else if (velocity * Math.Sin(angle) < terminalVelocity)
            {
                return (Math.Pow(velocity, 2) * Math.Sin(2 * angle)) / gravity;
            }
            return -1;
        }

        public double CalculatePositionX(double velocity, double angle, double gravity, double time, double terminalVelocity)
        {
            return (velocity * terminalVelocity * Math.Cos(angle)) / gravity * (1 - Math.Pow(Math.E, -gravity * time / terminalVelocity));
        }

        public double CalculatePositionY(double velocity, double angle, double gravity, double time, double terminalVelocity)
        {
            return (terminalVelocity / gravity) * (velocity * Math.Sin(angle) + terminalVelocity) * (1 - Math.Pow(Math.E, -gravity * time / terminalVelocity)) - terminalVelocity * time;
        }

        public double CalculateTimeInFlight(double velocity, double angle, double gravity, double terminalVelocity)
        {
            if (velocity * Math.Sin(angle) > terminalVelocity / gravity)
            {
                return 2 * velocity * Math.Sin(angle) / gravity;
            }
            else if (velocity * Math.Sin(angle) < terminalVelocity / gravity)
            {
                return velocity * Math.Sin(angle) / gravity;
            }
            return -1;
        }
    }
}
