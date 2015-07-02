using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace tictactoeApp
{
    /// <summary>
    /// The main page for the game.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //Global Variables
        public static string playerUri;
        public static string playerKey;
        public static string playerName;
        public static string serviceName;

        //Reset variables on page load.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            playerUri = null;
            playerKey = null;
            playerName = null;
            serviceName = null;
        }

        //Function to display URI and API key for the selected
        //mobile service.
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //If the .NET radio button is selected.
            if (netRadio.IsChecked == true)
            {
                //Hide the javascript window.
                jsGrid.Visibility = Visibility.Collapsed;
                //Display the .NET window.
                netGrid.Visibility = Visibility.Visible;
                //Set the uri.
                playerUri = netUri.Text;
                //Set the key.
                playerKey = netKey.Text;
                //Set the service name.
                serviceName = "net";
            }
            //Else, if the javascript radio button is selected.
            else if (jsRadio.IsChecked == true)
            {
                //Hide the .NET window.
                netGrid.Visibility = Visibility.Collapsed;
                //Display the javascript window.
                jsGrid.Visibility = Visibility.Visible;
                //Set the uri.
                playerUri = jsUri.Text;
                //Set the key.
                playerKey = jsKey.Text;
                //Set the service name.
                serviceName = "js";
            }
        }
        
        private void human_Click(object sender, RoutedEventArgs e)
        {
            //Set the starting player.
            playerName = "human";
        }

        private void computer_Click(object sender, RoutedEventArgs e)
        {
            //Set the starting player.
            playerName = "service";
        }

        //Function to start the game.
        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            //If user has selected all required options.
            if(playerName != null && playerUri != null && playerKey != null)
            {
                Player s = new Player() { startingPlayer = playerName, uri = playerUri, key = playerKey, name = serviceName }; 
                //Open the game window and pass player input.
                this.Frame.Navigate(typeof(Game), s);
            }
            else
            {
                //Display error message.
                MessageDialog dialog = new MessageDialog("Please choose a service and 1st player to start the game.");
                await dialog.ShowAsync();
            }
        }

    }
}
