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
    class Save:NewDatabase
    {
        public Save(Player player)//contains all of the save methods so the users data can be put into the database
        {
            SavePlayer(player);
            SaveQuest(player);
            SaveItems(player);
            SaveSkilltree(player);
        }
        public void SavePlayer(Player player)//runs sql statement for inserting the new record for the player table then if it fails it updates the already existing record
        {
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();//opens the connection to the database
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Parameters.AddWithValue("@Name", player.GetName());// adds the parameter @Name as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@CurrentHP", player.GetCurrentHP());// adds the parameter @CurrentHp as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@MaxHP", player.GetMaxHP());// adds the parameter @MaxHP as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@CurrentMANA", player.GetCurrentMana());// adds the parameter @CurrentMANA as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@MaxMANA", player.GetMaxMana());// adds the parameter @MaxMANA as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@Role", player.GetRole());// adds the parameter @Role as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@Attack", player.GetAttack());// adds the parameter @Attack as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@Defence", player.GetDefense());// adds the parameter @Defence as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@Speed", player.GetSpeed());// adds the parameter @Speed as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@Zenny", player.GetMoney());// adds the parameter @Zenny as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@Skillpoints", player.GetSkillpoints());// adds the parameter @Skillpoints as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@CurrentWorld", World.GetCurrentWorld());// adds the parameter @CurrentWorld as an @ parameter with a value
                    cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());// adds the parameter @playerID as an @ parameter with a value
                    cmd.Connection = con;//sets the connection to the connection string which allows it to connect to the database
                    try
                    {
                        SQLString = "INSERT INTO Player (Name, CurrentHP, MaxHP, CurrentMANA, MaxMANA, Role, Attack, Defence, Speed, Zenny, Skillpoints, CurrentWorld, PlayerID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";// string variablee which contains an insert SQL statement 
                        cmd.CommandText = SQLString;//makes the command text equal to the sql statement so it can be used to execute 
                        cmd.ExecuteNonQuery();//executes the sql statement in the command text
                        SaveSlot(player);
                    }
                    catch (Exception)
                    {
                        SQLString = "UPDATE Player SET Name=?, CurrentHP=?, MaxHP=?, CurrentMANA=?, MaxMANA=?, Role=?, Attack=?, Defence=?, Speed=?, Zenny=?, Skillpoints=?, CurrentWorld=? WHERE PlayerID=?";// string variablee which contains an insert SQL statement 
                        cmd.CommandText = SQLString;//makes the command text equal to the sql statement so it can be used to execute 
                        cmd.ExecuteNonQuery();//executes the sql statement in the command text
                    }
                }
               
                con.Close();//closes the connection to the database
            }
        }
        public void SaveSlot(Player player)//runs sql statement for inserting the new record for the Slots table
        {
            using(OleDbConnection con =new OleDbConnection(connectionString))
            {
                con.Open();
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Parameters.AddWithValue("@SlotNum", AutoSlotID());
                    cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());
                    cmd.Connection = con;
                    try
                    {
                        SQLString = "INSERT INTO Slots (SlotNum, PlayerID) VALUES (?, ?)";
                        cmd.CommandText = SQLString;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
                con.Close();
            }
        }
        public void SaveQuest(Player player)//runs sql statement for inserting the new record for the quest table then if it fails it updates the already existing record
        {
            
            for (int i = 0; i < 5; i++)//for loop which loops 5 times which creates a record for all 5 quests
            {
                using(OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();//opens the connection to the database
                    using(OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Parameters.AddWithValue("@Acquired", player.GetQuestAquired(i));//adds an @ parameter @Acquired with a value
                        cmd.Parameters.AddWithValue("@BossDefeat", player.GetQuestBossDefeat(i));//adds an @ parameter @BossDefeat with a value
                        cmd.Parameters.AddWithValue("@Completed", player.GetQuestComplete(i));//adds an @ parameter @Completed with a value
                        cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());//adds an @ parameter @PlayerID with a value
                        cmd.Parameters.AddWithValue("@QuestID", ((player.GetPlayerID() - 1) * 5) + (i + 1));

                        cmd.Connection = con;//sets the connection to the connection string which allows it to connect to the database
                        try
                        {
                            SQLString = "INSERT INTO Quests (Acquired, BossDefeat, Completed, PlayerID, QuestID) VALUES (?, ?, ?, ?, ?)";//sql statement               
                            cmd.CommandText = SQLString;//sets the command text to the SQL insert Statement
                            cmd.ExecuteNonQuery();//execute the SQL statement inside of the command text
                        }
                        catch (Exception)
                        {
                            cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());//adds an @ parameter @PlayerID with a value
                            SQLString = "UPDATE Quests SET Acquired=?, BossDefeat=?, Completed=?, PlayerID=? WHERE QuestID=? AND PlayerID=?";//sql statement
                            cmd.CommandText = SQLString;//sets the command text to the SQL insert Statement
                            cmd.ExecuteNonQuery();//execute the SQL statement inside of the command text
                        }
                    }
                    con.Close();//closes the connection to the database
                }
                
            }
        }
        public void SaveSkilltree(Player player)//runs sql statement for inserting the new record for the skilltree table then if it fails it updates the already existing record
        {
            for (int i = 0; i < 12; i++)//for loop so it can make a record for each index of the skill tree struct 
            {
                using(OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();//opens the connection to the 
                    using(OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Parameters.AddWithValue("@Acquired", player.GetNodeAcquired(i));//adds an @ parameter @Acquired with a value
                        cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());//adds an @ parameter @PlayerID with a value
                        cmd.Parameters.AddWithValue("@NodeID", ((player.GetPlayerID() - 1) * 12) + (i + 1));
                        cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());//adds an @ parameter @PlayerID with a value
                        cmd.Connection = con;//sets the connection to the connection string which allows it to connect to the database
                        try
                        {
                            SQLString = "INSERT INTO Skilltree (Acquired, PlayerID, NodeID) VALUES (@Acquired, @PlayerID, @NodeID)";//this is the SQL insert statement
                            cmd.CommandText = SQLString;//sets the command text to the SQL insert statment
                            cmd.ExecuteNonQuery();//executes the sql statement in the command text 
                        }
                        catch (Exception)
                        { 
                            SQLString = "UPDATE Skilltree SET Acquired = ?, PlayerID = ? WHERE NodeID = ? AND PlayerID = ?";//this is the SQL insert statement
                            cmd.CommandText = SQLString;//sets the command text to the SQL insert statment
                            cmd.ExecuteNonQuery();//executes the sql statement in the command text 
                        }
                    }
                    con.Close();//closes the connection to the database
                }
                
            }
        }
        public void SaveItems(Player player)//runs sql statement for inserting the new record for the items table then if it fails it updates the already existing record
        {
            for (int i = 0; i < 3; i++)//for loop which updates the players item records
            {
                using(OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();//opens the connection to the database 
                    using(OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Parameters.AddWithValue("@Amount", player.GetItemAmount(i));
                        cmd.Parameters.AddWithValue("@ItemID", ((player.GetPlayerID() - 1) * 3) + (i + 1));
                        cmd.Parameters.AddWithValue("@PlayerID", player.GetPlayerID());
                        cmd.Connection = con;//sets the connection to the connection string which allows it to connect to the database

                        try
                        {
                            SQLString = "INSERT INTO Items (Amount, ItemID, PlayerID) VALUES(@Amount, @ItemID, @PlayerID)";//sql statement
                            cmd.CommandText = SQLString;//sets the command text to the sql statement
                            cmd.ExecuteNonQuery();//executes the sql statement
                        }
                        catch (Exception)
                        {
                            SQLString = "UPDATE Items SET Amount = ? WHERE ItemID = ? AND PlayerID = ?";//sql statement
                            cmd.CommandText = SQLString;//sets the command text to the sql statement
                            cmd.ExecuteNonQuery();//executes the sql statement
                        }
                    }
                    con.Close();//closes the connection to the database 
                }
            }
        }
    }
}