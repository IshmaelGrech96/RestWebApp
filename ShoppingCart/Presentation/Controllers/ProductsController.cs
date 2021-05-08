using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private ICategoryService _categoriesService;
        private IWebHostEnvironment _env;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger,IProductsService productsService, ICategoryService categoriesService,IWebHostEnvironment env)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _logger = logger;
            _env = env;
        }

        public async Task<IActionResult> Index(string searchString, int pagenumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;

            try
            {
                var list = _productsService.GetProducts();
                var catList = _categoriesService.GetCategories();
                ViewBag.Categories = catList;

                if (!String.IsNullOrEmpty(searchString))
                {
                    list = _productsService.GetProductsByCateg(searchString);
                }
                return View(await PaginatedList<ProductViewModel>.CreateAsync(list, pagenumber, 10));
            } catch (Exception ex)
            {
                _logger.LogError("Display error: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }

            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            try
            {
                var catList = _categoriesService.GetCategories();
                ViewBag.Categories = catList;
            }
            catch(Exception ex)
            {
                _logger.LogError("Add error: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
            
            return View();
        }

        public IActionResult Details(Guid id)
        {
            try
            {
                var myProduct = _productsService.GetProduct(id);
                return View(myProduct);
            }
            catch(Exception ex)
            {
                _logger.LogError("View details error: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
            
        }

        public IActionResult MyOrders()
        {
            return View();

        }

        public IActionResult SingleMealPlan()
        {
            return View();

        }

        public IActionResult WeeklyMealPlan()
        {
            return View();

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(ProductViewModel data, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        string newFileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        string absolutePath = _env.WebRootPath + @"\Images\";

                        using (var stream = System.IO.File.Create(absolutePath + newFileName))
                        {
                            file.CopyTo(stream);
                        }

                        data.ImageUrl = @"\Images\" + newFileName;

                    }
                }
                _productsService.AddProduct(data);
                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                //log errors
                ViewData["warning"] = "Product was not added! Check your details";
                _logger.LogError("Adding Product error: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }

            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();

        }
        [Authorize]
        public IActionResult AddToCart(Guid id,int qty)
        {
            string email = HttpContext.User.Identity.Name;
            try
            {
                if(!_productsService.CheckStock(id,qty))
                {
                    TempData["AddToCartFail"] = "Invalid Quantity";
                    return RedirectToAction("Index");
                }
                _productsService.AddToCart(id, email,qty);
                TempData["AddToCartSuccess"] = "Successfully Added To Cart";
            } catch (Exception ex)
            {
                TempData["AddToCartFail"] = "Product was not added! Check your details";
                _logger.LogError("Add to cart error: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Hide(Guid id)
        {
            _productsService.Hide(id);
            TempData["Hide"] = "Product hidden";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Show(Guid id)
        {
            _productsService.Show(id);
            TempData["Show"] = "Product available";
            return RedirectToAction("Index");
        }


    }
}