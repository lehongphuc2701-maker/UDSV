namespace UDSV
{
    partial class FrmMonHoc
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            pnlHeader = new UDSV.Controls.GradientPanel();
            lblSub = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            lblIcon = new System.Windows.Forms.Label();
            cardForm = new UDSV.Controls.CardPanel();
            btnLamMoi = new System.Windows.Forms.Button();
            btnXoa = new System.Windows.Forms.Button();
            btnSua = new System.Windows.Forms.Button();
            btnThem = new UDSV.Controls.GradientButton();
            txtSoTinChi = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            txtTenMH = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            txtMaMH = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            lblSectionInfo = new System.Windows.Forms.Label();
            cardData = new UDSV.Controls.CardPanel();
            btnTimKiem = new UDSV.Controls.GradientButton();
            txtTimKiem = new System.Windows.Forms.TextBox();
            lblSearch = new System.Windows.Forms.Label();
            lblThongKe = new System.Windows.Forms.Label();
            lblListTitle = new System.Windows.Forms.Label();
            dgvMonHoc = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            cardForm.SuspendLayout();
            cardData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonHoc).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Angle = 135F;
            pnlHeader.ColorEnd = System.Drawing.Color.FromArgb(16, 185, 129);
            pnlHeader.ColorStart = System.Drawing.Color.FromArgb(5, 150, 105);
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
            lblSub.ForeColor = System.Drawing.Color.FromArgb(209, 250, 229);
            lblSub.Location = new System.Drawing.Point(75, 43);
            lblSub.Name = "lblSub";
            lblSub.Size = new System.Drawing.Size(384, 20);
            lblSub.TabIndex = 2;
            lblSub.Text = "Quản lý danh mục học phần và số tín chỉ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(73, 11);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(394, 35);
            label1.TabIndex = 1;
            label1.Text = "QUẢN LÝ MÔN HỌC & HỌC PHẦN";
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
            lblIcon.Text = "📚";
            // 
            // cardForm
            // 
            cardForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardForm.BackColor = System.Drawing.Color.White;
            cardForm.Controls.Add(btnLamMoi);
            cardForm.Controls.Add(btnXoa);
            cardForm.Controls.Add(btnSua);
            cardForm.Controls.Add(btnThem);
            cardForm.Controls.Add(txtSoTinChi);
            cardForm.Controls.Add(label4);
            cardForm.Controls.Add(txtTenMH);
            cardForm.Controls.Add(label3);
            cardForm.Controls.Add(txtMaMH);
            cardForm.Controls.Add(label2);
            cardForm.Controls.Add(lblSectionInfo);
            cardForm.Location = new System.Drawing.Point(25, 90);
            cardForm.Name = "cardForm";
            cardForm.Radius = 14;
            cardForm.Size = new System.Drawing.Size(1110, 168);
            cardForm.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnLamMoi.Location = new System.Drawing.Point(455, 108);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new System.Drawing.Size(125, 40);
            btnLamMoi.TabIndex = 12;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnXoa.ForeColor = System.Drawing.Color.White;
            btnXoa.Location = new System.Drawing.Point(318, 108);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new System.Drawing.Size(125, 40);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "🗑️ Xóa môn";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnSua.ForeColor = System.Drawing.Color.White;
            btnSua.Location = new System.Drawing.Point(180, 108);
            btnSua.Name = "btnSua";
            btnSua.Size = new System.Drawing.Size(125, 40);
            btnSua.TabIndex = 10;
            btnSua.Text = "✏️ Cập nhật";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            btnThem.BackColor = System.Drawing.Color.Transparent;
            btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btnThem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnThem.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Bold);
            btnThem.ForeColor = System.Drawing.Color.White;
            btnThem.GradientEnd = System.Drawing.Color.FromArgb(16, 185, 129);
            btnThem.GradientStart = System.Drawing.Color.FromArgb(5, 150, 105);
            btnThem.Location = new System.Drawing.Point(25, 108);
            btnThem.Name = "btnThem";
            btnThem.Radius = 12;
            btnThem.Size = new System.Drawing.Size(142, 40);
            btnThem.TabIndex = 9;
            btnThem.Text = "＋ Thêm mới";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // txtSoTinChi
            // 
            txtSoTinChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtSoTinChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtSoTinChi.Location = new System.Drawing.Point(590, 60);
            txtSoTinChi.Name = "txtSoTinChi";
            txtSoTinChi.PlaceholderText = "Ví dụ: 3";
            txtSoTinChi.Size = new System.Drawing.Size(160, 30);
            txtSoTinChi.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label4.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label4.Location = new System.Drawing.Point(590, 37);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(147, 20);
            label4.TabIndex = 5;
            label4.Text = "Số tín chỉ (1 - 10) *";
            // 
            // txtTenMH
            // 
            txtTenMH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTenMH.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtTenMH.Location = new System.Drawing.Point(225, 60);
            txtTenMH.Name = "txtTenMH";
            txtTenMH.PlaceholderText = "Ví dụ: Lập trình C#";
            txtTenMH.Size = new System.Drawing.Size(345, 30);
            txtTenMH.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label3.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label3.Location = new System.Drawing.Point(225, 37);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(111, 20);
            label3.TabIndex = 3;
            label3.Text = "Tên môn học *";
            // 
            // txtMaMH
            // 
            txtMaMH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtMaMH.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtMaMH.Location = new System.Drawing.Point(25, 60);
            txtMaMH.Name = "txtMaMH";
            txtMaMH.PlaceholderText = "Ví dụ: IT101";
            txtMaMH.Size = new System.Drawing.Size(180, 30);
            txtMaMH.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label2.Location = new System.Drawing.Point(25, 37);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(108, 20);
            label2.TabIndex = 1;
            label2.Text = "Mã môn học *";
            // 
            // lblSectionInfo
            // 
            lblSectionInfo.AutoSize = true;
            lblSectionInfo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblSectionInfo.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblSectionInfo.Location = new System.Drawing.Point(22, 12);
            lblSectionInfo.Name = "lblSectionInfo";
            lblSectionInfo.Size = new System.Drawing.Size(217, 25);
            lblSectionInfo.TabIndex = 0;
            lblSectionInfo.Text = "THÔNG TIN MÔN HỌC";
            // 
            // cardData
            // 
            cardData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardData.BackColor = System.Drawing.Color.White;
            cardData.Controls.Add(btnTimKiem);
            cardData.Controls.Add(txtTimKiem);
            cardData.Controls.Add(lblSearch);
            cardData.Controls.Add(lblThongKe);
            cardData.Controls.Add(lblListTitle);
            cardData.Controls.Add(dgvMonHoc);
            cardData.Location = new System.Drawing.Point(25, 272);
            cardData.Name = "cardData";
            cardData.Radius = 14;
            cardData.Size = new System.Drawing.Size(1110, 400);
            cardData.TabIndex = 2;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnTimKiem.BackColor = System.Drawing.Color.Transparent;
            btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btnTimKiem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnTimKiem.ForeColor = System.Drawing.Color.White;
            btnTimKiem.GradientEnd = System.Drawing.Color.FromArgb(16, 185, 129);
            btnTimKiem.GradientStart = System.Drawing.Color.FromArgb(5, 150, 105);
            btnTimKiem.Location = new System.Drawing.Point(985, 13);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Radius = 10;
            btnTimKiem.Size = new System.Drawing.Size(100, 34);
            btnTimKiem.TabIndex = 5;
            btnTimKiem.Text = "🔍 Tìm";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtTimKiem.Location = new System.Drawing.Point(705, 15);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập mã hoặc tên môn để lọc...";
            txtTimKiem.Size = new System.Drawing.Size(270, 30);
            txtTimKiem.TabIndex = 4;
            // 
            // lblSearch
            // 
            lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblSearch.AutoSize = true;
            lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblSearch.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSearch.Location = new System.Drawing.Point(625, 19);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(76, 20);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // lblThongKe
            // 
            lblThongKe.AutoSize = true;
            lblThongKe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblThongKe.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            lblThongKe.Location = new System.Drawing.Point(235, 18);
            lblThongKe.Name = "lblThongKe";
            lblThongKe.Size = new System.Drawing.Size(205, 21);
            lblThongKe.TabIndex = 2;
            lblThongKe.Text = "📊 Tổng: 0 môn | 0 tín chỉ";
            // 
            // lblListTitle
            // 
            lblListTitle.AutoSize = true;
            lblListTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblListTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblListTitle.Location = new System.Drawing.Point(22, 16);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Size = new System.Drawing.Size(207, 25);
            lblListTitle.TabIndex = 1;
            lblListTitle.Text = "DANH SÁCH MÔN HỌC";
            // 
            // dgvMonHoc
            // 
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.AllowUserToDeleteRows = false;
            dgvMonHoc.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvMonHoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvMonHoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvMonHoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvMonHoc.BackgroundColor = System.Drawing.Color.White;
            dgvMonHoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvMonHoc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMonHoc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvMonHoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMonHoc.ColumnHeadersHeight = 40;
            dgvMonHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(45, 52, 54);
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(209, 250, 229);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(6, 78, 59);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvMonHoc.DefaultCellStyle = dataGridViewCellStyle3;
            dgvMonHoc.EnableHeadersVisualStyles = false;
            dgvMonHoc.GridColor = System.Drawing.Color.FromArgb(238, 242, 246);
            dgvMonHoc.Location = new System.Drawing.Point(20, 56);
            dgvMonHoc.MultiSelect = false;
            dgvMonHoc.Name = "dgvMonHoc";
            dgvMonHoc.ReadOnly = true;
            dgvMonHoc.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvMonHoc.RowHeadersVisible = false;
            dgvMonHoc.RowHeadersWidth = 51;
            dgvMonHoc.RowTemplate.Height = 36;
            dgvMonHoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvMonHoc.Size = new System.Drawing.Size(1070, 325);
            dgvMonHoc.TabIndex = 0;
            // 
            // FrmMonHoc
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1160, 690);
            Controls.Add(cardData);
            Controls.Add(cardForm);
            Controls.Add(pnlHeader);
            MinimumSize = new System.Drawing.Size(1050, 620);
            Name = "FrmMonHoc";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quản lý Môn học & Học phần";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            cardForm.ResumeLayout(false);
            cardForm.PerformLayout();
            cardData.ResumeLayout(false);
            cardData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonHoc).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.GradientPanel pnlHeader;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSub;
        private Controls.CardPanel cardForm;
        private System.Windows.Forms.Label lblSectionInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaMH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenMH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSoTinChi;
        private Controls.GradientButton btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private Controls.CardPanel cardData;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblThongKe;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
        private Controls.GradientButton btnTimKiem;
        private System.Windows.Forms.DataGridView dgvMonHoc;
    }
}
