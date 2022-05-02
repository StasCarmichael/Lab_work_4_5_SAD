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

            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                Anticafe anticafe1 = new Anticafe("Somerset", "Kiev Chrechatic 32 Б");


                Client client1 = new Client("Stas", "Kyrei", 300);
                Client client2 = new Client("Elic", "Wise", 400);
                Client client3 = new Client("Berni", "Clai", 200);


                var res1 = anticafe1.AddRestroom("Перегляд кіно", 200, 8, 22);
                var res2 = anticafe1.AddRestroom("Настільні ігри", 50, 9, 20);
                var res3 = anticafe1.AddRestroom("Домашня читальня", 10, 10, 20);

                res1.restroom?.ReserveRestroom(client1, DateTime.Now, 8, 20);
                res2.restroom?.ReserveRestroom(client1, new DateTime(2022, 5, 4), 9, 12);
                res2.restroom?.ReserveRestroom(client2, new DateTime(2022, 5, 4), 12, 15);
                res3.restroom?.ReserveRestroom(client2, new DateTime(2022, 5, 8), 10, 20);
                res3.restroom?.ReserveRestroom(client3, new DateTime(2022, 5, 10), 10, 20);
                res1.restroom?.ReserveRestroom(client3, new DateTime(2022, 5, 6), 8, 22);

                context.Clients.AddRange(client1, client2, client3);
                context.Anticafes.AddRange(anticafe1);

                context.SaveChanges();
            }



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

            /*
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
            */

        }
    }
}
