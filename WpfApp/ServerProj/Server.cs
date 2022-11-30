using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerProj
{
    internal class Server
    {
        private TcpListener tcpListener;
        ConcurrentDictionary<int, ConnectedClient> clients;

        public Server(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(ip, port);
        }
        public void Start()
        {
            clients = new ConcurrentDictionary<int, ConnectedClient>();
            int clientIndex = 0;

            tcpListener.Start();
            Console.WriteLine("Listening...");
            while (true)
            {
                Socket socket = tcpListener.AcceptSocket();
                Console.WriteLine("Connection Made");
                ConnectedClient client = new ConnectedClient(socket);
                int index = clientIndex;
                clientIndex++;
                clients.TryAdd(index, client);
                Thread thread = new Thread(() => { ClientMethod(index); });
                thread.Start();
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private void ClientMethod(int index)
        {
            string recievedMessage;

            string close = "end";

            clients[index].Send("You Have Connected To The Server - send 0 for valid actions");

            while ((recievedMessage = clients[index].Read()) != null)
            {
                clients[index].Send(GetReturnMessage(recievedMessage));

                if (recievedMessage == close)
                {
                    break;
                }
            }
            clients[index].Close();
            ConnectedClient c;
            clients.TryRemove(index, out c);
        }
        private string GetReturnMessage(string code)
        {
            Console.WriteLine(code);
            return code;
        }
    }
}
