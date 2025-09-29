using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;

namespace User.Domain.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User.Domain.Models.User> Users { get; set; }              
        public DbSet<UserRole> Roles { get; set; }   // = default!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductsDb;Trusted_Connection=True;");

        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)       //proba zastosowania filtra do usuwania produktow- tak zeby sie nie zwracaly pozniej w get
        //{
        //    modelBuilder.Entity<User>().HasQueryFilter(p => !p.Deleted);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
