using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NIR.Models;

namespace NIR.Data
{
    public class NIRContext : DbContext
    {
        public NIRContext (DbContextOptions<NIRContext> options)
            : base(options)
        {
        }

        public DbSet<NIR.Models.User> User { get; set; } = default!;
        public DbSet<NIR.Models.Book> Book { get; set; } = default!;
        public DbSet<NIR.Models.Genre> Genre { get; set; } = default!;
    }
}
