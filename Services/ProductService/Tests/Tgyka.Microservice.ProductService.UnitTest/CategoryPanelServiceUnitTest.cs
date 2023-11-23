using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Data.Enum;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;
using Tgyka.Microservice.ProductService.Services.Implementations;

namespace Tgyka.Microservice.ProductService.UnitTest
{
    public class CategoryPanelServiceUnitTest
    {
        [Fact]
        public void ListCategorysGrid_ShouldReturnCorrectData()
        {
            // Arrange
            var page = 1;
            var size = 10;
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var expectedData = new PaginationModel<CategoryGridPanelDto>(new List<CategoryGridPanelDto>
            {
                new CategoryGridPanelDto {Id = 1 , Name = "Category1" ,  ProductCount = 5}

            }, 1, 1, 10);

            mockCategoryRepository.Setup(repo => repo.GetAllMapped<CategoryGridPanelDto>(null,null,null,false,page,size))
                                  .Returns(expectedData);

            // Act
            var categoryPanelService = new CategoryPanelService(mockCategoryRepository.Object);
            var result = categoryPanelService.GetCategorysGrid(page, size);

            // Assert
            Assert.Equal(200, result.Code);
            Assert.Equal(expectedData, result.Data);
        }

        [Fact]
        public async Task CreateCategory_ShouldReturnCorrectData()
        {
            // Arrange
            var categoryRequest = new CategoryPanelCreateDto
            {
                Name = "Category2",
                Description = "The second category"
            };
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var expectedData = new CategoryPanelDto
            {
                Id = 2,
                Name = "Category2",
                Description = "The second category"
            };
            mockCategoryRepository.Setup(repo => repo.SetAndCommit<CategoryPanelCreateDto, CategoryPanelDto>(categoryRequest, EntityCommandType.Create,null)).ReturnsAsync(expectedData);


            // Act
            var categoryPanelService = new CategoryPanelService(mockCategoryRepository.Object);
            var result = await categoryPanelService.CreateCategory(categoryRequest);

            // Assert
            Assert.Equal(201, result.Code);
            Assert.Equal(expectedData, result.Data);
        }
    }
}
