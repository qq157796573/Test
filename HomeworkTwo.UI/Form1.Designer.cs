namespace HomeworkTwo.UI
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
            this.UserInfoBtn = new System.Windows.Forms.Button();
            this.CustomersInfoBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserInfoBtn
            // 
            this.UserInfoBtn.Location = new System.Drawing.Point(35, 72);
            this.UserInfoBtn.Name = "UserInfoBtn";
            this.UserInfoBtn.Size = new System.Drawing.Size(75, 23);
            this.UserInfoBtn.TabIndex = 0;
            this.UserInfoBtn.Text = "用户信息";
            this.UserInfoBtn.UseVisualStyleBackColor = true;
            this.UserInfoBtn.Click += new System.EventHandler(this.UserInfoBtn_Click);
            // 
            // CustomersInfoBtn
            // 
            this.CustomersInfoBtn.Location = new System.Drawing.Point(144, 72);
            this.CustomersInfoBtn.Name = "CustomersInfoBtn";
            this.CustomersInfoBtn.Size = new System.Drawing.Size(75, 23);
            this.CustomersInfoBtn.TabIndex = 1;
            this.CustomersInfoBtn.Text = "客户信息";
            this.CustomersInfoBtn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CustomersInfoBtn);
            this.Controls.Add(this.UserInfoBtn);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UserInfoBtn;
        private System.Windows.Forms.Button CustomersInfoBtn;
    }
}

