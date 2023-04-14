using AutoMapper;
using Ecommerce.BLL.Interfaces;
using Ecommerce.DAL.Entities;
using Ecommerce.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await unitOfWork.CategoryRepo.GeAllAsync());
                return View(Categories);
            }
            else
            {
                var Categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await unitOfWork.CategoryRepo.SearchByName(SearchValue));
                return View(Categories);
            }
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var category = mapper.Map<Category, CategoryDto>(await unitOfWork.CategoryRepo.GetByIdAsync(id));

            if (category == null)
                return NotFound();

            ViewBag.Categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await unitOfWork.CategoryRepo.GeAllAsync());
            return View(ViewName, category);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var category = mapper.Map<CategoryDto, Category>(categoryDto);
                    await unitOfWork.CategoryRepo.Update(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(categoryDto);
                }
            }
            return View(categoryDto);

        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await unitOfWork.CategoryRepo.GeAllAsync());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            Console.WriteLine(categoryDto);

            if (ModelState.IsValid) 
            {
                var category = mapper.Map<CategoryDto, Category>(categoryDto);
                await unitOfWork.CategoryRepo.Add(category);
                return RedirectToAction("Index");
            }
            return View(categoryDto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return NotFound();
            try
            {
                var category = mapper.Map<CategoryDto, Category>(categoryDto);
                await unitOfWork.CategoryRepo.Delete(category);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(categoryDto);
            }
        }
    }
}
