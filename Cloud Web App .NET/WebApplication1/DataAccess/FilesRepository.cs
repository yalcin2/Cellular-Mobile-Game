using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class FilesRepository: ConnectionClass
    {
        public FilesRepository() : base() { }

        public void AddFile(File p)
        {
            string sql = "INSERT INTO files(\"Name\",\"Link\",\"Owner\",\"Recipient\") VALUES(" + "'" + p.Name + "','" + p.Link + "'," + p.Owner + ",'" + p.Recipient + "')";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);

            bool connectionOpenedInThisMethod = false;

            if (MyConnection.State == System.Data.ConnectionState.Closed)
            {
                MyConnection.Open();
                connectionOpenedInThisMethod = true;
            }

            if (MyTransaction != null)
            {
                cmd.Transaction = MyTransaction; 
            }
            cmd.ExecuteNonQuery();

            if (connectionOpenedInThisMethod == true)
            {
                MyConnection.Close();
            }
            
            new LogsRepository().WriteLogEntry("Saved");
        }

        public List<File> GetAllFiles()
        {
            string sql = "Select \"Id\", \"Name\", \"Link\", \"Owner\", \"Recipient\" from files";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
            

            MyConnection.Open();
            List<File> results = new List<File>();

            using (var reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    File p = new File();
                    p.Id = reader.GetInt32(0);
                    p.Name = reader.GetString(1);
                    p.Link = KeyRepository.Decrypt(reader.GetString(2));
                    p.Owner = reader.GetInt32(3);
                    p.Recipient = KeyRepository.Decrypt(reader.GetString(4));
                    results.Add(p);
                }
            }

            MyConnection.Close();

            return results;
        }

        public List<File> GetFiles(int id)
        {
            string sql = "Select \"Id\", \"Name\", \"Link\", \"Owner\", \"Recipient\" from files where \"Owner\"=@id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
            cmd.Parameters.AddWithValue("@id", id);

            MyConnection.Open();
            List<File> results = new List<File>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    File p = new File();
                    p.Id = reader.GetInt32(0);
                    p.Name = reader.GetString(1);
                    p.Link = KeyRepository.Decrypt(reader.GetString(2));
                    p.Owner = reader.GetInt32(3);
                    p.Recipient = KeyRepository.Decrypt(reader.GetString(4));
                    results.Add(p);
                }
            }

            MyConnection.Close();

            return results;
        }

        public void DeleteFile(int id)
        {
            string sql = "Delete from files where Id = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, MyConnection);
            cmd.Parameters.AddWithValue("@id", id);

            bool connectionOpenedInThisMethod = false;

            if (MyConnection.State == System.Data.ConnectionState.Closed)
            {
                MyConnection.Open();
                connectionOpenedInThisMethod = true;
            }

            if(MyTransaction != null)
            {
                cmd.Transaction = MyTransaction; //to participate in the opened trasaction (somewhere else), assign the Transaction property to the opened transaction
            }
            cmd.ExecuteNonQuery();

            if (connectionOpenedInThisMethod == true)
            {
                MyConnection.Close();
            }
        }
    }
}