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
        public delegate double RtoR(double x);
        public static void DrawParametricCurve(this Graphics g, Pen pen, RtoR x, RtoR y, double a, double b, int n = 500)
        {
            double t = a;
            double h = (b - a) / n;
            Vector2 v0 = new Vector2(x(t), y(t));
            Vector2 v1;
            while (t < b)
            {
                t += h;
                v1 = new Vector2(x(t), y(t));
                g.DrawLine(pen, (float)v0.x, (float)v0.y, (float)v1.x, (float)v1.y);
                v0 = v1;
            }
        }

        public static void DrawBezierN(this Graphics g, Pen pen, List<Vector2> p)
        {
            for (int i = 0; i < p.Count - 1; i += 3)
            {
                if (i + 3 < p.Count)
                {
                    g.DrawParametricCurve(pen,
                        t =>
                            Bezier3.B0(t) * p[i].x +
                            Bezier3.B1(t) * p[i + 1].x +
                            Bezier3.B2(t) * p[i + 2].x +
                            Bezier3.B3(t) * p[i + 3].x,
                        t =>
                            Bezier3.B0(t) * p[i].y +
                            Bezier3.B1(t) * p[i + 1].y +
                            Bezier3.B2(t) * p[i + 2].y +
                            Bezier3.B3(t) * p[i + 3].y,
                        0.0, 1.0);
                }
            }
        }
    }
}
