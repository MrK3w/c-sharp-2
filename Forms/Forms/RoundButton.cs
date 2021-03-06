using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Forms
{
    public delegate void MyButtonClickHandler(object sender, EventArgs args);
    public class RoundButton : Control
    {   
        public event MyButtonClickHandler MyButtonClick;
        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Red);
            e.Graphics.FillEllipse(brush,0,0,Width,Height);

            base.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            this.MyButtonClick?.Invoke(this,EventArgs.Empty);
            base.OnMouseClick(e);
        }

      
    }
}
