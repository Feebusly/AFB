using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FeedBuilder.FTP;
using System.Net;
using System.Net.FtpClient;

namespace FeedBuilder
{
    public partial class FTPConnectForm : Form
    {
        private bool mCancelled;
        private FTPServerInfo mFTPServerInfo;

        public FTPConnectForm()
        {
            InitializeComponent();
        }

        private void mConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsComplete())
                {
                    MessageBox.Show("Please enter a server, user, and password.");
                    return;
                }
                if (mFTPServerInfo == null)
                {
                    mFTPServerInfo = FTPServerInfo.CreateInstanceNoSave(mServer.Text, mUsername.Text,
                        mPassword.Text, mRemotePath.Text);
                }

                mFTPServerInfo.Server = mServer.Text;
                mFTPServerInfo.User = mUsername.Text;
                mFTPServerInfo.Password = mPassword.Text;
                mFTPServerInfo.RemotePath = mRemotePath.Text;
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// A server info object created by the form.  Use this for connecting.
        /// </summary>
        public FTPServerInfo ServerInfo
        {
            get { return mFTPServerInfo; }
        }

        public bool IsComplete()
        {
            return mServer.Text != null && mUsername.Text != null && mPassword.Text != null;
        }

        public bool IsCancelled()
        {
            return mCancelled;
        }

        private void mCancel_Click(object sender, EventArgs e)
        {
            mCancelled = true;
            this.Close();
        }

        private void mServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string server = mServer.Items[mServer.SelectedIndex] as string;
            FTPServerInfo serverInfo = FTPServerInfo.GetInstance(server);
            if (serverInfo != null)
            {
                mServer.Text = serverInfo.Server;
                mUsername.Text = serverInfo.User;
                mPassword.Text = serverInfo.Password;
                mRemotePath.Text = serverInfo.RemotePath;
            }
        }

        private void FTPConnectForm_Load(object sender, EventArgs e)
        {
            List<string> servers = FTPServerInfo.GetServerList();
            foreach (string server in servers)
            {
                mServer.Items.Add(server);
            }
        }

        private void mSaveButton_Click(object sender, EventArgs e)
        {
            mFTPServerInfo = FTPServerInfo.CreateInstance(mServer.Text, mUsername.Text,
                mPassword.Text, mRemotePath.Text);
            mFTPServerInfo.Save();
            if (!mServer.Items.Contains(mFTPServerInfo.Server))
            {
                mServer.Items.Add(mFTPServerInfo.Server);
            }
        }

        private void mDeleteButton_Click(object sender, EventArgs e)
        {
            mFTPServerInfo = FTPServerInfo.GetInstance(mServer.Text);
            if (mFTPServerInfo != null)
            {
                mFTPServerInfo.Delete();
                mFTPServerInfo = null;
            }
        }
    }
}
