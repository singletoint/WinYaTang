namespace WinYatang
{
    partial class frm_Login
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
            this.components = new System.ComponentModel.Container();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.PnLogin = new System.Windows.Forms.Panel();
            this.rdoSelectUser = new System.Windows.Forms.RadioButton();
            this.rdoNewUser = new System.Windows.Forms.RadioButton();
            this.cboSelectUser = new System.Windows.Forms.ComboBox();
            this.txtPayPWD = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PnOptor = new System.Windows.Forms.Panel();
            this.txtLuckMoneyRate = new System.Windows.Forms.TextBox();
            this.txtInvestValue = new System.Windows.Forms.TextBox();
            this.chkUseLuckMoney = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLoginOut = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPayPWD = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tmrAutoInvest = new System.Windows.Forms.Timer(this.components);
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tmrRandFloat = new System.Windows.Forms.Timer(this.components);
            this.PnLogin.SuspendLayout();
            this.PnOptor.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(98, 47);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(100, 21);
            this.txt_UserName.TabIndex = 0;
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(98, 88);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(100, 21);
            this.txt_Password.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(98, 290);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(288, 19);
            this.txtLogs.Margin = new System.Windows.Forms.Padding(2);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(680, 413);
            this.txtLogs.TabIndex = 5;
            // 
            // PnLogin
            // 
            this.PnLogin.Controls.Add(this.rdoSelectUser);
            this.PnLogin.Controls.Add(this.rdoNewUser);
            this.PnLogin.Controls.Add(this.cboSelectUser);
            this.PnLogin.Controls.Add(this.txtPayPWD);
            this.PnLogin.Controls.Add(this.label1);
            this.PnLogin.Controls.Add(this.txt_UserName);
            this.PnLogin.Controls.Add(this.btn_Login);
            this.PnLogin.Controls.Add(this.txt_Password);
            this.PnLogin.Controls.Add(this.label5);
            this.PnLogin.Controls.Add(this.label6);
            this.PnLogin.Controls.Add(this.label2);
            this.PnLogin.Location = new System.Drawing.Point(477, 27);
            this.PnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.PnLogin.Name = "PnLogin";
            this.PnLogin.Size = new System.Drawing.Size(273, 401);
            this.PnLogin.TabIndex = 6;
            // 
            // rdoSelectUser
            // 
            this.rdoSelectUser.AutoSize = true;
            this.rdoSelectUser.Location = new System.Drawing.Point(20, 197);
            this.rdoSelectUser.Name = "rdoSelectUser";
            this.rdoSelectUser.Size = new System.Drawing.Size(95, 16);
            this.rdoSelectUser.TabIndex = 6;
            this.rdoSelectUser.Text = "选择用户登录";
            this.rdoSelectUser.UseVisualStyleBackColor = true;
            this.rdoSelectUser.CheckedChanged += new System.EventHandler(this.rdoUser_CheckedChanged);
            // 
            // rdoNewUser
            // 
            this.rdoNewUser.AutoSize = true;
            this.rdoNewUser.Checked = true;
            this.rdoNewUser.Location = new System.Drawing.Point(20, 14);
            this.rdoNewUser.Name = "rdoNewUser";
            this.rdoNewUser.Size = new System.Drawing.Size(83, 16);
            this.rdoNewUser.TabIndex = 6;
            this.rdoNewUser.TabStop = true;
            this.rdoNewUser.Tag = "";
            this.rdoNewUser.Text = "新用户登录";
            this.rdoNewUser.UseVisualStyleBackColor = true;
            this.rdoNewUser.CheckedChanged += new System.EventHandler(this.rdoUser_CheckedChanged);
            // 
            // cboSelectUser
            // 
            this.cboSelectUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectUser.Enabled = false;
            this.cboSelectUser.FormattingEnabled = true;
            this.cboSelectUser.Location = new System.Drawing.Point(98, 240);
            this.cboSelectUser.Name = "cboSelectUser";
            this.cboSelectUser.Size = new System.Drawing.Size(150, 20);
            this.cboSelectUser.TabIndex = 5;
            this.cboSelectUser.SelectedIndexChanged += new System.EventHandler(this.cboSelectUser_SelectedIndexChanged);
            // 
            // txtPayPWD
            // 
            this.txtPayPWD.Location = new System.Drawing.Point(98, 131);
            this.txtPayPWD.Name = "txtPayPWD";
            this.txtPayPWD.Size = new System.Drawing.Size(100, 21);
            this.txtPayPWD.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 134);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "支付密码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "选择用户";
            // 
            // PnOptor
            // 
            this.PnOptor.Controls.Add(this.txtLuckMoneyRate);
            this.PnOptor.Controls.Add(this.txtInvestValue);
            this.PnOptor.Controls.Add(this.chkUseLuckMoney);
            this.PnOptor.Controls.Add(this.btnStop);
            this.PnOptor.Controls.Add(this.btnStart);
            this.PnOptor.Controls.Add(this.btnLoginOut);
            this.PnOptor.Controls.Add(this.lblUserName);
            this.PnOptor.Controls.Add(this.label8);
            this.PnOptor.Controls.Add(this.label4);
            this.PnOptor.Controls.Add(this.lblPayPWD);
            this.PnOptor.Controls.Add(this.label7);
            this.PnOptor.Controls.Add(this.label3);
            this.PnOptor.Location = new System.Drawing.Point(9, 27);
            this.PnOptor.Margin = new System.Windows.Forms.Padding(2);
            this.PnOptor.Name = "PnOptor";
            this.PnOptor.Size = new System.Drawing.Size(259, 404);
            this.PnOptor.TabIndex = 5;
            // 
            // txtLuckMoneyRate
            // 
            this.txtLuckMoneyRate.Location = new System.Drawing.Point(103, 145);
            this.txtLuckMoneyRate.Margin = new System.Windows.Forms.Padding(2);
            this.txtLuckMoneyRate.Name = "txtLuckMoneyRate";
            this.txtLuckMoneyRate.Size = new System.Drawing.Size(114, 21);
            this.txtLuckMoneyRate.TabIndex = 3;
            this.txtLuckMoneyRate.Text = "50";
            // 
            // txtInvestValue
            // 
            this.txtInvestValue.Enabled = false;
            this.txtInvestValue.Location = new System.Drawing.Point(103, 185);
            this.txtInvestValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtInvestValue.Name = "txtInvestValue";
            this.txtInvestValue.Size = new System.Drawing.Size(114, 21);
            this.txtInvestValue.TabIndex = 3;
            this.txtInvestValue.Text = "0";
            // 
            // chkUseLuckMoney
            // 
            this.chkUseLuckMoney.AutoSize = true;
            this.chkUseLuckMoney.Checked = true;
            this.chkUseLuckMoney.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseLuckMoney.Location = new System.Drawing.Point(39, 113);
            this.chkUseLuckMoney.Margin = new System.Windows.Forms.Padding(2);
            this.chkUseLuckMoney.Name = "chkUseLuckMoney";
            this.chkUseLuckMoney.Size = new System.Drawing.Size(72, 16);
            this.chkUseLuckMoney.TabIndex = 2;
            this.chkUseLuckMoney.Text = "使用红包";
            this.chkUseLuckMoney.UseVisualStyleBackColor = true;
            this.chkUseLuckMoney.CheckedChanged += new System.EventHandler(this.chkUseLuckMoney_CheckedChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(126, 239);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(56, 18);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "暂停抢标";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(29, 239);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 18);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "开始抢标";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnLoginOut
            // 
            this.btnLoginOut.Location = new System.Drawing.Point(191, 14);
            this.btnLoginOut.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoginOut.Name = "btnLoginOut";
            this.btnLoginOut.Size = new System.Drawing.Size(56, 18);
            this.btnLoginOut.TabIndex = 1;
            this.btnLoginOut.Text = "注销";
            this.btnLoginOut.UseVisualStyleBackColor = true;
            this.btnLoginOut.Click += new System.EventHandler(this.btnLoginOut_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(112, 42);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(53, 12);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "UserName";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 148);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "红包比例";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 187);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "投资金额";
            // 
            // lblPayPWD
            // 
            this.lblPayPWD.AutoSize = true;
            this.lblPayPWD.Location = new System.Drawing.Point(112, 72);
            this.lblPayPWD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPayPWD.Name = "lblPayPWD";
            this.lblPayPWD.Size = new System.Drawing.Size(41, 12);
            this.lblPayPWD.TabIndex = 0;
            this.lblPayPWD.Text = "PayPWD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 72);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "支付密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "用户名";
            // 
            // tmrAutoInvest
            // 
            this.tmrAutoInvest.Interval = 2000;
            this.tmrAutoInvest.Tick += new System.EventHandler(this.tmrAutoInvest_Tick);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(822, 436);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(132, 26);
            this.webBrowser1.TabIndex = 7;
            this.webBrowser1.Url = new System.Uri("https://jr.yatang.cn/Invest/ViewBorrow/ibid/1122099", System.UriKind.Absolute);
            this.webBrowser1.Visible = false;
            // 
            // tmrRandFloat
            // 
            this.tmrRandFloat.Tick += new System.EventHandler(this.tmrRandFloat_Tick);
            // 
            // frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(998, 463);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.PnOptor);
            this.Controls.Add(this.PnLogin);
            this.Controls.Add(this.txtLogs);
            this.MaximizeBox = false;
            this.Name = "frm_Login";
            this.Text = "frm_Login";
            this.Load += new System.EventHandler(this.frm_Login_Load);
            this.PnLogin.ResumeLayout(false);
            this.PnLogin.PerformLayout();
            this.PnOptor.ResumeLayout(false);
            this.PnOptor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.Panel PnLogin;
        private System.Windows.Forms.Panel PnOptor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Timer tmrAutoInvest;
        private System.Windows.Forms.Button btnLoginOut;
        private System.Windows.Forms.TextBox txtPayPWD;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.CheckBox chkUseLuckMoney;
        private System.Windows.Forms.TextBox txtInvestValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer tmrRandFloat;
        private System.Windows.Forms.ComboBox cboSelectUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdoSelectUser;
        private System.Windows.Forms.RadioButton rdoNewUser;
        private System.Windows.Forms.Label lblPayPWD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLuckMoneyRate;
        private System.Windows.Forms.Label label8;
    }
}