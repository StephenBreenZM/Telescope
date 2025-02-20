using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Auth0.OidcClient;
using Telescope.Model;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;

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
    
    public async Task<ObservableCollection<ReportView>> GetReportsList()
    {
        try
        {
            const string apiUrl = "https://ire.api.zeromission.ai/reports/powerBI/";
            
            var loginResult = await _auth0Client.LoginAsync(new Dictionary<string,string>{["audience"]="https://api.zeromission.ai"} );
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);
            
            var apiResponse = await _httpClient.GetAsync(apiUrl);
            apiResponse.EnsureSuccessStatusCode();

            var responseContentAsString = await apiResponse.Content.ReadAsStreamAsync();
            var responseContent = JsonSerializer.Deserialize<List<PowerBiReport>>(responseContentAsString);
            var powerBiReports = new ObservableCollection<ReportView>();
            
            if(responseContent is null)
                throw new Exception("No reports found");

            foreach (var report in responseContent)
            {
                if (report.Id == 0) continue;
                var reportResponse = await _httpClient.GetAsync(apiUrl+report.Id);
                if(reportResponse.StatusCode != HttpStatusCode.OK)
                { 
                    Debug.WriteLine("Issue with report id: " + report.Id);
                    continue;
                }
                var reportContent = await reportResponse.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(reportContent);
                var accessToken = jsonResult["accessToken"]!.Value<string>();
                var embedUrl = jsonResult["embedUrl"]!.Value<string>();
                if(reportContent is null)
                    throw new Exception("Wrong report id found");
                powerBiReports.Add(new ReportView()
                {
                    Name = report.Name,
                    AccessToken = accessToken,
                    Url = "https://app.powerbi.com/reportEmbed?reportId=f1796900-c791-4357-81e0-d0e166fb63a2&autoAuth=true&ctid=98d6686e-0da4-43d4-9d28-1f70cddd3861" // embedUrl
                });
            }
            return powerBiReports;
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