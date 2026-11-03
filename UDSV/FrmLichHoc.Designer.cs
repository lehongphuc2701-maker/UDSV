namespace UDSV
{
    partial class FrmLichHoc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            labelKhoa = new Label();
            cboKhoa = new ComboBox();
            labelKy = new Label();
            cboKy = new ComboBox();
            btnTruoc = new Button();
            lblTuan = new Label();
            btnSau = new Button();
            btnTuanNay = new Button();
            pnlTuan = new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(400, 9);
            label1.Name = "label1";
            label1.Size = new Size(380, 41);
            label1.TabIndex = 0;
            label1.Text = "THỜI KHOÁ BIỂU THEO TUẦN";
            // 
            // labelKhoa
            // 
            labelKhoa.AutoSize = true;
            labelKhoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelKhoa.ForeColor = SystemColors.Highlight;
            labelKhoa.Location = new Point(30, 72);
            labelKhoa.Name = "labelKhoa";
            labelKhoa.Size = new Size(44, 20);
            labelKhoa.Text = "Khóa";
            // 
            // cboKhoa
            // 
            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoa.Location = new Point(85, 68);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new Size(140, 28);
            cboKhoa.TabIndex = 1;
            // 
            // labelKy
            // 
            labelKy.AutoSize = true;
            labelKy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelKy.ForeColor = SystemColors.Highlight;
            labelKy.Location = new Point(245, 72);
            labelKy.Name = "labelKy";
            labelKy.Size = new Size(28, 20);
            labelKy.Text = "Kỳ";
            // 
            // cboKy
            // 
            cboKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKy.Location = new Point(285, 68);
            cboKy.Name = "cboKy";
            cboKy.Size = new Size(100, 28);
            cboKy.TabIndex = 2;
            // 
            // btnTruoc
            // 
            btnTruoc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTruoc.Location = new Point(430, 67);
            btnTruoc.Name = "btnTruoc";
            btnTruoc.Size = new Size(45, 30);
            btnTruoc.TabIndex = 3;
            btnTruoc.Text = "◀";
            btnTruoc.UseVisualStyleBackColor = true;
            // 
            // lblTuan
            // 
            lblTuan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTuan.ForeColor = Color.DarkGreen;
            lblTuan.Location = new Point(485, 67);
            lblTuan.Name = "lblTuan";
            lblTuan.Size = new Size(330, 30);
            lblTuan.TabIndex = 4;
            lblTuan.Text = "Tuần —";
            lblTuan.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSau
            // 
            btnSau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSau.Location = new Point(825, 67);
            btnSau.Name = "btnSau";
            btnSau.Size = new Size(45, 30);
            btnSau.TabIndex = 5;
            btnSau.Text = "▶";
            btnSau.UseVisualStyleBackColor = true;
            // 
            // btnTuanNay
            // 
            btnTuanNay.Location = new Point(890, 67);
            btnTuanNay.Name = "btnTuanNay";
            btnTuanNay.Size = new Size(120, 30);
            btnTuanNay.TabIndex = 6;
            btnTuanNay.Text = "Tuần này";
            btnTuanNay.UseVisualStyleBackColor = true;
            // 
            // pnlTuan
            // 
            pnlTuan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTuan.BackColor = Color.White;
            pnlTuan.BorderStyle = BorderStyle.FixedSingle;
            pnlTuan.Location = new Point(30, 110);
            pnlTuan.Name = "pnlTuan";
            pnlTuan.Size = new Size(980, 560);
            pnlTuan.TabIndex = 7;
            // 
            // FrmLichHoc
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 700);
            Controls.Add(pnlTuan);
            Controls.Add(btnTuanNay);
            Controls.Add(btnSau);
            Controls.Add(lblTuan);
            Controls.Add(btnTruoc);
            Controls.Add(cboKy);
            Controls.Add(labelKy);
            Controls.Add(cboKhoa);
            Controls.Add(labelKhoa);
            Controls.Add(label1);
            Name = "FrmLichHoc";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thời khoá biểu theo tuần";
            SuspendLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label labelKhoa, labelKy;
        private ComboBox cboKhoa, cboKy;
        private Button btnTruoc, btnSau, btnTuanNay;
        private Label lblTuan;
        private Panel pnlTuan;
    }
}
