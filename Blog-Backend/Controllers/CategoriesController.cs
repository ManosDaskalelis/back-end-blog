using Blog_Backend.Data;
using Blog_Backend.DTO;
using Blog_Backend.Models;
using Blog_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("AddCategory")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory(CategoryAddDTO categoryAddDTO)
        {
            var category = new Category
            {
                Name = categoryAddDTO.Name,
                UrlHandle = categoryAddDTO.UrlHandle,
            };
            category = await _categoryRepository.AddAsync(category);

            var response = new CategoryReadOnlyDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);
        }


        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetAllCategories(string? query,
            string? sortBy,
            string? sortDirection,
            int? pageNumber,
            int? pageSize)
        {
            var categories = await _categoryRepository.GetAllASync(query, sortBy, sortDirection, pageNumber, pageSize);

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

        [HttpGet("GetCategory/{id:Guid}")]
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

        [HttpPut("UpdateCategory/{id:Guid}")]
        [Authorize(Roles = "Writer")]
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

        [HttpDelete("DeleteCategory/{id:Guid}")]
        [Authorize(Roles = "Writer")]
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

        [HttpGet("GetCount")]
        public async Task<IActionResult> GetCategoriesCount()
        {
            var count = await _categoryRepository.GetCountAsync();

            return Ok(count);
        }
    }
}
