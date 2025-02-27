using Microsoft.EntityFrameworkCore;
using Pizza_App.Models;

namespace Pizza_App.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options)
        : base(options)
        {
        }

        public DbSet<Pizza> Pizzas => Set<Pizza>();
    }
}
