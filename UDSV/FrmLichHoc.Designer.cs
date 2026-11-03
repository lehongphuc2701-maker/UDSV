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
            pnlTuan = new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(340, 9);
            label1.Name = "label1";
            label1.Size = new Size(380, 41);
            label1.TabIndex = 0;
            label1.Text = "THỜI KHOÁ BIỂU THEO TUẦN";
            // 
            // labelKhoa
            // 
            labelKhoa.AutoSize = true;
            labelKhoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelKhoa.ForeColor = SystemColors.Highlight;
            labelKhoa.Location = new Point(30, 72);
            labelKhoa.Name = "labelKhoa";
            labelKhoa.Size = new Size(50, 23);
            labelKhoa.Text = "Khóa";
            // 
            // cboKhoa
            // 
            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoa.Font = new Font("Segoe UI", 10F);
            cboKhoa.Location = new Point(95, 68);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new Size(160, 31);
            cboKhoa.TabIndex = 1;
            // 
            // labelKy
            // 
            labelKy.AutoSize = true;
            labelKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelKy.ForeColor = SystemColors.Highlight;
            labelKy.Location = new Point(285, 72);
            labelKy.Name = "labelKy";
            labelKy.Size = new Size(31, 23);
            labelKy.Text = "Kỳ";
            // 
            // cboKy
            // 
            cboKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKy.Font = new Font("Segoe UI", 10F);
            cboKy.Location = new Point(325, 68);
            cboKy.Name = "cboKy";
            cboKy.Size = new Size(110, 31);
            cboKy.TabIndex = 2;
            // 
            // pnlTuan
            // 
            pnlTuan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTuan.BackColor = Color.White;
            pnlTuan.BorderStyle = BorderStyle.FixedSingle;
            pnlTuan.Location = new Point(30, 110);
            pnlTuan.Name = "pnlTuan";
            pnlTuan.Size = new Size(980, 560);
            pnlTuan.TabIndex = 3;
            // 
            // FrmLichHoc
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 700);
            Controls.Add(pnlTuan);
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
        private Panel pnlTuan;
    }
}
