using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSHTool
{
    public partial class FormSshLogin : Form
    {
        public DialogResult dialogResult
        {
            get;
            set;
        }

        public FormSshLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.dialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtUserName.Text.Trim().Equals("") 
                    || this.txtUserName.Text.Trim().Equals("")
                    || this.txtPassword.Text.Trim().Equals(""))
                {
                    resetForm();
                    MessageBox.Show("Địa chỉ, tên đăng nhập, mật khẩu  không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int port = 22;

                if (this.txtPort.Text.Trim().Equals("") || !Int32.TryParse(this.txtPort.Text.Trim(), out port))
                {
                    MessageBox.Show("Cổng không được để trống và phải là số trong khoảng từ 1 ~ 65535", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.dialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show("Lỗi trong quá trình đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetForm();
            }
        }

        private void resetForm()
        {
            this.txtHost.Text = "";
            this.txtUserName.Text = "";
            this.txtPassword.Text = "";
            this.txtHost.Focus();
        }
    }
}
