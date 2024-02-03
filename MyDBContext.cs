using System;
using System.Data.Entity;


namespace Diplom
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("Diplom.Properties.Settings.Электронный_учебникConnectionString")
        {

        }
        

    }
}
