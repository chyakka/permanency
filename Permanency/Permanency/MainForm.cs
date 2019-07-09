using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Permanency
{
    public partial class MainForm : Form
    {

        TcpClient clientSocket = new TcpClient();
        NetworkStream stream = default;
        string readData = null;

        public User User { get; set; }

        private string ConnectedIP { get; set; }

        private string Username { get; set; }


        public MainForm()
        {
            InitializeComponent();
            AcceptButton = SendMessageBtn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OutputTextBox.MaximumSize = new Size(OutputTextBox.Size.Width * 2, OutputTextBox.Size.Height * 2);
            ToggleLoginWindow(true);
        }

        private void ToggleLoginWindow(bool toggle)
        {
            if(toggle)
            {
                UsernameTextBox.Visible = true;
                PasswordTextBox.Visible = true;
                EmailTextBox.Visible = true;
                LoginButton.Visible = true;
                RegisterButton.Visible = true;
                ErrorLabel.Visible = true;

                IPTextBox.Visible = false;
                PortTextBox.Visible = false;
                OutputTextBox.Visible = false;
                MsgInputBox.Visible = false;
                SendMessageBtn.Visible = false;
                ConnectBtn.Visible = false;
                DisconnectBtn.Visible = false;
                UserCountLabel.Visible = false;
            }
            else
            {
                UsernameTextBox.Visible = false;
                PasswordTextBox.Visible = false;
                EmailTextBox.Visible = false;
                LoginButton.Visible = false;
                RegisterButton.Visible = false;
                ErrorLabel.Visible = false;

                IPTextBox.Visible = true;
                PortTextBox.Visible = true;
                OutputTextBox.Visible = true;
                MsgInputBox.Visible = true;
                SendMessageBtn.Visible = true;
                ConnectBtn.Visible = true;
                DisconnectBtn.Visible = true;
                UserCountLabel.Visible = true;
                if (!clientSocket.Connected) UserCountLabel.Text = "User Count: N/A";
            }
        }

        private void GetMessage()
        {
            while (true)
            {
                try //look for new written data from the stream 
                {
                    stream = clientSocket.GetStream();
                    byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
                    string returndata = Encoding.UTF8.GetString(inStream, 0, stream.Read(inStream, 0, clientSocket.ReceiveBufferSize));
                    readData = returndata;
                    ProcessMessage();
                }
                catch
                {
                    break;
                }
            }
        }

        private void ProcessMessage()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(ProcessMessage));
            }
            else
            {
                if (readData.Length > 10 && readData.Substring(0, 11) == "User Count:")
                {
                    UserCountLabel.Text = $"{readData}";
                }
                else
                {
                    if (OutputTextBox.Text.Length != 0)
                    {
                        OutputTextBox.Text += $"\n{DateTime.Now.Hour}:{DateTime.Now.Minute} - {readData}";
                    }
                    else
                    {
                        OutputTextBox.Text += $"{DateTime.Now.Hour}:{DateTime.Now.Minute} - {readData}";
                    }
                }
            }
        }

        private string ReturnPrefixName()
        {
            string title = string.Empty;
            if (User.Moderator) title = "Moderator ";
            if (User.Developer) title = "Developer ";
            return title;
        }

        private void SendMessageBtn_Click(object sender, EventArgs e)
        {
            if (clientSocket.Connected)
            {
                if (MsgInputBox.Text.Length > 0)
                {
                    byte[] outStream = Encoding.UTF8.GetBytes($"VALID-MSG23U8: {ReturnPrefixName()}{User.Username}: {MsgInputBox.Text}");
                    stream.Write(outStream, 0, outStream.Length);
                    stream.Flush();
                    MsgInputBox.ResetText();
                    MsgInputBox.Select();
                }
            }
            else
            {
                MessageBox.Show("You need to be connected to a server to do this");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clientSocket.Connected)
            {
                clientSocket.Client.Disconnect(false);
                string path = $"{Application.StartupPath}\\{ConnectedIP}\\log.txt";
                Directory.CreateDirectory($"{Application.StartupPath}\\{ConnectedIP}");
                File.WriteAllText(path, OutputTextBox.Text);
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(HandleConnection);
            thread.Start();
        }

        private void HandleConnection()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(HandleConnection));
            }
            else
            {
                try
                {
                    if (!clientSocket.Connected)
                    {
                        int port = 8888;
                        if (!IPAddress.TryParse(IPTextBox.Text, out IPAddress address))
                        {
                            MessageBox.Show("Invalid IP Address", "Error");
                            return;
                        }
                        if (int.TryParse(PortTextBox.Text, out port))
                        {
                            if (port < 1 && port > 65535)
                            {
                                MessageBox.Show("Invalid port number", "Error");
                            }
                        }
                        else
                        {
                            port = 8888;
                        }
                        StatusLbl.Text = "Connecting to server...";
                        clientSocket.Connect(IPTextBox.Text, port);
                        ConnectedIP = IPTextBox.Text;
                        stream = clientSocket.GetStream();
                        byte[] outstream = Encoding.UTF8.GetBytes($"JOIN-AUTHENTIC:{UsernameTextBox.Text}");
                        stream.Write(outstream, 0, outstream.Length);
                        stream.Flush();
                        Username = UsernameTextBox.Text;
                        Thread thread = new Thread(GetMessage);
                        thread.Start();
                        StatusLbl.Text = "Connected";
                        MsgInputBox.Select();
                        if (File.Exists($"{Application.StartupPath}\\{ConnectedIP}\\log.txt"))
                        {
                            FileStream fs = File.OpenRead($"{Application.StartupPath}\\{ConnectedIP}\\log.txt");
                            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                            {
                                OutputTextBox.Text = sr.ReadToEnd();
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("You're already connected to a server.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    StatusLbl.Text = "Connection failed...";
                }
            }
        }

        private void MsgInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MsgInputBox.Focused && e.KeyCode == Keys.Enter)
            {
                SendMessageBtn_Click(sender, e);
            }
        }

        private void OutputTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process p = new Process();
            p = Process.Start(e.LinkText);
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {

        }

        private void PortTextBox_Enter(object sender, EventArgs e)
        {
            if (PortTextBox.Text == "Port... (default 8888)")
            {
                PortTextBox.Text = string.Empty;
            }
        }

        private void PortTextBox_Leave(object sender, EventArgs e)
        {
            if (PortTextBox.Text == string.Empty)
            {
                PortTextBox.Text = "Port... (default 8888)";
            }
        }

        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            if(clientSocket.Connected)
            {
                try
                {
                    clientSocket.Client.Disconnect(false);
                    clientSocket = new TcpClient();
                    string path = $"{Application.StartupPath}\\{ConnectedIP}\\log.txt";
                    Directory.CreateDirectory($"{Application.StartupPath}\\{ConnectedIP}");
                    File.WriteAllText(path, OutputTextBox.Text);
                    OutputTextBox.Text = string.Empty;
                    UserCountLabel.Text = "User Count: N/A";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("You're not connected to a server.", "Error");
            }
        }

        private void IPTextBox_Enter(object sender, EventArgs e)
        {
            if (IPTextBox.Text == "IP Address...")
            {
                IPTextBox.Text = string.Empty;
            }
        }

        private void IPTextBox_Leave(object sender, EventArgs e)
        {
            if (IPTextBox.Text == string.Empty)
            {
                IPTextBox.Text = "IP Address...";
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (AuthenticateLoginInput())
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Database.EnsureCreated();
                    User usercheck = db.Users.FirstOrDefault(x => x.Username == UsernameTextBox.Text);
                    if (usercheck == null)
                    {
                        ErrorLabel.ForeColor = Color.Red;
                        ErrorLabel.Text = "Username/password is incorrect";
                    }
                    else
                    {
                        if (BCrypt.Net.BCrypt.Verify(PasswordTextBox.Text, usercheck.Password))
                        {
                            ToggleLoginWindow(false);
                            User = usercheck;
                        }
                        else
                        {
                            ErrorLabel.ForeColor = Color.Red;
                            ErrorLabel.Text = "Username/password is incorrect";
                        }
                    }
                }
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (AuthenticateInput())
            {
                User user = new User
                {
                    Username = UsernameTextBox.Text,
                    Password = BCrypt.Net.BCrypt.HashPassword(PasswordTextBox.Text),
                    Email = EmailTextBox.Text
                };
                using (DatabaseContext db = new DatabaseContext())
                {
                    User usercheck = db.Users.FirstOrDefault(x => x.Username == UsernameTextBox.Text);
                    if (usercheck == null)
                    {
                        try
                        {
                            db.Users.Add(user);
                            db.SaveChanges();
                            ErrorLabel.ForeColor = Color.Green;
                            ErrorLabel.Text = "Registration successful, you may now login.";
                        }
                        catch (Exception ex)
                        {
                            ErrorLabel.Text = ex.ToString();
                        }
                    }
                    else
                    {
                        ErrorLabel.ForeColor = Color.Red;
                        ErrorLabel.Text = "Username already exists";
                    }
                }
            }
        }

        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.+^:-;`¬'<>,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        private bool AuthenticateInput()
        {
            if (UsernameTextBox.Text.Contains("Developer")) return false;
            if (UsernameTextBox.Text.Contains("Moderator")) return false;
            if (UsernameTextBox.Text.Contains("Administrator")) return false;
            if (UsernameTextBox.Text.Contains("Staff")) return false;
            if (hasSpecialChar(UsernameTextBox.Text)) return false;
            if (UsernameTextBox.Text.Length == 0) return false;
            if (PasswordTextBox.Text.Length == 0) return false;
            if (EmailTextBox.Text.Length == 0) return false;
            if (!EmailTextBox.Text.Contains("@")) return false;

            return true;
        }

        private bool AuthenticateLoginInput()
        {
            if (UsernameTextBox.Text.Length == 0) return false;
            if (PasswordTextBox.Text.Length == 0) return false;

            return true;
        }

        private void UsernameTextBox_Enter(object sender, EventArgs e)
        {
            if (UsernameTextBox.Text == "Username...")
            {
                UsernameTextBox.Text = string.Empty;
            }
        }

        private void UsernameTextBox_Leave(object sender, EventArgs e)
        {
            if (UsernameTextBox.Text == string.Empty)
            {
                UsernameTextBox.Text = "Username...";
            }
        }

        private void EmailTextBox_Enter(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == "Email (if registering)...")
            {
                EmailTextBox.Text = string.Empty;
            }
        }

        private void EmailTextBox_Leave(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == string.Empty)
            {
                EmailTextBox.Text = "Email (if registering)...";
            }
        }

        private void PasswordTextBox_Enter(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == "Password...")
            {
                PasswordTextBox.Text = string.Empty;
                PasswordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void PasswordTextBox_Leave(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == string.Empty)
            {
                PasswordTextBox.Text = "Password...";
                PasswordTextBox.UseSystemPasswordChar = false;
            }
        }
    }
}
