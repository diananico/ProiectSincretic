using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PS2
{
    public class TCP_API
    {
        public static void send(TcpClient client, byte[] buffer)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(buffer, 0, buffer.Length);

        }
        public static int receive(TcpClient client, ref byte[] buffer)
        {
            NetworkStream stream = client.GetStream();
            int i = stream.Read(buffer, 0, buffer.Length);
            return i;
        }
    }
}
