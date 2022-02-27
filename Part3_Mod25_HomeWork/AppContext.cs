using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Part3_Mod25_HomeWork
{
    public class AppContext : DbContext 
    {
        //Tables....
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public AppContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=192.168.1.220;Database=Mod25HomeWork;User Id=sa;Password=Qwerty12;");
            optionsBuilder.UseInMemoryDatabase("Part3_Mod25_HomeWork");
        }

    }
}
