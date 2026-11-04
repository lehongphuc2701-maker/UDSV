namespace UDSV
{
    partial class FrmLichHoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new UDSV.Controls.GradientPanel();
            lblSub = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            lblIcon = new System.Windows.Forms.Label();
            cardFilter = new UDSV.Controls.CardPanel();
            lblGhiChu = new System.Windows.Forms.Label();
            lblThongTin = new System.Windows.Forms.Label();
            cboKy = new System.Windows.Forms.ComboBox();
            labelKy = new System.Windows.Forms.Label();
            cboKhoa = new System.Windows.Forms.ComboBox();
            labelKhoa = new System.Windows.Forms.Label();
            cardSchedule = new UDSV.Controls.CardPanel();
            pnlTuan = new System.Windows.Forms.Panel();
            pnlHeader.SuspendLayout();
            cardFilter.SuspendLayout();
            cardSchedule.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Angle = 135F;
            pnlHeader.ColorEnd = System.Drawing.Color.FromArgb(245, 158, 11);
            pnlHeader.ColorStart = System.Drawing.Color.FromArgb(217, 119, 6);
            pnlHeader.Controls.Add(lblSub);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Controls.Add(lblIcon);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(1160, 75);
            pnlHeader.TabIndex = 0;
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.BackColor = System.Drawing.Color.Transparent;
            lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblSub.ForeColor = System.Drawing.Color.FromArgb(254, 243, 199);
            lblSub.Location = new System.Drawing.Point(75, 43);
            lblSub.Name = "lblSub";
            lblSub.Size = new System.Drawing.Size(377, 20);
            lblSub.TabIndex = 2;
            lblSub.Text = "Thời khóa biểu tuần mẫu theo Khóa đào tạo và Học kỳ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(73, 11);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(383, 35);
            label1.TabIndex = 1;
            label1.Text = "THỜI KHÓA BIỂU THEO TUẦN";
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = true;
            lblIcon.BackColor = System.Drawing.Color.Transparent;
            lblIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            lblIcon.ForeColor = System.Drawing.Color.White;
            lblIcon.Location = new System.Drawing.Point(18, 10);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new System.Drawing.Size(61, 54);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "📅";
            // 
            // cardFilter
            // 
            cardFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardFilter.BackColor = System.Drawing.Color.White;
            cardFilter.Controls.Add(lblGhiChu);
            cardFilter.Controls.Add(lblThongTin);
            cardFilter.Controls.Add(cboKy);
            cardFilter.Controls.Add(labelKy);
            cardFilter.Controls.Add(cboKhoa);
            cardFilter.Controls.Add(labelKhoa);
            cardFilter.Location = new System.Drawing.Point(25, 90);
            cardFilter.Name = "cardFilter";
            cardFilter.Radius = 14;
            cardFilter.Size = new System.Drawing.Size(1110, 72);
            cardFilter.TabIndex = 1;
            // 
            // lblGhiChu
            // 
            lblGhiChu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblGhiChu.AutoSize = true;
            lblGhiChu.BackColor = System.Drawing.Color.Transparent;
            lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblGhiChu.Location = new System.Drawing.Point(855, 26);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new System.Drawing.Size(232, 20);
            lblGhiChu.TabIndex = 5;
            lblGhiChu.Text = "💡 Quy chuẩn: 12 tiết học / ngày";
            // 
            // lblThongTin
            // 
            lblThongTin.AutoSize = true;
            lblThongTin.BackColor = System.Drawing.Color.Transparent;
            lblThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblThongTin.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6);
            lblThongTin.Location = new System.Drawing.Point(520, 25);
            lblThongTin.Name = "lblThongTin";
            lblThongTin.Size = new System.Drawing.Size(235, 21);
            lblThongTin.TabIndex = 4;
            lblThongTin.Text = "Chọn Khóa và Kỳ để xem lịch";
            // 
            // cboKy
            // 
            cboKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            cboKy.FormattingEnabled = true;
            cboKy.Location = new System.Drawing.Point(375, 21);
            cboKy.Name = "cboKy";
            cboKy.Size = new System.Drawing.Size(120, 31);
            cboKy.TabIndex = 3;
            // 
            // labelKy
            // 
            labelKy.AutoSize = true;
            labelKy.BackColor = System.Drawing.Color.Transparent;
            labelKy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelKy.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelKy.Location = new System.Drawing.Point(295, 25);
            labelKy.Name = "labelKy";
            labelKy.Size = new System.Drawing.Size(76, 21);
            labelKy.TabIndex = 2;
            labelKy.Text = "🎓 Học kỳ:";
            // 
            // cboKhoa
            // 
            cboKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            cboKhoa.FormattingEnabled = true;
            cboKhoa.Location = new System.Drawing.Point(115, 21);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new System.Drawing.Size(160, 31);
            cboKhoa.TabIndex = 1;
            // 
            // labelKhoa
            // 
            labelKhoa.AutoSize = true;
            labelKhoa.BackColor = System.Drawing.Color.Transparent;
            labelKhoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelKhoa.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelKhoa.Location = new System.Drawing.Point(20, 25);
            labelKhoa.Name = "labelKhoa";
            labelKhoa.Size = new System.Drawing.Size(91, 21);
            labelKhoa.TabIndex = 0;
            labelKhoa.Text = "🏛️ Khóa học:";
            // 
            // cardSchedule
            // 
            cardSchedule.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardSchedule.BackColor = System.Drawing.Color.White;
            cardSchedule.Controls.Add(pnlTuan);
            cardSchedule.Location = new System.Drawing.Point(25, 175);
            cardSchedule.Name = "cardSchedule";
            cardSchedule.Radius = 14;
            cardSchedule.Size = new System.Drawing.Size(1110, 500);
            cardSchedule.TabIndex = 2;
            // 
            // pnlTuan
            // 
            pnlTuan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlTuan.BackColor = System.Drawing.Color.White;
            pnlTuan.Location = new System.Drawing.Point(12, 12);
            pnlTuan.Name = "pnlTuan";
            pnlTuan.Size = new System.Drawing.Size(1086, 476);
            pnlTuan.TabIndex = 0;
            // 
            // FrmLichHoc
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1160, 695);
            Controls.Add(cardSchedule);
            Controls.Add(cardFilter);
            Controls.Add(pnlHeader);
            MinimumSize = new System.Drawing.Size(1060, 620);
            Name = "FrmLichHoc";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Thời khoá biểu theo tuần";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            cardFilter.ResumeLayout(false);
            cardFilter.PerformLayout();
            cardSchedule.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls.GradientPanel pnlHeader;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSub;
        private Controls.CardPanel cardFilter;
        private System.Windows.Forms.Label labelKhoa;
        private System.Windows.Forms.ComboBox cboKhoa;
        private System.Windows.Forms.Label labelKy;
        private System.Windows.Forms.ComboBox cboKy;
        private System.Windows.Forms.Label lblThongTin;
        private System.Windows.Forms.Label lblGhiChu;
        private Controls.CardPanel cardSchedule;
        private System.Windows.Forms.Panel pnlTuan;
    }
}