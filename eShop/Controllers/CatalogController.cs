using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;

        public CatalogController(CatalogContext context)
        {
            _context = context;
        }

        // GET: api/<CatalogController1>
        [HttpGet]
        public IEnumerable<CatalogItem> Get()
        {
            return _context.CatalogItems.ToList();
        }

        // GET api/<CatalogController1>/5
        [HttpGet("{id}")]
        public CatalogItem Get(int id)
        {
            return _context.CatalogItems.Find(id);
        }

        // POST api/<CatalogController1>
        [HttpPost]
        public IActionResult Post([FromBody] CatalogItem item)
        {
            _context.CatalogItems.Add(item);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/<CatalogController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
