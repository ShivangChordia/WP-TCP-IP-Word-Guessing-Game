/*
*  FILE : server.cs
*  PROJECT : PROG2121 – WINDOWS PROGRAMMING - ICP
*  PROGRAMMER : Shivang Chordia - schordia1092@conestogac.on.ca - 8871092, Divya Patel - dpatel0488@conestogac.on.ca - 8870488
*  FIRST VERSION : 19th Nov 2023
*  DESCRIPTION : The functions in file contains all the definitions of the classes related to the server which facilitates the IPC.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ServerSide_a5
{
    internal class server
    {
        /* Function: void StartListener()
*  Parameters: 
*  Description: This function starts the server and connects with the client
*  Returns: none
*/
        internal void StartListener()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();


                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    ParameterizedThreadStart ts = new ParameterizedThreadStart(Worker);
                    Thread clientThread = new Thread(ts);
                    clientThread.Start(client);
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
        }


        /* Function:  public void Worker(Object o)
*  Parameters: objects o
*  Description: This function is the worker for a thread and connect with a single client
*  Returns: none
*/
        public void Worker(Object o)
        {
            TcpClient client = (TcpClient)o;
            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            data = null;

            string relativePath = @"..\..\..\StringS";

            string[] files = Directory.GetFiles(relativePath);
            if (files.Length == 0)
            {
                throw new FileNotFoundException("No text files found in the directory.");
            }

            string TimeLimit = null;

            Random random = new Random();
            string randomFile = files[random.Next(files.Length)];

            // Read content from the selected file
            string[] FileLines = File.ReadAllLines(randomFile);
            List<string> WordsFound = new List<string>();

            if (FileLines.Length < 2)
            {
                throw new InvalidDataException("Invalid file format.");
            }

            // Extract the 80-character string and the number of words
            string characters = FileLines[0].Trim();
            
            int numberOfWords;

            if (!int.TryParse(FileLines[1].Trim(), out numberOfWords))
            {
                throw new InvalidDataException("Invalid number of words format.");
            }

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();
            bool timeLeft = true;
           
            Task timeMonitor = new Task(() =>
            {
                string timeString = TimeLimit;

                if (timeString == "2:00 mins")
                {
                    Thread.Sleep(120000);
                }
                else if (timeString == "3:00 mins")
                {
                    Thread.Sleep(180000);
                }
                else if (timeString == "5:00 mins")
                {
                    Thread.Sleep(300000);
                }
                timeLeft = false;

            });


            int i;
            bool clientDisconnected = false;
            // Loop to receive all the data sent by the client.
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0 && clientDisconnected != true)
                {


                    if (TimeLimit == null)
                    {

                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        TimeLimit = data;
                        timeMonitor.Start();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("String:" + characters + "NumberOfWords:" + numberOfWords);

                        stream.Write(msg, 0, msg.Length);
                        data = System.Text.Encoding.ASCII.GetString(msg, 0, msg.Length);
                        Console.WriteLine("sent: {0}", data);
                    }
                    else
                    {

                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        if (timeLeft == true)
                        {
                            if (data.Contains("Word="))
                            {
                                data = data.Substring(5);

                                byte[] msg = null;

                                if (WordsFound.Contains(data))
                                {
                                    data = "This Word is already guessed";
                                }
                                else if (FileLines.Contains(data))
                                {
                                    for (int j = 2; j < FileLines.Length; j++)
                                    {
                                        if (FileLines[j].Contains(data))
                                        {
                                            FileLines[j] = "";
                                            numberOfWords--;
                                            WordsFound.Add(data);
                                            data = "NumberOfWords:" + numberOfWords.ToString();
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    data = "The Word is not in the String.";
                                }

                                msg = System.Text.Encoding.ASCII.GetBytes(data);
                                stream.Write(msg, 0, msg.Length);
                                Console.WriteLine("sent: {0}", data);

                            }
                            else if (data == "ClientWon" )
                            {
                                clientDisconnected = true;

                            }
                            else if(data == "EndGame")
                            {
                                clientDisconnected = true;
                                data = "Confirm?";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                                stream.Write(msg, 0, msg.Length);
                                Console.WriteLine("sent: {0}", data);
                            }
                            else if(data =="Yes")
                            {
                                clientDisconnected = true;
                            }
                        }
                        else
                        {
                            data = "TimeOver";
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                            stream.Write(msg, 0, msg.Length);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public bool timeWorker(object o)
        {
            string timeString = (string)o;
            bool status = false;
            if(timeString =="2:00 mins")
            {
                Thread.Sleep(120000);
            }
            else if (timeString == "3:00 mins")
            {
                Thread.Sleep(180000);
            }
            else if (timeString == "5:00 mins")
            {
                Thread.Sleep(300000);
            }
            return status;
        }
    }
}





