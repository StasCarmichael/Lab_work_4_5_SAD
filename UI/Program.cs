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


                Client client1 = new Client("Stas", "Kyrei", 300);
                Client client2 = new Client("Elic", "Wise");


                Order order1 = new Order(200, DateTime.Now, 8, 10);
                Order order2 = new Order(10, DateTime.Now, 6, 7);
                Order order3 = new Order(1000, DateTime.Now, 10, 15);
                Order order4 = new Order(500, DateTime.Now, 7, 10);
                Order order5 = new Order(30, DateTime.Now, 8, 9);
                Order order6 = new Order(800, DateTime.Now, 8, 13);


                client1.AddOrder(order1);
                client1.AddOrder(order3);
                client1.AddOrder(order4);
                client1.AddOrder(order5);

                client2.AddOrder(order2);
                client2.AddOrder(order4);
                client2.AddOrder(order6);


                context.Clients.AddRange(client1, client2);
                context.Orders.AddRange(order1, order2, order3, order4, order5, order6);

                context.SaveChanges();
            }
            */


            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureCreated();

                var clients = context.Clients.ToList();
                var orders = context.Orders.ToList();


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

        }
    }
}
