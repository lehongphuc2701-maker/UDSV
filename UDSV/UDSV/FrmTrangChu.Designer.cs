namespace UDSV
{
    partial class FrmTrangChu
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTrangChu));
            label1 = new Label();
            groupBox1 = new GroupBox();
            btnDangXuat = new Button();
            btnLichHoc = new Button();
            btnMonHoc = new Button();
            btnSinhVien = new Button();
            imageList1 = new ImageList(components);
            imageList2 = new ImageList(components);
            pictureBox1 = new PictureBox();
            lblXinChao = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(401, 9);
            label1.Name = "label1";
            label1.Size = new Size(470, 41);
            label1.TabIndex = 0;
            label1.Text = "Hệ thống quản lí phòng đào tạo";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDangXuat);
            groupBox1.Controls.Add(btnLichHoc);
            groupBox1.Controls.Add(btnMonHoc);
            groupBox1.Controls.Add(btnSinhVien);
            groupBox1.ForeColor = SystemColors.HotTrack;
            groupBox1.Location = new Point(9, 69);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(269, 410);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Bảng điều khiển";
            // 
            // btnDangXuat
            // 
            btnDangXuat.BackColor = SystemColors.ButtonShadow;
            btnDangXuat.Location = new Point(6, 342);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(231, 34);
            btnDangXuat.TabIndex = 3;
            btnDangXuat.Text = "ĐĂNG XUẤT";
            btnDangXuat.UseVisualStyleBackColor = false;
            // 
            // btnLichHoc
            // 
            btnLichHoc.BackColor = Color.Red;
            btnLichHoc.Location = new Point(6, 232);
            btnLichHoc.Name = "btnLichHoc";
            btnLichHoc.Size = new Size(231, 35);
            btnLichHoc.TabIndex = 2;
            btnLichHoc.Text = "3. QUẢN LÝ LỊCH HỌC";
            btnLichHoc.UseVisualStyleBackColor = false;
            // 
            // btnMonHoc
            // 
            btnMonHoc.BackColor = Color.DarkOrange;
            btnMonHoc.Location = new Point(2, 134);
            btnMonHoc.Name = "btnMonHoc";
            btnMonHoc.Size = new Size(235, 39);
            btnMonHoc.TabIndex = 1;
            btnMonHoc.Text = "2. QUẢN LÝ MÔN HỌC";
            btnMonHoc.UseVisualStyleBackColor = false;
            // 
            // btnSinhVien
            // 
            btnSinhVien.BackColor = Color.LawnGreen;
            btnSinhVien.Location = new Point(6, 36);
            btnSinhVien.Name = "btnSinhVien";
            btnSinhVien.Size = new Size(236, 41);
            btnSinhVien.TabIndex = 0;
            btnSinhVien.Text = "1. QUẢN LÝ SINH VIÊN";
            btnSinhVien.UseVisualStyleBackColor = false;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // imageList2
            // 
            imageList2.ColorDepth = ColorDepth.Depth32Bit;
            imageList2.ImageSize = new Size(16, 16);
            imageList2.TransparentColor = Color.Transparent;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(466, 69);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(586, 450);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // lblXinChao
            // 
            lblXinChao.AutoSize = true;
            lblXinChao.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblXinChao.ForeColor = SystemColors.Highlight;
            lblXinChao.Location = new Point(35, 15);
            lblXinChao.Name = "lblXinChao";
            lblXinChao.Size = new Size(101, 25);
            lblXinChao.TabIndex = 3;
            lblXinChao.Text = "Xin chào,..";
            // 
            // FrmTrangChu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1256, 599);
            Controls.Add(lblXinChao);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "FrmTrangChu";
            Text = "FrmTrangChu";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Button btnDangXuat;
        private Button btnLichHoc;
        private Button btnMonHoc;
        private Button btnSinhVien;
        private ImageList imageList1;
        private ImageList imageList2;
        private PictureBox pictureBox1;
        private Label lblXinChao;
    }
}