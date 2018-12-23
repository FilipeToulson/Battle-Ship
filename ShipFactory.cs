using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    static class ShipFactory
    {
        public static Ship Build(int type, Point pos, char orientation, List<Ship> shipsOnBoard)
        {
            int size = GetShipSize(type);
            List<ShipPart> shipBody = CreateShipBody(size, pos, orientation, shipsOnBoard);

            Ship ship = new Ship(type, shipBody, orientation);
            return ship;
        }

        private static int GetShipSize(int type)
        {
            int size = 0;

            switch (type)
            {
                case 0: //Carrier
                {
                    size = 5;
                    break;
                }
                case 1: //Battleship
                {
                    size = 4;
                    break;
                }
                case 2: case 3: //Cruiser/Submarine
                {
                    size = 3;
                    break;
                }
                case 4: //Cruiser
                {
                    size = 2;
                    break;
                }
            }

            return size;
        }

        //Constructs the ships "body" by creating a certain number of ship parts for a given ship
        private static List<ShipPart> CreateShipBody(int size, Point pos, char orientation, List<Ship> shipsOnBoard)
        {
            List<ShipPart> shipBody = new List<ShipPart>();

            int xTranslate = 0; //Translates a point along the x axis
            int yTranslate = 0; //Translates a point along the x axis
            for (int i = 0; i < size; i++)
            {
                switch(orientation)
                {
                    case 'v':
                    {
                        yTranslate = i;

                        break;
                    }
                    case 'h':
                    {
                        xTranslate = i;

                        break;
                    }
                }

                Point point = new Point(pos.X + xTranslate, pos.Y + yTranslate);

                if (!Board.OnBoard(point))
                {
                    throw new PointOutOfBoundsException();
                }

                if (OverlapsWithOtherShips(point, shipsOnBoard))
                {
                    throw new ShipOverLapException();
                }

                ShipPart shipPart = new ShipPart(point);
                shipBody.Add(shipPart);
            }

            return shipBody;
        }

        private static bool OverlapsWithOtherShips(Point point, List<Ship> shipsOnBoard)
        {
            bool overlaps = false;

            foreach (Ship ship in shipsOnBoard)
            {
                List<ShipPart> shipBody = ship.ShipBody;

                foreach (ShipPart shipPart in shipBody)
                {
                    Point partPosition = shipPart.Position;

                    if (point.Equals(partPosition))
                    {
                        overlaps = true;

                        break;
                    }
                }

                if (overlaps)
                {
                    break;
                }
            }

            return overlaps;
        }
    }
}
