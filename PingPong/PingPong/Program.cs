﻿using System;
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
            var serverModule = new ServerModule();
            serverModule.ExecuteServer();
        }

        
    }
}