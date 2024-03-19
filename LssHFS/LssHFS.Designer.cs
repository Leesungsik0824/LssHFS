namespace LssHFS
{
    partial class LssHFS
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.CBO_아이피 = new System.Windows.Forms.ComboBox();
            this.BTN_시작 = new System.Windows.Forms.Button();
            this.TXT_포트 = new System.Windows.Forms.TextBox();
            this.TXT_홈경로 = new System.Windows.Forms.TextBox();
            this.TXT_로그 = new System.Windows.Forms.RichTextBox();
            this.BTN_중지 = new System.Windows.Forms.Button();
            this.BTN_열기 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CBO_아이피
            // 
            this.CBO_아이피.FormattingEnabled = true;
            this.CBO_아이피.Location = new System.Drawing.Point(12, 12);
            this.CBO_아이피.Name = "CBO_아이피";
            this.CBO_아이피.Size = new System.Drawing.Size(180, 20);
            this.CBO_아이피.TabIndex = 0;
            // 
            // BTN_시작
            // 
            this.BTN_시작.Location = new System.Drawing.Point(280, 12);
            this.BTN_시작.Name = "BTN_시작";
            this.BTN_시작.Size = new System.Drawing.Size(101, 23);
            this.BTN_시작.TabIndex = 1;
            this.BTN_시작.Text = "서비스시작";
            this.BTN_시작.UseVisualStyleBackColor = true;
            this.BTN_시작.Click += new System.EventHandler(this.BTN_시작_Click);
            // 
            // TXT_포트
            // 
            this.TXT_포트.Location = new System.Drawing.Point(198, 12);
            this.TXT_포트.Name = "TXT_포트";
            this.TXT_포트.Size = new System.Drawing.Size(76, 21);
            this.TXT_포트.TabIndex = 2;
            this.TXT_포트.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TXT_홈경로
            // 
            this.TXT_홈경로.Location = new System.Drawing.Point(12, 38);
            this.TXT_홈경로.Name = "TXT_홈경로";
            this.TXT_홈경로.ReadOnly = true;
            this.TXT_홈경로.Size = new System.Drawing.Size(407, 21);
            this.TXT_홈경로.TabIndex = 3;
            // 
            // TXT_로그
            // 
            this.TXT_로그.Location = new System.Drawing.Point(12, 65);
            this.TXT_로그.Name = "TXT_로그";
            this.TXT_로그.Size = new System.Drawing.Size(474, 373);
            this.TXT_로그.TabIndex = 5;
            this.TXT_로그.Text = "";
            // 
            // BTN_중지
            // 
            this.BTN_중지.Enabled = false;
            this.BTN_중지.Location = new System.Drawing.Point(385, 12);
            this.BTN_중지.Name = "BTN_중지";
            this.BTN_중지.Size = new System.Drawing.Size(101, 23);
            this.BTN_중지.TabIndex = 6;
            this.BTN_중지.Text = "서비스중지";
            this.BTN_중지.UseVisualStyleBackColor = true;
            this.BTN_중지.Click += new System.EventHandler(this.BTN_중지_Click);
            // 
            // BTN_열기
            // 
            this.BTN_열기.Location = new System.Drawing.Point(425, 38);
            this.BTN_열기.Name = "BTN_열기";
            this.BTN_열기.Size = new System.Drawing.Size(61, 21);
            this.BTN_열기.TabIndex = 4;
            this.BTN_열기.Text = "열기";
            this.BTN_열기.UseVisualStyleBackColor = true;
            this.BTN_열기.Click += new System.EventHandler(this.BTN_열기_Click);
            // 
            // LssHFS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 450);
            this.Controls.Add(this.BTN_중지);
            this.Controls.Add(this.TXT_로그);
            this.Controls.Add(this.BTN_열기);
            this.Controls.Add(this.TXT_홈경로);
            this.Controls.Add(this.TXT_포트);
            this.Controls.Add(this.BTN_시작);
            this.Controls.Add(this.CBO_아이피);
            this.Name = "LssHFS";
            this.Text = "LssHFS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CBO_아이피;
        private System.Windows.Forms.Button BTN_시작;
        private System.Windows.Forms.TextBox TXT_포트;
        private System.Windows.Forms.TextBox TXT_홈경로;
        private System.Windows.Forms.RichTextBox TXT_로그;
        private System.Windows.Forms.Button BTN_중지;
        private System.Windows.Forms.Button BTN_열기;
    }
}

