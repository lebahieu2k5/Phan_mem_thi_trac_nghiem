namespace TaoDeThi
{
    partial class DeThi2
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_CauHoi2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_Them = new System.Windows.Forms.Button();
            this.button_Sua = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ND = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_DapanA = new System.Windows.Forms.TextBox();
            this.textBox_DapAnB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_DapAnC = new System.Windows.Forms.TextBox();
            this.textBox_DapAnD = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_DapAnDung = new System.Windows.Forms.TextBox();
            this.button_QuayLai = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button_Luu = new System.Windows.Forms.Button();
            this.comboBox_DeThiID = new System.Windows.Forms.ComboBox();
            this.textBox_MonID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(255, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(303, 53);
            this.button2.TabIndex = 1;
            this.button2.Text = "Tạo đề thi từ ngân hàng chung";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(628, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(233, 53);
            this.button1.TabIndex = 2;
            this.button1.Text = "Tạo đề thi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(460, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Môn Thi ";
            // 
            // textBox_CauHoi2
            // 
            this.textBox_CauHoi2.Location = new System.Drawing.Point(257, 610);
            this.textBox_CauHoi2.Name = "textBox_CauHoi2";
            this.textBox_CauHoi2.Size = new System.Drawing.Size(137, 22);
            this.textBox_CauHoi2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(142, 608);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Câu hỏi ID:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(140, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(907, 321);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Các Câu Hỏi";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(895, 294);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button_Them
            // 
            this.button_Them.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Them.Location = new System.Drawing.Point(1078, 190);
            this.button_Them.Name = "button_Them";
            this.button_Them.Size = new System.Drawing.Size(104, 53);
            this.button_Them.TabIndex = 11;
            this.button_Them.Text = "Thêm";
            this.button_Them.UseVisualStyleBackColor = true;
            this.button_Them.Click += new System.EventHandler(this.button_Them_Click);
            // 
            // button_Sua
            // 
            this.button_Sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Sua.Location = new System.Drawing.Point(1078, 273);
            this.button_Sua.Name = "button_Sua";
            this.button_Sua.Size = new System.Drawing.Size(104, 53);
            this.button_Sua.TabIndex = 11;
            this.button_Sua.Text = "Sửa";
            this.button_Sua.UseVisualStyleBackColor = true;
            this.button_Sua.Click += new System.EventHandler(this.button_Sua_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(1078, 365);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 53);
            this.button5.TabIndex = 11;
            this.button5.Text = "Xóa";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(176, 473);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nội dung câu hỏi";
            // 
            // textBox_ND
            // 
            this.textBox_ND.Location = new System.Drawing.Point(341, 473);
            this.textBox_ND.Multiline = true;
            this.textBox_ND.Name = "textBox_ND";
            this.textBox_ND.Size = new System.Drawing.Size(644, 77);
            this.textBox_ND.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(495, 606);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Đáp án A:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(495, 679);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 22);
            this.label5.TabIndex = 6;
            this.label5.Text = "Đáp án B:";
            // 
            // textBox_DapanA
            // 
            this.textBox_DapanA.Location = new System.Drawing.Point(610, 608);
            this.textBox_DapanA.Name = "textBox_DapanA";
            this.textBox_DapanA.Size = new System.Drawing.Size(137, 22);
            this.textBox_DapanA.TabIndex = 9;
            // 
            // textBox_DapAnB
            // 
            this.textBox_DapAnB.Location = new System.Drawing.Point(610, 681);
            this.textBox_DapAnB.Name = "textBox_DapAnB";
            this.textBox_DapAnB.Size = new System.Drawing.Size(137, 22);
            this.textBox_DapAnB.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(807, 606);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 22);
            this.label6.TabIndex = 6;
            this.label6.Text = "Đáp án C:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(807, 679);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 22);
            this.label7.TabIndex = 6;
            this.label7.Text = "Đáp án D:";
            // 
            // textBox_DapAnC
            // 
            this.textBox_DapAnC.Location = new System.Drawing.Point(922, 608);
            this.textBox_DapAnC.Name = "textBox_DapAnC";
            this.textBox_DapAnC.Size = new System.Drawing.Size(137, 22);
            this.textBox_DapAnC.TabIndex = 9;
            // 
            // textBox_DapAnD
            // 
            this.textBox_DapAnD.Location = new System.Drawing.Point(922, 681);
            this.textBox_DapAnD.Name = "textBox_DapAnD";
            this.textBox_DapAnD.Size = new System.Drawing.Size(137, 22);
            this.textBox_DapAnD.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(133, 683);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 22);
            this.label8.TabIndex = 6;
            this.label8.Text = "Đáp án đúng:";
            // 
            // textBox_DapAnDung
            // 
            this.textBox_DapAnDung.Location = new System.Drawing.Point(257, 683);
            this.textBox_DapAnDung.Name = "textBox_DapAnDung";
            this.textBox_DapAnDung.Size = new System.Drawing.Size(137, 22);
            this.textBox_DapAnDung.TabIndex = 9;
            // 
            // button_QuayLai
            // 
            this.button_QuayLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_QuayLai.Location = new System.Drawing.Point(12, 314);
            this.button_QuayLai.Name = "button_QuayLai";
            this.button_QuayLai.Size = new System.Drawing.Size(104, 53);
            this.button_QuayLai.TabIndex = 11;
            this.button_QuayLai.Text = "Quay lại";
            this.button_QuayLai.UseVisualStyleBackColor = true;
            this.button_QuayLai.Click += new System.EventHandler(this.button4_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(160, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 22);
            this.label9.TabIndex = 5;
            this.label9.Text = "Đề Thi ID:";
            // 
            // button_Luu
            // 
            this.button_Luu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Luu.Location = new System.Drawing.Point(12, 220);
            this.button_Luu.Name = "button_Luu";
            this.button_Luu.Size = new System.Drawing.Size(104, 53);
            this.button_Luu.TabIndex = 11;
            this.button_Luu.Text = "Lưu";
            this.button_Luu.UseVisualStyleBackColor = true;
            this.button_Luu.Click += new System.EventHandler(this.button_Luu_Click);
            // 
            // comboBox_DeThiID
            // 
            this.comboBox_DeThiID.FormattingEnabled = true;
            this.comboBox_DeThiID.Location = new System.Drawing.Point(257, 101);
            this.comboBox_DeThiID.Name = "comboBox_DeThiID";
            this.comboBox_DeThiID.Size = new System.Drawing.Size(129, 24);
            this.comboBox_DeThiID.TabIndex = 12;
            this.comboBox_DeThiID.SelectedIndexChanged += new System.EventHandler(this.comboBox_DeThiID_SelectedIndexChanged_1);
            // 
            // textBox_MonID
            // 
            this.textBox_MonID.Location = new System.Drawing.Point(573, 101);
            this.textBox_MonID.Name = "textBox_MonID";
            this.textBox_MonID.Size = new System.Drawing.Size(132, 22);
            this.textBox_MonID.TabIndex = 13;
            // 
            // DeThi2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 770);
            this.Controls.Add(this.textBox_MonID);
            this.Controls.Add(this.comboBox_DeThiID);
            this.Controls.Add(this.button_QuayLai);
            this.Controls.Add(this.button_Luu);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button_Sua);
            this.Controls.Add(this.button_Them);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_DapAnDung);
            this.Controls.Add(this.textBox_DapAnD);
            this.Controls.Add(this.textBox_DapAnB);
            this.Controls.Add(this.textBox_ND);
            this.Controls.Add(this.textBox_DapAnC);
            this.Controls.Add(this.textBox_DapanA);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_CauHoi2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "DeThi2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_CauHoi2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Them;
        private System.Windows.Forms.Button button_Sua;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_ND;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_DapanA;
        private System.Windows.Forms.TextBox textBox_DapAnB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_DapAnC;
        private System.Windows.Forms.TextBox textBox_DapAnD;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_DapAnDung;
        private System.Windows.Forms.Button button_QuayLai;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_Luu;
        private System.Windows.Forms.ComboBox comboBox_DeThiID;
        private System.Windows.Forms.TextBox textBox_MonID;
    }
}