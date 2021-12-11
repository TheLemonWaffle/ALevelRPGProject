using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using ADOX;

namespace ALevelRPGProject
{
    class Load:NewDatabase
    {
        public Load(Player player)//every player in the database is printed out as a list the player can then choose one of the players and load their data into the game from the database
        {
            //have the menu here then valdiation that lets the user return to the main menu if there are no options avaliable
            string stringChoice = "";
            bool input = false;
            while (input == false)
            {
                Console.Clear();
                Console.WriteLine("Enter a number with the corresponding slot you want to load");
                Console.WriteLine("");
                for (int i = 1; i < AutoSlotID(); i++)//for loop that that finds the all of the names of players in the database and puts them into an array list
                {
                    Console.WriteLine("{0}. {1}", i, NameReturn(i));//changed -1
                    Console.WriteLine("");
                    if (i == AutoSlotID() - 1)
                    {
                        Console.WriteLine("{0}. Exit", i + 1);
                    }
                }
                try
                {
                    stringChoice = Console.ReadLine();
                    if (int.Parse(stringChoice) < AutoSlotNumber() && stringChoice != "0")
                    {
                        for (int i = 1; i < AutoSlotNumber(); i++)//validation for each user input for the slot load menu
                        {
                            if (int.Parse(stringChoice) == i)
                            {
                                input = true;
                            }
                        }
                    }
                    else if(stringChoice==AutoSlotNumber().ToString())
                    {
                        input = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter a correct slot number");
                        Console.ReadLine();
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a correct slot number");
                    Console.ReadLine();
                }
            }
            if (int.Parse(stringChoice) == AutoSlotNumber())
            {
                Console.WriteLine();
                Console.WriteLine("Going back to the main menu");
                Console.ReadLine();
            }
            else
            {
                SQLString = "SELECT PlayerID FROM Slots WHERE SlotNum = ?";//sql statement
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Parameters.AddWithValue("@SlotNum", int.Parse(stringChoice));
                        cmd.Connection = con;//sets the connection to the connection string which allows it to connect to the database
                        cmd.CommandText = SQLString;
                        using (OleDbDataReader dbReader = cmd.ExecuteReader())
                        {
                            if (dbReader.Read())//moves the reader to the next record
                            {
                                player.SetPlayerID(int.Parse(dbReader[0].ToString()));//asigns the value in the first index of the record to the variable slot choice 
                            }
                        }
                    }
                    con.Close();
                }
                //contains all of the loading subroutines and functions
                player.CreateNPCStats();
                SelectPlayerTable(player);
                SelectQuestTable(player);
                SelectSkillTreeTable(player);
                SelectItemTable(player);
                World world = new World(player);
            }
        }
        public string NameReturn(int i)// returns the name of a player in the player table where the playerid in slot is the same as playerid in the player table
        {
            string Names = "";
            SQLString = "SELECT Player.Name FROM Player INNER JOIN Slots ON Player.PlayerID = Slots.PlayerID WHERE Slots.PlayerID = @PlayerID";//sql statement
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();//opens connection with the database
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Parameters.AddWithValue("@PlayerID", i);//changed +1
                    cmd.Connection = con;//sets the connection string so that it can connect to the database
                    cmd.CommandText = SQLString;//sets the command text to the sql statement
                    using (OleDbDataReader dbReader = cmd.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            Names = dbReader[0].ToString();
                        }
                    }
                }
                con.Close();//closes connection with the database
            }
            return Names;
        }
        public void SelectPlayerTable(Player player)// selects the data from the player table in the database and assigns it to their corresponding the variables
        {
            SQLString = "SELECT Name, CurrentHP, MaxHP, CurrentMANA, MaxMANA, Role, Attack, Defence, Speed, Zenny, Skillpoints, CurrentWorld FROM Player WHERE PlayerID = ?";//sql statement
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();//opens a connection to the database 
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = SQLString;
                    cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());

                    using (OleDbDataReader dbReader = cmd.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            //assigns all of the player stats from each index of the record to their respective player attributes
                            player.SetName(dbReader[0].ToString());
                            player.SetCurrentHP(int.Parse(dbReader[1].ToString()));
                            player.SetMaxHP(int.Parse(dbReader[2].ToString()));
                            player.SetCurrentMana(int.Parse(dbReader[3].ToString()));
                            player.SetMaxMana(int.Parse(dbReader[4].ToString()));
                            player.SetRole(dbReader[5].ToString());
                            player.SetAttack(int.Parse(dbReader[6].ToString()));
                            player.SetDefense(int.Parse(dbReader[7].ToString()));
                            player.SetSpeed(int.Parse(dbReader[8].ToString()));
                            player.SetMoney(int.Parse(dbReader[9].ToString()));
                            player.SetSkillpoints(int.Parse(dbReader[10].ToString()));
                            World.SetCurrentWorld(int.Parse(dbReader[11].ToString()));
                        }
                    }
                }   
                con.Close();//closes the connection to the database
            }
            player.CreateSkillInv();
        }
        public void SelectQuestTable(Player player)// selects the the data from the quest table in the databasr and assigns it to 
        {
            player.CreateQuestlog();
            SQLString = "SELECT Acquired, BossDefeat, Completed FROM Quests WHERE PlayerID = ?";
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();//opens connection with the database
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;//sets the connection to the connection string which allows it to connect to the database
                    cmd.CommandText = SQLString;//sets the 
                    cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());
                    using (OleDbDataReader dbReader = cmd.ExecuteReader())//executes the sql statement and creates an instance of the data reader class
                    {
                        for (int i = 0; i < 5; i++)//for loop that loops 5 times which moves the reader onto the record 5 times to take the infomation from the 5 record and put them back into the program
                        {
                            if (dbReader.Read())//moves the reader to the next record
                            {
                                //these set the values of the acquired, bossdefeat and completed values in the quest struct 
                                player.SetQuestAcquired(bool.Parse(dbReader[0].ToString()), i);
                                player.SetQuestBossDefeat(bool.Parse(dbReader[1].ToString()), i);
                                player.SetQuestCompleted(bool.Parse(dbReader[2].ToString()), i);
                            }
                        }
                    }
                }
                con.Close();//closes the connection with the database
            }
           
            //these are parts of the quest struct that are not stored in the database and so need to be re asigned on the loading of the save
        }
        public void SelectSkillTreeTable(Player player)// selects the data from the SkillTree table in the database and assigns it to respective the parameters in the program
        {
            player.CreateSkillTree();
            SQLString = "SELECT Acquired FROM Skilltree WHERE PlayerID = ?";
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();//opens connection with the database
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = SQLString;
                    cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());
                    using(OleDbDataReader dbReader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < 12; i++)//for loop that loops 12 times which moves the reader onto the record 12 times to take the infomation from the 12 record and put them back into the program                       
                        {
                            if (dbReader.Read())//moves the reader to the next record
                            {
                                player.SetNodeAcquired(bool.Parse(dbReader[0].ToString()), i);
                            }
                        }
                    }
                }
                con.Close();//closes the connection to the database
            }
            //these are parts of the quest struct that are not stored in the database and so need to be re asigned on the loading of the save
        }
        public void SelectItemTable(Player player)// selects the data from the Item table in the database and assigns it to respective the parameters in the program
        {
            player.CreateItemInv();
            SQLString = "SELECT Amount FROM Items WHERE PlayerID = ?";//sql statement
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();//opens the connection to the database
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = SQLString;
                    cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());
                    using(OleDbDataReader dbReader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (dbReader.Read())
                            {
                                player.SetAmount(int.Parse(dbReader[0].ToString()), i);
                            }
                        }
                    }
                }
                con.Close();
            }
        }
    }
}
