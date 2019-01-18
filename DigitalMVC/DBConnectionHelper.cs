using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;


/* Example Usage
 * 
 *DBConnectionHelper dbcon = new DBConnectionHelper("digitaloceanmvc");
            dbcon.IsConnect();
            //dbcon.InsertRecord();
            foreach(string thing in dbcon.GetAllValues())
            {
                Console.WriteLine(thing);
            }
            dbcon.Close(); 
 * 
 */
namespace DigitalMVC
{
    class DBConnectionHelper
    {


        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;


        public MySqlConnection Connection
        {
            get { return connection; }
        }


        public DBConnectionHelper(string dbName)
        {
            databaseName = dbName;
        }
     

        public bool IsConnect()
        {
            if (Connection == null)
            {

                string connstring = string.Format("Server=127.0.0.1; database=digitaloceanmvc; UID=Chewy; password=Marksmen12#1206704!2");
                connection = new MySqlConnection(connstring);
                try
                {
                    connection.Open();
                    Console.WriteLine("Opened database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex);
                }

            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }

        public IEnumerable<string> GetAllValues()
        {
            List<string> valuesList = new List<string>();

            string query = "SELECT * FROM user";

            //TODO: Test if mysql connection is open
            MySqlCommand myCommand = new MySqlCommand(query, connection);

            MySqlDataReader MyDataReader;

            try
            {
                MyDataReader = myCommand.ExecuteReader();
                while (MyDataReader.Read())
                {
                    //General idea: MyDataReader at [0] string int etc...

                    valuesList.Add(MyDataReader.GetString(0));
                    valuesList.Add(MyDataReader.GetString(1));

                }
                MyDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception reading from db : " + ex);
            }


            return valuesList;


        }

        public void InsertRecord()
        {
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = "INSERT INTO testtable(id,name) VALUES(?id, ?name)";
            comm.Parameters.AddWithValue("id", "2");
            comm.Parameters.AddWithValue("name", "jeremy Farmer");
            try
            {
                comm.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception inserting record: " + ex);
            }

            
        }

        public async Task<string> InsertUser(string username, string base64Password, string email, string base64Salt)
        {
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = "INSERT INTO user(id,username,password,email,salt) VALUES(?id, ?username, ?password, ?email, ?salt)";
            comm.Parameters.AddWithValue("id", null); // Passing null to activate the auto_increment on the ID

            comm.Parameters.AddWithValue("username", username);
            comm.Parameters.AddWithValue("password", base64Password);
            comm.Parameters.AddWithValue("email", email);
            comm.Parameters.AddWithValue("salt", base64Salt);
            try
            {
                int x = await comm.ExecuteNonQueryAsync();
                return x.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception inserting record: " + ex);
                return ex.Message;
            }
        }

        //0 not successful 1 is successful
        public async Task<string> ConfirmPassword(string username, string base64Password)
        {
            MySqlCommand comm = connection.CreateCommand();

            DbDataReader reader = await comm.ExecuteReaderAsync();

            while (reader.Read())
            {
                if(reader["password"].ToString() == base64Password)
                {
                    return reader["username"].ToString();
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<PasswordData> GetPasswordData(string username)
        {
       
            MySqlCommand comm = connection.CreateCommand();
            //SELECT salt, password FROM digitaloceanmvc.user WHERE username LIKE "Domgrar";
            comm.CommandText = "SELECT salt, password FROM user WHERE username LIKE " +"\""+ username + "\";";
            DbDataReader reader = await comm.ExecuteReaderAsync();

            while (reader.Read())
            {
                PasswordData passData = new PasswordData(reader["salt"].ToString(), reader["password"].ToString());
                return passData;
            }

            return null;

           
        }


        public async Task<string> GetEmail(string username)
        {

            MySqlCommand comm = connection.CreateCommand();
            //SELECT salt, password FROM digitaloceanmvc.user WHERE username LIKE "Domgrar";
            comm.CommandText = "SELECT email FROM user WHERE username LIKE " + "\"" + username + "\";";
            DbDataReader reader = await comm.ExecuteReaderAsync();

            while (reader.Read())
            {
               // PasswordData passData = new PasswordData(reader["salt"].ToString(), reader["password"].ToString());
                return reader["email"].ToString();
            }

            return null;


        }
    }
}
