using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    class AIPlayer : Player, ShipSunkListener
    {
        private List<Ship> ships;
        private List<Shot> shotsTaken;
        private int numOfFunctioningShips;
        private bool hasLost;
        private Random random;
        //ORIENTATION: used to determine if a ship is placed horizontally or vertically on the board
        private readonly char[] ORIENTATION = { 'v', 'h' };

        public AIPlayer()
        {
            ships = new List<Ship>();
            shotsTaken = new List<Shot>();
            random = new Random();
            numOfFunctioningShips = 5;
            hasLost = false;
        }

        //Places the ai's ships on the board
        public void SetUp()
        {
            int x;
            int y;
            char orientation;
            bool shipAdded = false;

            for (int i = 0; i < 5; i++)
            {
                do
                {
                    x = GetRandomNumber(Board.SIZE);
                    y = GetRandomNumber(Board.SIZE);

                    orientation = ORIENTATION[GetRandomNumber(2)];

                    try
                    {
                        Ship ship = ShipFactory.Build(i, new Point(x, y), orientation, new List<Ship>(ships));
                        ship.SetShipSunkListener(this);
                        ships.Add(ship);

                        shipAdded = true;
                    }
                    catch (PointOutOfBoundsException)
                    {
                        shipAdded = false;
                    }
                    catch (ShipOverLapException)
                    {
                        shipAdded = false;
                    }
                } while (!shipAdded);
            }
        }

        public Shot Shoot()
        {
            bool validShot = false;
            Shot shot = null;

            do
            {
                int x = GetRandomNumber(Board.SIZE);
                int y = GetRandomNumber(Board.SIZE);

                Point shotPos = new Point(x, y);

                if (!AlreadyShotAtPosition(shotPos))
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

            foreach (Shot shotTaken in shotsTaken)
            {
                Point shotTakenPos = shotTaken.Position;

                if (position.Equals(shotTakenPos))
                {
                    alreadyShotAtPosition = true;

                    break;
                }
            }

            return alreadyShotAtPosition;
        }

        private int GetRandomNumber(int max)
        {
            return random.Next(max);
        }

        //Checks to see if the opponent's shot hit any of this player's ships
        public void RecieveShot(Shot shot)
        {
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

            if(numOfFunctioningShips == 0)
            {
                UI.DisplayWinScreen();

                hasLost = true;
            }
        }

        public bool HasLost()
        {
            return hasLost;
        }
    }
}
