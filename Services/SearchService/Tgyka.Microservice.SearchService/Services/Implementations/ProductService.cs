using Nest;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.SearchService.Model.Dtos;
using Tgyka.Microservice.SearchService.Services.Abstractions;
using Tgyka.Microservice.SearchService.Settings;

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

            if (!_client.Indices.Exists(_elasticSetttings.DefaultIndex).Exists)
            {
                _client.Indices.Create(_elasticSetttings.DefaultIndex,
                     index => index.Map<ProductDto>(
                          x => x
                         .AutoMap()
                  ));
            }
        }

        public async Task<ApiResponse<List<ProductDto>>> GetProducts(string searchString,int page,int size,bool priceIsDescending)
        {
            var data = (await _client.SearchAsync<ProductDto>(s => s
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

            return ApiResponse<List<ProductDto>>.Success(200, data);
        }

        public async Task<ApiResponse<ProductDto>> CreateProduct(ProductDto request)
        {
            var result = await _client.IndexAsync(request, i => i.Index(_elasticSetttings.DefaultIndex));

            if(!result.IsValid)
            {
                return ApiResponse<ProductDto>.Error(400,"Error");
            }
            
            return ApiResponse<ProductDto>.Success(200, request);
        }

        public async Task<ApiResponse<ProductDto>> UpdateProduct(ProductDto request)
        {
            var result = await _client.UpdateAsync<ProductDto>(request.Id, u => u
                  .Index(_elasticSetttings.DefaultIndex)
                  .Doc(request));
            return ApiResponse<ProductDto>.Success(200, request);
        }

        public async Task<ApiResponse<bool>> DeleteProduct(int productId)
        {
            var result = await _client.DeleteAsync<ProductDto>(productId.ToString());
            return ApiResponse<bool>.Success(200, result.IsValid);
        }
    }
}
