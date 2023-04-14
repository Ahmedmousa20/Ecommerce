using AutoMapper;
using Ecommerce.BLL.Interfaces;
using Ecommerce.BLL.Repositories.BaseSpecification;
using Ecommerce.DAL.Entities;
using Ecommerce.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
           
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var spec = new ProductWithCategorySpecifications();
                var products = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(await unitOfWork.ProductRepo.GeAllWithSpecAsync(spec));
                return View(products);
            }
            else
            {
                var products = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(await unitOfWork.ProductRepo.SearchByName(SearchValue));
                return View(products);
            }
        }


        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var spec = new ProductWithCategorySpecifications(id);
            var product = mapper.Map<Product, ProductDto>( await unitOfWork.ProductRepo.GetByIdWithSpecAsync(spec));

            if (product == null)
                return NotFound();

            ViewBag.Categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await unitOfWork.CategoryRepo.GeAllAsync());
            return View(ViewName, product);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = mapper.Map<ProductDto, Product>(productDto);
                    await unitOfWork.ProductRepo.Update(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(productDto);
                }
            }
            return View(productDto);

        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await unitOfWork.CategoryRepo.GeAllAsync());
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            Console.WriteLine(productDto);

            if (ModelState.IsValid) 
            {
                var product = mapper.Map<ProductDto, Product>(productDto);
                await unitOfWork.ProductRepo.Add(product);
                return RedirectToAction("Index");
            }
            return View(productDto);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
                return NotFound();
            try
            {
                var product = mapper.Map<ProductDto, Product>(productDto);
                await unitOfWork.ProductRepo.Delete(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(productDto);
            }
        }
    }
}
