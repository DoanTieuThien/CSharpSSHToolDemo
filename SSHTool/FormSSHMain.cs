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
    public partial class FormSSHMain : Form
    {
        private SshShellControl sshShellControl = null;
        private StringBuilder commandLine = new StringBuilder();
        private int loginIndex = 0;

        public FormSSHMain()
        {
            InitializeComponent();
        }

        private void txtSSHCommandLine_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.sshShellControl.SendCommand(this.commandLine.ToString());
                    commandLine = new StringBuilder();
                }
                else
                {
                    this.commandLine.Append(e.KeyData);
                }
            }
            catch (Exception exp)
            {
                if (this.sshShellControl == null || !this.sshShellControl.isConnected())
                {
                    MessageBox.Show("Phiên đăng nhập mất kết nối, xin hãy đăng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loginIndex = 0;
                    loginSSh();
                }
                else
                {
                    invokeAppendTextBox(this.txtSSHCommandLine, "Thực thi command thất bại, xin hãy thử lại \r\n");
                }
                Console.WriteLine(exp.Message);
            }
        }

        private void receiverData(Object o, String dataLine)
        {
            try
            {
                invokeAppendTextBox(this.txtSSHCommandLine, dataLine);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        private void txtSSHCommandLine_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void loginSSh()
        {
            FormSshLogin formSshLogin = new FormSshLogin();
            loginIndex++;

            if(loginIndex > 4)
            {
                this.Close();
            }
            formSshLogin.ShowDialog();
            if (formSshLogin.dialogResult != DialogResult.OK)
            {
                this.Close();
            }
            this.sshShellControl = new SshShellControl();
            bool blResult = this.sshShellControl.OpenConnect(formSshLogin.txtHost.Text.Trim(),formSshLogin.txtUserName.Text.Trim()
                ,formSshLogin.txtPassword.Text.Trim(),Convert.ToInt32(formSshLogin.txtPort.Text.Trim()),120,(uint)this.Width, (uint)this.Height);

            if(!blResult)
            {
                loginSSh();
            }
            this.sshShellControl.SetReveiverDataEvent(receiverData);
        }

        private void invokeAppendTextBox(TextBox t, String s)
        {
            if(t.InvokeRequired)
            {
                t.Invoke(new Action<TextBox, String>(invokeAppendTextBox), new object[] { t, s });
            }
            else
            {
                t.AppendText(s);
            }
        }

        //load login form
        private void FormSSHMain_Load(object sender, EventArgs e)
        {
            loginIndex = 0;
            loginSSh();
        }

        //close form, close ssh session
        private void FormSSHMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(this.sshShellControl != null)
                {
                    this.sshShellControl.Close();
                    this.sshShellControl = null;
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
