using Nest;
using System.Reflection.Metadata;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.SearchService.Model.Dtos;
using Tgyka.Microservice.SearchService.Services.Abstractions;
using Tgyka.Microservice.SearchService.Settings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tgyka.Microservice.SearchService.Services.Implementations
{
    public class ProductService: IProductService
    {
        private readonly ConnectionSettings _connectionSettings;
        private readonly ElasticSettings _elasticSetttings;
        private readonly ElasticClient _client;

        public ProductService(ElasticSettings elasticSetttings)
        {
            _elasticSetttings = elasticSetttings;
            _connectionSettings = new ConnectionSettings(new Uri(elasticSetttings.ConnectionUri))
                        .DefaultIndex(elasticSetttings.DefaultIndex);
            _client = new ElasticClient(_connectionSettings);

            if (!_client.Indices.Exists("productsearchs").Exists)
            {
                _client.Indices.Create("productsearchs",
                     index => index.Map<ProductResponseDto>(
                          x => x
                         .AutoMap()
                  ));
            }
        }

        public async Task<ApiResponseDto<List<ProductResponseDto>>> GetProducts(string searchString,int page,int size,bool priceIsDescending)
        {
            var data = (await _client.SearchAsync<ProductResponseDto>(s => s
                .From((page - 1) * size)
                .Size(size)
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f
                            .Field(p => p.Name, boost: 3)
                            .Field(p => p.Description, boost: 2)
                            .Field(p => p.CategoryName, boost: 1)
                        )
                        .Query(searchString)
                    )
                )
                .Sort(sort => sort
                    .Field(f => f.Field(p => p.Price).Order(priceIsDescending ? SortOrder.Descending : SortOrder.Ascending))
                ))).Documents.ToList();

            return ApiResponseDto<List<ProductResponseDto>>.Success(200, data);
        }

        public async Task<ApiResponseDto<ProductResponseDto>> CreateProduct(ProductResponseDto request)
        {
            await _client.IndexDocumentAsync(request);
            return ApiResponseDto<ProductResponseDto>.Success(200, request);
        }

        public async Task<ApiResponseDto<bool>> DeleteProduct(int productId)
        {
            var result = await _client.DeleteAsync<ProductResponseDto>(productId.ToString());
            return ApiResponseDto<bool>.Success(200, result.IsValid);
        }
    }
}
