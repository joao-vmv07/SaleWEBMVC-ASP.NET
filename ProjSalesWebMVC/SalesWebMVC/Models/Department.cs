using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sallers { get; set; } = new List<Seller>();

        public Department(){}

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public void AddSaller(Seller sr)
        {
            Sallers.Add(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sallers.Sum(p => p.TotalSales(initial, final));
        }
    }
}
