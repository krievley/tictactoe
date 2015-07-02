using System;
using System.Collections.Generic;
using System.Net;
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
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Windows.UI;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace tictactoeApp
{
    /// <summary>
    /// Page that shows the game board and handles the functions.
    /// </summary>
    public sealed partial class Game : Page
    {
        public Game()
        {
            this.InitializeComponent();
        }

        //Global Variables
        //Uri of the chosen service.
        public static string serviceUri;
        //API Key of the chosen service.
        public static string serviceKey;
        //Player one's identity.
        public static string player1;
        //Player two's identity.
        public static string player2;
        //Name of the chosen service.
        public static string serviceName;
        //Name of the player's piece.
        public static string piece;
        //The variable to hold the current move.
        public static int move = 1;
        //Variable to hold the current player's identity.
        public static string currentPlayer;
        //Variable to hold the result of the mobile service request.
        public JToken result { get; set; }
        
        //Set variables on game load.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Get the input passed to view.
            Player s = (Player)e.Parameter;
            //Pass the uri to global variable.
            serviceUri = s.uri;
            //Pass the key to global variable.
            serviceKey = s.key;
            //Pass the starting player to global variable.
            player1 = s.startingPlayer;
            //Pass the service name to variable.
            serviceName = s.name;
            //Reset the move.
            move = 1;

            //If player 1 is human.
            if(player1 == "human")
            {
                //Player 2 is the service.
                player2 = "service";
            }
            //If player 1 is the service
            else
            {
                //Player 2 is human.
                player2 = "human";
            }

            //Call the turn function.
            turn();
        }

        //Function to update the game board.
        private void updateGridButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the button that was clicked.
            Button b = sender as Button;
            //Disable the grid.
            grid.IsHitTestVisible = false;
            updateBoard(b.Name, piece);
            //Increment the move.
            move++;
            //Call the turn function.
            turn();
        }

        //Function to update the board.
        private void updateBoard(string position, string play)
        {
            if (position == "one")
            {
                //Disable the button.
                one.IsEnabled = false;
                //Change the background to the player's piece.
                one.Content = play;
                //Update the piece color to white.
                one.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "two")
            {
                //Disable the button.
                two.IsEnabled = false;
                //Change the background to the player's piece.
                two.Content = play;
                //Update the piece color to white.
                two.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "three")
            {
                //Disable the button.
                three.IsEnabled = false;
                //Change the background to the player's piece.
                three.Content = play;
                //Update the piece color to white.
                three.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "four")
            {
                //Disable the button.
                four.IsEnabled = false;
                //Change the background to the player's piece.
                four.Content = play;
                //Update the piece color to white.
                four.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "five")
            {
                //Disable the button.
                five.IsEnabled = false;
                //Change the background to the player's piece.
                five.Content = play;
                //Update the piece color to white.
                five.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "six")
            {
                //Disable the button.
                six.IsEnabled = false;
                //Change the background to the player's piece.
                six.Content = play;
                //Update the piece color to white.
                six.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "seven")
            {
                //Disable the button.
                seven.IsEnabled = false;
                //Change the background to the player's piece.
                seven.Content = play;
                //Update the piece color to white.
                seven.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "eight")
            {
                //Disable the button.
                eight.IsEnabled = false;
                //Change the background to the player's piece.
                eight.Content = play;
                //Update the piece color to white.
                eight.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (position == "nine")
            {
                //Disable the button.
                nine.IsEnabled = false;
                //Change the background to the player's piece.
                nine.Content = play;
                //Update the piece color to white.
                nine.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        //Function to quit the game.
        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            //Return to the home page.
            this.Frame.Navigate(typeof(MainPage));
        }

        //Function to update turn order.
        public void turn()
        {
            //If the move is odd.
            if (move % 2 != 0)
            {
                //It is the 1st player's turn.
                //Hide the 2nd player's title, name, progress, and image.
                player2Title.Visibility = Visibility.Collapsed;
                player2Img.Visibility = Visibility.Collapsed;
                humanName2.Visibility = Visibility.Collapsed;
                azureName2.Visibility = Visibility.Collapsed;
                netName2.Visibility = Visibility.Collapsed;
                jsName2.Visibility = Visibility.Collapsed;
                progress2.Visibility = Visibility.Collapsed;
                progressTitle2.Visibility = Visibility.Collapsed;

                //Display the 1st player's title, name, and image.
                player1Title.Visibility = Visibility.Visible;
                player1Img.Visibility = Visibility.Visible;
                //Set the piece.
                piece = "X";

                if (player1 == "human")
                {
                    //Enable grid so user can play.
                    grid.IsHitTestVisible = true;
                    //Display the human name title.
                    humanName.Visibility = Visibility.Visible;
                }
                else if (player1 == "service")
                {
                    //Display the azure name title.
                    azureName.Visibility = Visibility.Visible;
                    //Display the progress bar.
                    progress1.Visibility = Visibility.Visible;
                    //Display the progress title.
                    progressTitle1.Visibility = Visibility.Visible;

                    //Identify which service it is.
                    if (serviceName == "net")
                    {
                        //Display the net name.
                        netName.Visibility = Visibility.Visible;
                    }
                    if (serviceName == "js")
                    {
                        //Display the js name.
                        jsName.Visibility = Visibility.Visible;
                    }
                    //Call the mobile service.
                    callService();
                }
            }
            //If the move is odd.
            else if (move % 2 == 0)
            {
                //It is the 2nd player's turn.
                //Hide the 1st player's title, name, progress, and image.
                player1Title.Visibility = Visibility.Collapsed;
                player1Img.Visibility = Visibility.Collapsed;
                humanName.Visibility = Visibility.Collapsed;
                azureName.Visibility = Visibility.Collapsed;
                netName.Visibility = Visibility.Collapsed;
                jsName.Visibility = Visibility.Collapsed;
                progress1.Visibility = Visibility.Collapsed;
                progressTitle1.Visibility = Visibility.Collapsed;

                //Display the 1st player's title and name, image.
                player2Title.Visibility = Visibility.Visible;
                player2Img.Visibility = Visibility.Visible;
                //Set the piece.
                piece = "O";

                //If the player is human...
                if (player2 == "human")
                {
                    //Enable grid so user can play.
                    grid.IsHitTestVisible = true;
                    //Display the human name title.
                    humanName2.Visibility = Visibility.Visible;
                }
                //If the player is a mobile service...
                else if (player2 == "service")
                {
                    //Display the azure name title.
                    azureName2.Visibility = Visibility.Visible;
                    //Display the progress bar.
                    progress2.Visibility = Visibility.Visible;
                    //Display the progress title.
                    progressTitle2.Visibility = Visibility.Visible;

                    //Identify which service it is.
                    if (serviceName == "net")
                    {
                        //Display the net name.
                        netName2.Visibility = Visibility.Visible;
                    }
                    if (serviceName == "js")
                    {
                        //Display the js name.
                        jsName2.Visibility = Visibility.Visible;
                    }
                    //Call the mobile service.
                    callService();
                }
            }
        }

        //Call the javascript service.
        private async void callService()
        {
            //Store the board into a jobject.
            var data = new JObject();
            data.Add("one", one.Content.ToString());
            data.Add("two", two.Content.ToString());
            data.Add("three", three.Content.ToString());
            data.Add("four", four.Content.ToString());
            data.Add("five", five.Content.ToString());
            data.Add("six", six.Content.ToString());
            data.Add("seven", seven.Content.ToString());
            data.Add("eight", eight.Content.ToString());
            data.Add("nine", nine.Content.ToString());
            data.Add("piece", piece);

            try
            {
                // Asynchronously call the custom API using the POST method.
                //If the service name is javascript.
                if (serviceName == "js")
                {
                    result = await App.jsMobileService.InvokeApiAsync("executemove", data);
                }
                //If the service name is .net
                else if (serviceName == "net")
                {
                    result = await App.netMobileService.InvokeApiAsync("executemove", data);
                }
                //Store the winner in a string variable.
                var winner = result.SelectToken("winner").ToString();
                //Store the move in a string variable.
                var move = result.SelectToken("move").ToString();
                //Process the service's response.
                //If the service returned a move.
                if (move != "n/a")
                {
                    //Call the api move function.
                    apiMove(move);
                }
                //If a winner is returned.
                if (winner != "inconclusive")
                {
                    //Call the declare winner function.
                    declareWinner(winner);
                }
            }
            //Catch any errors.
            catch (MobileServiceInvalidOperationException ex)
            {
                var message = ex.Message;
                MessageDialog dialog = new MessageDialog(message);

            }
            
        }
        //Function to process the move returned from the service.
        public void apiMove(string apiMove)
        {
            //Update the board.
            updateBoard(apiMove, piece);
            //Call the next turn.
            move++;
            turn();
        }
    
        //Declare a winner.
        public void declareWinner(string winner)
        {
            //If there is a winner...
            //Update all the pieces to a gray color.
            one.Foreground = new SolidColorBrush(Colors.DarkGray);
            two.Foreground = new SolidColorBrush(Colors.DarkGray);
            three.Foreground = new SolidColorBrush(Colors.DarkGray);
            four.Foreground = new SolidColorBrush(Colors.DarkGray);
            five.Foreground = new SolidColorBrush(Colors.DarkGray);
            six.Foreground = new SolidColorBrush(Colors.DarkGray);
            seven.Foreground = new SolidColorBrush(Colors.DarkGray);
            eight.Foreground = new SolidColorBrush(Colors.DarkGray);
            nine.Foreground = new SolidColorBrush(Colors.DarkGray);
            //If the first player is the winner.
            if (winner == "X")
            {
                //Disable the grid.
                grid.IsHitTestVisible = false;
                //Hide the 2nd player's title, name, progress, and image.
                player2Title.Visibility = Visibility.Collapsed;
                player2Img.Visibility = Visibility.Collapsed;
                humanName2.Visibility = Visibility.Collapsed;
                azureName2.Visibility = Visibility.Collapsed;
                netName2.Visibility = Visibility.Collapsed;
                jsName2.Visibility = Visibility.Collapsed;
                progress2.Visibility = Visibility.Collapsed;
                progressTitle2.Visibility = Visibility.Collapsed;

                //Display the 1st player's title, name, and image.
                player1Title.Visibility = Visibility.Visible;
                player1Img.Visibility = Visibility.Visible;
                //Hide the progress.
                progress1.Visibility = Visibility.Collapsed;
                progressTitle1.Visibility = Visibility.Collapsed;
                //If the player is human...
                if (player1 == "human")
                {
                    //Display the human name title.
                    humanName.Visibility = Visibility.Visible;
                }
                //If the player is a mobile service...
                else if (player1 == "service")
                {
                    //Display the azure name title.
                    azureName.Visibility = Visibility.Visible;

                    //Identify which service it is.
                    if (serviceName == "net")
                    {
                        //Display the net name.
                        netName.Visibility = Visibility.Visible;
                    }
                    if (serviceName == "js")
                    {
                        //Display the js name.
                        jsName.Visibility = Visibility.Visible;
                    }
                }

                //Display winner title and arrow.
                winnerTitle.Visibility = Visibility.Visible;
                left.Visibility = Visibility.Visible;
                //Display winning tiles in green.
                //If the first row contains the same pieces.
                if (one.Content.ToString() == "X" && two.Content.ToString() == "X" && three.Content.ToString() == "X")
                {
                    //Display that row in Green.
                    one.Foreground = new SolidColorBrush(Colors.LightGreen);
                    two.Foreground = new SolidColorBrush(Colors.LightGreen);
                    three.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    h1.Visibility = Visibility.Visible;
                }
                //If the second row contains the same pieces.
                else if (four.Content.ToString() == "X" && five.Content.ToString() == "X" && six.Content.ToString() == "X")
                {
                    //Display that row in Green.
                    four.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    six.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    h2.Visibility = Visibility.Visible;
                }
                //If the third row contains the same pieces.
                else if (seven.Content.ToString() == "X" && eight.Content.ToString() == "X" && nine.Content.ToString() == "X")
                {
                    //Display that row in Green.
                    seven.Foreground = new SolidColorBrush(Colors.LightGreen);
                    eight.Foreground = new SolidColorBrush(Colors.LightGreen);
                    nine.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    h3.Visibility = Visibility.Visible;
                }
                //If the first column contains the same pieces.
                else if (one.Content.ToString() == "X" && four.Content.ToString() == "X" && seven.Content.ToString() == "X")
                {
                    //Display that column in Green.
                    one.Foreground = new SolidColorBrush(Colors.LightGreen);
                    four.Foreground = new SolidColorBrush(Colors.LightGreen);
                    seven.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    v1.Visibility = Visibility.Visible;
                }
                //If the second column contains the same pieces.
                else if (two.Content.ToString() == "X" && five.Content.ToString() == "X" && eight.Content.ToString() == "X")
                {
                    //Display that column in Green.
                    two.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    eight.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    v2.Visibility = Visibility.Visible;
                }
                //If the third column contains the same pieces.
                else if (three.Content.ToString() == "X" && six.Content.ToString() == "X" && nine.Content.ToString() == "X")
                {
                    //Display that column in Green.
                    three.Foreground = new SolidColorBrush(Colors.LightGreen);
                    six.Foreground = new SolidColorBrush(Colors.LightGreen);
                    nine.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    v3.Visibility = Visibility.Visible;
                }
                //If the diagonal cells contains the same pieces.
                else if (one.Content.ToString() == "X" && five.Content.ToString() == "X" && nine.Content.ToString() == "X")
                {
                    //Display those cells in Green.
                    one.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    nine.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    d1.Visibility = Visibility.Visible;
                }
                //If the diagonal cells contains the same pieces.
                else if (three.Content.ToString() == "X" && five.Content.ToString() == "X" && seven.Content.ToString() == "X")
                {
                    //Display those cells in Green.
                    three.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    seven.Foreground = new SolidColorBrush(Colors.LightGreen);
                    //Show the line through the winning pieces.
                    d2.Visibility = Visibility.Visible;
                }
            }
            //If the second player is the winner.
            else if (winner == "O")
            {
                //Disable the grid.
                grid.IsHitTestVisible = false;
                //Hide the 1st player's title, name, progress, and image.
                player1Title.Visibility = Visibility.Collapsed;
                player1Img.Visibility = Visibility.Collapsed;
                humanName.Visibility = Visibility.Collapsed;
                azureName.Visibility = Visibility.Collapsed;
                netName.Visibility = Visibility.Collapsed;
                jsName.Visibility = Visibility.Collapsed;
                progress1.Visibility = Visibility.Collapsed;
                progressTitle1.Visibility = Visibility.Collapsed;

                //Display the 2nd player's title, name, and image.
                player2Title.Visibility = Visibility.Visible;
                player2Img.Visibility = Visibility.Visible;
                //Hide the 2nd player's progress.
                progress2.Visibility = Visibility.Collapsed;
                progressTitle2.Visibility = Visibility.Collapsed;
                //If the player is human...
                if (player2 == "human")
                {
                    //Display the human name title.
                    humanName2.Visibility = Visibility.Visible;
                }
                //If the player is a mobile service...
                else if (player2 == "service")
                {
                    //Display the azure name title.
                    azureName2.Visibility = Visibility.Visible;

                    //Identify which service it is.
                    if (serviceName == "net")
                    {
                        //Display the net name.
                        netName2.Visibility = Visibility.Visible;
                    }
                    if (serviceName == "js")
                    {
                        //Display the js name.
                        jsName2.Visibility = Visibility.Visible;
                    }
                }

                //Display winner title and arrow.
                winnerTitle.Visibility = Visibility.Visible;
                right.Visibility = Visibility.Visible;
                if (one.Content.ToString() == "O" && two.Content.ToString() == "O" && three.Content.ToString() == "O")
                {
                    one.Foreground = new SolidColorBrush(Colors.LightGreen);
                    two.Foreground = new SolidColorBrush(Colors.LightGreen);
                    three.Foreground = new SolidColorBrush(Colors.LightGreen);
                    h1.Visibility = Visibility.Visible;
                }
                else if (four.Content.ToString() == "O" && five.Content.ToString() == "O" && six.Content.ToString() == "O")
                {
                    four.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    six.Foreground = new SolidColorBrush(Colors.LightGreen);
                    h2.Visibility = Visibility.Visible;
                }
                else if (seven.Content.ToString() == "O" && eight.Content.ToString() == "O" && nine.Content.ToString() == "O")
                {
                    seven.Foreground = new SolidColorBrush(Colors.LightGreen);
                    eight.Foreground = new SolidColorBrush(Colors.LightGreen);
                    nine.Foreground = new SolidColorBrush(Colors.LightGreen);
                    h3.Visibility = Visibility.Visible;
                }
                else if (one.Content.ToString() == "O" && four.Content.ToString() == "O" && seven.Content.ToString() == "O")
                {
                    one.Foreground = new SolidColorBrush(Colors.LightGreen);
                    four.Foreground = new SolidColorBrush(Colors.LightGreen);
                    seven.Foreground = new SolidColorBrush(Colors.LightGreen);
                    v1.Visibility = Visibility.Visible;
                }
                else if (two.Content.ToString() == "O" && five.Content.ToString() == "O" && eight.Content.ToString() == "O")
                {
                    two.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    eight.Foreground = new SolidColorBrush(Colors.LightGreen);
                    v2.Visibility = Visibility.Visible;
                }
                else if (three.Content.ToString() == "O" && six.Content.ToString() == "O" && nine.Content.ToString() == "O")
                {
                    three.Foreground = new SolidColorBrush(Colors.LightGreen);
                    six.Foreground = new SolidColorBrush(Colors.LightGreen);
                    nine.Foreground = new SolidColorBrush(Colors.LightGreen);
                    v3.Visibility = Visibility.Visible;
                }
                else if (one.Content.ToString() == "O" && five.Content.ToString() == "O" && nine.Content.ToString() == "O")
                {
                    one.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    nine.Foreground = new SolidColorBrush(Colors.LightGreen);
                    d1.Visibility = Visibility.Visible;
                }
                else if (three.Content.ToString() == "O" && five.Content.ToString() == "O" && seven.Content.ToString() == "O")
                {
                    three.Foreground = new SolidColorBrush(Colors.LightGreen);
                    five.Foreground = new SolidColorBrush(Colors.LightGreen);
                    seven.Foreground = new SolidColorBrush(Colors.LightGreen);
                    d2.Visibility = Visibility.Visible;
                }
            }
            //If there is a tie
            else if (winner == "Tie")
            {
                //Display the 1st player's title, name, and image.
                player1Title.Visibility = Visibility.Visible;
                player1Img.Visibility = Visibility.Visible;
                //Hide the progress bar.
                progress1.Visibility = Visibility.Collapsed;
                progressTitle1.Visibility = Visibility.Collapsed;
                //If the player is human...
                if (player1 == "human")
                {
                    //Display the human name title.
                    humanName.Visibility = Visibility.Visible;
                }
                //If the player is a mobile service...
                else if (player1 == "service")
                {
                    //Display the azure name title.
                    azureName.Visibility = Visibility.Visible;

                    //Identify which service it is.
                    if (serviceName == "net")
                    {
                        //Display the net name.
                        netName.Visibility = Visibility.Visible;
                    }
                    if (serviceName == "js")
                    {
                        //Display the js name.
                        jsName.Visibility = Visibility.Visible;
                    }
                }

                //Display the 2nd player's title, name, and image.
                player2Title.Visibility = Visibility.Visible;
                player2Img.Visibility = Visibility.Visible;
                //Hide the progress bar.
                progress2.Visibility = Visibility.Collapsed;
                progressTitle2.Visibility = Visibility.Collapsed;
                //If the player is human...
                if (player2 == "human")
                {
                    //Display the human name title.
                    humanName2.Visibility = Visibility.Visible;
                }
                //If the player is a mobile service...
                else if (player2 == "service")
                {
                    //Display the azure name title.
                    azureName2.Visibility = Visibility.Visible;

                    //Identify which service it is.
                    if (serviceName == "net")
                    {
                        //Display the net name.
                        netName2.Visibility = Visibility.Visible;
                    }
                    if (serviceName == "js")
                    {
                        //Display the js name.
                        jsName2.Visibility = Visibility.Visible;
                    }
                }

                //Display the tie title.
                tieTitle.Visibility = Visibility.Visible;
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            //Enable the button.
            one.IsEnabled = true;
            //Reset the content
            one.Content = "?";
            //Update the color to black.
            one.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            two.IsEnabled = true;
            //Reset the content
            two.Content = "?";
            //Update the color to black.
            two.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            three.IsEnabled = true;
            //Reset the content
            three.Content = "?";
            //Update the color to black.
            three.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            four.IsEnabled = true;
            //Reset the content
            four.Content = "?";
            //Update the color to black.
            four.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            five.IsEnabled = true;
            //Reset the content
            five.Content = "?";
            //Update the color to black.
            five.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            six.IsEnabled = true;
            //Reset the content
            six.Content = "?";
            //Update the color to black.
            six.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            seven.IsEnabled = true;
            //Reset the content
            seven.Content = "?";
            //Update the color to black.
            seven.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            eight.IsEnabled = true;
            //Reset the content
            eight.Content = "?";
            //Update the color to black.
            eight.Foreground = new SolidColorBrush(Colors.Black);
            //Enable the button.
            nine.IsEnabled = true;
            //Reset the content
            nine.Content = "?";
            //Update the color to black.
            nine.Foreground = new SolidColorBrush(Colors.Black);
            //Reset move to 1.
            Game.move = 1;
            //Hide all of the content.
            player1Title.Visibility = Visibility.Collapsed;
            humanName.Visibility = Visibility.Collapsed;
            player2Title.Visibility = Visibility.Collapsed;
            azureName.Visibility = Visibility.Collapsed;
            player1Img.Visibility = Visibility.Collapsed;
            player2Img.Visibility = Visibility.Collapsed;
            netName.Visibility = Visibility.Collapsed;
            jsName.Visibility = Visibility.Collapsed;
            humanName2.Visibility = Visibility.Collapsed;
            azureName2.Visibility = Visibility.Collapsed;
            netName2.Visibility = Visibility.Collapsed;
            jsName2.Visibility = Visibility.Collapsed;
            winnerTitle.Visibility = Visibility.Collapsed;
            left.Visibility = Visibility.Collapsed;
            right.Visibility = Visibility.Collapsed;
            tieTitle.Visibility = Visibility.Collapsed;
            progress1.Visibility = Visibility.Collapsed;
            progressTitle1.Visibility = Visibility.Collapsed;
            progress2.Visibility = Visibility.Collapsed;
            progressTitle2.Visibility = Visibility.Collapsed;
            grid.IsHitTestVisible = true;
            h1.Visibility = Visibility.Collapsed;
            h2.Visibility = Visibility.Collapsed;
            h3.Visibility = Visibility.Collapsed;
            v1.Visibility = Visibility.Collapsed;
            v2.Visibility = Visibility.Collapsed;
            v3.Visibility = Visibility.Collapsed;
            d1.Visibility = Visibility.Collapsed;
            d2.Visibility = Visibility.Collapsed;

            //Call the turn function.
            turn();
        }
    }
}

