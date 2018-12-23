using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    class Shot
    {
        public Point Position { get; }
        public bool IsHit { get; set; }

        public Shot(Point position)
        {
            Position = position;
            IsHit = false;
        }
    }
}
