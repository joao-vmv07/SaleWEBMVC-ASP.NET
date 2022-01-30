using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Models.Services.Exceptions;
using System.Diagnostics;
using SalesWebMVC.Models.ViewModels.Services.Exceptions;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerservice;
        private readonly DepartmentService _departmentservice;
       
        public SellersController(SellerService sellerservice, DepartmentService departmentservice)
        {
            _departmentservice = departmentservice;
            _sellerservice = sellerservice;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _sellerservice.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentservice.FindAllAsync();
            var viewModel = new SellerFormViewModel{Departments = departments};

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Metodo que vai add o seller que vem do front
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var deparments = await _departmentservice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = deparments };
                return View(viewModel);
            }
            await _sellerservice.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = await _sellerservice.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(obj);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = await _sellerservice.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try {
                await _sellerservice.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { e.Message });
            }
        }
        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { Message = "Id not found" });
                }
                var obj = await _sellerservice.FindByIdAsync(id.Value);
                if (obj == null)
                {
                    return RedirectToAction(nameof(Error), new { Message = "Id not found" });
                }
                List<Department> departments = await _departmentservice.FindAllAsync();
                SellerFormViewModel view = new SellerFormViewModel { Seller = obj, Departments = departments };
                return View(view);
            } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var deparments = await _departmentservice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = deparments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
            }
            try {
                await _sellerservice.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));

            }catch(NotFoundExceptions e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
            }
        public IActionResult Error(string message)
        {
            var viewmodel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewmodel);
        }

    
    }
}

