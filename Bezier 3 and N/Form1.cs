using GrafikaAlap.Classes;
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
        double lambda = 0.0;

        public Form1()
        {
            InitializeComponent();
            cbBezier.DataSource = Enum.GetValues(typeof(BezierCurvesEnum));
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

            if ((BezierCurvesEnum)cbBezier.SelectedItem == BezierCurvesEnum.Bezier_3)
            {
                if (v.Count == 4)
                {
                    g.DrawBezier3P(penCurve, v[0], v[1], v[2], v[3], lambda);
                    label1.Text = "[" + v[0].x.ToString() + ";" + v[0].y + "]";
                    label3.Text = "[" + v[1].x.ToString() + ";" + v[1].y + "]";
                    label5.Text = "[" + v[2].x.ToString() + ";" + v[2].y + "]";
                    label7.Text = "[" + v[3].x.ToString() + ";" + v[3].y + "]";
                }
            }
            else if ((BezierCurvesEnum)cbBezier.SelectedItem == BezierCurvesEnum.Bezier_N)
            {
                g.DrawBezierN(penCurve, v);
            }
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

            if ((BezierCurvesEnum)cbBezier.SelectedItem == BezierCurvesEnum.Bezier_3)
            {
                if (grabbed == -1 && v.Count < 4)
                {
                    v.Add(new Vector2(e.X, e.Y));
                    grabbed = v.Count - 1;
                    canvas.Invalidate();
                }
            }
            else if ((BezierCurvesEnum)cbBezier.SelectedItem == BezierCurvesEnum.Bezier_N)
            {
                if (grabbed == -1)
                {
                    v.Add(new Vector2(e.X, e.Y));
                    grabbed = v.Count - 1;
                    canvas.Invalidate();
                }
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
        private void sbLambda_ValueChanged(object sender, EventArgs e)
        {
            int offset = 250; 
            int scrollbarValue = sbLambda.Value;
            lambda = (scrollbarValue - offset) / 62.5;
            lbLambdaValue.Text = lambda.ToString();
            canvas.Invalidate();
        }
        private void cbBezier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBezier.SelectedIndex == 0 || cbBezier.SelectedIndex == 2)
            {
                panel1.Enabled = false;
                panel1.Visible = false;
            }
            else
            {
                panel1.Enabled = true;
                panel1.Visible = true;
            }

            v.Clear();
            canvas.Invalidate();
        }
    }
}
