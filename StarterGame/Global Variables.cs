using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace StarterGame
{
    class Global_Variables
    {
        public static Microsoft.Xna.Framework.Rectangle safeAreaRectangle;
   
  //SETTINGS --------------------------------------------------------
        public static string gameNAME = "Santa Snowball";
        public static string version = "1.0.0.0"; 
        public static bool showIntroMovie = false;
        public static bool showStartMenu = true;
        public static bool showGraphicalHowToPlay = false;
        public static bool showTextBasedHowToPlay = true;
        public static bool gameHasNetworking = false;
        public static bool gameHasMultiplayer = true;
        public static bool showDebuggingMenuOptions = false; //turns boxart viewing and highscore clearing on or off. FALSE means they're off. 
        public static bool showScreenshotsMenuOption = false;
        public static Color titleScreenColor = Color.White;
        public static bool simulateTrialMode = false; //leave as false for final submission!
  //-----------------------------------------------------------------

        public static int levelNumber = 1;
        public static bool shouldLoadMainMenu = false;
        public static bool shouldReloadGame = false;
        public static PlayerIndex playerWhoBeganGame;
        public static int playerWhoBeganGameZeroIndexedInt;
        public static bool player1isplaying = false;
        public static bool player2isplaying = false;
        public static bool player3isplaying = false;
        public static bool player4isplaying = false;
    }
}
