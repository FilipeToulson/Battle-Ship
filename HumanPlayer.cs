using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    class HumanPlayer : Player, ShipSunkListener
    {
        private List<Ship> ships;
        private List<Shot> shotsTaken;
        private Shot mostRecentOpponentShot;
        private int numOfFunctioningShips;
        private bool hasLost;

        public HumanPlayer()
        {
            ships = new List<Ship>();
            shotsTaken = new List<Shot>();
            numOfFunctioningShips = 5;
            hasLost = false;
        }

        //Gets the player to place their ships on the board
        public void SetUp()
        {
            int x;
            int y;
            char orientation;
            bool shipAdded = false;
            bool setUpDone = false;

            do
            {
                for (int i = 0; i < 5; i++)
                {
                    UI.DisplaySetUpInstructions(i);
                    UI.DisplayShipsBoard(ships);

                    do
                    {
                        x = UI.GetCoordinateFromUser('x');
                        y = UI.GetCoordinateFromUser('y');

                        orientation = UI.GetOrientationFromUser();

                        try
                        {
                            List<Ship> shipsCopy = new List<Ship>(ships);
                            Ship ship = ShipFactory.Build(i, new Point(x, y), orientation, shipsCopy);
                            ship.SetShipSunkListener(this);
                            ships.Add(ship);

                            shipAdded = true;
                        }
                        catch (PointOutOfBoundsException)
                        {
                            UI.DisplayErrorMessage("The ship did not fit inside the board. Try again.");
                            shipAdded = false;
                        }
                        catch (ShipOverLapException)
                        {
                            UI.DisplayErrorMessage("The ship overlaps with one or more ships on the borad. Try again.");
                            shipAdded = false;
                        }
                    } while (!shipAdded);
                }

                if(UI.PlayerReadyToStartGame(ships))
                {
                    setUpDone = true;
                }
                else
                {
                    ships.Clear();
                }
            } while (!setUpDone);
        }

        public Shot Shoot()
        {
            bool validShot = false;
            Shot shot = null;

            UI.DisplayPlayScreen(ships, shotsTaken, mostRecentOpponentShot);

            do
            {
                int x = UI.GetCoordinateFromUser('x');
                int y = UI.GetCoordinateFromUser('y');

                Point shotPos = new Point(x, y);

                if (AlreadyShotAtPosition(shotPos))
                {
                    UI.DisplayErrorMessage("You've already shot at position (" + x + ", " + y + "). Try again.");
                }
                else
                {
                    shot = new Shot(shotPos);
                    shotsTaken.Add(shot);

                    validShot = true;
                }
            } while (!validShot);
            
            return shot;
        }

        private bool AlreadyShotAtPosition(Point position)
        {
            bool alreadyShotAtPosition = false;

            foreach(Shot shotTaken in shotsTaken)
            {
                Point shotTakenPos = shotTaken.Position;

                if(position.Equals(shotTakenPos))
                {
                    alreadyShotAtPosition = true;

                    break;
                }
            }

            return alreadyShotAtPosition;
        }

        //Checks to see if the opponent's shot hit any of this player's ships
        public void RecieveShot(Shot shot)
        {
            mostRecentOpponentShot = shot;

            foreach (Ship ship in ships)
            {
                List<ShipPart> shipBody = ship.ShipBody;

                foreach (ShipPart shipPart in shipBody)
                {
                    Point shipPartPos = shipPart.Position;
                    Point shotPos = shot.Position;

                    if (shotPos.Equals(shipPartPos))
                    {
                        shot.IsHit = true;
                        shipPart.SetIsFunctional(false);

                        break;
                    }
                }
            }
        }

        public void ShipSunk()
        {
            numOfFunctioningShips--;

            if (numOfFunctioningShips == 0)
            {
                UI.DisplayLooseScreen();

                hasLost = true;
            }
        }

        public bool HasLost()
        {
            return hasLost;
        }
    }
}
