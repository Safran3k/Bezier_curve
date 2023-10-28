using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaAlap
{
    public static class ExtensionGraphics
    {
        public delegate double RtoRP(double t, double p);
        public static void DrawParametricCurveP(this Graphics g, Pen pen, RtoRP x, RtoRP y, double a, double b, double p, int n = 500)
        {
            double t = a;
            double h = (b - a) / n;
            Vector2 v0 = new Vector2(x(t, p), y(t, p));
            Vector2 v1;

            while (t < b)
            {
                t += h;
                v1 = new Vector2(x(t, p), y(t, p));
                g.DrawLine(pen, (float)v0.x, (float)v0.y, (float)v1.x, (float)v1.y);
                v0 = v1;
            }
        }

        public static void DrawBezier3P(this Graphics g, Pen pen, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, double p)
        {
            g.DrawParametricCurveP(pen,
                (t, pValue) => 
                    Bezier3P.B0(t, pValue) * p0.x + 
                    Bezier3P.B1(t, pValue) * p1.x + 
                    Bezier3P.B2(t, pValue) * p2.x + 
                    Bezier3P.B3(t, pValue) * p3.x,
                (t, pValue) => 
                    Bezier3P.B0(t, pValue) * p0.y + 
                    Bezier3P.B1(t, pValue) * p1.y + 
                    Bezier3P.B2(t, pValue) * p2.y + 
                    Bezier3P.B3(t, pValue) * p3.y,
                0.0, 1.0, p);
        }

    }
}
