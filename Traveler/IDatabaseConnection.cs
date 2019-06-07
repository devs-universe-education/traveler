using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler
{
    public interface IDatabaseConnection
    {
        string GetConnectionString();
    }
}
