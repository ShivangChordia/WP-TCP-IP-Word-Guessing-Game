/*
*  FILE : gamePage.xaml.cs
*  PROJECT : PROG2121 – WINDOWS PROGRAMMING - ICP
*  PROGRAMMER : Shivang Chordia - schordia1092@conestogac.on.ca - 8871092, Divya Patel - dpatel0488@conestogac.on.ca - 8870488
*  FIRST VERSION : 19th Nov 2023
*  DESCRIPTION : The functions in file contains all the definitions of the code behind of gamePage.xaml
*/




using ClientSide_a5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A5_shivang_divya
{
    
    public partial class gamePage : Page
    {

        private TcpClient tcpConnection;


        /* Function:  public gamePage(TcpClient connection)
*  Parameters:  (TcpClient connection)
*  Description: This is constructor for the gamePage.xaml page
*  Returns: none
*/
        public gamePage(TcpClient connection)
        {
            InitializeComponent();
            tcpConnection = connection;
            

            Byte[] bytes = new Byte[256];
            String data = null;
            NetworkStream stream = tcpConnection.GetStream();

            StringBuilder receivedData = new StringBuilder();
            int bytesRead;
            do
            {
                bytesRead = stream.Read(bytes, 0, bytes.Length);
                receivedData.Append(System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead));
            } while (stream.DataAvailable);

            data = receivedData.ToString();
            if (bytesRead > 0)
            {
                int stringPos = data.IndexOf("String:");
                int numOfWordsPos = data.IndexOf("NumberOfWords:");

                if (stringPos != -1 && numOfWordsPos != -1)
                {
                    // Extract the string of 80 characters
                    int stringStart = stringPos + "String:".Length;
                    int stringLength = Math.Min(80, numOfWordsPos - stringStart);
                    string stringData = data.Substring(stringStart, stringLength).Trim();

                    // Extract the number of words
                    int numOfWordsStart = numOfWordsPos + "NumberOfWords:".Length;
                    string numOfWordsPart = data.Substring(numOfWordsStart).Trim();

                    // Update gamePage
                    StringHolder.Text = stringData;
                    NumberOfWordsTextBlock.Text = numOfWordsPart;
                    

                }
                else if (numOfWordsPos != -1)
                {
                    int numOfWordsStart = numOfWordsPos + "NumberOfWords:".Length;
                    string numOfWordsPart = data.Substring(numOfWordsStart).Trim();

                    // Update gamePage
                    NumberOfWordsTextBlock.Text = numOfWordsPart;
   
                }  
            }
        }


        /* Function:  private void guessButton_Click(object sender, RoutedEventArgs e)
*  Parameters:  object sender, RoutedEventArgs e
*  Description: This function contains the logic on event of guessButton click
*  Returns: none
*/
        private void guessButton_Click(object sender, RoutedEventArgs e)
        {

            Byte[] bytes = new Byte[256];
            String data = GuessInputTextBox.Text;

            bytes = System.Text.Encoding.ASCII.GetBytes("Word="+data);

            // Get a stream object for reading and writing
            NetworkStream stream = tcpConnection.GetStream();

            stream.Write(bytes, 0, bytes.Length);

            Array.Clear(bytes, 0, bytes.Length);

            StringBuilder receivedData = new StringBuilder();
            int bytesRead;
            do
            {
                bytesRead = stream.Read(bytes, 0, bytes.Length);
                receivedData.Append(System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead));
            } while (stream.DataAvailable);

            data = receivedData.ToString();
            
            if (bytesRead > 0)
            {
                
                if(data=="TimeOver")
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to play Again?", "TimeOut", MessageBoxButton.YesNo);
                    if(result == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new dashboard());
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Application.Current.Shutdown();
                    }
                }
                int numOfWordsPos = data.IndexOf("NumberOfWords:");
                if (numOfWordsPos != -1)
                {
                    int numOfWordsStart = numOfWordsPos + "NumberOfWords:".Length;
                    string numOfWordsPart = data.Substring(numOfWordsStart);

                    NumberOfWordsTextBlock.Text = numOfWordsPart;
                    infoBox.Text = "Your word was Correct!";
                }
                else
                {
                    infoBox.Text = data;
                }

                if (NumberOfWordsTextBlock.Text == "0")
                {
                    data = "ClientWon";
                    bytes = System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(bytes, 0, bytes.Length);

                    stream.Close();
                    tcpConnection.Close();

                    NavigationService.Navigate(new winningPage());
                    
                }
                GuessInputTextBox.Text = "";
            }
        }


        /* Function:  private void EndButton_Click(object sender, RoutedEventArgs e)
*  Parameters:  object sender, RoutedEventArgs e
*  Description: This function contains the logic on event of EndButton click
*  Returns: none
*/
        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            Byte[] bytes = new Byte[256];
            String data = "EndGame";

            bytes = System.Text.Encoding.ASCII.GetBytes(data);

            // Get a stream object for reading and writing
            NetworkStream stream = tcpConnection.GetStream();

            stream.Write(bytes, 0, bytes.Length);

            Array.Clear(bytes, 0, bytes.Length);

            StringBuilder receivedData = new StringBuilder();
            int bytesRead;
            do
            {
                bytesRead = stream.Read(bytes, 0, bytes.Length);
                receivedData.Append(System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead));
            } while (stream.DataAvailable);

            data = receivedData.ToString();
            
            if(bytesRead >0)
            {
                if (data == "Confirm?")
                {
                    MessageBoxResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        data = "Yes";
                        bytes = System.Text.Encoding.ASCII.GetBytes(data);
                        stream.Write(bytes, 0, bytes.Length);
                        Application.Current.Shutdown();
                    }
                     
                }

            }

           
        }
    }
}
