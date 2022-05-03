using System;
using System.Linq;
using System.Collections.Generic;

using Xunit;

using DAL;
using BLL.Entity;
using BLL.Interface;
using UoW.UnitWork;

using Microsoft.EntityFrameworkCore;


namespace BLL.Test
{
    public class UnitTest
    {
        [Fact]
        public void TestAddedEntityInitDb()
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
                res3.restroom?.ReserveRestroom(client3, new DateTime(2022, 5, 10), 10, 20);
                res1.restroom?.ReserveRestroom(client3, new DateTime(2022, 5, 6), 8, 22);
                res1.restroom?.ReserveSpecialProgramRestroom(client3, "JUST CHILL", new DateTime(2022, 5, 7));

                clients.Add(client1, client2, client3);
                anticafes.Add(anticafe1);

                unitOfWork.Save();

                Assert.Equal(unitOfWork.Clients.Get().ToList().Where(val => val.Id == 1).FirstOrDefault().Name, client1.Name);
            }
        }

        [Fact]
        public void TestReadDBbyEF()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                TestAddedEntityInitDb();

                UnitOfWork unitOfWork = new UnitOfWork(context);

                var anticafes = unitOfWork.Anticafes.Get().ToList();
                var restrooms = unitOfWork.Restrooms.Get().ToList();
                var orders = unitOfWork.Orders.Get().ToList();
                var clients = unitOfWork.Clients.Get().ToList();

                restrooms.FirstOrDefault().IsReserve(new DateTime(2022, 5, 7));

                GetInfoMessage(anticafes);

                unitOfWork.Dispose(true);

                Assert.True(true);
            }
        }

        [Fact]
        public void TestLogicInDB()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                TestAddedEntityInitDb();

                UnitOfWork unitOfWork = new UnitOfWork(context);

                var anticafes = unitOfWork.Anticafes;
                var restrooms = unitOfWork.Restrooms;
                var orders = unitOfWork.Orders;
                var clients = unitOfWork.Clients;

                var res = true;
                res = res || anticafes.Get().First().RemoveRestroom(1);

                unitOfWork.Save();

                anticafes = unitOfWork.Anticafes;
                restrooms = unitOfWork.Restrooms;
                orders = unitOfWork.Orders;
                clients = unitOfWork.Clients;

                res = res || restrooms.Get().FirstOrDefault().UnreserveRestroom(restrooms.Get().FirstOrDefault().Orders.FirstOrDefault());
                unitOfWork.Save();

                anticafes = unitOfWork.Anticafes;
                restrooms = unitOfWork.Restrooms;
                orders = unitOfWork.Orders;
                clients = unitOfWork.Clients;

                var ans = restrooms.Get().FirstOrDefault().ReserveRestroom(clients.Get().FirstOrDefault(), new DateTime(2022, 5, 7), 11, 13);

                unitOfWork.Save();

                anticafes = unitOfWork.Anticafes;
                restrooms = unitOfWork.Restrooms;
                orders = unitOfWork.Orders;
                clients = unitOfWork.Clients;

                clients.Update(clients.Get().FirstOrDefault());
                clients.Count();

                restrooms.Get().FirstOrDefault().UnreserveRestroom(ans.order.Id);

                Assert.True(res);
            }
        }

        [Fact]
        public void TestCustomDB()
        {
            using (ApplicationContext context = new ApplicationContext("Data Source=appDataBase.sqlite"))
            {
                UnitOfWork unitOfWork = new UnitOfWork(context);

                Assert.True(true);
            }
        }

        [Fact]
        public void TestAnticafeFunstional()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                TestAddedEntityInitDb();

                UnitOfWork unitOfWork = new UnitOfWork(context);

                var anticafes = unitOfWork.Anticafes;
                var restrooms = unitOfWork.Restrooms;
                var orders = unitOfWork.Orders;
                var clients = unitOfWork.Clients;

                anticafes.Count();
                anticafes.Get().First().RemoveRestroom(anticafes.Get().FirstOrDefault().Restrooms.FirstOrDefault());
                anticafes.Get().First().RemoveRestroom(2);

                var res1 = restrooms.Get().FirstOrDefault().Anticafe;
                var res2 = restrooms.Get().FirstOrDefault().AnticafeId;

                unitOfWork.Save();

                unitOfWork = new UnitOfWork(context);

                var dataanticafes = unitOfWork.Anticafes.Get().ToList();
                var datarestrooms = unitOfWork.Restrooms.Get().ToList();
                var dataorders = unitOfWork.Orders.Get().ToList();
                var dataclients = unitOfWork.Clients.Get().ToList();

                anticafes = unitOfWork.Anticafes;
                restrooms = unitOfWork.Restrooms;
                orders = unitOfWork.Orders;
                clients = unitOfWork.Clients;

                restrooms.Get().FirstOrDefault().UnreserveRestroom(restrooms.Get().FirstOrDefault().Orders.FirstOrDefault());

                anticafes.GetByName("IMAS");
                clients.GetByName("Stas");

                orders.Delete(1);
                orders.Delete(val => val.Day == 5);

                unitOfWork.Save();

                Assert.True(true);
            }
        }

        [Fact]
        public void TestClientFunstional()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                TestAddedEntityInitDb();

                UnitOfWork unitOfWork = new UnitOfWork(context);

                var anticafes = unitOfWork.Anticafes;
                var restrooms = unitOfWork.Restrooms;
                var orders = unitOfWork.Orders;
                var clients = unitOfWork.Clients;

                var res1 = clients.Get(1).Surname;
                var res2 = clients.Get(1).AmountOfMoney;
                clients.Get(1).PutMoney(100);
                clients.Get(1).WithdrawMoney(100);

                var res3 = orders.Get(1).ClientId;
                var res4 = orders.Get(1).Client;
                var res5 = orders.Get(1).Restroom;
                var res6 = orders.Get(1).RestroomId;

                var res7 = restrooms.Get(1).GetOrder(restrooms.Get(1).Orders.FirstOrDefault().Id);

                unitOfWork.Save();
                unitOfWork.Dispose(true);
                unitOfWork.Dispose(true);
                unitOfWork.Dispose();

                Assert.True(true);
            }
        }



        private static void GetInfoMessage(List<Anticafe> anticafes)
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
