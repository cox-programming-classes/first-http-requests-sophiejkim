using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CS_First_HTTP_Client;

public class ApiService
{
    private readonly HttpClient _client = new()
    {
        BaseAddress = new("https://form-dev.winsor.edu")
    };

    private AuthResponse _auth { get; set; }

    private ApiService()
    {
    }

    public static readonly ApiService Current = new();


    public async Task<bool> AuthenticateAsync(Login login)
    {
        HttpRequestMessage request = new(HttpMethod.Post, "api auth");

        string jsonContent = JsonSerializer.Serialize(login);

        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Debug.WriteLine(responseContent);
            return false;
        }

        _auth = JsonSerializer.Deserialize<AuthResponse>(responseContent);
        return true;
    }

    public async Task<TOut?> SendAsync<TOut, TIn>(HttpMethod method, string endpoint, TIn content,
        bool authorize = true)
    {
        HttpRequestMessage request = new(method, endpoint);

        if (authorize)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _auth.jwt);
        }

        var jsonContent = JsonSerializer.Serialize(content);
        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        
        var response = await _client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer
                .Deserialize<TOut>(responseContent);
        Debug.WriteLine(responseContent);
        return default;
    }

    public async Task<TOut?> SendAsync<TOut>(HttpMethod method, string endpoint,
        bool authorize = true)
    {
        HttpRequestMessage request = new(method, endpoint);

        if (authorize)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _auth.jwt);
        }
        
        
        var response = await _client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer
                .Deserialize<TOut>(responseContent);
        Debug.WriteLine(responseContent);
        return default;
    }
    public async Task<bool> SendAsync<TIn>(HttpMethod method, string endpoint, TIn content,
        bool authorize = true)
    {
        HttpRequestMessage request = new(method, endpoint);

        if (authorize)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _auth.jwt);
        }

        var jsonContent = JsonSerializer.Serialize(content);
        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return true;
        Debug.WriteLine(responseContent);
        return false;
    }
}