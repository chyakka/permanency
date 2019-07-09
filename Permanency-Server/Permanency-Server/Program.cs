using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Net;
using System.Data.SQLite;

namespace ConsoleApplication1
{
    class Program
    {
        /* Our hashtable is used to track all of our current clients. */
        public static Hashtable clientsList = new Hashtable();

        public static int UserCount { get; set; }

        static void Main(string[] args)
        {
            try
            {
                /* This can be modified to only listen for certain IP Addresses, simply write out the IP as a string and cast it as an IPAddress */
                TcpListener serverSocket = new TcpListener(IPAddress.Any, 8888);

                serverSocket.Start();
                Console.WriteLine("Server started...");
                /* Our while true loop ensures it goes on forever and that while the server is on it is always accepting TCP connection requests. */
                while (true)
                {
                    TcpClient clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    /* Reading data from the stream & writes it to the console. */
                    string clientData = Encoding.UTF8.GetString(bytesFrom, 0, clientSocket.GetStream().Read(bytesFrom, 0, clientSocket.ReceiveBufferSize));
                    Console.WriteLine($"INCOMING DATA: {clientData}");

                    if (clientData.Length > 15)
                    {
                        /* This was the method to verify what data was used for, I would advice modifying this to something else or 
                         * designing a new solution as people will be able to spoof data if they know the string. */
                        if (clientData.Substring(0, 15).Contains("JOIN-AUTHENTIC:"))
                        {
                            string formattedClientData = clientData.Substring(15);
                            Console.WriteLine($"{formattedClientData} joined");

                            clientsList.Add(formattedClientData, clientSocket);

                            Broadcast($"{formattedClientData} joined", formattedClientData, 1);

                            /* Handle our new clients connection. */
                            HandleClient client = new HandleClient();
                            client.StartClient(clientSocket, formattedClientData, clientsList);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Server has been terminated.");
            }
        }

        public static void Broadcast(string msg, string username, int type)
        {
            try
            {
                foreach (DictionaryEntry client in clientsList)
                {
                    TcpClient broadcastSocket = (TcpClient)client.Value;
                    NetworkStream broadcastStream = broadcastSocket.GetStream();
                    byte[] broadcastBytes = null;

                    switch (type)
                    {
                        case 0:
                            {
                                broadcastBytes = Encoding.UTF8.GetBytes($"{msg}");
                                break;
                            }
                        case 1:
                            {
                                broadcastBytes = Encoding.UTF8.GetBytes($"{msg}");
                                break;
                            }
                        case 2:
                            {
                                string users = string.Empty;
                                foreach (string name in clientsList.Keys)
                                {
                                    users += name + "\n";
                                }
                                broadcastBytes = Encoding.UTF8.GetBytes($"User Count: {msg}\n{users}");
                                break;
                            }
                    }

                    broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                    broadcastStream.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }
    }


    public class HandleClient
    {
        TcpClient clientSocket;
        string clNo;
        Hashtable clientsList;

        public void StartClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            clientSocket = inClientSocket;
            clNo = clineNo;
            clientsList = cList;
            Thread ctThread = new Thread(ProcessBytes);
            ctThread.Start();
            Program.UserCount++;
            Program.Broadcast(Program.UserCount.ToString(), string.Empty, 2);
        }

        private void ProcessBytes()
        {
            byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
            int requestCount = 0;

            while (true)
            {
                if (IsConnected)
                {
                    try
                    {
                        requestCount++;
                        /* Reads our data stream and converts it from bytes > string */
                        string dataFromClient = Encoding.UTF8.GetString(bytesFrom, 0, clientSocket.GetStream().Read(bytesFrom, 0, clientSocket.ReceiveBufferSize));

                        /* Once again verifies what the bytes are suppose to be used for by a unique identifier, would advise changing this or making a new solution. */
                        if (dataFromClient.Contains("VALID-MSG23U8:"))
                        {
                            Console.WriteLine($"From client - {clNo} : {dataFromClient}");
                            if (dataFromClient != string.Empty)
                            {
                                Program.Broadcast(dataFromClient.Replace("VALID-MSG23U8: ", ""), clNo, 0);
                            }
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"{clNo} left");
                    Program.Broadcast($"{clNo} left", clNo, 1);
                    Program.UserCount--;
                    Program.clientsList.Remove(clNo);
                    Program.Broadcast(Program.UserCount.ToString(), "", 2);
                    while (true)
                    {
                        Thread.Sleep(10000);
                    }
                }
            }
        }

        /* Polls the connection to verify if it's still active, if not it'll class them as disconnected, thanks StackOverflow. */
        public bool IsConnected
        {
            get
            {
                try
                {
                    if (clientSocket != null && clientSocket.Client != null && clientSocket.Client.Connected)
                    {
                        if (clientSocket.Client.Poll(0, SelectMode.SelectRead))
                        {
                            byte[] buff = new byte[1];
                            if (clientSocket.Client.Receive(buff, SocketFlags.Peek) == 0)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}