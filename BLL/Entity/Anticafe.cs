using System.Collections.Generic;
using System.Linq;

using BLL.Interface;

namespace BLL.Entity
{
    public class Anticafe : IAnticafe
    {
        private readonly double MinPricePerHour;
        private readonly double MaxPricePerHour;
        private readonly int MIN_WORK;
        private readonly int MAX_WORK;

        private List<Restroom> restrooms;


        public int Id { get; private set; }


        private Anticafe()
        {
            MaxPricePerHour = 10000;
            MinPricePerHour = 10;
            MIN_WORK = 0;
            MAX_WORK = 23;

            restrooms = new List<Restroom>();
        }
        public Anticafe(string name, string address) : this()
        {
            Name = name;
            Address = address;
        }


        public string Name { get; set; }
        public string Address { get; private set; }


        public (bool result, IRestroom restroom) AddRestroom(string typeRecreation, double pricePerHour, int workOut, int workUp)
        {
            if (workOut < workUp && workOut >= MIN_WORK && workUp <= MAX_WORK
                && pricePerHour >= MinPricePerHour && pricePerHour <= MaxPricePerHour)
            {
                var restroom = new Restroom(typeRecreation, pricePerHour, workOut, workUp);

                restrooms.Add(restroom);
                return (true, restroom);
            }
            else { return (false, null); }
        }
        public bool RemoveRestroom(Restroom restroom) { return restrooms.Remove(restroom); }
        public bool RemoveRestroom(int restroomId)
        {
            var restroom = restrooms.Where(val => val.Id == restroomId).FirstOrDefault();
            if (restroom != null)
                return restrooms.Remove(restroom);
            else { return false; }
        }


        public IRestroom GetRestroom(int restroomId) { return restrooms.Where(val => val.Id == restroomId).FirstOrDefault(); }


        public ICollection<Restroom> Restrooms
        {
            get
            {
                List<Restroom> rest = new List<Restroom>();

                foreach (var item in restrooms)
                    rest.Add(item);


                return rest;
            }
        }
    }
}
