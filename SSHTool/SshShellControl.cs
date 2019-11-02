using System;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace SSHTool
{
    public class SshShellControl
    {
        private SshClient sshClient = null;
        private ShellStream shellStream = null;
        private Action<Object, String> reveiverDataAction = null;

        //message return for agent when have some problem
        public String SSHMessage
        {
            get;
            set;
        }

        //function open connection to ssh server, return true when connected and false else
        public bool OpenConnect(String sshHost, string userName, String password, int port,
            int timeout,uint consoleWidth, uint consoleHeight)
        {
            //return value, default is false
            bool blResult = false;

            try
            {
                //set message default is ""
                SSHMessage = "";

                //create new sshClient variable, input connect to ssh server (user,pass,port,host)
                this.sshClient = new SshClient(sshHost, port, userName, password);
                //set connection timeout when connect
                this.sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(timeout);
                //create connect session
                this.sshClient.Connect();
                //create shell console let interactive with ssh server
                this.shellStream = this.sshClient.CreateShellStream("vt100", 800, 600, consoleWidth, consoleHeight, 655536);
                //return true when connect is successed
                blResult = true;
            }
            catch(Exception exp)
            {
                SSHMessage = exp.Message;
            }
            return blResult;
        }

        //send command via shell stream
        public void SendCommand(String sshCommand)
        {
            try
            {
                SSHMessage = "";
                this.shellStream.WriteLine(sshCommand);
                this.shellStream.Flush();
            }
            catch(Exception exp)
            {
                SSHMessage = exp.Message;
            }
        }

        //read ssh data reciever via shell stream
        public String ReadData()
        {
            String data = "";
            try
            {
                SSHMessage = "";
                if (this.shellStream.CanRead)
                {
                    data = this.shellStream.ReadLine();
                }
            }
            catch (Exception exp)
            {
                SSHMessage = exp.Message;
            }
            return data;
        }

        //register event recv data when have data is receivered from ssh server
        public void SetReveiverDataEvent(Action<Object,String> action)
        {
            try
            {
                SSHMessage = "";
                if(this.shellStream == null)
                {
                    return;
                }
                this.shellStream.DataReceived += new EventHandler<Renci.SshNet.Common.ShellDataEventArgs>(ReceiverData);
                this.reveiverDataAction = action;
            }
            catch (Exception exp)
            {
                SSHMessage = exp.Message;
            }
        }

        //convert event from ssh event to string data
        private void ReceiverData(Object o, ShellDataEventArgs e)
        {
            if(this.reveiverDataAction != null)
            {
                this.reveiverDataAction.DynamicInvoke(o,System.Text.Encoding.UTF8.GetString(e.Data));
            }
        }
        //check ssh session is connected
        public bool isConnected()
        {
            return this.sshClient != null && this.shellStream != null
                && this.sshClient.IsConnected;
        }

        public void Close()
        {
            //close all stream in/out when interactive with ssh server

            //shell command read/write data
            if (this.shellStream != null)
            {
                try
                {
                    this.shellStream.Close();
                    this.shellStream = null;
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

            //ssh client
            if (this.sshClient != null)
            {
                try
                {
                    this.sshClient.Disconnect();
                    this.sshClient = null;
                }
                catch(Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
    }
}
