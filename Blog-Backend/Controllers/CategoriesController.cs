using Blog_Backend.Data;
using Blog_Backend.DTO;
using Blog_Backend.Models;
using Blog_Backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryAddDTO categoryAddDTO)
        {
            var category = new Category
            {
                Name = categoryAddDTO.Name,
                UrlHandle = categoryAddDTO.UrlHandle,
            };
            await _categoryRepository.AddAsync(category);

            var response = new CategoryReadOnlyDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllASync();

            var response = new List<CategoryReadOnlyDTO>();
            foreach (var category in categories)
            {
                response.Add(new CategoryReadOnlyDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetCategoryByIdASync(id);

            if (category is null)
            {
                return NotFound();
            }
            var response = new CategoryReadOnlyDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDTO updateDTO)
        {
            var category = new Category
            {
                Id = id,
                Name = updateDTO.Name,
                UrlHandle = updateDTO.UrlHandle,
            };

            category = await _categoryRepository.UpdateCategoryAsync(category);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryReadOnlyDTO()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };


            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryRepository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryReadOnlyDTO()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }
    }
}
