using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    public static class Bezier3
    {
        public static double B0(double t) { return (1.0 - t) * (1.0 - t) * (1.0 - t); }
        public static double B1(double t) { return 3.0 * (1.0 - t) * (1.0 - t) * t; }
        public static double B2(double t) { return 3.0 * (1.0 - t) * t * t; }
        public static double B3(double t) { return t * t * t; }
    }
}
