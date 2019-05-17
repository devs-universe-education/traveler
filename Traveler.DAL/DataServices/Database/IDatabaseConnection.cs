using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.DataServices.Database
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteAsyncConnection DbConnection();
    }
}
