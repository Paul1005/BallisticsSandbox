using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallisticsSandbox.Objects
{
    class Surface
    {
        public Surface(double thickness, double density, double angle)
        {
            this.Thickness = thickness;
            this.Density = density;
            this.Angle = angle;
        }

        public double Angle { get; set; }
        public double Density { get; set; }
        public double Thickness { get; set; }
    }
}
