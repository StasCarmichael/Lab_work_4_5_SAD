using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class ApplicationContext : DbContext
    {
        private string connectionString;


        public ApplicationContext()
        {
            connectionString = "Data Source=appDataBase.sqlite";

            Database.EnsureCreated();
        }
        public ApplicationContext(string connectionString)
        {
            this.connectionString = connectionString;

            Database.EnsureCreated();
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
