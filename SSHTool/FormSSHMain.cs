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
                //[0m
                while(dataLine.IndexOf("\u001b[0m\u001b[01;34m") >= 0)
                {
                    dataLine = dataLine.Replace("\u001b[0m\u001b[01;34m", "");
                }
                while (dataLine.IndexOf("\u001b[0m") >= 0)
                {
                    dataLine = dataLine.Replace("\u001b[0m", "");
                }
                while (dataLine.IndexOf("\u001b[01;34m") >= 0)
                {
                    dataLine = dataLine.Replace("\u001b[01;34m", "");
                }
                while (dataLine.IndexOf("\u001b[0m") >= 0)
                {
                    dataLine = dataLine.Replace("\u001b[0m", "");
                }
                while (dataLine.IndexOf("i\b\u001b[K??\b\u001b[K") >= 0)
                {
                    dataLine = dataLine.Replace("i\b\u001b[K??\b\u001b[K", "");
                }
                while (dataLine.IndexOf("\u001b[01;31m\u001b[K") >= 0)
                {
                    dataLine = dataLine.Replace("\u001b[01;31m\u001b[K", "");
                }
                while (dataLine.IndexOf("\u001b[m\u001b[K") >= 0)
                {
                    dataLine = dataLine.Replace("\u001b[m\u001b[K", "");
                }
                while (dataLine.IndexOf("\u001b[01;31m\u001b[K") >= 0)
                {
                    dataLine = dataLine.Replace("\u001b[01;31m\u001b[K", "");
                }
                if (!"".Equals(dataLine))
                {
                    invokeAppendTextBox(this.txtSSHCommandLine, dataLine);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
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
                return;
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
                if(t.Lines.Length > 400)
                {
                    t.Lines = t.Lines.Skip(30).ToArray() ;
                }
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

        private void txtSSHCommandLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtSSHCommandLine.Lines.Length > 400)
            {
                this.txtSSHCommandLine.Lines = this.txtSSHCommandLine.Lines.Skip(30).ToArray();
            }
            this.commandLine.Append(e.KeyChar);
        }
    }
}
