using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizza_App.Data;
using Pizza_App.Models;
using Pizza_App.Services;
namespace Pizza_App.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class PizzaController : ControllerBase
    {
        private readonly PizzaContext _context;
        public PizzaController(PizzaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetAll()
        {
            return await _context.Pizzas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> Get(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        [HttpPost]
        public async Task<ActionResult<Pizza>> Create(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
                return NotFound();

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
    }
}
