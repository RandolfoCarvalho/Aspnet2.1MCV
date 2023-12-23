using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //usamos o ICollection aqui por que é uma coleção genérica e pode aceitar List, Hash etc...
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        public Department() 
        {

        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            //soma o total de sales de cada vendedor que esta contido em Sellers, do departamento em questão
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }

    }
    
}
