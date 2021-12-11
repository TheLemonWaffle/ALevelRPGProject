using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOX;
using System.Data.OleDb;
using System.IO;

namespace ALevelRPGProject
{
    class Create:NewDatabase
    {
        public Create()//runs the create database method which creates the access database file
        {
            CreateDatabase();
        }
        public int AutoPlayerNumber()//returns the maximum playerid increased by 1 from the player table
        {
            int autoNumber = 0;
            SQLString = "SELECT MAX(PlayerID) FROM Player";
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = SQLString;
                    using(OleDbDataReader dbReader = cmd.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            try
                            {
                                autoNumber = int.Parse(dbReader[0].ToString());
                                autoNumber++;
                            }
                            catch (Exception)
                            {
                                autoNumber = 1;
                            }

                        }
                    }
                }
                con.Close();
            }
            return autoNumber;
        }
        public void CreateTables()//Creates the tables in the database file for the player, quests, items, skilltree and slots
        {
            string[] TablesCreationArray = new string[] { "CREATE TABLE Player(PlayerID int NOT NULL PRIMARY KEY, Name varchar(50) NOT NULL, CurrentHP int NOT NULL, MaxHP int NOT NULL, CurrentMANA int NOT NULL, MaxMANA int NOT NULL, Role varchar(10) NOT NULL, Attack int NOT NULL, Defence int NOT NULL, Speed int NOT NULL, Zenny int NOT NULL, Skillpoints int NOT NULL, CurrentWorld int NOT NULL)", "CREATE TABLE Items (ItemID int NOT NULL PRIMARY KEY, Amount int NOT NULL, PlayerID int NOT NULL, FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID))", "CREATE TABLE Slots (SlotNum int NOT NULL PRIMARY KEY, PlayerID int, FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID))", "CREATE TABLE Quests(QuestID int NOT NULL PRIMARY KEY, Acquired YESNO NOT NULL, BossDefeat YESNO NOT NULL, Completed YESNO NOT NULL, PlayerID int NOT NULL, FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID))", "CREATE TABLE Skilltree (NodeID int NOT NULL PRIMARY KEY, Acquired YESNO NOT NULL, PlayerID int NOT NULL, FOREIGN KEY(PlayerID) REFERENCES Player (PlayerID))" };//this array contains all of the create table SQL string that the method will create
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();//opens the connection to the database
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;//sets the connection to the connection string which allows it to connect to the database
                    foreach (string CreationString in TablesCreationArray)//loop that goes through each element of the table creation array
                    {
                        try
                        {
                            cmd.CommandText = CreationString;//asigns the commandtext as the sql string from the array
                            cmd.ExecuteNonQuery();//executes the commandtext

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error creating tables with this {0}",CreationString);
                            Console.ReadLine();
                        }
                    }
                }
                con.Close();//closes the connection
            }
        }
        public void CreateDatabase()//creates the database file
        {
            Catalog cat = new Catalog();
            string[] TablesCreationArray = new string[] { "CREATE TABLE Player(PlayerID int NOT NULL AUTO_INCREMENT, Name varchar(50) NOT NULL, CurrentHP int NOT NULL, MaxHP int NOT NULL, CurrentMANA int NOT NULL, MaxMANA int NOT NULL, Role varchar(10) NOT NULL, Attack int NOT NULL, Defence int NOT NULL, Speed int NOT NULL, Zenny int NOT NULL, Skillpoints int NOT NULL, CurrentWorld int NOT NULL, PRIMARY KEY(PlayerID))", "CREATE TABLE Items (ItemID int NOT NULL PRIMARY KEY, Amount int NOT NULL, PlayerID int NOT NULL, FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID))", "CREATE TABLE Slots (SlotNum int NOT NULL, PlayerID int, FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID))", "CREATE TABLE Quests(QuestID int NOT NULL PRIMARY KEY, Acquired YESNO NOT NULL, BossDefeat YESNO NOT NULL, Completed YESNO NOT NULL, PlayerID int NOT NULL, FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID))", "CREATE TABLE Skilltree (NodeID int NOT NULL PRIMARY KEY, Acquired YESNO NOT NULL, PlayerID int NOT NULL, FOREIGN KEY(PlayerID) REFERENCES Player (PlayerID))" };//this array contains all of the create table SQL string that the method will create
            try
            {
                cat.Create(connectionString);//uses the connect string to create the access file
                CreateTables();
                Console.WriteLine("Database Created");//displays a message to notify the user that the file has been created
                Console.ReadLine();

            }
            catch (Exception)//cathes the exception and stop the program from crashing
            {

                Console.WriteLine("Loading pre existing database");//notifies the user that a pre existing database is being loaded
                Console.ReadLine();
            }
        }
    }
}
