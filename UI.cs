using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    class UI
    {
        public static void DisplayTitleScreen()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("~~~~~~~~   ____        _   _   _         _____ _     _         ~~~~~~~~");
            Console.WriteLine("~~~~~~~~  |  _ \\      | | | | | |       / ____| |   (_)        ~~~~~~~~");
            Console.WriteLine("~~~~~~~~  | |_) | __ _| |_| |_| | ___  | (___ | |__  _ _ __    ~~~~~~~~");
            Console.WriteLine("~~~~~~~~  |  _ < / _` | __| __| |/ _ \\  \\___ \\| '_ \\| | '_ \\   ~~~~~~~~");
            Console.WriteLine("~~~~~~~~  | |_) | (_| | |_| |_| |  __/  ____) | | | | | |_) |  ~~~~~~~~");
            Console.WriteLine("~~~~~~~~  |____/ \\__,_|\\__|\\__|_|\\___| |_____/|_| |_|_| .__/   ~~~~~~~~");
            Console.WriteLine("~~~~~~~~                                              | |      ~~~~~~~~");
            Console.WriteLine("~~~~~~~~                                              |_|      ~~~~~~~~");
        }

        public static void DisplayGameStartScreen()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~   __ _             _     ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~  / _\\ |_ __ _ _ __| |_   ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~  \\ \\| __/ _` | '__| __|  ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~  _\\ \\ || (_| | |  | |_   ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~  \\__/\\__\\__,_|_|   \\__|  ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~                          ~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public static void DisplayWinScreen()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("~~~~~~~~~~    __     __          __          ___       _    ~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~    \\ \\   / /          \\ \\        / (_)     | |   ~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~     \\ \\_/ /__  _   _   \\ \\  /\\  / / _ _ __ | |   ~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~      \\   / _ \\| | | |   \\ \\/  \\/ / | | '_ \\| |   ~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~       | | (_) | |_| |    \\  /\\  /  | | | | |_|   ~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~       |_|\\___/ \\__,_|     \\/  \\/   |_|_| |_(_)   ~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~                                                  ~~~~~~~~~~~");
            Console.WriteLine("-----------------------------------------------------------------------");
        }

        public static void DisplayLooseScreen()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("~~~~~~~~    __     __           _                             ~~~~~~~~~");
            Console.WriteLine("~~~~~~~~    \\ \\   / /          | |                            ~~~~~~~~~");
            Console.WriteLine("~~~~~~~~     \\ \\_/ /__  _   _  | |     ___   ___  ___  ___    ~~~~~~~~~");
            Console.WriteLine("~~~~~~~~      \\   / _ \\| | | | | |    / _ \\ / _ \\/ __|/ _ \\   ~~~~~~~~~");
            Console.WriteLine("~~~~~~~~       | | (_) | |_| | | |___| (_) | (_) \\__ \\  __/   ~~~~~~~~~");
            Console.WriteLine("~~~~~~~~       |_|\\___/ \\__,_| |______\\___/ \\___/|___/\\___|   ~~~~~~~~~");
            Console.WriteLine("~~~~~~~~                                                      ~~~~~~~~~");
            Console.WriteLine("-----------------------------------------------------------------------");
        }

        public static void DisplaySetUpInstructions(int shipIndex)
        {
            int[] shipSizes = { 5, 4, 3, 3, 2 };
            string[] shipNames = { "Carrier", "Battleship", "Cruiser", "Submarine", "Cruiser" };

            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Add a " + shipNames[shipIndex] + " to the board. This ship takes up " 
                + shipSizes[shipIndex] + " places.");
            Console.WriteLine("Becareful to not have the ship overlap with others or go off the board.");
        }

        public static void DisplayPlayScreen(List<Ship> ships, List<Shot> shotsTaken, Shot opponentShot)
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Your shots (M: shot missed enemy ship, H: shot hit enemy ship):");

            DisplayShotsBoard(shotsTaken);

            //Only display oppoinent shot info if the opponent has taken a shot
            if(opponentShot != null)
            {
                DisplayOpponenetShotInfo(opponentShot);
            }

            Console.WriteLine("Your ships (an X shows where you've been hit):");

            DisplayShipsBoard(ships);

            Console.WriteLine("Input coordinates to take a shot:");
        }

        private static void DisplayOpponenetShotInfo(Shot opponentShot)
        {
            Point opponentShotPos = opponentShot.Position;

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("The opponent has shot at position " +
                "(" + opponentShotPos.X + ", " + opponentShotPos.Y + ")");

            if (opponentShot.IsHit)
            {
                Console.WriteLine("It HIT one of your ships!");
            }
            else
            {
                Console.WriteLine("It MISSED your ships");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("");
        }

        public static int GetCoordinateFromUser(char axis)
        {
            int coordinate = 0;
            bool done = false;

            do
            {
                Console.Write("Input " + axis + " coordinate: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out coordinate))
                {
                    if (coordinate >= 0 && coordinate < Board.SIZE)
                    {
                        done = true;
                    }
                    else
                    {
                        DisplayErrorMessage("Coordinate must lie within the board");
                    }
                }
                else
                {
                    DisplayErrorMessage("Coordinate must be an integer");
                }
            } while (!done);

            return coordinate;
        }

        //Orientation in this case determines whether a ship is placed verically or horizontally on the board
        public static char GetOrientationFromUser()
        {
            char orientation;
            bool done = false;

            do
            {
                Console.Write("Input orientation (v: vertical, h: horizontal): ");
                string input = Console.ReadLine();
                if (char.TryParse(input, out orientation) && (orientation == 'v' || orientation == 'h'))
                {
                    done = true;
                }
                else
                {
                    DisplayErrorMessage("Orientation must be either v or h");
                }
            } while (!done);

            return orientation;
        }

        public static bool PlayerReadyToStartGame(List<Ship> ships)
        {
            char playerChoice;
            bool done = false;
            bool playerReady = false;

            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Here are your ships:");

            DisplayShipsBoard(ships);

            do
            {
                Console.Write("Are you happy with the ships' layout? (y: to start game, n: to try again): ");
                string input = Console.ReadLine();
                if (char.TryParse(input, out playerChoice) && (playerChoice == 'y' || playerChoice == 'n'))
                {
                    done = true;
                }
                else
                {
                    DisplayErrorMessage("Your choice must be either y or n");
                }
            } while (!done);

            if(playerChoice == 'y')
            {
                playerReady = true;
            }

            return playerReady;
        }

        public static void DisplayShipsBoard(List<Ship> ships)
        {
            char[,] board = Board.GetDefaultBoard();
            AddShipsToBoard(board, ships);

            Console.WriteLine("");
            Console.WriteLine("# 0 1 2 3 4 5 6 7 8 9");

            for (int i = 0; i < Board.SIZE; i++)
            {
                Console.Write(i + " ");

                for (int j = 0; j < Board.SIZE; j++)
                {
                    Console.Write(board[i, j] + " ");
                }

                Console.Write("\n");
            }

            Console.WriteLine("");
        }

        //Places a character on the board to represent a functioning or non-functioning ship part
        private static void AddShipsToBoard(char[,] board, List<Ship> ships)
        {
            foreach(Ship ship in ships)
            {
                List<ShipPart> shipBody = ship.ShipBody;

                foreach(ShipPart shipPart in shipBody)
                {
                    Point point = shipPart.Position;

                    if(shipPart.IsFunctional())
                    {
                        board[point.Y, point.X] = '=';
                    }
                    else
                    {
                        board[point.Y, point.X] = 'X';
                    }
                }
            }
        }

        public static void DisplayShotsBoard(List<Shot> shotsTaken)
        {
            char[,] board = Board.GetDefaultBoard();
            AddShotsToBoard(board, shotsTaken);

            Console.WriteLine("");
            Console.WriteLine("# 0 1 2 3 4 5 6 7 8 9");

            for (int i = 0; i < Board.SIZE; i++)
            {
                Console.Write(i + " ");

                for (int j = 0; j < Board.SIZE; j++)
                {
                    Console.Write(board[i, j] + " ");
                }

                Console.Write("\n");
            }

            Console.WriteLine("");
        }

        //Places a character on the board to represent a shot that either hit or missed
        private static void AddShotsToBoard(char[,] board, List<Shot> shotsTaken)
        {
            foreach (Shot shot in shotsTaken)
            {
                Point point = shot.Position;

                if (shot.IsHit)
                {
                    board[point.Y, point.X] = 'H';
                }
                else
                {
                    board[point.Y, point.X] = 'M';
                }
            }
        }

        public static void DisplayErrorMessage(string message)
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("-----------------------------------------------------------------------");
        }
    }
}
