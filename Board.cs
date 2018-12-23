using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    static class Board
    {
        public const int SIZE = 10;
        private static char[,] defaultBoard = {
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' },
            { '~', '~', '~', '~', '~', '~', '~', '~', '~', '~' }
        };

        public static char[,] GetDefaultBoard()
        {
            return (char[,])defaultBoard.Clone();
        }

        public static bool OnBoard(Point point)
        {
            bool onBoard = true;

            onBoard = (point.X >= 0 && point.X < SIZE) &&
                      (point.Y >= 0 && point.Y < SIZE);

            return onBoard;
        }
    }
}
