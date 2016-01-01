//#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


//#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Net;


namespace StarterGame
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        //#region Initialization
#if XBOX
        public static MenuEntry unlockgameMenuEntry;
#endif
        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base(Global_Variables.gameNAME)//name of game
        {
            // Create our menu entries.
            MenuEntry startMenuEntry = new MenuEntry("Single Player");
            MenuEntry multiplayerMenuEntry = new MenuEntry("Multiplayer");
            MenuEntry howtoplayMenuEntry = new MenuEntry("How To Play");
            MenuEntry howtoplaytextMenuEntry = new MenuEntry("Instructions");
            MenuEntry boxartMenuEntry = new MenuEntry("View Boxart");
            MenuEntry ClearHighscoreMenuEntry = new MenuEntry("Clear Highscores");
            MenuEntry screenshotsMenuEntry = new MenuEntry("View Screenshots");

            MenuEntry optionsMenuEntry = new MenuEntry("Instructions");
            MenuEntry togglefullscreenmodeMenuEntry = new MenuEntry("Toggle Fullscreen Mode");
#if XBOX
            unlockgameMenuEntry = new MenuEntry("Unlock full version!");
            if (Guide.IsTrialMode == true)
            {
                MenuEntries.Add(unlockgameMenuEntry);
                unlockgameMenuEntry.Selected += unlockgameMenuEntrySelected;
            }
#endif

            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            MenuEntry singlePlayerMenuEntry = new MenuEntry(Resources.SinglePlayer);
            // MenuEntry liveMenuEntry = new MenuEntry(Resources.PlayerMatch);
            // MenuEntry systemLinkMenuEntry = new MenuEntry(Resources.SystemLink);


            MenuEntry liveMenuEntry = new MenuEntry("Xbox Live");
            MenuEntry systemLinkMenuEntry = new MenuEntry("System Link");

            //// MenuEntry exitMenuEntry = new MenuEntry(Resources.Exit);

            // Hook up menu event handlers.
            singlePlayerMenuEntry.Selected += SinglePlayerMenuEntrySelected;
            liveMenuEntry.Selected += LiveMenuEntrySelected;
            systemLinkMenuEntry.Selected += SystemLinkMenuEntrySelected;



            // Hook up menu event handlers.
            ClearHighscoreMenuEntry.Selected += ClearHighscoreMenuEntrySelected;
            howtoplayMenuEntry.Selected += howtoplayMenuEntrySelected;
            howtoplaytextMenuEntry.Selected += howtoplaytextMenuEntrySelected;
            boxartMenuEntry.Selected += boxartMenuEntrySelected;
            screenshotsMenuEntry.Selected += screenshotsMenuEntrySelected;
            startMenuEntry.Selected += GamePlayCHANGENAMEMenuEntrySelected;
            multiplayerMenuEntry.Selected += multiplayerMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;

            togglefullscreenmodeMenuEntry.Selected += togglefullscreenmodeMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu. Choose the order of menu entries here.
            MenuEntries.Add(startMenuEntry);

#if XBOX
            if (Global_Variables.gameHasMultiplayer)
                MenuEntries.Add(multiplayerMenuEntry);
#endif

            if (Global_Variables.gameHasNetworking)
            {
                //   MenuEntries.Add(singlePlayerMenuEntry); //pretty much pointless- almost to the point of deletability


                MenuEntries.Add(systemLinkMenuEntry);
                MenuEntries.Add(liveMenuEntry);
            }

            if (Global_Variables.showGraphicalHowToPlay)
                MenuEntries.Add(howtoplayMenuEntry);

            if (Global_Variables.showTextBasedHowToPlay)
                MenuEntries.Add(howtoplaytextMenuEntry);

            //       MenuEntries.Add(optionsMenuEntry);

            if (Global_Variables.showScreenshotsMenuOption)
                MenuEntries.Add(screenshotsMenuEntry);

            if (Global_Variables.showDebuggingMenuOptions)
            {
                MenuEntries.Add(boxartMenuEntry);
            }

            if (Global_Variables.showDebuggingMenuOptions)
            {
                MenuEntries.Add(ClearHighscoreMenuEntry);
            }

#if WINDOWS
            MenuEntries.Add(togglefullscreenmodeMenuEntry);
#endif
            MenuEntries.Add(exitMenuEntry);

        }


        void SinglePlayerMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Global_Variables.playerWhoBeganGame = e.PlayerIndex;
            switch (e.PlayerIndex)
            {
                case PlayerIndex.One:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 0;
                    break;
                case PlayerIndex.Two:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 1;
                    break;
                case PlayerIndex.Three:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 2;
                    break;
                case PlayerIndex.Four:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 3;
                    break;
            }
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new AAgameplayScreen(null));
        }

        void togglefullscreenmodeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Game1.togglefullscreen = true;
        }
        void ClearHighscoreMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Game1.clearHighscores = true;
        }

        /// <summary>
        /// Event handler for when the Live menu entry is selected.
        /// </summary>
        void LiveMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            CreateOrFindSession(NetworkSessionType.PlayerMatch, e.PlayerIndex);
        }


        /// <summary>
        /// Event handler for when the System Link menu entry is selected.
        /// </summary>
        void SystemLinkMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            CreateOrFindSession(NetworkSessionType.SystemLink, e.PlayerIndex);
        }


        /// <summary>
        /// Helper method shared by the Live and System Link menu event handlers.
        /// </summary>
        void CreateOrFindSession(NetworkSessionType sessionType,
                                 PlayerIndex playerIndex)
        {
            // First, we need to make sure a suitable gamer profile is signed in.
            ProfileSignInScreen profileSignIn = new ProfileSignInScreen(sessionType);

            // Hook up an event so once the ProfileSignInScreen is happy,
            // it will activate the CreateOrFindSessionScreen.
            profileSignIn.ProfileSignedIn += delegate
            {
                GameScreen createOrFind = new CreateOrFindSessionScreen(sessionType);

                ScreenManager.AddScreen(createOrFind, playerIndex);
            };

            // Activate the ProfileSignInScreen.
            ScreenManager.AddScreen(profileSignIn, playerIndex);
        }


        //#region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>


        void GamePlayCHANGENAMEMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

#if XBOX
            BackgroundScreen.player1isplaying = false;
            BackgroundScreen.player2isplaying = false;
            BackgroundScreen.player3isplaying = false;
            BackgroundScreen.player4isplaying = false;
#endif

            Global_Variables.playerWhoBeganGame = e.PlayerIndex;
            switch (e.PlayerIndex)
            {
                case PlayerIndex.One:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 0;
                    break;
                case PlayerIndex.Two:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 1;
                    break;
                case PlayerIndex.Three:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 2;
                    break;
                case PlayerIndex.Four:
                    Global_Variables.playerWhoBeganGameZeroIndexedInt = 3;
                    break;
            }

            BackgroundScreen.playerwhoselectedplay = e.PlayerIndex;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.One)
                BackgroundScreen.player1isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Two)
                BackgroundScreen.player2isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Three)
                BackgroundScreen.player3isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Four)
                BackgroundScreen.player4isplaying = true;
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new AAgameplayScreen(null));

        }

        void multiplayerMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (Guide.IsTrialMode == true
         && PlayerIndexExtensions.CanBuyGame(e.PlayerIndex)
             )
            {
                Guide.ShowMarketplace(e.PlayerIndex);
            }
            if (Guide.IsTrialMode)
            {

                MessageBoxScreen purchaseMessageBox;
                purchaseMessageBox = new MessageBoxScreen("Multiplayer gameplay is not available in the\ntrial version. Please purchase the game!\nA button = Okay", false);
                purchaseMessageBox.Accepted += purchaseMessageBoxCancelled;
                purchaseMessageBox.Cancelled += purchaseMessageBoxCancelled;
                ScreenManager.AddScreen(purchaseMessageBox, e.PlayerIndex);
            }
            if (Guide.IsTrialMode == false)
            {
                BackgroundScreen.player1isplaying = false;
                BackgroundScreen.player2isplaying = false;
                BackgroundScreen.player3isplaying = false;
                BackgroundScreen.player4isplaying = false;

                Global_Variables.playerWhoBeganGame = e.PlayerIndex;
                switch (e.PlayerIndex)
                {
                    case PlayerIndex.One:
                        Global_Variables.playerWhoBeganGameZeroIndexedInt = 0;
                        break;
                    case PlayerIndex.Two:
                        Global_Variables.playerWhoBeganGameZeroIndexedInt = 1;
                        break;
                    case PlayerIndex.Three:
                        Global_Variables.playerWhoBeganGameZeroIndexedInt = 2;
                        break;
                    case PlayerIndex.Four:
                        Global_Variables.playerWhoBeganGameZeroIndexedInt = 3;
                        break;

                }
                BackgroundScreen.playerwhoselectedplay = e.PlayerIndex;
                if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.One)
                    BackgroundScreen.player1isplaying = true;
                if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Two)
                    BackgroundScreen.player2isplaying = true;
                if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Three)
                    BackgroundScreen.player3isplaying = true;
                if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Four)
                    BackgroundScreen.player4isplaying = true;


                Guide.ShowSignIn(4, false);


                LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                                   new Black(null));
            }
        }


        void howtoplayMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            BackgroundScreen.playerwhoselectedplay = e.PlayerIndex;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.One)
                BackgroundScreen.player1isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Two)
                BackgroundScreen.player2isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Three)
                BackgroundScreen.player3isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Four)
                BackgroundScreen.player4isplaying = true;
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new HowToPlay());

        }

        void howtoplaytextMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            BackgroundScreen.playerwhoselectedplay = e.PlayerIndex;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.One)
                BackgroundScreen.player1isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Two)
                BackgroundScreen.player2isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Three)
                BackgroundScreen.player3isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Four)
                BackgroundScreen.player4isplaying = true;
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new HowToPlayText(null));

        }

        void boxartMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            BackgroundScreen.playerwhoselectedplay = e.PlayerIndex;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.One)
                BackgroundScreen.player1isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Two)
                BackgroundScreen.player2isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Three)
                BackgroundScreen.player3isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Four)
                BackgroundScreen.player4isplaying = true;
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new BoxArt());

        }

        void screenshotsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            BackgroundScreen.playerwhoselectedplay = e.PlayerIndex;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.One)
                BackgroundScreen.player1isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Two)
                BackgroundScreen.player2isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Three)
                BackgroundScreen.player3isplaying = true;
            if (BackgroundScreen.playerwhoselectedplay == PlayerIndex.Four)
                BackgroundScreen.player4isplaying = true;
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new Screenshots());

        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>

        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }
#if XBOX
        void unlockgameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (Guide.IsTrialMode == true
            &&PlayerIndexExtensions.CanBuyGame(e.PlayerIndex)
                )
            {
                Guide.ShowMarketplace(e.PlayerIndex);
            }
            else if (Guide.IsTrialMode)
            {
              
                 MessageBoxScreen purchaseMessageBox;
               purchaseMessageBox = new MessageBoxScreen("Please sign into an Xbox Live enabled profile that can purchase content.\nA button = Choose an Account\nB button = Cancel",false);
                purchaseMessageBox.Accepted += purchaseMessageBoxAccepted;
                purchaseMessageBox.Cancelled += purchaseMessageBoxCancelled;
                ScreenManager.AddScreen(purchaseMessageBox, e.PlayerIndex);
            }

        }
#endif
        void purchaseMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            // ExitScreen();
            Guide.ShowSignIn(1, true);
        }
        void purchaseMessageBoxCancelled(object sender, PlayerIndexEventArgs e)
        {
            //ExitScreen();

        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>

        protected override void OnCancel(PlayerIndex playerIndex)
        {

            const string message = "Are you sure you want to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();


        }



    }
    public static class PlayerIndexExtensions
    {
        public static bool CanBuyGame(this PlayerIndex player)
        {
            SignedInGamer gamer = Gamer.SignedInGamers[player];

            if (gamer == null)
                return false;

            if (!gamer.IsSignedInToLive)
                return false;

            return gamer.Privileges.AllowPurchaseContent;
        }
    }
}
