using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using ADOX;

namespace ALevelRPGProject
{
    class Player:Character
    {
        private int MaxMANA;// maximum resource that the player has to use skills
        private int CurrentMANA;// current amount of resource that the player is on 
        private string Role;// role of the player e.g. archer/warrior/wizard etc.
        private int PlayerID;// used to keep track of which player is current loaded into the game
        private int Skillpoints;// current amount of points the player has to spend on the skilltree
        Skills[] SkillInv = new Skills[4];// this creates an instance of the SkillInv struct
        NPC[] QuestVessel = new NPC[5];// this creates an instance of the QuestVessel struct
        Quests[] Quest = new Quests[5];// this creates an instance of the Quest struct
        Items[] ItemInv = new Items[3];// this creates an instance of the ItemInv struct
        SkillsTree[] TreeData = new SkillsTree[12];// this creates an instance of the TreeData struct
        public void Chest()// triggers the object interaction with chests on the board 
        {
            Console.Clear();
            if(World.GetCurrentWorld() ==8|| World.GetCurrentWorld() == 12 || World.GetCurrentWorld() == 16 || World.GetCurrentWorld() == 20)//checks if the player is on the second floor of all 4 dungeons
            {
                ItemInv[0].Amount += 1;//gives them a health potion
                Money += 50;//give the player 50 money
                Console.WriteLine("You opened the chest and found 1 {0} and 50z",ItemInv[0].Name);
            }
            else if(World.GetCurrentWorld() == 9 || World.GetCurrentWorld() == 13 || World.GetCurrentWorld() == 17 || World.GetCurrentWorld() == 21)//checks if the player is on the final floor of the dungeon
            {
                ItemInv[0].Amount += 5;//gives them a health potion
                ItemInv[1].Amount += 5;//gives them a mana potion
                Money += 150;//gives the player 150 money
                Console.WriteLine("You opened the chest and found 5 {0}, 5 {1} and 150z", ItemInv[0].Name, ItemInv[1].Name);
            }
            Console.ReadLine();
        }
        public void Skilltree()// skill tree is used to purchase skills and stats upgrades for the player by following the branches and purchasing nodes to unlock there rewards
        {
            bool Exit = false;
            int NodeTracker;
            string option = "";
            string NodeOption = "";
            bool input = false;
            bool NodeInput = false;
            while (Exit == false)
            {
                while (input == false)
                {
                    Console.Clear();
                    Console.WriteLine("This is a skill tree");//this is the menu that allows the user to choose which skill path they want to follow
                    Console.WriteLine("1. The {0} Path", SkillInv[0].Name);
                    Console.WriteLine("");
                    Console.WriteLine("2. The {0} Path", SkillInv[1].Name);
                    Console.WriteLine("");
                    Console.WriteLine("3. The {0} Path", SkillInv[2].Name);
                    Console.WriteLine("");
                    Console.WriteLine("4. The {0} Path", SkillInv[3].Name);
                    Console.WriteLine("");
                    Console.WriteLine("5. Back");
                    option = Console.ReadLine();
                    if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5")
                    {
                        NodeInput = false;
                        input = true;
                    }
                }
                switch (option)
                {
                    case "1":
                        NodeTracker = 0;//sets them on the first node of the path
                        while (NodeTracker != -1)//loops while node tracker is not equal to -1 which is the main menu
                        {

                            while (NodeInput == false)//loops until player input is true
                            {
                                Console.Clear();
                                if (NodeTracker != 2)//checks if the current node is not 2 which is the last node in the path it cannot display the next node message
                                {
                                    Console.WriteLine("Next Node: {0}", TreeData[TreeData[NodeTracker].NextNode].Name);//displays the next node in the skill tree
                                }
                                Console.WriteLine("Current Node: {0}", TreeData[NodeTracker].Name);//displays the current node the player is on
                                if (TreeData[NodeTracker].PreviousNode == -1)//checks if the previous node is equal to -1 as it will need to display a different message for people on the first node
                                {
                                    Console.WriteLine("Previous Node: Home Node");//displays the previous node as the home node
                                }
                                else//if its anything other than the first node then it will run this
                                {
                                    Console.WriteLine("Previous Node: {0}", TreeData[TreeData[NodeTracker].PreviousNode].Name);//displays the previous node of the current node
                                }
                                
                                if (TreeData[NodeTracker].Aquired == true)//checks if the current node has been acquired
                                {
                                    Console.WriteLine("{0} Aquired", TreeData[NodeTracker].Name);//tells the user this node has been acquired
                                }
                                else//if the node has not been acquired
                                {
                                    Console.WriteLine("Cost: {0} Skillpoints", TreeData[NodeTracker].Cost);//prints the cost of the node
                                    Console.WriteLine("1. Purchase Current");//give the user an option to purchase it 
                                }
                                if (NodeTracker != 2)//checks if the current node is not 2 which is the last node in the path it cannot display the go to next node message
                                {
                                    Console.WriteLine("2. Go to next");//gives the user an option to go to the next node
                                }
                                Console.WriteLine("3. Go to previous");//gives the user an option to go to the previous node


                                NodeOption = Console.ReadLine();//takes a user input
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker != 2)//checks if the current node is not acquired and not equal to 2 which is the last node in the path
                                {
                                    if (NodeOption == "1" || NodeOption == "2" || NodeOption == "3")//checks the user input is within the accepted ranges
                                    {
                                        NodeInput = true;//breaks the user input loop
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker == 2)//checks if the current node is not acquired and equal to 2 which is the last node in the path
                                {
                                    if (NodeOption == "1" || NodeOption == "3")//checks the user input is within the accepted ranges
                                    {
                                        NodeInput = true;//breaks the user input loop
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker == 2)//checks if the current node is acquired and equal to 2 which is the last node in the path
                                {
                                    if (NodeOption == "3")//checks the user input is within the accepted ranges
                                    {
                                        NodeInput = true;//breaks the user input loop
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker != 2)//checks if the current node is acquired and not equal to 2 which is the last node in the path
                                {
                                    if (NodeOption == "3" || NodeOption == "2")//checks the user input is within the accepted ranges
                                    {
                                        NodeInput = true;//breaks the user input loop
                                    }
                                }
                            }
                            if (NodeTracker == 0)//if current node is 0
                            {
                                if (NodeOption == "1")//checks if option is equal to 1 
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)//checks if the user has enough skillpoints to purchase the node
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill point to purchase this node");//displays to notify the user that they do not have enough 
                                        Console.ReadLine();
                                    }
                                    else//if they have enough skillpoints to purchase the node 
                                    {
                                        Skillpoints -= TreeData[NodeTracker].Cost;//takes the skill points off the player
                                        TreeData[NodeTracker].Aquired = true;//makes the node acquired
                                        Attack += TreeData[NodeTracker].Attack;//increases the players attack
                                        Defense += TreeData[NodeTracker].Defense;//increases the players defense
                                        Speed += TreeData[NodeTracker].Speed;//increases the players speed
                                        MaxMANA += TreeData[NodeTracker].Mana;//increases the players MaxMana
                                        CurrentMANA += TreeData[NodeTracker].Mana;//increases the players CurrentMana
                                        MaxHP += TreeData[NodeTracker].Mana;//increases the players MaxHP
                                        CurrentHP += TreeData[NodeTracker].Mana;//increases the players CurrentHP
                                        SkillInv[0].SkillAttack += TreeData[NodeTracker].SkillAttack;////increases the players skill attack
                                    }
                                    NodeInput = false;//will loop back to the menu and allow the player to make another input
                                }
                                else if (NodeOption == "2")//checks if the players input was 2 
                                {
                                    NodeTracker = 1;//sets the users current node to 1 
                                    NodeInput = false;//will loop back to the menu and allow the player to make another input
                                }
                                else if (NodeOption == "3")//checks if the players input was 3
                                {
                                    NodeTracker = -1;//sets the users current node to -1 or home page
                                    NodeInput = true;//stops the player from inputing anymore to move them along the skill path as they are now moving back to the homepage
                                    input = false;//will loop back to home page and allow the player to make another input
                                }
                            }
                            else if (NodeTracker == 1)//if current node is 1
                            {
                                if (NodeOption == "1")//checks if option is equal to 1 
                                {

                                    if (Skillpoints < TreeData[NodeTracker].Cost)//checks if the user has enough skillpoints to purchase the node
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill point to purchase this node");//displays to notify the user that they do not have enough 
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)//checks to see if the previous node has been acquired 
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);//display warning message to user about purchasing a further node before the previous one 
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;//takes the skill points off the player
                                            TreeData[NodeTracker].Aquired = true;//makes the node acquired
                                            Attack += TreeData[NodeTracker].Attack;//increases the players attack
                                            Defense += TreeData[NodeTracker].Defense;//increases the players defense
                                            Speed += TreeData[NodeTracker].Speed;//increases the players speed
                                            MaxMANA += TreeData[NodeTracker].Mana;//increases the players MaxMana
                                            CurrentMANA += TreeData[NodeTracker].Mana;//increases the players CurrentMana
                                            MaxHP += TreeData[NodeTracker].Mana;//increases the players MaxHP
                                            CurrentHP += TreeData[NodeTracker].Mana;//increases the players CurrentHP
                                            SkillInv[0].SkillAttack += TreeData[NodeTracker].SkillAttack;////increases the players skill attack
                                        }
                                    }
                                    NodeInput = false;//will loop back to home page and allow the player to make another input
                                }
                                else if (NodeOption == "2")//checks if the player pick the 2nd option
                                {
                                    NodeTracker = 2;//sets tne current node to 2
                                    NodeInput = false;//will loop back to home page and allow the player to make another input
                                }
                                else if (NodeOption == "3")//checks if the player pick the 3rd option
                                {
                                    NodeTracker = 0;//sets the current node to 0
                                    NodeInput = false;//will loop back to home page and allow the player to make another input
                                }
                            }
                            else if (NodeTracker == 2)//checks if the players current node to see if they are on node 2 or last node on the path
                            {
                                if (NodeOption == "1")//checks if the player option is 1
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)//checks if the player has enough skillpoints to purchase the node
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill point to purchase this node");//notifies the user that they do not have enough skillpoints to purchase this node 
                                        Console.ReadLine();
                                    }
                                    else//if they have enough skillpoints to purchase the node 
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)//this checks to see if the previous ndoe has been purchased
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);//notifies the user that they have not purchased the previous node
                                            Console.ReadLine();
                                        }
                                        else//if the user has purchased the previous node
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;//takes the skill points off the player
                                            TreeData[NodeTracker].Aquired = true;//makes the node acquired
                                            Attack += TreeData[NodeTracker].Attack;//increases the players attack
                                            Defense += TreeData[NodeTracker].Defense;//increases the players defense
                                            Speed += TreeData[NodeTracker].Speed;//increases the players speed
                                            MaxMANA += TreeData[NodeTracker].Mana;//increases the players MaxMana
                                            CurrentMANA += TreeData[NodeTracker].Mana;//increases the players CurrentMana
                                            MaxHP += TreeData[NodeTracker].Mana;//increases the players MaxHP
                                            CurrentHP += TreeData[NodeTracker].Mana;//increases the players CurrentHP
                                            SkillInv[0].SkillAttack += TreeData[NodeTracker].SkillAttack;////increases the players skill attack

                                        }
                                    }
                                    NodeInput = false;//allows the user to make another choice in the menu
                                }
                                else if (NodeOption == "3")//check if user input was 3 
                                {
                                    NodeTracker = 1;//sets the players current node to 1 
                                    NodeInput = false;//allows the user to make another input 
                                }
                            }


                        }
                        break;
                    case "2":
                        NodeTracker = 3;
                        while (NodeTracker != -1)
                        {

                            while (NodeInput == false)
                            {
                                Console.Clear();
                                if (NodeTracker != 5)
                                {
                                    Console.WriteLine("Next Node: {0}", TreeData[TreeData[NodeTracker].NextNode].Name);
                                }
                                Console.WriteLine("Current Node: {0}", TreeData[NodeTracker].Name);
                                if (TreeData[NodeTracker].PreviousNode == -1)
                                {
                                    Console.WriteLine("Previous Node: Home Node");
                                }
                                else
                                {
                                    Console.WriteLine("Previous Node: {0}", TreeData[TreeData[NodeTracker].PreviousNode].Name);
                                }

                                if (TreeData[NodeTracker].Aquired == true)
                                {
                                    Console.WriteLine("{0} Aquired", TreeData[NodeTracker].Name);
                                }
                                else
                                {
                                    Console.WriteLine("Cost: {0} Skillpoints", TreeData[NodeTracker].Cost);
                                    Console.WriteLine("1. Purchase Current");
                                }
                                if (NodeTracker != 5)
                                {
                                    Console.WriteLine("2. Go to next");
                                }
                                Console.WriteLine("3. Go to previous");
                                NodeOption = Console.ReadLine();
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker != 5)
                                {
                                    if (NodeOption == "1" || NodeOption == "2" || NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker == 5)
                                {
                                    if (NodeOption == "1" || NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker == 5)
                                {
                                    if (NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker != 5)
                                {
                                    if (NodeOption == "3" || NodeOption == "2")
                                    {
                                        NodeInput = true;
                                    }
                                }
                            }
                            if (NodeTracker == 3)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill point to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Skillpoints -= TreeData[NodeTracker].Cost;
                                        TreeData[NodeTracker].Aquired = true;
                                        Attack += TreeData[NodeTracker].Attack;
                                        Defense += TreeData[NodeTracker].Defense;
                                        Speed += TreeData[NodeTracker].Speed;
                                        MaxMANA += TreeData[NodeTracker].Mana;
                                        CurrentMANA += TreeData[NodeTracker].Mana;
                                        MaxHP += TreeData[NodeTracker].Mana;
                                        CurrentHP += TreeData[NodeTracker].Mana;
                                        SkillInv[1].SkillAttack += TreeData[NodeTracker].SkillAttack;

                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "2")
                                {
                                    NodeTracker = 4;
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = -1;
                                    NodeInput = true;
                                    input = false;
                                }
                            }
                            else if (NodeTracker == 4)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill point to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;
                                            TreeData[NodeTracker].Aquired = true;
                                            Attack += TreeData[NodeTracker].Attack;
                                            Defense += TreeData[NodeTracker].Defense;
                                            Speed += TreeData[NodeTracker].Speed;
                                            MaxMANA += TreeData[NodeTracker].Mana;
                                            CurrentMANA += TreeData[NodeTracker].Mana;
                                            MaxHP += TreeData[NodeTracker].Mana;
                                            CurrentHP += TreeData[NodeTracker].Mana;
                                            SkillInv[1].SkillAttack += TreeData[NodeTracker].SkillAttack;

                                        }
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "2")
                                {
                                    NodeTracker = 5;
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = 3;
                                    NodeInput = false;
                                }
                            }
                            else if (NodeTracker == 5)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill point to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;
                                            TreeData[NodeTracker].Aquired = true;
                                            Attack += TreeData[NodeTracker].Attack;
                                            Defense += TreeData[NodeTracker].Defense;
                                            Speed += TreeData[NodeTracker].Speed;
                                            MaxMANA += TreeData[NodeTracker].Mana;
                                            CurrentMANA += TreeData[NodeTracker].Mana;
                                            MaxHP += TreeData[NodeTracker].Mana;
                                            CurrentHP += TreeData[NodeTracker].Mana;
                                            SkillInv[1].SkillAttack += TreeData[NodeTracker].SkillAttack;

                                        }
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = 4;
                                    NodeInput = false;
                                }
                            }


                        }
                        break;
                    case "3":
                        NodeTracker = 6;
                        while (NodeTracker != -1)
                        {

                            while (NodeInput == false)
                            {
                                Console.Clear();
                                if (NodeTracker != 8)
                                {
                                    Console.WriteLine("Next Node: {0}", TreeData[TreeData[NodeTracker].NextNode].Name);
                                }
                                Console.WriteLine("Current Node: {0}", TreeData[NodeTracker].Name);
                                if (TreeData[NodeTracker].PreviousNode == -1)
                                {
                                    Console.WriteLine("Previous Node: Home Node");
                                }
                                else
                                {
                                    Console.WriteLine("Previous Node: {0}", TreeData[TreeData[NodeTracker].PreviousNode].Name);
                                }

                                if (TreeData[NodeTracker].Aquired == true)
                                {
                                    Console.WriteLine("{0} Aquired", TreeData[NodeTracker].Name);
                                }
                                else
                                {
                                    Console.WriteLine("Cost: {0} Skillpoints", TreeData[NodeTracker].Cost);
                                    Console.WriteLine("1. Purchase Current");
                                }
                                if (NodeTracker != 8)
                                {
                                    Console.WriteLine("2. Go to next");
                                }
                                Console.WriteLine("3. Go to previous");
                                NodeOption = Console.ReadLine();
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker != 8)
                                {
                                    if (NodeOption == "1" || NodeOption == "2" || NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker == 8)
                                {
                                    if (NodeOption == "1" || NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker == 8)
                                {
                                    if (NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker != 8)
                                {
                                    if (NodeOption == "3" || NodeOption == "2")
                                    {
                                        NodeInput = true;
                                    }
                                }
                            }
                            if (NodeTracker == 6)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill point to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Skillpoints -= TreeData[NodeTracker].Cost;
                                        TreeData[NodeTracker].Aquired = true;
                                        Attack += TreeData[NodeTracker].Attack;
                                        Defense += TreeData[NodeTracker].Defense;
                                        Speed += TreeData[NodeTracker].Speed;
                                        MaxMANA += TreeData[NodeTracker].Mana;
                                        CurrentMANA += TreeData[NodeTracker].Mana;
                                        MaxHP += TreeData[NodeTracker].Mana;
                                        CurrentHP += TreeData[NodeTracker].Mana;
                                        SkillInv[2].SkillAttack += TreeData[NodeTracker].SkillAttack;
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "2")
                                {
                                    NodeTracker = 7;
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = -1;
                                    NodeInput = true;
                                    input = false;
                                }
                            }
                            else if (NodeTracker == 7)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill points to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;
                                            TreeData[NodeTracker].Aquired = true;
                                            Attack += TreeData[NodeTracker].Attack;
                                            Defense += TreeData[NodeTracker].Defense;
                                            Speed += TreeData[NodeTracker].Speed;
                                            MaxMANA += TreeData[NodeTracker].Mana;
                                            CurrentMANA += TreeData[NodeTracker].Mana;
                                            MaxHP += TreeData[NodeTracker].Mana;
                                            CurrentHP += TreeData[NodeTracker].Mana;
                                            SkillInv[2].SkillAttack += TreeData[NodeTracker].SkillAttack;

                                        }
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "2")
                                {
                                    NodeTracker = 8;
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = 6;
                                    NodeInput = false;
                                }
                            }
                            else if (NodeTracker == 8)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill points to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;
                                            TreeData[NodeTracker].Aquired = true;
                                            Attack += TreeData[NodeTracker].Attack;
                                            Defense += TreeData[NodeTracker].Defense;
                                            Speed += TreeData[NodeTracker].Speed;
                                            MaxMANA += TreeData[NodeTracker].Mana;
                                            CurrentMANA += TreeData[NodeTracker].Mana;
                                            MaxHP += TreeData[NodeTracker].Mana;
                                            CurrentHP += TreeData[NodeTracker].Mana;
                                            SkillInv[2].SkillAttack += TreeData[NodeTracker].SkillAttack;

                                        }
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = 7;
                                    NodeInput = false;
                                }
                            }


                        }
                        break;
                    case "4":
                        NodeTracker = 9;
                        while (NodeTracker != -1)
                        {

                            while (NodeInput == false)
                            {
                                Console.Clear();
                                if (NodeTracker != 11)
                                {
                                    Console.WriteLine("Next Node: {0}", TreeData[TreeData[NodeTracker].NextNode].Name);
                                }
                                Console.WriteLine("Current Node: {0}", TreeData[NodeTracker].Name);
                                if (TreeData[NodeTracker].PreviousNode == -1)
                                {
                                    Console.WriteLine("Previous Node: Home Node");
                                }
                                else
                                {
                                    Console.WriteLine("Previous Node: {0}", TreeData[TreeData[NodeTracker].PreviousNode].Name);
                                }

                                if (TreeData[NodeTracker].Aquired == true)
                                {
                                    Console.WriteLine("Node Aquired");
                                }
                                else
                                {
                                    Console.WriteLine("Cost: {0}p", TreeData[NodeTracker].Cost);
                                    Console.WriteLine("1. Purchase Current");
                                }
                                if (NodeTracker != 11)
                                {
                                    Console.WriteLine("2. Go to next");
                                }
                                Console.WriteLine("3. Go to previous");
                                NodeOption = Console.ReadLine();
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker != 11)
                                {
                                    if (NodeOption == "1" || NodeOption == "2" || NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == false & NodeTracker == 11)
                                {
                                    if (NodeOption == "1" || NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker == 11)
                                {
                                    if (NodeOption == "3")
                                    {
                                        NodeInput = true;
                                    }
                                }
                                if (TreeData[NodeTracker].Aquired == true & NodeTracker != 11)
                                {
                                    if (NodeOption == "3" || NodeOption == "2")
                                    {
                                        NodeInput = true;
                                    }
                                }
                            }
                            if (NodeTracker == 9)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill points to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Skillpoints -= TreeData[NodeTracker].Cost;
                                        TreeData[NodeTracker].Aquired = true;
                                        Attack += TreeData[NodeTracker].Attack;
                                        Defense += TreeData[NodeTracker].Defense;
                                        Speed += TreeData[NodeTracker].Speed;
                                        MaxMANA += TreeData[NodeTracker].Mana;
                                        CurrentMANA += TreeData[NodeTracker].Mana;
                                        MaxHP += TreeData[NodeTracker].Mana;
                                        CurrentHP += TreeData[NodeTracker].Mana;
                                        SkillInv[3].SkillAttack += TreeData[NodeTracker].SkillAttack;
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "2")
                                {
                                    NodeTracker = 10;
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = -1;
                                    NodeInput = true;
                                    input = false;
                                }
                            }
                            else if (NodeTracker == 10)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill points to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;
                                            TreeData[NodeTracker].Aquired = true;
                                            Attack += TreeData[NodeTracker].Attack;
                                            Defense += TreeData[NodeTracker].Defense;
                                            Speed += TreeData[NodeTracker].Speed;
                                            MaxMANA += TreeData[NodeTracker].Mana;
                                            CurrentMANA += TreeData[NodeTracker].Mana;
                                            MaxHP += TreeData[NodeTracker].Mana;
                                            CurrentHP += TreeData[NodeTracker].Mana;
                                            SkillInv[3].SkillAttack += TreeData[NodeTracker].SkillAttack;

                                        }
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "2")
                                {
                                    NodeTracker = 11;
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = 9;
                                    NodeInput = false;
                                }
                            }
                            else if (NodeTracker == 11)
                            {
                                if (NodeOption == "1")
                                {
                                    if (Skillpoints < TreeData[NodeTracker].Cost)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("You do not have enough skill points to purchase this node");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        if (TreeData[TreeData[NodeTracker].PreviousNode].Aquired == false)
                                        {
                                            Console.WriteLine("You need to Purchase {0} first before purchasing {1}", TreeData[TreeData[NodeTracker].PreviousNode].Name, TreeData[NodeTracker].Name);
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Skillpoints -= TreeData[NodeTracker].Cost;
                                            TreeData[NodeTracker].Aquired = true;
                                            Attack += TreeData[NodeTracker].Attack;
                                            Defense += TreeData[NodeTracker].Defense;
                                            Speed += TreeData[NodeTracker].Speed;
                                            MaxMANA += TreeData[NodeTracker].Mana;
                                            CurrentMANA += TreeData[NodeTracker].Mana;
                                            MaxHP += TreeData[NodeTracker].Mana;
                                            CurrentHP += TreeData[NodeTracker].Mana;
                                            SkillInv[3].SkillAttack += TreeData[NodeTracker].SkillAttack;
                                        }
                                    }
                                    NodeInput = false;
                                }
                                else if (NodeOption == "3")
                                {
                                    NodeTracker = 10;
                                    NodeInput = false;
                                }
                            }
                        }
                        break;
                    case "5":
                        Exit = true;
                        break;
                }

            }
        }
        public void Npc()// interaction with the npc object on the board which is used to acquire quests 
        {
            if (World.GetCurrentWorld() == 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Quest[0].Completed == true)
                    {
                        Console.Clear();
                        Console.WriteLine("This quest has already been completed please go and complete others");
                        Console.ReadLine();
                        break;
                    }
                    else if (Quest[0].BossDefeat == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Thanks for defeating the dungeon boss there is your reward");
                        Console.WriteLine("");
                        Console.WriteLine("+ 1000z");
                        Console.WriteLine("+ 3 Skillpoints");
                        Console.ReadLine();
                        Skillpoints += 3;
                        Money += 1000;
                        Quest[0].Completed = true;
                        break;
                    }
                    else if (Quest[0].Aquired == true)
                    {
                        Console.Clear();
                        Console.WriteLine("You have already unlocked this Quest if you want help read the description in the questlog");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        if (i >= 0)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[0].Name, QuestVessel[0].Dialogue1);
                        }
                        if (i >= 1)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[0].Name, QuestVessel[0].Dialogue2);
                        }
                        if (i >= 2)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[0].Name, QuestVessel[0].Dialogue3);
                        }
                        Console.WriteLine("Press enter to get the next line of dialogue");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("You have unlocked {0]", Quest[0].Name);
                        Quest[1].Aquired = true;
                        Console.ReadLine();
                    }
                }
            }
            else if (World.GetCurrentWorld() == 6)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Quest[1].Completed == true)
                    {
                        Console.Clear();
                        Console.WriteLine("This quest has already been completed please go and complete others");
                        Console.ReadLine();
                        break;
                    }
                    else if (Quest[1].BossDefeat == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Thanks for defeating the dungeon boss there is your reward");
                        Console.WriteLine("");
                        Console.WriteLine("+ 1000z");
                        Console.WriteLine("+ 3 Skillpoints");
                        Console.ReadLine();
                        Skillpoints += 3;
                        Money += 1000;
                        Quest[1].Completed = true;
                        break;
                    }
                    else if (Quest[1].Aquired == true)
                    {
                        Console.Clear();
                        Console.WriteLine("You have already unlocked this Quest if you want help read the description in the questlog");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        if (i >= 0)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[1].Name, QuestVessel[1].Dialogue1);
                        }
                        if (i >= 1)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[1].Name, QuestVessel[1].Dialogue2);
                        }
                        if (i >= 2)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[1].Name, QuestVessel[1].Dialogue3);
                            Quest[1].Aquired = true;
                        }
                        Console.WriteLine("Press enter to get the next line of dialogue");
                        Console.ReadLine();
                        if (Quest[1].Aquired == true)
                        {
                            Console.Clear();
                            Console.WriteLine("You have unlocked {0}", Quest[1].Name);
                            Console.ReadLine();
                        }
                    }
                }
            }
            else if (World.GetCurrentWorld() == 10)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Quest[2].Completed == true)
                    {
                        Console.Clear();
                        Console.WriteLine("This quest has already been completed please go and complete others");
                        Console.ReadLine();
                        break;
                    }
                    else if (Quest[2].BossDefeat == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Thanks for defeating the dungeon boss there is your reward");
                        Console.WriteLine("");
                        Console.WriteLine("+ 1000z");
                        Console.WriteLine("+ 3 Skillpoints");
                        Console.ReadLine();
                        Skillpoints += 3;
                        Money += 1000;
                        Quest[2].Completed = true;
                        break;
                    }
                    else if (Quest[2].Aquired == true)
                    {
                        Console.Clear();
                        Console.WriteLine("You have already unlocked this Quest if you want help read the description in the questlog");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        if (i >= 0)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[2].Name, QuestVessel[2].Dialogue1);
                        }
                        if (i >= 1)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[2].Name, QuestVessel[2].Dialogue2);
                        }
                        if (i >= 2)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[2].Name, QuestVessel[2].Dialogue3);
                            Quest[2].Aquired = true;
                        }
                        Console.WriteLine("Press enter to get the next line of dialogue");
                        Console.ReadLine();
                        if (Quest[2].Aquired == true)
                        {
                            Console.Clear();
                            Console.WriteLine("You have unlocked {0}", Quest[2].Name);
                            Console.ReadLine();
                        }
                    }
                }
            }
            else if (World.GetCurrentWorld() == 14)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Quest[3].Completed == true)
                    {
                        Console.Clear();
                        Console.WriteLine("This quest has already been completed please go and complete others");
                        Console.ReadLine();
                        break;
                    }
                    else if (Quest[3].BossDefeat == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Thanks for defeating the dungeon boss there is your reward");
                        Console.WriteLine("");
                        Console.WriteLine("+ 1000z");
                        Console.WriteLine("+ 3 Skillpoints");
                        Console.ReadLine();
                        Skillpoints += 3;
                        Money += 1000;
                        Quest[3].Completed = true;
                        break;
                    }
                    else if (Quest[3].Aquired == true)
                    {
                        Console.Clear();
                        Console.WriteLine("You have already unlocked this Quest if you want help read the description in the questlog");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        if (i >= 0)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[3].Name, QuestVessel[3].Dialogue1);
                        }
                        if (i >= 1)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[3].Name, QuestVessel[3].Dialogue2);
                        }
                        if (i >= 2)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[3].Name, QuestVessel[3].Dialogue3);
                            Quest[3].Aquired = true;
                        }
                        Console.WriteLine("Press enter to get the next line of dialogue");
                        Console.ReadLine();
                        if (Quest[3].Aquired == true)
                        {
                            Console.Clear();
                            Console.WriteLine("You have unlocked {0}", Quest[3].Name);
                            Console.ReadLine();
                        }
                    }
                }
            }
            else if (World.GetCurrentWorld() == 18)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Quest[4].Completed == true)
                    {
                        Console.Clear();
                        Console.WriteLine("This quest has already been completed please go and complete others");
                        Console.ReadLine();
                        break;
                    }
                    else if (Quest[4].BossDefeat == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Thanks for defeating the dungeon boss there is your reward");
                        Console.WriteLine("");
                        Console.WriteLine("+ 1000z");
                        Console.WriteLine("+ 3 Skillpoints");
                        Console.ReadLine();
                        Skillpoints += 3;
                        Money += 1000;
                        Quest[4].Completed = true;

                        break;
                    }
                    else if (Quest[4].Aquired == true)
                    {
                        Console.Clear();
                        Console.WriteLine("You have already unlocked this Quest if you want help read the description in the questlog");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        if (i >= 0)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[4].Name, QuestVessel[4].Dialogue1);
                        }
                        if (i >= 1)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[4].Name, QuestVessel[4].Dialogue2);
                        }
                        if (i >= 2)
                        {
                            Console.WriteLine("{0}: {1}", QuestVessel[4].Name, QuestVessel[4].Dialogue3);
                            Quest[4].Aquired = true;
                        }
                        Console.WriteLine("Press enter to get the next line of dialogue");
                        Console.ReadLine();
                        if (Quest[4].Aquired == true)
                        {
                            Console.Clear();
                            Console.WriteLine("You have unlocked {0}", Quest[4].Name);
                            Console.ReadLine();
                        }
                    }
                }
            }
        }
        public void Questlog()// lets the user see a visual representatuon of there ongoing and completed quests
        {
            Console.Clear();
            if (Quest[0].Aquired == true)
            {
                Console.WriteLine("{0}", Quest[0].Name);
                Console.WriteLine("");
                Console.WriteLine("{0}", Quest[0].Description);
                Console.WriteLine("");
                Console.WriteLine("Zenny: {0}z", QuestVessel[0].RewardMoney);
                Console.WriteLine("Skillpoints: {0}p", QuestVessel[0].RewardSkillpoint);
                Console.WriteLine("");
                Console.WriteLine("World: Tutorial World");
            }
            if (Quest[0].Completed == true)
            {
                Console.WriteLine("QUEST COMPLETE");
            }

            if (Quest[1].Aquired == true)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("{0}", Quest[1].Name);
                Console.WriteLine("");
                Console.WriteLine("{0}", Quest[1].Description);
                Console.WriteLine("");
                Console.WriteLine("Zenny: {0}z", QuestVessel[1].RewardMoney);
                Console.WriteLine("Skillpoints: {0}p", QuestVessel[1].RewardSkillpoint);
                Console.WriteLine("");
                Console.WriteLine("World: World1");
            }
            if (Quest[1].Completed == true)
            {
                Console.WriteLine("QUEST COMPLETE");
            }

            if (Quest[2].Aquired == true)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("{0}", Quest[2].Name);
                Console.WriteLine("");
                Console.WriteLine("{0}", Quest[2].Description);
                Console.WriteLine("");
                Console.WriteLine("Zenny: {0}z", QuestVessel[2].RewardMoney);
                Console.WriteLine("Skillpoints: {0}p", QuestVessel[2].RewardSkillpoint);
                Console.WriteLine("");
                Console.WriteLine("World: World2");
            }
            if (Quest[2].Completed == true)
            {
                Console.WriteLine("QUEST COMPLETE");
            }

            if (Quest[3].Aquired == true)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("{0}", Quest[3].Name);
                Console.WriteLine("");
                Console.WriteLine("{0}", Quest[3].Description);
                Console.WriteLine("");
                Console.WriteLine("Zenny: {0}z", QuestVessel[3].RewardMoney);
                Console.WriteLine("Skillpoints: {0}p", QuestVessel[3].RewardSkillpoint);
                Console.WriteLine("");
                Console.WriteLine("World: World3");
            }
            if (Quest[3].Completed == true)
            {
                Console.WriteLine("QUEST COMPLETE");
            }

            if (Quest[4].Aquired == true)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("{0}", Quest[4].Name);
                Console.WriteLine("");
                Console.WriteLine("{0}", Quest[4].Description);
                Console.WriteLine("");
                Console.WriteLine("Zenny: {0}z", QuestVessel[4].RewardMoney);
                Console.WriteLine("Skillpoints: {0}p", QuestVessel[4].RewardSkillpoint);
                Console.WriteLine("");
                Console.WriteLine("World: World4");

            }
            if (Quest[4].Completed == true)
            {
                Console.WriteLine("QUEST COMPLETE");
            }
            if (Quest[0].Aquired == false & Quest[1].Aquired == false & Quest[2].Aquired == false & Quest[3].Aquired == false & Quest[4].Aquired == false)
            {
                Console.WriteLine("You have Aquired No Quests");
            }
            Console.ReadLine();
        }
        public void Item()// allows the player to use their items like health/mana potions
        {
            string option = "";
            bool input = false;
            while (option != "3")
            {
                while (input == false)
                {
                    Console.Clear();
                    Console.WriteLine("Please choose an Item to use");
                    Console.WriteLine("1. {0}", ItemInv[0].Name);
                    Console.WriteLine("Amount: {0}", ItemInv[0].Amount);
                    Console.WriteLine("");
                    Console.WriteLine("2. {0}", ItemInv[1].Name);
                    Console.WriteLine("Amount: {0}", ItemInv[1].Amount);
                    Console.WriteLine("");
                    Console.WriteLine("3. Back");
                    option = Console.ReadLine();
                    if (option == "1" || option == "2" || option == "3")
                    {
                        input = true;
                    }
                }
                switch (option)
                {
                    case "1":
                        if (ItemInv[0].Amount < 1)
                        {
                            Console.WriteLine("You do not have enough of this item");
                            Console.ReadLine();
                            input = false;

                        }
                        else if (CurrentHP + ItemInv[0].Health>=MaxHP)
                        {
                            Console.WriteLine("You cant heal when on full health");
                            Console.ReadLine();
                            input = false;
                        }
                        else
                        {
                            CurrentHP += ItemInv[0].Health;
                            if (CurrentHP > MaxHP)
                            {
                                CurrentHP = MaxHP;
                            }
                            Console.WriteLine("You have healed {0} health", ItemInv[0].Health);
                            Console.ReadLine();
                            ItemInv[0].Amount--;
                            input = false;
                        }
                        break;
                    case "2":
                        if (ItemInv[1].Amount < 1)
                        {
                            Console.WriteLine("You do not have enough of this item");
                            Console.ReadLine();
                            input = false;
                        }
                        else if (CurrentMANA + ItemInv[0].Mana >= MaxMANA)
                        {
                            Console.WriteLine("You cant gain mana on full mana");
                            Console.ReadLine();
                            input = false;
                        }
                        else
                        {
                            CurrentMANA += ItemInv[1].Mana;
                            if (CurrentMANA > MaxMANA)
                            {
                                CurrentMANA = MaxMANA;
                            }
                            Console.WriteLine("You have gained {0} mana", ItemInv[1].Mana);
                            Console.ReadLine();
                            ItemInv[1].Amount--;
                            input = false;
                        }
                        break;
                }
            }
        }
        public void Merchant()// triggers the object interaction with the merchants where you can purchase potions and restore health and mana
        {
            string option = "";
            bool input = false;
            while (option != "5")
            {
                while (input == false)
                {
                    Console.Clear();
                    Console.WriteLine("What Would you like to purchase");
                    Console.WriteLine("1. {0}", ItemInv[0].Name);
                    Console.WriteLine("Cost: 30z");
                    Console.WriteLine("");
                    Console.WriteLine("2. {0}", ItemInv[1].Name);
                    Console.WriteLine("Cost: 30z");
                    Console.WriteLine("");
                    Console.WriteLine("3. {0}", ItemInv[2].Name);
                    Console.WriteLine("Cost: 30z");
                    Console.WriteLine("");
                    Console.WriteLine("4. Buy a bed and rest the night");
                    Console.WriteLine("Cost: 100z");
                    Console.WriteLine("");
                    Console.WriteLine("5. Back");
                    option = Console.ReadLine();
                    if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5")
                    {
                        input = true;
                    }
                }
                switch (option)
                {
                    case "1":
                        if (Money < 30)
                        {
                            Console.WriteLine("You do not have enough money to purchase this item");
                            Console.ReadLine();
                            input = false;

                        }
                        else
                        {

                            Console.WriteLine("You have Purchased 1 {0}", ItemInv[0].Name);
                            Console.ReadLine();
                            Money -= 30;
                            ItemInv[0].Amount++;
                            input = false;
                        }
                        break;
                    case "2":
                        if (Money < 30)
                        {
                            Console.WriteLine("You do not have enough money to purchase this item");
                            Console.ReadLine();
                            input = false;

                        }
                        else
                        {

                            Console.WriteLine("You have Purchased 1 {0}", ItemInv[1].Name);
                            Console.ReadLine();
                            Money -= 30;
                            ItemInv[1].Amount++;
                            input = false;
                        }
                        break;
                    case "3":
                        if (Money < 30)
                        {
                            Console.WriteLine("You do not have enough money to purchase this item");
                            Console.ReadLine();
                            input = false;

                        }
                        else
                        {

                            Console.WriteLine("You have Purchased 1 {0}", ItemInv[2].Name);
                            Console.ReadLine();
                            Money -= 30;
                            ItemInv[2].Amount++;
                            input = false;
                        }
                        break;
                    case "4":
                        if (Money < 30)
                        {
                            Console.WriteLine("You do not have enough money to purchase this item");
                            Console.ReadLine();
                            input = false;

                        }
                        else
                        {

                            Console.WriteLine("You have rested the night at the inn and restored HP and Mana");
                            Console.ReadLine();
                            Money -= 100;
                            CurrentHP = MaxHP;
                            CurrentMANA = MaxMANA;
                            input = false;
                            option = "5";
                        }
                        break;
                }
            }
        }
        public void DisplayStats()// displays the players current stats 
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Name:        {0}", Name);
            Console.WriteLine("Class:       {0}", Role);
            Console.WriteLine("HP:          {0}/{1}", CurrentHP, MaxHP);
            Console.WriteLine("MANA:        {0}/{1}", CurrentMANA, MaxMANA);
            Console.WriteLine("Attack:      {0}", Attack);
            Console.WriteLine("Defence:     {0}", Defense);
            Console.WriteLine("Speed:       {0}", Speed);
            Console.WriteLine("Money:       {0}", Money);
            Console.WriteLine("Skillpoints: {0}", Skillpoints);
            Console.WriteLine("Please press enter to proceed");
            Console.ReadLine();
        }
        public void CreateQuestlog()//This creates a quest log for the player
        {
            Quest[0].QuestID = 1;
            Quest[0].Name = "Quest1";
            Quest[0].Description = "This is Quest1";
            Quest[0].Completed = false;
            Quest[0].BossDefeat = false;
            Quest[0].Aquired = false;
            Quest[0].QuestID = 1;
            Quest[1].Name = "Pest Problem";
            Quest[1].Description = "A Strange Drunkard was tell you about a rat infestation and a possible\n dungeon being reocupited. Its possible that the two are connected so look around";
            Quest[1].Completed = false;
            Quest[1].BossDefeat = false;
            Quest[1].Aquired = false;
            Quest[2].QuestID = 1;
            Quest[2].Name = "Quest3";
            Quest[2].Description = "This is Quest3";
            Quest[2].Completed = false;
            Quest[2].BossDefeat = false;
            Quest[2].Aquired = false;
            Quest[3].QuestID = 1;
            Quest[3].Name = "Quest4";
            Quest[3].Description = "This is Quest4";
            Quest[3].Completed = false;
            Quest[3].BossDefeat = false;
            Quest[3].Aquired = false;
            Quest[4].QuestID = 1;
            Quest[4].Name = "Quest5";
            Quest[4].Description = "This is Quest5";
            Quest[4].Completed = false;
            Quest[4].BossDefeat = false;
            Quest[4].Aquired = false;

        }
        public void CreateItemInv()//this creates an item inventory for the player
        {
            ItemInv[0].ItemID = 1;
            ItemInv[0].Name = "Health Potion";
            ItemInv[0].Health = 50;
            ItemInv[0].Mana = 0;
            ItemInv[0].Amount = 0;
            ItemInv[1].ItemID = 1;
            ItemInv[1].Name = "Mana Potion";
            ItemInv[1].Health = 0;
            ItemInv[1].Mana = 50;
            ItemInv[1].Amount = 4;
            ItemInv[2].ItemID = 2;
            ItemInv[2].Name = "Revive Crystal";
            ItemInv[2].Amount = 0;
        }
        public void CreateSkillInv()//this dynamicly creates a skill inv for the player depending on their role
        {
            if (Role == "Mage")
            {
                SkillInv[0].SkillID = 1;
                SkillInv[0].Name = "Hells Fire";
                SkillInv[0].Description = "Just incase the enemy isnt dead!";
                SkillInv[0].SkillAttack = 80;
                SkillInv[0].Resource = 10;

                SkillInv[1].SkillID = 1;
                SkillInv[1].Name = "Ice Age";
                SkillInv[1].Description = "Hie Hie no Mi";
                SkillInv[1].SkillAttack = 120;
                SkillInv[1].Resource = 25;

                SkillInv[2].SkillID = 1;
                SkillInv[2].Name = "Maximum Inferno";
                SkillInv[2].Description = "Just to be extra cautious";
                SkillInv[2].SkillAttack = 200;
                SkillInv[2].Resource = 50;

                SkillInv[3].SkillID = 1;
                SkillInv[3].Name = "EXPLOSION!!!";
                SkillInv[3].Description = "ECUUSSSPLOSSSSION!!!!!";
                SkillInv[3].SkillAttack = 100000;
                SkillInv[3].Resource = 100;

            }
            else if (Role == "Warrior")
            {
                SkillInv[0].SkillID = 1;
                SkillInv[0].Name = "Side Slash";
                SkillInv[0].Description = "Fast slash that comes from the side";
                SkillInv[0].SkillAttack = 80;
                SkillInv[0].Resource = 10;

                SkillInv[1].SkillID = 1;
                SkillInv[1].Name = "German Suplex!";
                SkillInv[1].Description = "....3, 2, 1 OUT";
                SkillInv[1].SkillAttack = 120;
                SkillInv[1].Resource = 25;

                SkillInv[2].SkillID = 1;
                SkillInv[2].Name = "Thunder Cross Split Attack";
                SkillInv[2].Description = "The Ultimate form of attack and defense";
                SkillInv[2].SkillAttack = 200;
                SkillInv[2].Resource = 50;

                SkillInv[3].SkillID = 1;
                SkillInv[3].Name = "Serious Series: Serious Punch!";
                SkillInv[3].Description = "Very Serious";
                SkillInv[3].SkillAttack = 100000;
                SkillInv[3].Resource = 100;

            }
            else if (Role == "Archer")
            {
                SkillInv[0].SkillID = 1;
                SkillInv[0].Name = "Twin Arrows";
                SkillInv[0].Description = "Fire two arrows at once";
                SkillInv[0].SkillAttack = 80;
                SkillInv[0].Resource = 10;

                SkillInv[1].SkillID = 1;
                SkillInv[1].Name = "Invasion";
                SkillInv[1].Description = "Piercing arrow";
                SkillInv[1].SkillAttack = 120;
                SkillInv[1].Resource = 25;

                SkillInv[2].SkillID = 1;
                SkillInv[2].Name = "Emerald Splash!";
                SkillInv[2].Description = "No one can deflect the emerald splash";
                SkillInv[2].SkillAttack = 200;
                SkillInv[2].Resource = 50;

                SkillInv[3].SkillID = 1;
                SkillInv[3].Name = "1000 dragons";
                SkillInv[3].Description = "Unstopable arrow power";
                SkillInv[3].SkillAttack = 100000;
                SkillInv[3].Resource = 100;

            }
        }
        public void CreateSkillTree()//this creates data for the Skill tree method. this allows it to be dynamic to the users location using a tracker
        {
            TreeData[0].NodeID = 1;
            TreeData[0].Name = SkillInv[0].Name;
            TreeData[0].NextNode = 1;
            TreeData[0].CurrentNode = 0;
            TreeData[0].PreviousNode = -1;
            TreeData[0].Attack = 0;
            TreeData[0].Defense = 0;
            TreeData[0].Mana = 0;
            TreeData[0].Speed = 0;
            TreeData[0].SkillAttack = 0;
            TreeData[0].Health = 0;
            TreeData[0].Aquired = false;
            TreeData[0].Cost = 1;

            TreeData[1].NodeID = 2;
            TreeData[1].Name = "Health and Attack";
            TreeData[1].NextNode = 2;
            TreeData[1].CurrentNode = 1;
            TreeData[1].PreviousNode = 0;
            TreeData[1].Attack = 40;
            TreeData[1].Defense = 0;
            TreeData[1].Mana = 0;
            TreeData[1].Speed = 0;
            TreeData[1].SkillAttack = 0;
            TreeData[1].Health = 100;
            TreeData[1].Aquired = false;
            TreeData[1].Cost = 1;
            TreeData[2].NodeID = 3;
            TreeData[2].Name = "Skill attack and mana";

            TreeData[2].CurrentNode = 2;
            TreeData[2].PreviousNode = 1;
            TreeData[2].Attack = 0;
            TreeData[2].Defense = 0;
            TreeData[2].Mana = 25;
            TreeData[2].Speed = 0;
            TreeData[2].SkillAttack = 100;
            TreeData[2].Health = 0;
            TreeData[2].Aquired = false;
            TreeData[2].Cost = 1;
            TreeData[3].NodeID = 4;
            TreeData[3].Name = SkillInv[1].Name;
            TreeData[3].NextNode = 4;
            TreeData[3].CurrentNode = 3;
            TreeData[3].PreviousNode = -1;
            TreeData[3].Attack = 0;
            TreeData[3].Defense = 0;
            TreeData[3].Mana = 0;
            TreeData[3].Speed = 0;
            TreeData[3].SkillAttack = 0;
            TreeData[3].Health = 0;
            TreeData[3].Aquired = false;
            TreeData[3].Cost = 1;
            TreeData[4].NodeID = 5;
            TreeData[4].Name = "Defense and Speed";
            TreeData[4].NextNode = 5;
            TreeData[4].CurrentNode = 4;
            TreeData[4].PreviousNode = 3;
            TreeData[4].Attack = 0;
            TreeData[4].Defense = 50;
            TreeData[4].Mana = 0;
            TreeData[4].Speed = 30;
            TreeData[4].SkillAttack = 0;
            TreeData[4].Health = 0;
            TreeData[4].Aquired = false;
            TreeData[4].Cost = 1;
            TreeData[5].NodeID = 6;
            TreeData[5].Name = "Skill attack and Mana";

            TreeData[5].CurrentNode = 5;
            TreeData[5].PreviousNode = 4;
            TreeData[5].Attack = 0;
            TreeData[5].Defense = 0;
            TreeData[5].Mana = 0;
            TreeData[5].Speed = 0;
            TreeData[5].SkillAttack = 100;
            TreeData[5].Health = 0;
            TreeData[5].Aquired = false;
            TreeData[5].Cost = 1;
            TreeData[6].NodeID = 7;
            TreeData[6].Name = SkillInv[2].Name;
            TreeData[6].NextNode = 7;
            TreeData[6].CurrentNode = 6;
            TreeData[6].PreviousNode = -1;
            TreeData[6].Attack = 0;
            TreeData[6].Defense = 0;
            TreeData[6].Mana = 0;
            TreeData[6].Speed = 0;
            TreeData[6].SkillAttack = 0;
            TreeData[6].Health = 0;
            TreeData[6].Aquired = false;
            TreeData[6].Cost = 1;
            TreeData[7].NodeID = 8;
            TreeData[7].Name = "Heath and speed";
            TreeData[7].NextNode = 8;
            TreeData[7].CurrentNode = 7;
            TreeData[7].PreviousNode = 6;
            TreeData[7].Attack = 0;
            TreeData[7].Defense = 0;
            TreeData[7].Mana = 0;
            TreeData[7].Speed = 30;
            TreeData[7].SkillAttack = 0;
            TreeData[7].Health = 100;
            TreeData[7].Aquired = false;
            TreeData[7].Cost = 1;
            TreeData[8].NodeID = 9;
            TreeData[8].Name = "Skill attack and mana";

            TreeData[8].CurrentNode = 8;
            TreeData[8].PreviousNode = 7;
            TreeData[8].Attack = 0;
            TreeData[8].Defense = 0;
            TreeData[8].Mana = 25;
            TreeData[8].Speed = 0;
            TreeData[8].SkillAttack = 100;
            TreeData[8].Health = 0;
            TreeData[8].Aquired = false;
            TreeData[8].Cost = 1;
            TreeData[9].NodeID = 10;
            TreeData[9].Name = SkillInv[3].Name;
            TreeData[9].NextNode = 10;
            TreeData[9].CurrentNode = 9;
            TreeData[9].PreviousNode = -1;
            TreeData[9].Attack = 0;
            TreeData[9].Defense = 0;
            TreeData[9].Mana = 0;
            TreeData[9].Speed = 0;
            TreeData[9].SkillAttack = 0;
            TreeData[9].Health = 0;
            TreeData[9].Aquired = false;
            TreeData[9].Cost = 1;
            TreeData[10].NodeID = 11;
            TreeData[10].Name = "Defense and attack";
            TreeData[10].NextNode = 11;
            TreeData[10].CurrentNode = 10;
            TreeData[10].PreviousNode = 9;
            TreeData[10].Attack = 50;
            TreeData[10].Defense = 40;
            TreeData[10].Mana = 0;
            TreeData[10].Speed = 0;
            TreeData[10].SkillAttack = 0;
            TreeData[10].Health = 0;
            TreeData[10].Aquired = false;
            TreeData[10].Cost = 1;
            TreeData[11].NodeID = 12;
            TreeData[11].Name = "Skill attack and speed";

            TreeData[11].CurrentNode = 11;
            TreeData[11].PreviousNode = 10;
            TreeData[11].Attack = 0;
            TreeData[11].Defense = 0;
            TreeData[11].Mana = 0;
            TreeData[11].Speed = 50;
            TreeData[11].SkillAttack = 1;
            TreeData[11].Health = 0;
            TreeData[11].Aquired = false;
            TreeData[11].Cost = 1;
        }
        public void CreateNPCStats()//this sets the stats for npcs
        {
            QuestVessel[0].Name = "Boris The Drunkard";
            QuestVessel[0].Dialogue1 = "Barrrgh... theres been some complaints around the town about r...rr..rats";
            QuestVessel[0].Dialogue2 = "I've also heard r....rumours that something strong has moved into the \n has moved into the dungeon up north maybe the two are connected";
            QuestVessel[0].Dialogue3 = "Anyway if you could sort out the rat problem i'll reward you!";
            QuestVessel[0].RewardMoney = 100;
            QuestVessel[0].RewardSkillpoint = 2;
            QuestVessel[1].Name = "Boris The Drunkard";
            QuestVessel[1].Dialogue1 = "Barrrgh... theres been some complaints around the town about r...rr..rats";
            QuestVessel[1].Dialogue2 = "I've also heard r....rumours that something strong has moved into the \n has moved into the dungeon up north maybe the two are connected";
            QuestVessel[1].Dialogue3 = "Anyway if you could sort out the rat problem i'll reward you!";
            QuestVessel[1].RewardMoney = 1000;
            QuestVessel[1].RewardSkillpoint = 3;
            QuestVessel[2].Name = "Npc3";
            QuestVessel[2].Dialogue1 = "This is Dialogue1";
            QuestVessel[2].Dialogue2 = "This is Dialogue2";
            QuestVessel[2].Dialogue3 = "This is Dialogue3";
            QuestVessel[2].RewardMoney = 100;
            QuestVessel[2].RewardSkillpoint = 100;
            QuestVessel[3].Name = "Npc4";
            QuestVessel[3].Dialogue1 = "This is Dialogue1";
            QuestVessel[3].Dialogue2 = "This is Dialogue2";
            QuestVessel[3].Dialogue3 = "This is Dialogue3";
            QuestVessel[3].RewardMoney = 100;
            QuestVessel[3].RewardSkillpoint = 100;
            QuestVessel[4].Name = "Npc5";
            QuestVessel[4].Dialogue1 = "This is Dialogue1";
            QuestVessel[4].Dialogue2 = "This is Dialogue2";
            QuestVessel[4].Dialogue3 = "This is Dialogue3";
            QuestVessel[4].RewardMoney = 100;
            QuestVessel[4].RewardSkillpoint = 100;
        }
        public void SetCurrentHP(int value)//Sets the value currentHP
        {
            CurrentHP = value;
        }
        public void SetMoney(int value)//Sets the value Money
        {
            Money = value;
        }
        public void SetAttack(int value)//Sets the value Attack
        {
            Attack = value;
        }
        public void SetDefense(int value)//Sets the value Defense
        {
            Defense = value;
        }
        public void SetName(string value)//Sets the value Name
        {
            Name = value;
        }
        public void SetMaxHP(int value)//Sets the value MaxHP
        {
            MaxHP = value;
        }
        public void SetSpeed(int value)//sets the value of speed
        {
            Speed = value;
        }
        public int GetPlayerID()//returns the value of playerid
        {
            return PlayerID;
        }
        public void SetPlayerID(int value)//sets the value of playerid
        {
            PlayerID = value;
        }
        public int GetMaxMana()//returns the value of max mana
        {
            return MaxMANA;
        }
        public void SetMaxMana(int value)//sets the value of max mana
        {
            MaxMANA = value;
        }
        public int GetCurrentMana()//returns the value of current mana
        {
            return CurrentMANA;
        }
        public void SetCurrentMana(int value)//sets the value of current mana
        {
            CurrentMANA = value;
        }
        public string GetRole()//returns the value of role
        {
            return Role;
        }
        public void SetRole(string value)//sets the value of role 
        {
            Role = value;
        }
        public int GetSkillpoints()//returns the value of skillpoints
        {
            return Skillpoints;
        }
        public void SetSkillpoints(int value)//sets the value of skillpoints
        {
            Skillpoints = value;
        }
        public bool GetQuestComplete(int index)//passes the index as a parameter and returns the value of quest complete
        {
            return Quest[index].Completed;
        }
        public void SetQuestCompleted(bool value, int index)//passes the index as a parameter and sets the value of quest complete
        {
            Quest[index].Completed = value;
        }
        public bool GetQuestAquired(int index)//passes the index as a parameter and returns the value of quest acquired
        {
            return Quest[index].Aquired;
        }
        public void SetQuestAcquired(bool value,int index)//passes the index as a parameter and sets the value of quest acquired
        {
            Quest[index].Aquired = value;
        }
        public bool GetQuestBossDefeat(int index)//passes the index as a parameter and returns the value of quest boss defeat
        {
            return Quest[index].BossDefeat;
        }
        public void SetQuestBossDefeat(bool value,int index)//passes the index as a parameter and sets the value of quest boss defeat
        {
            Quest[index].BossDefeat = value;
        }
        public void SetQuestName(string value, int index)//passes the index as a parameter and sets the value of quest name
        {
            Quest[index].Name = value;
        }
        public void SetQuestDesc(string value, int index)//passes the index as a parameter and sets the value of quest description
        {
            Quest[index].Description = value;
        }
        public string GetSkillName(int index)//passes the index as a parameter and returns the value of skill name
        {
            return SkillInv[index].Name;
        }
        public int GetSkillResource(int index)//passes the index as a parameter and returns the value of skill resource
        {
            return SkillInv[index].Resource;
        }
        public int GetSkillAttack(int index)//passes the index as a parameter and returns the value of skill attack 
        {
            return SkillInv[index].SkillAttack;
        }
        public string GetSkillDesc(int index)//passes the index as a parameter and returns the value of skill description
        {
            return SkillInv[index].Description;
        }
        public bool GetNodeAcquired(int index)//passes the index as a parameter and returns the value of node acquired
        {
            return TreeData[index].Aquired;
        }
        public string GetItemName(int index)//passes the index as a parameter and returns the value of item name
        {
            return ItemInv[index].Name;
        }
        public int GetItemAmount(int index)//passes the index as a parameter and returns the value of item amount
        {
            return ItemInv[index].Amount;
        }
        public int GetMana()//return the mana gained from the mana potion
        {
            return ItemInv [1].Mana;
        }
        public int GetHP()//returns the hp gained from the health potion
        {
            return ItemInv [0].Health;
        }
        public void SetAmount(int value,int index)//passes the index as a parameter and sets the value of item amount
        {
            ItemInv[index].Amount = value;
        }
        public void SetNodeAcquired(bool value, int index)//passes the index as a parameter and sets the value of node acquired
        {
            TreeData[index].Aquired = value;
        }
        public struct Skills //holds the data for each of the players skills
        {
            public int SkillID;
            public string Name;
            public string Description;
            public int Resource;
            public int SkillAttack;
        }
        public struct SkillsTree //this holds the data for the skill tree and helps to keep track of skill tree progression and traversal
        {
            public int NodeID;
            public int CurrentNode;
            public string Name;
            public int PreviousNode;
            public int NextNode;
            public int Mana;
            public int Attack;
            public int Health;
            public int Defense;
            public int Speed;
            public int SkillAttack;
            public int Cost;
            public bool Aquired;
        }
        public struct Quests //this stores data about the players quest progression
        {
            public int QuestID;
            public string Name;
            public string Description;
            public bool Completed;
            public bool BossDefeat;
            public bool Aquired;
        }
        public struct Items //this stores data about the players inventory
        {
            public int ItemID;
            public string Name;
            public int Health;
            public int Mana;
            public int Amount;
        }
        public struct NPC //holds data about the npc character like dialogues name and rewards they give you for quests
        {
            public string Name;
            public string Dialogue1;
            public string Dialogue2;

            public string Dialogue3;
            public int RewardMoney;
            public int RewardSkillpoint;

        }
    }
}
