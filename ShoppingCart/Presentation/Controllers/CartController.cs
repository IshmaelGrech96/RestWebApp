using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class CartController : Controller
    {
        private IProductsService _productsService;
        private ICategoryService _categoriesService;
        private ICartProductService _cartProductService;
        private IWebHostEnvironment _env;
        private readonly ILogger<CartController> _logger;

        public CartController(IProductsService productsService, ICategoryService categoriesService, IWebHostEnvironment env, ICartProductService cartProductService, ILogger<CartController> logger)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _cartProductService = cartProductService;
            _logger = logger;
            _env = env;
        }

        [Authorize]
        public IActionResult ViewCart()
        {
            //TO DO: Return USER Cart Products 
            string email = HttpContext.User.Identity.Name;
            try
            {
                var list = _cartProductService.GetCartProducts(email);
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("View cart error: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }          
        }

        [Authorize]
        public IActionResult Checkout()
        {
            string email = HttpContext.User.Identity.Name;
            try
            {
                _cartProductService.Checkout(email);
                TempData["CheckoutSuccess"] = "Checkout Successful";
            }
            catch(Exception ex)
            {
                _logger.LogError("Checkout error: "+ex.Message);
                TempData["CheckoutFail"] = "Checkout Failed!";
                return RedirectToAction("Error","Home");
            }
            return RedirectToAction("ViewCart");
        }
    }
}
