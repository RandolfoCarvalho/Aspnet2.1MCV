using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //pega os registros do banco e retorna isso para view
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() 
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
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
