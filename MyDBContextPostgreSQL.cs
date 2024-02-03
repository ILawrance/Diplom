using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public class MyDBContextPostgreSQL : DbContext
    {
        public MyDBContextPostgreSQL() : base("PostgreSQL.Diplom") 
        {

        }
    }
}
