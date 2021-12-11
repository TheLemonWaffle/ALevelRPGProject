using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelRPGProject
{
    class World
    {
        private const char Player_SYMBOL = 'P'; //player object to place on board
        private const char SPACE = ' '; //Blank spaces
        private char[,] Players; //this handles the players current position
        private const int Scale = 10; //scale of the board e.g. 10x10
        private string WorldAcross;//what the across part of the grid looks like 
        private string WorldUp;//what the up part of the grid looks like 
        private int positionX;//players x position
        private int positionY;//players y position
        private const char Object1 = '-';//these act as wall objects that the player cant pass through 
        private const char Object2 = '|';
        private const char ENEMY_Symbol = 'E';// enemy object to place on board and trigger combat
        private const char WORLD1 = '1';// world1 object to place on board and trigger world movement
        private const char WORLD2 = '2';// world2 object to place on board and trigger world movement
        private const char WORLD3 = '3';// world3 object to place on board and trigger world movement
        private const char WORLD4 = '4';// world4 object to place on board and trigger world movement
        private const char WIN = 'W';// WIN object to place on board and completion of the game
        private const char INN_symbol = 'I';// INN object to place on the board and trigger movement to the inn area
        private const char NPC_symbol = 'N';// NPC object to place on the board and trigger NPC interaction
        private const char DUNGEON_symbol = 'D';// DUNGEON object to place on the board and trigger movement to the DUNGEON area
        private const char Merchant_symbol = 'M';// MERCHANT object to place on the board and trigger merchant interaction
        private const char Chest_symbol = 'C';//CHEST object to place on the board and trigger Chests in dungeons
        private static int CurrentWorld;// currentworld counter 
        private bool WorldExit = false;
        private bool GameExit = false;
        protected Stack PreviousWorld = new Stack();//creates an instance of the stack class
        public World(Player player)//this is where the player is loaded into the world that matches their currentworld 
        {
            //this is  what controls the selection of the worlds
            while (GameExit == false)//this loops the game until the user tries to quit 
            {
                switch (CurrentWorld)//checks the current world and puts the player onto the respective board
                {
                    case 0:
                        while (CurrentWorld == 0)//loops until player isnt in world 0
                        {
                            //sets the world across and world up variable which make up the virtual representation of the board
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];//sets up the virtual board by setting it as a 10*10 board 
                            PopulateTutorial();//populates the board
                            //sets the coordinates of the player
                            positionX = 9;
                            positionY = 4;
                            WorldExit = false;
                            DisplayGrid(player);//displays the board which will have other object on aswell such as the player, inns, dungeons and enemies
                            //this virtually the same for each case 
                        }
                        break;
                    case 1:
                        while (CurrentWorld == 1)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateWorld1(player);
                            positionX = 4;
                            positionY = 4;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 2:
                        while (CurrentWorld == 2)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateWorld2(player);
                            positionX = 4;
                            positionY = 8;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 3:
                        while (CurrentWorld == 3)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateWorld3(player);
                            positionX = 4;
                            positionY = 1;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 4:
                        while (CurrentWorld == 4)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateWorld4(player);
                            positionX = 8;
                            positionY = 4;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 5:
                        while (CurrentWorld == 5)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[4, Scale];
                            PopulateInn();
                            positionX = 3;
                            positionY = 2;
                            WorldExit = false;
                            DisplayInn(player);
                        }
                        break;
                    case 6:
                        while (CurrentWorld == 6)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateInn();
                            positionX = 3;
                            positionY = 2;
                            WorldExit = false;
                            DisplayInn(player);
                        }
                        break;
                    case 7:
                        while (CurrentWorld == 7)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor1();
                            positionX = 8;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 8:
                        while (CurrentWorld == 8)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor2();
                            positionX = 0;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 9:
                        while (CurrentWorld == 9)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor3();
                            positionX = 9;
                            positionY = 6;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 10:
                        while (CurrentWorld == 10)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateInn();
                            positionX = 3;
                            positionY = 2;
                            WorldExit = false;
                            DisplayInn(player);
                        }
                        break;
                    case 11:
                        while (CurrentWorld == 11)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor1();
                            positionX = 8;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 12:
                        while (CurrentWorld == 12)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor2();
                            positionX = 0;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 13:
                        while (CurrentWorld == 13)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor3();
                            positionX = 9;
                            positionY = 6;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 14:
                        while (CurrentWorld == 14)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateInn();
                            positionX = 3;
                            positionY = 2;
                            WorldExit = false;
                            DisplayInn(player);
                        }
                        break;
                    case 15:
                        while (CurrentWorld == 15)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor1();
                            positionX = 8;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 16:
                        while (CurrentWorld == 16)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor2();
                            positionX = 0;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 17:
                        while (CurrentWorld == 17)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor3();
                            positionX = 9;
                            positionY = 6;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 18:
                        while (CurrentWorld == 18)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateInn();
                            positionX = 3;
                            positionY = 2;
                            WorldExit = false;
                            DisplayInn(player);
                        }
                        break;
                    case 19:
                        while (CurrentWorld == 19)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor1();
                            positionX = 8;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 20:
                        while (CurrentWorld == 20)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor2();
                            positionX = 0;
                            positionY = 0;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 21:
                        while (CurrentWorld == 21)
                        {
                            WorldAcross = "+---";
                            WorldUp = "| ";
                            Players = new char[Scale, Scale];
                            PopulateDungeonFloor3();
                            positionX = 9;
                            positionY = 6;
                            WorldExit = false;
                            DisplayGrid(player);
                        }
                        break;
                    case 22:
                        while (CurrentWorld == 22)
                        {
                            GameExit = true;
                        }
                        break;
                }
            }
        }// this handles where the player is dependent on their current world 
        public void PopulateTutorial()//populates the tutorial area
        {

        }
        public void PopulateWorld1(Player player)//populates the first world
        {
            for (int r = 0; r < Scale; r++)//loops every row
            {
                for (int c = 0; c < Scale; c++)//loops every column
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (player.GetQuestComplete(4) == true)
                    {
                        Players[9, 0] = WIN;
                    }
                    if (r == 4 & c == 4)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if (r == 2 & c == 7)
                    {
                        if (player.GetQuestAquired(CurrentWorld) == true & player.GetQuestBossDefeat(CurrentWorld) == false)
                        {
                            Players[r, c] = DUNGEON_symbol;
                        }
                    }
                    else if (r == 7 & c == 7)
                    {
                        Players[r, c] = INN_symbol;
                    }
                    else if (r == 4 & c == 0)
                    {
                        if (player.GetQuestComplete(1) == true)
                        {
                            Players[r, c] = WORLD2;
                        }
                    }
                    else if (r == 4 & c == 9)
                    {
                        if (player.GetQuestComplete(2) == true)
                        {
                            Players[r, c] = WORLD3;
                        }
                    }
                    else if (r == 0 & c == 4)
                    {
                        if (player.GetQuestComplete(3) == true)
                        {
                            Players[r, c] = WORLD4;
                        }
                    }
                    else if (r == 7 & c == 2)
                    {
                        Players[r, c] = ENEMY_Symbol;
                    }
                    else
                    {
                        Players[r, c] = SPACE;
                    }

                }
            }
        }
        public void PopulateWorld2(Player player)//populates the second world
        {
            for (int r = 0; r < Scale; r++)//loops for every row
            {
                for (int c = 0; c < Scale; c++)//loops for every column
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (r == 4 & c == 8)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if (r == 2 & c == 7)
                    {
                        if (player.GetQuestAquired(CurrentWorld) == true & player.GetQuestBossDefeat(CurrentWorld) == false)
                        {
                            Players[r, c] = DUNGEON_symbol;
                        }
                    }
                    else if (r == 4 & c == 9)
                    {
                        Players[r, c] = WORLD1;
                    }
                    else if (r == 7 & c == 7)
                    {
                        Players[r, c] = INN_symbol;
                    }
                    else
                    {
                        Players[r, c] = SPACE;
                    }

                }
            }
        }
        public void PopulateWorld3(Player player)//populates the third world
        {
            for (int r = 0; r < Scale; r++)//loops for every row
            {
                for (int c = 0; c < Scale; c++)//loops for every column
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (r == 4 & c == 1)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if (r == 4 & c == 0)
                    {
                        Players[r, c] = WORLD1;
                    }
                    else if (r == 2 & c == 7)
                    {
                        if (player.GetQuestAquired(CurrentWorld) == true & player.GetQuestBossDefeat(CurrentWorld) == false)
                        {
                            Players[r, c] = DUNGEON_symbol;
                        }
                    }
                    else if (r == 7 & c == 7)
                    {
                        Players[r, c] = INN_symbol;
                    }
                    else
                    {
                        Players[r, c] = SPACE;
                    }

                }
            }
        }
        public void PopulateWorld4(Player player)//populates the fourth world
        {
            for (int r = 0; r < Scale; r++)//loops for every row
            {
                for (int c = 0; c < Scale; c++)//loops for every column
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (r == 8 & c == 4)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if (r == 2 & c == 7)
                    {
                        if (player.GetQuestAquired(CurrentWorld) == true & player.GetQuestBossDefeat(CurrentWorld) == false)
                        {
                            Players[r, c] = DUNGEON_symbol;
                        }
                    }
                    else if (r == 9 & c == 4)
                    {
                        Players[r, c] = WORLD1;
                    }
                    else if (r == 7 & c == 7)
                    {
                        Players[r, c] = INN_symbol;
                    }
                    else
                    {
                        Players[r, c] = SPACE;
                    }

                }
            }
        }
        public void PopulateInn()//populates the inn
        {
            for (int r = 0; r < 5; r++)//loops for every row 
            {
                for (int c = 0; c < Scale; c++)//loops for every column 
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (CurrentWorld == 5)
                    {

                    }
                    else if (CurrentWorld == 6)
                    {
                        Players[4, 2] = WORLD1;
                    }
                    else if (CurrentWorld == 10)
                    {
                        Players[4, 2] = WORLD2;
                    }
                    else if (CurrentWorld == 14)
                    {
                        Players[4, 2] = WORLD3;
                    }
                    else if (CurrentWorld == 18)
                    {
                        Players[4, 2] = WORLD4;
                    }
                    if (r == 3 & c == 2)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if (r == 2 & c == 9)
                    {
                        Players[r, c] = NPC_symbol;
                    }
                    else if (r == 4 & c == 2)
                    {
                        Players[r, c] = WORLD1;
                    }
                    else if (r == 1 & c == 2 || r == 1 & c == 3 || r == 1 & c == 4 || r == 1 & c == 5 || r == 1 & c == 6)
                    {
                        Players[r, c] = Object1;
                    }
                    else if (r == 0 & c == 2 || r == 0 & c == 6)
                    {
                        Players[r, c] = Object2;
                    }
                    else if (r == 0 & c == 4)
                    {
                        Players[r, c] = Merchant_symbol;
                    }
                    else
                    {
                        Players[r, c] = SPACE;
                    }

                }
            }
        }
        public void DisplayInn(Player player)//displays the inn area 
        {
            while (WorldExit == false)//loops until
            {
                Console.Clear();
                Console.WriteLine("    0   1   2   3   4   5   6   7   8   9");//prints the numbers at the top of the board
                for (int r = 0; r < 5; r++)//loops for every row
                {
                    Console.Write("  ");//prints blank space
                    for (int c = 0; c < Scale; c++)//loops for every column
                    {
                        Console.Write(WorldAcross);//prints +--- onto the board 5 times 
                    }
                    Console.Write("+\n");//ends the row of +--- with a + then moves to next line

                    for (int c = 0; c < Scale; c++)//loops for every column
                    {
                        if (c == 0)// if column is equal to 0 then it prints the num of the row and puts a space down
                        {
                            Console.Write(r + " ");
                        }
                        Console.Write(WorldUp + Players[r, c] + " ");//prints | , a player coord e.g. blank char and a blank space

                    }
                    Console.Write("|\n");//prints | and moves to the next page

                }
                Console.Write("  ");
                for (int c = 0; c < Scale; c++)//loops for every column 
                {
                    Console.Write(WorldAcross);//prints +--- across the bottom of the board
                }
                Console.Write("+\n\n");//pints + in the bottom right corner, moves to the next 2 lines
                GetInput(player);//gets a user input 

            }
        }
        public void PopulateDungeonFloor1()//populates the first floor of the dungeon
        {
            for (int r = 0; r < Scale; r++)//loops for every row
            {
                for (int c = 0; c < Scale; c++)//loops for every column
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (CurrentWorld == 7)
                    {
                        Players[9, 0] = WORLD1;
                    }
                    if (CurrentWorld == 11)
                    {
                        Players[9, 0] = WORLD2;
                    }
                    if (CurrentWorld == 15)
                    {
                        Players[9, 0] = WORLD3;
                    }
                    if (CurrentWorld == 19)
                    {
                        Players[9, 0] = WORLD4;
                    }

                    if (r == 8 & c == 0)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if(r==0 & c==2 || r == 0 & c == 4|| r == 2 & c == 6)
                    {
                        Players[r, c] = Chest_symbol;
                    }
                    else if (r == 0 & c == 3 || r == 0 & c == 5 || r == 1 & c == 5 || r == 1 & c == 7 || r == 1 & c == 9 || r == 2 & c == 5 || r == 2 & c == 7 || r == 2 & c == 9 || r == 3 & c == 5 || r == 3 & c == 9 || r == 4 & c == 5 || r == 4 & c == 9 || r == 5 & c == 1 || r == 5 & c == 3 || r == 5 & c == 9 || r == 6 & c == 1 || r == 6 & c == 3 || r == 7 & c == 1 || r == 7 & c == 3 || r == 8 & c == 1 || r == 9 & c == 1|| r == 4 & c == 3)
                    {
                        Players[r, c] = Object2;
                    }
                    else if(r == 1 & c == 1 || r == 1 & c == 2 || r == 1 & c == 3 || r == 3 & c == 0 || r == 3 & c == 1 || r == 3 & c == 2 || r == 3 & c == 3 || r == 3 & c == 6 || r == 3 & c == 7 || r == 6 & c == 5 || r == 6 & c == 6 || r == 6 & c == 7 || r == 6 & c == 8 || r == 6 & c == 9 || r == 8 & c == 3 || r == 8 & c == 4 || r == 8 & c == 5 || r == 8 & c == 6 || r == 8 & c == 7 || r == 8 & c == 8)
                    {
                        Players[r, c] = Object1;
                    }
                    else if (r == 1 & c == 0 || r == 1 & c == 6 || r == 5 & c == 5)
                    {
                        Players[r, c] = ENEMY_Symbol;
                    }
                }
            }
        }
        public void PopulateDungeonFloor2()//populates the second floor of the dungeon
        {
            for (int r = 0; r < Scale; r++)//loops for every row
            {
                for (int c = 0; c < Scale; c++)//loops for every column
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (r == 0 & c == 0)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if (r == 2 & c == 1 || r == 4 & c == 6 || r == 2 & c == 9)
                    {
                        Players[r, c] = ENEMY_Symbol;
                    }
                    else if (r == 1 & c == 0 || r == 1 & c == 1 || r == 1 & c == 2 || r == 1 & c == 3 || r == 8 & c == 7 || r == 8 & c == 8 || r == 8 & c == 9 || r == 6 & c == 6 || r == 6 & c == 7 || r == 6 & c == 8 || r == 4 & c == 7 || r == 4 & c == 8 || r == 4 & c == 9 || r == 2 & c == 6 || r == 2 & c == 7 || r == 2 & c == 8 || r == 0 & c == 7 || r == 0 & c == 8 || r == 0 & c == 9)
                    {
                        Players[r, c] = Object1;
                    }
                    else if (r == 3 & c == 1 || r == 4 & c == 1 || r == 5 & c == 1 || r == 6 & c == 1 || r == 7 & c == 1 || r == 8 & c == 1 || r == 9 & c == 1 || r == 0 & c == 5 || r == 1 & c == 5 || r == 2 & c == 5 || r == 3 & c == 5 || r == 4 & c == 5 || r == 5 & c == 5 || r == 6 & c == 5 || r == 7 & c == 5 || r == 8 & c == 5 || r == 2 & c == 3 || r == 3 & c == 3 || r == 4 & c == 3 || r == 5 & c == 3 || r == 6 & c == 3 || r == 7 & c == 3 || r == 8 & c == 3)
                    {
                        Players[r, c] = Object2;
                    }
                    else if (r == 9 & c == 0 || r == 9 & c == 9)
                    {
                        Players[r, c] = Chest_symbol;
                    }
                    else
                    {
                        Players[r, c] = SPACE;
                    }

                }
            }
        }
        public void PopulateDungeonFloor3()//populates the third floor of the dungeon
        {
            for (int r = 0; r < Scale; r++)//loops for every row
            {
                for (int c = 0; c < Scale; c++)//loops for every column
                {
                    //when the row and column matches the if statements below it prints an object on the board
                    if (CurrentWorld == 9)
                    {
                        Players[9, 8] = WORLD1;

                    }
                    else if (CurrentWorld == 13)
                    {
                        Players[9, 8] = WORLD2;
                    }
                    else if (CurrentWorld == 17)
                    {
                        Players[9, 8] = WORLD3;
                    }
                    else if (CurrentWorld == 21)
                    {
                        Players[9, 8] = WORLD4;
                    }
                    if (r == 9 & c == 6)
                    {
                        Players[r, c] = Player_SYMBOL;
                    }
                    else if (r == 9 & c == 3)
                    {
                        Players[r, c] = Merchant_symbol;
                    }
                    else if (r == 2 & c == 7)
                    {
                        Players[r, c] = ENEMY_Symbol;
                    }
                    else if (r == 9 & c == 5 || r == 8 & c == 5 || r == 7 & c == 5 || r == 9 & c == 4 || r == 9 & c == 2 || r == 0 & c == 7 || r == 1 & c == 7 || r == 9 & c == 7 || r == 3 & c == 7 || r == 4 & c == 7 || r == 5 & c == 7 || r == 6 & c == 7 || r == 7 & c == 7 || r == 8 & c == 7)
                    {
                        Players[r, c] = Object2;
                    }
                    else if (r == 9 & c == 9 || r == 5 & c == 1 || r == 5 & c == 2 || r == 5 & c == 3 || r == 5 & c == 4 || r == 5 & c == 5 || r == 5 & c == 6 || r == 5 & c == 7 || r == 8 & c == 2 || r == 8 & c == 3 || r == 8 & c == 4)
                    {
                        Players[r, c] = Object1;
                    }
                    else if (r == 4 & c == 9 || r == 6 & c == 9)
                    {
                        Players[r, c] = Chest_symbol;
                    }
                    else
                    {
                        Players[r, c] = SPACE;
                    }

                }
            }
        }
        public void DisplayGrid(Player player)//displays the grid 
        {
            while (WorldExit == false)//prints the grid until the user exits the game
            {
                Console.Clear();
                Console.WriteLine("    0   1   2   3   4   5   6   7   8   9");//prints the numbers at the top of the board
                for (int r = 0; r < Scale; r++)//loops for every row
                {
                    Console.Write("  ");//prints blank space
                    for (int c = 0; c < Scale; c++)//loops for every column
                    {
                        Console.Write(WorldAcross);//prints +--- onto the board 5 times 
                    }
                    Console.Write("+\n");//ends the row of +--- with a + then moves to next line

                    for (int c = 0; c < Scale; c++)//loops for every column
                    {
                        if (c == 0)
                        {
                            Console.Write(r + " ");
                        }
                        Console.Write(WorldUp + Players[r, c] + " ");//prints | , a player coord e.g. blank char and a blank space
                    }
                    Console.Write("|\n");//prints | and moves to the next page

                }
                Console.Write("  ");
                for (int c = 0; c < Scale; c++)//loops for every column
                {
                    Console.Write(WorldAcross);//prints +--- across the bottom of the board
                }
                Console.Write("+\n\n");//pints + in the bottom right corner, moves to the next 2 lines
                GetInput(player) ;//gets a user input 

            }
        }
        public void GetInput(Player player)//gets input from the player
        {

            bool input = false;
            ConsoleKey command = ConsoleKey.W;
            while (input == false)//could also write while (!input)//loops while user input is false
            {
                Console.WriteLine("Which direction do you want to move in Foward(w) Backward(s) Left(a) Right(d) Menu(M)");//prompt user for movement, menu and exit input
                command = Console.ReadKey().Key;
                if (command==ConsoleKey.W || command == ConsoleKey.S || command == ConsoleKey.A || command == ConsoleKey.D || command == ConsoleKey.M)//validates inputs
                {
                    input = true;
                }

            }
            switch (command)
            {
                case ConsoleKey.W:
                    if (CurrentWorld == 5 || CurrentWorld == 6 || CurrentWorld == 10 || CurrentWorld == 14 | CurrentWorld == 18)//this handles the input for users moving foward in the inns
                    {
                        if (positionX - 1 < 0)//breaks if the player moves out of the bound of the virtual grid
                        {
                            break;
                        }
                        if (positionX - 1 == 1 & positionY == 4)//if they move into the merchant
                        {
                            Console.Write("You have interacted with the merchant");
                            Console.ReadLine();
                            player.Merchant();
                            break;
                        }
                        if (Players[positionX - 1, positionY] == Object1)//breaks if the player moves into object1
                        {
                            break;
                        }
                        if (Players[positionX - 1, positionY] == Object2)//breaks if the player moves into object2
                        {
                            break;
                        }
                        if (Players[positionX - 1, positionY] == NPC_symbol)//when the user interacts with the npc symbol
                        {
                            player.Npc();
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD2)//when the user interacts with the world 2 teleport pad
                        {
                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD1)//when the user interacts with the world 1 teleport pad
                        {

                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD3)//when the user interacts with the world 3 teleport pad
                        {

                            CurrentWorld = 3;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD4)//when the user interacts with the world 4 teleport pad
                        {

                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }
                    }
                    else//if the player is anywhere else then accept these inputs and interactions
                    {
                        if (positionX - 1 < 0)//break if people move out of the bounds of the virtual grid
                        {
                            break;
                        }
                        if (positionX - 1 == 0 & positionY == 6)//if the position of the player is 1,6 
                        {
                            if (CurrentWorld == 8)//if they are on the second floor of the dungeon it moves them to the third floor
                            {
                                PreviousWorld.Push(CurrentWorld);
                                CurrentWorld = 9;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 12)//if they are on the second floor of the dungeon it moves them to the third floor
                            {
                                PreviousWorld.Push(CurrentWorld);
                                CurrentWorld = 13;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 16)//if they are on the second floor of the dungeon it moves them to the third floor
                            {
                                PreviousWorld.Push(CurrentWorld);
                                CurrentWorld = 17;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 20)//if they are on the second floor of the dungeon it moves them to the third floor
                            {
                                PreviousWorld.Push(CurrentWorld);
                                CurrentWorld = 21;
                                WorldExit = true;
                                break;
                            }

                        }
                        if (Players[positionX - 1, positionY] == Object1)//breaks if the user moves into object1
                        {
                            break;
                        }
                        if (Players[positionX - 1, positionY] == Object2)//breaks if the user moves into object2
                        {
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD2)//moves the player to the second world if the user moves onto the world2 symbol
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD1)//moves the player to the first world if the user moves onto the world1 symbol
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == Chest_symbol)//runs the chest sub routine if the user moves onto the chest symbol
                        {
                            player.Chest();
                            Players[positionX - 1, positionY] = SPACE;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD3)//moves the player to the third world if the user moves onto the world3 symbol
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 3;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == WORLD4)//moves the player to the fourth world if the user moves onto the world4 symbol
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX - 1, positionY] == DUNGEON_symbol)//moves the player to the first floor of the dungeon dependant on their current world
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 1)
                            {
                                CurrentWorld = 7;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 11;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 15;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 19;
                                WorldExit = true;

                            }
                            break;
                        }
                        if (Players[positionX - 1, positionY] == ENEMY_Symbol)//starts combat with the enemy if the user moves onto the enemy symbol
                        {
                            Combat combat = new Combat(player);//creates an instance of the combat class

                            if (CurrentWorld == 7 || CurrentWorld == 8 || CurrentWorld == 9 || CurrentWorld == 11 || CurrentWorld == 12 || CurrentWorld == 13 || CurrentWorld == 15 || CurrentWorld == 16 || CurrentWorld == 17 || CurrentWorld == 19 || CurrentWorld == 20 || CurrentWorld == 21)
                            {
                                Players[positionX - 1, positionY] = SPACE;
                            }

                            break;
                        }
                        if (Players[positionX - 1, positionY] == INN_symbol)//moves the player into an inn dependand on their world
                        {
                            if (CurrentWorld == 0)
                            {
                                CurrentWorld = 5;
                                WorldExit = true;
                            }
                            else if (CurrentWorld == 1)
                            {
                                CurrentWorld = 6;
                                WorldExit = true;
                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 10;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 14;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 18;
                                WorldExit = true;

                            }
                            break;
                        }
                    }
                    Players[positionX - 1, positionY] = Player_SYMBOL;//moves the player symbol foward one space
                    Players[positionX, positionY] = SPACE;//makes the current space on the visual board blank
                    positionX--;//adjusts the players coordinates on virtual board

                    break;
                case ConsoleKey.S:
                    if (CurrentWorld == 5 || CurrentWorld == 6 || CurrentWorld == 10 || CurrentWorld == 14 | CurrentWorld == 18)
                    {
                        if (positionX + 1 > 4)
                        {
                            break;
                        }
                        if (Players[positionX + 1, positionY] == Object1)
                        {
                            break;
                        }
                        if (Players[positionX + 1, positionY] == Object2)
                        {
                            break;
                        }
                        if (Players[positionX + 1, positionY] == NPC_symbol)
                        {
                            player.Npc();
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD2)
                        {

                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD1)
                        {

                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD3)
                        {

                            CurrentWorld = 3;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD4)
                        {

                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }

                    }
                    else
                    {

                        
                        if (positionX + 1 > 9)
                        {
                            break;
                        }
                        if (Players[positionX + 1, positionY] == Chest_symbol)
                        {
                            player.Chest();
                            Players[positionX + 1, positionY] = SPACE;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WIN)
                        {
                            Win();
                            break;
                        }
                        if (CurrentWorld == 9 || CurrentWorld == 13 || CurrentWorld == 17 || CurrentWorld == 21)
                        {
                            if (positionX + 1 == 8 & positionY == 3)
                            {
                                player.Merchant();
                                break;
                            }

                        }
                        if (Players[positionX + 1, positionY] == Object1)
                        {
                            break;
                        }
                        if (Players[positionX + 1, positionY] == Object2)
                        {
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD2)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 13)
                            {
                                player.SetQuestBossDefeat(true, 2);
                                Console.Clear();
                                Console.WriteLine("You have beaten the boss of World2");
                                Console.ReadLine();
                            }
                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD1)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 9)
                            {
                                player.SetQuestBossDefeat(true,1);
                                Console.Clear();
                                Console.WriteLine("You have beaten the boss of World1");
                                Console.ReadLine();
                            }
                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD3)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 17)
                            {
                                player.SetQuestBossDefeat(true,3);
                                Console.Clear();
                                Console.WriteLine("You have beaten the boss of World3");
                                Console.ReadLine();
                            }
                            CurrentWorld = 3;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == WORLD4)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 21)
                            {
                                player.SetQuestBossDefeat(true, 4);
                                Console.Clear();
                                Console.WriteLine("You have beaten the boss of World4");
                                Console.ReadLine();
                            }
                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX + 1, positionY] == DUNGEON_symbol)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 1)
                            {
                                CurrentWorld = 7;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 11;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 15;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 19;
                                WorldExit = true;

                            }
                            break;
                        }
                        if (Players[positionX + 1, positionY] == ENEMY_Symbol)
                        {

                            Combat combat = new Combat(player);
                            if (CurrentWorld == 7 || CurrentWorld == 8 || CurrentWorld == 9 || CurrentWorld == 11 || CurrentWorld == 12 || CurrentWorld == 13 || CurrentWorld == 15 || CurrentWorld == 16 || CurrentWorld == 17 || CurrentWorld == 19 || CurrentWorld == 20 || CurrentWorld == 21)
                            {
                                Players[positionX + 1, positionY] = SPACE;
                            }
                            break;
                        }
                        if (Players[positionX + 1, positionY] == INN_symbol)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 0)
                            {
                                CurrentWorld = 5;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 1)
                            {
                                CurrentWorld = 6;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 10;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 14;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 18;
                                WorldExit = true;

                            }
                            break;
                        }

                    }
                    Players[positionX + 1, positionY] = Players[positionX, positionY];
                    Players[positionX, positionY] = SPACE;
                    positionX++;
                    break;
                case ConsoleKey.A:
                    if (CurrentWorld == 5 || CurrentWorld == 6 || CurrentWorld == 10 || CurrentWorld == 14 | CurrentWorld == 18)
                    {
                        if (positionY - 1 < 0)
                        {
                            break;
                        }
                        if (Players[positionX, positionY - 1] == Object1)
                        {
                            break;
                        }
                        if (Players[positionX, positionY - 1] == Object2)
                        {
                            break;
                        }
                        if (Players[positionX, positionY - 1] == NPC_symbol)
                        {
                            player.Npc();
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WORLD2)
                        {

                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WORLD1)
                        {

                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WORLD3)
                        {

                            CurrentWorld = 3;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WORLD4)
                        {

                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }
                    }
                    else
                    {
                        if (positionY - 1 < 0)
                        {
                            break;
                        }
                        if (Players[positionX, positionY - 1] == Chest_symbol)
                        {
                            player.Chest();
                            Players[positionX, positionY - 1] = SPACE;
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WIN)
                        {
                            Win();
                            break;
                        }
                        if (Players[positionX, positionY - 1] == Object1)
                        {
                            break;
                        }
                        if (Players[positionX, positionY - 1] == Object2)
                        {
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WORLD2)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WORLD1)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY - 1] == WORLD4)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY - 1] == DUNGEON_symbol)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 1)
                            {
                                CurrentWorld = 7;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 11;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 15;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 19;
                                WorldExit = true;
                                break;
                            }
                            break;
                        }
                        if (Players[positionX, positionY - 1] == ENEMY_Symbol)
                        {

                            Combat combat = new Combat(player);
                            if (CurrentWorld == 7 || CurrentWorld == 8 || CurrentWorld == 9 || CurrentWorld == 11 || CurrentWorld == 12 || CurrentWorld == 13 || CurrentWorld == 15 || CurrentWorld == 16 || CurrentWorld == 17 || CurrentWorld == 19 || CurrentWorld == 20 || CurrentWorld == 21)
                            {
                                Players[positionX, positionY - 1] = SPACE;
                            }

                            break;
                        }
                        if (Players[positionX, positionY - 1] == INN_symbol)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 0)
                            {
                                CurrentWorld = 5;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 1)
                            {
                                CurrentWorld = 6;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 10;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 14;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 18;
                                WorldExit = true;

                            }
                            break;
                        }
                    }
                    Players[positionX, positionY - 1] = Players[positionX, positionY];
                    Players[positionX, positionY] = SPACE;
                    positionY--;
                    break;
                case ConsoleKey.D:
                    if (CurrentWorld == 5 || CurrentWorld == 6 || CurrentWorld == 10 || CurrentWorld == 14 | CurrentWorld == 18)
                    {
                        if (positionY + 1 > 9)
                        {
                            break;
                        }
                        if (Players[positionX, positionY + 1] == Object1)
                        {
                            break;
                        }
                        if (Players[positionX, positionY + 1] == Object2)
                        {
                            break;
                        }
                        if (Players[positionX, positionY + 1] == NPC_symbol)
                        {
                            player.Npc();
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD2)
                        {

                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD1)
                        {

                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD3)
                        {

                            CurrentWorld = 3;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD4)
                        {

                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }
                    }
                    else
                    {

                        if (positionY + 1 > 9)
                        {
                            break;
                        }
                        if (Players[positionX, positionY + 1] == Chest_symbol)
                        {
                            player.Chest();
                            Players[positionX, positionY + 1] = SPACE;
                            break;
                        }
                        if (positionX == 0 & positionY + 1 == 9)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 7)
                            {
                                CurrentWorld = 8;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 11)
                            {
                                CurrentWorld = 12;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 15)
                            {
                                CurrentWorld = 16;
                                WorldExit = true;
                                break;
                            }
                            else if (CurrentWorld == 19)
                            {
                                CurrentWorld = 20;
                                WorldExit = true;
                                break;
                            }

                        }
                        if (Players[positionX, positionY + 1] == Object1)
                        {
                            break;
                        }
                        if (Players[positionX, positionY + 1] == Object2)
                        {
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD2)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 2;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD1)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 1;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD3)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 3;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY + 1] == WORLD4)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            CurrentWorld = 4;
                            WorldExit = true;
                            break;
                        }
                        if (Players[positionX, positionY + 1] == DUNGEON_symbol)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 1)
                            {
                                CurrentWorld = 7;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 11;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 15;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 19;
                                WorldExit = true;

                            }
                            break;
                        }
                        if (Players[positionX, positionY + 1] == ENEMY_Symbol)
                        {

                            Combat combat = new Combat(player);

                            if (CurrentWorld == 7 || CurrentWorld == 8 || CurrentWorld == 9 || CurrentWorld == 11 || CurrentWorld == 12 || CurrentWorld == 13 || CurrentWorld == 15 || CurrentWorld == 16 || CurrentWorld == 17 || CurrentWorld == 19 || CurrentWorld == 20 || CurrentWorld == 21)
                            {
                                Players[positionX, positionY + 1] = SPACE;
                            }

                            break;
                        }
                        if (Players[positionX, positionY + 1] == INN_symbol)
                        {
                            PreviousWorld.Push(CurrentWorld);
                            if (CurrentWorld == 0)
                            {
                                CurrentWorld = 5;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 1)
                            {
                                CurrentWorld = 6;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 2)
                            {
                                CurrentWorld = 10;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 3)
                            {
                                CurrentWorld = 14;
                                WorldExit = true;

                            }
                            else if (CurrentWorld == 4)
                            {
                                CurrentWorld = 18;
                                WorldExit = true;

                            }
                            break;
                        }
                    }

                    Players[positionX, positionY + 1] = Players[positionX, positionY];
                    Players[positionX, positionY] = SPACE;
                    positionY++;
                    break;
                case ConsoleKey.M:
                    GameMenu(player);
                    break;
            }
        }
        public void GameMenu(Player player)//main menu within the game that allows the player to access their questlog/SkillTree/Items/Stats/Saves
        {
            bool input = false;
            string option = "";
            while (option != "8")
            {
                while (input == false)
                {
                    Console.Clear();//menu in-game which allows you to look at your stats, items, skilltree progression and allows you to save your game
                    Console.WriteLine("What option do you want?");
                    Console.WriteLine("1. Stats");
                    Console.WriteLine("2. Items");
                    Console.WriteLine("3. Skill Tree");
                    Console.WriteLine("4. Questlog");
                    Console.WriteLine("5. Save Game");
                    Console.WriteLine("6. Teleport to previous World");
                    Console.WriteLine("7. Exit");
                    Console.WriteLine("8. Back");
                    option = Console.ReadLine();
                    if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5" || option == "6" || option == "7" ||option == "8")
                    {
                        input = true;
                    }
                }
                switch (option)
                {
                    case "1":
                        player.DisplayStats();
                        option = "8";
                        break;
                    case "2":
                        player.Item();
                        option = "8";
                        break;
                    case "3":
                        player.Skilltree();
                        option = "8";
                        break;
                    case "4":
                        player.Questlog();
                        option = "8";
                        break;
                    case "5":
                        Save save = new Save(player);
                        option = "8";
                        break;
                    case "6":
                        Teleport(PreviousWorld);
                        option = "8";
                        break;
                    case "7":
                        WorldExit = true;
                        GameExit = true;
                        CurrentWorld = 22;
                        option = "8";
                        break;
                }
            }
        }
        public void Teleport(Stack PreviousWorld)
        {
            if(PreviousWorld.IsEmpty()==false)
            {
                if(PreviousWorld.Peak()==CurrentWorld)
                {
                    Console.WriteLine();
                    Console.WriteLine("You cannot teleport to the world you are currently on");
                    Console.ReadLine();
                }
                else if (PreviousWorld.Peak() == 7 || PreviousWorld.Peak() == 8 || PreviousWorld.Peak() == 9)
                {
                    if(CurrentWorld==1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You cannot teleport to the world you are currently on");
                        Console.ReadLine();
                    }
                    else
                    {
                        WorldExit = true;
                        CurrentWorld = 1;
                    }
                }
                else if (PreviousWorld.Peak() == 11 || PreviousWorld.Peak() == 12)
                {
                    if (CurrentWorld == 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You cannot teleport to the world you are currently on");
                        Console.ReadLine();
                    }
                    else
                    {
                        WorldExit = true;
                        CurrentWorld = 2;
                    }
                }
                else if (PreviousWorld.Peak() == 15 || PreviousWorld.Peak() == 16)
                {
                    if (CurrentWorld == 3)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You cannot teleport to the world you are currently on");
                        Console.ReadLine();
                    }
                    else
                    {
                        WorldExit = true;
                        CurrentWorld = 3;
                    }
                }
                else if (PreviousWorld.Peak() == 19 || PreviousWorld.Peak() == 20)
                {
                    if (CurrentWorld == 4)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You cannot teleport to the world you are currently on");
                        Console.ReadLine();
                    }
                    else
                    {
                        WorldExit = true;
                        CurrentWorld = 4;
                    }
                }
                else if (PreviousWorld.Peak() == 5 || PreviousWorld.Peak() == 6 || PreviousWorld.Peak() == 10 || PreviousWorld.Peak() == 14 || PreviousWorld.Peak() == 18 || PreviousWorld.Peak() == 13 || PreviousWorld.Peak() == 17 || PreviousWorld.Peak() == 21)
                {
                    Console.WriteLine();
                    Console.WriteLine("You cant Teleport back to there");
                    Console.ReadLine();
                }
                else
                {
                    WorldExit = true;
                    CurrentWorld = PreviousWorld.Peak();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You Have Not Yet Visited anyworlds");
                Console.ReadLine();
            }

        }
        public void Win()//this displays a message when the user wins the game
        {
            Console.WriteLine("You have won! Congratulations!");
            Console.ReadLine();
            CurrentWorld = 22;
            WorldExit = true;
        }
        public static int GetCurrentWorld()//returns the current world of the player
        {
            return CurrentWorld;
        }
        public static void SetCurrentWorld(int value)//sets the current world of the player
        {
            CurrentWorld = value;
        }
    }
}
