﻿using FoodyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodyAPI.Data
{

    public class APIContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }


        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();   // создаем бд с новой схемой
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=foodydb.cxa6afpccjzk.us-east-2.rds.amazonaws.com;" +
                "user=admin;" +
                "password=MyFoodyDB123;" +
                "database=foodydb;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.Favourite)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Statistics)
                .WithOne(e => e.user)
                .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<User>()
            //   .HasMany(em => em.Favourite)
            //   .WithOne(fa => fa.User)
            //   .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<User>()
            //    .HasMany(em => em.Statistics)
            //    .WithOne(fa => fa.user)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
