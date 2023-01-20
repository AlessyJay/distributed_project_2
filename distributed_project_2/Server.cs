using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace distributed_project_2
{
    class Server
    {
        static void Main(string[] args)
        {
            // We are using TCP socket listener.

            // Create a TCP socket listener to connect with local IP address.
            TcpListener listener = new TcpListener(IPAddress.Any, 3000);
            listener.Start();

            // Waiting for client side to communicate with the server.
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream network_stream = client.GetStream();

            // Generate number and send to client
            while (true)
            {
                Random rand = new Random();
                int num = rand.Next();
                byte[] Data = Encoding.ASCII.GetBytes(num.ToString());
                network_stream.Write(Data, 0, Data.Length);

                // Checking if the client has sent messages to the server.
                if (network_stream.DataAvailable)
                {
                    byte[] buffer = new byte[client.Available];
                    network_stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.ASCII.GetString(buffer);

                    if (message == "FINISHED") break;
                }
            }

            // Close the connection.
            client.Close();
            network_stream.Close();
        }
    }
}
