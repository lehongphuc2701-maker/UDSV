namespace UDSV
{
    partial class FrmKetQua
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            pnlHeader = new UDSV.Controls.GradientPanel();
            lblSub = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            lblIcon = new System.Windows.Forms.Label();
            cardFilter = new UDSV.Controls.CardPanel();
            btnLamMoi = new System.Windows.Forms.Button();
            btnLuu = new UDSV.Controls.GradientButton();
            cboMonHoc = new System.Windows.Forms.ComboBox();
            labelMon = new System.Windows.Forms.Label();
            cboKy = new System.Windows.Forms.ComboBox();
            labelKy = new System.Windows.Forms.Label();
            cboKhoa = new System.Windows.Forms.ComboBox();
            labelKhoa = new System.Windows.Forms.Label();
            cardData = new UDSV.Controls.CardPanel();
            lblHuongDan = new System.Windows.Forms.Label();
            lblThongTin = new System.Windows.Forms.Label();
            lblTableTitle = new System.Windows.Forms.Label();
            dgvBangDiem = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            cardFilter.SuspendLayout();
            cardData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBangDiem).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Angle = 135F;
            pnlHeader.ColorEnd = System.Drawing.Color.FromArgb(244, 63, 94);
            pnlHeader.ColorStart = System.Drawing.Color.FromArgb(225, 29, 72);
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
            lblSub.ForeColor = System.Drawing.Color.FromArgb(255, 228, 230);
            lblSub.Location = new System.Drawing.Point(75, 43);
            lblSub.Name = "lblSub";
            lblSub.Size = new System.Drawing.Size(420, 20);
            lblSub.TabIndex = 2;
            lblSub.Text = "Nhập và quản lý kết quả học tập theo Khóa, Kỳ và Học phần";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(73, 11);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(359, 35);
            label1.TabIndex = 1;
            label1.Text = "QUẢN LÝ KẾT QUẢ HỌC TẬP";
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
            lblIcon.Text = "📊";
            // 
            // cardFilter
            // 
            cardFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardFilter.BackColor = System.Drawing.Color.White;
            cardFilter.Controls.Add(btnLamMoi);
            cardFilter.Controls.Add(btnLuu);
            cardFilter.Controls.Add(cboMonHoc);
            cardFilter.Controls.Add(labelMon);
            cardFilter.Controls.Add(cboKy);
            cardFilter.Controls.Add(labelKy);
            cardFilter.Controls.Add(cboKhoa);
            cardFilter.Controls.Add(labelKhoa);
            cardFilter.Location = new System.Drawing.Point(25, 90);
            cardFilter.Name = "cardFilter";
            cardFilter.Radius = 14;
            cardFilter.Size = new System.Drawing.Size(1110, 78);
            cardFilter.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnLamMoi.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnLamMoi.Location = new System.Drawing.Point(990, 20);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new System.Drawing.Size(102, 38);
            btnLamMoi.TabIndex = 7;
            btnLamMoi.Text = "🔄 Tải lại";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnLuu
            // 
            btnLuu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnLuu.BackColor = System.Drawing.Color.Transparent;
            btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btnLuu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Bold);
            btnLuu.ForeColor = System.Drawing.Color.White;
            btnLuu.GradientEnd = System.Drawing.Color.FromArgb(16, 185, 129);
            btnLuu.GradientStart = System.Drawing.Color.FromArgb(5, 150, 105);
            btnLuu.Location = new System.Drawing.Point(842, 20);
            btnLuu.Name = "btnLuu";
            btnLuu.Radius = 12;
            btnLuu.Size = new System.Drawing.Size(140, 38);
            btnLuu.TabIndex = 6;
            btnLuu.Text = "💾 Lưu điểm";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // cboMonHoc
            // 
            cboMonHoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cboMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboMonHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            cboMonHoc.FormattingEnabled = true;
            cboMonHoc.Location = new System.Drawing.Point(520, 24);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new System.Drawing.Size(305, 31);
            cboMonHoc.TabIndex = 5;
            // 
            // labelMon
            // 
            labelMon.AutoSize = true;
            labelMon.BackColor = System.Drawing.Color.Transparent;
            labelMon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelMon.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelMon.Location = new System.Drawing.Point(432, 28);
            labelMon.Name = "labelMon";
            labelMon.Size = new System.Drawing.Size(86, 21);
            labelMon.TabIndex = 4;
            labelMon.Text = "📚 Môn học:";
            // 
            // cboKy
            // 
            cboKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            cboKy.FormattingEnabled = true;
            cboKy.Location = new System.Drawing.Point(320, 24);
            cboKy.Name = "cboKy";
            cboKy.Size = new System.Drawing.Size(95, 31);
            cboKy.TabIndex = 3;
            // 
            // labelKy
            // 
            labelKy.AutoSize = true;
            labelKy.BackColor = System.Drawing.Color.Transparent;
            labelKy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelKy.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelKy.Location = new System.Drawing.Point(245, 28);
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
            cboKhoa.Location = new System.Drawing.Point(100, 24);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new System.Drawing.Size(130, 31);
            cboKhoa.TabIndex = 1;
            // 
            // labelKhoa
            // 
            labelKhoa.AutoSize = true;
            labelKhoa.BackColor = System.Drawing.Color.Transparent;
            labelKhoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelKhoa.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelKhoa.Location = new System.Drawing.Point(15, 28);
            labelKhoa.Name = "labelKhoa";
            labelKhoa.Size = new System.Drawing.Size(83, 21);
            labelKhoa.TabIndex = 0;
            labelKhoa.Text = "🏛️ Khóa:";
            // 
            // cardData
            // 
            cardData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardData.BackColor = System.Drawing.Color.White;
            cardData.Controls.Add(lblHuongDan);
            cardData.Controls.Add(lblThongTin);
            cardData.Controls.Add(lblTableTitle);
            cardData.Controls.Add(dgvBangDiem);
            cardData.Location = new System.Drawing.Point(25, 180);
            cardData.Name = "cardData";
            cardData.Radius = 14;
            cardData.Size = new System.Drawing.Size(1110, 495);
            cardData.TabIndex = 2;
            // 
            // lblHuongDan
            // 
            lblHuongDan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblHuongDan.AutoSize = true;
            lblHuongDan.BackColor = System.Drawing.Color.Transparent;
            lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            lblHuongDan.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblHuongDan.Location = new System.Drawing.Point(620, 18);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new System.Drawing.Size(470, 20);
            lblHuongDan.TabIndex = 3;
            lblHuongDan.Text = "💡 Nhập trực tiếp điểm vào cột 'Điểm' (0.0 - 10.0), ô trống = chưa có điểm";
            // 
            // lblThongTin
            // 
            lblThongTin.AutoSize = true;
            lblThongTin.BackColor = System.Drawing.Color.Transparent;
            lblThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblThongTin.ForeColor = System.Drawing.Color.FromArgb(225, 29, 72);
            lblThongTin.Location = new System.Drawing.Point(225, 17);
            lblThongTin.Name = "lblThongTin";
            lblThongTin.Size = new System.Drawing.Size(325, 21);
            lblThongTin.TabIndex = 2;
            lblThongTin.Text = "Chọn khóa → kỳ → môn học để nhập điểm";
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblTableTitle.Location = new System.Drawing.Point(20, 15);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(193, 25);
            lblTableTitle.TabIndex = 1;
            lblTableTitle.Text = "BẢNG ĐIỂM CHI TIẾT";
            // 
            // dgvBangDiem
            // 
            dgvBangDiem.AllowUserToAddRows = false;
            dgvBangDiem.AllowUserToDeleteRows = false;
            dgvBangDiem.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(255, 241, 242);
            dgvBangDiem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvBangDiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvBangDiem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvBangDiem.BackgroundColor = System.Drawing.Color.White;
            dgvBangDiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvBangDiem.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBangDiem.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(225, 29, 72);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(225, 29, 72);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvBangDiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBangDiem.ColumnHeadersHeight = 40;
            dgvBangDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(45, 52, 54);
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(254, 205, 211);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(136, 19, 55);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvBangDiem.DefaultCellStyle = dataGridViewCellStyle3;
            dgvBangDiem.EnableHeadersVisualStyles = false;
            dgvBangDiem.GridColor = System.Drawing.Color.FromArgb(238, 242, 246);
            dgvBangDiem.Location = new System.Drawing.Point(18, 52);
            dgvBangDiem.Name = "dgvBangDiem";
            dgvBangDiem.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvBangDiem.RowHeadersVisible = false;
            dgvBangDiem.RowHeadersWidth = 51;
            dgvBangDiem.RowTemplate.Height = 36;
            dgvBangDiem.Size = new System.Drawing.Size(1072, 425);
            dgvBangDiem.TabIndex = 0;
            // 
            // FrmKetQua
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1160, 695);
            Controls.Add(cardData);
            Controls.Add(cardFilter);
            Controls.Add(pnlHeader);
            MinimumSize = new System.Drawing.Size(1050, 620);
            Name = "FrmKetQua";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quản lý kết quả học tập & Nhập điểm";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            cardFilter.ResumeLayout(false);
            cardFilter.PerformLayout();
            cardData.ResumeLayout(false);
            cardData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBangDiem).EndInit();
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
        private System.Windows.Forms.Label labelMon;
        private System.Windows.Forms.ComboBox cboMonHoc;
        private Controls.GradientButton btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private Controls.CardPanel cardData;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.Label lblThongTin;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.DataGridView dgvBangDiem;
    }
}