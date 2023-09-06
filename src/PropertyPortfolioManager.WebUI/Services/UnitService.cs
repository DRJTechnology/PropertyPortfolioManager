using Microsoft.Identity.Abstractions;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class UnitService : IUnitService
    {
        private readonly ILogger<UnitService> _logger;
        private IDownstreamApi _downstreamApi;

        public UnitService(ILogger<UnitService> logger, IDownstreamApi ppmService)
        {
            _logger = logger;
            _downstreamApi = ppmService;
        }

        public async Task<int> Create(UnitEditModel unit)
        {
            try
            {
                //var testJson = new StringContent(JsonSerializer.Serialize(unit), Encoding.UTF8, "application/json");
                // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/ignore-properties?pivots=dotnet-7-0#ignore-all-null-value-properties

                JsonSerializerOptions options = new()
                {
                    //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };

                var testJson = new StringContent(JsonSerializer.Serialize(unit, options), Encoding.UTF8, "application/json");
                // testJson.ReadAsStringAsync().Result;

                string relativePath = "/api/Unit/Create";

                var options2 = new DownstreamApiOptions()
                {
                    RelativePath = relativePath,
                    //Serializer = StringContent(JsonSerializer.Serialize(unit, options), Encoding.UTF8, "application/json")
                };

                // TODO Purchase & Sale Prices canot be null
                var response = await _downstreamApi.PostForUserAsync<UnitEditModel, ApiCreateResponse>(
                         "PpmWebApi",
                         unit,
                         testOptions()
                         //new Action<DownstreamApiOptionsReadOnlyHttpMethod>()
                         //{

                         //}
                         //options => options.Serializer = StringContent(JsonSerializer.Serialize(unit, options), Encoding.UTF8, "application/json")
                         //new Action<DownstreamApiOptionsReadOnlyHttpMethod>(

                         //    )
                         //options => options.RelativePath = relativePath
                         );

                if (response != null && response.Success)
                {
                    return response.CreatedId;
                }
                else
                {
                    throw new Exception(response != null ? response.ErrorMessage : "Error creating Unit!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Action<DownstreamApiOptionsReadOnlyHttpMethod>? testOptions()
        {
            return new Action<DownstreamApiOptionsReadOnlyHttpMethod>()
            {
                option
            }
        }

        public async Task<List<UnitBasicResponseModel>> GetAll()
        {
            try
            {
                string relativePath = "/api/Unit/GetAll";

                var value = await _downstreamApi.GetForUserAsync<List<UnitBasicResponseModel>>(
                         "PpmWebApi",
                         options => options.RelativePath = relativePath);
                return value;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<UnitResponseModel> GetById(int unitId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UnitEditModel unit)
        {
            throw new NotImplementedException();
        }
    }
}
