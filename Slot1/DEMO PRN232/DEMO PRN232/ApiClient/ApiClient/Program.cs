using System.Text.Json;
using System.Threading.Tasks;


using ApiClient;
public class Program
{
    static readonly HttpClient client = new HttpClient();
    public static async Task Main(string[] args)
    {
        // get all posts 
        string url = "https://jsonplaceholder.typicode.com/posts";
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            Console.WriteLine($"Status: {response.StatusCode}");
            string responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");



            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };


            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(responseJson, options);
            if(posts != null)
            {
                foreach (Post post in posts)
                {
                    Console.WriteLine($"Post ID: {post.id}, Title: {post.title}");
                }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
    }
}