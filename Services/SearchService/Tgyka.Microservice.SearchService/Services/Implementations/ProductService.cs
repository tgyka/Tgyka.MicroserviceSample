using Microsoft.Extensions.Options;
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
        private readonly ElasticSettings _elasticSettings;
        private readonly ElasticClient _client;

        public ProductService(IOptions<ElasticSettings> elasticSetttings)
        {
            _elasticSettings = elasticSetttings.Value;

            _connectionSettings = new ConnectionSettings(new Uri(_elasticSettings.ConnectionUri))
                        .DefaultIndex(_elasticSettings.DefaultIndex)
                        .BasicAuthentication(_elasticSettings.Username, _elasticSettings.Password);

            _client = new ElasticClient(_connectionSettings);

            if (!_client.Indices.Exists(_elasticSettings.DefaultIndex).Exists)
            {
                _client.Indices.Create(_elasticSettings.DefaultIndex,
                     index => index.Map<ProductDto>(
                          x => x
                         .AutoMap()
                  ));
            }
        }

        public async Task<ApiResponse<List<ProductDto>>> GetProducts(string searchString,int page,int size,bool priceIsDescending)
        {
            var data = (await _client.SearchAsync<ProductDto>(s => s
                .Index(_elasticSettings.DefaultIndex)
                .From((page - 1) * size)
                .Size(size)
                .Query(q => q
                .QueryString(qs => qs
                .AnalyzeWildcard()
                    .Query("*" + searchString.ToLower() + "*")
                    .Fields(fs => fs
                        .Field(p => p.Name, boost: 3)
                        .Field(p => p.Description, boost: 2)
                        .Field(p => p.CategoryName, boost: 1)
                ))))).Documents.ToList();

            return ApiResponse<List<ProductDto>>.Success(200, data);
        }

        public async Task<ApiResponse<ProductDto>> CreateProduct(ProductDto request)
        {
            var result = await _client.IndexAsync(request, i => i.Index(_elasticSettings.DefaultIndex));

            if(!result.IsValid)
            {
                return ApiResponse<ProductDto>.Error(400,"Error");
            }
            
            return ApiResponse<ProductDto>.Success(200, request);
        }

        public async Task<ApiResponse<ProductDto>> UpdateProduct(ProductDto request)
        {
            var result = await _client.UpdateAsync<ProductDto>(request.Id, u => u
                  .Index(_elasticSettings.DefaultIndex)
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
