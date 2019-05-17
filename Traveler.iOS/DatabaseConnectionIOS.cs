using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using Traveler.DAL.DataServices.Database;
using Traveler.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnectionIOS))]
namespace Traveler.iOS
{
    public class DatabaseConnectionIOS : IDatabaseConnection
    {
        public SQLiteAsyncConnection DbConnection()
        {
            var dbName = "TravelerDB.db3";
            string personalFolder = System.Environment.
                GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Librray");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteAsyncConnection(path);
        }
    }
}