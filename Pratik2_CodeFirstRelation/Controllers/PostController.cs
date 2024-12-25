using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pratik2_CodeFirstRelation.Data;
using Pratik2_CodeFirstRelation.Data.Entity;

namespace Pratik2_CodeFirstRelation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PatikaSecondDbContext _context;

        public PostController(PatikaSecondDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult Get()
        {
            var posts = _context.Posts.Include(p => p.User).ToList();
            return Ok(posts);
        }

        [HttpGet("{id}")]

        public ActionResult GetById(int id)
        {
            var post = _context.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }
    }
}
