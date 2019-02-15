using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace design_dance
{
    class RoundedButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath graph = new GraphicsPath();
            graph.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(graph);
            base.OnPaint(pevent);
        }
    }
}
