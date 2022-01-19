using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Server
{
    class Program
    {

        // Main Method
        static void Main(string[] args)
        {
            ExecuteServer();
        }

        public static void ExecuteServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using
            // Socket Class Constructor
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this
                // Server Socket must know this network
                // Address
                listener.Bind(localEndPoint);

                // Using Listen() method we create
                // the Client list that will want
                // to connect to Server
                listener.Listen(10);

                Console.WriteLine("Waiting For Connections ... ");
                while (true)
				{
                    
                    Socket clientSocket = listener.Accept();
					Console.WriteLine("New Connection! Handling.");
                    Task.Run(() =>
                    {
                        HandleClient(clientSocket);
                    });
                }
                    
                
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void HandleClient(Socket clientSocket)
		{
            byte[] bytes = new Byte[1024];
            string data = string.Empty;

			while (data.IndexOf("<CloseSocket>") == -1)
			{
                data = string.Empty;

                while (true)
                {
                    int numByte = clientSocket.Receive(bytes);

                    data += Encoding.ASCII.GetString(bytes,
                                               0, numByte);
                    if (data.IndexOf("<EOF>") > -1)
                        break;
                }
                string dataParsed = data;
                if(data.IndexOf("<CloseSocket>") != -1)
				{
                   dataParsed = dataParsed.Remove(dataParsed.Length - "<CloseSocket>".Length, "<CloseSocket>".Length);
                }
                dataParsed = dataParsed.Remove(dataParsed.Length-6, 6);
                Console.WriteLine("Text received -> {0} ", dataParsed);
                byte[] message = Encoding.ASCII.GetBytes(dataParsed);

                // Send a message to Client
                // using Send() method
                clientSocket.Send(message);

            }
			// Close client Socket using the
			// Close() method. After closing,
			// we can use the closed Socket
			// for a new Client Connection
			Console.WriteLine("Connection Closed");
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}