using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

using DAL;
using BLL.Entity;
using BLL.Interface;

using UoW.Repository;
using UoW.UnitWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UI
{
    class Program
    {
        private static string BASE_CONNECTION_STRING = "Data Source=appDataBase.sqlite";


        private static string GetStringConnection()
        {
            try
            {
                // установка пути к текущему каталогу
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
                // получаем конфигурацию из файла appsettings.json  создаем конфигурацию
                var config = builder.AddJsonFile("appsettings.json").Build();
                // получаем строку подключения
                return config.GetConnectionString("DefaultConnection");
            }
            catch (Exception)
            {
                return BASE_CONNECTION_STRING;
            }
        }
        private static void SyncroniceDataAfterDelete(ref UnitOfWork unitOfWork, DbContext context)
        {
            unitOfWork.Save();

            unitOfWork = new UnitOfWork(context);
        }



        private static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            Console.WriteLine("\t\t\t\tDeveloped by STAS KYREI");
            Console.WriteLine("Вас вітає програма для емуляції роботи з сутністю Антикафе !!!");

            Console.WriteLine("1 - Провести комплекну емуляцію");
            Console.WriteLine("2 - Переглянути дані");

            Console.Write("Введіть потрібну клавішу : ");
            var keyCoe = Console.ReadKey().Key;


            Console.WriteLine("\n****************************************************");
            string stringConnection = GetStringConnection();

            switch (keyCoe)
            {
                case (ConsoleKey.D1):
                    {
                        FirstTask(stringConnection);
                        SecondTask(stringConnection);

                        Console.ForegroundColor = ConsoleColor.Green ;
                        Console.WriteLine("\n\n#######################################################\n");
                        Console.ResetColor();

                        ThirtTask(stringConnection);
                        SecondTask(stringConnection);

                        break;
                    }
                case (ConsoleKey.D2):
                    {
                        SecondTask(stringConnection);
                        break;
                    }
                default:
                    { break; }
            }

        }


        private static void FirstTask(string stringConnection)
        {
            using (ApplicationContext context = new ApplicationContext(stringConnection))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                UnitOfWork unitOfWork = new UnitOfWork(context);

                var anticafes = unitOfWork.Anticafes;
                var clients = unitOfWork.Clients;

                Anticafe anticafe1 = new Anticafe("Somerset", "Kiev Chrechatic 32 Б");
                Anticafe anticafe2 = new Anticafe("Somerset", "Odesa Derevacovskaya 22");


                Client client1 = new Client("Stas", "Kyrei", 300);
                Client client2 = new Client("Elic", "Wise", 400);
                Client client3 = new Client("Berni", "Clai", 200);
                Client client4 = new Client("Alfred", "Low", 3000);
                Client client5 = new Client("Anna", "Vercin");


                var res1 = anticafe1.AddRestroom("Перегляд кіно", 200, 8, 22);
                var res2 = anticafe1.AddRestroom("Настільні ігри", 50, 9, 20);
                var res3 = anticafe1.AddRestroom("Домашня читальня", 10, 10, 20);

                var res4 = anticafe2.AddRestroom("Клуб по інтересам", 20, 10, 19);
                var res5 = anticafe2.AddRestroom("Клуб рукотворів", 40, 8, 20);


                res1.restroom?.ReserveRestroom(client1, DateTime.Now, 8, 20);
                res2.restroom?.ReserveRestroom(client1, new DateTime(2022, 5, 4), 9, 12);
                res2.restroom?.ReserveRestroom(client2, new DateTime(2022, 5, 4), 12, 15);
                res3.restroom?.ReserveRestroom(client2, new DateTime(2022, 5, 8), 10, 20);
                res3.restroom?.ReserveRestroom(client3, new DateTime(2022, 5, 10), 10, 20);
                res1.restroom?.ReserveRestroom(client3, new DateTime(2022, 5, 6), 8, 22);
                res1.restroom?.ReserveSpecialProgramRestroom(client2, "День народження", new DateTime(2022, 5, 7));


                res4.restroom?.ReserveSpecialProgramRestroom(client5, "Корпоративне свято", new DateTime(2022, 5, 10));
                res5.restroom?.ReserveRestroom(client1, new DateTime(2022, 5, 4), 9, 12);
                res5.restroom?.ReserveRestroom(client5, new DateTime(2022, 5, 13), 14, 20);
                res3.restroom?.ReserveRestroom(client5, new DateTime(2022, 5, 6), 12, 14);
                res4.restroom?.ReserveRestroom(client4, new DateTime(2022, 5, 8), 10, 15);
                res4.restroom?.ReserveRestroom(client3, new DateTime(2022, 5, 8), 15, 18);
                res5.restroom?.ReserveSpecialProgramRestroom(client4, "Відне заннятя для всіх бажаючих", new DateTime(2022, 5, 12));


                clients.Add(client1, client2, client3, client4, client5);
                anticafes.Add(anticafe1, anticafe2);

                unitOfWork.Save();
            }
        }
        private static void SecondTask(string stringConnection)
        {
            using (ApplicationContext context = new ApplicationContext(stringConnection))
            {
                UnitOfWork unitOfWork = new UnitOfWork(context);

                var data = GetData(unitOfWork);

                PrintAnricafes(data.anticafes);
            }
        }
        private static void ThirtTask(string stringConnection)
        {
            using (ApplicationContext context = new ApplicationContext(stringConnection))
            {
                UnitOfWork unitOfWork = new UnitOfWork(context);

                var data = GetData(unitOfWork);

                var anticafe1 = unitOfWork.Anticafes.Get(1);
                anticafe1.RemoveRestroom(anticafe1.Restrooms.FirstOrDefault());


                SyncroniceDataAfterDelete(ref unitOfWork , context);
                data = GetData(unitOfWork);


                anticafe1 = unitOfWork.Anticafes.Get(2);
                anticafe1.RemoveRestroom(anticafe1.Restrooms.FirstOrDefault());


                SyncroniceDataAfterDelete(ref unitOfWork, context);
                data = GetData(unitOfWork);


                anticafe1 = unitOfWork.Anticafes.Get(1);
                anticafe1.Restrooms.FirstOrDefault().UnreserveRestroom(anticafe1.Restrooms.FirstOrDefault().Orders.FirstOrDefault().Id);

                SyncroniceDataAfterDelete(ref unitOfWork, context);
                data = GetData(unitOfWork);

                unitOfWork.Save();
            }
        }



        private static (List<Anticafe> anticafes, List<Restroom> restrooms, List<Order> orders, List<Client> clients1) GetData(UnitOfWork unitOfWork)
        {
            return (unitOfWork.Anticafes.Get().ToList(), unitOfWork.Restrooms.Get().ToList(),
                unitOfWork.Orders.Get().ToList(), unitOfWork.Clients.Get().ToList());
        }

        private static void PrintAnricafes(List<Anticafe> anticafes)
        {
            string anticafePass = "//////////////////////////////////////////////";
            string restroomPass = "++++++++++++++++++++++++++++++++++++++++++++++";
            string orderPass = "==============================================";


            foreach (var anticafe in anticafes)
            {
                Console.WriteLine("\n" + anticafePass);
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
                        Console.WriteLine("\t\tType Recreation = " + order.TypeRecreation);
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
