using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelRPGProject
{
    class Enemy:Character
    {
        public Enemy()//loads in diffent enemies dependant on players currentworld
        {
            if(World.GetCurrentWorld()==1|| World.GetCurrentWorld() == 7 || World.GetCurrentWorld() == 8 || World.GetCurrentWorld() == 9)//checks if you are in world 1 or any of its dungeon floors
            {
                World1Enemies();
            }
            else if(World.GetCurrentWorld()==2|| World.GetCurrentWorld() == 11 || World.GetCurrentWorld() == 12 || World.GetCurrentWorld() == 13)//checks if you are in world 2 or any of its dungeon floors
            {
                World2Enemies();
            }
            else if(World.GetCurrentWorld() == 3 || World.GetCurrentWorld() == 15 || World.GetCurrentWorld() == 16 || World.GetCurrentWorld() == 17)//checks if you are in world 3 or any of its dungeon floors
            {
                World3Enemies();
            }
            else if (World.GetCurrentWorld() == 4 || World.GetCurrentWorld() == 19 || World.GetCurrentWorld() == 20 || World.GetCurrentWorld() == 21)//checks if you are in world 3 or any of its dungeon floors
            {
                World4Enemies();
            }
        }
        public struct Enemies//holds data about each enemy
        {
            public string Name;//stores the name of the enemy
            public int MaxHP;//stores the maximum health points of the the enemy
            public int CurrentHP;//stores the current health points of the enemy
            public int Attack;//stores the attack stat of the enemy
            public int Defense;//stores the defense stat of the enemy
            public int Speed;//stores the speed stat of the enemy
            public int Money;//stores the amount of money the enemy will drop on death
        }
        public void World1Enemies()//creates data for each enemy then picks from 3 different enemies to fight randomly and if you are in the last floor of the dungeon you will encounter the boss
        {
            int i = 0;
            Enemies[] worldEnemy1 = new Enemies[4];//creates an instance of the Enemies struct
            //sets the data for all of the enemies 
            worldEnemy1[0].Name = "Snake";
            worldEnemy1[0].MaxHP = 150;
            worldEnemy1[0].CurrentHP = worldEnemy1[0].MaxHP;
            worldEnemy1[0].Attack = 50;
            worldEnemy1[0].Defense = 40;
            worldEnemy1[0].Speed = 60;
            worldEnemy1[0].Money = 100;
            worldEnemy1[1].Name = "Mutated Rat";
            worldEnemy1[1].MaxHP = 200;
            worldEnemy1[1].CurrentHP = worldEnemy1[1].MaxHP;
            worldEnemy1[1].Attack = 50;
            worldEnemy1[1].Defense = 40;
            worldEnemy1[1].Speed = 45;
            worldEnemy1[1].Money = 100;
            worldEnemy1[2].Name = "Slime";
            worldEnemy1[2].MaxHP = 100;
            worldEnemy1[2].CurrentHP = worldEnemy1[2].MaxHP;
            worldEnemy1[2].Attack = 50;
            worldEnemy1[2].Defense = 40;
            worldEnemy1[2].Speed = 1;
            worldEnemy1[2].Money = 100;
            worldEnemy1[3].Name = "Rat Overlord";
            worldEnemy1[3].MaxHP = 250;
            worldEnemy1[3].CurrentHP = worldEnemy1[3].MaxHP;
            worldEnemy1[3].Attack = 50;
            worldEnemy1[3].Defense = 40;
            worldEnemy1[3].Speed = 50;
            worldEnemy1[3].Money = 250;
            if (World.GetCurrentWorld() == 1)//if you are in world 1 then it generates enemies from 0-2 so 3 excluding the boss
            {
                Random rnd = new Random();
                i = rnd.Next(0, 3);
            }
            else if (World.GetCurrentWorld() == 9)//if you are on the last floor of the dungeon it sets i to 3
            {
                i = 3;
            }
            //using the number generated as an index it asigned the enemy stats from the structs to the class attributes
            Name = worldEnemy1[i].Name;
            MaxHP = worldEnemy1[i].MaxHP;
            CurrentHP = worldEnemy1[i].CurrentHP;
            Attack = worldEnemy1[i].Attack;
            Defense = worldEnemy1[i].Defense;
            Speed = worldEnemy1[i].Speed;
            Money = worldEnemy1[i].Money;
            Console.WriteLine("You have encountered {0}", Name);
            Console.ReadLine();
        }
        public void World2Enemies()//creates data for each enemy then picks from 3 different enemies to fight randomly and if you are in the last floor of the dungeon you will encounter the boss
        {
            int i = 0;
            Enemies[] worldEnemy2 = new Enemies[4];//creates an instance of the enemies struct
            //sets the data for all of the enemies
            worldEnemy2[0].Name = "Giant Spider";
            worldEnemy2[0].MaxHP = 100;
            worldEnemy2[0].CurrentHP = 100;
            worldEnemy2[0].Attack = 10;
            worldEnemy2[0].Defense = 100;
            worldEnemy2[0].Speed = 30;
            worldEnemy2[0].Money = 100;
            worldEnemy2[1].Name = "Bandit Archer";
            worldEnemy2[1].MaxHP = 100;
            worldEnemy2[1].CurrentHP = 100;
            worldEnemy2[1].Attack = 10;
            worldEnemy2[1].Defense = 100;
            worldEnemy2[1].Speed = 30;
            worldEnemy2[1].Money = 100;
            worldEnemy2[2].Name = "Bandit Warrior";
            worldEnemy2[2].MaxHP = 100;
            worldEnemy2[2].CurrentHP = 100;
            worldEnemy2[2].Attack = 10;
            worldEnemy2[2].Defense = 100;
            worldEnemy2[2].Speed = 30;
            worldEnemy2[2].Money = 100;
            worldEnemy2[3].Name = "Kobolt The Mage";
            worldEnemy2[3].MaxHP = 100;
            worldEnemy2[3].CurrentHP = 100;
            worldEnemy2[3].Attack = 10;
            worldEnemy2[3].Defense = 100;
            worldEnemy2[3].Speed = 30;
            worldEnemy2[3].Money = 100;
            if (World.GetCurrentWorld() == 2)//if you are in world 2 then it generates enemies from 0-2 so 3 excluding the boss
            {
                Random rnd = new Random();
                i = rnd.Next(0, 3);
            }
            else if (World.GetCurrentWorld() == 13)//if you are on the last floor of the dungeon you will encounter the boss
            {
                i = 3;
            }
            //using the number generated as an index it asigned the enemy stats from the structs to the class attributes
            Name = worldEnemy2[i].Name;
            MaxHP = worldEnemy2[i].MaxHP;
            CurrentHP = worldEnemy2[i].CurrentHP;
            Attack = worldEnemy2[i].Attack;
            Defense = worldEnemy2[i].Defense;
            Speed = worldEnemy2[i].Speed;
            Money = worldEnemy2[i].Money;
            Console.WriteLine("You have encountered {0}", Name);
            Console.ReadLine();
        }
        public void World3Enemies()//creates data for each enemy then picks from 3 different enemies to fight randomly and if you are in the last floor of the dungeon you will encounter the boss
        {
            int i = 0;
            Enemies[] worldEnemy3 = new Enemies[4];
            //sets the data for all of the enemies
            worldEnemy3[0].Name = "Frost Orc";
            worldEnemy3[0].MaxHP = 100;
            worldEnemy3[0].CurrentHP = 100;
            worldEnemy3[0].Attack = 10;
            worldEnemy3[0].Defense = 100;
            worldEnemy3[0].Speed = 30;
            worldEnemy3[0].Money = 100;
            worldEnemy3[1].Name = "Lesser Ice Troll";
            worldEnemy3[1].MaxHP = 100;
            worldEnemy3[1].CurrentHP = 100;
            worldEnemy3[1].Attack = 10;
            worldEnemy3[1].Defense = 100;
            worldEnemy3[1].Speed = 30;
            worldEnemy3[1].Money = 100;
            worldEnemy3[2].Name = "Frost Sprite";
            worldEnemy3[2].MaxHP = 100;
            worldEnemy3[2].CurrentHP = 100;
            worldEnemy3[2].Attack = 10;
            worldEnemy3[2].Defense = 100;
            worldEnemy3[2].Speed = 30;
            worldEnemy3[2].Money = 100;
            worldEnemy3[3].Name = "Grog The Troll";
            worldEnemy3[3].MaxHP = 100;
            worldEnemy3[3].CurrentHP = 100;
            worldEnemy3[3].Attack = 10;
            worldEnemy3[3].Defense = 100;
            worldEnemy3[3].Speed = 30;
            worldEnemy3[3].Money = 100;
            if (World.GetCurrentWorld() == 3)//if you are in world 1 then it generates enemies from 0-2 so 3 excluding the boss
            {
                Random rnd = new Random();
                i = rnd.Next(0, 3);
            }
            else if (World.GetCurrentWorld() == 17)//if you are on the last floor of the dungeon you will encounter the boss
            {
                i = 3;
            }
            //using the number generated as an index it asigned the enemy stats from the structs to the class attributes
            Name = worldEnemy3[i].Name;
            MaxHP = worldEnemy3[i].MaxHP;
            CurrentHP = worldEnemy3[i].CurrentHP;
            Attack = worldEnemy3[i].Attack;
            Defense = worldEnemy3[i].Defense;
            Defense = worldEnemy3[i].Speed;
            Money = worldEnemy3[i].Money;
            Console.WriteLine("You have encountered {0}", Name);
            Console.ReadLine();
        }
        public void World4Enemies()//creates data for each enemy then picks from 3 different enemies to fight randomly and if you are in the last floor of the dungeon you will encounter the boss
        {
            int i = 0;
            Enemies[] worldEnemy4 = new Enemies[4];
            //sets the data for all of the enemies
            worldEnemy4[0].Name = "Undead Bandit";
            worldEnemy4[0].MaxHP = 100;
            worldEnemy4[0].CurrentHP = 100;
            worldEnemy4[0].Attack = 10;
            worldEnemy4[0].Defense = 100;
            worldEnemy4[0].Speed = 30;
            worldEnemy4[0].Money = 100;
            worldEnemy4[1].Name = "Lesser Wyvern";
            worldEnemy4[1].MaxHP = 100;
            worldEnemy4[1].CurrentHP = 100;
            worldEnemy4[1].Attack = 10;
            worldEnemy4[1].Defense = 100;
            worldEnemy4[1].Speed = 30;
            worldEnemy4[1].Money = 100;
            worldEnemy4[2].Name = "Blood Mage";
            worldEnemy4[2].MaxHP = 100;
            worldEnemy4[2].CurrentHP = 100;
            worldEnemy4[2].Attack = 10;
            worldEnemy4[2].Defense = 100;
            worldEnemy4[2].Speed = 30;
            worldEnemy4[2].Money = 100;
            worldEnemy4[3].Name = "The Undead Wyvern";
            worldEnemy4[3].MaxHP = 100;
            worldEnemy4[3].CurrentHP = 100;
            worldEnemy4[3].Attack = 10;
            worldEnemy4[3].Defense = 100;
            worldEnemy4[3].Speed = 30;
            worldEnemy4[3].Money = 100;
            if (World.GetCurrentWorld() == 4)//if you are in world 4 then it generates enemies from 0-2 so 3 excluding the boss
            {
                Random rnd = new Random();
                i = rnd.Next(0, 3);
            }
            else if (World.GetCurrentWorld() == 21)//if you are on the last floor of the dungeon you will encounter the boss
            {
                i = 3;
            }
            //using the number generated as an index it asigned the enemy stats from the structs to the class attributes
            Name = worldEnemy4[i].Name;
            MaxHP = worldEnemy4[i].MaxHP;
            CurrentHP = worldEnemy4[i].CurrentHP;
            Attack = worldEnemy4[i].Attack;
            Defense = worldEnemy4[i].Defense;
            Defense = worldEnemy4[i].Speed;
            Money = worldEnemy4[i].Money;
            Console.WriteLine("You have encountered {0}", Name);
            Console.ReadLine();
        }
    }
}
