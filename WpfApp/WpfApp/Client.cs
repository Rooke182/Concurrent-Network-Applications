using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClientProj
{
    internal class Client
    {
        TcpClient tcpClient;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;
        MainWindow form;

        public Client()
        {
            tcpClient = new TcpClient();
        }

        public bool Connect(string ipAddress, int port)
        {
            try
            {
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                return false;
            }
        }
        public void Run()
        {
            string userInput;

            form = new MainWindow(new Client());

            Thread thread = new Thread(() => {  });

            //ProcessServerResponse();
            //while ((userInput = Console.ReadLine()) != null)
            //{
            //    writer.WriteLine(userInput);
            //    writer.Flush();
            //    if (userInput == "end")
            //    {
            //        break;
            //    }
            //}
            //tcpClient.Close();
        }
        private void ProcessServerResponse()
        {
            Console.WriteLine("Server says: " + reader.ReadLine());
            Console.WriteLine();
        }
    }
}
