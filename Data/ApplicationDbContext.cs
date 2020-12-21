using System;
using newshangfire.Models;
using Microsoft.EntityFrameworkCore;

namespace newshangfire.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Article> Article { get; set; }

    }
}
