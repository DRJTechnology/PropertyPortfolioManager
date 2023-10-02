using Microsoft.Identity.Abstractions;
using PropertyPortfolioManager.Models.InternalObjects;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PropertyPortfolioManager.WebUI.Helpers
{
    public class PpmApiFacade : IPpmApiFacade
    {
        private readonly ILogger<PpmApiFacade> _logger;
        private readonly IDownstreamApi _downstreamApi;

        private const string apiServiceName = "PpmWebApi";
        private const string apiPrefix = "/api/";

        public PpmApiFacade(ILogger<PpmApiFacade> logger, IDownstreamApi ppmService)
        {
            _logger = logger;
            _downstreamApi = ppmService;
        }

        public async Task<int> PostAsync<TInput>(TInput obj, string relativePath)
        {
            JsonSerializerOptions jsonSerializerOptions = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            var response = await _downstreamApi.PostForUserAsync<TInput, PpmApiResponse>(
                         apiServiceName,
                         obj,
                         (opt) =>
                             {
                                 opt.RelativePath = apiPrefix + relativePath;
                                 opt.Serializer = (obj) => new StringContent(JsonSerializer.Serialize(obj, jsonSerializerOptions), Encoding.UTF8, "application/json");
                             }
                         );

            if (response != null && response.Success)
            {
                return response.CreatedId;
            }
            else
            {
                throw new Exception(response != null ? response.ErrorMessage : "Error creating entity!");
            }
        }

        public async Task<TOutput> GetAsync<TOutput>(string relativePath) where TOutput : class
        {
            try
            {
                //JsonSerializerOptions jsonSerializerOptions = new()
                //{
                //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                //    PropertyNameCaseInsensitive = true
                //};

                var value = await _downstreamApi.GetForUserAsync<TOutput>(
                         apiServiceName,
                            //(opt) =>
                            //{
                            //    opt.RelativePath = apiPrefix + relativePath;
                            //    opt.Deserializer = (content) => JsonSerializer.Deserialize<TOutput>(content.ReadAsStream(), jsonSerializerOptions);
                            //}
                         options => options.RelativePath = apiPrefix + relativePath
                         );
                return value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
