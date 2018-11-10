using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallisticsSandbox.Objects
{
    class Environment
    {
        public Environment(double airDensity, double gravity, double wind)
        {
            this.AirDensity = airDensity;
            this.Gravity = gravity;
            this.Wind = wind;
        }

        public double Wind { get; set; }
        public double Gravity { get; set; }
        public double AirDensity { get; set; }
    }
}
