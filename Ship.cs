using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    class Ship : ShipPartHitListener
    {
        public int Type { get; }
        public List<ShipPart> ShipBody { get; }
        private int numOfFunctioningParts;
        public char Orientation { get; }
        private ShipSunkListener shipSunkListener;
        public bool Sunk { get; set; }

        public Ship(int type, List<ShipPart> shipBody, char orientation)
        {
            Type = type;
            ShipBody = shipBody;
            numOfFunctioningParts = shipBody.Count;
            Orientation = orientation;
            Sunk = false;

            SetShipPartHitListener();
        }

        private void SetShipPartHitListener()
        {
            foreach (ShipPart shipPart in ShipBody)
            {
                shipPart.SetShipPartHitListener(this);
            }
        }

        public void SetShipSunkListener(ShipSunkListener inShipSunkListener)
        {
            shipSunkListener = inShipSunkListener;
        }

        public void ShipPartHit()
        {
            numOfFunctioningParts--;

            if(numOfFunctioningParts == 0)
            {
                shipSunkListener.ShipSunk();
            }
        }
    }
}
