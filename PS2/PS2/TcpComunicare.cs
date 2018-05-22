using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PS2
{
    public class TcpComunicare
    {
        TcpClient client = null;
        byte[] buffer_send = new byte[4];
       
        
        public void SendToServer()
        {
            TCP_API.send(client, buffer_send);
        }
        public TcpComunicare()
        {
            buffer_send[0] = 255;
            buffer_send[1] = 255;
            buffer_send[2] = 255;
            buffer_send[3] = 255;
            client = new TcpClient("192.168.0.155", 13000);
            SendToServer();
        }
    }
}
