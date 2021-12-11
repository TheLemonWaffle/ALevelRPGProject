using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelRPGProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Create create = new Create();//creates an instance of the create class which wil create the database file and populate it with tables 
            Player player = new Player();//creates an instance of the player class which will be refered to throughout the program
            bool GameExit = false;//used to determine when the game ends
            string option = "";
            bool input = false;
            while (GameExit == false)//loops game
            {
                while (input == false)
                {
                    Console.Clear();// this is the main menu 
                    Console.WriteLine("Welcome to the console RPG");
                    Console.WriteLine("1. New Game");
                    Console.WriteLine("2. Load Game");
                    Console.WriteLine("3. Quit Game");
                    option = Console.ReadLine();
                    if (option == "1" || option == "2" || option == "3")
                    {
                        input = true;
                    }
                }
                input = false;
                
                switch (option)
                {
                    case "1":
                        option = "";
                        bool classInput = false;
                        while (classInput == false)
                        {
                            Console.Clear();
                            Console.WriteLine("Please select a class");// allows the player to choose their role
                            Console.WriteLine("1. Mage");
                            Console.WriteLine("2. Warrior");
                            Console.WriteLine("3. Archer");
                            Console.WriteLine("4. Back");
                            option = Console.ReadLine();
                            if (option == "1" || option == "2" || option == "3" || option == "4")
                            {
                                classInput = true;
                            }
                        }
                        if(option=="1")//if player chooses mage them assign the mage specific stats
                        {
                            Console.WriteLine("Mages are magic wielding characters that can launch fireballs and call forth thunderbolts");
                            player.SetRole("Mage");
                            
                        }
                        else if(option=="2")//if player chooses Warrior them assign the Warrior specific stats
                        {
                            Console.WriteLine("Warriors are sword wielding characters that attack with their pure strength performing heavy slashes and tanking enemy attacks");
                            player.SetRole("Warrior");

                        }
                        else if(option=="3")//if player chooses Archers them assign the Archer specific stats
                        {
                            Console.WriteLine("Archers are bow wielding characters that attack from range with flurries of arrows and power shots");
                            player.SetRole("Archer");
                        }
                        else
                        {
                            break;
                        }
                        //sets the variables for the player stats
                        player.SetPlayerID(create.AutoPlayerNumber());
                        World.SetCurrentWorld(1);
                        player.SetMaxHP(200);
                        player.SetCurrentHP(player.GetMaxHP());
                        player.SetMaxMana(50);
                        player.SetCurrentMana(player.GetMaxMana());
                        player.SetAttack(85);
                        player.SetDefense(35);
                        player.SetSpeed(60);
                        player.SetSkillpoints(2);
                        player.SetMoney(100);
                        Console.ReadLine();
                        //clears the console and prompt the user for its name
                        Console.Clear();
                        Console.WriteLine("Whats your name?");
                        player.SetName(Console.ReadLine());
                        player.DisplayStats();//displays the user stats
                        player.CreateQuestlog();
                        player.CreateItemInv();
                        player.CreateNPCStats();
                        player.CreateSkillInv();
                        player.CreateSkillTree();
                        World world = new World(player);
                        break;
                    case "2"://this loads a game that the player specifies 
                        Load load = new Load(player);
                        break;
                    case "3"://quits the game
                        GameExit = true;
                        break;
                }
            }
        }
    }
}