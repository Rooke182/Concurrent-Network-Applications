using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProj
{
    internal class ConnectedClient
    {
        Socket _socket;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        object readLock;
        object writeLock;

        public ConnectedClient(Socket socket)
        {
            _socket = socket;
            stream = new NetworkStream(socket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            readLock = new object();
            writeLock = new object();
        }
        public void Close()
        {
            stream.Close();
            reader.Close();
            writer.Close();
            _socket.Close();
        }
        public string Read()
        {
            lock (readLock)
            {
                return reader.ReadLine();
            }
        }
        public void Send(string message)
        {
            lock (writeLock)
            {
                writer.WriteLine(message);
                writer.Flush();
            }
        }
    }
}
