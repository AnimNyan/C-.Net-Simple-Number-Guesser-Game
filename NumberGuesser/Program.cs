﻿using System;
//used for using C# lists
using System.Collections.Generic;


//Small application for playing a simple command line based number guessing game!
//Written By Seabrook Ng

namespace NumberGuesser
{  
    //create a class as a global variable
    static class globalList
    {
        static List<string> _list; // Static List instance

        static globalList()
        {
            //
            // Allocate the list.
            //
            _list = new List<string>();
        }

        public static void Record(string value)
        {
            //
            // Record this value in the list.
            //
            _list.Add(value);
        }

        public static void Display()
        {
            //
            // Write out the results.
            //
            foreach (var value in _list)
            {
                Console.WriteLine(value);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //getAppInfo displays app version and creator
            getAppInfo();
           

            //Ask users name
            Console.WriteLine("What is your name?");

            //Get User input
            string inputName = Console.ReadLine();

            displayMainMenu();
            moveToMenuOptions(inputName);
        }

        //Get and display app info
        static void getAppInfo()
        {
            //set app vars
            string appName = "Number Guesser";
            string appVersion = "1.0.0";
            string appAuthor = "Seabrook Ng";

            // Change text color
            Console.ForegroundColor = ConsoleColor.Green;

            //write app info
            Console.WriteLine("{0}: Version {1} by {2}", appName, appVersion, appAuthor);

            // Change text color
            Console.ResetColor();
        }

        // Print color message
        static void printColorMessage(ConsoleColor color, string message)
        {
            //Change command line text color
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            //Reset text color
            Console.ResetColor();
        }

        //print the main menu options
        static void displayMainMenu()
        {
            Console.WriteLine("Welcome to Number Guesser!");
            Console.WriteLine("[0] Play Game");
            Console.WriteLine("[1] View Highscores");
            Console.WriteLine("[2] Exit Game");
            Console.WriteLine("Select an option and press enter:");
            
        }

        //move from main menu to an option in the menu
        static void moveToMenuOptions(string inputName)
        {
            bool isAsking = true;
            while (isAsking)
            {
                int option = askForStringInputReturnInt();
                //let user out of menu if it is a valid option
                //otherwise force user to enter a valid response   
                if (option == 0)
                {
                    isAsking = false;
                    bool isPlaying = true;

                    while (isPlaying)
                    {
                        isPlaying = playGame(inputName);
                    }

                    //display the main menu again
                    displayMainMenu();
                    moveToMenuOptions(inputName);
                }
                else if (option == 1)
                {
                    isAsking = false;
                    displayHighScores(inputName);
                }
                else if (option == 2)
                {
                    isAsking = false;
                    exitGame();
                }
                else
                {
                    Console.WriteLine("Please enter a valid response from 0-2!");
                }
            }
        }

        //return a string input
        //with input validation and force a valid input
        static int askForStringInputReturnInt()
        {
            bool isWaiting = true;

            //by default assign negative number
            //which is normally impossible for error debugging
            int integer = -1;

            while(isWaiting)
            {
                string input = Console.ReadLine();

                //input validation make sure valid integer
                //if not print error message
                //and ask for another input
                if (!int.TryParse(input, out integer))
                {
                    //print error messsage
                    printColorMessage(ConsoleColor.Red, "Please enter a valid integer!");
                }
                //else if the input is a valid integer
                else
                {
                    isWaiting = false;

                    //convert string input to int
                    integer = Convert.ToInt32(input);
                }
            }

            return integer;
        }


        static bool playGame(string inputName)
        {
            //by default keep the loop going of playGame()
            //and correct this variable if the user chooses to stop playing
            bool isPlaying = true;

            Console.WriteLine("Hello {0}, let's play a game!", inputName);

            //create a new random number
            Random rnd = new Random();

            //initialise random correct number
            //returns random integers 1 to 100
            int correctNumber = rnd.Next(1, 101);

            //initialise guess var
            int guess;

            Console.WriteLine("Guess a number between 1 and 100");

            bool isGuessing = true;
            //initialise number of Guesses
            int numGuesses = 0;

            //begin loop to guess variable
            while (isGuessing)
            {
                

                string input = Console.ReadLine();

                //input validation make sure valid integer
                if (!int.TryParse(input, out guess))
                {
                    //print error messsage
                    printColorMessage(ConsoleColor.Red, "Please enter a valid integer!");

                    //go to next loop
                    continue;
                }

                //prompt the user to guess
                //convert string to int
                guess = Convert.ToInt32(input);

                //input validation
                //if input is not a valid number from 1 to 100
                ///print an error message
                if (!(guess >= 1 && guess <= 100))
                {
                    printColorMessage(ConsoleColor.Red, "Please enter an integer from 1 to 100!");
                    //go to next loop
                    continue;
                }

                //valid guesses begin now and only if the code gets to
                //this point will it add it to the tally
                //every loop is one guess so add one guess to the tally
                numGuesses += 1;

                if (guess == correctNumber)
                {
                    //take user out of loop
                    isGuessing = false;
                    string successMessage = "Great job! You guessed correctly! You win " + inputName + " with " + numGuesses + " guesses!";
                    printColorMessage(ConsoleColor.Yellow, successMessage);
                    recordHighScore(inputName, numGuesses);
                }
                else if (guess > correctNumber)
                {
                    printColorMessage(ConsoleColor.Red, "Your guess is too large! Guess again:");
                }
                else if (guess < correctNumber)
                {
                    printColorMessage(ConsoleColor.Red, "Your guess is too small! Guess again:");
                }
            }

            //Ask to play again
            Console.WriteLine("Play Again? [Y/N]:");

            string answer = Console.ReadLine().ToUpper();

            if (answer == "Y")
            {
                //return true playGame() function is within a loop and will repeat by itself
                isPlaying = true;
            }
            else if (answer == "N")
            {
                //retrun false to stop the loop of the playGame() function
                isPlaying = false;
                
            }
            else
            {
                Console.WriteLine("Error: Y/N answer input is in invalid area of code!");
                isPlaying = true;
            }
            return isPlaying;
        }

        //records
        static void recordHighScore(string inputName, int numGuesses)
        {
            //convert numGuesses to string
            string stringGuesses = numGuesses.ToString();
            globalList.Record(inputName + ": " + stringGuesses);
        }

        //display high scores and back option
        static void displayHighScores(string inputName)
        {
            globalList.Display();
            
            Console.WriteLine("[0]Go back to main menu:");
            bool isAsking = true;
            while (isAsking)
            {
                int option = askForStringInputReturnInt();
                //let user out of menu if it is a valid option
                //otherwise force user to enter a valid response
                if (option == 0)
                {
                    isAsking = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid response which is 0!");
                }
            }
            displayMainMenu();
            moveToMenuOptions(inputName);

        }

        static void exitGame()
        {
            printColorMessage(ConsoleColor.Yellow, "Thanks for Playing!");
        }
    }
}
