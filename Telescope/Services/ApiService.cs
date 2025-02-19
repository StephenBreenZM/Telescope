using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Auth0.OidcClient;
using Telescope.Model;

namespace Telescope.Services;

public class ApiService
{
    private readonly Auth0Client _auth0Client;
    private readonly HttpClient _httpClient;
    
    public ApiService(Auth0Client auth0Client)
    {
        _auth0Client = auth0Client;
        _httpClient = new HttpClient();
    }
    
    // public async Task<T> GetAsync<T>(string apiUrl)
    // {
    //     var accessToken = await GetAccessTokenAsync();

    //     _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    //     var response = await _httpClient.GetAsync(apiUrl);
    //     response.EnsureSuccessStatusCode();

    //     var responseContent = await response.Content.ReadAsStringAsync();
    //     return JsonSerializer.Deserialize<T>(responseContent);
    // }
    
    public async Task<List<PowerBiReport>> GetReportsList()
    {
        try
        {
            const string apiUrl = "https://ire.api.zeromission.ai/reports/powerBI";
            
            var loginResult = await _auth0Client.LoginAsync(new Dictionary<string,string>{["audience"]="https://api.zeromission.ai"} );
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);
            var apiResponse = await _httpClient.GetAsync(apiUrl);
            apiResponse.EnsureSuccessStatusCode();

            var responseContentAsString = await apiResponse.Content.ReadAsStreamAsync();
            var responseContent = JsonSerializer.Deserialize<List<PowerBiReport>>(responseContentAsString);
            if(responseContent is null)
                throw new Exception("No reports found");
            
            return responseContent;
        }
        catch (Exception e)
        {
            throw new Exception($"{e.Message}", e);
        }
    }
    
    // private async Task<string> GetAccessTokenAsync()
    // {
    //     var loginResult = await _auth0Client.LoginAsync();

    //     if (loginResult.IsError)
    //     {
    //         // Handle error
    //         throw new Exception($"Error getting access token: {loginResult.Error}");
    //     }

    //     return loginResult.AccessToken;
    // }
}