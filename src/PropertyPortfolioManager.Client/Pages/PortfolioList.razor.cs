using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PropertyPortfolioManager.Shared;
//using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Pages
{
	[Authorize]
	public partial class PortfolioList
    {
		private WeatherForecast[]? forecasts;
		private HttpClient _http;

		public PortfolioList(HttpClient http)
        {
			_http = http;
        }
        protected override async Task OnInitializedAsync()
		{
			try
			{
				forecasts = await _http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
			}
			catch (AccessTokenNotAvailableException exception)
			{
				exception.Redirect();
			}
		}
	}
}
