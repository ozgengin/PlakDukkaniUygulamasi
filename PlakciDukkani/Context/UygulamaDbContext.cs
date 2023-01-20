using Microsoft.EntityFrameworkCore;
using PlakciDukkani.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlakciDukkani.Context
{
    public class UygulamaDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=PlakciDukkaniDb;trusted_connection=true");
        }
        public DbSet<Album> Albumler { get; set; }
        public DbSet<Yonetici> Yoneticiler { get; set; }

    }
}
