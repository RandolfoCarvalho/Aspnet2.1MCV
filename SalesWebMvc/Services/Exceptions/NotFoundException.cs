using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //construtor vai repassar a mensagem para a classe base
        public NotFoundException(string message) : base (message) 
        {
            
        }
    }
}
