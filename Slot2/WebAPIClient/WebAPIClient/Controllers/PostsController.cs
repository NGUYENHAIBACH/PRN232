using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml.Linq;
using WebAPIClient.Models;

namespace WebAPIClient.Controllers
{
    public class PostsController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        private string getGivenAPIBaseUrl()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string baseUrl = config["GivenAPIBaseUrl"];
            return baseUrl;
        }
        public async Task<IActionResult> List()
        {
            string baseUrl = getGivenAPIBaseUrl();
            string uri = $"{baseUrl}/posts";
            string responseJson = "";
            List<Post> posts = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                //Console.WriteLine($"Status: {response.StatusCode}");
                responseJson = await response.Content.ReadAsStringAsync();
                //Console.WriteLine($"Data: {responseJson}");
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                posts = JsonSerializer.Deserialize<List<Post>>(responseJson, options);
                //if (posts != null)
                //{
                //    foreach (var post in posts)
                //    {
                //        Console.WriteLine($"Post ID: {post.Id}, Title: {post.Title}, written by {post.UserId}");
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(model: posts);
        }
        public async Task<IActionResult> Detail(int id)
        {
            string baseUrl = getGivenAPIBaseUrl();
            string uri1 = $"{baseUrl}/posts/{id}";
            string uri2 = $"{baseUrl}/comments?postId={id}";
            Post post = null;
            List<Comment> comments = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri1);
                string responseJson = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                post = JsonSerializer.Deserialize<Post>(responseJson, options);

                response = await client.GetAsync(uri2);
                responseJson = await response.Content.ReadAsStringAsync();
                comments = JsonSerializer.Deserialize<List<Comment>>(responseJson, options);

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
            string baseUrl = getGivenAPIBaseUrl();
            string uri = $"{baseUrl}/users";
            string responseJson = "";
            List<User> users = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                responseJson = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                users = JsonSerializer.Deserialize<List<User>>(responseJson, options);
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
            string baseUrl = getGivenAPIBaseUrl();
            string uri = $"{baseUrl}/posts";
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                
                string newPostJson = JsonSerializer.Serialize(post, options);
                StringContent requestBody = new StringContent(newPostJson, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, requestBody);
            }
            catch (Exception ex)
            {
            }
            //return View(post);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string baseUrl = getGivenAPIBaseUrl();
            string uri = $"{baseUrl}/posts/{id}";
            Post post = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                string responseJson = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                post = JsonSerializer.Deserialize<Post>(responseJson, options);

            }
            catch (Exception ex)
            {        
            }
            return View(model: post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Post post)
        {
            string baseUrl = getGivenAPIBaseUrl();
            string uri = $"{baseUrl}/posts/{post.Id}";
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                string updatedPostJson = JsonSerializer.Serialize(post, options);
                StringContent requestBody = new StringContent(updatedPostJson, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, requestBody);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int id)
        {
            string baseUrl = getGivenAPIBaseUrl();
            string uri = $"{baseUrl}/posts/{id}";
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("List");
        }
    }
}
