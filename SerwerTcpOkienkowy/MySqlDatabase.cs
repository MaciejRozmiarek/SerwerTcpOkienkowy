using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SerwerTcpOkienkowy
{
    class MySqlDatabase
    {
        public MySqlConnection connection;
        public bool connectionStatus = false;
        //string connectionString = "SERVER=localhost;DATABASE=mysql;UID=root;PASSWORD=;";

        public bool OpenConnection(string ConnectionString)
        {
            string connectionString = ConnectionString;
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                this.connectionStatus = true;
                return true;

            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.

                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        this.connectionStatus = false;
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        this.connectionStatus = false;
                        break;
                }
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                this.connectionStatus = false;
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                this.connectionStatus = false;
                return false;
            }
        }
        public bool insertToDB(string Query)
        {
            /*
            string query = Query;
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command

                    //dodac try cach dla duplikatow i bledow
                    cmd.ExecuteNonQuery();

                    this.CloseConnection();

                    return true;
                }
                catch (Exception exc)
                {

                    return false;
                }


                //close connection


            }
            else
            {
                return false;
            }
            */
            return false;
        }

        public DataSet getDataSet(string Command)
        {

            string sqlCmd = Command;

            var firstWord = sqlCmd.Substring(0, sqlCmd.IndexOf(" "));


            DataSet DS = new DataSet();
            //DS = null;
            if (sqlCmd == null)
            {
                DataTable dt = new DataTable("MyTable");

                dt.Columns.Add(new DataColumn("Błąd", typeof(string)));

                DataRow dr = dt.NewRow();

                dr["Błąd"] = "Podałeś puste zapytanie!!!";
                dt.Rows.Add(dr);
                DS.Tables.Add(dt);
            }
            else if (firstWord == "SELECT")
            {
                if (connectionStatus == true)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(sqlCmd, connection);
                        cmd.CommandType = CommandType.Text;
                        // MySqlDataReader rdr = cmd.ExecuteReader();

                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlCmd, connection);

                        mySqlDataAdapter.Fill(DS);
                        mySqlDataAdapter.Dispose();
                    }
                    catch (Exception ex)
                    {

                        DataTable dt = new DataTable("MyTable");

                        dt.Columns.Add(new DataColumn("Błąd", typeof(string)));

                        DataRow dr = dt.NewRow();

                        dr["Błąd"] = ex;
                        dt.Rows.Add(dr);
                        DS.Tables.Add(dt);
                    }
                }

            }
            else if (firstWord == "UPDATE")
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, connection);
                    cmd.CommandType = CommandType.Text;
                    // MySqlDataReader rdr = cmd.ExecuteReader();

                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlCmd, connection);
                    mySqlDataAdapter.Fill(DS);
                    mySqlDataAdapter.Dispose();
                    DataTable dt = new DataTable("MyTable");

                    dt.Columns.Add(new DataColumn("Status wykonania", typeof(string)));

                    DataRow dr = dt.NewRow();

                    dr["Status wykonania"] = "WYKONANO UPDATE";
                    dt.Rows.Add(dr);
                    DS.Tables.Add(dt);
                }
                catch (Exception ex)
                {

                    DataTable dt = new DataTable("MyTable");

                    dt.Columns.Add(new DataColumn("Błąd", typeof(string)));

                    DataRow dr = dt.NewRow();

                    dr["Błąd"] = ex;
                    dt.Rows.Add(dr);
                    DS.Tables.Add(dt);
                }
            }
            else if (firstWord == "INSERT")
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, connection);
                    cmd.CommandType = CommandType.Text;
                    // MySqlDataReader rdr = cmd.ExecuteReader();

                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlCmd, connection);
                    mySqlDataAdapter.Fill(DS);
                    mySqlDataAdapter.Dispose();
                    DataTable dt = new DataTable("MyTable");

                    dt.Columns.Add(new DataColumn("Status wykonania", typeof(string)));

                    DataRow dr = dt.NewRow();

                    dr["Status wykonania"] = "WYKONANO INSERT";
                    dt.Rows.Add(dr);
                    DS.Tables.Add(dt);
                }
                catch (Exception ex)
                {

                    DataTable dt = new DataTable("MyTable");

                    dt.Columns.Add(new DataColumn("Błąd", typeof(string)));

                    DataRow dr = dt.NewRow();

                    dr["Błąd"] = ex;
                    dt.Rows.Add(dr);
                    DS.Tables.Add(dt);
                }
            }
            else
            {
                DataTable dt = new DataTable("MyTable");

                dt.Columns.Add(new DataColumn("Status wykonania", typeof(string)));

                DataRow dr = dt.NewRow();

                dr["Status wykonania"] = "NIE MOŻNA WYKONAĆ INNY ZAPYTAŃ NIŻ SELECT, UPDATE ORAZ INSERT.";
                dt.Rows.Add(dr);
                DS.Tables.Add(dt);
            }

            return DS;

        }
        public int checkLogin(string Szukana)
        {
            string szukana = Szukana;
            int Count = -1;
            /*string sqlCmd = "select count(*) from uzytkownicy where login = '" + szukana + "';";

            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(sqlCmd, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
            */
            return Count;

        }
    }
}