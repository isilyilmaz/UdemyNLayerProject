using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.Controllers;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.API.Mapping;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using Xunit;

namespace UdemyNLayerProject.Test
{
    public class CategoriesApiControllerTest
    {
        //Fake methods
        private readonly Mock<ICategoryService> _mockService;

        //Test Definitions
        private readonly CategoriesController _categoriesController;

        //Data Example
        private IMapper _mapper;
        private List<Category> _categories;

        public CategoriesApiControllerTest()
        {
            //create mapper definition from profile
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());
            _mapper = config.CreateMapper();

            //create mock service method as fake service
            _mockService = new Mock<ICategoryService>();

            //create definition for test
            _categoriesController = new CategoriesController(_mockService.Object, _mapper);

            //Data Examples
            _categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Saglik Urunleri",
                    IsDeleted = false,
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Id=1,
                            CategoryId = 1,
                            Name = "Diş Macunu",
                            Stock = 10,
                            Price = (decimal)20.2,
                            IsDeleted = false
                        }
                    }
                },
                new Category()
                {
                    Id = 2,
                    Name = "Yiyecekler",
                    IsDeleted = false,
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Id=1,
                            CategoryId = 2,
                            Name = "Tahin Helvası",
                            Stock = 8,
                            Price = (decimal)16.2,
                            IsDeleted = false
                        }
                    }
                }
            };
            
            
        }

        [Fact]
        public async void GetAll_CallServiceMethod_ReturnOkResultWithCategories()
        {            
            //Setup parameters for behaviour
            _mockService.Setup(x => x.GetAllAsync()).ReturnsAsync(_categories);

            //Call the method for test
            var result = await _categoriesController.GetAll();

            //Control results

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnCategories = Assert.IsAssignableFrom<IEnumerable<CategoryDto>>(okResult.Value);

            Assert.Equal<int>(2, returnCategories.ToList().Count);

        }

        [Theory]
        [InlineData(0)]
        public async void GetById_IdIsInvalid_ReturnResult(int id)
        {
            Category category = null;

            //Setup parameters for behaviour
            _mockService.Setup(x => x.GetByIdAsync(0)).ReturnsAsync(category);

            //Call the method for test
            var result = await _categoriesController.GetById(id);

            //Control results
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetById_IdIsValid_ReturnResult(int id)
        {
            var category = _categories.First(i => i.Id == id);

            //Setup parameters for behaviour
            _mockService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(category);

            //Call the method for test
            var result = await _categoriesController.GetById(id);

            //Control results
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnCategory = Assert.IsAssignableFrom<CategoryDto>(okResult.Value);

            Assert.Equal(category.Id, returnCategory.Id);

            Assert.Equal(category.Name, returnCategory.Name);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetWithProductsById_IdIsValid_ReturnResult(int id)
        {
            var category = _categories.First(i => i.Id == id);

            //Setup parameters for behaviour
            _mockService.Setup(x => x.GetWithProductsByIdAsync(id)).ReturnsAsync(category);

            //Call the method for test
            var result = await _categoriesController.GetWithProductsById(id);

            //Control results
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnCategory = Assert.IsAssignableFrom<CategoryWithProductDto>(okResult.Value);

            Assert.Equal(category.Id, returnCategory.Id);

            Assert.Equal(category.Name, returnCategory.Name);
            
            Assert.IsAssignableFrom <ICollection<ProductDto>>(returnCategory.Products);

            Assert.Equal(category.Products.Count, returnCategory.Products.Count);

        }

    }
}
