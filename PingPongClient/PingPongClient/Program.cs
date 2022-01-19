using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PingPongClient
{

    class Program
    {

        // Main Method
        static void Main(string[] args)
        {
            var client = new SocketClient();
            client.ExecuteClient();
        }


        
    }
}