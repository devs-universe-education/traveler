using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.dbSQLite
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteAsyncConnection DbConnection();
    }
}
