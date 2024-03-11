using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartOnePass
{
    class CustomRoundGroupBox : GroupBox
    {
        public int cornerRadius = 15; //라운드 너비
        public Color borderColor = Color.DarkGray;//외곽선 색상
        public int borderWidth = 1;//외곽선 두께
        //public Color backColor = Color.LightGray;//배경 색상
        public Color backColor = Color.Transparent;//배경 색상

        public bool isFillLeftTop = false;//왼쪽위 사각으로 채우기(라운드 적용X)
        public bool isFillRightTop = false;//오른쪽위 사각으로 채우기(라운드 적용X)
        public bool isFillLeftBtm = false;//왼쪽아래 사각으로 채우기(라운드 적용X)
        public bool isFillRightBtm = false;//오른쪽아래 사각으로 채우기(라운드 적용X)


        public CustomRoundGroupBox()
        {
            this.DoubleBuffered = true;
            this.borderColor = Color.LightGray;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            using (var graphicsPath = _getRoundRectangle(this.ClientRectangle))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var brush = new SolidBrush(backColor);
                var pen = new Pen(borderColor, borderWidth);
                e.Graphics.FillPath(brush, graphicsPath);
                e.Graphics.DrawPath(pen, graphicsPath);
                var textPosition = new Point(10,0);
                var rect = new RectangleF(textPosition.X, textPosition.Y, this.Text.Length*(this.Font.Size - 1), 10);
                //Filling a rectangle before drawing the string.

                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 92, 170)), rect);
                TextRenderer.DrawText(e.Graphics, Text, this.Font, textPosition, this.ForeColor);
            }
        }

        private GraphicsPath _getRoundRectangle(Rectangle rectangle)
        {
            GraphicsPath path = new GraphicsPath();

            //path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);

            int left = rectangle.X;
            int top = rectangle.Y;
            int right = rectangle.X + rectangle.Width - borderWidth;
            int bottom = rectangle.Y + rectangle.Height - borderWidth;

            if (isFillLeftTop)
            {//좌상
                path.AddLine(left, top + cornerRadius, left, top);
                path.AddLine(left, top, left + cornerRadius, top);
            }
            else
            {
                //path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(rectangle.X, rectangle.Y + 5, cornerRadius, cornerRadius, 180, 90);
            }
            
            if (isFillRightTop)
            {//우상
                path.AddLine(right - cornerRadius, top, right, top);
                path.AddLine(right, top, right, top + cornerRadius);
            }
            else
            {
                //path.AddArc(rectangle.X + rectangle.Width - cornerRadius - borderWidth, rectangle.Y, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(rectangle.X + rectangle.Width - cornerRadius - borderWidth, rectangle.Y+5, cornerRadius, cornerRadius, 270, 90);
            }
            if (isFillRightBtm)
            {//우하
                path.AddLine(right, bottom - cornerRadius, right, bottom);
                path.AddLine(right, bottom, right - cornerRadius, bottom);
            }
            else
            {
                path.AddArc(rectangle.X + rectangle.Width - cornerRadius - borderWidth, rectangle.Y + rectangle.Height - cornerRadius - borderWidth, cornerRadius, cornerRadius, 0, 90);
            }
            if (isFillLeftBtm)
            {//좌하
                path.AddLine(left + cornerRadius, bottom, left, bottom);
                path.AddLine(left, bottom, left, bottom - cornerRadius);
            }
            else
            {
                path.AddArc(rectangle.X, rectangle.Y + rectangle.Height - cornerRadius - borderWidth, cornerRadius, cornerRadius, 90, 90);
            }

            path.CloseAllFigures();
            
            return path;
        }

    }
}
