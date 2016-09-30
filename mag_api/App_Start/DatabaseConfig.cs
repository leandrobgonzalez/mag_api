using System;
using mag_api.Models;
using ServiceStack.OrmLite;

namespace mag_api
{
    public static class DatabaseConfig
    {
        public static string ConnectionString
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\mag_api\\db.db";
            }
        }

        public static void Configure()
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnectionString, SqliteDialect.Provider);

            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Cliente>();
            }
        }

        
    }
}