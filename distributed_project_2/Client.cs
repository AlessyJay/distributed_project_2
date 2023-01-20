using System;
using System.Net.Sockets;
using System.Text;

namespace distributed_project_2
{
    class Client
    {
        static void Main(string[] args)
        {
            // Connect to the server.
            TcpClient client = new TcpClient("127.0.0.1", 3000);
            NetworkStream network_stream = client.GetStream();

            // Proceed to read the numbers that have been sent from server and calculate.
            int sum = 0;
            while (true)
            {
                byte[] Data = new byte[1024];
                int readBytes = network_stream.Read(Data, 0, Data.Length);
                string num = Encoding.ASCII.GetString(Data, 0, Data.Length);

                if (num == "FINISHED") break;

                sum += int.Parse(num);
            };

            Console.WriteLine($"Sum: {sum}");

            // Send messages to the server.
            byte[] buffer = Encoding.ASCII.GetBytes("FINISHED");
            network_stream.Write(buffer, 0, buffer.Length);

            // Close the connection.
            network_stream.Close();
            client.Close();
        }
    }
}
