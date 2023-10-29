using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<Vector2> v = new List<Vector2>();
        Pen penControl = Pens.Black;
        Pen penCurve = new Pen(Color.Blue, 3f);
        int grabbed = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            for (int i = 0; i < v.Count; i++)
            {
                g.DrawRectangle(penControl, (float)v[i].x - ExtensionPoint.GRAB_DISTANCE,
                                            (float)v[i].y - ExtensionPoint.GRAB_DISTANCE,
                                            2 * ExtensionPoint.GRAB_DISTANCE,
                                            2 * ExtensionPoint.GRAB_DISTANCE);              
            }
            for (int i = 0; i < v.Count - 1; i++)
            {
                g.DrawLine(penControl, (float)v[i].x, (float)v[i].y,
                                     (float)v[i + 1].x, (float)v[i + 1].y);
            }

            g.DrawBezierN(penCurve, v);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < v.Count; i++)
            {
                if (e.Location.Grabbed(v[i]))
                {
                    grabbed = i;
                    break;
                }
            }

            if (grabbed == -1)
            {
                v.Add(new Vector2(e.X, e.Y));
                grabbed = v.Count - 1;
                canvas.Invalidate();
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed != -1)
            {
                v[grabbed].x = e.X;
                v[grabbed].y = e.Y;
                canvas.Invalidate();
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = -1;
        }        

    }
}
