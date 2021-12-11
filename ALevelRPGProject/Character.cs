using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelRPGProject
{
    class Character
    {
        protected string Name;//shared stats of the player and enemy
        protected int MaxHP;//Max Health points
        protected int CurrentHP;//current health points
        protected int Attack;//stat which is used to deal damage to the player/enemy
        protected int Defense;//stat which stops the player/enemy from taking  
        protected int Speed;//stat which determines if the player/enemy goes first 
        protected int Money;//stat used to keep track of the players money, used to purchase items, lost on death and gained from killing enemies
        public string GetName()//returns the value for name
        {
            return Name;
        }
        public int GetMaxHP()//returns the value for MaxHP
        {
            return MaxHP;
        }
        public int GetCurrentHP()//returns the value for CurrentHP
        {
            return CurrentHP;
        }
        public int GetAttack()//returns the value for Attack
        {
            return Attack;
        }
        public int GetDefense()//returns the value for Defense
        {
            return Defense;
        }
        public int GetSpeed()//returns the value for Speed
        {
            return Speed;
        }
        public int GetMoney()//returns the value for money
        {
            return Money;
        }
    }
}

