using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebApplication1.DataAccess
{
    public class UsersRepository: ConnectionClass
    {
        public UsersRepository() : base()
        { }

        public void AddUser(string email, string name, string surname)
        {
            string sql = "INSERT INTO users(\"Email\", \"Name\", \"Surname\", \"Lastloggedin\") VALUES(@email, @name, @surname, @lastloggedin)";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@lastloggedin", DateTime.Now);

            bool connectionOpenedInThisMethod = false;

            if (MyConnection.State == System.Data.ConnectionState.Closed)
            {
                MyConnection.Open();
                connectionOpenedInThisMethod = true;
            }

            cmd.ExecuteNonQuery();

            if (connectionOpenedInThisMethod == true)
            {
                MyConnection.Close();
            }
        }

        public bool DoesEmailExist(string email)
        {
            try
            {
                string sql = "Select Count(*) from users where \"Email\" = @email";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
                cmd.Parameters.AddWithValue("@email", email);


                bool connectionOpenedInThisMethod = false;

                if (MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    MyConnection.Open();
                    connectionOpenedInThisMethod = true;
                }

                bool result = Convert.ToBoolean(cmd.ExecuteScalar());

                if (connectionOpenedInThisMethod == true)
                {
                    MyConnection.Close();
                }

                return result;
            }
            catch (Exception ex){
                return false;
            }
        }

        public int GetId(string email)
        {
            try
            {
                string sql = "Select \"Id\" from users where \"Email\" = @email";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
                cmd.Parameters.AddWithValue("@email", email);

                int id = -1;

                bool connectionOpenedInThisMethod = false;

                if (MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    MyConnection.Open();
                    connectionOpenedInThisMethod = true;
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
                if (connectionOpenedInThisMethod == true)
                {
                    MyConnection.Close();
                }

                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public string GetEmail(int id)
        {
            try
            {
                string sql = "Select \"Email\" from users where \"Id\" = @id";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
                cmd.Parameters.AddWithValue("@id", id);

                string email = "";

                bool connectionOpenedInThisMethod = false;

                if (MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    MyConnection.Open();
                    connectionOpenedInThisMethod = true;
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        email = reader.GetString(0);
                    }
                }
                if (connectionOpenedInThisMethod == true)
                {
                    MyConnection.Close();
                }

                return email;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public void UpdateLastLoggedIn(string email)
        {

            string sql = "update users set \"Lastloggedin\" = @lastloggedin where \"Email\" = @email";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@lastloggedin", DateTime.Now); ;

            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();

        }

    }
}