using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallisticsSandbox.Objects
{
    class Bullet
    {
        public Bullet(double diameter, double weight) {
            this.Weight = weight;
            this.Diameter = diameter;
        }

        public double Diameter { get; set; }
        public double Weight { get; set; }
    }
}
