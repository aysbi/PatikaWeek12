using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pratik2_CodeFirstRelation.Data;
using Pratik2_CodeFirstRelation.Data.Entity;

namespace Pratik2_CodeFirstRelation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PatikaSecondDbContext _context;

        public UserController(PatikaSecondDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult<List<User>> Get()
        {
            var users = _context.Users.Include(u => u.Posts).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]

        public ActionResult<User> GetById(int id)
        {
            var user = _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }


    }
}
