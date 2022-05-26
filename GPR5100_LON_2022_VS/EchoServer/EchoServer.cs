using System;
using System.Net;
using System.Net.Sockets;

namespace EchoServer
{
    class EchoServer
    {
        static void Main(string[] args)
        {
            // "IPAddress.Any" is equivalent to "IPAddress.Parse("0.0.0.0")"
            IPAddress addr = IPAddress.Any;

            // 55555 is not a reserved port, and so is typically available
            int port = 55555;
            IPEndPoint endPoint = new IPEndPoint(addr, port);

            Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            skt.Bind(endPoint);

            Console.WriteLine("Opening socket, echoing from " + addr.ToString() + ":" + endPoint.Port);

            byte[] buffer = new byte[1024];

            while (true)
            {
                int byteCount = skt.Receive(buffer);
                string text = System.Text.Encoding.ASCII.GetString(buffer, 0, byteCount);
                Console.WriteLine(text);
            }
        }

        static void LookupLoop()
        {
            bool endProcess = false;
            bool firstRun = true;
            while (!endProcess)
            {
                if (firstRun)
                {
                    firstRun = false;
                    Console.WriteLine("< Enter \"exit\" instead of a hostname to terminate the process >");
                }
                endProcess = DNSLookup();
                Console.WriteLine("");
            }
        }

        // DNS service example
        static bool DNSLookup()
        {
            Console.WriteLine("Please enter a hostname...");
            string hostname;
            while (true)
            {
                hostname = Console.ReadLine();
                if (hostname != null)
                {
                    break;
                }
            }

            if (hostname == "exit")
            {
                return true;
            }

            Console.WriteLine("\n" + hostname + " resolves to:");
            IPAddress[] addresses = Dns.GetHostAddresses(hostname);
            foreach (IPAddress addr in addresses)
            {
                Console.WriteLine("- " + addr.ToString());
            }

            return false;
        }
    }
}