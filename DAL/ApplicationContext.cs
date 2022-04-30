﻿using System;
using System.Collections.Generic;
using System.Linq;

using BLL.Entity;

using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        private string connectionString;

        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }


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
