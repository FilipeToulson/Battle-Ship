using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    interface Player
    {
        void SetUp();
        Shot Shoot();
        void RecieveShot(Shot shot);
        bool HasLost();
    }
}
