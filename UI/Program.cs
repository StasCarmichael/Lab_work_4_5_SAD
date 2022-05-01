using System;
using System.Collections.Generic;
using System.Linq;

using DAL;
using BLL.Entity;
using BLL.Interface;

using Microsoft.EntityFrameworkCore;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                Restroom restroom1 = new Restroom("Перегляд кіно", 100, 8, 22);

                Client client1 = new Client("Stas", "Kyrei", 300);
                Client client2 = new Client("Elic", "Wise");

                Console.WriteLine(restroom1.ReserveRestroom(client1, DateTime.Now, 8, 10));
                Console.WriteLine(restroom1.ReserveRestroom(client2, DateTime.Now, 10, 11));
                restroom1.ReserveRestroom(client1, DateTime.Now, 13, 18);
                restroom1.ReserveRestroom(client2, DateTime.Now, 19, 20);


                context.Orders.AddRange(restroom1.Orders.ToArray());
                context.Clients.AddRange(client1, client2);
                context.Restrooms.AddRange(restroom1);

                context.SaveChanges();
            }
            */


            /*
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureCreated();

                var restroom = context.Restrooms.ToList();
                var clients = context.Clients.ToList();
                var orders = context.Orders.ToList();

                restroom.First().UnreserveRestroom(3);

                
                foreach (var item in clients)
                {
                    Console.WriteLine("Name = " + item.Name + "  Surname = " + item.Surname);
                    foreach (var order in item.Orders)
                    {
                        Console.WriteLine("Id = " + order.Id);
                    }
                    Console.WriteLine("========================");
                }
                

                context.SaveChanges();
            }
            */


            /*
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureCreated();

                var restrooms = context.Restrooms.ToList();
                var clients = context.Clients.ToList();
                var orders = context.Orders.ToList();

                var restroom1 = new Restroom("Настільні ігри", 20, 10, 20);
                var client1 = clients.Where(val => val.Id == 1).FirstOrDefault();
                restroom1.ReserveRestroom(client1, new DateTime(2022, 05, 12), 10, 18);
                restroom1.ReserveRestroom(client1, new DateTime(2022, 05, 12), 16, 20);

                context.Restrooms.Add(restroom1);
                
                context.SaveChanges();
            }
            */


            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureCreated();

                var restrooms = context.Restrooms.ToList();
                var clients = context.Clients.ToList();
                var orders = context.Orders.ToList();

                var restroom1 = restrooms.Where(val => val.Id == 2).FirstOrDefault();
                var client1 = clients.Where(val => val.Id == 2).FirstOrDefault();
                restroom1.ReserveRestroom(client1, new DateTime(2022, 05, 2), 10, 13);

                context.SaveChanges();
            }


        }
    }
}
