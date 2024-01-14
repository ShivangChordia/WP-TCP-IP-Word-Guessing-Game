/*
*  FILE : mainWindow.xaml.cs
*  PROJECT : PROG2121 – WINDOWS PROGRAMMING - ICP
*  PROGRAMMER : Shivang Chordia - schordia1092@conestogac.on.ca - 8871092, Divya Patel - dpatel0488@conestogac.on.ca - 8870488
*  FIRST VERSION : 19th Nov 2023
*  DESCRIPTION : The functions in file contains all the definitions of the code behind of mainWindow.xaml
*/




using A5_shivang_divya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ClientSide_a5
{
    
    public partial class MainWindow : Window
    {
        /* Function:  public MainWindow()
*  Parameters:  
*  Description: This is the constructor of MainWindow
*/
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MyWindow_loaded;
        }


        /* Function:  private void MyWindow_loaded(object sender, RoutedEventArgs e)
*  Parameters:  object sender, RoutedEventArgs e
*  Description: This function contains the logic on loading of the window
*/
        private void MyWindow_loaded(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new dashboard());
        }

    }
}
