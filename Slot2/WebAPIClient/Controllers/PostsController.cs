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
        string apiBaseUrl = GetGivenAPIBaseURL();
        string uri = $"{apiBaseUrl}/posts/{id}";
        string responseJson = "";
        Post post = null;

        string uriComments = $"{uri}/comments";
        List<Comment> comments = null;
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Post detail
            HttpResponseMessage response
                = await client.GetAsync(uri);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            post = JsonSerializer.Deserialize<Post>(responseJson, options);
            if (post != null)
            {
                Console.WriteLine($"Post: {post.Id} - {post.Title} written by {post.UserId}");
            }

            // Comments
            response = await client.GetAsync(uriComments);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            comments = JsonSerializer.Deserialize<List<Comment>>(responseJson, options);
            if (comments != null)
            {
                foreach (Comment c in comments)
                {
                    Console.WriteLine($"Comment: {c.Id}; {c.Name}; {c.Email}");
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        ViewBag.Comments = comments;

        return View(model: post);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        string apiBaseUrl = GetGivenAPIBaseURL();
        string uriUsers = $"{apiBaseUrl}/users";
        string responseJson = "";
        List<User> users = null;
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Users
            HttpResponseMessage response
                = await client.GetAsync(uriUsers);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            users = JsonSerializer.Deserialize<List<User>>(responseJson, options);
            if (users != null)
            {
                foreach (User u in users)
                {
                    Console.WriteLine($"User: {u.Id}; {u.Name} ({u.Email})");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        ViewBag.Users = users;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Post post)
    {
        string apiBaseUrl = GetGivenAPIBaseURL();
        string uri = $"{apiBaseUrl}/posts";
        string responseJson = "";
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Post to json
            string newPostJson = JsonSerializer.Serialize(post, options);
            StringContent requestBody
                = new StringContent(newPostJson, System.Text.Encoding.UTF8, "application/json");

            // Post create
            HttpResponseMessage response
                = await client.PostAsync(uri, requestBody);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

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
        //return View();
        return RedirectToAction("List");
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        string apiBaseUrl = GetGivenAPIBaseURL();
        string uri = $"{apiBaseUrl}/posts/{id}";
        string responseJson = "";
        Post post = null;

        string uriUsers = $"{apiBaseUrl}/users";
        List<User> users = null;
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Post detail
            HttpResponseMessage response
                = await client.GetAsync(uri);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            post = JsonSerializer.Deserialize<Post>(responseJson, options);
            if (post != null)
            {
                Console.WriteLine($"Post: {post.Id} - {post.Title} written by {post.UserId}");
            }

            // Users
            response = await client.GetAsync(uriUsers);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            users = JsonSerializer.Deserialize<List<User>>(responseJson, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        ViewBag.Users = users;

        return View(model: post);
    }
    [HttpPost]
    public async Task<IActionResult> Update(Post post)
    {
        string apiBaseUrl = GetGivenAPIBaseURL();
        string uri = $"{apiBaseUrl}/posts/{post.Id}";
        string responseJson = "";
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Post to json
            string updatePostJson = JsonSerializer.Serialize(post, options);
            StringContent requestBody
                = new StringContent(updatePostJson, System.Text.Encoding.UTF8, "application/json");

            // Post update
            HttpResponseMessage response
                = await client.PutAsync(uri, requestBody);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

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
        return RedirectToAction("List");
    }
    public async Task<IActionResult> Delete(int id)
    {
        string apiBaseUrl = GetGivenAPIBaseURL();
        string uri = $"{apiBaseUrl}/posts/{id}";
        string responseJson = "";
        try
        {
            // Post delete
            HttpResponseMessage response
                = await client.DeleteAsync(uri);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return RedirectToAction("List");
    }
}
