namespace BCatchAutoConverter
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_SetPath_OriginalVedio = new System.Windows.Forms.Button();
            this.Text_Path_OriginalVedio = new System.Windows.Forms.Label();
            this.Button_SetPath_SetOriginalAudio = new System.Windows.Forms.Button();
            this.Text_Path_OriginalAudio = new System.Windows.Forms.Label();
            this.Button_Decrypt = new System.Windows.Forms.Button();
            this.Button_ConvertFormat = new System.Windows.Forms.Button();
            this.Button_ChangeExt = new System.Windows.Forms.Button();
            this.Button_AutoStart = new System.Windows.Forms.Button();
            this.Button_Combine = new System.Windows.Forms.Button();
            this.InputField_OutputFileName = new System.Windows.Forms.TextBox();
            this.Text_OutputFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Button_SetPath_OriginalVedio
            // 
            this.Button_SetPath_OriginalVedio.Location = new System.Drawing.Point(12, 24);
            this.Button_SetPath_OriginalVedio.Name = "Button_SetPath_OriginalVedio";
            this.Button_SetPath_OriginalVedio.Size = new System.Drawing.Size(110, 23);
            this.Button_SetPath_OriginalVedio.TabIndex = 0;
            this.Button_SetPath_OriginalVedio.Text = "设置原视频路径";
            this.Button_SetPath_OriginalVedio.UseVisualStyleBackColor = true;
            this.Button_SetPath_OriginalVedio.Click += new System.EventHandler(this.Button_SetPath_OriginalVedio_Click);
            // 
            // Text_Path_OriginalVedio
            // 
            this.Text_Path_OriginalVedio.AutoSize = true;
            this.Text_Path_OriginalVedio.Location = new System.Drawing.Point(12, 9);
            this.Text_Path_OriginalVedio.Name = "Text_Path_OriginalVedio";
            this.Text_Path_OriginalVedio.Size = new System.Drawing.Size(77, 12);
            this.Text_Path_OriginalVedio.TabIndex = 1;
            this.Text_Path_OriginalVedio.Text = "无原视频路径";
            // 
            // Button_SetPath_SetOriginalAudio
            // 
            this.Button_SetPath_SetOriginalAudio.Location = new System.Drawing.Point(12, 76);
            this.Button_SetPath_SetOriginalAudio.Name = "Button_SetPath_SetOriginalAudio";
            this.Button_SetPath_SetOriginalAudio.Size = new System.Drawing.Size(108, 23);
            this.Button_SetPath_SetOriginalAudio.TabIndex = 2;
            this.Button_SetPath_SetOriginalAudio.Text = "设置原音频路径";
            this.Button_SetPath_SetOriginalAudio.UseVisualStyleBackColor = true;
            this.Button_SetPath_SetOriginalAudio.Click += new System.EventHandler(this.Button_SetPath_SetOriginalAudio_Click);
            // 
            // Text_Path_OriginalAudio
            // 
            this.Text_Path_OriginalAudio.AutoSize = true;
            this.Text_Path_OriginalAudio.Location = new System.Drawing.Point(12, 61);
            this.Text_Path_OriginalAudio.Name = "Text_Path_OriginalAudio";
            this.Text_Path_OriginalAudio.Size = new System.Drawing.Size(77, 12);
            this.Text_Path_OriginalAudio.TabIndex = 3;
            this.Text_Path_OriginalAudio.Text = "无原音频路径";
            // 
            // Button_Decrypt
            // 
            this.Button_Decrypt.Location = new System.Drawing.Point(12, 174);
            this.Button_Decrypt.Name = "Button_Decrypt";
            this.Button_Decrypt.Size = new System.Drawing.Size(75, 23);
            this.Button_Decrypt.TabIndex = 6;
            this.Button_Decrypt.Text = "解密文件";
            this.Button_Decrypt.UseVisualStyleBackColor = true;
            this.Button_Decrypt.Click += new System.EventHandler(this.Button_Decrypt_Click);
            // 
            // Button_ConvertFormat
            // 
            this.Button_ConvertFormat.Location = new System.Drawing.Point(14, 232);
            this.Button_ConvertFormat.Name = "Button_ConvertFormat";
            this.Button_ConvertFormat.Size = new System.Drawing.Size(75, 23);
            this.Button_ConvertFormat.TabIndex = 7;
            this.Button_ConvertFormat.Text = "转换格式";
            this.Button_ConvertFormat.UseVisualStyleBackColor = true;
            this.Button_ConvertFormat.Click += new System.EventHandler(this.Button_ConvertFormat_Click);
            // 
            // Button_ChangeExt
            // 
            this.Button_ChangeExt.Location = new System.Drawing.Point(14, 203);
            this.Button_ChangeExt.Name = "Button_ChangeExt";
            this.Button_ChangeExt.Size = new System.Drawing.Size(75, 23);
            this.Button_ChangeExt.TabIndex = 8;
            this.Button_ChangeExt.Text = "更改后缀";
            this.Button_ChangeExt.UseVisualStyleBackColor = true;
            this.Button_ChangeExt.Click += new System.EventHandler(this.Button_ChangeExt_Click);
            // 
            // Button_AutoStart
            // 
            this.Button_AutoStart.Location = new System.Drawing.Point(14, 286);
            this.Button_AutoStart.Name = "Button_AutoStart";
            this.Button_AutoStart.Size = new System.Drawing.Size(75, 23);
            this.Button_AutoStart.TabIndex = 9;
            this.Button_AutoStart.Text = "一键转换";
            this.Button_AutoStart.UseVisualStyleBackColor = true;
            this.Button_AutoStart.Click += new System.EventHandler(this.Button_AutoStart_Click);
            // 
            // Button_Combine
            // 
            this.Button_Combine.Location = new System.Drawing.Point(14, 315);
            this.Button_Combine.Name = "Button_Combine";
            this.Button_Combine.Size = new System.Drawing.Size(75, 23);
            this.Button_Combine.TabIndex = 10;
            this.Button_Combine.Text = "合并音视频";
            this.Button_Combine.UseVisualStyleBackColor = true;
            this.Button_Combine.Click += new System.EventHandler(this.Button_Combine_Click);
            // 
            // InputField_OutputFileName
            // 
            this.InputField_OutputFileName.Location = new System.Drawing.Point(12, 135);
            this.InputField_OutputFileName.Name = "InputField_OutputFileName";
            this.InputField_OutputFileName.Size = new System.Drawing.Size(167, 21);
            this.InputField_OutputFileName.TabIndex = 11;
            // 
            // Text_OutputFileName
            // 
            this.Text_OutputFileName.AutoSize = true;
            this.Text_OutputFileName.Location = new System.Drawing.Point(12, 120);
            this.Text_OutputFileName.Name = "Text_OutputFileName";
            this.Text_OutputFileName.Size = new System.Drawing.Size(149, 12);
            this.Text_OutputFileName.TabIndex = 12;
            this.Text_OutputFileName.Text = "输出文件/合并文件 的名称";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.Text_OutputFileName);
            this.Controls.Add(this.InputField_OutputFileName);
            this.Controls.Add(this.Button_Combine);
            this.Controls.Add(this.Button_AutoStart);
            this.Controls.Add(this.Button_ChangeExt);
            this.Controls.Add(this.Button_ConvertFormat);
            this.Controls.Add(this.Button_Decrypt);
            this.Controls.Add(this.Text_Path_OriginalAudio);
            this.Controls.Add(this.Button_SetPath_SetOriginalAudio);
            this.Controls.Add(this.Text_Path_OriginalVedio);
            this.Controls.Add(this.Button_SetPath_OriginalVedio);
            this.MaximumSize = new System.Drawing.Size(300, 400);
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "MainForm";
            this.Text = "BCatchExtractor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_SetPath_OriginalVedio;
        private System.Windows.Forms.Label Text_Path_OriginalVedio;
        private System.Windows.Forms.Button Button_SetPath_SetOriginalAudio;
        private System.Windows.Forms.Label Text_Path_OriginalAudio;
        private System.Windows.Forms.Button Button_Decrypt;
        private System.Windows.Forms.Button Button_ConvertFormat;
        private System.Windows.Forms.Button Button_ChangeExt;
        private System.Windows.Forms.Button Button_AutoStart;
        private System.Windows.Forms.Button Button_Combine;
        private System.Windows.Forms.TextBox InputField_OutputFileName;
        private System.Windows.Forms.Label Text_OutputFileName;
    }
}

