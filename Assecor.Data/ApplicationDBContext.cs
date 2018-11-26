using Assecor.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assecor.Data
{
   public class ApplicationDBContext : DbContext
   {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}