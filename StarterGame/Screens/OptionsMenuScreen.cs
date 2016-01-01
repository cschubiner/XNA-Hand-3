//#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;


namespace StarterGame
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        //#region Fields


        MenuEntry gamemodeMenuEntry;
        MenuEntry comdifficultyMenuEntry;
        MenuEntry playerspeedstaysequalMenuEntry;
        MenuEntry playerballstartingspeedMenuEntry;
        MenuEntry ballstartingspeedMenuEntry;
        MenuEntry ballspeedmaxMenuEntry;
        MenuEntry playerspeedmaxMenuEntry;
        MenuEntry havecomputerplayerMenuEntry;
        MenuEntry ballspeedincreaserMenuEntry;
        MenuEntry playerspeedincreaserMenuEntry;
        MenuEntry ballpointvalueMenuEntry;
        MenuEntry evilballpointvalueMenuEntry;
        MenuEntry pointstowinMenuEntry;
        MenuEntry timerdurationMenuEntry;
        MenuEntry timerrolloverMenuEntry;
        MenuEntry resetdefaultsMenuEntry;
        MenuEntry backMenuEntry = new MenuEntry("Back");


        static string[] gamemodes = { "Lowest average!", "Panic!", "Points to win!", "Practice Mode" };
        static string[] difficulties = { "Easy","Medium","Hard","Intense","God","Unbeatable"};
        

        //defaults
        public static int currentgamemode = 2;
        public static int currentdifficulty = 1;
        public static bool havecomputerplayer = true;
        public static bool playerspeedstaysequal =true;
        public static int playerballstartingspeed = 5;
        public static int ballstartingspeed = 5;
        public static int ballspeedmax = 23;
        public static int playerspeedmax = 18;
        public static int ballspeedwallincreaser = 0;
        public static int ballspeedincreaser = 5;
        public static int playerspeedincreaser = 5;
        public static int ballpointvalue = 1;
        public static int evilballpointvalue = 0;
        public static int pointstowin = 40;
        public static double timerduration = 2;
        public static double timerrollover=.5;

        

        //#region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Instructions")
        {
            // Create our menu entries.

            gamemodeMenuEntry = new MenuEntry(string.Empty);
            comdifficultyMenuEntry = new MenuEntry(string.Empty);
            playerspeedstaysequalMenuEntry = new MenuEntry(string.Empty);
            playerballstartingspeedMenuEntry = new MenuEntry(string.Empty);
            ballstartingspeedMenuEntry = new MenuEntry(string.Empty);
            ballspeedmaxMenuEntry = new MenuEntry(string.Empty);
            playerspeedmaxMenuEntry = new MenuEntry(string.Empty);
            havecomputerplayerMenuEntry = new MenuEntry(string.Empty);
            ballspeedincreaserMenuEntry = new MenuEntry(string.Empty);
            playerspeedincreaserMenuEntry = new MenuEntry(string.Empty);
            ballpointvalueMenuEntry = new MenuEntry(string.Empty);
            evilballpointvalueMenuEntry = new MenuEntry(string.Empty);
            pointstowinMenuEntry = new MenuEntry(string.Empty);
            timerdurationMenuEntry = new MenuEntry(string.Empty);
            timerrolloverMenuEntry = new MenuEntry(string.Empty);
            resetdefaultsMenuEntry = new MenuEntry(string.Empty);
            SetMenuEntryText();

            

            // Hook up menu event handlers.

            gamemodeMenuEntry.Selected += gamemodeMenuEntrySelected;
            comdifficultyMenuEntry.Selected += comdifficultyMenuEntrySelected;
            playerspeedstaysequalMenuEntry.Selected += playerspeedstaysequalMenuEntrySelected;
            playerballstartingspeedMenuEntry.Selected += playerballstartingspeedMenuEntrySelected;
            ballstartingspeedMenuEntry.Selected += ballstartingspeedMenuEntrySelected;
            ballspeedmaxMenuEntry.Selected += ballspeedmaxMenuEntrySelected;
            playerspeedmaxMenuEntry.Selected += playerspeedmaxMenuEntrySelected;
            havecomputerplayerMenuEntry.Selected += havecomputerplayerMenuEntrySelected;
            ballspeedincreaserMenuEntry.Selected += ballspeedincreaserMenuEntrySelected;
            playerspeedincreaserMenuEntry.Selected += playerspeedincreaserMenuEntrySelected;
            ballpointvalueMenuEntry.Selected += ballpointvalueMenuEntrySelected;
            evilballpointvalueMenuEntry.Selected += evilballpointvalueMenuEntrySelected;
            pointstowinMenuEntry.Selected += pointstowinMenuEntrySelected;
            timerdurationMenuEntry.Selected += timerdurationMenuEntrySelected;
            timerrolloverMenuEntry.Selected += timerrolloverMenuEntrySelected;
            resetdefaultsMenuEntry.Selected += resetdefaultsMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;

            // Add entries to the menu.

           
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        
        
        void SetMenuEntryText()
        {
            for (int i = 0; i < Gamer.SignedInGamers.Count; i++)
            {
                Gamer.SignedInGamers[i].Presence.PresenceMode = GamerPresenceMode.ConfiguringSettings;
            }
            MenuEntries.Clear();
            MenuEntries.Add(gamemodeMenuEntry);
         //   MenuEntries.Add(havecomputerplayerMenuEntry);
           // if (havecomputerplayer==true)
            MenuEntries.Add(comdifficultyMenuEntry);
            MenuEntries.Add(playerspeedstaysequalMenuEntry);
            MenuEntries.Add(playerballstartingspeedMenuEntry);
            MenuEntries.Add(ballstartingspeedMenuEntry);
            MenuEntries.Add(ballspeedmaxMenuEntry);
           MenuEntries.Add(playerspeedmaxMenuEntry);
            
           /* MenuEntries.Add(ballspeedincreaserMenuEntry);
            MenuEntries.Add(playerspeedincreaserMenuEntry);

            if (currentgamemode == 2 | currentgamemode == 3)
            MenuEntries.Add(evilballpointvalueMenuEntry);
            if (currentgamemode == 2|currentgamemode == 0)
                MenuEntries.Add(pointstowinMenuEntry);
            if (currentgamemode == 1)
            {
                MenuEntries.Add(timerdurationMenuEntry);
                MenuEntries.Add(timerrolloverMenuEntry);
            }
            MenuEntries.Add(resetdefaultsMenuEntry);*/
            MenuEntries.Add(backMenuEntry);

            gamemodeMenuEntry.Text = "Your objective: " ;
            comdifficultyMenuEntry.Text = "     Move with left thumbstick.  " + "";
            playerspeedstaysequalMenuEntry.Text = "     Avoid all obstacles. " + "";
            playerballstartingspeedMenuEntry.Text = "     Collect meds to stay alive. " + "";
            ballstartingspeedMenuEntry.Text = "     Don't let your health run out!" + "";
            ballspeedmaxMenuEntry.Text = "     Get over half health to enter CRAZY!!11!! mode" + "";
            playerspeedmaxMenuEntry.Text = "Can you make it to the shore on time?!";
            havecomputerplayerMenuEntry.Text = "     Have computer player: " + (havecomputerplayer ? "true" : "false");
            ballspeedincreaserMenuEntry.Text = "     Ball speed increaser: " + ballspeedincreaser;
            playerspeedincreaserMenuEntry.Text = "     Player speed increaser: " + playerspeedincreaser;
            ballpointvalueMenuEntry.Text = "     Ball point value: " + ballpointvalue;
            evilballpointvalueMenuEntry.Text = "     Evil ball point value: " + evilballpointvalue + " ";
            pointstowinMenuEntry.Text = "     Points needed to win: " + pointstowin;
            timerdurationMenuEntry.Text = "     Timer duration (seconds): " + timerduration;
            timerrolloverMenuEntry.Text = "     Timer rolling-over multiplier: " + timerrollover;
            resetdefaultsMenuEntry.Text = "Reset Default Settings";
        }


        

        //#region Handle Input


        /// <summary>
        /// Event handler for when the gamemode menu entry is selected.
        /// </summary>
       

        /// <summary>
        /// Event handler for when the gamemode menu entry is selected.
        /// </summary>
        void gamemodeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentgamemode = (currentgamemode + 1) % gamemodes.Length;
         //   SetMenuEntryText();

        }
       
        void comdifficultyMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentdifficulty = (currentdifficulty + 1) % difficulties.Length;
#if WINDOWS
          //  SetMenuEntryText();
#endif
       
#if XBOX

   if (currentdifficulty == 5 && comdifficultyMenuEntry.Text.Contains("available") == false&& Guide.IsTrialMode == true)
   {
       currentdifficulty = 4;
       comdifficultyMenuEntry.Text = "     Computer difficulty: " + difficulties[currentdifficulty] + " (Unbeatable isn't available in trial version)";

   }
   else if (comdifficultyMenuEntry.Text.Contains("God") == true && comdifficultyMenuEntry.Text.Contains("available") == true && Guide.IsTrialMode == true)
   {
       currentdifficulty = 0;
      // SetMenuEntryText();
   }
  // else SetMenuEntryText();
#endif

        }


        void playerspeedstaysequalMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            playerspeedstaysequal = !playerspeedstaysequal;
          //  SetMenuEntryText();
        }

        void timerrolloverMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
           
            
            if (timerrollover >= 1.2)
            {
                timerrollover = 0;
            }
            else { timerrollover += .05; }

            //  SetMenuEntryText();
      
        }


        /// <summary>
        /// Event handler for when the playerballstartingspeed menu entry is selected.
        /// </summary>
        void playerballstartingspeedMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (playerballstartingspeed == 20)
            {
                playerballstartingspeed = 1;
            }
            else { playerballstartingspeed++; }

            // SetMenuEntryText();
        }

        void timerdurationMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            double rateofincrease = .1;
            if (timerduration > 2.8)
                rateofincrease = .2;
            if (timerduration > 3.6)
                rateofincrease = .5;
            if (timerduration > 4.5)
                rateofincrease = .7;
            if (timerduration >= 6)
            {
                timerduration = .5;
            }
            else { timerduration+=rateofincrease; }

            // SetMenuEntryText();
             /*
#if XBOX
           if (Guide.IsTrialMode == true)
            {
                timerdurationMenuEntry.Text = "     Timer duration (seconds): Not changeable in trial version.";
                timerduration = 2;
            }
#endif
              */
        }

        void ballstartingspeedMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (ballstartingspeed == 20)
            {
                ballstartingspeed = 0;
            }
            else { ballstartingspeed++; }

            //     SetMenuEntryText();
        }

        void ballspeedmaxMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (ballspeedmax >= 44)
            {
                ballspeedmax = ballstartingspeed+1;
            }
            else { ballspeedmax+=4; }

            //   SetMenuEntryText();
#if XBOX
           if (Guide.IsTrialMode == true)
            {
                ballspeedmaxMenuEntry.Text = "     Ball's maximum speed: Not changeable in trial version.";
               ballspeedmax = 23;
            }
#endif
        }

        void playerspeedmaxMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (playerspeedmax >= 39)
            {
                playerspeedmax = playerballstartingspeed+1;
            }
            else { playerspeedmax+=4; }

            //  SetMenuEntryText();
#if XBOX
           if (Guide.IsTrialMode == true)
            {
                playerspeedmaxMenuEntry.Text = "     Player's maximum speed: Not changeable in trial version.";
                playerspeedmax = 18;
            }
#endif
        }

        void havecomputerplayerMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            havecomputerplayer = !havecomputerplayer;
            // SetMenuEntryText();
        }

        void ballspeedincreaserMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (ballspeedincreaser == 15)
            {
                ballspeedincreaser = -10;
            }
            else { ballspeedincreaser+=1; }

            //  SetMenuEntryText();
        }

        void playerspeedincreaserMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (playerspeedincreaser == 15)
            {
                playerspeedincreaser = -10;
            }
            else { playerspeedincreaser+=1; }

           // SetMenuEntryText();
        }

        void ballpointvalueMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (ballpointvalue == 5)
            {
                ballpointvalue = -5;
            }
            else { ballpointvalue++; }

           // SetMenuEntryText();
            #if XBOX
            if (Guide.IsTrialMode == true)
            {
                ballpointvalueMenuEntry.Text = "     Ball point value: Not changeable in trial version.";
                ballpointvalue = 1;
            }
            #endif
        }

        void evilballpointvalueMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            
                if (evilballpointvalue == 5)
                {
                    evilballpointvalue = -5;
                }
                else { evilballpointvalue++; }
               // SetMenuEntryText();
#if XBOX
           if (Guide.IsTrialMode == true)
            {
                evilballpointvalueMenuEntry.Text = "     Evil ball point value: No evil ball in trial version.";
               evilballpointvalue = 0;
            }
#endif

              
        }

        void resetdefaultsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OptionsMenuScreen.currentgamemode = 2;
            OptionsMenuScreen.currentdifficulty = 1;
            OptionsMenuScreen.havecomputerplayer = true;
            OptionsMenuScreen.playerspeedstaysequal = true;
            OptionsMenuScreen.playerballstartingspeed = 5;
            OptionsMenuScreen.ballstartingspeed = 5;
            OptionsMenuScreen.ballspeedmax = 23;
            OptionsMenuScreen.playerspeedmax = 18;
            OptionsMenuScreen.ballspeedwallincreaser = 0;
            OptionsMenuScreen.ballspeedincreaser = 5;
            OptionsMenuScreen.playerspeedincreaser = 5;
            OptionsMenuScreen.ballpointvalue = 1;
            OptionsMenuScreen.evilballpointvalue = 0;
            OptionsMenuScreen.pointstowin = 40;
            OptionsMenuScreen.timerduration = 2;
            OptionsMenuScreen.timerrollover = .5;
          //  SetMenuEntryText();
        }    


        void pointstowinMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            
            if (pointstowin == 9999999)
                pointstowin = 5;
            if (pointstowin >= 150)
            {
                pointstowin = 9999999;
               
            }
            else { pointstowin+=15; }
            

          //  SetMenuEntryText();
#if XBOX
            if (Guide.IsTrialMode == true)
            {
                pointstowinMenuEntry.Text = "     Points needed to win: Not changeable in trial version.";
                pointstowin = 40;
            }
#endif
        }


        
    }
}
