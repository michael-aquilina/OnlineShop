using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data
{
    public static class DataShared
    {
        public static SQLiteConnection GetConnection(){
            return new SQLiteConnection(@"Data Source=DB\onlineshopdb.db");
        }
    }
}
