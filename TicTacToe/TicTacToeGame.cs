#define CSHARP2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1030    // disable user defined warnings

namespace TicTacToe
{
    class TicTacToeGame
    {
        static void Main()
        {
            // stores locations each player has moved
            int[] playerPositions = { 0, 0 };

            // initially set the currentPlayer to Player 1
            int currentPlayer = 1;

            //winning player
            int winner = 0;

            string input = null;

            // Display the board and prompt the current player for his next move
            for (int turn = 1; turn <= 10; ++turn)
            {
                DisplayBoard(playerPositions);

                #region Check for End Game
                if (EndGame(winner, turn, input))
                {
                    break;
                }
                #endregion Check for End Game

                input = NextMove(playerPositions, currentPlayer);

                winner = DetermineWinner(playerPositions);

                // switch players
                currentPlayer = (currentPlayer == 2) ? 1 : 2;
            }
        }
        private static string NextMove(int[] playerPositions, int currentPlayer)
        {
            string input;

            // repeatedly prompt the player for a move until a valid move is entered
            bool validMove;
            do
            {
                // request a move from the current player
                System.Console.Write("\nPlayer {0} - Enter move:", currentPlayer);
                input = System.Console.ReadLine();
                validMove = validateAndMove(playerPositions, currentPlayer, input);
            }
            while (!validMove);

            return input;
        }

        static bool EndGame(int winner, int turn, string input)
        {
            bool endGame = false;
            if (winner > 0)
            {
                System.Console.WriteLine("\nPlayer {0} has won!!!!", winner);
                endGame = true;
            }
            else if (turn == 10)
            {
                // After completing the 10th display of the board, exit out rather than prompting the user again.
                System.Console.WriteLine("\nThe game was a tie!");
                endGame = true;
            }
            else if (input == "" || input == "quit")
            {
                // Check if user quit by hitting Enter without any characters or by typing "quit".
                System.Console.WriteLine("\nThe last player quit");
                endGame = true;
            }
            return endGame;
        }

        static int DetermineWinner(int[] playerPositions)
        {
            int winner = 0;

            // Determine if there is a winner
            int[] winningMasks = {7,56,448,73,146,292,84,273};

            foreach (int mask in winningMasks)
            {
                if ((mask & playerPositions[0]) == mask)
                {
                    winner = 1;
                    break;
                }
                else if ((mask & playerPositions[1]) == mask)
                {
                    winner = 2;
                    break;
                }
            }
            return winner;
        }

        static bool validateAndMove(int[] playerPositions, int currentPlayer, string input)
        {
            bool valid = false;

            // Check the current player's input
            switch (input)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
#warning "Same move allowed multiple times."
                    int shifter;    // The number of places to shift over in order to set a bit
                    int position;  // the bit which is to be set

                    // int.Parse() converts "input" to an integer
                    // "int.Parse(input) - 1" because arrays are zero-based
                    shifter = int.Parse(input) - 1;

                    // shift mask of 00000000000000000000000000000001 over by celllocations
                    position = 1 << shifter;

                    // Take the current player cells and OR them to set the new position as well.
                    // Since currentPlayter is either 1 or 2 you subtract one to use currentPlayer
                    // as an index in a 0-based array.
                    playerPositions[currentPlayer - 1] |= position;

                    valid = true;
                    break;

                case "":
                case "quit":
                    valid = true;
                    break;

                default:
                    // If none of the other case statements is encountered, then the text is invalid.
                    System.Console.WriteLine("\nERROR:  Enter a value from 1-9." + "Push ENTER to quit");
                    break;
            }

            return valid;
        }

        static void DisplayBoard(int[] playerPositions)
        {
            //  This represents the borders between each cell for one row.
            string[] borders = {"|","|","\n---+---+---\n","|","|","\n---+---+---\n","|","|",""};

            // Display the current board
            int border = 0; // set the first border (border[0] = "/")

#if CSHARP2
            System.Console.Clear();
#endif

            for (int position = 1; position <= 256; position <<= 1, border++)
            {
                char token = CalculateToken(playerPositions, position);

                // Write out a cell value and the border that comes after it
                System.Console.Write(" {0} {1}", token, borders[border]);
            }
        }

        static char CalculateToken(int[] playerPositions, int position)
        {
            // intialize the players to 'X' and 'O'
            char[] players = { 'X', 'O' };

            char token;
            // If player has the position set, then set the token to that player.
            if ((position & playerPositions[0]) == position)
            {
                // Player 1 has that position marked
                token = players[0];
            }
            else if ((position & playerPositions[1]) == position)
            {
                // Player 2 has that position marked
                token = players[1];
            }
            else
            {
                // the position is empty
                token = ' ';
            }
            return token;
        }

#line 113 "TicTacToeGame.cs"
        // Generated code goes here
#line default
    }
}
