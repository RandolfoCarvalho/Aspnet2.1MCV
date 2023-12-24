using SalesWebMvc.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        //Meu Bd no contexto atual
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //consultando todos os vendedores
        public List<Seller> FindAll()
        {
            //retorna uma lista de objetos do tipo Seller
            return _context.Seller.ToList();
        }
        //Inserindo um novo vendedor no Bd
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
