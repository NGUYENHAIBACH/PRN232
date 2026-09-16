using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApiClient.Models;

public class PostsController : Controller
{
    static readonly HttpClient client = new HttpClient();
    private string GetGivenAPIBaseURL()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        string baseURL = config["GivenAPIBaseUrl"];
        return baseURL;
    }
    public async Task<IActionResult> List()
    {
        string apiBaseUrl = GetGivenAPIBaseURL() + "";
        string uri = $"{apiBaseUrl}/posts";
        string responseJson = "";
        List<Post> posts = null;
        try
        {
            HttpResponseMessage response
                = await client.GetAsync(uri);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
             posts = JsonSerializer.Deserialize<List<Post>>(responseJson, options);
            if (posts != null)
            {
                foreach (Post post in posts)
                {
                    Console.WriteLine($"Post: {post.Id} - {post.Title} written by {post.UserId}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return View(posts);
    }
    public async Task<IActionResult> Detail(int id)
    {
        string apiBaseUrl = GetGivenAPIBaseURL() + "";
        string uri = $"{apiBaseUrl}/posts/{id}";
        string responseJson = "";
        Post post = null;
        try
        {
            HttpResponseMessage response
                = await client.GetAsync(uri);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            post = JsonSerializer.Deserialize<Post>(responseJson, options);
            if (post != null)
            {
                Console.WriteLine($"Post: {post.Id} - {post.Title} written by {post.UserId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return View(post);
    }
}