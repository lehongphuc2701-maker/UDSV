using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace UDSV
{
    /// <summary>
    /// Hộp thoại thông báo hiện đại (Custom MessageBox) chuẩn nhận diện BVU / UDSV:
    /// - Bo góc tròn tinh tế, viền mềm mại, icon badge trực quan theo từng loại thông báo.
    /// - Hỗ trợ đầy đủ các kiểu nút (OK, YesNo, OKCancel, YesNoCancel).
    /// - Tương thích 100% chữ ký của MessageBox.Show để làm đẹp toàn bộ ứng dụng.
    /// </summary>
    public static class ThongBao
    {
        public static DialogResult Show(string text) =>
            Show(null, text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static DialogResult Show(string text, string caption) =>
            Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons) =>
            Show(null, text, caption, buttons, MessageBoxIcon.Information);

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) =>
            Show(null, text, caption, buttons, icon);

        public static DialogResult Show(IWin32Window? owner, string text) =>
            Show(owner, text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static DialogResult Show(IWin32Window? owner, string text, string caption) =>
            Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static DialogResult Show(IWin32Window? owner, string text, string caption, MessageBoxButtons buttons) =>
            Show(owner, text, caption, buttons, MessageBoxIcon.Information);

        public static DialogResult Show(
            IWin32Window? owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            using var frm = new FrmThongBaoDialog(text, caption, buttons, icon);
            if (owner != null)
                return frm.ShowDialog(owner);
            return frm.ShowDialog();
        }

        // ══════════════════════════════════════════════════════════════
        // Các hàm helper tiện lợi (Semantic Helpers)
        // ══════════════════════════════════════════════════════════════
        public static DialogResult ThanhCong(string text, string caption = "Thành công", IWin32Window? owner = null) =>
            Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static DialogResult CanhBao(string text, string caption = "Cảnh báo", IWin32Window? owner = null) =>
            Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static DialogResult Loi(string text, string caption = "Lỗi", IWin32Window? owner = null) =>
            Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static bool XacNhan(string text, string caption = "Xác nhận", IWin32Window? owner = null) =>
            Show(owner, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    /// <summary>
    /// Form popup thông báo hiện đại, bo tròn, có hiệu ứng đổ bóng và nút bấm màu sắc.
    /// </summary>
    internal class FrmThongBaoDialog : Form
    {
        private readonly string _text;
        private readonly string _caption;
        private readonly MessageBoxButtons _buttons;
        private readonly MessageBoxIcon _icon;

        private Point _mouseOffset;
        private bool _isMouseDown;

        public FrmThongBaoDialog(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            _text = text ?? "";
            _caption = string.IsNullOrWhiteSpace(caption) ? "Thông báo" : caption;
            _buttons = buttons;
            _icon = icon;

            KhoiTaoGiaoDien();
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

        private void KhoiTaoGiaoDien()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            Width = 480;

            // Phân loại kiểu thông báo (Màu sắc, Icon, Âm thanh)
            string capLower = _caption.ToLowerInvariant();
            bool isError = _icon == MessageBoxIcon.Error || _icon == MessageBoxIcon.Hand || _icon == MessageBoxIcon.Stop ||
                           capLower.Contains("lỗi") || capLower.Contains("error") || capLower.Contains("thất bại") || capLower.Contains("không thể");

            bool isWarning = _icon == MessageBoxIcon.Warning || _icon == MessageBoxIcon.Exclamation ||
                             capLower.Contains("cảnh báo") || capLower.Contains("không") || capLower.Contains("chú ý");

            bool isQuestion = _icon == MessageBoxIcon.Question || _buttons == MessageBoxButtons.YesNo || _buttons == MessageBoxButtons.YesNoCancel ||
                              capLower.Contains("xác nhận") || capLower.Contains("hỏi");

            Color badgeBg, badgeBorder, iconColor, btnStart, btnEnd;
            string iconChar;
            bool useEmojiFont = false;

            if (isError)
            {
                badgeBg = Color.FromArgb(254, 242, 242);
                badgeBorder = Color.FromArgb(254, 202, 202);
                iconColor = Color.FromArgb(239, 68, 68);
                iconChar = "✕";
                btnStart = Color.FromArgb(239, 68, 68);
                btnEnd = Color.FromArgb(220, 38, 38);
                try { SystemSounds.Hand.Play(); } catch { }
            }
            else if (isWarning)
            {
                badgeBg = Color.FromArgb(254, 243, 199);
                badgeBorder = Color.FromArgb(253, 230, 138);
                iconColor = Color.FromArgb(217, 119, 6);
                iconChar = "⚠️";
                useEmojiFont = true;
                btnStart = Color.FromArgb(245, 158, 11);
                btnEnd = Color.FromArgb(217, 119, 6);
                try { SystemSounds.Exclamation.Play(); } catch { }
            }
            else if (isQuestion)
            {
                badgeBg = Color.FromArgb(238, 242, 255);
                badgeBorder = Color.FromArgb(199, 210, 254);
                iconColor = Color.FromArgb(79, 70, 229);
                iconChar = "❓";
                useEmojiFont = true;
                btnStart = Color.FromArgb(79, 70, 229);
                btnEnd = Color.FromArgb(124, 58, 237);
                try { SystemSounds.Question.Play(); } catch { }
            }
            else
            {
                badgeBg = Color.FromArgb(236, 253, 245);
                badgeBorder = Color.FromArgb(167, 243, 208);
                iconColor = Color.FromArgb(16, 185, 129);
                iconChar = "✓";
                btnStart = Color.FromArgb(16, 185, 129);
                btnEnd = Color.FromArgb(5, 150, 105);
                try { SystemSounds.Asterisk.Play(); } catch { }
            }

            // 1. Icon Badge Panel
            var pnlBadge = new Panel
            {
                Size = new Size(52, 52),
                Location = new Point(24, 24),
                BackColor = badgeBg
            };
            pnlBadge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var r = new Rectangle(1, 1, pnlBadge.Width - 3, pnlBadge.Height - 3);
                using var br = new SolidBrush(badgeBg);
                e.Graphics.FillEllipse(br, r);
                using var pen = new Pen(badgeBorder, 2f);
                e.Graphics.DrawEllipse(pen, r);
            };
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, 52, 52);
                pnlBadge.Region = new Region(gp);
            }

            var lblIcon = new Label
            {
                Text = iconChar,
                Font = useEmojiFont ? new Font("Segoe UI Emoji", 19F) : new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = iconColor,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            pnlBadge.Controls.Add(lblIcon);
            Controls.Add(pnlBadge);

            // 2. Nút Đóng "✕" góc trên cùng bên phải
            var btnClose = new Label
            {
                Text = "✕",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Size = new Size(28, 28),
                Location = new Point(Width - 38, 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            btnClose.MouseEnter += (s, e) => { btnClose.ForeColor = Color.FromArgb(71, 85, 105); btnClose.BackColor = Color.FromArgb(241, 245, 249); };
            btnClose.MouseLeave += (s, e) => { btnClose.ForeColor = Color.FromArgb(148, 163, 184); btnClose.BackColor = Color.Transparent; };
            btnClose.Click += (s, e) =>
            {
                DialogResult = _buttons == MessageBoxButtons.YesNo ? DialogResult.No : DialogResult.Cancel;
                Close();
            };
            Controls.Add(btnClose);

            // 3. Tiêu đề
            var lblTitle = new Label
            {
                Text = _caption,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(90, 22),
                Size = new Size(Width - 90 - 45, 26),
                AutoEllipsis = true
            };
            Controls.Add(lblTitle);

            // 4. Nội dung thông báo (tính toán chiều cao tự động)
            int maxTextWidth = Width - 90 - 28;
            var fontMsg = new Font("Segoe UI", 9.75F);
            Size textSize = TextRenderer.MeasureText(
                _text,
                fontMsg,
                new Size(maxTextWidth, 0),
                TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);

            int textHeight = Math.Max(26, Math.Min(260, textSize.Height + 6));

            Control textControl;
            if (textSize.Height > 260)
            {
                // Thông báo quá dài (e.g. chi tiết lỗi ngoại lệ) -> hiển thị trong Panel cuộn
                var pnlScroll = new Panel
                {
                    Location = new Point(90, 52),
                    Size = new Size(maxTextWidth, 260),
                    AutoScroll = true
                };
                var lblMsg = new Label
                {
                    Text = _text,
                    Font = fontMsg,
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    MaximumSize = new Size(maxTextWidth - 18, 0),
                    Location = new Point(0, 0)
                };
                pnlScroll.Controls.Add(lblMsg);
                textControl = pnlScroll;
            }
            else
            {
                var lblMsg = new Label
                {
                    Text = _text,
                    Font = fontMsg,
                    ForeColor = Color.FromArgb(71, 85, 105),
                    Location = new Point(90, 52),
                    Size = new Size(maxTextWidth, textHeight)
                };
                textControl = lblMsg;
            }
            Controls.Add(textControl);

            // 5. Tính toán chiều cao Form
            int contentBottom = Math.Max(24 + 52, 52 + textHeight);
            int buttonY = contentBottom + 22;
            int formHeight = buttonY + 38 + 20;
            Height = Math.Max(185, formHeight);

            // 6. Thanh phân cách mờ tinh tế phía trên các nút
            var pnlDivider = new Panel
            {
                Location = new Point(24, buttonY - 10),
                Size = new Size(Width - 48, 1),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            Controls.Add(pnlDivider);

            // 7. Tạo các nút bấm tương ứng MessageBoxButtons
            TaoNutBam(buttonY, btnStart, btnEnd);
        }

        private void TaoNutBam(int y, Color btnStart, Color btnEnd)
        {
            switch (_buttons)
            {
                case MessageBoxButtons.YesNo:
                    {
                        var btnYes = TaoNutGradient("Có", btnStart, btnEnd, 95);
                        btnYes.Location = new Point(Width - 24 - 95, y);
                        btnYes.Click += (s, e) => { DialogResult = DialogResult.Yes; Close(); };

                        var btnNo = TaoNutOutline("Không", 95);
                        btnNo.Location = new Point(Width - 24 - 95 - 12 - 95, y);
                        btnNo.Click += (s, e) => { DialogResult = DialogResult.No; Close(); };

                        Controls.Add(btnYes);
                        Controls.Add(btnNo);

                        AcceptButton = btnYes;
                        CancelButton = btnNo;
                        break;
                    }

                case MessageBoxButtons.OKCancel:
                    {
                        var btnOk = TaoNutGradient("Đồng ý", btnStart, btnEnd, 105);
                        btnOk.Location = new Point(Width - 24 - 105, y);
                        btnOk.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };

                        var btnCancel = TaoNutOutline("Hủy", 90);
                        btnCancel.Location = new Point(Width - 24 - 105 - 12 - 90, y);
                        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

                        Controls.Add(btnOk);
                        Controls.Add(btnCancel);

                        AcceptButton = btnOk;
                        CancelButton = btnCancel;
                        break;
                    }

                case MessageBoxButtons.YesNoCancel:
                    {
                        var btnYes = TaoNutGradient("Có", btnStart, btnEnd, 85);
                        btnYes.Location = new Point(Width - 24 - 85, y);
                        btnYes.Click += (s, e) => { DialogResult = DialogResult.Yes; Close(); };

                        var btnNo = TaoNutOutline("Không", 85);
                        btnNo.Location = new Point(Width - 24 - 85 - 10 - 85, y);
                        btnNo.Click += (s, e) => { DialogResult = DialogResult.No; Close(); };

                        var btnCancel = TaoNutOutline("Hủy", 80);
                        btnCancel.Location = new Point(Width - 24 - 85 - 10 - 85 - 10 - 80, y);
                        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

                        Controls.Add(btnYes);
                        Controls.Add(btnNo);
                        Controls.Add(btnCancel);

                        AcceptButton = btnYes;
                        CancelButton = btnCancel;
                        break;
                    }

                case MessageBoxButtons.OK:
                default:
                    {
                        var btnOk = TaoNutGradient("Đồng ý", btnStart, btnEnd, 115);
                        btnOk.Location = new Point(Width - 24 - 115, y);
                        btnOk.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };

                        Controls.Add(btnOk);
                        AcceptButton = btnOk;
                        break;
                    }
            }
        }

        private Button TaoNutGradient(string text, Color start, Color end, int width)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(width, 38),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            bool hover = false;
            btn.MouseEnter += (s, e) => { hover = true; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { hover = false; btn.Invalidate(); };

            btn.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                using var path = RoundRect(rect, 10);

                var c1 = hover ? ControlPaint.Light(start, 0.15f) : start;
                var c2 = hover ? ControlPaint.Light(end, 0.15f) : end;
                using var brush = new LinearGradientBrush(rect, c1, c2, 45f);
                g.FillPath(brush, path);

                TextRenderer.DrawText(g, btn.Text, btn.Font, rect, btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            return btn;
        }

        private Button TaoNutOutline(string text, int width)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(width, 38),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            bool hover = false;
            btn.MouseEnter += (s, e) => { hover = true; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { hover = false; btn.Invalidate(); };

            btn.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                using var path = RoundRect(rect, 10);

                Color bg = hover ? Color.FromArgb(226, 232, 240) : Color.FromArgb(241, 245, 249);
                using var brush = new SolidBrush(bg);
                g.FillPath(brush, path);

                using var pen = new Pen(Color.FromArgb(203, 213, 225), 1f);
                g.DrawPath(pen, path);

                TextRenderer.DrawText(g, btn.Text, btn.Font, rect, btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            return btn;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using var path = RoundRect(new Rectangle(0, 0, Width, Height), 16);
            Region = new Region(path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Viền bo tròn nhẹ nhàng quanh hộp thoại
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = RoundRect(rect, 16);
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1.5f);
            g.DrawPath(pen, path);
        }

        // Cho phép rê chuột kéo hộp thoại di chuyển tự do
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                _mouseOffset = new Point(-e.X, -e.Y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isMouseDown)
            {
                Point mousePos = MousePosition;
                mousePos.Offset(_mouseOffset.X, _mouseOffset.Y);
                Location = mousePos;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left) _isMouseDown = false;
        }
    }
}

