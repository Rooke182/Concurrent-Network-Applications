using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProj
{
    internal class Server
    {
        private TcpListener tcpListener;

        public Server(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(ip, port);
        }
        public void Start()
        {
            tcpListener.Start();
            Console.WriteLine("Listening...");
            Socket socket = tcpListener.AcceptSocket();
            Console.WriteLine("Connection Made");
            ClientMethod(socket);
        }
        public void Stop()
        {
            tcpListener.Stop();
        }
        private void ClientMethod(Socket socket)
        {
            string recievedMessage;

            string close = "end";

            NetworkStream stream = new NetworkStream(socket, true);
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);

            writer.WriteLine("You Have Connected To The Server - send 0 for valid actions");
            writer.Flush();

            while ((recievedMessage = reader.ReadLine()) != null)
            {
                writer.WriteLine(GetReturnMessage(recievedMessage));
                writer.Flush();

                if (recievedMessage == close)
                {
                    break;
                }
            }
            socket.Close();
        }
        private string GetReturnMessage(string code)
        {
            Console.WriteLine(code);
            return code;
        }
    }
}
