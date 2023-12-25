using SalesWebMvc.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

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
        public async Task<List<Seller>> FindAllAsync()
        {
            //retorna uma lista de objetos do tipo Seller
            return await _context.Seller.ToListAsync();
        }
        //Inserindo um novo vendedor no Bd
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            //da um join para buscar também o departamento pelo Id
            //isso é chamado de eager loading, carregar outros objetos associados ao obj principal, que no caso o princial 
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task removeAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            //verifica no banco de dados se já existe algum Id desse obj x no banco de dados
            if (!hasAny)
            {
                throw new KeyNotFoundException("Id not Found");
            }
            //um try pois pode dar erro de conflito de concorrencia no update
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();

            } catch(DbUpdateConcurrencyException e)
            {
                //reenviando a exceção em nivel de serviço (a que eu criei na pasta serviços)
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
