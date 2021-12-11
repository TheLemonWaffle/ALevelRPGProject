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
    class NewDatabase
    {
        protected string SQLString;//variable accessable to all the database classes which the sql statement is stored to 
        protected const string connectionString="Provider = Microsoft.JET.OLEDB.4.0;" + "Data Source =" + "RPGDatabase.accdb";//this stores the connection string which has the filename for the database file 
        public int AutoSlotID()//returns the maximum playerid increased by 1 from the slot table
        {
            int autoNumber = 0;
            SQLString = "SELECT MAX(SlotNum) FROM Slots";            
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
        public int AutoSlotNumber()//returns the maximum SlotNum increased by 1
        {
            
            int autoNumber = 0;
            string SaveData = "SELECT MAX(SlotNum) FROM Slots";
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                using(OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = SaveData;

                    using (OleDbDataReader dbReader = cmd.ExecuteReader())
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
    }
}
