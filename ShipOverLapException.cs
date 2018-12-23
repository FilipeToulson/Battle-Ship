using System;
using System.Runtime.Serialization;

namespace Battle_Ship
{
    [Serializable]
    internal class ShipOverLapException : Exception
    {
        public ShipOverLapException() : base()
        {
        }
    }
}