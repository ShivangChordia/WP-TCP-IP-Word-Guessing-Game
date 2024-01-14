/*
*  FILE : program.cs
*  PROJECT : PROG2121 – WINDOWS PROGRAMMING - ICP
*  PROGRAMMER : Shivang Chordia - schordia1092@conestogac.on.ca - 8871092, Divya Patel - dpatel0488@conestogac.on.ca - 8870488
*  FIRST VERSION : 19th Nov 2023
*  DESCRIPTION : The functions in file contains instantiation of the server class
*/




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide_a5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            server listener = new server();
            listener.StartListener();

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
