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
    //  CARD PANEL - Panel trắng bo tròn kiểu thẻ
    // ══════════════════════════════════════════════
    public class CardPanel : Panel
    {
        public int Radius { get; set; } = 14;

        public CardPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }

        private static GraphicsPath RoundRect(Rectangle r, int rad)
        {
            rad = Math.Min(rad, Math.Min(r.Width, r.Height) / 2);
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
            g.Clear(Parent?.BackColor ?? Color.FromArgb(245, 247, 250));

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = RoundRect(rect, Radius);
            using var fill = new SolidBrush(BackColor);
            g.FillPath(fill, path);

            // Viền nhẹ tinh tế
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
            g.DrawPath(pen, path);
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
            Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            Cursor = Cursors.Hand;
            Size = new Size(140, 42);
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
        }

        private static GraphicsPath RoundRect(Rectangle r, int rad)
        {
            rad = Math.Min(rad, Math.Min(r.Width, r.Height) / 2);
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
            rad = Math.Min(rad, Math.Min(r.Width, r.Height) / 2);
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

    // ══════════════════════════════════════════════
    //  FEATURE CARD BUTTON - Thẻ chức năng Dashboard
    // ══════════════════════════════════════════════
    public class FeatureCardButton : Button
    {
        public string Title { get; set; } = "Tiêu đề";
        public string Description { get; set; } = "Mô tả chức năng";
        public string IconText { get; set; } = "📁";
        public Color AccentColor { get; set; } = Color.FromArgb(79, 70, 229);
        public int Radius { get; set; } = 14;
        private bool _hover;

        public FeatureCardButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            BackColor = Color.White;
            Cursor = Cursors.Hand;
            Size = new Size(345, 96);
            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }

        private static GraphicsPath RoundRect(Rectangle r, int rad)
        {
            rad = Math.Min(rad, Math.Min(r.Width, r.Height) / 2);
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
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Xóa nền theo cha
            g.Clear(Parent?.BackColor ?? Color.FromArgb(243, 244, 246));

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var cardPath = RoundRect(rect, Radius);

            // Nền thẻ: màu trắng hoặc ánh màu nhẹ khi hover
            Color bg = _hover ? Color.FromArgb(252, 253, 255) : Color.White;
            using (var brush = new SolidBrush(bg))
            {
                g.FillPath(brush, cardPath);
            }

            // Viền thẻ
            Color borderColor = _hover ? AccentColor : Color.FromArgb(226, 232, 240);
            float borderWidth = _hover ? 1.8f : 1.2f;
            using (var pen = new Pen(borderColor, borderWidth))
            {
                g.DrawPath(pen, cardPath);
            }

            // Dải màu nhấn bên trái
            int barWidth = _hover ? 5 : 4;
            var barRect = new Rectangle(0, 0, barWidth, Height);
            using (var clipPath = RoundRect(rect, Radius))
            {
                var oldClip = g.Clip;
                g.SetClip(clipPath);
                using (var barBrush = new SolidBrush(AccentColor))
                {
                    g.FillRectangle(barBrush, 0, 0, barWidth, Height);
                }
                g.Clip = oldClip;
            }

            // Hộp chứa Icon
            int iconBoxSize = 48;
            int iconBoxX = 16;
            int iconBoxY = (Height - iconBoxSize) / 2;
            var iconRect = new Rectangle(iconBoxX, iconBoxY, iconBoxSize, iconBoxSize);
            using (var iconPath = RoundRect(iconRect, 10))
            {
                Color iconBg = _hover ? AccentColor : Color.FromArgb(20, AccentColor);
                using var iconBrush = new SolidBrush(iconBg);
                g.FillPath(iconBrush, iconPath);
            }

            // Vẽ Icon (emoji)
            using (var iconFont = new Font("Segoe UI Emoji", 17f, FontStyle.Regular))
            {
                Color iconColor = _hover ? Color.White : AccentColor;
                TextRenderer.DrawText(g, IconText, iconFont, iconRect, iconColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            // Văn bản: Tiêu đề và Mô tả
            int textLeft = iconBoxX + iconBoxSize + 14;
            int textRight = Width - (_hover ? 30 : 16);
            int textWidth = Math.Max(20, textRight - textLeft);

            int titleY = iconBoxY + 3;
            using (var titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            {
                Color titleColor = _hover ? AccentColor : Color.FromArgb(30, 41, 59);
                var titleRect = new Rectangle(textLeft, titleY, textWidth, 22);
                TextRenderer.DrawText(g, Title, titleFont, titleRect, titleColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }

            int descY = titleY + 23;
            using (var descFont = new Font("Segoe UI", 8.5f, FontStyle.Regular))
            {
                Color descColor = Color.FromArgb(100, 116, 139);
                var descRect = new Rectangle(textLeft, descY, textWidth, Height - descY - 4);
                TextRenderer.DrawText(g, Description, descFont, descRect, descColor,
                    TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
            }

            // Mũi tên tương tác khi rê chuột
            if (_hover)
            {
                using var arrowFont = new Font("Segoe UI", 12f, FontStyle.Bold);
                var arrowRect = new Rectangle(Width - 26, 0, 20, Height);
                TextRenderer.DrawText(g, "→", arrowFont, arrowRect, AccentColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
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
}
