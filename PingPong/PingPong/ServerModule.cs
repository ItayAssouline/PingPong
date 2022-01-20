using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
	class ServerModule
	{
        public void ExecuteServer()
        {

            string serverIP = "127.0.0.1";
            int port = 11111;
            try
            {
                Console.WriteLine("Initializing.....");
                TcpListener server = new TcpListener(IPAddress.Parse(serverIP), port);
                server.Start();
                Console.WriteLine($"Server is ready and waiting for connections on IP: {serverIP} & Port {port}");

                while (true)
                {
                    var client = server.AcceptTcpClient();
                    Task.Run(() => {
                        HandleClient(client);
                    });

                }
                
                server.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void HandleClient(TcpClient client)
        {
            Console.WriteLine("Connected!");
            Byte[] bytes = new Byte[256];
            String data = null;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] message = Encoding.ASCII.GetBytes($"{data}");

                // Send back a response.
                stream.Write(message, 0, message.Length);
                Console.WriteLine("Sent: {0}", data);
            }

			// Shutdown and end connection
			Console.WriteLine("Closes Connection");
            client.Close();

        }
    }
}
