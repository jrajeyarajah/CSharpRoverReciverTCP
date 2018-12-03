using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace NasaRover
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TcpListener server = null;
            Plateau plateau = null;
            List<RoverUnit> roverList = new List<RoverUnit>();

            try
            {
                IPAddress ipAd = IPAddress.Parse("127.0.0.1");

                /* Initializes the Listener */
                server = new TcpListener(ipAd, 9999);

                /* Start Listeneting at the specified port */
                server.Start();

                // Buffer for reading data
                byte[] bytes = new byte[256];
                string data = null;


                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    data = null;

                    // Perform a blocking call to accept requests.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine($"Received: {data}");

                        // Process the data sent by the client.
                        //data = data.ToUpper();


                        if (Regex.IsMatch(data, @"\d+\s\d+$"))
                        {
                            string[] numbers = data.Split(' ');
                            plateau = new Plateau(int.Parse(numbers[0]), int.Parse(numbers[1]));
                        }

                        else if (Regex.IsMatch(data, @"\d+\s\d+\s[NSEW]$"))
                        {                           
                            string[] numbers = data.Split(' ');
                            if (plateau != null)
                            {
                                roverList.Add(new RoverUnit(plateau, numbers[0], numbers[1], numbers[2]));
                            }
                            Console.WriteLine("Set Rover");
                        }
                        else if (Regex.IsMatch(data, @"[MRL]+$"))
                        {
                            if (!roverList.Any())
                            {
                                RoverUnit lastrover = roverList.LastOrDefault();
                                foreach (char c in data)
                                {
                                    lastrover.processMessage(c);
                                }
                            }

                            Console.WriteLine("Rover moved");
                        }

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        //Console.WriteLine("Sent: {0}", data); 
                    }
                    Console.WriteLine("Rover status");
                    foreach (RoverUnit rv in roverList)
                        {
                            Console.WriteLine(rv.RoverStatus());

                        }
                    plateau.SetObstacle(3, 4);
                    plateau.SetObstacle(2, 4);
                    plateau.SetObstacle(2, 3);
                    plateau.SetObstacle(2, 2);
                    plateau.SetObstacle(3, 2);

                    plateau.Status();

                    // Shutdown and end connection
                    client.Close();


                    roverList.Clear();
                    plateau = null;


                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }









            //try
            //{
            //    Plateau plateau = new Plateau(5, 5);

            //    RoverUnit rover1 = new RoverUnit(plateau,"1","2","N", "Rover1");
            //    Console.WriteLine($"Starting {rover1.RoverStatus()}");
            //    foreach (char c in "LMLMLMLMM")
            //        rover1.processMessage(c);
            //    Console.WriteLine(rover1.RoverStatus());

            //    RoverUnit rover2 = new RoverUnit(plateau, "3", "3", "E", "Rover2");
            //    Console.WriteLine($"Starting {rover2.RoverStatus()}");
            //    foreach (char c in "MMRMMRMRRM")
            //        rover2.processMessage(c);
            //    Console.WriteLine(rover2.RoverStatus());

            //    RoverUnit rover3 = new RoverUnit(plateau, "5", "2", "N", "Rover3");
            //    Console.WriteLine($"Starting {rover3.RoverStatus()}");
            //    foreach (char c in "LMLMLMLMM")
            //        rover3.processMessage(c);
            //    Console.WriteLine(rover3.RoverStatus());

            //    RoverUnit rover4 = new RoverUnit(plateau, "3", "3", "E", "Rover4");
            //    Console.WriteLine($"Starting {rover4.RoverStatus()}");
            //    foreach (char c in "MMRMMRMRRM")
            //        rover4.processMessage(c);
            //    Console.WriteLine(rover4.RoverStatus());

            //    RoverUnit rover5 = new RoverUnit(plateau, "3", "3", "W", "Rover5");
            //    Console.WriteLine($"Starting {rover5.RoverStatus()}");
            //    foreach (char c in "MMRMMRLMLMLMM")
            //        rover5.processMessage(c);
            //    Console.WriteLine(rover5.RoverStatus());

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Exception {e.GetType().FullName} thrown." );
            //    Console.WriteLine( e.Message);
            //}

            Console.ReadLine();


        }
    }
}
