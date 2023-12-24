using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        //Id interno da requisição
        public string RequestId { get; set; }
        public string Message { get; set; }

        //testa se tal id existe
        //se ele nao for nulo ou vazio retorna
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}