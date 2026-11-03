using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UDSV.Controls
{
    // ══════════════════════════════════════════════
    //  GRADIENT PANEL - Panel với nền gradient đẹp
    // ══════════════════════════════════════════════
    public class GradientPanel : Panel
    {
        public Color ColorStart { get; set; } = Color.FromArgb(102, 126, 234);
        public Color ColorEnd { get; set; } = Color.FromArgb(118, 75, 162);
        public float Angle { get; set; } = 135f;

        public GradientPanel()
        {
            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
            {
                base.OnPaintBackground(e);
                return;
            }
            using var brush = new LinearGradientBrush(
                ClientRectangle, ColorStart, ColorEnd, Angle);
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ các hình tròn trang trí mờ
            using var b1 = new SolidBrush(Color.FromArgb(15, 255, 255, 255));
            g.FillEllipse(b1, -50, -50, 200, 200);
            g.FillEllipse(b1, Width - 100, Height - 80, 200, 200);

            using var b2 = new SolidBrush(Color.FromArgb(10, 255, 255, 255));
            g.FillEllipse(b2, Width / 2 - 80, -40, 160, 160);
            g.FillEllipse(b2, -30, Height - 60, 120, 120);
        }
    }

    // ══════════════════════════════════════════════
    //  GRADIENT BUTTON - Nút bấm gradient bo tròn
    // ══════════════════════════════════════════════
    public class GradientButton : Button
    {
        public Color GradientStart { get; set; } = Color.FromArgb(102, 126, 234);
        public Color GradientEnd { get; set; } = Color.FromArgb(118, 75, 162);
        public int Radius { get; set; } = 24;
        private bool _hover;

        public GradientButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            Cursor = Cursors.Hand;
            Size = new Size(300, 48);
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
        }

        private static GraphicsPath RoundRect(Rectangle r, int rad)
        {
            var path = new GraphicsPath();
            int d = rad * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? SystemColors.Control);

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = RoundRect(rect, Radius);

            var c1 = _hover ? ControlPaint.Light(GradientStart, 0.15f) : GradientStart;
            var c2 = _hover ? ControlPaint.Light(GradientEnd, 0.15f) : GradientEnd;

            using var brush = new LinearGradientBrush(rect, c1, c2, 45f);
            g.FillPath(brush, path);

            TextRenderer.DrawText(g, Text, Font, rect, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _hover = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hover = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
    }

    // ══════════════════════════════════════════════
    //  TEXT BOX CONTAINER - Khung nhập liệu bo tròn
    // ══════════════════════════════════════════════
    public class TextBoxContainer : Panel
    {
        public Color BorderNormal { get; set; } = Color.FromArgb(223, 230, 233);
        public Color BorderFocus { get; set; } = Color.FromArgb(102, 126, 234);
        public int Radius { get; set; } = 12;
        private bool _focused;

        public TextBoxContainer()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Height = 48;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Gắn TextBox vào container với style hiện đại.
        /// </summary>
        public void AttachTextBox(TextBox tb)
        {
            tb.BorderStyle = BorderStyle.None;
            tb.BackColor = Color.White;
            tb.Font = new Font("Segoe UI", 11f);
            tb.ForeColor = Color.FromArgb(45, 52, 54);
            tb.Location = new Point(16, (Height - tb.PreferredHeight) / 2);
            tb.Width = Width - 32;
            tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb.GotFocus += (_, _) => { _focused = true; Invalidate(); };
            tb.LostFocus += (_, _) => { _focused = false; Invalidate(); };
            Controls.Add(tb);
        }

        private static GraphicsPath RoundRect(Rectangle r, int rad)
        {
            var path = new GraphicsPath();
            int d = rad * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Color.White);

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = RoundRect(rect, Radius);

            // Tô nền trắng bên trong
            using var fill = new SolidBrush(BackColor);
            g.FillPath(fill, path);

            // Vẽ viền - đổi màu khi focus
            var borderColor = _focused ? BorderFocus : BorderNormal;
            float borderWidth = _focused ? 2f : 1.5f;
            using var pen = new Pen(borderColor, borderWidth);
            g.DrawPath(pen, path);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (Control c in Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Location = new Point(16, (Height - tb.PreferredHeight) / 2);
                    tb.Width = Width - 32;
                }
            }
        }
    }
}

