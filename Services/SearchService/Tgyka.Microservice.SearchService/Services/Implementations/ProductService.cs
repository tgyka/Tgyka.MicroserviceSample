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

            if (!_client.Indices.Exists("esearchitems").Exists)
            {
                _client.Indices.Create("esearchitems",
                     index => index.Map<ProductResponseDto>(
                          x => x
                         .AutoMap()
                  ));
            }
        }

        public async Task<ApiResponseDto<ProductResponseDto>> GetProducts(string searchString,int page,int size)
        {
            var data = await _client.SearchAsync<ProductResponseDto>(s => s
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
                    .Field(f => f.Field(p => p.Price).Order(SortOrder.Ascending))
                ));
            return ApiResponseDto<ProductResponseDto>(200, data);
        }
    }
}
