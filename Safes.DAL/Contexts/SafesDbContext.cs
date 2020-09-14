using Microsoft.EntityFrameworkCore;
using Safes.Models.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safes.DAL.Contexts
{
    public class SafesDbContext : DbContext
    {
        public SafesDbContext(DbContextOptions<SafesDbContext> options)
            :base(options)
        {

        }
        public SafesDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Box>()
                .HasIndex(b => b.BoxId)
                .IsUnique();

            modelBuilder.Entity<StaticBox>()
                .HasIndex(b => b.SBoxId)
                .IsUnique();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Server=localhost;Port=5000;Database=Safes;User Id=postgres;Password=postgres;");
        //}
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Meditor> Meditors { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<StaticBox> StaticBoxes { get; set; }
        public DbSet<StaticBoxReuse> StaticBoxReuses { get; set; }
        public DbSet<PlaceEvent> Events { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Thank> Thanks { get; set; }
    }
}
