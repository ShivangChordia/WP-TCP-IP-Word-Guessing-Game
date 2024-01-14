/*
*  FILE : dashboard.xaml.cs
*  PROJECT : PROG2121 – WINDOWS PROGRAMMING - ICP
*  PROGRAMMER : Shivang Chordia - schordia1092@conestogac.on.ca - 8871092, Divya Patel - dpatel0488@conestogac.on.ca - 8870488
*  FIRST VERSION : 19th Nov 2023
*  DESCRIPTION : The functions in file contains all the definitions of the code behind of dashboard.xaml
*/




using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A5_shivang_divya
{
    public partial class dashboard : Page
    {
        public dashboard()
        {
            InitializeComponent();
        }


        /* Function:  public void StartGame_Click(object sender, RoutedEventArgs e)
*  Parameters:  object sender, RoutedEventArgs e
*  Description: This function contains the logic on event of Start Game click
*  Returns: none
*/
        public void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IPAddressTextBox.Text) || !IPAddress.TryParse(IPAddressTextBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid IP address.");
                return;
            }

            // Validate Port Number
            if (string.IsNullOrWhiteSpace(PortNumberTextBox.Text) || !int.TryParse(PortNumberTextBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid port number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter a Player Name.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TimeComboBox.Text))
            {
                MessageBox.Show("Please enter a valid Time Duration.");
                return;
            }

            TcpClient client = null;

            try
            {
                client = new TcpClient(IPAddressTextBox.Text, int.Parse(PortNumberTextBox.Text));
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Cannot connect to server, Try Again?");
            }
            

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(TimeComboBox.Text);

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(bytes, 0, bytes.Length);

            gamePage gamePage = new gamePage(client);

            gamePage.NameHolder.Content = "Welcome! " + NameTextBox.Text;

            // Set gamePage content in MainWindow
            NavigationService.Navigate(gamePage);
        }
    }
}
