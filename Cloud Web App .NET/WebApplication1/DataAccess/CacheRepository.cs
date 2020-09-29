using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using StackExchange.Redis;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class CacheRepository
    {

        private IDatabase db;
        public CacheRepository()
        {
            try
            {
                // var connection = ConnectionMultiplexer.Connect("localhost"); //localhost if cache server is installed locally after downloaded from https://github.com/rgl/redis/downloads 
                // connection to your REDISLABS.com db as in the next line NOTE: DO NOT USE MY CONNECTION BECAUSE I HAVE A LIMIT OF 30MB...CREATE YOUR OWN ACCOUNT
                var connection = ConnectionMultiplexer.Connect("redis-14625.c14.us-east-1-3.ec2.cloud.redislabs.com:14625,password=BS4dUW3VaA10Fijsnj5cbLwKrwGB9GnS"); //<< connection here should be to your redis database from REDISLABS.COM
                db = connection.GetDatabase();
            }
            catch { }
        }

        /// <summary>
        /// store a list of files in cache
        /// </summary>
        /// <param name="files"></param>
        public void UpdateCache(List<File> files, int id)
        {
            if (db.KeyExists("files" + id) == true)
                db.KeyDelete("files" + id);

            string jsonString;
            jsonString = JsonSerializer.Serialize(files); 

            db.StringSet("files" + id, jsonString);
        }
        /// <summary>
        /// Gets a list of files from cache
        /// </summary>
        /// <returns></returns>
        public List<File> GetFilesFromCache(int id)
        {
            if (db.KeyExists("files" + id) == true)
            {
                var files = JsonSerializer.Deserialize<List<File>>(db.StringGet("files" + id));
                return files;
            }
            else
            {
                return new List<File>();
            }
        }
    }
}