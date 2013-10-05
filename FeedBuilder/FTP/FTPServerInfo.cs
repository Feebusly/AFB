using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace FeedBuilder.FTP
{

    public class FTPServerInfo
    {
        private string mServer;
        private string mUser;
        private string mPassword;
        private string mRemotePath;

        /// <summary>
        /// Default private constructor, use the GetInstance() method instead.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="remotePath"></param>
        private FTPServerInfo(string server, string user, string password, string remotePath)
        {
            mServer = server;
            mUser = user;
            mPassword = Encrypt(password);
            mRemotePath = remotePath;
        }

        /// <summary>
        /// Creates a new FTPServerInfo instance without saving data to the filesystem.
        /// </summary>
        /// <param name="server">The server name.</param>
        /// <param name="user">The username.</param>
        /// <param name="password">User's password in unencrypted format.</param>
        /// <param name="remotePath">The FTP Server remote path to log into.</param>
        public static FTPServerInfo CreateInstanceNoSave(string server, string user,
            string password, string remotePath)
        {
            return new FTPServerInfo(server, user, password, remotePath);
        }

        /// <summary>
        /// Returns the URL FTP string, without username and password data.
        /// e.g. ftp://myserver.com/relative/url/
        /// or ftp://myserver.com if no remote path
        /// </summary>
        public string UrlString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("ftp://");
                sb.Append(Server);
                string remotePath = RemotePath;
                if (remotePath != null && remotePath != "")
                {
                    if (!remotePath.StartsWith("/"))
                        remotePath = "/" + remotePath;
                    if (!remotePath.EndsWith("/"))
                        remotePath = remotePath + "/";
                }
                sb.Append(remotePath);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns the entire URI FTP string.
        /// e.g. ftp://uname:pword@myserver.com/relative/url/
        /// or ftp://uname:pword@myserver.com if no remote path
        /// </summary>
        public string UriString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("ftp://");
                sb.Append(User);
                sb.Append(":");
                sb.Append(Password);
                sb.Append("@");
                sb.Append(Server);
                string remotePath = RemotePath;
                if (remotePath != null && remotePath != "")
                {
                    if (!remotePath.StartsWith("/"))
                        remotePath = "/" + remotePath;
                    if (!remotePath.EndsWith("/"))
                        remotePath = remotePath + "/";
                }
                sb.Append(remotePath);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Creates a new FTPServerInfo instance and stores it on the file system.
        /// </summary>
        /// <param name="server">The server name.</param>
        /// <param name="user">The username.</param>
        /// <param name="password">User's password in unencrypted format.</param>
        /// <param name="remotePath">The FTP Server remote path to log into.</param>
        public static FTPServerInfo CreateInstance(string server, string user, 
            string password, string remotePath)
        {
            FTPServerInfo serverInfo = FTPServerInfo.GetInstance(server);
            if (serverInfo == null)
            {
                serverInfo = new FTPServerInfo(server, user, password, remotePath);
            }
            else
            {
                serverInfo.Server = server;
                serverInfo.Password = password;
                serverInfo.RemotePath = remotePath;
            }
            return serverInfo;
        }

        /// <summary>
        /// Returns an instance of an FTPServerInfo object that was previously stored on the
        /// filesystem.
        /// </summary>
        /// <param name="server">Server URL.</param>
        public static FTPServerInfo GetInstance(string server)
        {
            FTPServerInfo serverInfo = null;
            string path = FTPServerConfigFile;
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (data[0] == server)
                        {
                            string sv = null, user = null, pw = null, rp = null;
                            if (data.Length > 0)
                                sv = data[0];
                            if (data.Length > 1)
                                user = data[1];
                            if (data.Length > 2)
                                pw = Decrypt(data[2]);
                            if (data.Length > 3)
                                rp = data[3];

                            serverInfo = new FTPServerInfo(sv, user, pw, rp);
                            break;
                        }
                    }
                }
            }
            return serverInfo;
        }

        public static List<string> GetServerList()
        {
            List<string> servers = new List<string>();
            string configFile = FTPServerConfigFile;
            if (File.Exists(configFile))
            {
                using (StreamReader sr = File.OpenText(configFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        servers.Add(data[0]);
                    }
                }
            }
            return servers;
        }

        private static string FTPServerConfigFile
        {
            get
            {
                const string subFolder = "\\ServerData.config";
                return FTPServerConfigFileDir + subFolder;
            }
        }

        private static string FTPServerConfigFileDir
        {
            get
            {
                const string subFolder = "\\AnotherFeedBuilder";
                return Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + subFolder;
            }
        }

        public void Delete()
        {
            if (File.Exists(FTPServerConfigFile))
                Update(true);
        }

        public void UpDir()
        {
            if (mRemotePath != null && mRemotePath != string.Empty)
            {
                string[] splits = mRemotePath.Split('/', '\\');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < splits.Length - 1; i++)
                {
                    sb.Append(splits[i]);
                    sb.Append('/');
                }
                //Remove the trainling '/'.
                if (sb.Length > 0)
                    sb.Remove(sb.Length -1, 1);
                mRemotePath = sb.ToString();
            }
        }

        public void Save()
        {
            Update(false);
        }

        /// <summary>
        /// Puts information from this FTPServerInfo instance into the configuration file.
        /// </summary>
        /// <param name="delete">If true, then delete this server entry.</param>
        private void Update(bool delete)
        {
            if (!Directory.Exists(FTPServerConfigFileDir))
                Directory.CreateDirectory(FTPServerConfigFileDir);

            string path = FTPServerConfigFile;
            StringBuilder sb = new StringBuilder();
            bool serverFound = false;
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line.StartsWith(mServer))
                        {
                            serverFound = true;
                            if (!delete)
                                line = string.Join("\t", mServer, mUser, mPassword, mRemotePath);
                            else
                                line = null;
                        }
                        if (line != null)
                            sb.AppendLine(line);
                    }
                }
            }

            if (!serverFound && !delete)
                sb.AppendLine( string.Join("\t", mServer, mUser, mPassword, mRemotePath));

            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(sb.ToString());
            }
        }

        public string Server
        {
            get { return mServer; }
            set { mServer = value; }
        }

        public string User
        {
            get { return mUser; }
            set { mUser = value; }
        }

        public string Password
        {
            get { return Decrypt(mPassword); }
            set { mPassword = Encrypt(value); }
        }
        
        public string RemotePath
        {
            get { return mRemotePath; }
            set { mRemotePath = value; }
        }

        /// <summary>
        /// Encrypts a given password and returns the encrypted data
        /// as a base64 string.
        /// </summary>
        /// <param name="plainText">An unencrypted string that needs
        /// to be secured.</param>
        /// <returns>A base64 encoded string that represents the encrypted
        /// binary data.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="plainText"/>
        /// is a null reference.</exception>
        private static string Encrypt(string plainText)
        {
            if (plainText == null) throw new ArgumentNullException("plainText");

            //encrypt data
            byte[] data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        /// <param name="cipher">A base64 encoded string that was created
        /// through the <see cref="Encrypt(string)"/> or
        /// <see cref="Encrypt(SecureString)"/> extension methods.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="cipher"/>
        /// is a null reference.</exception>
        private static string Decrypt(string cipher)
        {
            if (cipher == null) throw new ArgumentNullException("cipher");

            //parse base64 string
            byte[] data = Convert.FromBase64String(cipher);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }


    }
}
