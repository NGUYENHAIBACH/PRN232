using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public ActionResult <IEnumerable<Post>> Get()
        {
            List<Post> posts = new List<Post>();
            posts.Add(new Post {
                UserId = 1,
                Id = 1,
                Title = "Post 1",
                Body = "This is the body of Post 1" });
            posts.Add(new Post {
                UserId = 2,
                Id = 2,
                Title = "Post 2",
                Body = "This is the body of Post 2" });
            return Ok(posts);
        }
    }
}
