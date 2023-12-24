using SalesWebMvc.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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
            //da um join para buscar também o departamento pelo Id
            //isso é chamado de eager loading, carregar outros objetos associados ao obj principal, que no caso o princial 
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        public void remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            //verifica no banco de dados se já existe algum Id desse obj x no banco de dados
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new KeyNotFoundException("Id not Found");
            }
            //um try pois pode dar erro de conflito de concorrencia no update
            try
            {
                _context.Update(obj);
                _context.SaveChanges();

            } catch(DbUpdateConcurrencyException e)
            {
                //reenviando a exceção em nivel de serviço (a que eu criei na pasta serviços)
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
