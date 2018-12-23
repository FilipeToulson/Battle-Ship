using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    class ShipPart
    {
        public Point Position { get; }
        private ShipPartHitListener shipPartHitListener;
        private bool isFunctional;

        public ShipPart(Point position)
        {
            Position = position;
            isFunctional = true;
        }

        public bool IsFunctional()
        {
            return isFunctional;
        }

        public void SetShipPartHitListener(ShipPartHitListener inShipPartHitListener)
        {
            shipPartHitListener = inShipPartHitListener;
        }

        public void SetIsFunctional(bool inIsFunctional)
        {
            isFunctional = inIsFunctional;

            shipPartHitListener.ShipPartHit();
        }
    }
}
