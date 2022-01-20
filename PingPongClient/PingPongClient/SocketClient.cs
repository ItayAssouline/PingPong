using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPongClient
{
	class SocketClient
	{
        public void ExecuteClient()
        {

            try
            {
                Int32 port = 11111;
                TcpClient client = new TcpClient("127.0.0.1", port);

                string message = string.Empty;
                NetworkStream stream = client.GetStream();
                while (message != "exit" )
				{
					Console.WriteLine("Please enter your message | To end enter \"exit\"");
                     message = Console.ReadLine();

                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    data = new Byte[256];
                    string responseData = string.Empty;
                    int bytes = stream.Read(data, 0, data.Length);
                    responseData = Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine($"recieved message: {responseData}");
                    
                }
                stream.Close();
                client.Close();


            }

            catch (Exception e)
            {
                //log
                Console.WriteLine(e.ToString());
            }
        }

    }
}
