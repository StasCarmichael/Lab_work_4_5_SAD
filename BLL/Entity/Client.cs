using System;
using System.Linq;
using System.Collections.Generic;

using BLL.Interface;

namespace BLL.Entity
{
    public class Client : IClient
    {
        private double amountOfMoney;


        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;

            Orders = new LinkedList<Order>();
        }
        public Client(string name, string surname, double amountOfMoney) : this(name , surname)
        {
            this.amountOfMoney = amountOfMoney;
        }


        public int Id { get; private set; }

        public string Name { get; set; }
        public string Surname { get; set; }


        #region Account

        public double AmountOfMoney
        {
            get
            {
                return amountOfMoney;
            }
            private set
            {
                amountOfMoney = value;
            }
        }
        public void PutMoney(double sum)
        {
            amountOfMoney += sum;
        }
        public bool WithdrawMoney(double sum)
        {
            if (amountOfMoney < sum)
            {
                return false;
            }
            else
            {
                amountOfMoney -= sum;
                return true;
            }
        }

        #endregion


        public void AddOrder(Order order) { Orders.Add(order); }
        public bool RemoveOrder(Order order) { return Orders.Remove(order); }
        public bool RemoveOrder(int orderId)
        {
            var res = Orders.Where(val => val.Id == orderId).FirstOrDefault();
            if (res != null)
                return Orders.Remove(res);
            else
                return false;
        }

        public ICollection<Order> Orders { get; private set; }
    }
}
