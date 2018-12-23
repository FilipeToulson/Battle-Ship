using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Ship
{
    class BattleShip
    {
        static void Main(string[] args)
        {
            UI.DisplayTitleScreen();

            Player player = new HumanPlayer();
            player.SetUp();

            Player ai = new AIPlayer();
            ai.SetUp();

            PlayGame(player, ai);
        }

        private static void PlayGame(Player player1, Player player2)
        {
            UI.DisplayGameStartScreen();

            Shot shot;

            while (!player1.HasLost() && !player2.HasLost())
            {
                shot = player1.Shoot();
                player2.RecieveShot(shot);

                shot = player2.Shoot();
                player1.RecieveShot(shot);
            }
        }
    }
}
