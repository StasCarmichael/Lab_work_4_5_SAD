using System;
using System.Collections.Generic;
using System.Linq;

using DAL;
using BLL.Entity;
using BLL.Interface;

using UoW.Repository;
using UoW.UnitWork;

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

                UnitOfWork unitOfWork = new UnitOfWork(context);

                var anticafes = unitOfWork.Anticafes;
                var clients = unitOfWork.Clients;

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

                clients.Add(client1, client2, client3);
                anticafes.Add(anticafe1);

                unitOfWork.Save();
            }
            


            
            using (ApplicationContext context = new ApplicationContext())
            {
                UnitOfWork unitOfWork = new UnitOfWork(context);

                var anticafes = unitOfWork.Anticafes.Get().ToList();
                var restrooms = unitOfWork.Restrooms.Get().ToList();
                var orders = unitOfWork.Orders.Get().ToList();
                var clients = unitOfWork.Clients.Get().ToList();


                PrintAnricafes(anticafes);

            }


        }



        static void PrintAnricafes(List<Anticafe> anticafes)
        {
            string anticafePass = "///////////////////////////////////////";
            string restroomPass = "+++++++++++++++++++++++++++++++++++++++";
            string orderPass = "========================================";


            foreach (var anticafe in anticafes)
            {
                Console.WriteLine("Anticafe Id = " + anticafe.Id);
                Console.WriteLine("Name = " + anticafe.Name);
                Console.WriteLine("Address = " + anticafe.Address);

                Console.WriteLine(anticafePass);
                foreach (var restroom in anticafe.Restrooms)
                {
                    Console.WriteLine("\tRestroom Id = " + restroom.Id);
                    Console.WriteLine("\tType of room = " + restroom.TypeRecreation);
                    Console.WriteLine("\tPrice per hour = " + restroom.PricePerHour);

                    Console.WriteLine(restroomPass);
                    foreach (var order in restroom.Orders)
                    {
                        Console.WriteLine(orderPass);

                        Console.WriteLine("\t\tOrder Id = " + order.Id);
                        Console.WriteLine("\t\tClient Id = " + order.Client.Id);
                        Console.WriteLine("\t\tName = " + order.Client.Name + "  Surname = " + order.Client.Surname);
                        Console.WriteLine("\t\tDate = " + order.GetTimeInterval().Date.ToShortDateString());
                        Console.WriteLine("\t\tSinceWhen = " + order.SinceWhen + "\t ToWhen = " + order.ToWhen);

                        Console.WriteLine(orderPass);
                    }
                    Console.WriteLine(restroomPass);
                }
                Console.WriteLine(anticafePass);
            }
        }
    }
}
