// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CS_First_HTTP_Client;

HttpClient client = new()
{
    BaseAddress = new Uri("https://forms-dev.winsor.edu")
};

#region Build Authentication Request

var login = new Login ("jinseo.kim@winsor.edu", "BTNrwo900%&!");

var jsonContent = JsonSerializer.Serialize(login);

var request= new HttpRequestMessage(HttpMethod.Post, requestUri: "api/auth");
request.Content= new StringContent(
    jsonContent, Encoding.UTF8, mediaType: "application/json");
    
    #endregion
    
    var response = await client.SendAsync(request);
    
    var jsonResponse = await response.Content.ReadAsStringAsync ();
    
    AuthResponse auth= JsonSerializer.Deserialize  <AuthResponse> (jsonResponse);
    
    Console.WriteLine(auth);
    
    request = new(HttpMethod.Get, requestUri: "api/users/self");
    request.Headers.Authorization
        =new AuthenticationHeaderValue(scheme:"Bearer", parameter:auth.jwt);
    
    response = await client.SendAsync (request);
    jsonResponse = await response. Content. ReadAsStringAsync();
    UserInfo user = JsonSerializer. Deserialize<UserInfo>(jsonResponse);
    Console.WriteLine(user);