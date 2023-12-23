using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() 
        {
            return View();
        }
        //notacao para indicar que é um método post
        [HttpPost]
        //impedir ataques csrf
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            //chama o metodo do SellerService
            _sellerService.Insert(seller);
            //redireciona para o index
            return RedirectToAction(nameof(Index));
        }
    }
}
