using System;
using System.Collections.Generic;
using System.Linq;
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
    
    public partial class winningPage : Page
    {
        /* Function:  public winningPage()
*  Parameters: 
*  Description: This is constructor for winningPage
*  Returns: none
*/
        public winningPage()
        {
            InitializeComponent();
        }

        /* Function:  void PlayAgainButton_Click(object sender, RoutedEventArgs e)
*  Parameters:  (object sender, RoutedEventArgs e)
*  Description: This function does the logic on Play button click event
*  Returns: none
*/
        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new dashboard());

            
        }
    }
}
