using System;
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
        }
        public Client(string name, string surname, double amountOfMoney)
        {
            Name = name;
            Surname = surname;

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

    }
}
