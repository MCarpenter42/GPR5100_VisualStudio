using System;
using System.Net;
using System.Net.Sockets;

namespace EchoClient
{
    internal class EchoClient
    {
        static void Main(string[] args)
        {
            // Create remote endpoint
            Console.WriteLine("Please enter target server IP address...");
            string ipString = Console.ReadLine();
            IPAddress remoteAddr = IPAddress.Parse(ipString);

            Console.WriteLine("\nPlease enter target server socket...");
            int serverSocketInt = Int32.Parse(Console.ReadLine());
            IPEndPoint destinationEndPoint = new IPEndPoint(remoteAddr, serverSocketInt);

            Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            bool endProcess = false;
            while (!endProcess)
            {
                Console.WriteLine("\n" + "Enter text to send to the server...");
                string text = Console.ReadLine();

                if (text == "exit")
                {
                    endProcess = true;
                }
                else if (text != null)
                {
                    byte[] sendData = System.Text.Encoding.ASCII.GetBytes(text);
                    skt.SendTo(sendData, destinationEndPoint);
                    Console.WriteLine(" > Sending...");
                }
                else
                {
                    Console.WriteLine(" > Error: no text present");
                }
                Console.WriteLine("");
            }
        }
    }
}

// 10.44.51.95
// 55555
