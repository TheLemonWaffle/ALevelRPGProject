using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelRPGProject
{
    class Combat
    {
        protected bool Enemyturn;//keeps track of the enemy turn
        protected bool Playerturn;//keeps track of the player turn 
        protected int PlayerHPRegister;//this is used as a form of temporary storage for the players current health
        protected int EnemyHPRegister;//this is used as a form of temporary storage for the enemy's current health
        protected int ManaRegister;//this is used as a form of temporary storage for the players current mana
        Enemy enemy;
        public Combat(Player player)//Constructor method passes the player instance into the combat class so it can be used to fight against the enemy 
        {
            enemy = new Enemy();//an instance of the enemy class is created and the constructor method is ran. it randomises an enemy for the player to fight dependant on the world they are currently in
            EnemyHPRegister = enemy.GetCurrentHP();//as the enemy enters combat it puts their current hp into a register so it can be used for damage calcuation during the fight
            PlayerHPRegister = player.GetCurrentHP(); //as the player enters combat it puts their current hp into a register so it can be used for damage calcuation during the fight
            ManaRegister = player.GetCurrentMana(); //as the player enters combat it puts their current mana into a register so it can be used to keep track of what mana they are on during the fight during the fight
            bool input;
            string combatOption = " ";
            bool CombatExit;
            int i;
            Playerturn = false; // enemy and player turn are used to control who goes first and it manages the turns in general through the use of false/true  
            Enemyturn = false; //this is used to determine if it is the enemies turn or not 
            CombatExit = false; //this is used to determine if it is the players turn or not 
            if (player.GetSpeed() >= enemy.GetSpeed())//checks if the player speed is faster than the enemy's speed to determine who goes first
            {
                Playerturn = true;//player turn is set to true to indicate that the player goes first
            }
            else if (enemy.GetSpeed() >= player.GetSpeed())//checks if the enemy speed is faster than the player's speed to determine who goes first
            {
                Enemyturn = true;//enemy turn is set to true to indicate that the player goes first 
            }
            while (CombatExit == false)//this loops the combat until CombatExit is not false 
            {
                input = false;//sets the input as false so before the player makes a choice for combat
                if (Playerturn == true)//check the variable Playerturn to see if it is true which indicates that it is the players turn 
                {
                    while (input == false)//loops the players action choice until they give a correct answer
                    {
                        Console.Clear();// this is the start of the menu for the player to decide what action they want to perform during combat
                        Console.WriteLine("What would you like to do?");
                        Console.WriteLine("1. Attack");
                        Console.WriteLine("2. Use an item");
                        Console.WriteLine("3. Skills");
                        Console.WriteLine("4. Run");
                        combatOption = Console.ReadLine();
                        if (combatOption == "1" || combatOption == "2" || combatOption == "3" || combatOption == "4")//checks if the input is equal to these numbers 
                        {
                            input = true;// these are the accepted numbers so the input is set to true which breaks the user out of the while loop 
                        }
                    }
                    switch (combatOption)//switch complex is used to handle the inputs from the menu
                    {
                        case "1"://this is the player attack
                            
                            Random rnd = new Random();
                            i = rnd.Next(0, 20);//generates a random number between 0 to 19 
                            if (i == 0)//checks if the number generated is 0 if it is then the player performs a heavy attack which deals double damage. so the user has a 1/20 chance of hitting double damage
                            {
                                EnemyHPRegister -= ((((player.GetAttack() / 5) * 3) * 2) - ((enemy.GetDefense() / 5) * 2)); //every 5 points of attack that the player has they gain 3 attack point and the total is doubled because its a heavy attack and every 5 points of defense the enemy has they gain 2 defense point then the defense points are taken off the attack then the attack is taken of the hp
                                Console.WriteLine("You have launched a heavy attack that dealt {0}", ((((player.GetAttack() / 5) * 3) * 2) - ((player.GetDefense() / 5) * 2)));// displays how much damage that the player does
                                Console.ReadLine();
                            }
                            else//if i is not equal to 0 then the player does a basic attack
                            {
                                EnemyHPRegister -= (((player.GetAttack() / 5) * 3) - ((enemy.GetDefense() / 5) * 2));//every 5 points of attack that the player has they gain 3 attack point and every 5 points of defense the enemy has they gain 2 defense point then the defense points are taken off the attack then the attack is taken of the hp
                                Console.WriteLine("You have launched an attack that dealt {0}", (((player.GetAttack() / 5) * 3) - ((enemy.GetDefense() / 5) * 2)));// displays how much damage that the player does
                                Console.ReadLine();
                            }
                            if (EnemyHPRegister <= 0)//checks if the enemy has less than or equal to 0 hp
                            {
                                Console.WriteLine("{0} has died", enemy.GetName());//enemy death message is printed 
                                Console.ReadLine();
                                player.SetMoney(player.GetMoney() + enemy.GetMoney());//rewards the player with the enemies money being added to theirs
                                player.SetCurrentHP(PlayerHPRegister);//sets the currenthp of the player to the value that is stored inside of the PlayerHPRegister
                                CombatExit = true;//assigns the variable combatexit with the value true which ends the combat by breaking the user out of the while loop
                            }
                            Playerturn = false;//assign the variable Playerturn with the value false to indicates it is the end of the player turn 
                            Enemyturn = true;//assign the variable Enemyturn with the value true to indicates it is the start of the enemy turn 
                            break;
                        case "2"://player use of items
                            input = false;
                            string option = "";
                            while (input == false)
                            {
                                Console.Clear();//this is the menu that displays the players items and how many of each they have and it also give them a choice of using them and also going back to choose a different action 
                                Console.WriteLine("Please choose an Item to use");
                                Console.WriteLine("1. {0}", player.GetItemName(0));
                                Console.WriteLine("Amount: {0}", player.GetItemAmount(0));
                                Console.WriteLine("");
                                Console.WriteLine("2. {0}", player.GetItemName(1));
                                Console.WriteLine("Amount: {0}", player.GetItemAmount(1));
                                Console.WriteLine("");
                                Console.WriteLine("3. Back");
                                option = Console.ReadLine();
                                if (option == "1" || option == "2" || option == "3")//checks if the input is equal to these numbers 
                                {
                                    input = true;
                                }
                            }
                            switch (option)//switch complex to 
                            {
                                case "1"://selection of item 1

                                    if (player.GetItemAmount(0) < 1)//checks if the player has enough Health potions
                                    {
                                        Console.WriteLine("You do not have enough of this item");
                                        Console.ReadLine();
                                    }
                                    else if (PlayerHPRegister == player.GetMaxHP())//checks if the player is already on full health
                                    {
                                        Console.WriteLine("You cant heal when on full health");
                                        Console.ReadLine();
                                    }
                                    else //player can heal
                                    {
                                        int HPIncrease = player.GetMaxHP() - PlayerHPRegister;//works out how much hp the player gains by using the health potion
                                        PlayerHPRegister += player.GetHP();//heals the player for how much the health potion gives
                                        if (PlayerHPRegister > player.GetMaxHP())//checks if the players current hp in the register is greater than the maximum hp
                                        {
                                            PlayerHPRegister = player.GetMaxHP();//makes the current hp equal to the maximum hp so the player cannot go over their max
                                        }
                                        Console.WriteLine("You have healed {0} health",HPIncrease);//prints a message that tells the player how much they have healed 
                                        Console.ReadLine();
                                        player.SetAmount(player.GetItemAmount(0) - 1, 0);//the amount of health potions that the player has decreases by one 
                                    }
                                    break;
                                case "2"://selection of item 2
                                    if (player.GetItemAmount(1) < 1)//checks if the player has enough mana potions
                                    {
                                        Console.WriteLine("You do not have enough of this item");
                                        Console.ReadLine();
                                        input = false;
                                    }
                                    else if (ManaRegister == player.GetMaxMana())//checks if the player is already on maximum mana
                                    {
                                        Console.WriteLine("You cant gain mana when on full mana");
                                        Console.ReadLine();
                                        input = false;
                                    }
                                    else//player drinks the health potion
                                    {
                                        int ManaIncrease = player.GetMaxMana() - ManaRegister;//works out how much mana the player gains by using the mana potion
                                        ManaRegister += player.GetMana();//restores the players mana 
                                        if (ManaRegister > player.GetMaxMana())//checks if the player current mana is equal to the maximum
                                        {
                                            ManaRegister = player.GetMaxMana();//sets the players current mana to the maximum
                                        }
                                        Console.WriteLine("You have gained {0} mana", ManaIncrease);//prints a message that tells the player how much they have restored
                                        Console.ReadLine();
                                        player.SetAmount(player.GetItemAmount(1) - 1, 1);//the amount of mana potions that the player has decreases by one
                                        input = false;
                                    }
                                    break;
                            }

                            break;
                        case "3"://selection of skills
                            option = "";
                            input = false;
                            while (option != "5") //while the option is not equal to 5 loop the code inside which would be equal to the exit option
                            {
                                while (input == false)//loop the menu till the user enters an accepted option
                                {
                                    Console.Clear();
                                    if (player.GetNodeAcquired(0) == false & player.GetNodeAcquired(3) == false & player.GetNodeAcquired(6) == false & player.GetNodeAcquired(9)== false)//checks if the user has no skills acquired
                                    {
                                        Console.WriteLine("You Currently have no skills aquired");//displays message to tell them they have no skills equipped
                                        Console.ReadLine();
                                        input = true;//breaks the user out of the menu
                                        option = "5";//breaks the user of of the selection of skills

                                    }
                                    else
                                    {
                                        Console.WriteLine("Please choose a Skill to use");
                                        if (player.GetNodeAcquired(0) == true)//checks if the user has the first skill equiped and then displays that skills name and description
                                        {
                                            Console.WriteLine("1. {0}", player.GetSkillName(0));
                                            Console.WriteLine("Desc: {0}", player.GetSkillDesc(0));
                                            Console.WriteLine("");
                                        }
                                        if (player.GetNodeAcquired(3) == true)//checks if the user has the fourth skill equiped and then displays that skills name and description
                                        {
                                            Console.WriteLine("2. {0}", player.GetSkillName(1));
                                            Console.WriteLine("Desc: {0}", player.GetSkillDesc(1));
                                            Console.WriteLine("");
                                        }
                                        if (player.GetNodeAcquired(6) == true)//checks if the user has the sixth skill equiped and then displays that skills name and description
                                        {
                                            Console.WriteLine("3. {0}", player.GetSkillName(2));
                                            Console.WriteLine("Desc: {0}", player.GetSkillDesc(2));
                                            Console.WriteLine("");
                                        }
                                        if (player.GetNodeAcquired(9) == true)//checks if the user has the ninth skill equiped and then displays that skills name and description
                                        {
                                            Console.WriteLine("4. {0}", player.GetSkillName(3));
                                            Console.WriteLine("Desc: {0}", player.GetSkillDesc(3));
                                            Console.WriteLine("");
                                        }

                                        Console.WriteLine("5. Back");
                                        option = Console.ReadLine();
                                        if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5")//checks if the input is equal to these numbers 
                                        {
                                            input = true; //these are the accepted numbers so the input is set to true which breaks the user out of the while loop 
                                        }
                                    }

                                }
                                switch (option)
                                {
                                    case "1"://selection of skill 1
                                        if (ManaRegister < player.GetSkillResource(0)) //checks if the players current mana is less than the resource cost of the skill
                                        {
                                            Console.WriteLine("You havent got Enough Mana");//displays user message to tell them that they dont have enough mana
                                            Console.ReadLine();
                                            input = false;
                                        }
                                        else
                                        {
                                            EnemyHPRegister -= (((player.GetSkillAttack(0)/5)*4) - ((enemy.GetDefense() / 5) * 2));//every five points of attack on the current skill gains 4 attack points and every 5 points of defense the enemy has they gain 2 defense point then the defense points are taken off the attack then the attack is taken of the hp
                                            ManaRegister -= player.GetSkillResource(0);//takes the resource cost off the players current mana
                                            if (EnemyHPRegister >= 1)//tests the enemy's current hp to see if they are alive 
                                            {
                                                Console.WriteLine("");
                                                Console.WriteLine("You hit the enemy with {0} for {1}", player.GetSkillName(0), (((player.GetSkillAttack(0) / 5) * 4) - ((enemy.GetDefense() / 5) * 2)));//prints how much damage was dealt to the enemy
                                                Console.WriteLine("Press enter to proceed");
                                                Console.ReadLine();
                                                Playerturn = false;
                                                Enemyturn = true;
                                            }
                                            option = "5";
                                        }
                                        break;
                                    case "2"://selection of skill 2 anotation from above still aplies 
                                        if (player.GetCurrentMana() < player.GetSkillResource(1))
                                        {
                                            Console.WriteLine("You havent got Enough Mana");
                                            Console.ReadLine();
                                            input = false;
                                        }
                                        else
                                        {
                                            EnemyHPRegister -= (((player.GetSkillAttack(1) / 5) * 4) - ((enemy.GetDefense() / 5) * 2));
                                            ManaRegister -= player.GetSkillResource(1);
                                            if (EnemyHPRegister >= 1)
                                            {
                                                Console.WriteLine("");
                                                Console.WriteLine("You hit the enemy with {0} for {1}", player.GetSkillName(1), (((player.GetSkillAttack(1)/ 5) * 4) - ((enemy.GetDefense() / 5) * 2)));
                                                Console.WriteLine("Press enter to proceed");
                                                Console.ReadLine();
                                                Playerturn = false;
                                                Enemyturn = true;
                                            }
                                            option = "5";
                                        }
                                        break;
                                    case "3"://selection of skill 3
                                        if (player.GetCurrentMana() < player.GetSkillResource(2))
                                        {
                                            Console.WriteLine("You havent got Enough Mana");
                                            Console.ReadLine();
                                            input = false;
                                        }
                                        else
                                        {
                                            EnemyHPRegister -= (((player.GetSkillAttack(2) / 5) * 4) - ((enemy.GetDefense() / 5) * 2));
                                            ManaRegister -= player.GetSkillResource(2);
                                            if (EnemyHPRegister >= 1)
                                            {
                                                Console.WriteLine("");
                                                Console.WriteLine("You hit the enemy with {0} for {1}", player.GetSkillName(2), (((player.GetSkillAttack(2) / 5) * 4) - ((enemy.GetDefense() / 5) * 2)));
                                                Console.WriteLine("Press enter to proceed");
                                                Console.ReadLine();
                                                Playerturn = false;
                                                Enemyturn = true;
                                            }
                                            option = "5";
                                        }
                                        break;
                                    case "4"://selection of skill 4
                                        if (player.GetCurrentMana() < player.GetSkillResource(3))
                                        {
                                            Console.WriteLine("You havent got Enough Mana");
                                            Console.ReadLine();
                                            input = false;
                                        }
                                        else
                                        {
                                            EnemyHPRegister -= (((player.GetSkillAttack(3) / 5) * 4) - ((enemy.GetDefense() / 5) * 2));
                                            ManaRegister -= player.GetSkillResource(3);
                                            if (EnemyHPRegister >= 1)
                                            {
                                                Console.WriteLine("");
                                                Console.WriteLine("You hit the enemy with {0} for {1}", player.GetSkillName(3), (((player.GetSkillAttack(3) / 5) * 4) - ((enemy.GetDefense() / 5) * 2)));
                                                Console.WriteLine("Press enter to proceed");
                                                Console.ReadLine();
                                                Playerturn = false;
                                                Enemyturn = true;
                                            }
                                            option = "5";
                                        }
                                        break;
                                }
                            }
                            if (EnemyHPRegister <= 0)//checks if the enemys current hp is less than or equal to 0 
                            {
                                Console.WriteLine("{0} has died", enemy.GetName()); //displays the name of the enemy that has died
                                Console.ReadLine();
                                player.SetMoney(player.GetMoney() + enemy.GetMoney());//rewards the player with the enemy money
                                player.SetCurrentHP(PlayerHPRegister);//sets the players current hp to the value in the hp register 
                                CombatExit = true;//breaks the user out of the combat loop
                            }

                            break;
                        case "4"://player running away
                            player.SetCurrentHP(PlayerHPRegister);//sets the players current hp to the value in the hp register 
                            CombatExit = true;//breaks the user out of the combat loop
                            break;
                    }
                    
                }
                else if (Enemyturn == true)//enemy turn
                {
                    Console.Clear();
                    Random rnd = new Random();
                    bool actionLoop = false;
                    while (actionLoop == false)// this is where the enemy decides to use different attacks or heals
                    {
                        if (EnemyHPRegister <= enemy.GetMaxHP() * 0.40)// checks if the enemy's current hp is less than or equal to the maximum hp * 0.40
                        {
                            i = rnd.Next(0, 6);//randomly generates a number between 0 and 5 to determine if the enemy heals or attacks
                            if (i == 5)//enemy heals 1/6 chance if below certain hp thresh hold
                            {
                                EnemyHPRegister += 25;//enemy heals 25 hp
                                if (EnemyHPRegister >= enemy.GetMaxHP())//checks if the enemy's hp is greater than its maximum hp
                                {
                                    EnemyHPRegister = enemy.GetMaxHP();//set the enemies current hp back to the max so it cant have 120/100
                                }
                                Console.WriteLine("{0} has healed for 25 health points", enemy.GetName());// displays the enemy healing 25 hp
                                Console.ReadLine();
                                actionLoop = true;
                            }
                            else//enemy has 5/6 chance of attack if bellow less than or equal to 40%
                            {
                                i = rnd.Next(0, 20);// randomly generates anumber between 0 and 19
                                if (i == 0)//enemy heavy attack 1/20 chance
                                {//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
                                    PlayerHPRegister -= ((((enemy.GetAttack() / 5) * 3) * 2) - ((player.GetDefense() / 5) * 2));//every five points of attack the enemy has gains 3 attack points and every 5 points of defense the playerhas they gain 2 defense point then the defense points are taken off the attack then the attack is taken of the hp
                                    Console.WriteLine("{0} has launched an attack and dealt {1}", enemy.GetName(), ((((enemy.GetAttack() / 5) * 3) * 2) - ((player.GetDefense() / 5) * 2)));//displays the name of the enemy amd how much damage they dealt
                                    Console.ReadLine();
                                    actionLoop = true;
                                }
                                else// enemy basic attack 19/20 chance
                                {
                                    PlayerHPRegister -= (((enemy.GetAttack() / 5) * 3) - ((player.GetDefense() / 5) * 2));//does the same as a basic attack but the attack points are doubled
                                    Console.WriteLine("{0} has launched an attack and dealt {1}", enemy.GetName(), (((enemy.GetAttack() / 5) * 3) - ((player.GetDefense() / 5) * 2)));//displays the name of the enemy and how much damage they dealt
                                    Console.ReadLine();
                                    actionLoop = true;
                                }
                            }
                        }
                        else//enemy attacks instead of the chance to heal
                        {
                            i = rnd.Next(0, 20);// randomly generates anumber between 0 and 19
                            if (i == 0)//enemy heavy attack 1/20 chance
                            {
                                PlayerHPRegister -= ((((enemy.GetAttack() / 5) * 3) * 2) - ((player.GetDefense() / 5) * 2));//every five points of attack the enemy has gains 3 attack points and every 5 points of defense the playerhas they gain 2 defense point then the defense points are taken off the attack then the attack is taken of the hp
                                Console.WriteLine("{0} has launched an attack and dealt {1}", enemy.GetName(), ((((enemy.GetAttack() / 5) * 3) * 2) - ((player.GetDefense() / 5) * 2)));//displays the name of the enemy and how much damage they dealt
                                Console.ReadLine();
                                actionLoop = true;
                            }
                            else//enemy basic attack 19/20 chance
                            {
                                PlayerHPRegister -= (((enemy.GetAttack() / 5) * 3) - ((player.GetDefense() / 5) * 2));//does the same as a basic attack but the attack points are doubled
                                Console.WriteLine("{0} has launched an attack and dealt {1}", enemy.GetName(), (((enemy.GetAttack() / 5) * 3) - ((player.GetDefense() / 5) * 2)));//displays the name of the enemy and how much damage they dealt
                                Console.ReadLine();
                                actionLoop = true;
                            }
                        }
                    }
                    if (PlayerHPRegister <= 0)// check if the player is dead
                    {
                        if(player.GetItemAmount(2)>=1)//checks if the player has greater than or equal to 1 of revive potions
                        {
                            input = false;
                            int option=0;
                            while(input ==false)
                            {
                                Console.Clear();
                                Console.WriteLine("You Have {0} {1}", player.GetItemAmount(2), player.GetItemName(2));//tells the user how many revive potions they have and asks for a user input to revive or not to revive
                                Console.WriteLine("");
                                Console.WriteLine("Do you want to revive?");
                                Console.WriteLine("1. Yes");
                                Console.WriteLine("2. No");
                                Console.WriteLine("Enter the number that corresponds with your option of choice");
                                option = int.Parse(Console.ReadLine());
                                if (option== 1||option==2)
                                {
                                    input = true;
                                }
                            }
                            if(option==1)//checks if player option is 1 (YES)
                            {
                                Console.Clear();
                                Console.WriteLine("You Have been revived and have {0}/{1} HP",player.GetMaxHP()/2,player.GetMaxHP());//tells the user that they have revived and how much hp thy have left
                                player.SetAmount(player.GetItemAmount(2) - 1,2);//takes one revive potion off the player
                                PlayerHPRegister = player.GetMaxHP() /2;//sets the players current hp to max
                                Console.ReadLine();
                            }
                            else if(option==2)//checks if player option is 2 (NO)
                            {
                                Console.Clear();
                                Console.WriteLine("You Died and dropped 100z");
                                Console.WriteLine("Press enter to proceed");
                                Console.ReadLine();
                                player.SetMoney(player.GetMoney() - 100);//the player dies and there for loses 100 money
                                if (player.GetMoney() < 0)//check if money is lower than 0
                                {
                                    player.SetMoney(0);//sets money to 0 if so as money cannot be less than 0
                                } 
                                CombatExit = true;
                            }
                            
                        }
                        else if (player.GetItemAmount(2) <= 0)//check if the player has no revive potions 
                        {
                            Console.Clear();
                            Console.WriteLine("You Died and dropped 100z");
                            Console.WriteLine("Press enter to proceed");
                            Console.ReadLine();
                            player.SetMoney(player.GetMoney() - 100);//the player dies and there for loses 100 money
                            if (player.GetMoney() < 0)//check if money is lower than 0
                            {
                                player.SetMoney(0);//sets money to 0 if so as money cannot be less than 0
                            }
                            CombatExit = true;
                        }
                    }
                    Enemyturn = false;
                    Playerturn = true;
                }
            }
        }
    }
}
